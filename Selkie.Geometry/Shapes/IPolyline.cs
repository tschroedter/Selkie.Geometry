using System.Collections.Generic;
using JetBrains.Annotations;

namespace Selkie.Geometry.Shapes
{
    public interface IPolyline
    {
        [NotNull]
        Point StartPoint { get; }

        [NotNull]
        Point EndPoint { get; }

        [NotNull]
        IEnumerable <IPolylineSegment> Segments { get; }

        double Length { get; }
        void AddSegment([NotNull] IPolylineSegment segment);

        [NotNull]
        IPolyline Reverse();
    }
}