using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assets.Scripts.MazeSolver
{
    class Bot
    {
        public ImageLooper ImageLooper { get; set; }
        public Ray velocity;
        public CoordI grid_pos;
        public double energy;
        public bool alive;
        public bool goal;

        private double radius;
        private double speed;
        private double max_energy;
        private double energy_step;
        public Brain brain;
        private Eye[] eyes;
        private int num_eyes;

        private Maze m;

        public Bot(ref Maze m)
        {
            ImageLooper = new ImageLooper();
            this.m = m;
            num_eyes = MazeManager.settings.bot_eyes;
            energy_step = 1.0;

            int neurons = num_eyes +
                (MazeManager.settings.think_distance ? 1 : 0) +
                (MazeManager.settings.think_path ? 5 : 0) +
                (MazeManager.settings.think_velocity ? 3 : 0);

            brain = new Brain(neurons, neurons, 2);
            eyes = new Eye[num_eyes];
            velocity = new Ray(new CoordD(0.5, 0.5), 0.0, MazeManager.settings.bot_speed);

            for (int i = 0; i < num_eyes; ++i)
            {
                double theta = (double)i / (double)num_eyes * Consts.η2 - Consts.η;
                eyes[i] = new Eye(velocity.O, 2, 0, theta, 100);
            }

            init(new CoordD(0, 0));
        }

        /// <summary>
        /// Initialise in a given position.
        /// </summary>
        /// <param name="pos">The position to move to.</param>
        public void init(CoordD pos)
        {
            max_energy = MazeManager.settings.maze_time;
            num_eyes = MazeManager.settings.bot_eyes;
            radius = MazeManager.settings.bot_radius;

            alive = true;
            goal = false;
            energy = max_energy;
            grid_pos = new CoordI(pos / m.cell_size);

            move(pos);
            turn_to(0.0);
        }

        /// <summary>
        /// Act on given brain outputs.
        /// </summary>
        /// <param name="force">The force to move forwards with.</param>
        /// <param name="theta">The degree to turn by.</param>
        public void act(float force, float theta)
        {
            if (!alive)
                return;
            else if (grid_pos == m.goal)
            {
                alive = false;
                goal = true;
                return;
            }

            turn((theta * 2 - 1) / Consts.η);
            forward(force);
            speed = force * velocity.v * velocity.r;
            this.energy = Math.Max(0.0, energy - energy_step);

            if (energy == 0.0) alive = false;
        }

        /// <summary>
        /// Use the <c>Brain</c> with variable inputs based on <c>Program.settings</c>.
        /// </summary>
        public void think()
        {
            if (!alive) return;

            for (int i = 0; i < num_eyes; ++i)
                brain.set_input(i, (float)eyes[i].ray.v);

            int extra = 0;

            if (MazeManager.settings.think_distance)
            {
                brain.set_input(num_eyes + extra++, (float)(((m.goal * m.cell_size - velocity.O) / ((m.walls.size + 1) * m.cell_size)).length()));
            }

            if (MazeManager.settings.think_path)
            {
                int this_pos = m.walls.get_path_value(m.start) + 1;

                if (m.walls.in_bounds(grid_pos))
                    this_pos = m.walls.get_path_value(grid_pos);

                brain.set_input(num_eyes + extra++, (float)this_pos / (float)m.walls.get_path_value(m.start));

                int dir = 0;
                if (velocity.north_quad()) dir = 1;
                else if (velocity.east_quad()) dir = 2;
                else if (velocity.south_quad()) dir = 3;

                for (int i = 0; i < 4; ++i)
                {
                    int index = (i + dir) % 4;
                    if (m.walls.test_wall(grid_pos, index) || this_pos < m.walls.get_path_value(grid_pos + Wall_grid.order[index]))
                        brain.set_input(num_eyes + extra++, 0);
                    else
                        brain.set_input(num_eyes + extra++, 1);
                }
            }

            if (MazeManager.settings.think_velocity)
            {
                CoordD magnitude = new CoordD(0, 0).by_angle(velocity.θ, 1.0);

                brain.set_input(num_eyes + extra++, (float)magnitude.x);
                brain.set_input(num_eyes + extra++, (float)magnitude.y);
                brain.set_input(num_eyes + extra++, (float)speed);
            }

            brain.think();

            act(brain.get_output(0), brain.get_output(1));
        }

        public void move(int x, int y)
        {
            velocity.O.x = x;
            velocity.O.y = y;
            grid_pos = new CoordI(velocity.O / m.cell_size);

            for (int i = 0; i < num_eyes; ++i)
                eyes[i].move(velocity.O);
        }

        public void move(CoordD pos)
        {
            velocity.O.x = pos.x;
            velocity.O.y = pos.y;
            grid_pos = new CoordI(velocity.O / m.cell_size);

            for (int i = 0; i < num_eyes; ++i)
                eyes[i].move(velocity.O);
        }

        public void move(CoordI pos)
        {
            velocity.O.x = pos.x;
            velocity.O.y = pos.y;
            grid_pos = new CoordI(velocity.O / m.cell_size);

            for (int i = 0; i < num_eyes; ++i)
                eyes[i].move(velocity.O);
        }

        /// <summary>
        /// Move forward based on a raycast to detect collisions.
        /// </summary>
        /// <param name="s">The speed to move at.</param>
        public void forward(double s)
        {
            m.raycast(ref velocity, radius);
            CoordD to = velocity.O + ((velocity.cast_point() - velocity.O) * s);
            move(to);
        }

        public void turn_to(double theta)
        {
            for (int i = 0; i < eyes.Length; ++i)
                eyes[i].set_angle(theta);

            velocity.set_angle(theta);
        }

        public void turn(double theta)
        {
            for (int i = 0; i < eyes.Length; ++i)
                eyes[i].turn(theta);

            velocity.set_angle(theta + velocity.θ);
        }

        public void update_eyes(Maze m)
        {
            for (int i = 0; i < eyes.Length; ++i)
                m.raycast(ref eyes[i].ray, 0.0);
        }

        public void draw(PaintEventArgs e)
        {
            if (goal)
                for (int i = 0; i < eyes.Length; ++i)
                    eyes[i].draw(e, Pens.PowderBlue);
            else if (alive)
                for (int i = 0; i < eyes.Length; ++i)
                    eyes[i].draw(e, Pens.GreenYellow);
            else
                for (int i = 0; i < eyes.Length; ++i)
                    eyes[i].draw(e, Pens.Red);

            velocity.draw(e, Pens.Purple, 10.0 * speed);
            //e.Graphics.DrawImage(ImageLooper.GetNext(), (float)velocity.O.x - 6.0f, (float)velocity.O.y - 6.0f, 12.0f, 12.0f);
            e.Graphics.DrawImage(RotateImage(ImageLooper.GetNext(),velocity), (float)velocity.O.x - 6.0f-30, (float)velocity.O.y - 6.0f-50, 70, 70);
        }

        public Image RotateImage(Image image,Ray ray)
        {
            if (ray.IsToLeft())
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
            return image;
        }
    }

    public class ImageLooper
    {
        public int Index { get; set; }
        public string[] Bitmaps { get; set; }
        public ImageLooper()
        {
            Bitmaps = new string[] {
                @"C:\code\hack\Assets\dog\Run (1).png" ,
                @"C:\code\hack\Assets\dog\Run (2).png" ,
                @"C:\code\hack\Assets\dog\Run (3).png" ,
                @"C:\code\hack\Assets\dog\Run (4).png" ,
                @"C:\code\hack\Assets\dog\Run (5).png" ,
                @"C:\code\hack\Assets\dog\Run (6).png" ,
                @"C:\code\hack\Assets\dog\Run (7).png" ,
                @"C:\code\hack\Assets\dog\Run (8).png" ,
            };
        }

        public Bitmap GetNext()
        {
            Index = Index + 1;
            if (Index > Bitmaps.Length - 1)
            {
                Index = 0;
            }
            var bitmap = new Bitmap(Bitmaps[Index]);
            //bitmap= ResizeImage(bitmap, 1000, 1000);
            return bitmap;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
