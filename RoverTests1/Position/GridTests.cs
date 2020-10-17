using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Position.Tests
{
    [TestClass()]
    public class GridTests
    {
        [TestMethod()]
        public void CheckXBoundariesTest()
        {
            var g = new Grid();
            g.SetSurface(10, 20);
            Assert.AreEqual(10, g.CheckXBoundaries(20));
            Assert.AreEqual(0, g.CheckXBoundaries(-5));
        }

        [TestMethod()]
        public void CheckYBoundariesTest()
        {
            var g = new Grid();
            g.SetSurface(10, 20);
            Assert.AreEqual(20, g.CheckYBoundaries(21));
            Assert.AreEqual(0, g.CheckYBoundaries(-1));
        }
    }
}