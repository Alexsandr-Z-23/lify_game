using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lify_game
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        private GameEgein gameEgein;
        public Form1()
        {
            InitializeComponent();
        }
        private void StartGame()
        {
            if (timer1.Enabled)
                return;
            numPlot.Enabled = false;
            numRezolu.Enabled = false;
            BTStart.Enabled = false;
            resolution = (int)numRezolu.Value;
            gameEgein = new GameEgein
                (
                rows: pictureBox1.Height / resolution,
                cols:pictureBox1.Width/resolution,
                density:(int)(numPlot.Minimum)+(int)(numPlot.Maximum)-(int)numPlot.Value
                ) ;
            Text = $"Generation{gameEgein.CurrentGeneration}";

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();

        }
        private void DrawNextGenretion()
        
        {
            graphics.Clear(Color.Black);
            var field = gameEgein.GetCurrentGeneration();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j])graphics.FillRectangle(Brushes.Crimson, i * resolution, j * resolution, resolution - 1, resolution - 1);
                }
            }
            pictureBox1.Refresh();
            Text = $"Generation{gameEgein.CurrentGeneration}";
            gameEgein.NextGenration();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextGenretion();
        }

        private void BTStart_Click(object sender, EventArgs e)
        {
            StartGame();    
        }

        private void BTStop_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
            numPlot.Enabled = true;
            numRezolu.Enabled = true;
            BTStart.Enabled = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                    var y=e.Location.Y/resolution; 
                gameEgein.AddCell(x, y );
            }
            if (e.Button == MouseButtons.Right)
            {
                var x =e.Location.X / resolution;
                var y =e.Location.Y/resolution;
                gameEgein.RemoveCell(x, y );
            }
        }
    }
}
