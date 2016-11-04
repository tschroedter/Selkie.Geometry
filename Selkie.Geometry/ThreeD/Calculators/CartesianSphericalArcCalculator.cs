using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.Windsor;

namespace Selkie.Geometry.ThreeD.Calculators
{
    [ProjectComponent(Lifestyle.Transient)]
    public class CartesianSphericalArcCalculator
        : ICartesianSphericalArcCalculator
    {
        public CartesianSphericalArcCalculator(
            [NotNull] ICartesianToSphericalCalculator cartesianToSphericalCalculator,
            [NotNull] ISphericalToCartesianCalculator sphericalToCartesianCalculator,
            [NotNull] ISphericalArcCalculator sphericalArcSphericalArcCalculator)
        {
            m_CartesianToSphericalCalculator = cartesianToSphericalCalculator;
            m_SphericalToCartesianCalculator = sphericalToCartesianCalculator;
            m_SphericalArcCalculator = sphericalArcSphericalArcCalculator;

            Steps = 3;
            TurnDirection = Constants.TurnDirection.Clockwise;
            CartesianCoordinates = new CartesianCoordinates[0];
        }

        private readonly ICartesianToSphericalCalculator m_CartesianToSphericalCalculator;
        private readonly ISphericalArcCalculator m_SphericalArcCalculator;
        private readonly ISphericalToCartesianCalculator m_SphericalToCartesianCalculator;

        public CartesianCoordinates FromCoordinates { get; set; }
        public CartesianCoordinates ToCoordinates { get; set; }
        public IEnumerable <CartesianCoordinates> CartesianCoordinates { get; private set; }
        public Constants.TurnDirection TurnDirection { get; set; }
        public int Steps { get; set; } // todo should not be negative, same with radius

        public void Calculate()
        {
            SphericalCoordinates fromSphericalCoordinates = ToSpherical(FromCoordinates);
            SphericalCoordinates toSphericalCoordinates = ToSpherical(ToCoordinates);

            m_SphericalArcCalculator.FromCoordinates = fromSphericalCoordinates;
            m_SphericalArcCalculator.ToCoordinates = toSphericalCoordinates;
            m_SphericalArcCalculator.TurnDirection = TurnDirection;
            m_SphericalArcCalculator.Steps = Steps;
            m_SphericalArcCalculator.Calculate();

            CartesianCoordinates = ToCartesianCoordinates(m_SphericalArcCalculator.SphericalCoordinates);
        }

        private IEnumerable <CartesianCoordinates> ToCartesianCoordinates(
            [NotNull] IEnumerable <SphericalCoordinates> sphericalCoordinates)
        {
            var list = new List <CartesianCoordinates>();

            foreach ( SphericalCoordinates sphericalCoordinate in sphericalCoordinates )
            {
                m_SphericalToCartesianCalculator.SphericalCoordinates = sphericalCoordinate;
                m_SphericalToCartesianCalculator.Calculate();

                list.Add(m_SphericalToCartesianCalculator.CartesianCoordinates);
            }

            return list;
        }

        private SphericalCoordinates ToSpherical(CartesianCoordinates cartesianCoordinates)
        {
            m_CartesianToSphericalCalculator.CartesianCoordinates = cartesianCoordinates;
            m_CartesianToSphericalCalculator.Calculate();

            return m_CartesianToSphericalCalculator.SphericalCoordinates;
        }
    }
}