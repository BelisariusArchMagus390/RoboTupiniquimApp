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
        public static char[,] gridMap;
        public char[,] create(int lineSize, int columnSize)
        {
            gridMap = new char[lineSize, columnSize];

            for (int line = 0; line < lineSize; line++)
            {
                for (int column = 0; column < columnSize; column++)
                {
                    gridMap[line, column] = '#';
                }
            }

            return gridMap;
        }

        public void show(char[,] gridMap)
        {
            int lineSize = gridMap.GetLength(0);
            int columnSize = gridMap.GetLength(1);

            int numberLines = lineSize - 1;

            System.Console.WriteLine();

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
                        System.Console.Write($"{gridMap[line, column]} ");
                    else if (column == 0)
                        System.Console.Write($"  {column}");
                    else if ((column < columnSize) && (column > 0))
                        System.Console.Write($" {column}");
                }

                System.Console.WriteLine();
            }

            System.Console.WriteLine();
        }

        public char[,] update(int[] position, char marker)
        {
            int[] oldPosition = searchElementPosition(marker);
            int oldline = oldPosition[0];
            int oldColumn = oldPosition[1];
            gridMap[oldline, oldColumn] = '#';

            int line = position[0];
            int column = position[1];

            gridMap[line, column] = marker;

            return gridMap;
        }

        public int[] searchElementPosition(char marker)
        {
            int lineSize = gridMap.GetLength(0);
            int columnSize = gridMap.GetLength(1);
            int[] position = new int[2];

            for (int line = 0; line < lineSize; line++)
            {
                for (int column = 0; column < columnSize; column++)
                {
                    if (gridMap[line, column] == marker)
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
