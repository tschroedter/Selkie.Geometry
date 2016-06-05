using System;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Calculators
{
    public class CirclesIntersectionPointsCalculator : ICirclesIntersectionPointsCalculator
    {
        public CirclesIntersectionPointsCalculator([NotNull] ICirclePair circlePair)
        {
            double d = circlePair.Distance;
            double r0PlusR1 = circlePair.Zero.Radius + circlePair.One.Radius;
            double r0MinusR1Abs = Math.Abs(circlePair.Zero.Radius - circlePair.One.Radius);

            if ( IsSameCircles(circlePair,
                               d,
                               r0MinusR1Abs) )
            {
                m_IsCirclesAreSame = true;
                m_HasIntersectionPoints = false;
            }
            else if ( r0PlusR1 >= d &&
                      d >= r0MinusR1Abs )
            {
                Tuple <Point, Point> points = CalculateIntersectionPoints(circlePair);

                m_IsCirclesTouchAtSinglePoint = DetermineNumberOfIntersectionPoints(points) == 1;

                if ( m_IsCirclesTouchAtSinglePoint )
                {
                    var line = new Line(circlePair.Zero.CentrePoint,
                                        circlePair.One.CentrePoint);
                    Point point = circlePair.Zero.PointOnCircle(line.AngleToXAxis);

                    m_IntersectionPointOne = point;
                    m_IntersectionPointTwo = point;
                }
                else
                {
                    m_IntersectionPointOne = points.Item1;
                    m_IntersectionPointTwo = points.Item2;
                }

                m_HasIntersectionPoints = true;
            }
        }

        private readonly bool m_HasIntersectionPoints;
        private readonly Point m_IntersectionPointOne = Point.Unknown;
        private readonly Point m_IntersectionPointTwo = Point.Unknown;
        private readonly bool m_IsCirclesAreSame;
        private readonly bool m_IsCirclesTouchAtSinglePoint;

        [NotNull]
        internal Tuple <Point, Point> CalculatePointsForSameY([NotNull] ICirclePair circlePair)
        {
            Point one;
            Point two;

            if ( circlePair.Zero.X < circlePair.One.X )
            {
                one = new Point(circlePair.Zero.X + circlePair.Zero.Radius,
                                circlePair.Zero.Y);
                two = new Point(circlePair.One.X - circlePair.One.Radius,
                                circlePair.One.Y);
            }
            else
            {
                one = new Point(circlePair.Zero.X - circlePair.Zero.Radius,
                                circlePair.Zero.Y);
                two = new Point(circlePair.One.X + circlePair.One.Radius,
                                circlePair.One.Y);
            }

            return new Tuple <Point, Point>(one,
                                            two);
        }

        [NotNull]
        internal Tuple <Point, Point> CalculatePointsForSpecialCases([NotNull] ICirclePair circlePair)
        {
            double deltaX = circlePair.Zero.X - circlePair.One.X;
            double doubleRadius = circlePair.Zero.Radius * 2.0;

            if ( Math.Abs(Math.Abs(deltaX) - doubleRadius) < SelkieConstants.EpsilonDistance )
            {
                return CalculatePointsForSameY(circlePair);
            }

            double deltaY = circlePair.Zero.Y - circlePair.One.Y;

            if ( Math.Abs(Math.Abs(deltaY) - doubleRadius) < SelkieConstants.EpsilonDistance )
            {
                return CalculatePointsSameX(circlePair);
            }

            return new Tuple <Point, Point>(Point.Unknown,
                                            Point.Unknown);
        }

        [NotNull]
        internal Tuple <Point, Point> CalculatePointsSameX([NotNull] ICirclePair circlePair)
        {
            Point one;
            Point two;

            if ( circlePair.Zero.Y < circlePair.One.Y )
            {
                one = new Point(circlePair.Zero.X,
                                circlePair.Zero.Y + circlePair.Zero.Radius);
                two = new Point(circlePair.One.X,
                                circlePair.One.Y - circlePair.Zero.Radius);
            }
            else
            {
                one = new Point(circlePair.Zero.X,
                                circlePair.Zero.Y - circlePair.Zero.Radius);
                two = new Point(circlePair.One.X,
                                circlePair.One.Y + circlePair.Zero.Radius);
            }

            return new Tuple <Point, Point>(one,
                                            two);
        }

        [NotNull]
        // ReSharper disable once TooManyArguments
        private static Tuple <Point, Point> CalculatePointsForHBiggerThanEpsilon(double h,
                                                                                 double cy0,
                                                                                 double cy1,
                                                                                 double cx1,
                                                                                 double cx0,
                                                                                 double distance,
                                                                                 double a)
        {
            // Find P2.
            double cx2 = cx0 + a * ( cx1 - cx0 ) / distance;
            double cy2 = cy0 + a * ( cy1 - cy0 ) / distance;

            // Get the points P3.
            double ix1 = cx2 + h * ( cy1 - cy0 ) / distance;
            double iy1 = cy2 - h * ( cx1 - cx0 ) / distance;

            double ix2 = cx2 - h * ( cy2 - cy0 ) / distance;
            double iy2 = cy2 + h * ( cx2 - cx0 ) / distance;

            var one = new Point(ix1,
                                iy1);
            var two = new Point(ix2,
                                iy2);

            return new Tuple <Point, Point>(one,
                                            two);
        }

        private static bool IsSameCircles([NotNull] ICirclePair circlePair,
                                          double d,
                                          double r0MinusR1Abs)
        {
            return Math.Abs(d) < SelkieConstants.EpsilonDistance && r0MinusR1Abs < SelkieConstants.EpsilonDistance &&
                   Math.Abs(circlePair.Zero.X - circlePair.One.X) < SelkieConstants.EpsilonDistance &&
                   Math.Abs(circlePair.Zero.Y - circlePair.One.Y) < SelkieConstants.EpsilonDistance;
        }

        [NotNull]
        // ReSharper disable once MethodTooLong
        private Tuple <Point, Point> CalculateIntersectionPoints([NotNull] ICirclePair circlePair)
        {
            double r0 = circlePair.Zero.Radius;
            double r1 = circlePair.One.Radius;
            double distance = circlePair.Distance;

            // Find a and h
            double aTop = r0 * r0 - r1 * r1 + distance * distance;
            double aBottom = 2.0 * distance;
            double a = aTop / aBottom;
            double h = Math.Sqrt(r0 * r0 - a * a);

            double cx0 = circlePair.Zero.X;
            double cy0 = circlePair.Zero.Y;
            double cx1 = circlePair.One.X;
            double cy1 = circlePair.One.Y;

            if ( Math.Abs(h) > SelkieConstants.EpsilonRadians )
            {
                return CalculatePointsForHBiggerThanEpsilon(h,
                                                            cy0,
                                                            cy1,
                                                            cx1,
                                                            cx0,
                                                            distance,
                                                            a);
            }

            return CalculatePointsForSpecialCases(circlePair);
        }

        private int DetermineNumberOfIntersectionPoints([NotNull] Tuple <Point, Point> points)
        {
            if ( points.Item1 == Point.Unknown ||
                 points.Item2 == Point.Unknown )
            {
                return 1;
            }

            double deltaX = Math.Abs(points.Item1.X - points.Item2.X);
            double deltaY = Math.Abs(points.Item1.Y - points.Item2.Y);

            if ( deltaX < SelkieConstants.EpsilonDistance &&
                 deltaY < SelkieConstants.EpsilonDistance )
            {
                return 1;
            }

            return 2;
        }

        #region ICirclesIntersectionPointsCalculator Members

        public bool HasIntersectionPoints
        {
            get
            {
                return m_HasIntersectionPoints;
            }
        }

        public bool IsCirclesAreSame
        {
            get
            {
                return m_IsCirclesAreSame;
            }
        }

        public bool IsCirclesTouchAtSinglePoint
        {
            get
            {
                return m_IsCirclesTouchAtSinglePoint;
            }
        }

        public Point IntersectionPointOne
        {
            get
            {
                return m_IntersectionPointOne;
            }
        }

        public Point IntersectionPointTwo
        {
            get
            {
                return m_IntersectionPointTwo;
            }
        }

        #endregion
    }
}