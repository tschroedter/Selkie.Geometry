using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes.Calculators
{
    public interface ICircleCentreToPointCalculator
    {
        [NotNull]
        Point CentrePoint { get; }

        [NotNull]
        Point Point { get; }

        [NotNull]
        Angle AngleRelativeToXAxisCounterClockwise { get; }

        [NotNull]
        Angle AngleRelativeToYAxisClockwise { get; }

        [NotNull]
        Angle AngleRelativeToYAxisCounterclockwise { get; }

        void Calculate([NotNull] Point point);

        [NotNull]
        Angle CalculateAngleRelativeToXAxisCounterClockwise([NotNull] Point centre,
                                                            [NotNull] Point point);
    }
}