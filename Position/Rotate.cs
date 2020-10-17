using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Position
{
    public class Rotate : ICommand
    {
        char _rotation;
        IPosition _position;
        public Rotate(char rotation, IPosition position) 
        {
            _position = position ?? throw new ArgumentNullException("position cannot be null.");

            switch (rotation)
            {
                case 'R':
                case 'L':
                    break;
                default:
                    throw new ArgumentException("rotation must be 'L' or 'R'.");
            }

            _rotation = rotation;
        }

        public void Execute() 
        {
            _position.Rotate(_rotation);
        }
    }
}
