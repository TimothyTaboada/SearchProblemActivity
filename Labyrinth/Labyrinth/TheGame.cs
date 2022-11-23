using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labyrinth
{
    public partial class TheGame : Form
    {
        private int[,] mazeWalls;
        private int cellSize = 20;
        private int rowDimension = 13;
        private int colDimension = 22;
        private int playerPosX = 1;
        private int playerPosY = 6;
        private int berisPosX = 20;
        private int berisPosY = 7;
        private int derisPosX = 20;
        private int derisPosY = 5;

        public TheGame() => InitializeComponent();

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            for(int y = 0; y < this.rowDimension; y++)
            {
                for(int x = 0; x < this.colDimension; x++)
                {
                    graphics.DrawRectangle(new Pen(Color.Black), x * this.cellSize, y * this.cellSize, this.cellSize, this.cellSize);
                }
            }
            // draw player
            graphics.FillEllipse((Brush)new SolidBrush(Color.Green), this.playerPosX * this.cellSize + 3, this.playerPosY * this.cellSize + 3, this.cellSize - 6, this.cellSize - 6);
            // draw beris
            graphics.FillEllipse((Brush)new SolidBrush(Color.Red), this.berisPosX * this.cellSize + 3, this.berisPosY * this.cellSize + 3, this.cellSize - 6, this.cellSize - 6);
            // draw deris
            graphics.FillEllipse((Brush)new SolidBrush(Color.Blue), this.derisPosX * this.cellSize + 3, this.derisPosY * this.cellSize + 3, this.cellSize - 6, this.cellSize - 6);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {

        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {

        }

        private void buttonRight_Click(object sender, EventArgs e)
        {

        }

        private void buttonDown_Click(object sender, EventArgs e)
        {

        }

        private void buttonWait_Click(object sender, EventArgs e)
        {

        }
    }
}
