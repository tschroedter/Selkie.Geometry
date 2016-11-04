using System.Collections.Generic;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;

namespace Selkie.Geometry.Calculators
{
    public interface IAngelInterpolationCalculator
        : ICalculator
    {
        Angle FromAngle { get; set; }
        Angle ToAngle { get; set; }
        IEnumerable <Angle> Angles { get; }
        int Steps { get; set; }
        Angle Intervall { get; }
        Constants.TurnDirection TurnDirection { get; set; }
    }
}