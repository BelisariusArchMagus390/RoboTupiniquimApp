namespace RoboTupiniquimApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string commands = "";
            string dimensionsGrid = "";
            string firstPosition = "";
            char orientation = ' ';
            int lineSize = 0;
            int columnSize = 0;

            int[] position = new int[2];

            Console.Write("Escreva as dimensões do espaço que será percorrido pelo o rôbo (x y): ");
            dimensionsGrid = Console.ReadLine();
            string[] splitCommands = dimensionsGrid.Split(' ');

            lineSize = int.Parse(splitCommands[0]);
            columnSize = int.Parse(splitCommands[1]);

            lineSize++;
            columnSize++;

            char[,] grid = new char[lineSize, columnSize];
            createGrid(grid, lineSize, columnSize);

            Console.Write("Escreva a posição inicial do robô (x y o): ");
            firstPosition = Console.ReadLine();
            string[] splitPosition = firstPosition.Split(' ');

            position[0] = int.Parse(splitPosition[0]);
            position[1] = int.Parse(splitPosition[1]);
            orientation = char.Parse(splitPosition[2]);
            orientation = Char.ToUpper(orientation);

            grid = firstPositionDeploy(grid, position, columnSize);
            Console.WriteLine();
            showGrid(grid, lineSize, lineSize);
            Console.WriteLine();

            Console.Write("Escreva em uma única linha os comandos do robô: ");
            commands = Console.ReadLine();

            grid = useCommands(grid, commands, orientation);

            Console.WriteLine();
            showGrid(grid, lineSize, lineSize);
            Console.WriteLine();

            Console.ReadLine();
        }

        static char[,] firstPositionDeploy(char[,] grid, int[] position, int columnSize)
        {
            int column = position[0];
            int line = columnSize - (position[1] + 1);

            grid[line, column] = 'O';

            return grid;
        }

        static bool ifMove(char[,] grid, int[] position)
        {
            int nextLine = position[0];
            int nextColumn = position[1];

            int lineSize = grid.GetLength(0);
            int columnSize = grid.GetLength(1);

            if ((nextLine < lineSize) && (nextColumn < columnSize))
                return true;
            else
                return false;
        }

        static char[,] moveNorth(char[,] grid)
        {
            int[] position = searchElementPosition(grid);

            position[0] -= 1;

            if (ifMove(grid, position))
            {
                grid = updatePosition(grid, position);
            }

            return grid;
        }

        static char[,] moveEast(char[,] grid)
        {
            int[] position = searchElementPosition(grid);
            position[1] += 1;

            if (ifMove(grid, position))
            {
                grid = updatePosition(grid, position);
            }

            return grid;
        }

        static char[,] moveSouth(char[,] grid)
        {
            int[] position = searchElementPosition(grid);
            position[0] += 1;

            if (ifMove(grid, position))
            {
                grid = updatePosition(grid, position);
            }

            return grid;
        }

        static char[,] moveWest(char[,] grid)
        {
            int[] position = searchElementPosition(grid);
            position[1] -= 1;

            if (ifMove(grid, position))
            {
                grid = updatePosition(grid, position);
            }

            return grid;
        }

        static int newPositionDirection(char[] directions, int positionDirection, int movement)
        {
            int arraySize = directions.Length;
            int newPosition = positionDirection + movement;
            int resultPosition = 0;

            if (newPosition < arraySize && newPosition >= 0)
                resultPosition = positionDirection + movement;
            else if (newPosition < 0)
                resultPosition = arraySize - 1;
            else if (resultPosition >= arraySize)
                resultPosition = 0;

            return resultPosition;
        }

        static char[,] useCommands(char[,] grid, string commands, char orient)
        {
            commands = commands.ToUpper();
            char inputOrientation = orient;
            char[] directions = ['N', 'E', 'S', 'W'];

            char robotOrientation = orient;

            int positionDirection = 0;

            foreach (char command in commands)
            {
                if (command != 'M')
                {
                    inputOrientation = command;

                    positionDirection = Array.IndexOf(directions, robotOrientation);

                    switch (inputOrientation)
                    {
                        case 'N':

                            if (robotOrientation == 'E')
                                robotOrientation = directions[newPositionDirection(directions, positionDirection, -1)];
                            else if (robotOrientation == 'W')
                                robotOrientation = directions[newPositionDirection(directions, positionDirection, 1)];

                            break;
                        case 'E':

                            robotOrientation = directions[newPositionDirection(directions, positionDirection, 1)];

                            break;
                        case 'S':

                            if (robotOrientation == 'E')
                                robotOrientation = directions[newPositionDirection(directions, positionDirection, 1)];
                            else if (robotOrientation == 'W')
                                robotOrientation = directions[newPositionDirection(directions, positionDirection, -1)];

                            break;
                        case 'W':

                            robotOrientation = directions[newPositionDirection(directions, positionDirection, -1)];

                            break;
                    }
                }
                else if (command == 'M')
                {
                    switch (robotOrientation)
                    {
                        case 'N':
                            grid = moveNorth(grid);
                            break;
                        case 'E':
                            grid = moveEast(grid);
                            break;
                        case 'S':
                            grid = moveSouth(grid);
                            break;
                        case 'W':
                            grid = moveWest(grid);
                            break;
                    }
                }
            }
            return grid;
        }
    }
}