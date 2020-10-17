using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Position.Tests
{
    [TestClass()]
    public class RotateTests
    {
        [TestMethod()]
        public void Rotate_Creation_Test()
        {
            Assert.ThrowsException<ArgumentNullException>(()=>new Rotate('L', null));
            Assert.ThrowsException<ArgumentException>(() => new Rotate('O', new Position(0, 0, 'N')));
        }

        [TestMethod()]
        public void Execute_Rotate_Right_Test()
        {
            var p = new Position(0, 0, 'N');
            var r = new Rotate('R', p);
            r.Execute();
            Assert.AreEqual(eDirection.East,p.Direction);
        }

        [TestMethod()]
        public void Execute_Rotate_Left_Test()
        {
            var p = new Position(0, 0, 'N');
            var r = new Rotate('L', p);
            r.Execute();
            Assert.AreEqual(eDirection.West, p.Direction);
        }
    }
}