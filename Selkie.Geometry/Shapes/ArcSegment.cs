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
        private readonly Angle m_AngleClockwise;
        private readonly Angle m_AngleCounterClockwise;
        private readonly ICircle m_Circle;
        private readonly Point m_EndPoint;
        private readonly bool m_IsUnknown;
        private readonly double m_Length;
        private readonly double m_LengthClockwise;
        private readonly double m_LengthCounterClockwise;
        private readonly Point m_StartPoint;
        private readonly Constants.TurnDirection m_TurnDirection;

        private ArcSegment()
        {
            m_IsUnknown = true;
            m_Circle = Circle.Unknown;
            m_StartPoint = Point.Unknown;
            m_EndPoint = Point.Unknown;
            m_TurnDirection = Constants.TurnDirection.Unknown;
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
            m_StartPoint = startPoint;
            m_EndPoint = endPoint;
            m_TurnDirection = arcTurnDirection;
            ICircleCentrePointToPointCalculator calculator = new CircleCentrePointToPointCalculator(
                m_Circle.CentrePoint,
                m_StartPoint,
                m_EndPoint);

            m_AngleClockwise = calculator.AngleRelativeToYAxisCounterClockwise;
            m_AngleCounterClockwise = calculator.AngleRelativeToYAxisClockwise;

            m_LengthClockwise = CalculateLength(m_AngleClockwise,
                                                m_Circle.Radius);
            m_LengthCounterClockwise = CalculateLength(m_AngleCounterClockwise,
                                                       m_Circle.Radius);

            m_Length = arcTurnDirection == Constants.TurnDirection.Clockwise
                           ? m_LengthClockwise
                           : m_LengthCounterClockwise;
        }

        public bool IsUnknown
        {
            get
            {
                return m_IsUnknown;
            }
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

        internal bool ValidatePoint([NotNull] ICircle circle,
                                    [NotNull] Point startPoint)
        {
            return circle.IsPointOnCircle(startPoint);
        }

        internal double CalculateLength([NotNull] Angle angle,
                                        double radius)
        {
            double length = ( angle.Degrees * Math.PI * radius ) / 180.0;

            return length;
        }

        #region IArcSegment Members

        public Constants.TurnDirection TurnDirection
        {
            get
            {
                return m_TurnDirection;
            }
        }

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

        public Point StartPoint
        {
            get
            {
                return m_StartPoint;
            }
        }

        public Point EndPoint
        {
            get
            {
                return m_EndPoint;
            }
        }

        public Angle AngleClockwise
        {
            get
            {
                return m_AngleClockwise;
            }
        }

        public Angle AngleCounterClockwise
        {
            get
            {
                return m_AngleCounterClockwise;
            }
        }

        public double Length
        {
            get
            {
                return m_Length;
            }
        }

        public double LengthClockwise
        {
            get
            {
                return m_LengthClockwise;
            }
        }

        public double LengthCounterClockwise
        {
            get
            {
                return m_LengthCounterClockwise;
            }
        }

        public IPolylineSegment Reverse()
        {
            var reverse = new ArcSegment(m_Circle,
                                         m_EndPoint,
                                         m_StartPoint,
                                         m_TurnDirection);

            return reverse;
        }

        #endregion
    }
}