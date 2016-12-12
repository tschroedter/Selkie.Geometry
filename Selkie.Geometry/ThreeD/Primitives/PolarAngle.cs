using System.Diagnostics;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.ThreeD.Primitives
{
    [DebuggerDisplay("Radians = {Radians}, Degrees = {Degrees}")]
    public class PolarAngle
        : BaseAngle
    {
        // => PolarAngle => Phi [0° ≤ θ ≤ 180° (π rad)]
        private PolarAngle(double radians)
        {
            Radians = radians;
            Degrees = ConvertRadiansToDegrees(radians);
        }

        [NotNull]
        public static PolarAngle FromDegrees(double degrees)
        {
            double radians = ConvertDegreesToRadians(degrees);

            return CreateFromRadians(radians);
        }

        [NotNull]
        public static PolarAngle FromRadians(double radians)
        {
            return CreateFromRadians(radians);
        }

        public static PolarAngle operator +(PolarAngle one,
                                            PolarAngle two)
        {
            return CreateFromRadians(one.Radians + two.Radians);
        }

        public static PolarAngle operator -(PolarAngle one,
                                            PolarAngle two)
        {
            return CreateFromRadians(one.Radians - two.Radians);
        }

        private static PolarAngle CreateFromRadians(double radians)
        {
            if ( radians < 0.0 )
            {
                radians += RadiansFor180Degrees;
            }

            double remainder = radians % RadiansFor180Degrees;

            return new PolarAngle(remainder);
        }
    }
}