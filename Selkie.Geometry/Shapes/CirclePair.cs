using System;
using JetBrains.Annotations;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public class CirclePair : ICirclePair
    {
        public static readonly ICirclePair Unknown = new CirclePair(true);

        private readonly double m_Distance;
        private readonly bool m_IsUnknown;
        private readonly int m_NumberOfTangents;
        private readonly ICircle m_One = Circle.Unknown;
        private readonly ICircle m_Zero = Circle.Unknown;

        private CirclePair(bool isUnknown)
        {
            m_IsUnknown = isUnknown;
        }

        public CirclePair([NotNull] ICircle zero,
                          [NotNull] ICircle one)
        {
            m_Distance = Math.Abs(zero.Distance(one));
            m_Zero = CalculatedCircleZero(zero, one);
            m_One = CalculatedCircleOne(zero, one);
            m_NumberOfTangents = CalculatedNumberOfTangents(m_Distance,
                                                            m_Zero.Radius,
                                                            m_One.Radius);
        }

        #region ICirclePair Members

        public ICircle Zero
        {
            get { return m_Zero; }
        }

        public ICircle One
        {
            get { return m_One; }
        }

        public double RadiusZero
        {
            get { return m_Zero.Radius; }
        }

        public double RadiusOne
        {
            get { return m_One.Radius; }
        }

        public int NumberOfTangents
        {
            get { return m_NumberOfTangents; }
        }

        public double Distance
        {
            get { return m_Distance; }
        }

        #endregion

        public bool IsUnknown
        {
            get { return m_IsUnknown; }
        }

        [NotNull]
        private ICircle CalculatedCircleZero([NotNull] ICircle one,
                                             [NotNull] ICircle two)
        {
            return one.Radius > two.Radius ? one : two;
        }

        [NotNull]
        private ICircle CalculatedCircleOne([NotNull] ICircle one,
                                            [NotNull] ICircle two)
        {
            return one.Radius > two.Radius ? two : one;
        }

        // ReSharper disable once MethodTooLong
        private int CalculatedNumberOfTangents(double distance,
                                               double r0,
                                               double r1)
        {
            double r0MinusR1 = r0 - r1;
            double r0PlusR1 = r0 + r1;

            if (distance < r0MinusR1)
            {
                return 0;
            }

            if (Math.Abs(distance - r0MinusR1) < SelkieConstants.EpsilonDistance)
            {
                return 1;
            }

            if (r0MinusR1 < distance &&
                distance < r0PlusR1)
            {
                return 2;
            }

            if (Math.Abs(distance - r0PlusR1) < SelkieConstants.EpsilonDistance)
            {
                return 3;
            }

            // Note: There can't be more than 4 
            //       if (distance > r0PlusR1)
            //         {
            //            return 4;
            //         }

            return 4;
        }
    }
}