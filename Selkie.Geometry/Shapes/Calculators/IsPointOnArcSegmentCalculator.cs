using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes.Calculators
{
    public class IsPointOnArcSegmentCalculator : IIsPointOnArcSegmentCalculator
    {
        // todo better to have properties ArcSegment, Point, and call Calculate

        public IsPointOnArcSegmentCalculator([NotNull] IArcSegment segment,
                                             [NotNull] Point point)
        {
            Calculate(segment,
                      point);
        }

        public void Calculate(IArcSegment segment,
                              Point point)
        {
            IsPointOnArcSegment = CalculateIfPointIsOnArcSegment(segment,
                                                                 point);
        }

        public bool IsPointOnArcSegment { get; private set; }

        private bool CalculateIfPointIsOnArcSegment([NotNull] IArcSegment segment,
                                                    [NotNull] Point point)
        {
            if ( segment.StartPoint.Equals(point) ||
                 segment.EndPoint.Equals(point) )
            {
                return true;
            }

            if ( IsDistanceToPointGreaterThanRadius(segment,
                                                    point) )
            {
                return false;
            }

            ICircleCentreToPointCalculator calculatorStart = new CircleCentreToPointCalculator(segment.CentrePoint,
                                                                                               segment.StartPoint);
            ICircleCentreToPointCalculator calculatorEnd = new CircleCentreToPointCalculator(segment.CentrePoint,
                                                                                             segment.EndPoint);
            ICircleCentreToPointCalculator calculatorPoint = new CircleCentreToPointCalculator(segment.CentrePoint,
                                                                                               point);

            Angle angleStart = calculatorStart.AngleRelativeToYAxisCounterclockwise;
            Angle angleEnd = calculatorEnd.AngleRelativeToYAxisCounterclockwise;
            Angle anglePoint = calculatorPoint.AngleRelativeToYAxisCounterclockwise;

            if ( angleStart.Degrees >= 0.0 )
            {
                if ( angleEnd.Degrees >= 0.0 )
                {
                    if ( anglePoint >= angleStart &&
                         anglePoint <= angleEnd )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsDistanceToPointGreaterThanRadius(IArcSegment segment,
                                                        Point point)
        {
            var centreToPoint = new Line(segment.CentrePoint,
                                         point);

            return Math.Abs(centreToPoint.Length - segment.Radius) > SelkieConstants.EpsilonDistance;
        }
    }
}