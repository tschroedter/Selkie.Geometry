using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public class OuterTangentsCalculator : IOuterTangentsCalculator
    {
        private readonly Point m_CircleOneTangentPointOne;
        private readonly Point m_CircleOneTangentPointTwo;
        private readonly Point m_CircleZeroTangentPointOne;
        private readonly Point m_CircleZeroTangentPointTwo;
        private readonly Point m_IntersectionPoint;

        public OuterTangentsCalculator([NotNull] ICirclePair circlePair)
        {
            ICircle circleZero = circlePair.Zero;
            ICircle circleOne = circlePair.One;

            m_IntersectionPoint = CalculateOuterTangentLinesIntersectionPoint(circlePair);

            Tuple <Point, Point> pairZero;
            Tuple <Point, Point> pairOne;

            if ( m_IntersectionPoint.IsUnknown ||
                 double.IsInfinity(m_IntersectionPoint.X) ||
                 double.IsInfinity(m_IntersectionPoint.Y) )
            {
                if ( circlePair.NumberOfTangents > 1 )
                {
                    pairZero = CalculateTangenPointsForZeroBothSameRadius(circlePair);
                    pairOne = CalculateTangenPointsForOneBothSameRadius(circlePair);
                }
                else
                {
                    pairZero = new Tuple <Point, Point>(Point.Unknown,
                                                        Point.Unknown);
                    pairOne = new Tuple <Point, Point>(Point.Unknown,
                                                       Point.Unknown);
                }
            }
            else
            {
                pairZero = CalculateTangentPointsForCircle(circleZero);
                pairOne = CalculateTangentPointsForCircle(circleOne);
            }

            m_CircleZeroTangentPointOne = pairZero.Item1;
            m_CircleZeroTangentPointTwo = pairZero.Item2;

            m_CircleOneTangentPointOne = pairOne.Item1;
            m_CircleOneTangentPointTwo = pairOne.Item2;
        }

        [NotNull]
        internal Tuple <Point, Point> CalculateTangenPointsForZeroBothSameRadius([NotNull] ICirclePair circlePair)
        {
            var line = new Line(circlePair.Zero.CentrePoint,
                                circlePair.One.CentrePoint);

            Angle angle1 = line.AngleToXAxis - Angle.For90Degrees;
            Angle angle2 = line.AngleToXAxis + Angle.For90Degrees;

            Point point1 = circlePair.Zero.PointOnCircle(angle1);
            Point point2 = circlePair.Zero.PointOnCircle(angle2);

            return new Tuple <Point, Point>(point1,
                                            point2);
        }

        [NotNull]
        internal Tuple <Point, Point> CalculateTangenPointsForOneBothSameRadius([NotNull] ICirclePair circlePair)
        {
            var line = new Line(circlePair.Zero.CentrePoint,
                                circlePair.One.CentrePoint);

            Angle angle1 = line.AngleToXAxis - Angle.For90Degrees;
            Angle angle2 = line.AngleToXAxis + Angle.For90Degrees;

            Point point1 = circlePair.One.PointOnCircle(angle1);
            Point point2 = circlePair.One.PointOnCircle(angle2);

            return new Tuple <Point, Point>(point1,
                                            point2);
        }

        [NotNull]
        private Point CalculateOuterTangentLinesIntersectionPoint([NotNull] ICirclePair circlePair)
        {
            if ( circlePair.NumberOfTangents < 2 )
            {
                return Point.Unknown;
            }

            var calculator = new OuterTangentLinesIntersectionPointCalculator(circlePair);

            return calculator.IntersectionPoint;
        }

        [NotNull]
        private Tuple <Point, Point> CalculateTangentPointsForCircle([NotNull] ICircle circle)
        {
            Tuple <double, double> xt1And2 = CircleOneCalculateXt1And2(circle);
            Tuple <double, double> yt1And2 = CircleOneCalculateYt1And2(circle);

            ICoordinatePairCalculator calculator = new CoordinatePairCalculator(circle,
                                                                                xt1And2,
                                                                                yt1And2);

            Tuple <Point, Point> pair = calculator.Points;

            return pair;
        }

        [NotNull]
        // ReSharper disable once MethodTooLong
        private Tuple <double, double> CircleOneCalculateXt1And2([NotNull] ICircle circle)
        {
            // r0^2 * (xp-a) +- r0 * (yb-b) * sqrt((xp-a)^2+(yb-b)^2-r0^2) + a
            double r = circle.Radius;
            double a = circle.X;
            double b = circle.Y;
            double xp = m_IntersectionPoint.X;
            double yp = m_IntersectionPoint.Y;

            double rSquare = Math.Pow(r,
                                      2);
            double xpMinusA = xp - a;
            double ypMinusB = yp - b;

            double xpMinusASquare = Math.Pow(xpMinusA,
                                             2);
            double ypMinusBSquare = Math.Pow(ypMinusB,
                                             2);

            double rSquareMultiplyXpMinusA = rSquare * xpMinusA;
            double rMultipleYbMinusB = r * ypMinusB;
            double squareRootXt = Math.Sqrt(xpMinusASquare + ypMinusBSquare - rSquare);

            double commonBottom = xpMinusASquare + ypMinusBSquare;
            double topOne = rSquareMultiplyXpMinusA + rMultipleYbMinusB * squareRootXt;
            double topTwo = rSquareMultiplyXpMinusA - rMultipleYbMinusB * squareRootXt;

            double resultOne = topOne / commonBottom + a;
            double resultTwo = topTwo / commonBottom + a;

            return new Tuple <double, double>(resultOne,
                                              resultTwo);
        }

        [NotNull]
        // ReSharper disable once MethodTooLong
        private Tuple <double, double> CircleOneCalculateYt1And2([NotNull] ICircle circle)
        {
            // r0^2 * (yp-b) +- r0 * (xb-a) * sqrt((xp-a)^2+(yb-b)^2-r0^2) + b
            double r = circle.Radius;
            double a = circle.X;
            double b = circle.Y;
            double xp = m_IntersectionPoint.X;
            double yp = m_IntersectionPoint.Y;

            double rSquare = Math.Pow(r,
                                      2);
            double xpMinusA = xp - a;
            double ypMinusB = yp - b;

            double xpMinusASquare = Math.Pow(xpMinusA,
                                             2);
            double ypMinusBSquare = Math.Pow(ypMinusB,
                                             2);

            double rSquareMultiplyYpMinusB = rSquare * ypMinusB;
            double rMultipleXpMinusA = r * xpMinusA;
            double squareRootXt = Math.Sqrt(xpMinusASquare + ypMinusBSquare - rSquare);

            double commonBottom = xpMinusASquare + ypMinusBSquare;
            double topOne = rSquareMultiplyYpMinusB + rMultipleXpMinusA * squareRootXt;
            double topTwo = rSquareMultiplyYpMinusB - rMultipleXpMinusA * squareRootXt;

            double resultOne = topOne / commonBottom + b;
            double resultTwo = topTwo / commonBottom + b;

            return new Tuple <double, double>(resultOne,
                                              resultTwo);
        }

        #region IOuterTangentsCalculator Members

        public Point CircleZeroTangentPointOne
        {
            get
            {
                return m_CircleZeroTangentPointOne;
            }
        }

        public Point CircleZeroTangentPointTwo
        {
            get
            {
                return m_CircleZeroTangentPointTwo;
            }
        }

        public Point CircleOneTangentPointOne
        {
            get
            {
                return m_CircleOneTangentPointOne;
            }
        }

        public Point CircleOneTangentPointTwo
        {
            get
            {
                return m_CircleOneTangentPointTwo;
            }
        }

        public Point IntersectionPoint
        {
            get
            {
                return m_IntersectionPoint;
            }
        }

        #endregion
    }
}