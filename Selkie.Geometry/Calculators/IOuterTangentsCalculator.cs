using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public interface IOuterTangentsCalculator
    {
        [NotNull]
        Point CircleZeroTangentPointOne { get; }

        [NotNull]
        Point CircleZeroTangentPointTwo { get; }

        [NotNull]
        Point CircleOneTangentPointOne { get; }

        [NotNull]
        Point CircleOneTangentPointTwo { get; }

        [NotNull]
        Point IntersectionPoint { get; }
    }
}