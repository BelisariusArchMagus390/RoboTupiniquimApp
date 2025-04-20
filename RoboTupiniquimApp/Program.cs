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

        static char[,] createGrid(char[,] grid, int lineSize, int columnSize)
        {
            for (int line = 0; line < lineSize; line++)
            {
                for (int column = 0; column < columnSize; column++)
                {
                    grid[line, column] = '#';
                }
            }
            return grid;
        }

        static void showGrid(char[,] grid, int lineSize, int columnSize)
        {
            int numberLines = lineSize - 1;

            for (int line = 0; line <= lineSize; line++)
            {
                if (line < lineSize)
                {
                    Console.Write($"{numberLines} ");
                    numberLines--;
                }

                for (int column = 0; column <= columnSize; column++)
                {
                    if (column < columnSize && line < lineSize)
                        Console.Write($"{grid[line, column]} ");
                    else if (column == 0)
                        Console.Write($"  {column}");
                    else if ((column < columnSize) && (column > 0))
                        Console.Write($" {column}");
                }

                Console.WriteLine();
            }
        }

        static char[,] firstPositionDeploy(char[,] grid, int[] position, int columnSize)
        {
            int column = position[0];
            int line = columnSize - (position[1] + 1);

            grid[line, column] = 'O';

            return grid;
        }

        static char[,] updatePosition(char[,] grid, int[] position)
        {
            int[] oldPosition = searchElementPosition(grid);
            int oldline = oldPosition[0];
            int oldColumn = oldPosition[1];
            grid[oldline, oldColumn] = '#';

            int line = position[0];
            int column = position[1];

            grid[line, column] = 'O';

            return grid;
        }

        static int[] searchElementPosition(char[,] grid)
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