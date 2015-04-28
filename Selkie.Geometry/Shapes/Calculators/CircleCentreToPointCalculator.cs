using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes.Calculators
{
    public class CircleCentreToPointCalculator : ICircleCentreToPointCalculator
    {
        private readonly Point m_CentrePoint;
        private Angle m_AngleRelativeToXAxisCounterClockwise = Angle.Unknown;
        private Angle m_AngleRelativeToYAxisClockwise = Angle.Unknown;
        private Angle m_AngleRelativeToYAxisCounterclockwise = Angle.Unknown;
        private Point m_Point = Point.Unknown;

        public CircleCentreToPointCalculator([NotNull] Point centrePoint,
                                             [NotNull] Point point)
        {
            m_CentrePoint = centrePoint;
            Point = point;

            Calculate(Point);
        }

        public CircleCentreToPointCalculator([NotNull] Point centrePoint)
        {
            m_CentrePoint = centrePoint;
            Point = Point.Unknown;
        }

        #region ICircleCentreToPointCalculator Members

        public Point CentrePoint
        {
            get
            {
                return m_CentrePoint;
            }
        }

        public Point Point
        {
            get
            {
                return m_Point;
            }
            private set
            {
                m_Point = value;
            }
        }

        public void Calculate(Point point)
        {
            Point = point;

            if ( m_CentrePoint.IsUnknown ||
                 point.IsUnknown )
            {
                AngleRelativeToXAxisCounterClockwise = Angle.ForZeroDegrees;
                AngleRelativeToYAxisClockwise = Angle.ForZeroDegrees;
                AngleRelativeToYAxisCounterclockwise = Angle.ForZeroDegrees;

                return;
            }

            // Note: maybe it's enough to calculate m_AngleRelativeToXAxisCounterClockwise
            AngleRelativeToXAxisCounterClockwise = CalculateAngleRelativeToXAxisCounterClockwise(m_CentrePoint,
                                                                                                 point);
            AngleRelativeToYAxisClockwise = Angle.RelativeToYAxisClockwise(AngleRelativeToXAxisCounterClockwise);
            AngleRelativeToYAxisCounterclockwise = Angle.Inverse(AngleRelativeToYAxisClockwise);
        }

        public Angle AngleRelativeToXAxisCounterClockwise
        {
            get
            {
                return m_AngleRelativeToXAxisCounterClockwise;
            }
            private set
            {
                m_AngleRelativeToXAxisCounterClockwise = value;
            }
        }

        public Angle AngleRelativeToYAxisClockwise
        {
            get
            {
                return m_AngleRelativeToYAxisClockwise;
            }
            private set
            {
                m_AngleRelativeToYAxisClockwise = value;
            }
        }

        public Angle AngleRelativeToYAxisCounterclockwise
        {
            get
            {
                return m_AngleRelativeToYAxisCounterclockwise;
            }
            private set
            {
                m_AngleRelativeToYAxisCounterclockwise = value;
            }
        }

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