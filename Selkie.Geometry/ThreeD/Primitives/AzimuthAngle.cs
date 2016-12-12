using System.Diagnostics;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.ThreeD.Primitives
{
    [DebuggerDisplay("Radians = {Radians}, Degrees = {Degrees}")]
    public class AzimuthAngle
        : BaseAngle
    {
        // => Azimuth => Theta [0° ≤ φ < 360° (2π rad)]
        private AzimuthAngle(double radians)
        {
            Radians = radians;
            Degrees = ConvertRadiansToDegrees(radians);
        }

        [NotNull]
        public static AzimuthAngle FromDegrees(double degrees)
        {
            double radians = ConvertDegreesToRadians(degrees);

            return CreateFromRadians(radians);
        }

        [NotNull]
        public static AzimuthAngle FromRadians(double radians)
        {
            return CreateFromRadians(radians);
        }

        public static AzimuthAngle operator +(AzimuthAngle one,
                                              AzimuthAngle two)
        {
            return CreateFromRadians(one.Radians + two.Radians);
        }

        public static AzimuthAngle operator -(AzimuthAngle one,
                                              AzimuthAngle two)
        {
            return CreateFromRadians(one.Radians - two.Radians);
        }

        private static AzimuthAngle CreateFromRadians(double radians)
        {
            if ( radians < 0.0 )
            {
                radians += RadiansFor360Degrees;
            }

            double remainder = radians % RadiansFor360Degrees;

            return new AzimuthAngle(remainder);
        }
    }
}