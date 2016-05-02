using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public interface ISurveyFeature
    {
        bool IsUnknown { get; }

        Point StartPoint { get; }
        Point EndPoint { get; }

        [NotNull]
        Angle AngleToXAxisAtStartPoint { get; }

        [NotNull]
        Angle AngleToXAxisAtEndPoint { get; }

        Constants.LineDirection RunDirection { get; }

        int Id { get; }

        double Length { get; }
    }
}