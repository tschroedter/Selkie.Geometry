using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes
{
    public interface ITurnCircle
    {
        [NotNull]
        Point CentrePoint { get; }

        [NotNull]
        Distance Radius { get; }

        Constants.TurnDirection TurnDirection { get; }

        [NotNull]
        ICircle Circle { get; }

        Constants.CircleSide Side { get; }
        Constants.CircleOrigin Origin { get; }
        bool IsUnknown { get; }
        bool IsPointOnCircle([NotNull] Point point);
    }
}