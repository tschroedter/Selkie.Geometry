using JetBrains.Annotations;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Interfaces.Converters
{
    public interface ICartesianCoordinatesToStringConverter
        : IConverter
    {
        CartesianCoordinates Coordinates { get; set; }

        [NotNull]
        string String { get; }
    }
}