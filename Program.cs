using Rover.Position;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Rover
{
    class Program
    {
        static ICommandParser _parser = new CommandParser();
        static IDictionary<string, IPosition> _positions = new Dictionary<string, IPosition>();

        static IGrid CreateGrid(string cmd)
        {
            return ((IGridCreate)_parser).CreateGrid(cmd, () => new Grid());
        }

        static void SetPosition(string cmd, IPosition pos)
        {
            if (pos == null)
            {
                Console.WriteLine("A rover must first be selected or created.");
                return;
            }

            try
            {
                (int x, int y, char d) = ((ISetPositionParser)_parser).ParseSetPositionCommand(cmd);
                pos.SetPosition(x, y, d);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Unable to execute the set position command : {e.Message}");
            }
        }

        static void Move(string cmd, IPosition pos)
        {
            if (pos == null)
            {
                Console.WriteLine("A rover must first be selected or created.");
                return;
            }

            IEnumerable<ICommand> lstCmd = new List<ICommand>();
            try
            {
                lstCmd = ((IDisplacementParse)_parser).ParseDisplacement(cmd, pos);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Unable to execute the displacement command : {e.Message}");
            }
            foreach (var command in lstCmd)
            {
                command.Execute();
            }
            Console.WriteLine(pos.ToString());
        }

        static (string, IPosition) Create(string cmd)
        {
            return ((ICreateParser)_parser).CreateParser(cmd, () => new Position.Position());
        }

        static void Main(string[] args)
        {
            var exit = false;
            IPosition current = null;
            IGrid grid = null;
            Console.WriteLine("Starting, enter command\ncreate grid : -grid 30 40\ncreate rover : -create rover1 (must not start with - L R or number)),\nexecute command for rover :\nenter name then\nset position e.g.[0 0 N]\nor\nmove R1L5LRLR1234...");
            while(!exit)
            {
                var cmd = Console.ReadLine();
                if(cmd == "exit")
                {
                    exit = true;
                    Console.WriteLine("Exiting");
                    break;
                }
                try
                {
                    if(cmd[0] == '-')
                    {
                        if(cmd.Length < 2)
                        {
                            Console.WriteLine("A command starting with - must be -create or -grid.");
                        }
                        if (cmd[1] == 'g')
                        {
                            grid = CreateGrid(cmd.Substring(1));
                            foreach (var pos in _positions.Values)
                            {
                                pos.SetGrid(grid);
                            }
                        }
                        else if (cmd[1] == 'c')
                        {
                            var res = Create(cmd.Substring(1));
                            _positions.Add(res.Item1, res.Item2);
                            current = res.Item2;
                        }
                        else
                            Console.WriteLine("Unrecognised command.");
                    }
                    else if (cmd[0] == '[')
                    {
                        SetPosition(cmd, current);
                    }
                    else if(cmd[0] == 'L' || cmd[0] == 'R' || char.IsNumber(cmd[0]))
                    {
                        Move(cmd, current);
                    }
                    else 
                    {
                        if (_positions.ContainsKey(cmd))
                            current = _positions[cmd];
                        else
                            Console.WriteLine("Unrecognised command or rover name.");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Program exception {e.Message}, terminating.");
                    Console.ReadLine();
                    exit = true;
                }
            }
        }
    }
}
