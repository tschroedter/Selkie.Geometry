using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Geometry.Calculators
{
    [ProjectComponent(Lifestyle.Transient)]
    public class AngelInterpolationCalculator
        : IAngelInterpolationCalculator
    {
        public AngelInterpolationCalculator(
            [NotNull] IAngleIntervallCalculator angleIntervallCalculator)
        {
            m_AngleIntervallCalculator = angleIntervallCalculator;
            FromAngle = Angle.Unknown;
            ToAngle = Angle.Unknown;
            TurnDirection = Constants.TurnDirection.Clockwise;
        }

        private readonly IAngleIntervallCalculator m_AngleIntervallCalculator;

        public Constants.TurnDirection TurnDirection { get; set; }

        public Angle FromAngle { get; set; }
        public Angle ToAngle { get; set; }
        public Angle Intervall { get; private set; }
        public IEnumerable <Angle> Angles { get; private set; }
        public int Steps { get; set; }

        public void Calculate()
        {
            ValidateSteps();

            Intervall = CalculateIntervallAngle();
            Angles = CalculateIntervallAngles();
        }

        private Angle CalculateIntervallAngle()
        {
            m_AngleIntervallCalculator.FromAngle = FromAngle;
            m_AngleIntervallCalculator.ToAngle = ToAngle;
            m_AngleIntervallCalculator.Steps = Steps;
            m_AngleIntervallCalculator.TurnDirection = TurnDirection;
            m_AngleIntervallCalculator.Calculate();

            return m_AngleIntervallCalculator.Intervall;
        }

        private IEnumerable <Angle> CalculateIntervallAngles()
        {
            var angles = new List <Angle>
                         {
                             FromAngle
                         };

            IEnumerable <Angle> intervallAngels =
                TurnDirection == Constants.TurnDirection.Clockwise
                    ? Clockwise()
                    : CounterClockwise();

            angles.AddRange(intervallAngels);

            angles.Add(ToAngle);

            return angles;
        }

        private IEnumerable <Angle> Clockwise()
        {
            var angles = new List <Angle>();

            Angle intervallAngle = FromAngle + Intervall;

            for ( var i = 1 ; i < Steps - 1 ; i++, intervallAngle += Intervall )
            {
                angles.Add(intervallAngle);
            }

            return angles;
        }

        private IEnumerable <Angle> CounterClockwise()
        {
            var angles = new List <Angle>();

            Angle intervallAngle = FromAngle - Intervall;

            for ( var i = 1 ; i < Steps - 1 ; i++, intervallAngle -= Intervall )
            {
                angles.Add(intervallAngle);
            }

            return angles;
        }

        private void ValidateSteps()
        {
            if ( Steps < 3 )
            {
                throw new ArgumentException("Steps is {0} but must be greater than 2!".Inject(Steps));
            }
        }
    }
}