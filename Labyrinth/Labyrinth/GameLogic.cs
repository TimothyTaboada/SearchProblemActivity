using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    internal class GameLogic
    {
        private int[,] maze;
        private int rows;
        private int columns;

        public GameLogic(int rows, int columns)
        {
            this.maze = new int[rows, columns];
            this.rows = rows;
            this.columns = columns;
        }

        public int Rows => this.rows;

        public int Columns => this.columns;

        public int[,] GetMaze => this.maze;

        private bool playerDie(int playerX, int playerY, int berisX, int berisY, int derisX, int derisY)
        {
            if(playerX == berisX && playerY == berisY || playerX == derisX && playerY == derisY)
            {
                return true;
            }
            else return false;
        }

        private bool playerExit(int playerX, int playerY, int exitX, int exitY)
        {
            if(playerX == exitX && playerY == exitY)
            {
                return true;
            }
            else return false;
        }

        public int gameStatus(int playerX, int playerY, int exitX, int exitY, int berisX, int berisY, int derisX, int derisY)
        {
            if (playerExit(playerX, playerY, exitX, exitY))
            {
                return 1;
            }
            else if (playerDie(playerX, playerY, berisX, berisY, derisX, derisY))
            {
                return 2;
            }
            else return 0;
        }

        public void initializeMaze()
        {
            for(int y = 0; y < this.rows; y++)
            {
                for(int x = 0; x < this.columns; x++)
                {
                    if(x == 0 || y == 0 || x == this.columns - 1 || y == this.rows - 1)
                    {
                        maze[y, x] = 3;
                    }
                }
            }
            // pain peko
            maze[2, 2] = 3;
            maze[2, 3] = 3;
            maze[2, 4] = 3;
            maze[2, 5] = 3;
            maze[2, 7] = 3;
            maze[2, 8] = 3;
            maze[2, 9] = 3;
            maze[2, 11] = 3;
            maze[2, 12] = 3;
            maze[2, 13] = 3;
            maze[2, 14] = 3;
            maze[2, 16] = 3;
            maze[2, 17] = 3;
            maze[2, 18] = 3;
            maze[2, 19] = 3;
            maze[3, 2] = 3;
            maze[3, 7] = 3;
            maze[3, 19] = 3;
            maze[4, 2] = 3;
            maze[4, 4] = 3;
            maze[4, 5] = 3;
            maze[4, 7] = 3;
            maze[4, 9] = 3;
            maze[4, 10] = 3;
            maze[4, 11] = 3;
            maze[4, 12] = 3;
            maze[4, 14] = 3;
            maze[4, 16] = 3;
            maze[4, 17] = 3;
            maze[4, 19] = 3;
            maze[5, 2] = 3;
            maze[5, 4] = 3;
            maze[5, 5] = 3;
            maze[5, 7] = 3;
            maze[5, 9] = 3;
            maze[5, 10] = 3;
            maze[5, 11] = 3;
            maze[5, 12] = 3;
            maze[5, 14] = 3;
            maze[5, 16] = 3;
            maze[5, 17] = 3;
            maze[5, 19] = 3;
            maze[6, 4] = 3;
            maze[6, 5] = 3;
            maze[6, 7] = 3;
            maze[6, 10] = 3;
            maze[6, 11] = 3;
            maze[6, 14] = 3;
            maze[6, 16] = 3;
            maze[6, 17] = 3;
            maze[7, 2] = 3;
            maze[7, 4] = 3;
            maze[7, 5] = 3;
            maze[7, 7] = 3;
            maze[7, 9] = 3;
            maze[7, 10] = 3;
            maze[7, 11] = 3;
            maze[7, 12] = 3;
            maze[7, 14] = 3;
            maze[7, 16] = 3;
            maze[7, 17] = 3;
            maze[7, 19] = 3;
            maze[8, 2] = 3;
            maze[8, 4] = 3;
            maze[8, 5] = 3;
            maze[8, 7] = 3;
            maze[8, 9] = 3;
            maze[8, 10] = 3;
            maze[8, 11] = 3;
            maze[8, 12] = 3;
            maze[8, 14] = 3;
            maze[8, 16] = 3;
            maze[8, 17] = 3;
            maze[8, 19] = 3;
            maze[9, 2] = 3;
            maze[9, 14] = 3;
            maze[9, 19] = 3;
            maze[10, 2] = 3;
            maze[10, 3] = 3;
            maze[10, 4] = 3;
            maze[10, 5] = 3;
            maze[10, 7] = 3;
            maze[10, 8] = 3;
            maze[10, 9] = 3;
            maze[10, 10] = 3;
            maze[10, 12] = 3;
            maze[10, 13] = 3;
            maze[10, 14] = 3;
            maze[10, 16] = 3;
            maze[10, 17] = 3;
            maze[10, 18] = 3;
            maze[10, 19] = 3;
        }
    }
}
