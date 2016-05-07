using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes
{
    public interface IPolylineSegment : IShape
    {
        double Length { get; }

        [NotNull]
        Point StartPoint { get; }

        [NotNull]
        Point EndPoint { get; }

        Angle AngleToXAxisAtEndPoint { get; }
        Angle AngleToXAxisAtStartPoint { get; }

        [NotNull]
        IPolylineSegment Reverse();

        bool IsOnLine(Point point);

        Constants.TurnDirection TurnDirectionToPoint([NotNull] Point point);
    }
}