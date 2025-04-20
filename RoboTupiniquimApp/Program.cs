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
    }
}