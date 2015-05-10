using JetBrains.Annotations;

namespace Selkie.Geometry.Shapes
{
    public interface IPolylineSegment
    {
        double Length { get; }

        [NotNull]
        Point StartPoint { get; }

        [NotNull]
        Point EndPoint { get; }

        [NotNull]
        IPolylineSegment Reverse();
    }
}