using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using System.Diagnostics;

namespace Flocks
{
    public partial class Flock : Form
    {

        List<Boid> all_boids;
        Timer tm;

        public Flock()
        {
            InitializeComponent();

            all_boids = new List<Boid>();
            tm = new Timer();
            tm.Tick += new EventHandler(Update);
            tm.Interval = 10;
            tm.Enabled = true;
            tm.Start();

        }

        private void Add_Boid(object sender, MouseEventArgs e)
        {
            Random r = new Random();
            Point m = this.PointToClient(Cursor.Position);
            for (int _ = 0; _ < 10; _++)
                all_boids.Add(new Boid(m.X, m.Y, r.Next(-5, 5), r.Next(-3, 3)));
        }

        private void Boid_Draw(object sender, PaintEventArgs e)
        {
            foreach (Boid b in all_boids) {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 1), (float) b.x, (float) b.y, 5, 5);
            }
        }

        private void Update(object sender, EventArgs e)
        {
            Refresh();
            
            foreach (Boid b in all_boids)
            {
                b.Move();
            }
        }

        private void Flock_Load(object sender, EventArgs e)
        {

        }
    }
}
