using RoboTupiniquimApp.Console;
using System.Data.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoboTupiniquimApp
{
    internal class Program
    {
        static List<Robot> robots = new List<Robot>();
        static Input input = new Input();
        static Grid grid = new Grid();

        static void Main(string[] args)
        {
            System.Console.Title = "Robô Tupiniquim";

            string commands = "";
            int[] dimensions = new int[2];
            Dictionary<char, int[]> firstPositionDict = new Dictionary<char, int[]>();
            int line = 0;
            int column = 0;
            int[] position = [-1, -1];
            char orientation = ' ';
            string robotName = "";

            int robotMarkerCont = 1;

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
                System.Console.WriteLine(" 2 - Pedir um robô");
                System.Console.WriteLine(" 3 - Posição de início do robô");
                System.Console.WriteLine(" 4 - Comandos do robô");
                System.Console.WriteLine(" 5 - Sair");

                System.Console.Write("\n Escolha uma das opções: ");
                char optionMenu = System.Console.ReadLine()[0];

                switch (optionMenu)
                {
                    case '1':
                        System.Console.Clear();

                        dimensions = input.dimensionsGridMap();
                        line = dimensions[0];
                        column = dimensions[1];
                        
                        grid.create(line, column);

                        grid.show();

                        System.Console.WriteLine(" Aperte Enter para continuar...");
                        System.Console.ReadLine();

                        break;

                    case '2':
                        System.Console.Clear();

                        System.Console.Write("\n Escolha o nome do robô: ");
                        robotName = System.Console.ReadLine();

                        if (checkExistRobot(robotName))
                        {
                            showErrorMessage("Esse robô já existe.");
                        }
                        else
                        {
                            int[] coordinate = [-1, -1];
                            char marker = robotMarkerCont.ToString()[0]; ;

                            Robot robot = new Robot(robotName, marker);
                            robot.setPosition(coordinate);
                            robots.Add(robot);

                            robotMarkerCont++;

                            System.Console.WriteLine($"\n O novo robô {robotName} foi pedido! ");
                            System.Console.WriteLine("\n Aperte Enter para continuar...");
                            System.Console.ReadLine();
                        }

                        break;

                    case '3':
                        System.Console.Clear();

                        if (line == 0 && column == 0)
                        {
                            showErrorMessage("A grade de exploração ainda não foi criada.");
                        }
                        else
                        {
                            System.Console.Write(" Escolha um robô: ");
                            robotName = System.Console.ReadLine();

                            if (checkExistRobot(robotName))
                            {
                                Robot bot = getRobot(robotName);
                                int[] pos = bot.getPosition();

                                if (pos[0] == -1 && pos[1] == -1)
                                {
                                    System.Console.Clear();
                                    showLegends();

                                    showMap();
                                    System.Console.WriteLine($" Robô: {robotName}");

                                    firstPositionDict = input.firstPosition();

                                    char[] orientationArray = firstPositionDict.Keys.ToArray();
                                    orientation = orientationArray[0];

                                    position = firstPositionDict[orientation];

                                    bot.setPosition(position);

                                    bot.firstPositionDeploy();

                                    System.Console.Clear();
                                    showMap();

                                    int[] lol = bot.getPosition();
                                    System.Console.WriteLine(" Aperte Enter para continuar...");
                                    System.Console.ReadLine();
                                }
                                else
                                    showErrorMessage("Já foi feito o posicionamento inicial desse robô.");
                            }
                            else
                                showErrorMessage("Esse robô não existe.");
                        }

                        break;

                    case '4':
                        System.Console.Clear();

                        System.Console.Write(" Escolha um robô: ");
                        robotName = System.Console.ReadLine();

                        if (checkExistRobot(robotName))
                        {
                            Robot bot = getRobot(robotName);
                            int[] pos = bot.getPosition();

                            if (pos[0] == -1 && pos[1] == -1)
                            {
                                showErrorMessage("Ainda não foi feito o posicionamento inicial desse robô.");
                            }
                            else
                            {
                                System.Console.Clear();

                                showLegends();
                                showPossibleCommands();

                                showMap();

                                System.Console.WriteLine($" Robô: {robotName}\n");

                                commands = input.commandsRobot();

                                bot.useCommands(commands, orientation);

                                System.Console.Clear();
                                showMap();

                                position = bot.getPosition();
                                orientation = bot.getOrientation();

                                System.Console.WriteLine($" Posição final: {position[1]} { ((grid.getGridMap().GetLength(0) - 1) - position[0]) } | Orientação final: {orientation}\n");

                                System.Console.WriteLine(" Aperte Enter para continuar...");
                                System.Console.ReadLine();
                            }
                        }
                        else
                            showErrorMessage("Esse robô não existe.");

                        break;

                    case '5':
                        ifExit = true;
                        break;

                    default:
                        System.Console.Clear();
                        showErrorMessage("Opção incorreta.");

                        break;
                }
            }
        }

        static void showLegends()
        {
            System.Console.WriteLine(" LEGENDA");
            System.Console.WriteLine("\n Direções:");
            System.Console.WriteLine(" | N(North) - Norte | E(East) - Leste            |");
            System.Console.WriteLine(" | W(West) - Oeste  | S(South) - Sul             |");
            System.Console.WriteLine("\n Mapa: ");
            System.Console.WriteLine(" | # - Espaço vazio | Número - Espaço com o robô |\n");
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

        static bool checkExistRobot(string name)
        {
            bool response = false;

            foreach (Robot bot in robots)
            {
                if (bot.getName() == name)
                {
                    response = true;
                }
            }
            return response;
        }

        static Robot getRobot(string name)
        {
            char marker = '1';
            Robot robot = new Robot(name, marker);

            foreach (Robot bot in robots)
            {
                if (bot.getName() == name)
                {
                    robot = bot;
                }
            }
            return robot;
        }

        static void showMap()
        {
            System.Console.WriteLine();

            foreach (Robot bot in robots)
            {
                System.Console.Write($" -> ");
                bot.showMarker();
                System.Console.Write($": ");
                bot.showName();
                System.Console.WriteLine();
            }

            grid.show();
        }

        static void showErrorMessage(string message)
        {
            System.Console.WriteLine($"\n Erro! {message}");
            System.Console.WriteLine(" Aperte Enter para continuar...");
            System.Console.ReadLine();
        }
    }
}