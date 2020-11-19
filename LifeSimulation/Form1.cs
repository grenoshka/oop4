using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LifeSimulation
{
    public partial class Form1 : Form
    {
        public Graphics graphics;
        Runner LifeRunner;
        public int scale;
        private int PBInitialWidth;
        private int PBInitialHeight;


        Dictionary<string, Brush> Colors = new Dictionary<string, Brush>
        {
            {"main", Brushes.GreenYellow },
            {"grass" , Brushes.Green},
            {"water",  Brushes.LightSkyBlue},
            {"male",  Brushes.Black},
            {"female",  Brushes.BlueViolet},
            {"infected",  Brushes.LightGray},
            {"food", Brushes.Brown}
        };

        public Form1()
        {
            InitializeComponent();
            Load += Form1Load;
            Shown += Form1Shown;
        }


        private void Form1Load(object sender, EventArgs e)
        {
            PBInitialWidth = pictureBox1.Width;
            PBInitialHeight = pictureBox1.Height;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            scale = (int)nudResolution.Value;
            LifeRunner = new Runner(this);
            LifeRunner.StartLife();
        }


        private void Form1Shown(object sender, EventArgs e)
        {
            timer1.Start();
        }

        public void DrawCell(ref Cell cell,int x, int y)
        {
            if (cell.ContainsCreature == null && cell.ContainsFood == false)
                graphics.FillRectangle(Colors[cell.BiomType.ToString()], x * scale, y * scale, scale, scale);
            else if (cell.ContainsCreature != null)
            {
                Creature<Food> Creature = LifeRunner.LifeField.GetCreatureInCell(x, y);
                if (!Creature.Infected)
                    graphics.FillRectangle(Colors[Creature.Gender.ToString()], x * scale, y * scale, scale, scale);
                else
                    graphics.FillRectangle(Colors["infected"], x * scale, y * scale, scale, scale);
            }
            else
                graphics.FillRectangle(Colors["food"], x * scale, y * scale, scale, scale);
        }

        public void DrawBiom(Biom biom)
        {
            graphics.FillRectangle(Colors[biom.BiomType.ToString()], biom.x, biom.y, biom.BiomWidth*scale, biom.BiomHeight*scale);
        }

        public void RefreshField()
        {
            pictureBox1.Refresh();
        }

        private void RedrawScaledField()
        {
            for (int x=0; x<PBInitialWidth; x++)
            {
                for (int y=0; y<PBInitialHeight; y++)
                {
                    DrawCell(ref LifeRunner.LifeField.Cells[x, y], x, y);
                }
            } 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LifeRunner.SimulateLife();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scale = (int)nudResolution.Value;
            pictureBox1.Width = PBInitialWidth * scale;
            pictureBox1.Height = PBInitialHeight * scale;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            RedrawScaledField();
        }

        private void Stop_MouseClick(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
        }

        private void Resume_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return;
            timer1.Enabled = true;
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / scale;
                var y = e.Location.Y / scale;
                string info = LifeRunner.LifeField.CreatureInfo(x, y);
                textBox1.Text = info;
            }
        }

        private void infection_Click(object sender, EventArgs e)
        {
            LifeRunner.LifeField.InfectCreatures();
        }

        private void GenerateFood_Click(object sender, EventArgs e)
        {
            LifeRunner.AmountOfNewGeneratedFood = (int)nudFood.Value;
            LifeRunner.LifeField.GenerateNewFood((int)nudFood.Value);
        }

        private void ApplyLifeSpeed_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100 - (int)nudLifeSpeed.Value;
        }
    }
}
