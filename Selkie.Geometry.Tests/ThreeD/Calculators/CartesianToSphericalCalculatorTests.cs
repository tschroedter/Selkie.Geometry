using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Calculators;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.ThreeD.Calculators
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class CartesianToSphericalCalculatorTests
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
            double expectedRadius,
            double expectedPhiInRadians,
            double expectedThetaInRadians,
            double x,
            double y,
            double z)
        {
            // Arrange
            Angle expectedPhi = Angle.FromRadians(expectedPhiInRadians);
            Angle expectedTheta = Angle.FromRadians(expectedThetaInRadians);

            var sut = new CartesianToSphericalCalculator();

            var cartesianCoordinates = new CartesianCoordinates
                                       {
                                           X = x,
                                           Y = y,
                                           Z = z
                                       };

            sut.CartesianCoordinates = cartesianCoordinates;

            // Act
            sut.Calculate();

            // Assert
            SphericalCoordinates actual = sut.SphericalCoordinates;

            NUnitHelper.AssertIsEquivalent(expectedRadius,
                                           actual.Radius,
                                           "Radius");

            NUnitHelper.AssertIsEquivalent(expectedPhi.Radians,
                                           actual.Phi.Radians,
                                           "Phi");
            NUnitHelper.AssertIsEquivalent(expectedTheta.Radians,
                                           actual.Theta.Radians,
                                           "Theta");
        }
    }
}