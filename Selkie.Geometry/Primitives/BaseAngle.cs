using System;
using JetBrains.Annotations;
using Selkie.Windsor.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Primitives
{
    public abstract class BaseAngle : IEquatable <BaseAngle>
    {
        public const double RadiansFor360Degrees = 2.0 * Math.PI;
        public const double RadiansFor315Degrees = RadiansFor270Degrees + RadiansFor45Degrees;
        public const double RadiansFor270Degrees = RadiansFor360Degrees * 0.75;
        public const double RadiansFor225Degrees = RadiansFor180Degrees + RadiansFor45Degrees;
        public const double RadiansFor180Degrees = Math.PI;
        public const double RadiansFor135Degrees = RadiansFor90Degrees + RadiansFor45Degrees;
        public const double RadiansFor90Degrees = Math.PI / 2.0;
        public const double RadiansFor45Degrees = Math.PI / 4.0;
        public const double RadiansForZeroDegrees = 0.0;

        public double Radians { get; protected set; }
        public double Degrees { get; protected set; }

        public bool Equals(BaseAngle other)
        {
            if ( ReferenceEquals(null,
                                 other) )
            {
                return false;
            }
            if ( ReferenceEquals(this,
                                 other) )
            {
                return true;
            }

            double delta = Math.Abs(Radians - other.Radians);

            return delta < Constants.EpsilonRadians;
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            return Math.Abs(degrees - Constants.EpsilonDegrees) < Constants.EpsilonDegrees
                       ? 0.0
                       : degrees * Math.PI / 180.0;
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            return radians * 180.0 / Math.PI;
        }

        public static bool operator ==([NotNull] BaseAngle one,
                                       [NotNull] BaseAngle two)
        {
            return Math.Abs(one.Radians - two.Radians) < SelkieConstants.EpsilonRadians;
        }

        public static bool operator >(BaseAngle one,
                                      BaseAngle two)
        {
            double radians = one.Radians - two.Radians;

            return radians > 0.0;
        }

        public static bool operator >=(BaseAngle one,
                                       BaseAngle two)
        {
            double radians = one.Radians - two.Radians;

            return radians >= 0.0;
        }

        public static bool operator !=([NotNull] BaseAngle one,
                                       [NotNull] BaseAngle two)
        {
            return Math.Abs(one.Radians - two.Radians) > SelkieConstants.EpsilonRadians;
        }

        public static bool operator <(BaseAngle one,
                                      BaseAngle two)
        {
            double radians = one.Radians - two.Radians;

            return radians < 0.0;
        }

        public static bool operator <=(BaseAngle one,
                                       BaseAngle two)
        {
            double radians = one.Radians - two.Radians;

            return radians <= 0.0; // todo Math.Abs and epsilon, generic for all operations 
        }

        public override bool Equals(object obj)
        {
            if ( ReferenceEquals(null,
                                 obj) )
            {
                return false;
            }
            if ( ReferenceEquals(this,
                                 obj) )
            {
                return true;
            }
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if ( obj.GetType() != GetType() )
            {
                return false;
            }
            return Equals(( BaseAngle ) obj);
        }

        public override int GetHashCode()
        {
            return Radians.GetHashCode();
        }

        public override string ToString()
        {
            return "[Radians: {0:F6} Degrees: {1:F6}]".Inject(Radians,
                                                              Degrees);
        }
    }
}