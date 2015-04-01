using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes.Calculators
{
    public interface ICircleCentrePointToPointCalculator
    {
        [NotNull]
        Point CentrePoint { get; }

        [NotNull]
        Point StartPoint { get; }

        [NotNull]
        Point EndPoint { get; }

        [NotNull]
        Angle AngleClockwise { get; }

        [NotNull]
        Angle RadiansCounterClockwise { get; }
    }
}