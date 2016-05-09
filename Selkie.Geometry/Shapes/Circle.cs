using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes.Calculators;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public class Circle
        : ICircle,
          IEquatable <Circle>
    {
        private Circle()
        {
            IsUnknown = true;
            CentrePoint = Point.Unknown;
            m_Calculator = new CircleCentreToPointCalculator(CentrePoint);
        }

        public Circle(double x,
                      double y,
                      double radius)
            : this(new Point(x,
                             y),
                   radius)
        {
        }

        public Circle([NotNull] Point centrePoint,
                      double radius)
        {
            CentrePoint = centrePoint;
            Radius = radius;
            m_Calculator = new CircleCentreToPointCalculator(CentrePoint);
        }

        public static readonly ICircle Unknown = new Circle();
        private readonly ICircleCentreToPointCalculator m_Calculator;

        #region IEquatable<Circle> Members

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(Circle other)
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
            return other.CentrePoint.Equals(CentrePoint) && other.Radius.Equals(Radius);
        }

        #endregion

        public static bool operator ==(Circle left,
                                       Circle right)
        {
            return Equals(left,
                          right);
        }

        public static bool operator !=(Circle left,
                                       Circle right)
        {
            return !Equals(left,
                           right);
        }

        [NotNull]
        public Angle AngleBetweenPointsClockwise([NotNull] Point startPoint,
                                                 [NotNull] Point endPoint)
        {
            var calculator = new CircleCentrePointToPointCalculator(CentrePoint,
                                                                    startPoint,
                                                                    endPoint);

            return calculator.AngleRelativeToYAxisClockwise;
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
            if ( obj.GetType() != typeof ( Circle ) )
            {
                return false;
            }
            return Equals(( Circle ) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ( CentrePoint.GetHashCode() * 397 ) ^ Radius.GetHashCode();
            }
        }

        private static bool IsInsideEpsilonForPoints(double deltaY,
                                                     double deltaX)
        {
            return Math.Abs(deltaX) < SelkieConstants.EpsilonPointXy ||
                   Math.Abs(deltaY) < SelkieConstants.EpsilonPointXy;
        }

        #region ICircle Members

        public bool IsUnknown { get; }

        public Point PointOnCircle(Angle angle)
        {
            double r = Radius;

            double x = r * Math.Cos(angle.Radians);
            double y = r * Math.Sin(angle.Radians);

            x += CentrePoint.X;
            y += CentrePoint.Y;

            var point = new Point(x,
                                  y);

            return point;
        }

        public bool IsPointOnCircle(Point startPoint)
        {
            double distance = CentrePoint.DistanceTo(startPoint);

            return Math.Abs(Radius - distance) <= SelkieConstants.EpsilonDistance;
        }

        public bool Intersects(ICircle finishPointStarboard)
        {
            double distance = Distance(finishPointStarboard);
            double delta = distance - Radius;

            return delta < 0 || Math.Abs(delta) <= SelkieConstants.EpsilonDistance;
        }

        public Point CentrePoint { get; }

        public double X => CentrePoint.X;

        public double Y => CentrePoint.Y;

        public double Radius { get; }

        public double Distance(ICircle other)
        {
            var line = new Line(CentrePoint,
                                other.CentrePoint);

            return line.Length;
        }

        public Angle RadiansRelativeToXAxisCounterClockwise(Point point)
        {
            m_Calculator.Calculate(point);

            return m_Calculator.AngleRelativeToXAxisCounterClockwise;
        }

        public Angle GetAngleRelativeToXAxis(Point point)
        {
            double deltaX = point.X - CentrePoint.X;
            double deltaY = point.Y - CentrePoint.Y;

            double radians;

            if ( IsInsideEpsilonForPoints(deltaY,
                                          deltaX) )
            {
                if ( Math.Abs(deltaX) < SelkieConstants.EpsilonRadians )
                {
                    radians = CentrePoint.Y < point.Y
                                  ? Angle.RadiansFor90Degrees
                                  : Angle.RadiansFor270Degrees;
                }
                else
                {
                    radians = CentrePoint.X < point.X
                                  ? Angle.RadiansForZeroDegrees
                                  : Angle.RadiansFor180Degrees;
                }
            }
            else
            {
                radians = Math.Atan2(deltaY,
                                     deltaX);
            }

            return Angle.FromRadians(radians);
        }

        #endregion
    }
}