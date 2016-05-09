using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public class SurveyLine
        : SurveyFeature,
          ISurveyLine
    {
        public SurveyLine([NotNull] ILine line)
            : base(line.Id,
                   line.StartPoint,
                   line.EndPoint,
                   line.AngleToXAxis,
                   line.AngleToXAxis,
                   line.RunDirection,
                   line.Length,
                   line.IsUnknown)
        {
            Line = line;
        }

        public ILine Line { get; }
    }
}