using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Position.Tests
{
    [TestClass()]
    public class MoveTests
    {
        [TestMethod()]
        public void Move_Creation_Test()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Move(10, null));
            Assert.ThrowsException<ArgumentException>(() => new Move(-8, new Position(0, 0, 'N')));
        }

        [TestMethod()]
        public void Execute_Move_Test()
        {
            var p = new Position(0, 0, 'N');
            var m = new Move(5, p);
            m.Execute();
            Assert.AreEqual(5, p.Y);
        }
    }
}