using System;
using Selkie.Geometry.Primitives;
using Selkie.Windsor;

namespace Selkie.Geometry.Calculators
{
    [ProjectComponent(Lifestyle.Transient)]
    public class AngleIntervallCalculator
        : IAngleIntervallCalculator
    {
        public AngleIntervallCalculator()
        {
            TurnDirection = Constants.TurnDirection.Clockwise;
            Steps = 3;
            MaxAngleInRadians = BaseAngle.RadiansFor360Degrees;
        }

        public double FromAngleInRadians { get; set; }
        public double ToAngleInRadians { get; set; }
        public double MaxAngleInRadians { get; set; }
        public int Steps { get; set; }
        public Constants.TurnDirection TurnDirection { get; set; }

        public void Calculate()
        {
            double abs = Math.Abs(FromAngleInRadians - ToAngleInRadians);

            if ( ( abs < Constants.EpsilonRadians ) ||
                 ( Math.Abs(abs - MaxAngleInRadians) < Constants.EpsilonRadians ) )
            {
                Intervall = Angle.ForZeroDegrees.Radians;
            }
            else
            {
                Intervall = TurnDirection == Constants.TurnDirection.Clockwise
                                ? CalculateIntervallClockwise()
                                : CalculateIntervallCounterClockwise();
            }
        }

        public double Intervall { get; private set; }

        private double CalculateIntervallClockwise()
        {
            double intervallInRadians;

            if ( FromAngleInRadians < ToAngleInRadians )
            {
                intervallInRadians = ( ToAngleInRadians - FromAngleInRadians ) / ( Steps - 1 );
            }
            else
            {
                intervallInRadians = MaxAngleInRadians - ( FromAngleInRadians - ToAngleInRadians );
                intervallInRadians /= Steps - 1;
            }

            return intervallInRadians;
        }

        private double CalculateIntervallCounterClockwise()
        {
            double intervallInRadians;

            if ( FromAngleInRadians < ToAngleInRadians )
            {
                intervallInRadians = MaxAngleInRadians - Math.Abs(FromAngleInRadians - ToAngleInRadians);
                intervallInRadians /= Steps - 1;
            }
            else
            {
                intervallInRadians = Math.Abs(FromAngleInRadians - ToAngleInRadians);
                intervallInRadians /= Steps - 1;
            }

            return intervallInRadians;
        }
    }
}