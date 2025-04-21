using RoboTupiniquimApp.Console;
using System.Data.Common;

namespace RoboTupiniquimApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string commands = "";
            int[] dimensions = new int[2];
            Dictionary<char, int[]> firstPositionDict = new Dictionary<char, int[]>();
            int line = 0;
            int column = 0;
            char[,] gridMap = new char[line, column];
            int[] position;
            char orientation = ' ';

            Input input = new Input();
            Grid grid = new Grid();
            Robot robot = new Robot();

            bool ifExit = false;
            while(ifExit == false)
            {
                System.Console.Clear();
                System.Console.WriteLine("\n 1 - Criar grade de exploração");
                System.Console.WriteLine(" 2 - Posição de início do robô");
                System.Console.WriteLine(" 3 - Comandos do robô");
                System.Console.WriteLine(" 4 - Sair");

                System.Console.Write("\n Escolha uma das opções: ");
                char optionMenu = System.Console.ReadLine()[0];

                switch (optionMenu)
                {
                    case '1':
                        System.Console.Clear();

                        dimensions = input.dimensionsGridMap();
                        line = dimensions[0];
                        column = dimensions[1];
                        gridMap = new char[line, column];

                        gridMap = grid.create(line, column);

                        grid.show(gridMap);

                        System.Console.WriteLine(" Aperte Enter para continuar...");
                        System.Console.ReadLine();

                        break;

                    case '2':
                        System.Console.Clear();

                        firstPositionDict = input.firstPosition();

                        char[] orientationArray = firstPositionDict.Keys.ToArray();
                        orientation = orientationArray[0];

                        position = firstPositionDict[orientation];

                        robot.setRobotGridMap(gridMap);
                        robot.setPosition(position);
                        
                        gridMap = robot.getRobotGridMap();
                        robot.firstPositionDeploy();

                        grid.show(gridMap);

                        System.Console.WriteLine(" Aperte Enter para continuar...");
                        System.Console.ReadLine();

                        break;

                    case '3':
                        System.Console.Clear();

                        commands = input.commandsRobot();

                        robot.useCommands(commands, orientation);

                        grid.show(gridMap);

                        System.Console.WriteLine(" Aperte Enter para continuar...");
                        System.Console.ReadLine();

                        break;

                    case '4':
                        ifExit = true;
                        break;

                    default:
                        System.Console.Clear();
                        System.Console.WriteLine("\n Erro! Opção incorreta.");
                        System.Console.WriteLine(" Aperte Enter para continuar...");
                        System.Console.ReadLine();
                        break;
                }
            }
            System.Console.ReadLine();
        }
    }
}