using Selkie.Geometry.ThreeD.Interfaces.Converters;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Geometry.ThreeD.Converters
{
    [ProjectComponent(Lifestyle.Transient)]
    public class CartesianCoordinatesToStringConverter
        : ICartesianCoordinatesToStringConverter
    {
        public CartesianCoordinatesToStringConverter()
        {
            String = string.Empty;
        }

        public CartesianCoordinates Coordinates { get; set; }

        public string String { get; private set; }

        public void Convert()
        {
            String = "[{0},{1},{2}]".Inject(Coordinates.X,
                                            Coordinates.Y,
                                            Coordinates.Z);
        }
    }
}