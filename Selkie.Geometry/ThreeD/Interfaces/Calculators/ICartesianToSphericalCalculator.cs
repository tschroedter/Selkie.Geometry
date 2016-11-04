using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Interfaces.Calculators
{
    public interface ICartesianToSphericalCalculator
        : ICalculator
    {
        CartesianCoordinates CartesianCoordinates { get; set; }
        SphericalCoordinates SphericalCoordinates { get; }
    }
}