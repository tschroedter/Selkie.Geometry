using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Tests.Calculators
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class AngleIntervallCalculatorTests
    {
        private static AngleIntervallCalculator CreateSut()
        {
            return new AngleIntervallCalculator();
        }

        [Theory]
        [TestCase(0.0, 0.0, 3, Constants.TurnDirection.Clockwise, 0.0)]
        [TestCase(90.0, 90.0, 3, Constants.TurnDirection.Clockwise, 0.0)]
        [TestCase(0.0, 90.0, 3, Constants.TurnDirection.Clockwise, 45.0)]
        [TestCase(90.0, 180.0, 3, Constants.TurnDirection.Clockwise, 45.0)]
        [TestCase(180.0, 270.0, 3, Constants.TurnDirection.Clockwise, 45.0)]
        [TestCase(270.0, 360.0, 3, Constants.TurnDirection.Clockwise, 45.0)]
        [TestCase(90.0, 0.0, 3, Constants.TurnDirection.Clockwise, 135.0)]
        [TestCase(180.0, 90.0, 3, Constants.TurnDirection.Clockwise, 135.0)]
        [TestCase(270.0, 180.0, 3, Constants.TurnDirection.Clockwise, 135.0)]
        [TestCase(360.0, 270.0, 3, Constants.TurnDirection.Clockwise, 135.0)]
        [TestCase(270.0, 90.0, 3, Constants.TurnDirection.Clockwise, 90.0)]
        [TestCase(225.0, 45.0, 3, Constants.TurnDirection.Clockwise, 90.0)]
        [TestCase(0.0, 90.0, 3, Constants.TurnDirection.Counterclockwise, 135.0)]
        [TestCase(90.0, 180.0, 3, Constants.TurnDirection.Counterclockwise, 135.0)]
        [TestCase(180.0, 270.0, 3, Constants.TurnDirection.Counterclockwise, 135.0)]
        [TestCase(270.0, 360.0, 3, Constants.TurnDirection.Counterclockwise, 135.0)]
        [TestCase(90.0, 0.0, 3, Constants.TurnDirection.Counterclockwise, 45.0)]
        [TestCase(180.0, 90.0, 3, Constants.TurnDirection.Counterclockwise, 45.0)]
        [TestCase(270.0, 180.0, 3, Constants.TurnDirection.Counterclockwise, 45.0)]
        [TestCase(360.0, 270.0, 3, Constants.TurnDirection.Counterclockwise, 45.0)]
        [TestCase(270.0, 90.0, 3, Constants.TurnDirection.Counterclockwise, 90.0)]
        [TestCase(225.0, 45.0, 3, Constants.TurnDirection.Counterclockwise, 90.0)]
        public void CalculateIntervall_ReturnsIntervallAngle_ForGivenParameters(
            double fromAngleInDegrees,
            double toAngleInDegrees,
            int steps,
            Constants.TurnDirection turnDirection,
            double expectedIntervallInDegrees)
        {
            Console.WriteLine(
                              "fromAngleInDegrees: {0} toAngleInDegrees: {1} steps: {2} turnDirection: {3} expectedIntervallInDegrees: {4}",
                              fromAngleInDegrees,
                              toAngleInDegrees,
                              steps,
                              turnDirection,
                              expectedIntervallInDegrees);

            // Arrange
            double fromAngle = Angle.FromDegrees(fromAngleInDegrees).Radians;
            double toAngle = Angle.FromDegrees(toAngleInDegrees).Radians;
            double expectedIntervall = Angle.FromDegrees(expectedIntervallInDegrees).Radians;
            AngleIntervallCalculator sut = CreateSut();

            sut.FromAngleInRadians = fromAngle;
            sut.ToAngleInRadians = toAngle;
            sut.TurnDirection = turnDirection;
            sut.Steps = steps;

            // Act
            sut.Calculate();

            // Assert
            Assert.AreEqual(expectedIntervall,
                            sut.Intervall);
        }

        [Theory]
        [TestCase(0.0, 180.0, 3, Constants.TurnDirection.Clockwise, Math.PI, 0.0)]
        [TestCase(0.0, 180.0, 3, Constants.TurnDirection.Counterclockwise, Math.PI, 0.0)]
        [TestCase(0.0, 90.0, 3, Constants.TurnDirection.Clockwise, Math.PI, 45.0)]
        [TestCase(0.0, 90.0, 3, Constants.TurnDirection.Counterclockwise, Math.PI, 45.0)]
        [TestCase(0.0, 45.0, 3, Constants.TurnDirection.Clockwise, Math.PI, 22.5)]
        [TestCase(0.0, 45.0, 3, Constants.TurnDirection.Counterclockwise, Math.PI, 67.5)]
        public void CalculateIntervall_ReturnsIntervallAngle_ForMaxAngleRadians(
            double fromAngleInDegrees,
            double toAngleInDegrees,
            int steps,
            Constants.TurnDirection turnDirection,
            double maxAngleInRadians,
            double expectedIntervallInDegrees)
        {
            Console.WriteLine("fromAngleInDegrees: {0} toAngleInDegrees: {1} steps: {2} turnDirection: " +
                              "{3} maxAngleInRadians: {4} expectedIntervallInDegrees: {5}",
                              fromAngleInDegrees,
                              toAngleInDegrees,
                              steps,
                              turnDirection,
                              maxAngleInRadians,
                              expectedIntervallInDegrees);

            // Arrange
            double fromAngle = Angle.FromDegrees(fromAngleInDegrees).Radians;
            double toAngle = Angle.FromDegrees(toAngleInDegrees).Radians;
            double expectedIntervall = Angle.FromDegrees(expectedIntervallInDegrees).Radians;
            AngleIntervallCalculator sut = CreateSut();

            sut.FromAngleInRadians = fromAngle;
            sut.ToAngleInRadians = toAngle;
            sut.TurnDirection = turnDirection;
            sut.Steps = steps;
            sut.MaxAngleInRadians = maxAngleInRadians;

            // Act
            sut.Calculate();

            // Assert
            Assert.AreEqual(expectedIntervall,
                            sut.Intervall);
        }

        [Test]
        public void FromAngle_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngleIntervallCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(0.0,
                            sut.FromAngleInRadians);
        }

        [Test]
        public void Intervall_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngleIntervallCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(0.0,
                            sut.Intervall);
        }

        [Test]
        public void MaxAngleInRadians_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngleIntervallCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(BaseAngle.RadiansFor360Degrees,
                            sut.MaxAngleInRadians);
        }

        [Test]
        public void Steps_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngleIntervallCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(3,
                            sut.Steps);
        }

        [Test]
        public void ToAngle_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngleIntervallCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(0.0,
                            sut.ToAngleInRadians);
        }

        [Test]
        public void TurnDirection_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngleIntervallCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(Constants.TurnDirection.Clockwise,
                            sut.TurnDirection);
        }
    }
}