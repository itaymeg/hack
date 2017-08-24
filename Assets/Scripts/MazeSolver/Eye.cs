using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assets.Scripts.MazeSolver
{
    public class Eye
    {
        public Ray ray;
        public double offset;
        public double radius;

        public Eye(CoordD origin, double radius, double theta, double offset, double magnitude)
        {
            this.radius = radius;
            ray = new Ray(origin.by_angle(theta + offset, radius), theta, magnitude);

            this.offset = offset;
            while (this.offset < 0) this.offset += Consts.τ;
            while (this.offset >= Consts.τ) this.offset -= Consts.τ;
        }

        public void draw(PaintEventArgs e)
        {
            ray.draw(e);
        }

        public void draw(PaintEventArgs e, Pen p)
        {
            ray.draw(e, p);
        }

        public void set_angle(double theta)
        {
            ray.set_angle(theta + offset);
        }

        public void turn(double theta)
        {
            ray.set_angle(ray.θ + theta);
        }

        public void move(CoordD pos)
        {
            ray.O = pos.by_angle(ray.θ, radius);
        }
    }
}
