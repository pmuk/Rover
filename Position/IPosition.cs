using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Position
{
    public interface IPosition
    {
        int X { get; }
        int Y { get; }
        eDirection Direction { get; }
        void Rotate(char c);
        void Move(int i);
        void SetPosition(int x, int y, char direction);
        void SetGrid(IGrid grid);
    }
}
