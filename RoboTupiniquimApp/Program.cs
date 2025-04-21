using RoboTupiniquimApp.Console;

namespace RoboTupiniquimApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string commands = "";
            int[] dimensions = new int[2];
            Dictionary<char, int[]> firstPositionDict = new Dictionary<char, int[]>();

            Input input = new Input();
            Grid grid = new Grid();

            dimensions = input.dimensionsGridMap();
            int line = dimensions[0];
            int column = dimensions[1];
            char[,] gridMap = new char[line, column];

            gridMap = grid.create(line, column);

            firstPositionDict = input.firstPosition();

            char[] orientationArray = firstPositionDict.Keys.ToArray();
            char orientation = orientationArray[0];

            int[] position = firstPositionDict[orientation];

            Robot robot = new Robot(gridMap, position);

            gridMap = robot.getRobotGridMap();
            robot.firstPositionDeploy();

            System.Console.WriteLine();
            grid.show(gridMap);
            System.Console.WriteLine();

            commands = input.commandsRobot();

            
            robot.useCommands(commands, orientation);

            System.Console.WriteLine();
            grid.show(gridMap);
            System.Console.WriteLine();

            System.Console.ReadLine();
        }
    }
}