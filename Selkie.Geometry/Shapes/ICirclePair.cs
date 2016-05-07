using JetBrains.Annotations;

namespace Selkie.Geometry.Shapes
{
    public interface ICirclePair : IShape
    {
        [NotNull]
        ICircle Zero { get; }

        [NotNull]
        ICircle One { get; }

        double RadiusZero { get; }
        double RadiusOne { get; }
        int NumberOfTangents { get; }
        double Distance { get; }
        bool IsUnknown { get; }
    }
}