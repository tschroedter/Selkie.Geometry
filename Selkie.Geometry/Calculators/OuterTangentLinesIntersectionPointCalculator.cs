using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public class OuterTangentLinesIntersectionPointCalculator : IOuterTangentLinesIntersectionPointCalculator
    {
        // ReSharper disable InconsistentNaming
        public static OuterTangentLinesIntersectionPointCalculator Unknown = new OuterTangentLinesIntersectionPointCalculator();

        // ReSharper restore InconsistentNaming
        private readonly bool m_IsUnknown;
        private readonly Point m_Point;

        public OuterTangentLinesIntersectionPointCalculator([NotNull] ICirclePair pair)
        {
            double x = CalculateX(pair);
            double y = CalculateY(pair);

            m_Point = new Point(x,
                                y);
        }

        private OuterTangentLinesIntersectionPointCalculator()
        {
            m_Point = Point.Unknown;
            m_IsUnknown = true;
        }

        private double CalculateX([NotNull] ICirclePair pair)
        {
            double a = pair.Zero.X;
            double c = pair.One.X;

            double top = c * pair.RadiusZero - a * pair.RadiusOne;
            double bottom = pair.RadiusZero - pair.RadiusOne;

            double x = top / bottom;

            return x;
        }

        private double CalculateY([NotNull] ICirclePair pair)
        {
            double b = pair.Zero.Y;
            double d = pair.One.Y;

            double top = d * pair.RadiusZero - b * pair.RadiusOne;
            double bottom = pair.RadiusZero - pair.RadiusOne;

            double x = top / bottom;

            return x;
        }

        #region IOuterTangentLinesIntersectionPointCalculator Members

        public bool IsUnknown
        {
            get
            {
                return m_IsUnknown;
            }
        }

        public Point IntersectionPoint
        {
            get
            {
                return m_Point;
            }
        }

        #endregion
    }
}