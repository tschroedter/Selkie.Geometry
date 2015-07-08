using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes
{
    public interface ITurnCircleArcSegment : IPolylineSegment
    {
        [NotNull]
        IArcSegment ArcSegment { get; }

        Constants.CircleOrigin CircleOrigin { get; }
        bool IsUnknown { get; }
        Angle Angle { get; }
        Angle AngleClockwise { get; }
        Angle AngleCounterClockwise { get; }
        Constants.TurnDirection TurnDirection { get; }
    }
}