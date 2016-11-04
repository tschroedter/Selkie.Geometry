using Selkie.Geometry.ThreeD.Shapes;

namespace Selkie.Geometry.ThreeD.Interfaces.Calculators
{
    public interface IPointDistanceCalculator
        : ICalculator
    {
        Point FromPoint { get; set; }
        Point ToPoint { get; set; }
        double Distance { get; }
    }
}