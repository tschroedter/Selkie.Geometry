using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Interfaces.Calculators
{
    public interface ICartesianSphericalArcCalculator
        : ICalculator
    {
        CartesianCoordinates FromCoordinates { get; set; }
        CartesianCoordinates ToCoordinates { get; set; }

        [NotNull]
        IEnumerable <CartesianCoordinates> CartesianCoordinates { get; }

        Constants.TurnDirection TurnDirection { get; set; }
        int Steps { get; set; }
    }
}