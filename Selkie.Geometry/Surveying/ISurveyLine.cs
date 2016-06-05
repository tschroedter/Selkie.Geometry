using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public interface ISurveyLine : ISurveyFeature
    {
        [NotNull]
        ILine Line { get; }
    }
}