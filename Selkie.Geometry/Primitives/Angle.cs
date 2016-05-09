using System;
using JetBrains.Annotations;
using Selkie.Windsor.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Primitives
{
    public sealed class Angle : IEquatable <Angle>
    {
        private Angle(double radians)
        {
            Radians = NormalizeRadians(radians);
            Degrees = radians * 180.0 / Math.PI;
        }

        private const double TwoPi = 2.0 * Math.PI;
        private const double TwoPiMinusEpsilonRadians = TwoPi - EpsilonRadians;

        public const double RadiansFor360Degrees = 2.0 * Math.PI;
        public const double RadiansFor315Degrees = RadiansFor270Degrees + RadiansFor45Degrees;
        public const double RadiansFor270Degrees = RadiansFor360Degrees * 0.75;
        public const double RadiansFor225Degrees = RadiansFor180Degrees + RadiansFor45Degrees;
        public const double RadiansFor180Degrees = Math.PI;
        public const double RadiansFor135Degrees = RadiansFor90Degrees + RadiansFor45Degrees;
        public const double RadiansFor90Degrees = Math.PI / 2.0;
        public const double RadiansFor45Degrees = Math.PI / 4.0;
        public const double RadiansForZeroDegrees = 0.0;
        internal const double EpsilonRadians = SelkieConstants.EpsilonRadians;
        internal const double EpsilonDegrees = SelkieConstants.EpsilonDegrees;
        public static Angle Unknown = new Angle(double.NegativeInfinity);

        public double Radians { get; }

        public double Degrees { get; }

        public static double ConvertDegreesToRadians(double degrees)
        {
            if ( Math.Abs(degrees - 0.0) < EpsilonDegrees )
            {
                return 0.0;
            }

            return NormalizeRadians(degrees * Math.PI / 180.0);
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            radians = NormalizeRadians(radians);

            return radians * 180.0 / Math.PI;
        }

        [NotNull]
        public static Angle FromDegrees(double degrees)
        {
            double radians = ConvertDegreesToRadians(degrees);

            return new Angle(radians);
        }

        [NotNull]
        public static Angle FromRadians(double radians)
        {
            radians = NormalizeRadians(radians);

            return new Angle(radians);
        }

        [NotNull]
        public static Angle Inverse([NotNull] Angle angle)
        {
            double radiansCounterclockwise = angle.Radians;

            double radians = RadiansFor360Degrees - radiansCounterclockwise;

            if ( Math.Abs(radians - RadiansFor360Degrees) < 0.01 )
            {
                radians = 0.0;
            }

            return FromRadians(radians);
        }

        public static double NormalizeRadians(double radians)
        {
            double normalized = radians;

            if ( radians < 0.0 )
            {
                normalized = RadiansFor360Degrees + radians;
            }
            else if ( radians > RadiansFor360Degrees )
            {
                normalized = radians - RadiansFor360Degrees;
            }

            if ( normalized >= 0.0 &&
                 normalized < EpsilonRadians )
            {
                normalized = RadiansForZeroDegrees;
            }
            else if ( Math.Abs(RadiansFor360Degrees - normalized) < EpsilonRadians )
            {
                normalized = RadiansFor360Degrees;
            }

            return normalized;
        }

        public static Angle operator +(Angle one,
                                       Angle two)
        {
            double radians = NormalizeRadians(one.Radians + two.Radians);

            return FromRadians(radians);
        }

        public static bool operator ==(Angle left,
                                       Angle right)
        {
            return Equals(left,
                          right);
        }

        public static bool operator >(Angle one,
                                      Angle two)
        {
            double radians = one.Radians - two.Radians;

            return radians > 0.0;
        }

        public static bool operator >=(Angle one,
                                       Angle two)
        {
            double radians = one.Radians - two.Radians;

            if ( radians >= 0.0 )
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Angle left,
                                       Angle right)
        {
            return !Equals(left,
                           right);
        }

        public static bool operator <(Angle one,
                                      Angle two)
        {
            double radians = one.Radians - two.Radians;

            return radians < 0.0;
        }

        public static bool operator <=(Angle one,
                                       Angle two)
        {
            double radians = one.Radians - two.Radians;

            if ( radians <= 0.0 )
            {
                return true;
            }

            return false;
        }

        public static Angle operator -(Angle one,
                                       Angle two)
        {
            double radians = NormalizeRadians(one.Radians - two.Radians);

            return FromRadians(radians);
        }

        [NotNull]
        public static Angle RelativeToXAxisCountertclockwise([NotNull] Angle angle)
        {
            double radiansYAxisClockwise = angle.Radians;

            if ( Math.Abs(RadiansFor90Degrees - radiansYAxisClockwise) <= 0.01 )
            {
                return ForZeroDegrees;
            }

            double radiansYAxis = DetermineRadiansYAxisCountertclockwise(radiansYAxisClockwise);

            return FromRadians(radiansYAxis);
        }

        [NotNull]
        public static Angle RelativeToYAxisCounterclockwise([NotNull] Angle angle)
        {
            double radiansXAxis = angle.Radians;

            if ( Math.Abs(radiansXAxis) < SelkieConstants.EpsilonRadians ||
                 Math.Abs(RadiansFor360Degrees - radiansXAxis) < SelkieConstants.EpsilonRadians )
            {
                return FromRadians(RadiansFor90Degrees);
            }

            double radiansYAxis = DetermineRadiansYAxisClockwise(radiansXAxis);

            return FromRadians(radiansYAxis);
        }

        // ReSharper disable once CodeAnnotationAnalyzer
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
            if ( obj.GetType() != typeof ( Angle ) )
            {
                return false;
            }
            return Equals(( Angle ) obj);
        }

        public override int GetHashCode()
        {
            return Radians.GetHashCode();
        }

        public override string ToString()
        {
            return "[Radians: {0:F2} Degrees: {1:F2}]".Inject(Radians,
                                                              Degrees);
        }

        private static double DetermineRadiansYAxisClockwise(double radiansXAxis)
        {
            double radiansYAxis;

            if ( radiansXAxis <= RadiansFor90Degrees )
            {
                radiansYAxis = RadiansFor90Degrees - radiansXAxis;
            }
            else if ( radiansXAxis <= RadiansFor180Degrees )
            {
                radiansYAxis = RadiansFor270Degrees + RadiansFor180Degrees - radiansXAxis;
            }
            else if ( radiansXAxis <= RadiansFor270Degrees )
            {
                radiansYAxis = RadiansFor180Degrees + RadiansFor270Degrees - radiansXAxis;
            }
            else
            {
                radiansYAxis = RadiansFor90Degrees + RadiansFor360Degrees - radiansXAxis;
            }
            return radiansYAxis;
        }

        private static double DetermineRadiansYAxisCountertclockwise(double radiansYAxisClockwise)
        {
            double radiansYAxis;

            if ( radiansYAxisClockwise <= RadiansFor90Degrees )
            {
                radiansYAxis = RadiansFor90Degrees - radiansYAxisClockwise;
            }
            else if ( radiansYAxisClockwise <= RadiansFor180Degrees )
            {
                radiansYAxis = RadiansFor270Degrees + RadiansFor180Degrees - radiansYAxisClockwise;
            }
            else if ( radiansYAxisClockwise <= RadiansFor270Degrees )
            {
                radiansYAxis = RadiansFor180Degrees + RadiansFor270Degrees - radiansYAxisClockwise;
            }
            else
            {
                radiansYAxis = RadiansFor90Degrees + RadiansFor360Degrees - radiansYAxisClockwise;
            }
            return radiansYAxis;
        }

        #region IEquatable<Angle> Members

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(Angle other)
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

            if ( IsEqualForZeroAnd2Pi(other) )
            {
                return true;
            }

            return Math.Abs(other.Radians - Radians) < EpsilonRadians;
        }

        private bool IsEqualForZeroAnd2Pi(Angle other)
        {
            if ( other.Radians < EpsilonRadians &&
                 Radians > TwoPiMinusEpsilonRadians )
            {
                return true;
            }

            if ( other.Radians > TwoPiMinusEpsilonRadians &&
                 Radians < EpsilonRadians )
            {
                return true;
            }
            return false;
        }

        #endregion

        // ReSharper disable InconsistentNaming
        // ReSharper restore InconsistentNaming
        // ReSharper disable InconsistentNaming 
        public static Angle For360Degrees = FromRadians(RadiansFor360Degrees);
        public static Angle For315Degrees = FromRadians(RadiansFor270Degrees + RadiansFor45Degrees);
        public static Angle For270Degrees = FromRadians(RadiansFor360Degrees * 0.75);
        public static Angle For225Degrees = FromRadians(RadiansFor180Degrees + RadiansFor45Degrees);
        public static Angle For180Degrees = FromRadians(Math.PI);
        public static Angle For135Degrees = FromRadians(RadiansFor90Degrees + RadiansFor45Degrees);
        public static Angle For90Degrees = FromRadians(Math.PI / 2.0);
        public static Angle For45Degrees = FromRadians(Math.PI / 4.0);
        public static Angle ForZeroDegrees = FromRadians(0.0);
        // ReSharper restore InconsistentNaming
    }
}