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
        public static char[,] GridMap;

        // cria uma nova grid
        public char[,] create(int lineSize, int columnSize)
        {
            GridMap = new char[lineSize, columnSize];

            for (int line = 0; line < lineSize; line++)
            {
                for (int column = 0; column < columnSize; column++)
                {
                    GridMap[line, column] = '#';
                }
            }

            return GridMap;
        }

        public char[,] getGridMap()
        {
            return GridMap;
        }

        // mostra a grid em seu estado atual
        public void show()
        {
            int lineSize = GridMap.GetLength(0);
            int columnSize = GridMap.GetLength(1);

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
                        System.Console.Write($"{GridMap[line, column]} ");
                    else if (column == 0)
                        System.Console.Write($"  {column}");
                    else if ((column < columnSize) && (column > 0))
                        System.Console.Write($" {column}");
                }

                System.Console.WriteLine();
            }

            System.Console.WriteLine();
        }

        // atualiza a grid
        public char[,] update(int[] position, char marker)
        {
            int[] oldPosition = searchElementPosition(marker);
            int oldline = oldPosition[0];
            int oldColumn = oldPosition[1];
            GridMap[oldline, oldColumn] = '#';

            int line = position[0];
            int column = position[1];

            GridMap[line, column] = marker;

            return GridMap;
        }

        // busca a posição do marcador do robô na grid
        public int[] searchElementPosition(char marker)
        {
            int lineSize = GridMap.GetLength(0);
            int columnSize = GridMap.GetLength(1);
            int[] position = new int[2];

            for (int line = 0; line < lineSize; line++)
            {
                for (int column = 0; column < columnSize; column++)
                {
                    if (GridMap[line, column] == marker)
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
