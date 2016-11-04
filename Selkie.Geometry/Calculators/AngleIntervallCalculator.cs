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
            FromAngle = Angle.Unknown;
            ToAngle = Angle.Unknown;
            Intervall = Angle.Unknown;
            TurnDirection = Constants.TurnDirection.Clockwise;
            Steps = 3;
        }

        public Angle FromAngle { get; set; }
        public Angle ToAngle { get; set; }
        public int Steps { get; set; }
        public Constants.TurnDirection TurnDirection { get; set; }

        public void Calculate()
        {
            if ( FromAngle == ToAngle )
            {
                Intervall = Angle.ForZeroDegrees;
            }
            else
            {
                Intervall = TurnDirection == Constants.TurnDirection.Clockwise
                                ? CalculateIntervallClockwise()
                                : CalculateIntervallCounterClockwise();
            }
        }

        public Angle Intervall { get; private set; }

        private Angle CalculateIntervallClockwise()
        {
            double intervallInRadians;

            if ( FromAngle < ToAngle )
            {
                intervallInRadians = ( ToAngle - FromAngle ).Radians / ( Steps - 1 );
            }
            else
            {
                intervallInRadians = Angle.RadiansFor360Degrees - ( FromAngle - ToAngle ).Radians;
                intervallInRadians /= Steps - 1;
            }

            return Angle.FromRadians(intervallInRadians);
        }

        private Angle CalculateIntervallCounterClockwise()
        {
            double intervallInRadians;

            if ( FromAngle < ToAngle )
            {
                intervallInRadians = ( FromAngle - ToAngle ).Radians;
                intervallInRadians /= Steps - 1;
            }
            else
            {
                intervallInRadians = Angle.RadiansFor360Degrees - ( ToAngle - FromAngle ).Radians;
                intervallInRadians /= Steps - 1;
            }

            return Angle.FromRadians(intervallInRadians);
        }
    }
}