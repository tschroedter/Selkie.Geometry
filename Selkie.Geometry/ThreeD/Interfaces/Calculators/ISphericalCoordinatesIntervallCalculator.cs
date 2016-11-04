using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Interfaces.Calculators
{
    public interface ISphericalCoordinatesIntervallCalculator
        : ICalculator
    {
        SphericalCoordinates FromCoordinates { get; set; }
        SphericalCoordinates ToCoordinates { get; set; }
        int Steps { get; set; }
        Constants.TurnDirection TurnDirection { get; set; }

        [NotNull]
        IEnumerable <Angle> PhiAngles { get; }

        [NotNull]
        IEnumerable <Angle> ThetaAngles { get; }

        double Radius { get; }
    }
}