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
            TurnDirection = Constants.TurnDirection.Clockwise;
            MaxAngleInRadians = BaseAngle.RadiansFor360Degrees; // todo testing
        }

        private readonly IAngleIntervallCalculator m_AngleIntervallCalculator;

        public Constants.TurnDirection TurnDirection { get; set; }

        public double FromAngleInRadians { get; set; }
        public double ToAngleInRadians { get; set; }
        public double IntervallInRadians { get; private set; }
        public IEnumerable <double> AnglesInRadians { get; private set; }
        public int Steps { get; set; }
        public double MaxAngleInRadians { get; set; }

        public void Calculate()
        {
            ValidateSteps();

            IntervallInRadians = CalculateIntervallAngle();
            AnglesInRadians = CalculateIntervallAngles();
        }

        private double CalculateIntervallAngle()
        {
            m_AngleIntervallCalculator.FromAngleInRadians = FromAngleInRadians;
            m_AngleIntervallCalculator.ToAngleInRadians = ToAngleInRadians;
            m_AngleIntervallCalculator.Steps = Steps;
            m_AngleIntervallCalculator.TurnDirection = TurnDirection;
            m_AngleIntervallCalculator.MaxAngleInRadians = MaxAngleInRadians;
            m_AngleIntervallCalculator.Calculate();

            return m_AngleIntervallCalculator.Intervall;
        }

        private IEnumerable <double> CalculateIntervallAngles()
        {
            var angles = new List <double>
                         {
                             FromAngleInRadians
                         };

            IEnumerable <double> intervallAngels =
                TurnDirection == Constants.TurnDirection.Clockwise
                    ? Clockwise()
                    : CounterClockwise();

            angles.AddRange(intervallAngels);

            angles.Add(ToAngleInRadians);

            return angles;
        }

        private IEnumerable <double> Clockwise()
        {
            var angles = new List <double>();

            double intervallAngle = FromAngleInRadians + IntervallInRadians;

            for ( var i = 1 ; i < Steps - 1 ; i++, intervallAngle += IntervallInRadians )
                angles.Add(intervallAngle);

            return angles;
        }

        private IEnumerable <double> CounterClockwise()
        {
            var angles = new List <double>();

            double intervallAngle = FromAngleInRadians - IntervallInRadians;

            for ( var i = 1 ; i < Steps - 1 ; i++, intervallAngle -= IntervallInRadians )
            {
                if ( intervallAngle < 0.0 )
                {
                    intervallAngle += MaxAngleInRadians;
                }

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