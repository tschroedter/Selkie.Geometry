using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Selkie.Geometry.Shapes
{
    public class Polyline : IPolyline
    {
        private readonly List <IPolylineSegment> m_Segments = new List <IPolylineSegment>();
        private Point m_EndPoint = Point.Unknown;
        private Point m_StartPoint = Point.Unknown;

        public Polyline()
        {
        }

        private Polyline([NotNull] IEnumerable <IPolylineSegment> segments)
        {
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

            m_StartPoint = DetermineStartPoint(m_Segments);
            m_EndPoint = DetermineEndPoint(m_Segments);
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

        public IEnumerable <IPolylineSegment> Segments
        {
            get
            {
                return m_Segments;
            }
        }

        public double Length { get; private set; }

        public IPolyline Reverse()
        {
            IEnumerable <IPolylineSegment> reversedSegments = m_Segments.Select(segment => segment.Reverse());

            List <IPolylineSegment> segments = reversedSegments.ToList();

            segments.Reverse();

            var reverse = new Polyline(segments);

            return reverse;
        }

        #endregion
    }
}