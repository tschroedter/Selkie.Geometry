using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Interfaces.Converters
{
    public interface IRadiusPhiThetaToSphericalCoordinatesConverter
        : IConverter
    {
        double Radius { get; set; }

        [NotNull]
        IEnumerable <Angle> PhiAngles { get; set; }

        [NotNull]
        IEnumerable <Angle> ThetaAngles { get; set; }

        [NotNull]
        IEnumerable <SphericalCoordinates> SphericalCoordinates { get; }
    }
}