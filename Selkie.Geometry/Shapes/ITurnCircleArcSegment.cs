using JetBrains.Annotations;

namespace Selkie.Geometry.Shapes
{
    public interface ITurnCircleArcSegment : IArcSegment
    {
        [NotNull]
        IArcSegment ArcSegment { get; }
        Constants.CircleOrigin CircleOrigin { get; }
    }
}