using System.Collections.Generic;
using JetBrains.Annotations;

namespace Selkie.Geometry.Shapes
{
    public interface IPolyline
    {
        int Id { get; }
        bool IsUnknown { get; }
        double Length { get; }
        Constants.LineDirection RunDirection { get; }

        [NotNull]
        Point StartPoint { get; }

        [NotNull]
        Point EndPoint { get; }

        [NotNull]
        IEnumerable <IPolylineSegment> Segments { get; }

        Constants.TurnDirection TurnDirectionToPoint([NotNull] Point point);

        void AddSegment([NotNull] IPolylineSegment segment);

        [NotNull]
        IPolyline Reverse();

        bool IsOnLine([NotNull] Point point);
    }
}