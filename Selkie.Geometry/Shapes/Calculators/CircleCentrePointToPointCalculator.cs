using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes.Calculators
{
    public class CircleCentrePointToPointCalculator : ICircleCentrePointToPointCalculator
    {
        public CircleCentrePointToPointCalculator([NotNull] Point centrePoint,
                                                  [NotNull] Point startPoint,
                                                  [NotNull] Point endPoint)
        {
            m_CentrePoint = centrePoint;
            m_StartPoint = startPoint;
            m_EndPoint = endPoint;

            if ( ( m_CentrePoint == Point.Unknown ) ||
                 ( m_StartPoint == Point.Unknown ) ||
                 ( m_EndPoint == Point.Unknown ) )
            {
                return;
            }

            m_AngleRelativeToYAxisCounterClockwise = RadiansBetweenPointsCounterClockwise(centrePoint,
                                                                                          startPoint,
                                                                                          endPoint);
            m_AngleRelativeToYAxisClockwise = Angle.Inverse(m_AngleRelativeToYAxisCounterClockwise);
        }

        private readonly Angle m_AngleRelativeToYAxisClockwise;
        private readonly Angle m_AngleRelativeToYAxisCounterClockwise;
        private readonly Point m_CentrePoint;
        private readonly Point m_EndPoint;
        private readonly Point m_StartPoint;

        [NotNull]
        internal Angle RadiansBetweenPointsCounterClockwise([NotNull] Point centrePoint,
                                                            [NotNull] Point startPoint,
                                                            [NotNull] Point endPoint)
        {
            ICircleCentreToPointCalculator calculatorStart = new CircleCentreToPointCalculator(centrePoint,
                                                                                               startPoint);
            ICircleCentreToPointCalculator calculatorEnd = new CircleCentreToPointCalculator(centrePoint,
                                                                                             endPoint);

            Angle angleStart = calculatorStart.AngleRelativeToYAxisCounterclockwise;
            Angle angleEnd = calculatorEnd.AngleRelativeToYAxisCounterclockwise;

            Angle angle = angleEnd - angleStart;

            return angle;
        }

        #region ICircleCentrePointToPointCalculator Members

        public Point CentrePoint
        {
            get
            {
                return m_CentrePoint;
            }
        }

        public Point StartPoint
        {
            get
            {
                return m_StartPoint;
            }
        }

        public Point EndPoint
        {
            get
            {
                return m_EndPoint;
            }
        }

        public Angle AngleRelativeToYAxisClockwise
        {
            get
            {
                return m_AngleRelativeToYAxisClockwise;
            }
        }

        public Angle AngleRelativeToYAxisCounterClockwise
        {
            get
            {
                return m_AngleRelativeToYAxisCounterClockwise;
            }
        }

        #endregion
    }
}