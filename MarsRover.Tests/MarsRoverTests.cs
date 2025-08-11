namespace MarsRover.Tests
{
    [TestClass]
    public class MarsRoverTests
    {
        [TestMethod]
        public void RDT_Test_Example()
        {
            int gridMaxX = 5;
            int gridMaxY = 5;

            var rover1 = new Rover(1, 2, 'N', gridMaxX, gridMaxY);
            var rover2 = new Rover(3, 3, 'E', gridMaxX, gridMaxY);

            rover1.Controls("LMLMLMLMM");
            rover2.Controls("MMRMMRMRRM");

            Assert.AreEqual(1, rover1.X);
            Assert.AreEqual(3, rover1.Y);
            Assert.AreEqual('N', rover1.Bearing);

            Assert.AreEqual(5, rover2.X);
            Assert.AreEqual(1, rover2.Y);
            Assert.AreEqual('E', rover2.Bearing);
        }

        [TestMethod]
        public void UpdatesCoordinatesCorrectly()
        {
            int gridMaxX = 5;
            int gridMaxY = 5;

            // North
            var rover1 = new Rover(2, 2, 'N', gridMaxX, gridMaxY);
            rover1.Controls("M");
            Assert.AreEqual((2, 3, 'N'), (rover1.X, rover1.Y, rover1.Bearing));

            // East
            var rover2 = new Rover(2, 2, 'E', gridMaxX, gridMaxY);
            rover2.Controls("M");
            Assert.AreEqual((3, 2, 'E'), (rover2.X, rover2.Y, rover2.Bearing));

            // South
            var rover3 = new Rover(2, 2, 'S', gridMaxX, gridMaxY);
            rover3.Controls("M");
            Assert.AreEqual((2, 1, 'S'), (rover3.X, rover3.Y, rover3.Bearing));

            // West
            var rover4 = new Rover(2, 2, 'W', gridMaxX, gridMaxY);
            rover4.Controls("M");
            Assert.AreEqual((1, 2, 'W'), (rover4.X, rover4.Y, rover4.Bearing));
        }
        [TestMethod]
        public void Boundaries_CannotCrossAnyEdge()
        {
            // Top edge, facing North
            var top = new Rover(3, 5, 'N', 5, 5);
            top.Controls("M");
            Assert.AreEqual((3, 5, 'N'), (top.X, top.Y, top.Bearing));

            // Right edge, facing East
            var right = new Rover(5, 2, 'E', 5, 5);
            right.Controls("M");
            Assert.AreEqual((5, 2, 'E'), (right.X, right.Y, right.Bearing));

            // Bottom edge, facing South
            var bottom = new Rover(1, 0, 'S', 5, 5);
            bottom.Controls("M");
            Assert.AreEqual((1, 0, 'S'), (bottom.X, bottom.Y, bottom.Bearing));

            // Left edge, facing West
            var left = new Rover(0, 4, 'W', 5, 5);
            left.Controls("M");
            Assert.AreEqual((0, 4, 'W'), (left.X, left.Y, left.Bearing));
        }
    }
}