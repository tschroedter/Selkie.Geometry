using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes
{
    public class Polyline : IPolyline
    {
        public const int UnknownId = int.MinValue;
        public static readonly Polyline Unknown = new Polyline();

        private readonly List <IPolylineSegment> m_Segments = new List <IPolylineSegment>();

        private Polyline()
        {
            Id = UnknownId;
            IsUnknown = true;
            EndPoint = Point.Unknown;
            StartPoint = Point.Unknown;
            RunDirection = Constants.LineDirection.Unknown;
            AngleToXAxisAtEndPoint = Angle.Unknown;
            AngleToXAxisAtStartPoint = Angle.Unknown;
        }

        public Polyline(int id,
                        Constants.LineDirection runDirection)
        {
            Id = id;
            IsUnknown = false;
            EndPoint = Point.Unknown;
            StartPoint = Point.Unknown;
            RunDirection = runDirection;
            AngleToXAxisAtEndPoint = Angle.Unknown;
            AngleToXAxisAtStartPoint = Angle.Unknown;
        }

        private Polyline([NotNull] IEnumerable <IPolylineSegment> segments)
        {
            EndPoint = Point.Unknown;
            StartPoint = Point.Unknown;
            foreach ( IPolylineSegment segment in segments )
            {
                AddSegment(segment);
            }
        }

        [NotNull]
        internal Point DetermineEndPoint([NotNull] IEnumerable <IPolylineSegment> segments)
        {
            IPolylineSegment last = segments.LastOrDefault();

            if ( last == null )
            {
                return Point.Unknown;
            }

            return last.EndPoint;
        }

        [NotNull]
        internal Point DetermineStartPoint([NotNull] IEnumerable <IPolylineSegment> segments)
        {
            IPolylineSegment first = segments.FirstOrDefault();

            if ( first == null )
            {
                return Point.Unknown;
            }

            return first.StartPoint;
        }

        #region IPolyline Members

        public void AddSegment(IPolylineSegment segment)
        {
            m_Segments.Add(segment);
            Length += segment.Length;

            StartPoint = DetermineStartPoint(m_Segments);
            EndPoint = DetermineEndPoint(m_Segments);

            AngleToXAxisAtStartPoint = DetermineAngleToXAxisAtStartPoint(m_Segments);
            AngleToXAxisAtEndPoint = DetermineAngleToXAxisAtEndPoint(m_Segments);
        }

        private Angle DetermineAngleToXAxisAtEndPoint([NotNull] IEnumerable <IPolylineSegment> segments)
        {
            IPolylineSegment segment = segments.First();

            return segment.AngleToXAxisAtEndPoint;
        }

        private Angle DetermineAngleToXAxisAtStartPoint([NotNull] IEnumerable <IPolylineSegment> segments)
        {
            IPolylineSegment segment = segments.First();

            return segment.AngleToXAxisAtStartPoint;
        }

        public Constants.TurnDirection TurnDirectionToPoint(Point point)
        {
            IPolylineSegment firstSegment = m_Segments.FirstOrDefault();

            if ( firstSegment == null )
            {
                return Constants.TurnDirection.Unknown;
            }

            Constants.TurnDirection turnDirection = firstSegment.TurnDirectionToPoint(point);

            return turnDirection;
        }

        public Point StartPoint { get; private set; }

        public Point EndPoint { get; private set; }

        public IEnumerable <IPolylineSegment> Segments
        {
            get
            {
                return m_Segments;
            }
        }

        public Angle AngleToXAxisAtStartPoint { get; private set; }
        public Angle AngleToXAxisAtEndPoint { get; private set; }

        public int Id { get; private set; }

        public bool IsUnknown { get; private set; }

        public double Length { get; private set; }

        public Constants.LineDirection RunDirection { get; private set; }

        public IPolyline Reverse()
        {
            IEnumerable <IPolylineSegment> reversedSegments = m_Segments.Select(segment => segment.Reverse());

            List <IPolylineSegment> segments = reversedSegments.ToList();

            segments.Reverse();

            var reverse = new Polyline(segments);

            return reverse;
        }

        public bool IsOnLine(Point point)
        {
            foreach ( IPolylineSegment segment in m_Segments )
            {
                bool isOneSegment = segment.IsOnLine(point);

                if ( isOneSegment )
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}