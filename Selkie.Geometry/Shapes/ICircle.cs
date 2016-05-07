using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes
{
    public interface ICircle : IShape
    {
        [NotNull]
        Point CentrePoint { get; }

        double X { get; }
        double Y { get; }
        double Radius { get; }
        bool IsUnknown { get; }
        double Distance([NotNull] ICircle other);

        [NotNull]
        Point PointOnCircle([NotNull] Angle angle);

        bool IsPointOnCircle([NotNull] Point startPoint);
        bool Intersects([NotNull] ICircle finishPointStarboard);

        [NotNull]
        Angle GetAngleRelativeToXAxis([NotNull] Point point);

        [NotNull]
        Angle RadiansRelativeToXAxisCounterClockwise([NotNull] Point point);
    }
}