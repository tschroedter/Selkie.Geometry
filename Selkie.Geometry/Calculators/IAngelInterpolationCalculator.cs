using System.Collections.Generic;

namespace Selkie.Geometry.Calculators
{
    public interface IAngelInterpolationCalculator
        : ICalculator
    {
        double FromAngleInRadians { get; set; }
        double ToAngleInRadians { get; set; }
        IEnumerable <double> AnglesInRadians { get; }
        int Steps { get; set; }
        double IntervallInRadians { get; }
        Constants.TurnDirection TurnDirection { get; set; }
        double MaxAngleInRadians { get; set; }
    }
}