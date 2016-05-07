using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public interface ISurveyPolyline : ISurveyFeature
    {
        [NotNull]
        IPolyline Polyline { get; }
    }
}