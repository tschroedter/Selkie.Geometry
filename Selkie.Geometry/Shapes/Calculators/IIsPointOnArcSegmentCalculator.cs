using JetBrains.Annotations;

namespace Selkie.Geometry.Shapes.Calculators
{
    public interface IIsPointOnArcSegmentCalculator
    {
        bool IsPointOnArcSegment { get; }

        void Calculate([NotNull] IArcSegment segment,
                       [NotNull] Point point);
    }
}