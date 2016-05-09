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
        private TurnCircleArcSegment()
        {
            IsUnknown = true;
            ArcSegment = Shapes.ArcSegment.Unknown;

            AngleToXAxisAtEndPoint = Angle.Unknown;
            AngleToXAxisAtStartPoint = Angle.Unknown;
        }

        internal TurnCircleArcSegment(IArcSegment arcSegment,
                                      Constants.TurnDirection direction,
                                      Constants.CircleOrigin circleOrigin)
        {
            ArcSegment = arcSegment;
            TurnDirection = direction;
            CircleOrigin = circleOrigin;

            AngleToXAxisAtEndPoint = arcSegment.AngleToXAxisAtEndPoint;
            AngleToXAxisAtStartPoint = arcSegment.AngleToXAxisAtStartPoint;
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

            TurnDirection = direction;
            CircleOrigin = circleOrigin;

            AngleToXAxisAtEndPoint = ArcSegment.AngleToXAxisAtEndPoint;
            AngleToXAxisAtStartPoint = ArcSegment.AngleToXAxisAtStartPoint;
        }

        public static readonly ITurnCircleArcSegment Unknown = new TurnCircleArcSegment();

        public Angle Angle => ArcSegment.TurnDirection == Constants.TurnDirection.Clockwise
                                  ? ArcSegment.AngleClockwise
                                  : ArcSegment.AngleCounterClockwise;

        public override string ToString()
        {
            return "CentrePoint: {0} StartPoint: {1} EndPoint: {2} Direction: {3}"
                .Inject(ArcSegment.CentrePoint,
                        ArcSegment.StartPoint,
                        ArcSegment.EndPoint,
                        ArcSegment.TurnDirection);
        }

        #region ITurnCircleArcSegment Members

        public bool IsUnknown { get; }

        public bool IsPointInsideCircle(Point point)
        {
            throw new NotImplementedException();
        }

        public IArcSegment ArcSegment { get; }

        public Point CentrePoint => ArcSegment.CentrePoint;

        public Angle AngleClockwise => ArcSegment.AngleClockwise;

        public Angle AngleCounterClockwise => ArcSegment.AngleCounterClockwise;

        public Point StartPoint => ArcSegment.StartPoint;

        public Point EndPoint => ArcSegment.EndPoint;

        public Angle AngleToXAxisAtEndPoint { get; }
        public Angle AngleToXAxisAtStartPoint { get; }

        public double Length => ArcSegment.TurnDirection == Constants.TurnDirection.Clockwise
                                    ? ArcSegment.LengthClockwise
                                    : ArcSegment.LengthCounterClockwise;

        public IPolylineSegment Reverse()
        {
            var arcSegment = ArcSegment.Reverse() as IArcSegment;

            Constants.CircleOrigin origin = CircleOrigin == Constants.CircleOrigin.Start
                                                ? Constants.CircleOrigin.Finish
                                                : Constants.CircleOrigin.Start;

            var reverse = new TurnCircleArcSegment(arcSegment,
                                                   TurnDirection,
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

        public double Radius => ArcSegment.Radius;

        public double LengthClockwise => ArcSegment.LengthClockwise;

        public double LengthCounterClockwise => ArcSegment.LengthCounterClockwise;

        public Constants.TurnDirection TurnDirection { get; }

        public Constants.CircleOrigin CircleOrigin { get; }

        #endregion
    }
}