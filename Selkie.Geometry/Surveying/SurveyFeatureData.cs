using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public class SurveyFeatureData : ISurveyFeatureData
    {
        public SurveyFeatureData(
            int id,
            [NotNull] Point startPoint,
            [NotNull] Point endPoint,
            [NotNull] Angle angleToXAxisAtStartPoint,
            [NotNull] Angle angleToXAxisAtEndPoint,
            Constants.LineDirection runDirection,
            double length,
            bool isUnknown)
        {
            Id = id;
            StartPoint = startPoint;
            EndPoint = endPoint;
            AngleToXAxisAtStartPoint = angleToXAxisAtStartPoint;
            AngleToXAxisAtEndPoint = angleToXAxisAtEndPoint;
            RunDirection = runDirection;
            Length = length;
            IsUnknown = isUnknown;
        }

        public int Id { get; private set; }
        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }
        public Angle AngleToXAxisAtStartPoint { get; private set; }
        public Angle AngleToXAxisAtEndPoint { get; private set; }
        public Constants.LineDirection RunDirection { get; private set; }
        public double Length { get; private set; }
        public bool IsUnknown { get; private set; }
    }
}