using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;

namespace Selkie.Geometry.Calculators
{
    public interface IAngleIntervallCalculator
        : ICalculator
    {
        Angle FromAngle { get; set; }
        Angle ToAngle { get; set; }
        int Steps { get; set; }
        Constants.TurnDirection TurnDirection { get; set; }
        Angle Intervall { get; }
    }
}