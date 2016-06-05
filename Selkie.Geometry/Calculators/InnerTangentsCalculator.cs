using System;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public class InnerTangentsCalculator : IInnerTangentsCalculator
    {
        public InnerTangentsCalculator([NotNull] ICirclePair circlePair)
        {
            if ( circlePair.NumberOfTangents < 3 )
            {
                m_IntersectionPoint = Point.Unknown;
                m_CircleZeroTangentPointOne = Point.Unknown;
                m_CircleZeroTangentPointTwo = Point.Unknown;
                m_CircleOneTangentPointOne = Point.Unknown;
                m_CircleOneTangentPointTwo = Point.Unknown;

                m_HasTangentPoints = false;
            }
            else if ( circlePair.NumberOfTangents == 3 )
            {
                m_IntersectionPoint = CalculateInnerTangentLinesIntersectionPoint(circlePair);

                m_CircleZeroTangentPointOne = m_IntersectionPoint;
                m_CircleZeroTangentPointTwo = m_IntersectionPoint;

                m_CircleOneTangentPointOne = m_IntersectionPoint;
                m_CircleOneTangentPointTwo = m_IntersectionPoint;

                m_HasTangentPoints = true;
            }
            else
            {
                m_IntersectionPoint = CalculateInnerTangentLinesIntersectionPoint(circlePair);

                Tuple <Point, Point> pairZero = CalculateTangentPointsForCircleOne(circlePair.Zero);
                Tuple <Point, Point> pairOne = CalculateTangentPointsForCircleOne(circlePair.One);

                m_CircleZeroTangentPointOne = pairZero.Item1;
                m_CircleZeroTangentPointTwo = pairZero.Item2;

                m_CircleOneTangentPointOne = pairOne.Item1;
                m_CircleOneTangentPointTwo = pairOne.Item2;

                m_HasTangentPoints = true;
            }
        }

        private readonly Point m_CircleOneTangentPointOne;
        private readonly Point m_CircleOneTangentPointTwo;
        private readonly Point m_CircleZeroTangentPointOne;
        private readonly Point m_CircleZeroTangentPointTwo;
        private readonly bool m_HasTangentPoints;
        private readonly Point m_IntersectionPoint;

        [NotNull]
        private Point CalculateInnerTangentLinesIntersectionPoint([NotNull] ICirclePair circlePair)
        {
            var calculator = new InnerTangentLinesIntersectionPointCalculator(circlePair);

            return calculator.IntersectionPoint;
        }

        [NotNull]
        private Tuple <Point, Point> CalculateTangentPointsForCircleOne([NotNull] ICircle circle)
        {
            Tuple <double, double> xt1And2 = CircleOneCalculateXt1And2(circle);
            Tuple <double, double> yt1And2 = CircleOneCalculateYt1And2(circle);

            var calculator = new CoordinatePairCalculator(circle,
                                                          xt1And2,
                                                          yt1And2);

            return calculator.Points;
        }

        [NotNull]
        // ReSharper disable once MethodTooLong
        private Tuple <double, double> CircleOneCalculateXt1And2([NotNull] ICircle circle)
        {
            // r1^2 * (xp-c) +- r1 * (yb-d) * sqrt((xp-c)^2+(yb-d)^2-r1^2) + a
            double r = circle.Radius;
            double c = circle.X;
            double d = circle.Y;
            double xp = m_IntersectionPoint.X;
            double yp = m_IntersectionPoint.Y;

            double rSquare = Math.Pow(r,
                                      2);
            double xpMinusC = xp - c;
            double ypMinusD = yp - d;

            double xpMinusCSquare = Math.Pow(xpMinusC,
                                             2);
            double ypMinusDSquare = Math.Pow(ypMinusD,
                                             2);

            double rSquareMultiplyXpMinusA = rSquare * xpMinusC;
            double rMultipleYbMinusB = r * ypMinusD;
            double squareRootXt = Math.Sqrt(xpMinusCSquare + ypMinusDSquare - rSquare);

            double commonBottom = xpMinusCSquare + ypMinusDSquare;
            double topOne = rSquareMultiplyXpMinusA + rMultipleYbMinusB * squareRootXt;
            double topTwo = rSquareMultiplyXpMinusA - rMultipleYbMinusB * squareRootXt;

            double resultOne = topOne / commonBottom + c;
            double resultTwo = topTwo / commonBottom + c;

            return new Tuple <double, double>(resultOne,
                                              resultTwo);
        }

        [NotNull]
        // ReSharper disable once MethodTooLong
        private Tuple <double, double> CircleOneCalculateYt1And2([NotNull] ICircle circle)
        {
            // r1^2 * (yp-d) +- r1 * (xp-c) * sqrt((xp-c)^2+(yb-d)^2-r1^2) + b
            double r = circle.Radius;
            double c = circle.X;
            double d = circle.Y;
            double xp = m_IntersectionPoint.X;
            double yp = m_IntersectionPoint.Y;

            double rSquare = Math.Pow(r,
                                      2);
            double xpMinusC = xp - c;
            double ypMinusD = yp - d;

            double xpMinusCSquare = Math.Pow(xpMinusC,
                                             2);
            double ypMinusDSquare = Math.Pow(ypMinusD,
                                             2);

            double rSquareMultiplyYpMinusB = rSquare * ypMinusD;
            double rMultipleXpMinusA = r * xpMinusC;
            double squareRootXt = Math.Sqrt(xpMinusCSquare + ypMinusDSquare - rSquare);

            double commonBottom = xpMinusCSquare + ypMinusDSquare;
            double topOne = rSquareMultiplyYpMinusB + rMultipleXpMinusA * squareRootXt;
            double topTwo = rSquareMultiplyYpMinusB - rMultipleXpMinusA * squareRootXt;

            double resultOne = topOne / commonBottom + d;
            double resultTwo = topTwo / commonBottom + d;

            return new Tuple <double, double>(resultOne,
                                              resultTwo);
        }

        #region IInnerTangentsCalculator Members

        public bool HasTangentPoints
        {
            get
            {
                return m_HasTangentPoints;
            }
        }

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