using Rover.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover
{
    public interface IGrid
    {
        void SetSurface(int lengthX, int lenghtY);
        int CheckXBoundaries(int pos);
        int CheckYBoundaries(int pos);
    }
}
