using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes
{
    public interface IPolyline : IShape
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

        Angle AngleToXAxisAtStartPoint { get; }
        Angle AngleToXAxisAtEndPoint { get; }

        void AddSegment([NotNull] IPolylineSegment segment);

        bool IsOnLine([NotNull] Point point);

        [NotNull]
        IPolyline Reverse();

        Constants.TurnDirection TurnDirectionToPoint([NotNull] Point point);
    }
}