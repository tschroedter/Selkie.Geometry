using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public interface ILineDirectionCalculator
    {
        ILine Line { get; set; }
        Point Point { get; set; }
        Constants.TurnDirection Direction { get; }
        void Calculate();
    }
}