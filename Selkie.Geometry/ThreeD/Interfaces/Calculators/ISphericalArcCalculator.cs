using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Interfaces.Calculators
{
    public interface ISphericalArcCalculator
        : ICalculator
    {
        SphericalCoordinates FromCoordinates { get; set; }
        SphericalCoordinates ToCoordinates { get; set; }

        [NotNull]
        IEnumerable <SphericalCoordinates> SphericalCoordinates { get; }

        Constants.TurnDirection TurnDirection { get; set; }
        int Steps { get; set; }
    }
}