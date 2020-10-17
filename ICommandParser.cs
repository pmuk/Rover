using Rover.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover
{
    public interface ICommandParser { }
    public interface IGridCreate : ICommandParser
    {
        IGrid CreateGrid(string cmd, Func<IGrid> ctor);
    }
    public interface IDisplacementParse : ICommandParser
    {
        bool IsDisplacementCommandValid(string cmd);
        IEnumerable<ICommand> ParseDisplacement(string cmd, IPosition position);
    }

    public interface ISetPositionParser : ICommandParser
    {
        bool IsSetPositionCommandValid(string cmd);
        (int, int, char) ParseSetPositionCommand(string cmd);
    }

    public interface ICreateParser : ICommandParser
    {
        bool IsCreateCommandValid(string cmd);
        (string, IPosition) CreateParser(string cmd, Func<IPosition> creator);
    }
}
