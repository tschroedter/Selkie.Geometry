using System;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.Windsor;

namespace Selkie.Geometry.ThreeD.Calculators
{
    [ProjectComponent(Lifestyle.Transient)]
    public class CartesianToSphericalCalculator
        : ICartesianToSphericalCalculator
    {
        public CartesianCoordinates CartesianCoordinates { get; set; }
        public SphericalCoordinates SphericalCoordinates { get; private set; }

        public void Calculate()
        {
            double x = CartesianCoordinates.X;
            double y = CartesianCoordinates.Y;
            double z = CartesianCoordinates.Z;

            double radius = Math.Sqrt(x * x + y * y + z * z);
            double phiInRadians = Math.Atan2(y,
                                             x);
            double thetaInRadians = Math.Atan2(Math.Sqrt(x * x + y * y),
                                               z);

            Angle theta = Angle.FromRadians(thetaInRadians);
            Angle phi = Angle.FromRadians(phiInRadians);

            SphericalCoordinates = new SphericalCoordinates
                                   {
                                       Radius = radius,
                                       Phi = phi,
                                       Theta = theta
                                   };
        }
    }
}