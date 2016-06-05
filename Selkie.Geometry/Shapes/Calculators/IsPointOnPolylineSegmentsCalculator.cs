using System.Collections.Generic;

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
            foreach ( IPolylineSegment segment in m_Segments )
            {
                if ( segment.IsOnLine(point) )
                {
                    return true;
                }
            }

            return false;
        }
    }
}