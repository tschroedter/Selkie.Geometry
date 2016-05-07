using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public interface ISurveyLine : ISurveyFeature
    {
        ILine Line { get; }
    }
}