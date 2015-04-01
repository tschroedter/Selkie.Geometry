using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public interface ICirclesIntersectionPointsCalculator
    {
        bool HasIntersectionPoints { get; }
        bool IsCirclesAreSame { get; }
        bool IsCirclesTouchAtSinglePoint { get; }

        [NotNull]
        Point IntersectionPointOne { get; }

        [NotNull]
        Point IntersectionPointTwo { get; }
    }
}