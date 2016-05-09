using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes.Calculators
{
    public class CircleCentrePointToPointCalculator : ICircleCentrePointToPointCalculator
    {
        public CircleCentrePointToPointCalculator([NotNull] Point centrePoint,
                                                  [NotNull] Point startPoint,
                                                  [NotNull] Point endPoint)
        {
            CentrePoint = centrePoint;
            StartPoint = startPoint;
            EndPoint = endPoint;
            AngleRelativeToYAxisCounterClockwise = Angle.Unknown;
            AngleRelativeToYAxisClockwise = Angle.Unknown;

            if ( CentrePoint == Point.Unknown ||
                 StartPoint == Point.Unknown ||
                 EndPoint == Point.Unknown )
            {
                return;
            }

            AngleRelativeToYAxisCounterClockwise = RadiansBetweenPointsCounterClockwise(centrePoint,
                                                                                        startPoint,
                                                                                        endPoint);
            AngleRelativeToYAxisClockwise = Angle.Inverse(AngleRelativeToYAxisCounterClockwise);
        }

        [NotNull]
        internal Angle RadiansBetweenPointsCounterClockwise([NotNull] Point centrePoint,
                                                            [NotNull] Point startPoint,
                                                            [NotNull] Point endPoint)
        {
            ICircleCentreToPointCalculator calculatorStart = new CircleCentreToPointCalculator(centrePoint,
                                                                                               startPoint);
            ICircleCentreToPointCalculator calculatorEnd = new CircleCentreToPointCalculator(centrePoint,
                                                                                             endPoint);

            Angle angleStart = calculatorStart.AngleRelativeToYAxisCounterclockwise;
            Angle angleEnd = calculatorEnd.AngleRelativeToYAxisCounterclockwise;

            Angle angle = angleEnd - angleStart;

            return angle;
        }

        #region ICircleCentrePointToPointCalculator Members

        public Point CentrePoint { get; }

        public Point StartPoint { get; }

        public Point EndPoint { get; }

        public Angle AngleRelativeToYAxisClockwise { get; }

        public Angle AngleRelativeToYAxisCounterClockwise { get; }

        #endregion
    }
}