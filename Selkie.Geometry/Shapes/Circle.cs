using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes.Calculators;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public class Circle : ICircle,
                          IEquatable <Circle>
    {
        public static readonly ICircle Unknown = new Circle();
        private readonly ICircleCentreToPointCalculator m_Calculator;
        private readonly Point m_CentrePoint;
        private readonly bool m_IsUnknown;
        private readonly double m_Radius;

        private Circle()
        {
            m_IsUnknown = true;
            m_CentrePoint = Point.Unknown;
            m_Calculator = new CircleCentreToPointCalculator(m_CentrePoint);
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
            m_CentrePoint = centrePoint;
            m_Radius = radius;
            m_Calculator = new CircleCentreToPointCalculator(m_CentrePoint);
        }

        #region ICircle Members

        public bool IsUnknown
        {
            get
            {
                return m_IsUnknown;
            }
        }

        public Point PointOnCircle(Angle angle)
        {
            double r = m_Radius;

            double x = r * Math.Cos(angle.Radians);
            double y = r * Math.Sin(angle.Radians);

            x += m_CentrePoint.X;
            y += m_CentrePoint.Y;

            Point point = new Point(x,
                                    y);

            return point;
        }

        public bool IsPointOnCircle(Point startPoint)
        {
            double distance = m_CentrePoint.DistanceTo(startPoint);

            return Math.Abs(m_Radius - distance) <= SelkieConstants.EpsilonDistance;
        }

        public bool Intersects(ICircle finishPointStarPort)
        {
            double distance = Distance(finishPointStarPort);
            double delta = distance - m_Radius;

            return delta < 0 || Math.Abs(delta) <= SelkieConstants.EpsilonDistance;
        }

        public Point CentrePoint
        {
            get
            {
                return m_CentrePoint;
            }
        }

        public double X
        {
            get
            {
                return m_CentrePoint.X;
            }
        }

        public double Y
        {
            get
            {
                return m_CentrePoint.Y;
            }
        }

        public double Radius
        {
            get
            {
                return m_Radius;
            }
        }

        public double Distance(ICircle other)
        {
            Line line = new Line(m_CentrePoint,
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
            double deltaX = point.X - m_CentrePoint.X;
            double deltaY = point.Y - m_CentrePoint.Y;

            double radians;

            if ( IsInsideEpsilonForPoints(deltaY,
                                          deltaX) )
            {
                if ( Math.Abs(deltaX) < SelkieConstants.EpsilonRadians )
                {
                    radians = m_CentrePoint.Y < point.Y
                                  ? Angle.RadiansFor90Degrees
                                  : Angle.RadiansFor270Degrees;
                }
                else
                {
                    radians = m_CentrePoint.X < point.X
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
            return other.CentrePoint.Equals(m_CentrePoint) && other.Radius.Equals(m_Radius);
        }

        #endregion

        [NotNull]
        public Angle AngleBetweenPointsClockwise([NotNull] Point startPoint,
                                                 [NotNull] Point endPoint)
        {
            CircleCentrePointToPointCalculator calculator = new CircleCentrePointToPointCalculator(m_CentrePoint,
                                                                                                   startPoint,
                                                                                                   endPoint);

            return calculator.AngleClockwise;
        }

        private static bool IsInsideEpsilonForPoints(double deltaY,
                                                     double deltaX)
        {
            return Math.Abs(deltaX) < SelkieConstants.EpsilonPointXy || Math.Abs(deltaY) < SelkieConstants.EpsilonPointXy;
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
            return Equals((Circle) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ( ( m_CentrePoint != null
                               ? m_CentrePoint.GetHashCode()
                               : 0 ) * 397 ) ^ m_Radius.GetHashCode();
            }
        }

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
    }
}