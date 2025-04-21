using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTupiniquimApp.Console
{
    internal class Robot
    {
        Grid grid = new Grid();

        public static int[] Position { get; set; }
        public static char Orientation { get; set; }
        public string Name { get; set; }
        public char Marker { get; set; }

        public Robot (string name, char marker)
        {
            Name = name;
            Marker = marker;
        }

        public void setPosition(int[] position)
        {
            Position = position;
        }

        public int[] getPosition() 
        { 
            return Position; 
        }

        public char getOrientation()
        {
            return Orientation;
        }

        public string getName()
        {
            return Name;
        }

        public void firstPositionDeploy()
        {
            int columnSize = grid.getGridMap().GetLength(1);

            int column = Position[0];
            int line = columnSize - (Position[1] + 1);

            //RobotGridMap[line, column] = Marker;
            Position[0] = line;
            Position[1] = column;

            grid.update(Position, Marker);
        }

        public bool ifMove(int[] position)
        {
            int nextLine = position[0];
            int nextColumn = position[1];

            int lineSize = grid.getGridMap().GetLength(0);
            int columnSize = grid.getGridMap().GetLength(1);

            if ((nextLine < lineSize) && (nextColumn < columnSize))
                return true;
            else
                return false;
        }

        public void moveNorth()
        {
            int[] position = Position;
            position[0] -= 1;

            if (ifMove(position))
            {
                grid.update(position, Marker);
                Position = position;
            }
        }

        public void moveEast()
        {
            int[] position = Position;
            position[1] += 1;

            if (ifMove(position))
            {
                grid.update(position, Marker);
                Position = position;
            }
        }

        public void moveSouth()
        {
            int[] position = Position;
            position[0] += 1;

            if (ifMove(position))
            {
                grid.update(position, Marker);
                Position = position;
            }
        }

        public void moveWest()
        {
            int[] position = Position;
            position[1] -= 1;

            if (ifMove(position))
            {
                grid.update(position, Marker);
                Position = position;
            }            
        }

        public int newPositionDirection(char[] directions, int positionDirection, int movement)
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

        public void useCommands(string commands, char orient)
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
                            moveNorth();
                            break;
                        case 'E':
                            moveEast();
                            break;
                        case 'S':
                            moveSouth();
                            break;
                        case 'W':
                            moveWest();
                            break;
                    }
                }
            }
            Orientation = robotOrientation;
        }
    }
}
