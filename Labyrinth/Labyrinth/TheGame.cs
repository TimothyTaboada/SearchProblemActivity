using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private int exitPosX = 20;
        private int exitPosY = 6;

        public TheGame() => InitializeComponent();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.maze = new GameLogic(this.rowDimension, this.colDimension);
            this.maze.InitializeMaze();
            this.mazeCell = maze.GetMaze;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            CheckGameStatus();
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
                    if (this.mazeCell[y, x] == 10)
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
            // draw exit
            graphics.FillEllipse((Brush)new SolidBrush(Color.Goldenrod), this.exitPosX * this.cellSize + 1, this.exitPosY * this.cellSize + 1, this.cellSize - 2, this.cellSize - 2);
            graphics.FillEllipse((Brush)new SolidBrush(Color.Black), this.exitPosX * this.cellSize + 5, this.exitPosY * this.cellSize + 5, this.cellSize - 10, this.cellSize - 10);
            // draw player
            graphics.FillEllipse((Brush)new SolidBrush(Color.Green), this.playerPosX * this.cellSize + 3, this.playerPosY * this.cellSize + 3, this.cellSize - 6, this.cellSize - 6);
            // draw beris
            graphics.FillEllipse((Brush)new SolidBrush(Color.Red), this.berisPosX * this.cellSize + 3, this.berisPosY * this.cellSize + 3, this.cellSize - 6, this.cellSize - 6);
            // draw deris
            graphics.FillEllipse((Brush)new SolidBrush(Color.Blue), this.derisPosX * this.cellSize + 3, this.derisPosY * this.cellSize + 3, this.cellSize - 6, this.cellSize - 6);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if(this.mazeCell[playerPosY - 1, playerPosX] != 3)
            {
                this.playerPosY -= 1;
            }
            this.PlayerAction();
            this.Refresh();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if(this.mazeCell[playerPosY, playerPosX - 1] != 3)
            {
                this.playerPosX -= 1;
            }
            this.PlayerAction();
            this.Refresh();
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if(this.mazeCell[playerPosY, playerPosX + 1] != 3)
            {
                this.playerPosX += 1;
            }
            this.PlayerAction();
            this.Refresh();
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if(this.mazeCell[playerPosY + 1, playerPosX] != 3)
            {
                this.playerPosY += 1;
            }
            this.PlayerAction();
            this.Refresh();
        }

        private void buttonWait_Click(object sender, EventArgs e)
        {
            this.PlayerAction();
            this.Refresh();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.maze = new GameLogic(this.rowDimension, this.colDimension);
            this.maze.InitializeMaze();
            this.mazeCell = maze.GetMaze;
            this.SetControls(true);
            this.playerPosX = 1;
            this.playerPosY = 6;
            this.berisPosX = 20;
            this.berisPosY = 7;
            this.derisPosX = 20;
            this.derisPosY = 5;
            this.textBox1.Text = "";
            this.Refresh();
        }

        private void PlayerAction()
        {
            this.mazeCell = this.maze.GetMaze;
            int[,] path = this.maze.FindPath(this.berisPosX, this.berisPosY, this.playerPosX, this.playerPosY);
            this.mazeCell = path;
            this.AiMove();
        }

        private void AiMove()
        {
            this.mazeCell[this.berisPosY, this.berisPosX] = 0;
            if (this.berisPosX - 1 >= 0 && this.berisPosX - 1 < this.colDimension && this.mazeCell[this.berisPosY, this.berisPosX - 1] == 1)
                --this.berisPosX;
            else if (this.berisPosX + 1 >= 0 && this.berisPosX + 1 < this.colDimension && this.mazeCell[this.berisPosY, this.berisPosX + 1] == 1)
                ++this.berisPosX;
            else if (this.berisPosY - 1 >= 0 && this.berisPosY - 1 < this.rowDimension && this.mazeCell[this.berisPosY - 1, this.berisPosX] == 1)
                --this.berisPosY;
            else if (this.berisPosY + 1 >= 0 && this.berisPosY + 1 < this.rowDimension && this.mazeCell[this.berisPosY + 1, this.berisPosX] == 1)
                ++this.berisPosY;
        }

        private void SetControls(bool set)
        {
            this.buttonUp.Enabled = set;
            this.buttonDown.Enabled = set;
            this.buttonLeft.Enabled = set;
            this.buttonRight.Enabled = set;
            this.buttonWait.Enabled = set;
        }

        private void CheckGameStatus()
        {
            int status = this.maze.GameStatus(playerPosX, playerPosY, exitPosX, exitPosY, berisPosX, berisPosY, derisPosX, derisPosY);
            switch (status)
            {
                case 1:
                    this.textBox1.Text = "ESCAPED";
                    SetControls(false);
                    break;
                case 2:
                    this.textBox1.Text = "GAME OVER YEAH!";
                    SetControls(false);
                    break;
            }
        }
    }
}
