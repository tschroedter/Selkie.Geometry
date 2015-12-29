using JetBrains.Annotations;

namespace Selkie.Geometry.Shapes.Calculators
{
    public interface IIsPointOnArcSegmentCalculator
    {
        bool IsPointOnArcSegment { get; }

        [NotNull]
        IArcSegment ArcSegment { get; set; }

        [NotNull]
        Point Point { get; set; }

        void Calculate();
    }
}