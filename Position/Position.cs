using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Rover.Position
{
    public class Position : IPosition
    {
        int _x;
        int _y;
        eDirection _direction;

        public int X => _x;
        public int Y => _y;
        public eDirection Direction => _direction;
        IGrid _grid;

        public Position() { }
        public Position(int x, int y, char direction) : base()
        {
            SetPosition(x, y, direction);
        }

        public void SetGrid(IGrid grid)
        {
            _grid = grid;
            _x = _grid.CheckXBoundaries(_x);
            _y = _grid.CheckYBoundaries(_y);
        }

        public void Rotate(char c)
        {
            int increment = c switch
            {
                'L' => -1,
                'R' => 1,
                _ => throw new ArgumentException("Unrecognised char rotation")
            };

            if(_direction == eDirection.North && increment < 0)
                 _direction = eDirection.West;
            else _direction = (eDirection)(((int)_direction + increment) % 4);
        }

        public void Move(int i)
        {
            switch(_direction)
            {
                case eDirection.North:
                    _y = _grid == null ? _y + i : _grid.CheckYBoundaries(_y + i);
                    break;
                case eDirection.South:
                    _y = _grid == null ? _y - i < 0 ? 0 : _y - i : _grid.CheckYBoundaries(_y - i);
                    break;
                case eDirection.West:
                    _x = _grid == null ? _x - i < 0 ? 0 : _x - i : _grid.CheckXBoundaries(_x - i);
                    break;
                case eDirection.East:
                    _x = _grid == null ? _x + i : _grid.CheckXBoundaries(_x + i);
                    break;
                default:
                    throw new InvalidOperationException("Unrecognised direction");
            }
        }

        public override string ToString()
        {
            var arrDirection = Direction.ToString().ToCharArray();
            return $"[{X}, {Y}, {arrDirection[0]}]";
        }

        public void SetPosition(int x, int y, char direction)
        {
            if (x < 0)
                throw new ArgumentException($"invalid x = {x}, can only be positive.");
            if (y < 0)
                throw new ArgumentException($"invalid y = {y}, can only be positive.");

            _x = _grid == null ? x : _grid.CheckXBoundaries(x);
            _y = _grid == null ? y : _grid.CheckYBoundaries(y);
            _direction = direction switch
            {
                'N' => eDirection.North,
                'E' => eDirection.East,
                'W' => eDirection.West,
                'S' => eDirection.South,
                _ => throw new ArgumentException("Unrecognised direction."),
            };
        }
    }
}
