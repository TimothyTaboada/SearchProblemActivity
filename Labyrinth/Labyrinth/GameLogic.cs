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
    }
}
