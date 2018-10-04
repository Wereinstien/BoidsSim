using System;
using System.Collections.Generic;
using System.Windows;

namespace Engine
{
    public class Boid
    {
        Vector curr_pos;
        Vector vel;

        public double x
        {
            get { return curr_pos.X; }
        }

        public double y
        {
            get { return curr_pos.Y; }
        }

        const int radius_flock = 50;
        const int radius_space = 5;
        const int limit_vel = 5;

        const int x_min = 50;
        const int x_max = 650;
        const int y_min = 50;
        const int y_max = 550;

        public static List<Boid> all_boids = new List<Boid>();

        public Boid(double x, double y, double dx, double dy)
        {
            this.curr_pos = new Vector(x, y);
            this.vel = new Vector(dx, dy);
            all_boids.Add(this);
        }

        public void Move()
        {
            Vector v1 = Rule1();
            Vector v2 = Rule2();
            Vector v3 = Rule3();

            vel += v1 + v2 + v3;
            vel += Bounds();
            LimitVeloicity();

            curr_pos += vel;
        }

        private Vector Rule1()
        {
            Vector v1 = new Vector(0, 0);
            int n = 0;

            foreach(Boid b in all_boids)
            {
                if (!this.Equals(b) && Distance(this, b) < radius_flock)
                {
                    v1 += b.curr_pos;
                    n++;
                }
            }

            if (n == 0)
                n++;

            v1 /= n;

            return (v1 - curr_pos) / 100;
        }

        private Vector Rule2()
        {
            Vector v2 = new Vector(vel.X, vel.Y);

            foreach (Boid b in all_boids)
            {
                if (!this.Equals(b) && Distance(this, b) < radius_space)
                {
                    v2 -= b.curr_pos - curr_pos;
                }
            }

            return v2;
        }

        private Vector Rule3()
        {
            Vector v3 = new Vector(vel.X, vel.Y);
            int n = 0;

            foreach (Boid b in all_boids)
            {
                if (!this.Equals(b) && Distance(this, b) < radius_flock)
                {
                    v3 += b.vel;
                    n++;
                }
            }

            if (n == 0)
                n++;

            v3 /= n;

            return (v3 - vel) / 8;
        }

        private void LimitVeloicity()
        {
            if (vel.Length > limit_vel)
            {
                vel = vel / vel.Length * limit_vel;
            }
        }

        private Vector Bounds()
        {
            Vector b = new Vector(0, 0);

            if (curr_pos.X < x_min)
                b.X = 3;
            else if (curr_pos.X > x_max)
                b.X = -3;

            if (curr_pos.Y < y_min)
                b.Y = 2;
            else if (curr_pos.Y > y_max)
                b.Y = -2;

            return b;
        }

        private static double Distance(Boid a, Boid b)
        {
            return (double) (b.curr_pos - a.curr_pos).Length ;
        }

    }
 
}
