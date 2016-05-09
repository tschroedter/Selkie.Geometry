using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public class InnerTangentLinesIntersectionPointCalculator : IInnerTangentLinesIntersectionPointCalculator
    {
        public InnerTangentLinesIntersectionPointCalculator([NotNull] ICirclePair pair)
        {
            double x = CalculateX(pair);
            double y = CalculateY(pair);

            IntersectionPoint = new Point(x,
                                          y);
        }

        private InnerTangentLinesIntersectionPointCalculator()
        {
            IntersectionPoint = Point.Unknown;
            IsUnknown = true;
        }

        // ReSharper disable InconsistentNaming
        public static InnerTangentLinesIntersectionPointCalculator Unknown =
            new InnerTangentLinesIntersectionPointCalculator();

        // ReSharper restore InconsistentNaming

        public bool IsUnknown { get; }

        public Point IntersectionPoint { get; }

        private double CalculateX([NotNull] ICirclePair pair)
        {
            double a = pair.Zero.X;
            double c = pair.One.X;

            double top = c * pair.RadiusZero + a * pair.RadiusOne;
            double bottom = pair.RadiusZero + pair.RadiusOne;

            double x = top / bottom;

            return x;
        }

        private double CalculateY([NotNull] ICirclePair pair)
        {
            double b = pair.Zero.Y;
            double d = pair.One.Y;

            double top = d * pair.RadiusZero + b * pair.RadiusOne;
            double bottom = pair.RadiusZero + pair.RadiusOne;

            double x = top / bottom;

            return x;
        }
    }
}