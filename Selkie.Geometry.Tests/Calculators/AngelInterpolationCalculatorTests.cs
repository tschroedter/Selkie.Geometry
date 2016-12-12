using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Primitives;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.Calculators
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class AngelInterpolationCalculatorTests
    {
        private static AngelInterpolationCalculator CreateSut()
        {
            var angleIntervallCalculator = new AngleIntervallCalculator();

            return new AngelInterpolationCalculator(angleIntervallCalculator);
        }

        [Theory]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Calculate_ThrowsException_ForInvalidSteps(
            int steps)
        {
            // Arrange
            AngelInterpolationCalculator sut = CreateSut();

            sut.Steps = steps;

            // Act
            // Assert
            Assert.Throws <ArgumentException>(() => sut.Calculate());
        }

        [Theory]
        [TestCase(0.0, 90.0, 10, Constants.TurnDirection.Clockwise,
             new[]
             {
                 0.0,
                 10.0,
                 20.0,
                 30.0,
                 40.0,
                 50.0,
                 60.0,
                 70.0,
                 80.0,
                 90.0
             })]
        [TestCase(0.0, 90.0, 3, Constants.TurnDirection.Clockwise,
             new[]
             {
                 0.0,
                 45.0,
                 90.0
             }
         )]
        [TestCase(90.0, 0.0, 3, Constants.TurnDirection.Clockwise,
             new[]
             {
                 90.0,
                 225.0,
                 0.0
             }
         )]
        [TestCase(0.0, 90.0, 10, Constants.TurnDirection.Counterclockwise,
             new[]
             {
                 0.0,
                 330.0,
                 300.0,
                 270.0,
                 240.0,
                 210.0,
                 180.0,
                 150.0,
                 120.0,
                 90.0
             })]
        [TestCase(0.0, 90.0, 3, Constants.TurnDirection.Counterclockwise,
             new[]
             {
                 0.0,
                 225.0,
                 90.0
             }
         )]
        [TestCase(90.0, 0.0, 3, Constants.TurnDirection.Counterclockwise,
             new[]
             {
                 90.0,
                 45.0,
                 0.0
             }
         )]
        public void Calculate_ReturnsTheCorrectNumberOfAngles_ForGivenSteps(
            double fromAngleInDegrees,
            double toAngleInDegrees,
            int steps,
            Constants.TurnDirection turnDirection,
            double[] expectedAnglesInDegrees)
        {
            Console.WriteLine("fromAngleInDegrees: {0} toAngleInDegrees: {1} " +
                              "steps: {2} turnDirection: {3} expectedAnglesInDegrees: {4}",
                              fromAngleInDegrees,
                              toAngleInDegrees,
                              steps,
                              turnDirection,
                              expectedAnglesInDegrees);

            // Arrange
            IEnumerable <double> expectedAngles = ConvertToAngles(expectedAnglesInDegrees);

            AngelInterpolationCalculator sut = CreateSut();

            sut.FromAngleInRadians = Angle.FromDegrees(fromAngleInDegrees).Radians;
            sut.ToAngleInRadians = Angle.FromDegrees(toAngleInDegrees).Radians;
            sut.Steps = steps;
            sut.TurnDirection = turnDirection;

            // Act
            sut.Calculate();

            // Assert
            AssertAngles(expectedAngles,
                         sut.AnglesInRadians);
        }

        [TestCase(0.0, 90.0, 3, Constants.TurnDirection.Clockwise, Math.PI,
             new[]
             {
                 0.0,
                 45.0,
                 90.0
             }
         )]
        [TestCase(0.0, 90.0, 3, Constants.TurnDirection.Counterclockwise, Math.PI,
             new[]
             {
                 0.0,
                 135.0,
                 90.0
             }
         )]
        public void Calculate_ReturnsTheCorrectNumberOfAngles_ForMaxAngleInRadians(
            double fromAngleInDegrees,
            double toAngleInDegrees,
            int steps,
            Constants.TurnDirection turnDirection,
            double maxAngleInRadians,
            double[] expectedAnglesInDegrees)
        {
            // Arrange
            IEnumerable <double> expectedAngles = ConvertToAngles(expectedAnglesInDegrees);

            AngelInterpolationCalculator sut = CreateSut();

            sut.FromAngleInRadians = Angle.FromDegrees(fromAngleInDegrees).Radians;
            sut.ToAngleInRadians = Angle.FromDegrees(toAngleInDegrees).Radians;
            sut.Steps = steps;
            sut.TurnDirection = turnDirection;
            sut.MaxAngleInRadians = maxAngleInRadians;

            // Act
            sut.Calculate();

            // Assert
            AssertAngles(expectedAngles,
                         sut.AnglesInRadians);
        }

        private void AssertAngles(
            IEnumerable <double> expected,
            IEnumerable <double> actual)
        {
            double[] expectedArray = expected.ToArray();
            double[] actualArray = actual.ToArray();

            Assert.AreEqual(expectedArray.Length,
                            actualArray.Length,
                            "Length");

            for ( var i = 0 ; i < expectedArray.Length ; i++ )
            {
                double currentExpected = expectedArray [ i ];
                double currentActual = actualArray [ i ];

                Console.WriteLine("Testing: [{0}] Expected = {1} but Actual = {2}".Inject(i,
                                                                                          currentExpected,
                                                                                          currentActual));

                NUnitHelper.AssertRadians(expectedArray [ i ],
                                          actualArray [ i ]);
            }
        }

        private IEnumerable <double> ConvertToAngles(
            double[] anglesInRadians)
        {
            var angles = new List <double>();

            foreach ( double anglesInRadian in anglesInRadians )
                angles.Add(Angle.FromDegrees(anglesInRadian).Radians);

            return angles;
        }

        [Test]
        public void Calculate_ReturnsTheCorrectNumberOfAngles_ForGivenSteps()
        {
            // Arrange
            AngelInterpolationCalculator sut = CreateSut();

            sut.FromAngleInRadians = Angle.ForZeroDegrees.Radians;
            sut.ToAngleInRadians = Angle.For90Degrees.Radians;
            sut.Steps = 10;

            // Act
            sut.Calculate();

            // Assert
            IEnumerable <double> actual = sut.AnglesInRadians;

            Assert.AreEqual(10,
                            actual.Count());
        }

        [Test]
        public void FromAngle_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngelInterpolationCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(0.0,
                            sut.FromAngleInRadians);
        }

        [Test]
        public void MaxAngleInRadians_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngelInterpolationCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(BaseAngle.RadiansFor360Degrees,
                            sut.MaxAngleInRadians);
        }

        [Test]
        public void ToAngle_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngelInterpolationCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(0.0,
                            sut.ToAngleInRadians);
        }

        [Test]
        public void TurnDirection_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngelInterpolationCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(Constants.TurnDirection.Clockwise,
                            sut.TurnDirection);
        }
    }
}