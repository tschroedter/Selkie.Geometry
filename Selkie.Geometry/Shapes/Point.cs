using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Windsor.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public class Point : IEquatable <Point>
    {
        public static readonly Point Unknown = new Point();
        private readonly bool m_IsUnknown;
        private readonly double m_X;
        private readonly double m_Y;

        private Point()
        {
            m_X = double.NaN;
            m_Y = double.NaN;
            m_IsUnknown = true;
        }

        public Point(double x,
                     double y)
        {
            m_X = x;
            m_Y = y;
            m_IsUnknown = false;
        }

        public bool IsUnknown
        {
            get
            {
                return m_IsUnknown;
            }
        }

        public double X
        {
            get
            {
                return m_X;
            }
        }

        public double Y
        {
            get
            {
                return m_Y;
            }
        }

        #region IEquatable<Point> Members

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(Point other)
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

            return Math.Abs(m_X - other.X) < SelkieConstants.EpsilonPointXy &&
                   Math.Abs(m_Y - other.Y) < SelkieConstants.EpsilonPointXy;
        }

        #endregion

        [NotNull]
        public Point RelativeTo([NotNull] Point other)
        {
            double x = m_X - other.X;
            double y = m_Y - other.Y;

            return new Point(x,
                             y);
        }

        [NotNull]
        public Angle AngleBetweenPoints([NotNull] Point pointOne,
                                        [NotNull] Point pointTwo)
        {
            Point relativeOne = pointOne.RelativeTo(this);
            Point relativeTwo = pointTwo.RelativeTo(this);

            double a1 = relativeOne.X;
            double a2 = relativeOne.Y;
            double b1 = relativeTwo.X;
            double b2 = relativeTwo.Y;

            double d1 = Math.Sqrt(a1 * a1 + a2 * a2);
            double d2 = Math.Sqrt(b1 * b1 + b2 * b2);

            double scalarProduct = a1 * b1 + a2 * b2;
            double distances = d1 * d2;

            double radians = Math.Acos(scalarProduct / distances);

            return Angle.FromRadians(radians);
        }

        [NotNull]
        public Point Move(double distance,
                          double radians)
        {
            double x = m_X;
            double y = m_Y;
            double r = distance;

            double newX = x + r * Math.Sin(radians);
            double newY = y + r * Math.Cos(radians);

            var point = new Point(newX,
                                  newY);

            return point;
        }

        public double DistanceTo([NotNull] Point other)
        {
            double x = ( other.X - m_X ) * ( other.X - m_X );
            double y = ( other.Y - m_Y ) * ( other.Y - m_Y );
            double distance = Math.Sqrt(x + y);

            return distance;
        }

        public override string ToString()
        {
            return "[{0},{1}]".Inject(m_X,
                                      m_Y);
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
            if ( obj.GetType() != typeof ( Point ) )
            {
                return false;
            }
            return Equals(( Point ) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = m_X.GetHashCode();
                result = ( result * 397 ) ^ m_Y.GetHashCode();
                result = ( result * 397 ) ^ m_IsUnknown.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(Point left,
                                       Point right)
        {
            return Equals(left,
                          right);
        }

        public static bool operator !=(Point left,
                                       Point right)
        {
            return !Equals(left,
                           right);
        }
    }
}