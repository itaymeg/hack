using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assets.Scripts.MazeSolver
{
    public class Eye_Tester
    {
        public CoordD position;
        public Eye[] eyes;
        public int num_eyes;
        public bool visible;
        public double radius;

        public Eye_Tester(int number_of_eyes, double magnitude)
        {
            radius = 0.0;
            visible = false;
            num_eyes = number_of_eyes;
            eyes = new Eye[num_eyes];
            for (int i = 0; i < num_eyes; ++i)
            {
                eyes[i] = new Eye(position, 0.0, (double)i / (double)num_eyes * Consts.τ, 0.0, magnitude);
            }
        }

        public void adjust(double magnitude)
        {
            for (int i = 0; i < eyes.Length; ++i)
                eyes[i].ray.r = Math.Max(0.1, eyes[i].ray.r + magnitude);
        }

        public void move(int x, int y)
        {
            position.x = x;
            position.y = y;

            for (int i = 0; i < eyes.Length; ++i)
                eyes[i].ray.O.copy(position);
        }

        public void update_eyes(Maze m)
        {
            if (!visible) return;
            for (int i = 0; i < eyes.Length; ++i)
                m.raycast(ref eyes[i].ray, radius);
        }

        public void draw(PaintEventArgs e)
        {
            if (!visible) return;
            for (int i = 0; i < eyes.Length; ++i)
                eyes[i].draw(e);
        }
    }
}
