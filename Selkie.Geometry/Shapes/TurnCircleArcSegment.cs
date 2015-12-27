using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Geometry.Shapes
{
    [ProjectComponent(Lifestyle.Transient)]
    public class TurnCircleArcSegment : ITurnCircleArcSegment
    {
        public static readonly ITurnCircleArcSegment Unknown = new TurnCircleArcSegment();
        private readonly Constants.CircleOrigin m_CircleOrigin;
        private readonly Constants.TurnDirection m_Direction;

        private TurnCircleArcSegment()
        {
            IsUnknown = true;
            ArcSegment = Shapes.ArcSegment.Unknown;
        }

        internal TurnCircleArcSegment(IArcSegment arcSegment,
                                      Constants.TurnDirection direction,
                                      Constants.CircleOrigin circleOrigin)
        {
            ArcSegment = arcSegment;
            m_Direction = direction;
            m_CircleOrigin = circleOrigin;
        }

        public TurnCircleArcSegment([NotNull] ICircle circle,
                                    Constants.TurnDirection direction,
                                    Constants.CircleOrigin circleOrigin,
                                    [NotNull] Point startPoint,
                                    [NotNull] Point endPoint)
        {
            ArcSegment = new ArcSegment(circle,
                                        startPoint,
                                        endPoint,
                                        direction);

            m_Direction = direction;
            m_CircleOrigin = circleOrigin;
        }

        public Angle Angle
        {
            get
            {
                return ArcSegment.TurnDirection == Constants.TurnDirection.Clockwise
                           ? ArcSegment.AngleClockwise
                           : ArcSegment.AngleCounterClockwise;
            }
        }

        public override string ToString()
        {
            return "CentrePoint: {0} StartPoint: {1} EndPoint: {2} Direction: {3}"
                .Inject(ArcSegment.CentrePoint,
                        ArcSegment.StartPoint,
                        ArcSegment.EndPoint,
                        ArcSegment.TurnDirection);
        }

        #region ITurnCircleArcSegment Members

        public bool IsUnknown { get; private set; }

        public bool IsPointInsideCircle(Point point)
        {
            throw new NotImplementedException();
        }

        public IArcSegment ArcSegment { get; private set; }

        public Point CentrePoint
        {
            get
            {
                return ArcSegment.CentrePoint;
            }
        }

        public Angle AngleClockwise
        {
            get
            {
                return ArcSegment.AngleClockwise;
            }
        }

        public Angle AngleCounterClockwise
        {
            get
            {
                return ArcSegment.AngleCounterClockwise;
            }
        }

        public Point StartPoint
        {
            get
            {
                return ArcSegment.StartPoint;
            }
        }

        public Point EndPoint
        {
            get
            {
                return ArcSegment.EndPoint;
            }
        }

        public double Length
        {
            get
            {
                return ArcSegment.TurnDirection == Constants.TurnDirection.Clockwise
                           ? ArcSegment.LengthClockwise
                           : ArcSegment.LengthCounterClockwise;
            }
        }

        public IPolylineSegment Reverse()
        {
            var arcSegment = ArcSegment.Reverse() as IArcSegment;

            Constants.CircleOrigin origin = m_CircleOrigin == Constants.CircleOrigin.Start
                                                ? Constants.CircleOrigin.Finish
                                                : Constants.CircleOrigin.Start;

            var reverse = new TurnCircleArcSegment(arcSegment,
                                                   m_Direction,
                                                   origin);


            return reverse;
        }

        public bool IsOnLine(Point point)
        {
            return ArcSegment.IsOnLine(point);
        }

        public Constants.TurnDirection TurnDirectionToPoint(Point point)
        {
            // todo need to take this.TurnDirection into account? figure out how to do it, tangent line
            throw new NotImplementedException("TurnDirectionToPoint not implemented yet!");
        }

        public double Radius
        {
            get
            {
                return ArcSegment.Radius;
            }
        }

        public double LengthClockwise
        {
            get
            {
                return ArcSegment.LengthClockwise;
            }
        }

        public double LengthCounterClockwise
        {
            get
            {
                return ArcSegment.LengthCounterClockwise;
            }
        }

        public Constants.TurnDirection TurnDirection
        {
            get
            {
                return m_Direction;
            }
        }

        public Constants.CircleOrigin CircleOrigin
        {
            get
            {
                return m_CircleOrigin;
            }
        }

        #endregion
    }
}