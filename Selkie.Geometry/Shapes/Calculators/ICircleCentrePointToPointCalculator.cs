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
        Angle AngleRelativeToYAxisClockwise { get; } // todo rename ...RelativeToYAxisClockwise

        [NotNull]
        Angle AngleRelativeToYAxisCounterClockwise { get; }
    }
}