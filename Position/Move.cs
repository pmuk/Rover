using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Position
{
    public class Move : ICommand
    {
        int _movement;
        IPosition _position;
        public Move(int movement, IPosition position)
        {
            _position = position ?? throw new ArgumentNullException("position cannot be null.");

            if (movement < 0)
                throw new ArgumentException("movement cannot be negative.");

            _movement = movement;
        }

        public void Execute() 
        {
            _position.Move(_movement);
        }
    }
}
