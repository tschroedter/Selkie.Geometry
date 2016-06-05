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
        Angle GetAngleRelativeToXAxis([NotNull] Point point);

        bool Intersects([NotNull] ICircle finishPointStarboard);

        bool IsPointOnCircle([NotNull] Point startPoint);

        [NotNull]
        Point PointOnCircle([NotNull] Angle angle);

        [NotNull]
        Angle RadiansRelativeToXAxisCounterClockwise([NotNull] Point point);
    }
}