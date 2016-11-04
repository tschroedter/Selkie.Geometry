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
            // Arrange
            Angle fromAngle = Angle.FromDegrees(fromAngleInDegrees);
            Angle toAngle = Angle.FromDegrees(toAngleInDegrees);
            Angle expectedIntervall = Angle.FromDegrees(expectedIntervallInDegrees);
            AngleIntervallCalculator sut = CreateSut();

            sut.FromAngle = fromAngle;
            sut.ToAngle = toAngle;
            sut.TurnDirection = turnDirection;
            sut.Steps = steps;

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
            Assert.AreEqual(Angle.Unknown,
                            sut.FromAngle);
        }

        [Test]
        public void Intervall_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngleIntervallCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(Angle.Unknown,
                            sut.Intervall);
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
            Assert.AreEqual(Angle.Unknown,
                            sut.ToAngle);
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