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
            // Arrange
            IEnumerable <Angle> expectedAngles = ConvertToAngles(expectedAnglesInDegrees);

            AngelInterpolationCalculator sut = CreateSut();

            sut.FromAngle = Angle.FromDegrees(fromAngleInDegrees);
            sut.ToAngle = Angle.FromDegrees(toAngleInDegrees);
            sut.Steps = steps;
            sut.TurnDirection = turnDirection;

            // Act
            sut.Calculate();

            // Assert
            AssertAngles(expectedAngles,
                         sut.Angles);
        }

        private void AssertAngles(
            IEnumerable <Angle> expected,
            IEnumerable <Angle> actual)
        {
            Angle[] expectedArray = expected.ToArray();
            Angle[] actualArray = actual.ToArray();

            Assert.AreEqual(expectedArray.Length,
                            actualArray.Length,
                            "Length");

            for ( var i = 0 ; i < expectedArray.Length ; i++ )
            {
                Angle currentExpected = expectedArray [ i ];
                Angle currentActual = actualArray [ i ];

                Assert.AreEqual(expectedArray [ i ],
                                actualArray [ i ],
                                "[{0}] Expected = {1} but Actual = {2}".Inject(i,
                                                                               currentExpected,
                                                                               currentActual));
            }
        }

        private IEnumerable <Angle> ConvertToAngles(
            double[] anglesInRadians)
        {
            var angles = new List <Angle>();

            foreach ( double anglesInRadian in anglesInRadians )
            {
                angles.Add(Angle.FromDegrees(anglesInRadian));
            }

            return angles;
        }

        [Test]
        public void Calculate_ReturnsTheCorrectNumberOfAngles_ForGivenSteps()
        {
            // Arrange
            AngelInterpolationCalculator sut = CreateSut();

            sut.FromAngle = Angle.ForZeroDegrees;
            sut.ToAngle = Angle.For90Degrees;
            sut.Steps = 10;

            // Act
            sut.Calculate();

            // Assert
            IEnumerable <Angle> actual = sut.Angles;

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
            Assert.AreEqual(Angle.Unknown,
                            sut.FromAngle);
        }

        [Test]
        public void ToAngle_ReturnsDefault_WhenCalled()
        {
            // Arrange
            AngelInterpolationCalculator sut = CreateSut();

            // Act
            // Assert
            Assert.AreEqual(Angle.Unknown,
                            sut.ToAngle);
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