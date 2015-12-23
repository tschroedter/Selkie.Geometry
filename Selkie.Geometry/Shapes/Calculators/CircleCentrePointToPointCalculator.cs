﻿using JetBrains.Annotations;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.Shapes.Calculators
{
    public class CircleCentrePointToPointCalculator : ICircleCentrePointToPointCalculator
    {
        private readonly Angle m_AngleRelativeToYAxisClockwise;
        private readonly Point m_CentrePoint;
        private readonly Point m_EndPoint;
        private readonly Angle m_AngleRelativeToYAxisCounterClockwise;
        private readonly Point m_StartPoint;

        public CircleCentrePointToPointCalculator([NotNull] Point centrePoint,
                                                  [NotNull] Point startPoint,
                                                  [NotNull] Point endPoint)
        {
            m_CentrePoint = centrePoint;
            m_StartPoint = startPoint;
            m_EndPoint = endPoint;

            if ( m_CentrePoint == Point.Unknown ||
                 m_StartPoint == Point.Unknown ||
                 m_EndPoint == Point.Unknown )
            {
                return;
            }

            m_AngleRelativeToYAxisCounterClockwise = RadiansBetweenPointsCounterClockwise(centrePoint,
                                                                                          startPoint,
                                                                                          endPoint);
            m_AngleRelativeToYAxisClockwise = Angle.Inverse(m_AngleRelativeToYAxisCounterClockwise);
        }

        [NotNull]
        internal Angle RadiansBetweenPointsCounterClockwise([NotNull] Point centrePoint,
                                                            [NotNull] Point startPoint,
                                                            [NotNull] Point endPoint)
        {
            ICircleCentreToPointCalculator calculatorOne = new CircleCentreToPointCalculator(centrePoint,
                                                                                             startPoint);
            ICircleCentreToPointCalculator calculatorTwo = new CircleCentreToPointCalculator(centrePoint,
                                                                                             endPoint);

            Angle radiansOne = calculatorOne.AngleRelativeToYAxisCounterclockwise;
            Angle radiansTwo = calculatorTwo.AngleRelativeToYAxisCounterclockwise;

            Angle angle = radiansTwo - radiansOne;

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