using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Drawing;
using System.Windows.Forms;

namespace Assets.Scripts.MazeSolver
{
    public struct Consts
    {
        public const double π = 3.1415926535897;  // π, pi,      half turn,      C/D
        public const double τ = 6.2831853071795;  // τ, tau,     full turn,      C/R
        public const double η = 1.5707963267948;  // η, eta,     quarter turn,   C/4R
        public const double η2 = 2 * η;
        public const double η3 = 3 * η;
        public const double η4 = 4 * η;

    }

    /// <summary>
    /// Integer coordinate system.
    /// </summary>
    /// <remarks>
    /// Contains various operator overloads for CoordI, CoordD, int, and double types.
    /// In cases where doubles are assigned or compared to integers, doubles are truncated.
    /// In cases where integers and doubles are combined via operations, integers are promoted.
    /// </remarks>
    /// <seealso cref="CoordD"/>
    public struct CoordI
    {
        public int x;
        public int y;

        public CoordI(CoordI other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public CoordI(CoordD other)
        {
            this.x = (int)other.x;
            this.y = (int)other.y;
        }

        public CoordI(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public CoordI(double x, double y)
        {
            this.x = (int)x;
            this.y = (int)y;
        }

        public void copy(CoordI other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public void copy(CoordD other)
        {
            this.x = (int)other.x;
            this.y = (int)other.y;
        }

        public static bool operator ==(CoordI c1, CoordI c2)
        {
            return (c1.x == c2.x) && (c1.y == c2.y);
        }

        public static bool operator ==(CoordI c1, CoordD c2)
        {
            return (c1.x == (int)c2.x) && (c1.y == (int)c2.y);
        }

        public static bool operator ==(CoordD c1, CoordI c2)
        {
            return ((int)c1.x == c2.x) && ((int)c1.y == c2.y);
        }

        public static bool operator !=(CoordI c1, CoordI c2)
        {
            return (c1.x != c2.x) || (c1.y != c2.y);
        }

        public static bool operator !=(CoordI c1, CoordD c2)
        {
            return (c1.x != c2.x) || (c1.y != c2.y);
        }

        public static bool operator !=(CoordD c1, CoordI c2)
        {
            return (c1.x != c2.x) || (c1.y != c2.y);
        }

        public static CoordI operator +(CoordI c1, CoordI c2)
        {
            return new CoordI(c1.x + c2.x, c1.y + c2.y);
        }

        public static CoordI operator +(CoordI c1, int a)
        {
            return new CoordI(c1.x + a, c1.y + a);
        }

        public static CoordD operator +(CoordI c1, CoordD c2)
        {
            return new CoordD(c1.x + c2.x, c1.y + c2.y);
        }

        public static CoordD operator +(CoordD c1, CoordI c2)
        {
            return new CoordD(c1.x + c2.x, c1.y + c2.y);
        }

        public static CoordI operator -(CoordI c1, CoordI c2)
        {
            return new CoordI(c1.x - c2.x, c1.y - c2.y);
        }

        public static CoordI operator -(CoordI c1, int a)
        {
            return new CoordI(c1.x - a, c1.y - a);
        }

        public static CoordD operator -(CoordI c1, CoordD c2)
        {
            return new CoordD(c1.x - c2.x, c1.y - c2.y);
        }

        public static CoordD operator -(CoordD c1, CoordI c2)
        {
            return new CoordD(c1.x - c2.x, c1.y - c2.y);
        }

        public static CoordI operator *(CoordI c1, CoordI c2)
        {
            return new CoordI(c1.x * c2.x, c1.y * c2.y);
        }

        public static CoordI operator *(CoordI c1, int a)
        {
            return new CoordI(c1.x * a, c1.y * a);
        }

        public static CoordD operator *(CoordI c1, CoordD c2)
        {
            return new CoordD(c1.x * c2.x, c1.y * c2.y);
        }

        public static CoordD operator *(CoordD c1, CoordI c2)
        {
            return new CoordD(c1.x * c2.x, c1.y * c2.y);
        }

        public static CoordI operator /(CoordI c1, CoordI c2)
        {
            return new CoordI(c1.x / c2.x, c1.y / c2.y);
        }

        public static CoordI operator /(CoordI c1, int a)
        {
            return new CoordI(c1.x / a, c1.y / a);
        }

        public static CoordD operator /(CoordI c1, CoordD c2)
        {
            return new CoordD((double)c1.x / c2.x, (double)c1.y / c2.y);
        }

        public static CoordD operator /(CoordD c1, CoordI c2)
        {
            return new CoordD(c1.x / (double)c2.x, c1.y / (double)c2.y);
        }
    }

    /// <summary>
    /// Double precision coordinate system.
    /// </summary>
    /// <remarks>
    /// Contains various operator overloads for CoordI, CoordD, int, and double types.
    /// Many of the operators relating CoordD to CoordI are in the CoordI struct.
    /// In cases where integers and doubles are combined via operations, integers are promoted.
    /// </remarks>
    /// <seealso cref="CoordI"/>
    public struct CoordD
    {
        public double x;
        public double y;

        public CoordD(CoordI other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public CoordD(CoordD other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public CoordD(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public CoordD(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void copy(CoordI other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public void copy(CoordD other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public static bool operator ==(CoordD c1, CoordD c2)
        {
            return (c1.x == c2.x) && (c1.y == c2.y);
        }

        public static bool operator !=(CoordD c1, CoordD c2)
        {
            return (c1.x != c2.x) || (c1.y != c2.y);
        }

        public static CoordD operator +(CoordD c1, CoordD c2)
        {
            return new CoordD(c1.x + c2.x, c1.y + c2.y);
        }

        public static CoordD operator +(CoordD c1, double s)
        {
            return new CoordD(c1.x + s, c1.y + s);
        }

        public static CoordD operator -(CoordD c1, CoordD c2)
        {
            return new CoordD(c1.x - c2.x, c1.y - c2.y);
        }

        public static CoordD operator -(CoordD c1, double s)
        {
            return new CoordD(c1.x - s, c1.y - s);
        }

        public static CoordD operator *(CoordD c1, CoordD c2)
        {
            return new CoordD(c1.x * c2.x, c1.y * c2.y);
        }

        public static CoordD operator *(CoordD c1, double s)
        {
            return new CoordD(c1.x * s, c1.y * s);
        }

        public static CoordD operator /(CoordD c1, CoordD c2)
        {
            return new CoordD(c1.x / c2.x, c1.y / c2.y);
        }

        public static CoordD operator /(CoordD c1, double s)
        {
            return new CoordD(c1.x / s, c1.y / s);
        }

        /// <summary>
        /// Calculates the magnitude of a vector from origin (0, 0) to this (x, y).
        /// </summary>
        /// <returns>Length of a vector from origin to this.</returns>
        public double length()
        {
            return Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// Calculates the location of a point <c>magnitude</c> down a line starting at this, in the direction of <c>theta</c>.
        /// </summary>
        /// <param name="theta">The angle to calculate by.</param>
        /// <param name="magnitude">The length to calculate for.</param>
        /// <returns>The CoordD of the calculated point.</returns>
        public CoordD by_angle(double theta, double magnitude)
        {
            return new CoordD(x + magnitude * Math.Cos(theta), y + magnitude * Math.Sin(theta));
        }

        /// <summary>
        /// Ensures both x and y are positive.
        /// </summary>
        public void abs()
        {
            this.x = Math.Abs(x);
            this.y = Math.Abs(y);
        }
    }

    /// <summary>
    /// A Maze generator and storage.
    /// </summary>
    /// <field name="walls">An array of true/false representing walls on/off.</field>
    /// <field name="flood">Flood fill from the goal of the maze for path finding.</field>
    /// <field name="order">The order of cardinal directions.</field>
    public struct Wall_grid
    {
        public CoordI size;
        private bool[] walls;
        private int[,] flood;

        private static Random rand = new Random();

        public static CoordI[] order = new CoordI[] {
            new CoordI(-1, +0), // west
            new CoordI(+0, -1), // north
            new CoordI(+1, +0), // east
            new CoordI(+0, +1)  // south
        };

        public Wall_grid(int x, int y)
        {
            size = new CoordI(x, y);
            walls = null;
            flood = null;
        }

        /// <summary>
        /// Gets the size of the walls array, not the size of the maze.
        /// </summary>
        /// <returns>Size of walls array.</returns>
        /// <seealso cref="size"/>
        public int get_size()
        {
            return walls.Length;
        }

        public void resize(int x, int y)
        {
            size.x = x;
            size.y = y;
        }

        public int get_path_value(int x, int y)
        {
            return flood[x, y];
        }

        public int get_path_value(CoordI c)
        {
            return flood[c.x, c.y];
        }

        /// <summary>
        /// Obtains the one dimensional index from a CoordI for a cell and direction for a wall.
        /// </summary>
        /// <param name="c">The coordinates of the cell.</param>
        /// <param name="dir">The direction of the wall.</param>
        /// <returns>The corresponding index of the walls array.</returns>
        public int get_wall(CoordI c, int dir)
        {
            if (dir < 3)
            {
                return 2 * (c.x + c.y * size.x) + dir; // west, north, east
            }
            return 2 * (c.x + (c.y + 1) * size.x) + 1; // south
        }

        /// <summary>
        /// Obtains the one dimensional index from coordinates for a cell and direction for a wall.
        /// </summary>
        /// <param name="x">The x coordinate of the cell.</param>
        /// <param name="y">The y coordinate of the cell.</param>
        /// <param name="dir">The direction of the wall.</param>
        /// <returns>The corresponding index of the walls array.</returns>
        public int get_wall(int x, int y, int dir)
        {
            if (dir < 3)
            {
                return 2 * (x + y * size.x) + dir; // west, north, east
            }
            return 2 * (x + (y + 1) * size.x) + 1; // south
        }

        public bool in_bounds(int dir)
        {
            return dir >= 0 && dir < 4;
        }

        public bool in_bounds(CoordI c)
        {
            return c.x >= 0 && c.x < size.x && c.y >= 0 && c.y < size.y;
        }

        public bool in_bounds(int x, int y)
        {
            return x >= 0 && x < size.x && y >= 0 && y < size.y;
        }

        public bool in_bounds(CoordI c, int dir)
        {
            return in_bounds(c) && in_bounds(dir) &&
                !(dir == 2 && c.x == size.x - 1) &&
                !(dir == 3 && c.y == size.y - 1);
        }

        public bool in_bounds(int x, int y, int dir)
        {
            return in_bounds(x, y) && in_bounds(dir) &&
                !(dir == 2 && x == size.x - 1) &&
                !(dir == 3 && y == size.y - 1);
        }

        /// <summary>
        /// Tests if a cell and wall are out of bounds, or if it is set.
        /// </summary>
        /// <param name="c">The coordinates of the cell.</param>
        /// <param name="dir">The direction of the wall to test.</param>
        /// <returns>True if the wall is out of bounds, or if the wall is set.</returns>
        public bool test_wall(CoordI c, int dir)
        {
            return !in_bounds(c, dir) || walls[get_wall(c, dir)];
        }

        /// <summary>
        /// Tests if a cell and wall are out of bounds, or if it is set.
        /// </summary>
        /// <param name="x">The x coordinate of the cell.</param>
        /// <param name="y">The y coordinate of the cell.</param>
        /// <param name="dir">The direction of the wall to test.</param>
        /// <returns>True if the wall is out of bounds, or if the wall is set.</returns>
        public bool test_wall(int x, int y, int dir)
        {
            return !in_bounds(x, y, dir) || walls[get_wall(x, y, dir)];
        }

        /// <summary>
        /// Recursive maze generation based on the "Growing tree" algorithm.
        /// Should be invoked with the goal coordinates of the maze, and no value for f.
        /// </summary>
        /// <param name="c">Coordinates generate from.</param>
        /// <param name="f">Flood fill value for this cell.</param>
        private void r_generate(CoordI c, int f = 1)
        {
            if (flood[c.x, c.y] == 0)
            {
                flood[c.x, c.y] = f;

                walls[get_wall(c, 0)] = true;
                walls[get_wall(c, 1)] = true;
                if (c.x < size.x - 1) walls[get_wall(c, 2)] = true;
                if (c.y < size.y - 1) walls[get_wall(c, 3)] = true;

                Stack<int> items = new Stack<int>(new int[] { 0, 1, 2, 3 }.OrderBy(n => rand.Next()).ToArray());

                while (items.Count > 0)
                {
                    int active = items.Pop();
                    CoordI new_c = c + order[active];

                    if (in_bounds(new_c) && flood[new_c.x, new_c.y] == 0)
                    {
                        r_generate(new_c, f + 1);
                        walls[get_wall(c, active)] = false;
                    }
                }
            }
        }

        /// <summary>
        /// Finds a point in the maze that is as far or further than any other point from the goal.
        /// </summary>
        /// <returns>The furthest point in the maze from the goal.</returns>
        public CoordI furthest_from_goal()
        {
            int largest = 0;
            CoordI coord = new CoordI(0, 0);

            for (int x = 0; x < size.x; ++x)
            {
                for (int y = 0; y < size.y; ++y)
                {
                    if (largest < flood[x, y])
                    {
                        largest = flood[x, y];
                        coord.x = x;
                        coord.y = y;
                    }
                }
            }

            return coord;
        }

        /// <summary>
        /// Generates a new maze.
        /// </summary>
        /// <param name="goal">The intended goal of the maze.</param>
        /// <returns>The intended start of the maze.</returns>
        public CoordI generate(CoordI goal)
        {
            walls = new bool[size.x * size.y * 2];
            flood = new int[size.x, size.y];
            r_generate(goal);
            return furthest_from_goal();
        }
    }

    /// <summary>
    /// A vector, angle, and magnitude. A ray. A velocty. A line.
    /// </summary>
    /// <field name="O">Origin.</field>
    /// <field name="v">How far the ray is cast down it's magnitude.</field>
    /// <field name="θ">Angle.</field>
    /// <field name="r">Magnitude.</field>
    public struct Ray
    {
        public CoordD O;
        public double v;
        public double θ;
        public double r;

        public Ray(CoordD origin, double θ, double r)
        {
            O = origin;
            this.r = r;
            v = 1.0;

            this.θ = bound_angle(θ);
        }

        /// <summary>
        /// Ensures that a given angle is between 0 (inclusive) and τ (exclusive).
        /// </summary>
        /// <param name="θ">Angle.</param>
        /// <returns>The correct angle.</returns>
        public static double bound_angle(double θ)
        {
            while (θ < 0) θ += Consts.τ;
            while (θ >= Consts.τ) θ -= Consts.τ;
            return θ;
        }

        public double get_angle()
        {
            return θ;
        }

        public bool IsToLeft() { return get_angle() >= 1.57 && get_angle() <= 4.7; }

        public bool going_north()
        {
            return θ >= Consts.η2 && θ < Consts.η4;
        }

        public bool going_east()
        {
            return θ >= Consts.η3 || θ < Consts.η;
        }

        public bool going_south()
        {
            return θ < Consts.η2 && θ >= 0;
        }

        public bool going_west()
        {
            return θ < Consts.η3 && θ >= Consts.η;
        }

        public bool north_quad()
        {
            return θ >= Consts.η * 2.5 && θ < Consts.η * 3.5;
        }

        public bool east_quad()
        {
            return θ >= Consts.η * 3.5 || θ < Consts.η * 0.5;
        }

        public bool south_quad()
        {
            return θ >= Consts.η * 0.5 && θ < Consts.η * 1.5;
        }

        public bool west_quad()
        {
            return θ >= Consts.η * 1.5 && θ < Consts.η * 2.5;
        }

        public void set_angle(double θ)
        {
            this.θ = bound_angle(θ);
        }

        public CoordD end_point()
        {
            return O.by_angle(θ, r);
        }

        public CoordD cast_point()
        {
            return O.by_angle(θ, r * v);
        }

        /// <summary>
        /// Draws with the a default colour.
        /// </summary>
        /// <param name="e">Event data.</param>
        public void draw(PaintEventArgs e)
        {
            draw(e, new Pen(Color.FromArgb(255, 191, 255, 0)));
        }

        /// <summary>
        /// Draws with the alpha channel of a given pen modified by <c>v</c>.
        /// </summary>
        /// <param name="e">Event data.</param>
        /// <param name="p">The <c>Pen</c> to use.</param>
        public void draw(PaintEventArgs e, Pen p)
        {
            Pen p1 = new Pen(Color.FromArgb((int)((1.0 - Math.Max(0.0, v)) * 240) + 15, p.Color.R, p.Color.G, p.Color.B));
            CoordD end = cast_point();

            if (O.x >= e.ClipRectangle.X && O.x < e.ClipRectangle.Width &&
                O.y >= e.ClipRectangle.Y && O.y < e.ClipRectangle.Height)
                e.Graphics.DrawLine(p1,
                    new PointF((float)O.x, (float)O.y),
                    new PointF((float)end.x, (float)end.y));
        }

        /// <summary>
        /// Draws with a given pen and differed length.
        /// </summary>
        /// <param name="e">Event data.</param>
        /// <param name="p">The <c>Pen</c> to use.</param>
        /// <param name="val">Length.</param>
        public void draw(PaintEventArgs e, Pen p, double val)
        {
            CoordD end = O.by_angle(θ, val);

            if (O.x >= e.ClipRectangle.X && O.x < e.ClipRectangle.Width &&
                O.y >= e.ClipRectangle.Y && O.y < e.ClipRectangle.Height)
                e.Graphics.DrawLine(p,
                    new PointF((float)O.x, (float)O.y),
                    new PointF((float)end.x, (float)end.y));
        }
    }

    public class Maze
    {
        public Wall_grid walls;
        public CoordI start;
        public CoordI goal;
        public CoordD cell_size;
        public Rectangle draw_rect;
        private StringFormat font_format;
        private static Random rand = new Random();
        private static Font font = new Font("Deja vu Sans", 12);

        public Maze(int width, int height, int window_w, int window_h)
        {
            walls = new Wall_grid(width, height);

            draw_rect = new Rectangle(0, 0, window_w, window_h);
            cell_size = new CoordD((double)window_w / (double)width, (double)window_h / (double)height);

           font_format = new StringFormat();
           font_format.Alignment = StringAlignment.Center;
           font_format.LineAlignment = StringAlignment.Center;

            generate();
        }

        public void scale(int width, int height)
        {
            draw_rect = new Rectangle(0, 0, width, height);
            cell_size = new CoordD((double)width / (double)walls.size.x, (double)height / (double)walls.size.y);
        }

        public void resize(int width, int height)
        {
            walls.resize(width, height);
        }

        public void generate()
        {
            goal = new CoordI(rand.Next(0, 2) * (walls.size.x - 1), rand.Next(0, 2) * (walls.size.y - 1));
            start = walls.generate(goal);
        }

        public void draw(PaintEventArgs e)
        {
            for (int x = 0; x < walls.size.x; ++x)
            {
                for (int y = 0; y < walls.size.y; ++y)
                {
                    for (int d = 0; d < 2; ++d)
                    {
                        if (walls.test_wall(x, y, d))
                        {
                            float x1 = x * (float)cell_size.x;
                            float y1 = y * (float)cell_size.y;

                            e.Graphics.DrawLine(Pens.White, x1, y1,
                                x1 + (d == 0 ? 0 : (float)cell_size.x),
                                y1 + (d == 1 ? 0 : (float)cell_size.y)
                            );
                        }
                    }
                }
            }

            e.Graphics.DrawLine(Pens.White, (float)cell_size.x * (float)walls.size.x, 0, (float)cell_size.x * (float)walls.size.x, (float)cell_size.y * (float)walls.size.y);
            e.Graphics.DrawLine(Pens.White, 0, (float)cell_size.y * (float)walls.size.y, (float)cell_size.x * (float)walls.size.x, (float)cell_size.y * (float)walls.size.y);

            Rectangle r1 = new Rectangle((int)(start.x * cell_size.x), (int)(start.y * cell_size.y), (int)cell_size.x, (int)cell_size.y);
            e.Graphics.DrawString("S", font, Brushes.Red, r1, font_format);

            Rectangle r2 = new Rectangle((int)(goal.x * cell_size.x), (int)(goal.y * cell_size.y), (int)cell_size.x, (int)cell_size.y);
            e.Graphics.DrawString("G", font, Brushes.Green, r2, font_format);
        }

        /// <summary>
        /// Raycasts a given <c>Ray</c> in this maze.
        /// </summary>
        /// <param name="ray">The <c>Ray</c> to cast.</param>
        /// <param name="radius">The radius of the thing casting the ray.</param>
        public void raycast(ref Ray ray, double radius)
        {
            if (ray.r == 0)
            {
                ray.v = 0;
                return;
            }

            CoordI grid_coords = new CoordI(ray.O / cell_size);
            int dir = -1;
            double len = 0;
            double Ax;
            double Ay;

            bool walled = false;
            bool south = ray.going_south();
            bool east = ray.going_east();

            if ((south && !east) || (!south && east))
            {
                Ax = 1.0 / Math.Cos(Consts.η - ray.θ % Consts.η);
                Ay = 1.0 / Math.Cos(ray.θ % Consts.η);
            }
            else
            {
                Ax = 1.0 / Math.Cos(ray.θ % Consts.η);
                Ay = 1.0 / Math.Cos(Consts.η - ray.θ % Consts.η);
            }

            CoordD test_coord = ray.O - grid_coords * cell_size;
            CoordD test_space = cell_size - test_coord;

            if (walls.test_wall(grid_coords, 2) && test_space.x < radius)
                ray.O.x -= radius - test_space.x;
            else if (walls.test_wall(grid_coords, 3) && test_space.y < radius)
                ray.O.y -= radius - test_space.y;
            else if (walls.test_wall(grid_coords, 0) && test_coord.x < radius)
                ray.O.x += radius - test_coord.x;
            else if (walls.test_wall(grid_coords, 1) && test_coord.y < radius)
                ray.O.y += radius - test_coord.y;

            while (len < ray.r && !walled)
            {
                CoordD grid_space = ray.O.by_angle(ray.θ, len) - grid_coords * cell_size;
                CoordD cell_space = cell_size - grid_space;
                grid_space.abs();
                cell_space.abs();

                double hx = (east ? cell_space.x : grid_space.x) * Ax;
                double hy = (south ? cell_space.y : grid_space.y) * Ay;

                if (hx < hy)
                    len += (east ? Math.Max(0, cell_space.x - radius) : Math.Max(0, grid_space.x - radius)) * Ax;
                else
                    len += (south ? Math.Max(0, cell_space.y - radius) : Math.Max(0, grid_space.y - radius)) * Ay;

                dir = hx < hy ? 0 : 1;
                if ((hx < hy && east) || (hx >= hy && south)) dir += 2;

                walled = !walls.in_bounds(grid_coords, dir) || walls.test_wall(grid_coords, dir);

                if (!walled)
                {
                    if (dir == 0) grid_coords.x -= 1;
                    else if (dir == 1) grid_coords.y -= 1;
                    else if (dir == 2) grid_coords.x += 1;
                    else if (dir == 3) grid_coords.y += 1;
                }
            }

            ray.v = Math.Min(len, ray.r) / ray.r;
        }
    }
}
