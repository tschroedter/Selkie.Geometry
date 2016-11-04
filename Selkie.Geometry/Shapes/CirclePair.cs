using System;
using JetBrains.Annotations;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public class CirclePair : ICirclePair
    {
        private CirclePair(bool isUnknown)
        {
            One = Circle.Unknown;
            Zero = Circle.Unknown;
            IsUnknown = isUnknown;
        }

        public CirclePair([NotNull] ICircle zero,
                          [NotNull] ICircle one)
        {
            Distance = Math.Abs(zero.Distance(one));
            Zero = CalculatedCircleZero(zero,
                                        one);
            One = CalculatedCircleOne(zero,
                                      one);
            NumberOfTangents = CalculatedNumberOfTangents(Distance,
                                                          Zero.Radius,
                                                          One.Radius);
        }

        public static readonly ICirclePair Unknown = new CirclePair(true);

        public bool IsUnknown { get; private set; }

        [NotNull]
        private ICircle CalculatedCircleOne([NotNull] ICircle one,
                                            [NotNull] ICircle two)
        {
            return one.Radius > two.Radius
                       ? two
                       : one;
        }

        [NotNull]
        private ICircle CalculatedCircleZero([NotNull] ICircle one,
                                             [NotNull] ICircle two)
        {
            return one.Radius > two.Radius
                       ? one
                       : two;
        }

        // ReSharper disable once MethodTooLong
        private int CalculatedNumberOfTangents(double distance,
                                               double r0,
                                               double r1)
        {
            double r0MinusR1 = r0 - r1;
            double r0PlusR1 = r0 + r1;

            if ( distance < r0MinusR1 )
            {
                return 0;
            }

            if ( Math.Abs(distance - r0MinusR1) < SelkieConstants.EpsilonDistance )
            {
                return 1;
            }

            if ( r0MinusR1 < distance &&
                 distance < r0PlusR1 )
            {
                return 2;
            }

            return Math.Abs(distance - r0PlusR1) < SelkieConstants.EpsilonDistance
                       ? 3
                       : 4;

            // Note: There can't be more than 4 
            //       if (distance > r0PlusR1)
            //         {
            //            return 4;
            //         }
        }

        #region ICirclePair Members

        public ICircle Zero { get; private set; }

        public ICircle One { get; private set; }

        public double RadiusZero
        {
            get
            {
                return Zero.Radius;
            }
        }

        public double RadiusOne
        {
            get
            {
                return One.Radius;
            }
        }

        public int NumberOfTangents { get; private set; }

        public double Distance { get; private set; }

        #endregion
    }
}