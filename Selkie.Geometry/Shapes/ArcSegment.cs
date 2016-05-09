using System;
using JetBrains.Annotations;
using NLog;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes.Calculators;
using Selkie.Windsor.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public class ArcSegment : IArcSegment
    {
        private ArcSegment()
        {
            IsUnknown = true;
            m_Circle = Circle.Unknown;
            StartPoint = Point.Unknown;
            EndPoint = Point.Unknown;
            TurnDirection = Constants.TurnDirection.Unknown;
            AngleClockwise = Angle.Unknown;
            AngleCounterClockwise = Angle.Unknown;
            AngleToXAxisAtEndPoint = Angle.Unknown;
            AngleToXAxisAtStartPoint = Angle.Unknown;
        }

        public ArcSegment([NotNull] ICircle circle,
                          [NotNull] Point startPoint,
                          [NotNull] Point endPoint,
                          Constants.TurnDirection arcTurnDirection = Constants.TurnDirection.Clockwise)

        {
            ValidateStartAndEndPoint(circle,
                                     startPoint,
                                     endPoint);

            m_Circle = circle;
            StartPoint = startPoint;
            EndPoint = endPoint;
            TurnDirection = arcTurnDirection;
            ICircleCentrePointToPointCalculator calculator = new CircleCentrePointToPointCalculator(
                m_Circle.CentrePoint,
                StartPoint,
                EndPoint);

            AngleClockwise = calculator.AngleRelativeToYAxisCounterClockwise;
            AngleCounterClockwise = calculator.AngleRelativeToYAxisClockwise;

            LengthClockwise = CalculateLength(AngleClockwise,
                                              m_Circle.Radius);
            LengthCounterClockwise = CalculateLength(AngleCounterClockwise,
                                                     m_Circle.Radius);

            Length = arcTurnDirection == Constants.TurnDirection.Clockwise
                         ? LengthClockwise
                         : LengthCounterClockwise;

            AngleToXAxisAtStartPoint = CalculateTangentAngleToXAxisAtPoint(circle.CentrePoint,
                                                                           startPoint,
                                                                           arcTurnDirection);
            AngleToXAxisAtEndPoint = CalculateTangentAngleToXAxisAtPoint(circle.CentrePoint,
                                                                         endPoint,
                                                                         arcTurnDirection);
        }

        public static readonly IArcSegment Unknown = new ArcSegment();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ICircle m_Circle;

        public bool IsUnknown { get; }

        internal double CalculateLength([NotNull] Angle angle,
                                        double radius)
        {
            double length = angle.Degrees * Math.PI * radius / 180.0;

            return length;
        }

        internal bool ValidatePoint([NotNull] ICircle circle,
                                    [NotNull] Point startPoint)
        {
            return circle.IsPointOnCircle(startPoint);
        }

        internal void ValidateStartAndEndPoint([NotNull] ICircle circle,
                                               [NotNull] Point startPoint,
                                               [NotNull] Point endPoint)
        {
            if ( !ValidatePoint(circle,
                                startPoint) )
            {
                string message = "StartPoint {0} is not on circle [{1}] with radius {2}!".Inject(startPoint,
                                                                                                 circle.CentrePoint,
                                                                                                 circle.Radius);
                Logger.Error(message);

                throw new ArgumentException(message);
            }
            if ( !ValidatePoint(circle,
                                endPoint) )
            {
                string message = "EndPoint {0} is not on circle [{1}] with radius {2}!".Inject(endPoint,
                                                                                               circle.CentrePoint,
                                                                                               circle.Radius);

                Logger.Error(message);

                throw new ArgumentException(message);
            }
        }

        private Angle CalculateTangentAngleToXAxisAtPoint([NotNull] Point centrePoint,
                                                          [NotNull] Point pointOnCircle,
                                                          Constants.TurnDirection turnDirection)
        {
            var line = new Line(centrePoint,
                                pointOnCircle);

            Angle angleToXAxis = turnDirection == Constants.TurnDirection.Clockwise
                                     ? line.AngleToXAxis - Angle.For90Degrees
                                     : line.AngleToXAxis + Angle.For90Degrees;

            return angleToXAxis;
        }

        #region IArcSegment Members

        public Constants.TurnDirection TurnDirection { get; }

        public Point CentrePoint => m_Circle.CentrePoint;

        public double Radius => m_Circle.Radius;

        public Point StartPoint { get; }

        public Point EndPoint { get; }
        public Angle AngleToXAxisAtEndPoint { get; }
        public Angle AngleToXAxisAtStartPoint { get; }

        public Angle AngleClockwise { get; }

        public Angle AngleCounterClockwise { get; }

        public double Length { get; }

        public double LengthClockwise { get; }

        public double LengthCounterClockwise { get; }

        public IPolylineSegment Reverse()
        {
            var reverse = new ArcSegment(m_Circle,
                                         EndPoint,
                                         StartPoint,
                                         TurnDirection);

            return reverse;
        }

        public bool IsOnLine(Point point)
        {
            var calculator = new IsPointOnArcSegmentCalculator
                             {
                                 ArcSegment = this,
                                 Point = point
                             };

            calculator.Calculate();


            return calculator.IsPointOnArcSegment;
        }

        public Constants.TurnDirection TurnDirectionToPoint(Point point)
        {
            // todo figure out how to do it, tangent line -=> calculate circle center point to point and calculate intersection point with ArcSegment
            throw new NotImplementedException("TurnDirectionToPoint not implemented yet!");
        }

        #endregion
    }
}