using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public interface IInnerTangentLinesIntersectionPointCalculator
    {
        bool IsUnknown { get; }

        [NotNull]
        Point IntersectionPoint { get; }
    }
}