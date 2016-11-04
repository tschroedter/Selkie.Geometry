using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Calculators
{
    public class SphericalCoordinatesIntervallCalculator
        : ISphericalCoordinatesIntervallCalculator
    {
        public SphericalCoordinatesIntervallCalculator(
            [NotNull] IAngelInterpolationCalculator calculator)
        {
            m_Calculator = calculator;
            Steps = 3;
            TurnDirection = Constants.TurnDirection.Clockwise;
            PhiAngles = new Angle[0];
            ThetaAngles = new Angle[0];
        }

        private readonly IAngelInterpolationCalculator m_Calculator;

        public SphericalCoordinates FromCoordinates { get; set; }
        public SphericalCoordinates ToCoordinates { get; set; }
        public int Steps { get; set; }
        public Constants.TurnDirection TurnDirection { get; set; }
        public IEnumerable <Angle> PhiAngles { get; private set; }
        public IEnumerable <Angle> ThetaAngles { get; private set; }

        public double Radius
        {
            get
            {
                return FromCoordinates.Radius;
            }
        }

        public void Calculate()
        {
            PhiAngles = CalculateIntervallAngles(FromCoordinates.Phi,
                                                 ToCoordinates.Phi);

            ThetaAngles = CalculateIntervallAngles(FromCoordinates.Theta,
                                                   ToCoordinates.Theta);
        }

        internal IEnumerable <Angle> CalculateIntervallAngles(
            Angle fromAngle,
            Angle toAngle)
        {
            m_Calculator.FromAngle = fromAngle;
            m_Calculator.ToAngle = toAngle;
            m_Calculator.Steps = Steps;
            m_Calculator.TurnDirection = TurnDirection;
            m_Calculator.Calculate();

            return m_Calculator.Angles;
        }
    }
}