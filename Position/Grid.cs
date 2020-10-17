using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Position
{
    public class Grid : IGrid
    {
        int _minX;
        int _maxX;
        int _minY;
        int _maxY;

        public int CheckXBoundaries(int pos)
        {
            if (pos < _minX)
                return _minX;
            else if (pos > _maxX)
                return _maxX;
            else return pos;
        }

        public int CheckYBoundaries(int pos)
        {
            if (pos < _minY)
                return _minY;
            else if (pos > _maxY)
                return _maxY;
            else return pos;
        }

        public void SetSurface(int lengthX, int lenghtY)
        {
            _maxX = lengthX;
            _maxY = lenghtY;
        }
    }
}
