using JetBrains.Annotations;

namespace Selkie.Geometry.Shapes
{
    public interface IPolylineSegment : IShape
    {
        double Length { get; }

        [NotNull]
        Point StartPoint { get; }

        [NotNull]
        Point EndPoint { get; }

        [NotNull]
        IPolylineSegment Reverse();

        bool IsOnLine(Point point);

        Constants.TurnDirection TurnDirectionToPoint([NotNull] Point point);
    }
}