using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;
using Selkie.Geometry.ThreeD.Interfaces.Converters;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.Windsor;

namespace Selkie.Geometry.ThreeD.Calculators
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SphericalArcCalculator
        : ISphericalArcCalculator
    {
        public SphericalArcCalculator(
            [NotNull] ISphericalCoordinatesIntervallCalculator calculator,
            [NotNull] IRadiusPhiThetaToSphericalCoordinatesConverter converter)
        {
            m_Calculator = calculator;
            m_Converter = converter;
            Steps = 3;
            TurnDirection = Constants.TurnDirection.Clockwise;
            SphericalCoordinates = new SphericalCoordinates[0];
        }

        internal const double Tolerance = 1E-6;
        private readonly ISphericalCoordinatesIntervallCalculator m_Calculator;
        private readonly IRadiusPhiThetaToSphericalCoordinatesConverter m_Converter;
        public int Steps { get; set; }
        public Constants.TurnDirection TurnDirection { get; set; }

        public SphericalCoordinates FromCoordinates { get; set; }
        public SphericalCoordinates ToCoordinates { get; set; }
        public IEnumerable <SphericalCoordinates> SphericalCoordinates { get; private set; }

        public void Calculate()
        {
            Validate();

            SphericalCoordinates = CalculateSphericalCoordinates();
        }

        private IEnumerable <SphericalCoordinates> CalculateSphericalCoordinates()
        {
            m_Calculator.Steps = Steps;
            m_Calculator.FromCoordinates = FromCoordinates;
            m_Calculator.ToCoordinates = ToCoordinates;
            m_Calculator.TurnDirection = TurnDirection;
            m_Calculator.Calculate();

            m_Converter.Radius = m_Calculator.Radius;
            m_Converter.PhiAngles = m_Calculator.PhiAngles;
            m_Converter.ThetaAngles = m_Calculator.ThetaAngles;
            m_Converter.Convert();

            return m_Converter.SphericalCoordinates;
        }

        private void Validate()
        {
            if ( Math.Abs(FromCoordinates.Radius - ToCoordinates.Radius) > Tolerance )
            {
                throw new ArgumentException("Radius of given points must be equal!");
            }
        }
    }
}