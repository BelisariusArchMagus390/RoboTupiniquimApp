using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTupiniquimApp.Console
{
    internal class Input
    {
        private void showErrorMessage(string typeError)
        {
            System.Console.Clear();
            System.Console.WriteLine($"\n Erro! A entrada digitada {typeError}.");
            System.Console.WriteLine(" Aperte Enter para continuar...");
            System.Console.ReadLine();
            System.Console.Clear();
        }

        public int[] dimensionsGridMap()
        {
            int[] dimensions = new int[2];

            bool ifExit = false;
            while (ifExit == false)
            {
                System.Console.Write(" Escreva as dimensões do espaço que será percorrido pelo o rôbo (X Y): ");
                string dimensionsGrid = System.Console.ReadLine();
                string[] splitDimensionsGrid = dimensionsGrid.Split(' ');

                int size = splitDimensionsGrid.Length;

                if (size == 2)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (int.TryParse(splitDimensionsGrid[i], out dimensions[i]))
                        {
                            dimensions[i]++;

                            if (i == (size - 1))
                                ifExit = true;
                        }
                        else
                        {
                            showErrorMessage("não tem um ou mais números inteiros");
                            break;
                        }
                            
                    }       
                }
                else
                {
                    showErrorMessage("não tem dois elementos");
                }
            }
            return dimensions;
        }

        public Dictionary<char, int[]> firstPosition()
        {
            int[] coordinates = new int[2];
            Dictionary<char, int[]> firstPositionDict = new Dictionary<char, int[]>();

            bool ifExit = false;
            while (ifExit == false)
            {
                System.Console.Write(" Escreva a posição inicial do robô (X Y O): ");
                string position = System.Console.ReadLine();
                string[] splitPosition = position.Split(' ');

                int size = splitPosition.Length;

                bool ifIntNumbers = true;

                if (size == 3)
                {
                    for (int i = 0; i < (size-1); i++)
                    {
                        if (!int.TryParse(splitPosition[i], out coordinates[i]))
                        {
                            showErrorMessage("não tem um ou mais números inteiros");
                            i = size;
                            ifIntNumbers = false;
                        }   
                    }

                    if (ifIntNumbers == true)
                    {
                        char[] directions = ['N', 'E', 'S', 'W'];

                        char orientation = char.Parse(splitPosition[2]);
                        orientation = Char.ToUpper(orientation);

                        if (Array.IndexOf(directions, orientation) == -1)
                            showErrorMessage("não tem uma direção válida");
                        else
                        {
                            firstPositionDict.Add(orientation, coordinates);
                            ifExit = true;
                        }
                    }
                }
                else
                {
                    showErrorMessage("não tem três elementos");
                }
            }
            return firstPositionDict;
        }

        public string commandsRobot()
        {
            string commands = "";
            bool ifExit = false;

            while (ifExit == false)
            {
                System.Console.Write(" Escreva em uma única linha os comandos do robô: ");
                commands = System.Console.ReadLine();

                char[] possibleCommands = ['M', 'N', 'E', 'S', 'W'];

                int commandsSize = commands.Length;

                for (int i = 0; i < commandsSize; i++)
                {
                    if (Array.IndexOf(possibleCommands, commands[i]) != -1)
                    {
                        if (i == (commandsSize - 1))
                            ifExit = true;
                    }
                    else
                        showErrorMessage("não possui um ou mais comandos existentes");
                }
            }
            return commands;
        }
    }
}
