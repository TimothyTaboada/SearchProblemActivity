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
        private GameLogic maze;
        private int[,] mazeCell;
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
            this.maze = new GameLogic(this.rowDimension, this.colDimension);
            maze.initializeMaze();
            this.mazeCell = maze.GetMaze;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            for(int y = 0; y < this.rowDimension; y++)
            {
                for(int x = 0; x < this.colDimension; x++)
                {
                    // draw grid
                    graphics.DrawRectangle(new Pen(Color.Black), x * this.cellSize, y * this.cellSize, this.cellSize, this.cellSize);
                    // draw beris path
                    if (this.mazeCell[y, x] == 1)
                    {
                        graphics.FillRectangle((Brush)new SolidBrush(Color.Pink), x * this.cellSize + 1, y * this.cellSize + 1, this.cellSize - 1, this.cellSize - 1);
                    }
                    // draw deris path
                    if (this.mazeCell[y, x] == 2)
                    {
                        graphics.FillRectangle((Brush)new SolidBrush(Color.Cyan), x * this.cellSize + 1, y * this.cellSize + 1, this.cellSize - 1, this.cellSize - 1);
                    }
                    // draw maze walls
                    if (this.mazeCell[y, x] == 3)
                    {
                        graphics.FillRectangle((Brush)new SolidBrush(Color.DarkGray), x * this.cellSize + 1, y * this.cellSize + 1, this.cellSize - 1, this.cellSize - 1);
                    }
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
            if (this.mazeCell[playerPosY - 1, playerPosX] != 3)
            {
                this.playerPosY -= 1;
            }
            this.Refresh();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (this.mazeCell[playerPosY, playerPosX - 1] != 3)
            {
                this.playerPosX -= 1;
            }
            this.Refresh();
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (this.mazeCell[playerPosY, playerPosX + 1] != 3)
            {
                this.playerPosX += 1;
            }
            this.Refresh();
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (this.mazeCell[playerPosY + 1, playerPosX] != 3)
            {
                this.playerPosY += 1;
            }
            this.Refresh();
        }

        private void buttonWait_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
