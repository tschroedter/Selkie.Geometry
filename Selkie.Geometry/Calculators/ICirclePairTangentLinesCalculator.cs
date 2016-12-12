using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public interface ICirclePairTangentLinesCalculator
        : ICalculator // todo change other calculators to support ICalculator
    {
        [NotNull]
        IEnumerable <ILine> OuterTangents { get; }

        [NotNull]
        IEnumerable <ILine> InnerTangents { get; }

        [NotNull]
        IEnumerable <ILine> Tangents { get; }

        [NotNull]
        ICirclePair CirclePair { get; set; }
    }
}