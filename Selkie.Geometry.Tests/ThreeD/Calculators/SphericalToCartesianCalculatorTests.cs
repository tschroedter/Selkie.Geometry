using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Tests.ThreeD.Primitives;
using Selkie.Geometry.ThreeD.Calculators;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.Tests.ThreeD.Calculators
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SphericalToCartesianCalculatorTests
    {
        [Theory]
        [TestCase(0.0000000d, 0.0000000d, 0.0000000d, 0.0d, 0.0d, 0.0d)]
        [TestCase(1.7320508d, 0.7853982d, 0.9553166d, 1.0d, 1.0d, 1.0d)]
        [TestCase(1.0000000d, 0.0000000d, 1.5707963d, 1.0d, 0.0d, 0.0d)]
        [TestCase(1.4142136d, 0.7853982d, 1.5707963d, 1.0d, 1.0d, 0.0d)]
        [TestCase(1.4142136d, 0.0000000d, 0.7853982d, 1.0d, 0.0d, 1.0d)]
        [TestCase(1.4142136d, 1.5707963d, 0.7853982d, 0.0d, 1.0d, 1.0d)]
        [TestCase(1.0000000d, 0.0000000d, 0.0000000d, 0.0d, 0.0d, 1.0d)]
        [TestCase(1.7320508d, -2.3561945d, 2.1862760d, -1.0d, -1.0d, -1.0d)]
        [TestCase(1.0000000d, 3.1415926d, 1.5707963d, -1.0d, 0.0d, 0.0d)]
        [TestCase(1.4142136d, -2.3561945d, 1.5707963d, -1.0d, -1.0d, 0.0d)]
        [TestCase(1.4142136d, 3.1415927d, 2.3561945d, -1.0d, 0.0d, -1.0d)]
        [TestCase(1.4142136d, -1.5707963d, 2.3561945d, 0.0d, -1.0d, -1.0d)]
        [TestCase(1.0000000d, 0.0000000d, 3.1415926d, 0.0d, 0.0d, -1.0d)]
        public void Calculate_SetsVectorInCartesian_ForGivenValues(
            double radius,
            double phiInRadians,
            double polarThetaInRadians,
            double expectedX,
            double expectedY,
            double expectedZ)
        {
            // Arrange
            var sut = new SphericalToCartesianCalculator();

            var expected = new CartesianCoordinates
                           {
                               X = expectedX,
                               Y = expectedY,
                               Z = expectedZ
                           };

            var sphericalCoordinates = new SphericalCoordinates
                                       {
                                           Radius = radius,
                                           Phi = Angle.FromRadians(phiInRadians),
                                           Theta = Angle.FromRadians(polarThetaInRadians)
                                       };

            sut.SphericalCoordinates = sphericalCoordinates;

            // Act
            sut.Calculate();

            // Assert
            CartesianCoordinatesHelper.AssertCartesianCoordinates(expected,
                                                                  sut.CartesianCoordinates);
        }
    }
}