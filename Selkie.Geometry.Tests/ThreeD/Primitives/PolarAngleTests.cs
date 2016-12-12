using System;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.ThreeD.Primitives
{
    [TestFixture]
    internal sealed class PolarAngleTests
    {
        [Test]
        [TestCase(-180.0, 0.0)]
        [TestCase(-135.0, 45.0)]
        [TestCase(-90.0, 90.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(90.0, 90.0)]
        [TestCase(180.0, 0.0)]
        [TestCase(270.0, 90.0)]
        [TestCase(360.0, 0.0)]
        [TestCase(450.0, 90.0)]
        [TestCase(540.0, 0.0)]
        public void FromDegrees_CreatesInstanceWithRadians_ForGivenValues(
            double degrees,
            double expectedDegrees)
        {
            Console.WriteLine("Testing:");
            Console.WriteLine("degrees         = {0:F6}",
                              degrees);
            Console.WriteLine("expectedDegrees = {0:F6}",
                              expectedDegrees);

            // Arrange
            // Act
            PolarAngle sut = PolarAngle.FromDegrees(degrees);

            // Assert
            NUnitHelper.AssertDegrees(expectedDegrees,
                                      sut.Degrees);
        }

        [Test]
        [TestCase(-Math.PI, 0.0)]
        [TestCase(-Math.PI / 2, Math.PI / 2.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(Math.PI / 2.0, Math.PI / 2.0)]
        [TestCase(Math.PI, 0.0)]
        [TestCase(Math.PI * 1.5, Math.PI / 2.0)]
        [TestCase(Math.PI * 2.0, 0.0)]
        [TestCase(Math.PI * 2.5, Math.PI / 2.0)]
        [TestCase(Math.PI * 3.0, 0.0)]
        public void FromRadians_CreatesInstanceWithRadians_ForGivenValues(
            double radians,
            double expectedRadians)
        {
            Console.WriteLine("Testing:");
            Console.WriteLine("radians         = {0:F6}",
                              radians);
            Console.WriteLine("expectedRadians = {0:F6}",
                              expectedRadians);

            // Arrange
            // Act
            PolarAngle sut = PolarAngle.FromRadians(radians);

            // Assert
            NUnitHelper.AssertDegrees(expectedRadians,
                                      sut.Radians);
        }

        [Test]
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(90.0, 0.0, 90.0)]
        [TestCase(90.0, 45.0, 45.0)]
        [TestCase(90.0, 90.0, 0.0)]
        [TestCase(90.0, 180.0, 90.0)]
        [TestCase(180.0, 180.0, 0.0)]
        [TestCase(270.0, 90.0, 0.0)]
        [TestCase(270.0, 135.0, 135.0)]
        public void OperatorMinus_AddsAngles_ForGivenAngles(
            double oneInDegrees,
            double twoInDegrees,
            double expectedInDegrees)
        {
            // Arrange
            PolarAngle one = PolarAngle.FromDegrees(oneInDegrees);
            PolarAngle two = PolarAngle.FromDegrees(twoInDegrees);
            PolarAngle expected = PolarAngle.FromDegrees(expectedInDegrees);

            // Act
            PolarAngle actual = one - two;

            // Assert
            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }

        [Test]
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(90.0, 0.0, 90.0)]
        [TestCase(90.0, 45.0, 135.0)]
        [TestCase(90.0, 90.0, 180.0)]
        [TestCase(90.0, 180.0, 90.0)]
        [TestCase(180.0, 180.0, 0.0)]
        [TestCase(270.0, 90.0, 0.0)]
        [TestCase(270.0, 135.0, 45.0)]
        public void OperatorPlus_AddsAngles_ForGivenAngles(
            double oneInDegrees,
            double twoInDegrees,
            double expectedInDegrees)
        {
            // Arrange
            PolarAngle one = PolarAngle.FromDegrees(oneInDegrees);
            PolarAngle two = PolarAngle.FromDegrees(twoInDegrees);
            PolarAngle expected = PolarAngle.FromDegrees(expectedInDegrees);

            // Act
            PolarAngle actual = one + two;

            // Assert
            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }

        [Test]
        public void ToString_ReturnsString_WhenCalled()
        {
            // Arrange
            PolarAngle sut = PolarAngle.FromDegrees(BaseAngle.RadiansFor90Degrees);

            // Act
            string actual = sut.ToString();

            // Assert
            Assert.AreEqual("[Radians: 0.027416 Degrees: 1.570796]",
                            actual);
        }
    }
}