using System;
using NUnit.Framework;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.ThreeD.Primitives
{
    [TestFixture]
    internal sealed class AzimuthAngleTests
    {
        [Test]
        [TestCase(-180.0, 180.0)]
        [TestCase(-135.0, 225.0)]
        [TestCase(-90.0, 270.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(90.0, 90.0)]
        [TestCase(180.0, 180.0)]
        [TestCase(270.0, 270.0)]
        [TestCase(360.0, 0.0)]
        [TestCase(450.0, 90.0)]
        [TestCase(540.0, 180.0)]
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
            AzimuthAngle sut = AzimuthAngle.FromDegrees(degrees);

            // Assert
            NUnitHelper.AssertDegrees(expectedDegrees,
                                      sut.Degrees);
        }

        [Test]
        [TestCase(-Math.PI, Math.PI)]
        [TestCase(-Math.PI / 2, Math.PI * 1.5)]
        [TestCase(0.0, 0.0)]
        [TestCase(Math.PI / 2.0, Math.PI / 2.0)]
        [TestCase(Math.PI, Math.PI)]
        [TestCase(Math.PI * 1.5, Math.PI * 1.5)]
        [TestCase(Math.PI * 2.0, 0.0)]
        [TestCase(Math.PI * 2.5, Math.PI / 2.0)]
        [TestCase(Math.PI * 3.0, Math.PI)]
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
            AzimuthAngle sut = AzimuthAngle.FromRadians(radians);

            // Assert
            NUnitHelper.AssertDegrees(expectedRadians,
                                      sut.Radians);
        }

        [Test]
        [TestCase(0.0, 0.0, true)]
        [TestCase(0.0, 1.0, false)]
        [TestCase(1.0, 0.0, true)]
        [TestCase(1.0, 1.0, true)]
        public void OperatorGreaterOrEqualThan_AddsAngles_ForGivenAngles(
            double oneInDegrees,
            double twoInDegrees,
            bool expected)
        {
            // Arrange
            AzimuthAngle one = AzimuthAngle.FromDegrees(oneInDegrees);
            AzimuthAngle two = AzimuthAngle.FromDegrees(twoInDegrees);

            // Act
            // Assert
            Assert.AreEqual(expected,
                            one >= two);
        }

        [Test]
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(90.0, 0.0, 90.0)]
        [TestCase(90.0, 45.0, 45.0)]
        [TestCase(90.0, 90.0, 0.0)]
        [TestCase(90.0, 180.0, 270.0)]
        [TestCase(180.0, 180.0, 360.0)]
        [TestCase(270.0, 90.0, 180.0)]
        [TestCase(270.0, 135.0, 135.0)]
        public void OperatorMinus_AddsAngles_ForGivenAngles(
            double oneInDegrees,
            double twoInDegrees,
            double expectedInDegrees)
        {
            // Arrange
            AzimuthAngle one = AzimuthAngle.FromDegrees(oneInDegrees);
            AzimuthAngle two = AzimuthAngle.FromDegrees(twoInDegrees);
            AzimuthAngle expected = AzimuthAngle.FromDegrees(expectedInDegrees);

            // Act
            AzimuthAngle actual = one - two;

            // Assert
            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }

        [Test]
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(90.0, 0.0, 90.0)]
        [TestCase(90.0, 45.0, 135.0)]
        [TestCase(90.0, 90.0, 180.0)]
        [TestCase(90.0, 180.0, 270.0)]
        [TestCase(180.0, 180.0, 0.0)]
        [TestCase(270.0, 90.0, 0.0)]
        [TestCase(270.0, 135.0, 45.0)]
        public void OperatorPlus_AddsAngles_ForGivenAngles(
            double oneInDegrees,
            double twoInDegrees,
            double expectedInDegrees)
        {
            // Arrange
            AzimuthAngle one = AzimuthAngle.FromDegrees(oneInDegrees);
            AzimuthAngle two = AzimuthAngle.FromDegrees(twoInDegrees);
            AzimuthAngle expected = AzimuthAngle.FromDegrees(expectedInDegrees);

            // Act
            AzimuthAngle actual = one + two;

            // Assert
            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }
    }
}