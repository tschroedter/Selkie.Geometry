using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes.Calculators
{
    public class CircleCentreToPointCalculator : ICircleCentreToPointCalculator
    {
        public CircleCentreToPointCalculator([NotNull] Point centrePoint,
                                             [NotNull] Point point)
        {
            CentrePoint = centrePoint;
            Point = point;

            Calculate(Point);
        }

        public CircleCentreToPointCalculator([NotNull] Point centrePoint)
        {
            CentrePoint = centrePoint;
            Point = Point.Unknown;
        }

        #region ICircleCentreToPointCalculator Members

        public Point CentrePoint { get; }

        public Point Point { get; private set; }

        public void Calculate(Point point)
        {
            Point = point;

            if ( CentrePoint.IsUnknown ||
                 point.IsUnknown )
            {
                AngleRelativeToXAxisCounterClockwise = Angle.ForZeroDegrees;
                AngleRelativeToYAxisClockwise = Angle.ForZeroDegrees;
                AngleRelativeToYAxisCounterclockwise = Angle.ForZeroDegrees;

                return;
            }

            // Note: maybe it's enough to calculate m_AngleRelativeToXAxisCounterClockwise
            AngleRelativeToXAxisCounterClockwise = CalculateAngleRelativeToXAxisCounterClockwise(CentrePoint,
                                                                                                 point);
            AngleRelativeToYAxisCounterclockwise =
                Angle.RelativeToYAxisCounterclockwise(AngleRelativeToXAxisCounterClockwise);
            AngleRelativeToYAxisClockwise = Angle.Inverse(AngleRelativeToYAxisCounterclockwise);
        }

        public Angle AngleRelativeToXAxisCounterClockwise { get; private set; } = Angle.Unknown;

        public Angle AngleRelativeToYAxisClockwise // todo double check if this is really Clockwise
        { get; private set; } = Angle.Unknown;

        public Angle AngleRelativeToYAxisCounterclockwise // todo double check if this is really Counterclockwise
        { get; private set; } = Angle.Unknown;

        // todo double check if this is really Counterclockwise
        public Angle CalculateAngleRelativeToXAxisCounterClockwise(Point centre,
                                                                   Point point)
        {
            double deltaX = point.X - centre.X;
            double deltaY = point.Y - centre.Y;

            double radians;

            if ( IsDeltaXOrDeltaYLessThanEpsilon(deltaX,
                                                 deltaY) )
            {
                radians = DetermineRadiansForDeltaXOrDeltaYLessThanEpsilon(centre,
                                                                           point,
                                                                           deltaX,
                                                                           deltaY);
            }
            else
            {
                radians = Math.Atan2(deltaY,
                                     deltaX);

                if ( radians < 0.0 )
                {
                    radians = Angle.RadiansFor360Degrees + radians;
                }
            }

            return Angle.FromRadians(radians);
        }

        // ReSharper disable once TooManyArguments
        private static double DetermineRadiansForDeltaXOrDeltaYLessThanEpsilon([NotNull] Point centre,
                                                                               [NotNull] Point point,
                                                                               double deltaX,
                                                                               double deltaY)
        {
            double radians;
            if ( IsDeltaXAndDeltaYLessThanEpsilon(deltaX,
                                                  deltaY) )
            {
                radians = Angle.RadiansForZeroDegrees;
            }
            else if ( Math.Abs(deltaX) < SelkieConstants.EpsilonDistance )
            {
                radians = centre.Y < point.Y
                              ? Angle.RadiansFor90Degrees
                              : Angle.RadiansFor270Degrees;
            }
            else
            {
                radians = centre.X < point.X
                              ? Angle.RadiansForZeroDegrees
                              : Angle.RadiansFor180Degrees;
            }
            return radians;
        }

        private static bool IsDeltaXAndDeltaYLessThanEpsilon(double deltaX,
                                                             double deltaY)
        {
            return Math.Abs(deltaX) < SelkieConstants.EpsilonDistance &&
                   Math.Abs(deltaY) < SelkieConstants.EpsilonDistance;
        }

        private static bool IsDeltaXOrDeltaYLessThanEpsilon(double deltaX,
                                                            double deltaY)
        {
            return Math.Abs(deltaX) < SelkieConstants.EpsilonDistance ||
                   Math.Abs(deltaY) < SelkieConstants.EpsilonDistance;
        }

        #endregion
    }
}