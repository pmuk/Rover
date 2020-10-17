using Rover.Position;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Rover
{
    public class CommandParser : IDisplacementParse, ISetPositionParser, ICreateParser, IGridCreate
    {
        public bool IsDisplacementCommandValid(string command)
        {
            return IsCommandValid(command, "[^LR0-9]");
        }

        bool IsCommandValid(string cmd, string regex)
        {
            Regex r = new Regex(regex);
            if (r.IsMatch(cmd))
            {
                return false;
            }
            return true;
        }

        public bool IsSetPositionCommandValid(string cmd)
        {
            return IsCommandValid(cmd, "[^\\[\\]0-9 NESW]");
        }

        public IEnumerable<ICommand> ParseDisplacement(string cmd, IPosition position)
        {
            if (!IsDisplacementCommandValid(cmd))
                throw new ArgumentException($"{cmd} is an invalid command.");

            var arr = cmd.ToCharArray();
            var lstCmd = new List<ICommand>();
            for(int i = 0;i<arr.Length;)
            {
                var c = arr[i];
                if(!char.IsNumber(c))
                {
                    lstCmd.Add(new Rotate(c, position));
                    i++;
                }
                else
                {
                    var builder = new StringBuilder();
                    do
                    {
                        builder.Append(arr[i]);
                        i++;
                        if (i >= arr.Length)
                            break;
                    }
                    while (char.IsNumber(arr[i]));
                    int.TryParse(builder.ToString(), out int move);
                    lstCmd.Add(new Move(move, position));
                }
            }

            return lstCmd;
        }

        public (int, int, char) ParseSetPositionCommand(string cmd)
        {
            int x, y;
            char direction;

            if (!IsSetPositionCommandValid(cmd))
                throw new ArgumentException($"the set position command {cmd} contains unexpected characters, should be of the form [0 0 N]");

            var arr = cmd.ToCharArray();
            if (arr[0] != '[')
                throw new ArgumentException($"The set position command {cmd} must start with a [.");
            if (arr[arr.Length - 1] != ']')
                throw new ArgumentException($"The set position command {cmd} must end with a ].");

            var str = cmd.Split('[', ']', ' ');

            if (str.Length != 5)
                throw new ArgumentException($"The command {cmd} is not well formed, e.g. expecting [1 3 N].");

            if (!int.TryParse(str[1], out x))
                throw new ArgumentException($"The command {cmd} has not a X defined.");

            if(!int.TryParse(str[2], out y))
                throw new ArgumentException($"The command {cmd} has not a Y defined.");
            
            switch (str[3])
            {
                case "N":
                case "E":
                case "S":
                case "W":
                    direction = str[3][0];
                    break;
                default:
                    throw new ArgumentException($"The set position command {cmd} direction must be in (N, E, S, W).");
            }

            return (x, y, direction);
        }

        public bool IsCreateCommandValid(string cmd)
        {
            return cmd.Substring(0, 7) == "create ";
        }

        public (string, IPosition) CreateParser(string cmd, Func<IPosition> creator)
        {
            if (!IsCreateCommandValid(cmd))
                throw new ArgumentException($"The command {cmd} is invalid, it does not start with \"create \"");

            var arr = cmd.Split("create ");
            if (arr.Length < 2)
                throw new ArgumentException($"Invalid create command {cmd}, the rover name is missing.");

            return (arr[1], creator());
        }

        public IGrid CreateGrid(string cmd, Func<IGrid> ctor)
        {
            var split = cmd.Split(' ');
            var grid = ctor();
            if (split.Length < 3)
                throw new ArgumentException($"create grid command {cmd} is invalid, e.g. should be \"grid 10 20\".");
            if (!int.TryParse(split[1], out int x))
                throw new ArgumentException($"create grid command {cmd} is invalid, the first argument is not an int.");
            if (!int.TryParse(split[2], out int y))
                throw new ArgumentException($"create grid command {cmd} is invalidm the second argument is not an int.");
            grid.SetSurface(x, y);
            return grid;
        }
    }
}
