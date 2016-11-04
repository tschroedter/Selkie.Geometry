using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Interfaces.Calculators
{
    public interface ISphericalToCartesianCalculator
        : ICalculator
    {
        CartesianCoordinates CartesianCoordinates { get; }
        SphericalCoordinates SphericalCoordinates { get; set; }
    }
}