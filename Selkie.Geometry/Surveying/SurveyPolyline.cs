using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public class SurveyPolyline
        : SurveyFeature,
          ISurveyPolyline
    {
        public SurveyPolyline([NotNull] IPolyline polyline)
            : base(polyline.Id,
                   polyline.StartPoint,
                   polyline.EndPoint,
                   polyline.AngleToXAxisAtStartPoint,
                   polyline.AngleToXAxisAtEndPoint,
                   polyline.RunDirection,
                   polyline.Length,
                   polyline.IsUnknown)
        {
            Polyline = polyline;
        }

        public IPolyline Polyline { get; }
    }
}