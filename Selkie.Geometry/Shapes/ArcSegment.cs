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
        public static readonly IArcSegment Unknown = new ArcSegment();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ICircle m_Circle;

        private ArcSegment()
        {
            IsUnknown = true;
            m_Circle = Circle.Unknown;
            StartPoint = Point.Unknown;
            EndPoint = Point.Unknown;
            TurnDirection = Constants.TurnDirection.Unknown;
            AngleClockwise = Angle.Unknown;
            AngleCounterClockwise = Angle.Unknown;
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
        }

        public bool IsUnknown { get; private set; }

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

        internal bool ValidatePoint([NotNull] ICircle circle,
                                    [NotNull] Point startPoint)
        {
            return circle.IsPointOnCircle(startPoint);
        }

        internal double CalculateLength([NotNull] Angle angle,
                                        double radius)
        {
            double length = angle.Degrees * Math.PI * radius / 180.0;

            return length;
        }

        #region IArcSegment Members

        public Constants.TurnDirection TurnDirection { get; private set; }

        public Point CentrePoint
        {
            get
            {
                return m_Circle.CentrePoint;
            }
        }

        public double Radius
        {
            get
            {
                return m_Circle.Radius;
            }
        }

        public Point StartPoint { get; private set; }

        public Point EndPoint { get; private set; }

        public Angle AngleClockwise { get; private set; }

        public Angle AngleCounterClockwise { get; private set; }

        public double Length { get; private set; }

        public double LengthClockwise { get; private set; }

        public double LengthCounterClockwise { get; private set; }

        public IPolylineSegment Reverse()
        {
            var reverse = new ArcSegment(m_Circle,
                                         EndPoint,
                                         StartPoint,
                                         TurnDirection);

            return reverse;
        }

        #endregion
    }
}