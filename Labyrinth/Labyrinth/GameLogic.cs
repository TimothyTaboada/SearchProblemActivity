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

        public int GameStatus(int keys, int playerX, int playerY, int exitX, int exitY, int berisX, int berisY)
        {
            if (PlayerExit(playerX, playerY, exitX, exitY) && keys == 0)
            {
                return 1;
            }
            else if(keys == 0)
            {
                return 2;
            }
            else if(PlayerDie(playerX, playerY, berisX, berisY))
            {
                return 3;
            }
            else return 0;
        }

        public int CheckKey(int x, int y, int keys)
        {
            if (this.maze[y, x] == 4)
            {
                this.maze[y, x] = 0;
                keys--;
            }
            return keys;
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
                    if(x == 0 || y == 0 || x == 0 + 1 || y == 0 + 1 || x == this.columns - 1 || y == this.rows - 1 || x == this.columns - 2 || y == this.rows - 2)
                    {
                        maze[y, x] = 3;
                    }
                }
            }
            // keys
            maze[1, 16] = 4;
            maze[7, 10] = 4;
            maze[7, 13] = 4;
            maze[13, 7] = 4;
            // pain peko
            maze[3, 3] = 3;
            maze[3, 4] = 3;
            maze[3, 5] = 3;
            maze[3, 6] = 3;
            maze[3, 8] = 3;
            maze[3, 9] = 3;
            maze[3, 10] = 3;
            maze[3, 12] = 3;
            maze[3, 13] = 3;
            maze[3, 14] = 3;
            maze[3, 15] = 3;
            maze[3, 17] = 3;
            maze[3, 18] = 3;
            maze[3, 19] = 3;
            maze[3, 20] = 3;
            maze[4, 3] = 3;
            maze[4, 8] = 3;
            maze[4, 20] = 3;
            maze[5, 3] = 3;
            maze[5, 5] = 3;
            maze[5, 6] = 3;
            maze[5, 8] = 3;
            maze[5, 10] = 3;
            maze[5, 11] = 3;
            maze[5, 12] = 3;
            maze[5, 13] = 3;
            maze[5, 15] = 3;
            maze[5, 17] = 3;
            maze[5, 18] = 3;
            maze[5, 20] = 3;
            maze[6, 3] = 3;
            maze[6, 5] = 3;
            maze[6, 6] = 3;
            maze[6, 8] = 3;
            maze[6, 10] = 3;
            maze[6, 11] = 3;
            maze[6, 12] = 3;
            maze[6, 13] = 3;
            maze[6, 15] = 3;
            maze[6, 17] = 3;
            maze[6, 18] = 3;
            maze[6, 20] = 3;
            maze[7, 5] = 3;
            maze[7, 6] = 3;
            maze[7, 8] = 3;
            maze[7, 11] = 3;
            maze[7, 12] = 3;
            maze[7, 15] = 3;
            maze[7, 17] = 3;
            maze[7, 18] = 3;
            maze[8, 3] = 3;
            maze[8, 5] = 3;
            maze[8, 6] = 3;
            maze[8, 8] = 3;
            maze[8, 10] = 3;
            maze[8, 11] = 3;
            maze[8, 12] = 3;
            maze[8, 13] = 3;
            maze[8, 15] = 3;
            maze[8, 17] = 3;
            maze[8, 18] = 3;
            maze[8, 20] = 3;
            maze[9, 3] = 3;
            maze[9, 5] = 3;
            maze[9, 6] = 3;
            maze[9, 8] = 3;
            maze[9, 10] = 3;
            maze[9, 11] = 3;
            maze[9, 12] = 3;
            maze[9, 13] = 3;
            maze[9, 15] = 3;
            maze[9, 17] = 3;
            maze[9, 18] = 3;
            maze[9, 20] = 3;
            maze[10, 3] = 3;
            maze[10, 15] = 3;
            maze[10, 20] = 3;
            maze[11, 3] = 3;
            maze[11, 4] = 3;
            maze[11, 5] = 3;
            maze[11, 6] = 3;
            maze[11, 8] = 3;
            maze[11, 9] = 3;
            maze[11, 10] = 3;
            maze[11, 11] = 3;
            maze[11, 13] = 3;
            maze[11, 14] = 3;
            maze[11, 15] = 3;
            maze[11, 17] = 3;
            maze[11, 18] = 3;
            maze[11, 19] = 3;
            maze[11, 20] = 3;
        }
    }
}
