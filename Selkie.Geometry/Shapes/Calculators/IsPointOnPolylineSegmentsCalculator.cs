using System.Collections.Generic;
using System.Linq;

namespace Selkie.Geometry.Shapes.Calculators
{
    public class IsPointOnPolylineSegmentsCalculator : IIsPointOnPolylineSegmentsCalculator
    {
        public IsPointOnPolylineSegmentsCalculator(IEnumerable <IPolylineSegment> segments)
        {
            m_Segments = segments;
        }

        private readonly IEnumerable <IPolylineSegment> m_Segments;

        public bool IsOnLine(Point point)
        {
            return m_Segments.Any(segment => segment.IsOnLine(point));
        }
    }
}