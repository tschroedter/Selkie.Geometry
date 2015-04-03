using System;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public interface ICoordinatePairCalculator
    {
        [NotNull]
        ICircle Circle { get; }

        [NotNull]
        Tuple <double, double> Xt1And2 { get; }

        [NotNull]
        Tuple <double, double> Yt1And2 { get; }

        [NotNull]
        Tuple <Point, Point> Points { get; }
    }
}