using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Position.Tests
{
    [TestClass()]
    public class PositionTests
    {
        [TestMethod()]
        public void Create_Position_Test()
        {
            var p = new Position(10, 10, 'N');
            Assert.AreEqual(p.Direction, eDirection.North);
            Assert.AreEqual(10, p.X);
            Assert.AreEqual(10, p.Y);
            p = new Position(10, 10, 'W');
            Assert.AreEqual(p.Direction, eDirection.West);
            p = new Position(10, 10, 'S');
            Assert.AreEqual(p.Direction, eDirection.South);
            p = new Position(10, 10, 'E');
            Assert.AreEqual(p.Direction, eDirection.East);
        }

        [TestMethod()]
        public void Create_Position_Exception()
        {
            Assert.ThrowsException<ArgumentException>(() => new Position(0, 0, 'Y'));
            Assert.ThrowsException<ArgumentException>(() => new Position(-1, 9, 'N'));
            Assert.ThrowsException<ArgumentException>(() => new Position(0, -9, 'E'));
        }

        [TestMethod()]
        public void Rotate_Position_Right_East()
        {
            var p = new Position(10, 10, 'N');
            p.Rotate('R');
            Assert.AreEqual(p.Direction, eDirection.East);

        }

        [TestMethod()]
        public void Rotate_Position_Right_South()
        {
            var p = new Position(10, 10, 'E');
            p.Rotate('R');
            Assert.AreEqual(p.Direction, eDirection.South);
        }

        [TestMethod()]
        public void Rotate_Position_Right_West()
        {
            var p = new Position(10, 10, 'S');
            p.Rotate('R');
            Assert.AreEqual(p.Direction, eDirection.West);
        }

        [TestMethod()]
        public void Rotate_Position_Right_North()
        {
            var p = new Position(10, 10, 'W');
            p.Rotate('R');
            Assert.AreEqual(p.Direction, eDirection.North);
        }

        [TestMethod()]
        public void Rotate_Position_Left_West()
        {
            var p = new Position(10, 10, 'N');
            p.Rotate('L');
            Assert.AreEqual(p.Direction, eDirection.West);
        }

        [TestMethod()]
        public void Rotate_Position_Left_South()
        {
            var p = new Position(10, 10, 'W');
            p.Rotate('L');
            Assert.AreEqual(p.Direction, eDirection.South);
        }

        [TestMethod()]
        public void Rotate_Position_Left_East()
        {
            var p = new Position(10, 10, 'S');
            p.Rotate('L');
            Assert.AreEqual(p.Direction, eDirection.East);
        }

        [TestMethod()]
        public void Rotate_Position_Left_North()
        {
            var p = new Position(10, 10, 'E');
            p.Rotate('L');
            Assert.AreEqual(p.Direction, eDirection.North);
        }

        [TestMethod()]
        public void Rotate_Position_Exception()
        {
            var p = new Position(10, 10, 'N');
            Assert.ThrowsException<ArgumentException>(() => p.Rotate('A'));
        }

        [TestMethod()]
        public void Move_X_East()
        {
            var p = new Position(10, 10, 'E');
            p.Move(10);
            Assert.AreEqual(p.X, 20);

        }

        [TestMethod()]
        public void Move_X_West()
        {
            var p = new Position(10, 10, 'W');
            p.Move(10);
            Assert.AreEqual(p.X, 0);
        }

        [TestMethod()]
        public void Move_Y_North()
        {
            var p = new Position(10, 10, 'N');
            p.Move(10);
            Assert.AreEqual(p.Y, 20);
        }

        [TestMethod()]
        public void Move_Y_South()
        {
            var p = new Position(10, 10, 'S');
            p.Move(10);
            Assert.AreEqual(p.Y, 0);
        }

        [TestMethod()]
        public void North_ToString()
        {
            var p = new Position(0, 5, 'N');
            Assert.AreEqual("[0, 5, N]", p.ToString());
        }

        [TestMethod()]
        public void East_ToString()
        {
            var p = new Position(0, 5, 'E');
            Assert.AreEqual("[0, 5, E]", p.ToString());
        }

        [TestMethod()]
        public void South_ToString()
        {
            var p = new Position(0, 5, 'S');
            Assert.AreEqual("[0, 5, S]", p.ToString());
        }

        [TestMethod()]
        public void West_ToString()
        {
            var p = new Position(0, 5, 'W');
            Assert.AreEqual("[0, 5, W]", p.ToString());
        }

        [TestMethod()]
        public void SetGridTest()
        {
            var p = new Position(25, 35, 'N');
            var g = new Grid();
            g.SetSurface(20, 30);
            p.SetGrid(g);
            Assert.AreEqual(20, p.X);
            Assert.AreEqual(30, p.Y);
        }
    }
}