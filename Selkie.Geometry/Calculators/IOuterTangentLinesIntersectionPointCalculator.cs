using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public interface IOuterTangentLinesIntersectionPointCalculator
    {
        [NotNull]
        Point IntersectionPoint { get; }

        bool IsUnknown { get; }
    }
}