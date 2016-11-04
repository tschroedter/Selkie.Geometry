using System;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.Windsor;

namespace Selkie.Geometry.ThreeD.Calculators
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SphericalToCartesianCalculator
        : ISphericalToCartesianCalculator
    {
        public CartesianCoordinates CartesianCoordinates { get; private set; }
        public SphericalCoordinates SphericalCoordinates { get; set; }

        public void Calculate()
        {
            double radius = SphericalCoordinates.Radius;
            double phiRadians = SphericalCoordinates.Phi.Radians;
            double thetaRadians = SphericalCoordinates.Theta.Radians;

            double x = radius * Math.Cos(phiRadians) * Math.Sin(thetaRadians);
            double y = radius * Math.Sin(phiRadians) * Math.Sin(thetaRadians);
            double z = radius * Math.Cos(thetaRadians);

            CartesianCoordinates = new CartesianCoordinates
                                   {
                                       X = x,
                                       Y = y,
                                       Z = z
                                   };
        }
    }
}