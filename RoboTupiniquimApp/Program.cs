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
            int[] position = [-1, -1];
            char orientation = ' ';

            Input input = new Input();
            Grid grid = new Grid();
            Robot robot = new Robot();

            bool ifExit = false;
            while(ifExit == false)
            {
                System.Console.Clear();
                System.Console.WriteLine(" --------------------------------------------");
                System.Console.WriteLine($"\n ROBÔ TUPINIQUIM");
                System.Console.WriteLine("\n --------------------------------------------");

                showLegends();

                System.Console.WriteLine(" --------------------------------------------");
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

                        if (line == 0 && column == 0)
                        {
                            System.Console.WriteLine($"\n Erro! A grade de exploração ainda não foi criada.");
                            System.Console.WriteLine(" Aperte Enter para continuar...");
                            System.Console.ReadLine();
                        }
                        else
                        {
                            showLegends();

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
                        }

                        break;

                    case '3':
                        System.Console.Clear();

                        if (position[0] == -1 && position[1] == -1)
                        {
                            System.Console.WriteLine($"\n Erro! Ainda não foi feito o posicionamento inicial do robô.");
                            System.Console.WriteLine(" Aperte Enter para continuar...");
                            System.Console.ReadLine();
                        }
                        else
                        {
                            showLegends();
                            showPossibleCommands();

                            commands = input.commandsRobot();

                            robot.useCommands(commands, orientation);

                            grid.show(gridMap);

                            position = robot.getPosition();
                            orientation = robot.getOrientation();

                            System.Console.WriteLine($" Posição final: {position[1]} {position[0]} | Orientação final: {orientation}\n");

                            System.Console.WriteLine(" Aperte Enter para continuar...");
                            System.Console.ReadLine();
                        }

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
        }

        static void showLegends()
        {
            System.Console.WriteLine("\n LEGENDA");
            System.Console.WriteLine("\n Direções:");
            System.Console.WriteLine(" | N(North) - Norte | E(East) - Leste       |");
            System.Console.WriteLine(" | W(West) - Oeste  | S(South) - Sul        |");
            System.Console.WriteLine("\n Mapa: ");
            System.Console.WriteLine(" | # - Espaço vazio | O - Espaço com o robô |\n");
        }

        static void showPossibleCommands()
        {
            System.Console.WriteLine("\n COMANDOS");
            System.Console.WriteLine("\n Mudar direção do robô:");
            System.Console.WriteLine(" | N(North) - Norte | E(East) - Leste |");
            System.Console.WriteLine(" | W(West) - Oeste  | S(South) - Sul  |");
            System.Console.WriteLine("\n Mover o robô: ");
            System.Console.WriteLine(" | M - Mover                          |\n");
        }
    }
}