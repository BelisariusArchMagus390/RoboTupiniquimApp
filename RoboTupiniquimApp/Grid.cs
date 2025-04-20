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
        public char[,] create(int lineSize, int columnSize)
        {
            char[,] gridMap = new char[lineSize, columnSize];

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
        }

        public char[,] update(int[] position, char[,] gridMap)
        {
            int[] oldPosition = searchElementPosition(gridMap);
            int oldline = oldPosition[0];
            int oldColumn = oldPosition[1];
            gridMap[oldline, oldColumn] = '#';

            int line = position[0];
            int column = position[1];

            gridMap[line, column] = 'O';

            return gridMap;
        }

        public int[] searchElementPosition(char[,] gridMap)
        {
            char element = 'O';
            int lineSize = gridMap.GetLength(0);
            int columnSize = gridMap.GetLength(1);
            int[] position = new int[2];

            for (int line = 0; line < lineSize; line++)
            {
                for (int column = 0; column < columnSize; column++)
                {
                    if (gridMap[line, column] == 'O')
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
