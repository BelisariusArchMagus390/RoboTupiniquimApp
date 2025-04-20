﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTupiniquimApp.Console
{
    internal class Grid
    {
        public char[,] grid { get; set; }

        public void create(int lineSize, int columnSize)
        {
            grid = new char[lineSize, columnSize];

            for (int line = 0; line < lineSize; line++)
            {
                for (int column = 0; column < columnSize; column++)
                {
                    grid[line, column] = '#';
                }
            }
        }

        public void show()
        {
            int lineSize = grid.GetLength(0);
            int columnSize = grid.GetLength(1);

            int numberLines = lineSize - 1;

            for (int line = 0; line <= lineSize; line++)
            {
                if (line < lineSize)
                {
                    System.Console.Write($"{numberLines} ");
                    numberLines--;
                }

                for (int column = 0; column <= columnSize; column++)
                {
                    if (column < columnSize && line < lineSize)
                        System.Console.Write($"{grid[line, column]} ");
                    else if (column == 0)
                        System.Console.Write($"  {column}");
                    else if ((column < columnSize) && (column > 0))
                        System.Console.Write($" {column}");
                }

                System.Console.WriteLine();
            }
        }

        public void update(int[] position)
        {
            int[] oldPosition = searchElementPosition();
            int oldline = oldPosition[0];
            int oldColumn = oldPosition[1];
            grid[oldline, oldColumn] = '#';

            int line = position[0];
            int column = position[1];

            grid[line, column] = 'O';
        }

        private int[] searchElementPosition()
        {
            char element = 'O';
            int lineSize = grid.GetLength(0);
            int columnSize = grid.GetLength(1);
            int[] position = new int[2];

            for (int line = 0; line < lineSize; line++)
            {
                for (int column = 0; column < columnSize; column++)
                {
                    if (grid[line, column] == 'O')
                    {
                        position[0] = line;
                        position[1] = column;
                    }

                }
            }
            return position;
        }
    }
}
