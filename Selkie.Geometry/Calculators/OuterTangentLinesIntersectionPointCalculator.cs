using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public class OuterTangentLinesIntersectionPointCalculator : IOuterTangentLinesIntersectionPointCalculator
    {
        public OuterTangentLinesIntersectionPointCalculator([NotNull] ICirclePair pair)
        {
            double x = CalculateX(pair);
            double y = CalculateY(pair);

            IntersectionPoint = new Point(x,
                                          y);
        }

        private OuterTangentLinesIntersectionPointCalculator()
        {
            IntersectionPoint = Point.Unknown;
            IsUnknown = true;
        }

        // ReSharper disable InconsistentNaming
        public static readonly OuterTangentLinesIntersectionPointCalculator Unknown =
            new OuterTangentLinesIntersectionPointCalculator();

        // ReSharper restore InconsistentNaming

        public bool IsUnknown { get; }

        public Point IntersectionPoint { get; }

        private static double CalculateX([NotNull] ICirclePair pair)
        {
            double a = pair.Zero.X;
            double c = pair.One.X;

            double top = c * pair.RadiusZero - a * pair.RadiusOne;
            double bottom = pair.RadiusZero - pair.RadiusOne;

            double x = top / bottom;

            return x;
        }

        private static double CalculateY([NotNull] ICirclePair pair)
        {
            double b = pair.Zero.Y;
            double d = pair.One.Y;

            double top = d * pair.RadiusZero - b * pair.RadiusOne;
            double bottom = pair.RadiusZero - pair.RadiusOne;

            double x = top / bottom;

            return x;
        }
    }
}