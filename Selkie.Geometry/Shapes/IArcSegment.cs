using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public interface IArcSegment
        : IPolylineSegment
    {
        [NotNull]
        Point CentrePoint { get; }

        [NotNull]
        Angle AngleClockwise { get; }

        [NotNull]
        Angle AngleCounterClockwise { get; }

        double Radius { get; }
        double LengthClockwise { get; }
        double LengthCounterClockwise { get; }
        Constants.TurnDirection TurnDirection { get; }
        bool IsUnknown { get; }
    }
}