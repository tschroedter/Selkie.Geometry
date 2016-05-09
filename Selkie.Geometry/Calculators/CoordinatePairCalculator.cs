using System;
using JetBrains.Annotations;
using NLog;
using Selkie.Geometry.Shapes;
using Selkie.Windsor.Extensions;

namespace Selkie.Geometry.Calculators
{
    public class CoordinatePairCalculator : ICoordinatePairCalculator
    {
        public CoordinatePairCalculator()
        {
            Circle = Shapes.Circle.Unknown;
            Points = new Tuple <Point, Point>(Point.Unknown,
                                              Point.Unknown);
            Xt1And2 = new Tuple <double, double>(double.NaN,
                                                 double.NaN);
            Yt1And2 = new Tuple <double, double>(double.NaN,
                                                 double.NaN);
        }

        public CoordinatePairCalculator([NotNull] ICircle circle,
                                        [NotNull] Tuple <double, double> xt1And2,
                                        [NotNull] Tuple <double, double> yt1And2)
        {
            Circle = circle;
            Xt1And2 = xt1And2;
            Yt1And2 = yt1And2;

            Points = Calculate(circle,
                               xt1And2,
                               yt1And2);
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [NotNull]
        // ReSharper disable once MethodTooLong
        internal Tuple <Point, Point> Calculate([NotNull] ICircle circle,
                                                [NotNull] Tuple <double, double> xt1And2,
                                                [NotNull] Tuple <double, double> yt1And2)
        {
            if ( AreInputFieldsValid(circle,
                                     xt1And2,
                                     yt1And2) )
            {
                return new Tuple <Point, Point>(Point.Unknown,
                                                Point.Unknown);
            }

            double xt1 = xt1And2.Item1;
            double xt2 = xt1And2.Item2;
            double yt1 = yt1And2.Item1;
            double yt2 = yt1And2.Item2;

            var pointOne = new Point(xt1,
                                     yt1);
            var pointTwo = new Point(xt2,
                                     yt2);

            if ( !circle.IsPointOnCircle(pointOne) )
            {
                pointOne = new Point(xt1,
                                     yt2);
                pointTwo = new Point(xt2,
                                     yt1);

                if ( !circle.IsPointOnCircle(pointOne) )
                {
                    PointIsNotOnCircle(circle,
                                       pointOne);
                }
            }

            if ( !circle.IsPointOnCircle(pointTwo) )
            {
                PointIsNotOnCircle(circle,
                                   pointTwo);
            }

            return new Tuple <Point, Point>(pointOne,
                                            pointTwo);
        }

        private static bool AreInputFieldsValid([NotNull] ICircle circle,
                                                [NotNull] Tuple <double, double> xt1And2,
                                                [NotNull] Tuple <double, double> yt1And2)
        {
            return circle.IsUnknown || double.IsNaN(xt1And2.Item1) || double.IsNaN(xt1And2.Item2) ||
                   double.IsNaN(yt1And2.Item1) || double.IsNaN(yt1And2.Item2);
        }

        private static void PointIsNotOnCircle([NotNull] ICircle circle,
                                               [NotNull] Point point)
        {
            string message = "Point {0} is not on circle [{1}] with radius {2}!".Inject(point,
                                                                                        circle.CentrePoint,
                                                                                        circle.Radius);
            Logger.Error(message);

            throw new ArgumentException(message);
        }

        #region ICoordinatePairCalculator Members

        public ICircle Circle { get; }

        public Tuple <double, double> Xt1And2 { get; }

        public Tuple <double, double> Yt1And2 { get; }

        public Tuple <Point, Point> Points { get; }

        #endregion
    }
}