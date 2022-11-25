using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    internal class GameLogic
    {
        private int[,] maze;
        private int rows;
        private int columns;
        private int path = 1;

        public GameLogic(int rows, int columns)
        {
            this.maze = new int[rows, columns];
            this.rows = rows;
            this.columns = columns;
        }

        public int Rows => this.rows;

        public int Columns => this.columns;

        public int[,] GetMaze => this.maze;

        private bool PlayerDie(int playerX, int playerY, int berisX, int berisY)
        {
            if(playerX == berisX && playerY == berisY)
            {
                return true;
            }
            else return false;
        }

        private bool PlayerExit(int playerX, int playerY, int exitX, int exitY)
        {
            if(playerX == exitX && playerY == exitY)
            {
                return true;
            }
            else return false;
        }

        public int GameStatus(int playerX, int playerY, int exitX, int exitY, int berisX, int berisY)
        {
            if(PlayerExit(playerX, playerY, exitX, exitY))
            {
                return 1;
            }
            else if(PlayerDie(playerX, playerY, berisX, berisY))
            {
                return 2;
            }
            else return 0;
        }

        private int GetNodeContent(int[,] maze, int nodeNo)
        {
            int length = maze.GetLength(1);
            return maze[nodeNo / length, nodeNo - nodeNo / length * length];
        }

        private void ChangeNodeContent(int[,] maze, int nodeNo, int newValue)
        {
            int length = maze.GetLength(1);
            maze[nodeNo / length, nodeNo - nodeNo / length * length] = newValue;
        }

        public int[,] FindPath(int fromX, int fromY, int toX, int toY) => Search(fromY * this.Columns + fromX, toY * this.Columns + toX);

        private int[,] Search(int start, int end)
        {
            int sRows = this.rows;
            int sColumns = this.columns;
            int length = sRows * sColumns;
            int[] array1 = new int[length];
            int[] array2 = new int[length];
            int index1 = 0;
            int index2 = 0;
            if(this.GetNodeContent(this.maze, start) != 0)
            {
                return (int[,])null;
            }
            int[,] maze1 = new int[sRows, sColumns];
            for(int maze1X = 0; maze1X < sRows; maze1X++)
            {
                for(int maze1Y = 0; maze1Y < sColumns; maze1Y++)
                {
                    maze1[maze1X, maze1Y] = 0;
                }
            }
            array1[index2] = start;
            array2[index2] = -1;
            for(int index3 = index2 + 1; index1 != index3 && array1[index1] != end; index1++)
            {
                int nodeNo1 = array1[index1];
                // up
                int nodeNo2 = nodeNo1 - 1;
                if(nodeNo2 >= 0 && nodeNo2 / sColumns == nodeNo1 / sColumns && this.GetNodeContent(this.maze, nodeNo2) == 0 && this.GetNodeContent(maze1, nodeNo2) == 0)
                {
                    array1[index3] = nodeNo2;
                    array2[index3] = nodeNo1;
                    this.ChangeNodeContent(maze1, nodeNo2, 1);
                    index3++;
                }
                // down
                int nodeNo3 = nodeNo1 + 1;
                if (nodeNo3 < length && nodeNo3 / sColumns == nodeNo1 / sColumns && this.GetNodeContent(this.maze, nodeNo3) == 0 && this.GetNodeContent(maze1, nodeNo3) == 0)
                {
                    array1[index3] = nodeNo3;
                    array2[index3] = nodeNo1;
                    this.ChangeNodeContent(maze1, nodeNo3, 1);
                    ++index3;
                }
                // left
                int nodeNo4 = nodeNo1 - sColumns;
                if (nodeNo4 >= 0 && this.GetNodeContent(this.maze, nodeNo4) == 0 && this.GetNodeContent(maze1, nodeNo4) == 0)
                {
                    array1[index3] = nodeNo4;
                    array2[index3] = nodeNo1;
                    this.ChangeNodeContent(maze1, nodeNo4, 1);
                    ++index3;
                }
                // right
                int nodeNo5 = nodeNo1 + sColumns;
                if (nodeNo5 >= 0 && this.GetNodeContent(this.maze, nodeNo5) == 0 && this.GetNodeContent(maze1, nodeNo5) == 0)
                {
                    array1[index3] = nodeNo5;
                    array2[index3] = nodeNo1;
                    this.ChangeNodeContent(maze1, nodeNo5, 1);
                    ++index3;
                }
                this.ChangeNodeContent(maze1, nodeNo1, 2);
            }
            int[,] maze2 = new int[sRows, sColumns];
            for(int maze2X = 0; maze2X < sRows; maze2X++)
            {
                for(int maze2Y = 0; maze2Y < sColumns; maze2Y++)
                {
                    maze2[maze2X, maze2Y] = this.maze[maze2X, maze2Y];
                }
            }
            int nodeNo6 = end;
            this.ChangeNodeContent(maze2, nodeNo6, this.path);
            for(int index4 = index1; index4 >= 0; index4--)
            {
                if (array1[index4] == nodeNo6)
                {
                    nodeNo6 = array2[index4];
                    if(nodeNo6 == -1)
                    {
                        return maze2;
                    }
                    this.ChangeNodeContent(maze2, nodeNo6, this.path);
                }
            }
            return (int[,]) null;
        }

        public void InitializeMaze()
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
