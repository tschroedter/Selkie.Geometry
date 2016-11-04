using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Interfaces.Converters
{
    public interface ISphericalCoordinatesToStringConverter
        : IConverter
    {
        SphericalCoordinates Coordinates { get; set; }
        string String { get; }
    }
}