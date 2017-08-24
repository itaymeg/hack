using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assets.Scripts.MazeSolver
{
    public partial class Maze_Form : Form
    {
        public Maze_Form()
        {
            InitializeComponent();
            this.Size = new Size(MazeManager.settings.win_width, MazeManager.settings.win_height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MazeManager.mouse_tracker = new Eye_Tester(360, 200.0);
            this.MouseWheel += Form1_MouseWheel;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MazeManager.mouse_tracker.move(e.X, e.Y);
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            MazeManager.mouse_tracker.adjust(e.Delta * SystemInformation.MouseWheelScrollLines / 30);
        }

        private void Form1_LeftClick(object sender, MouseEventArgs e)
        {
            MazeManager.mouse_tracker.visible = !MazeManager.mouse_tracker.visible;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)
            {
                MazeManager.mouse_tracker.radius = 3.0 - MazeManager.mouse_tracker.radius;
                e.Handled = true;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (MazeManager.genalg != null)
                MazeManager.genalg.update(e);

            MazeManager.mouse_tracker.update_eyes(MazeManager.genalg.maze);
            MazeManager.mouse_tracker.draw(e);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (MazeManager.genalg != null)
                MazeManager.genalg.maze.scale(control.ClientSize.Width - 1, control.ClientSize.Height - 1);
        }

        private void tmrApp_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Maze_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            MazeManager.menu_form.Show();
        }
    }
}
