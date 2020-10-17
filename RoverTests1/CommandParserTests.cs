using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover;
using Rover.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Rover.Tests
{
    [TestClass()]
    public class CommandParserTests
    {
        [TestMethod()]
        public void Command_Diplacement_Is_valid()
        {
            string cmd = "RRLR1R2R45L9L6L45";
            Assert.IsTrue(new CommandParser().IsDisplacementCommandValid(cmd));
            cmd = "ALRR345";
            Assert.IsFalse(new CommandParser().IsDisplacementCommandValid(cmd));
        }

        [TestMethod()]
        public void Command_Displacement_Is_Invalid()
        {
            string cmd = "ALRR345";
            Assert.IsFalse(new CommandParser().IsDisplacementCommandValid(cmd));
        }

        [TestMethod()]
        public void Test_Parse_Command()
        {
            string cmd = "RRLR1R2R45L9L6L45";
            CommandParser parser = new CommandParser();
            IPosition p = new Rover.Position.Position(0, 0, 'N');
            var lstCmd = parser.ParseDisplacement(cmd, p) as List<ICommand>;
            Assert.AreEqual(15, lstCmd.Count());
            Assert.IsTrue(lstCmd[0] is Rover.Position.Rotate);//R
            Assert.IsTrue(lstCmd[1] is Rover.Position.Rotate);//R
            Assert.IsTrue(lstCmd[2] is Rover.Position.Rotate);//L
            Assert.IsTrue(lstCmd[3] is Rover.Position.Rotate);//R
            Assert.IsTrue(lstCmd[4] is Rover.Position.Move);//1
            Assert.IsTrue(lstCmd[5] is Rover.Position.Rotate);//R
            Assert.IsTrue(lstCmd[6] is Rover.Position.Move);//2
            Assert.IsTrue(lstCmd[7] is Rover.Position.Rotate);//R
            Assert.IsTrue(lstCmd[8] is Rover.Position.Move);//45
            Assert.IsTrue(lstCmd[9] is Rover.Position.Rotate);//L
            Assert.IsTrue(lstCmd[10] is Rover.Position.Move);//9
            Assert.IsTrue(lstCmd[11] is Rover.Position.Rotate);//L
            Assert.IsTrue(lstCmd[12] is Rover.Position.Move);//6
            Assert.IsTrue(lstCmd[13] is Rover.Position.Rotate);//L
            Assert.IsTrue(lstCmd[14] is Rover.Position.Move);//45
        }

        [TestMethod()]
        public void Parse__Command_Exception()
        {
            string cmd = "ALRR345";
            CommandParser parser = new CommandParser();
            IPosition p = new Rover.Position.Position(0, 0, 'N');
            Assert.ThrowsException<ArgumentException>(() => parser.ParseDisplacement(cmd, p));
        }

        [TestMethod()]
        public void SetPositionCommand_Is_Valid()
        {
            string cmd = "[0 0 N]";
            CommandParser parser = new CommandParser();
            Assert.IsTrue(parser.IsSetPositionCommandValid(cmd));
        }

        [TestMethod()]
        public void SetPositionCommand_Is_Invalid()
        {
            string cmd = "[0 0 P]";
            CommandParser parser = new CommandParser();
            Assert.IsFalse(parser.IsSetPositionCommandValid(cmd));
        }

        [TestMethod()]
        public void SetPosition_Test()
        {
            string cmd = "[15 68768 N]";
            CommandParser parser = new CommandParser();
            var res = parser.ParseSetPositionCommand(cmd);
            Assert.AreEqual(15, res.Item1);
            Assert.AreEqual(68768, res.Item2);
            Assert.AreEqual('N', res.Item3);
        }

        [TestMethod()]
        public void SetPosition_Length_Failure()
        {
            string cmd = "[15 68768 N ]";
            CommandParser parser = new CommandParser();
            Assert.ThrowsException<ArgumentException>(() => parser.ParseSetPositionCommand(cmd));
        }

        [TestMethod()]
        public void SetPosition_Opening_Failure()
        {
            string cmd = "15 68768 N]";
            CommandParser parser = new CommandParser();
            Assert.ThrowsException<ArgumentException>(() => parser.ParseSetPositionCommand(cmd));
        }

        [TestMethod()]
        public void SetPosition_Closing_Failure()
        {
            string cmd = "[15 68768 N";
            CommandParser parser = new CommandParser();
            Assert.ThrowsException<ArgumentException>(() => parser.ParseSetPositionCommand(cmd));
        }

        [TestMethod()]
        public void SetPosition_X_Failure()
        {
            string cmd = "[plo 68768 N]";
            CommandParser parser = new CommandParser();
            Assert.ThrowsException<ArgumentException>(() => parser.ParseSetPositionCommand(cmd));
        }

        [TestMethod()]
        public void SetPosition_Y_Failure()
        {
            string cmd = "[15 oioiui N]";
            CommandParser parser = new CommandParser();
            Assert.ThrowsException<ArgumentException>(() => parser.ParseSetPositionCommand(cmd));
        }

        [TestMethod()]
        public void SetPosition_Direction_Failure()
        {
            string cmd = "[15 9879 I]";
            CommandParser parser = new CommandParser();
            Assert.ThrowsException<ArgumentException>(() => parser.ParseSetPositionCommand(cmd));
        }

        [TestMethod()]
        public void CreateParserTest()
        {
            CommandParser p = new CommandParser();
            var res = p.CreateParser("create strange name rover234", () => new Position.Position());
            Assert.AreEqual("strange name rover234", res.Item1);
            Assert.AreEqual(typeof(IPosition), res.Item2.GetType().GetInterface("IPosition"));
        }

        [TestMethod()]
        public void CreateGrid_test()
        {
            var cmd = "grid 10 20";
            var parser = new CommandParser();
            var t = parser.CreateGrid(cmd, () => new Grid());
            Assert.AreEqual(typeof(Position.Grid), t.GetType());
        }
    }
}