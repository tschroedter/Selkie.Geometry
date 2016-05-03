using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public class SurveyFeature : ISurveyFeature
    {
        internal const int UnknownId = -1;
        internal const double UnknownLength = double.MaxValue;
        public static SurveyFeature Unknown = new SurveyFeature();

        private SurveyFeature()
        {
            IsUnknown = true;
            StartPoint = Point.Unknown;
            EndPoint = Point.Unknown;
            AngleToXAxisAtStartPoint = Angle.Unknown;
            AngleToXAxisAtEndPoint = Angle.Unknown;
            RunDirection = Constants.LineDirection.Unknown;
            Id = UnknownId;
            Length = UnknownLength;
        }

        public SurveyFeature(int id,
                             Point startPoint,
                             Point endPoint,
                             Angle angleToXAxisAtStartPoint,
                             Angle angleToXAxisAtEndPoint,
                             Constants.LineDirection runDirection,
                             double length,
                             bool isUnknown = false)
        {
            Id = id;
            IsUnknown = isUnknown;
            StartPoint = startPoint;
            EndPoint = endPoint;
            AngleToXAxisAtStartPoint = angleToXAxisAtStartPoint;
            AngleToXAxisAtEndPoint = angleToXAxisAtEndPoint;
            RunDirection = runDirection;
            Length = length;
        }

        public bool IsUnknown { get; private set; }
        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }
        public Angle AngleToXAxisAtStartPoint { get; private set; }
        public Angle AngleToXAxisAtEndPoint { get; private set; }
        public Constants.LineDirection RunDirection { get; private set; }
        public int Id { get; private set; }
        public double Length { get; private set; }

        public ISurveyFeature Reverse()
        {
            if ( IsUnknown )
            {
                return this;
            }

            Angle angleToXAxisAtStartPoint =
                Angle.FromDegrees(AngleToXAxisAtStartPoint.Degrees + Angle.For180Degrees.Degrees);
            Angle angleToXAxisAtEndPoint =
                Angle.FromDegrees(AngleToXAxisAtEndPoint.Degrees + Angle.For180Degrees.Degrees);

            return new SurveyFeature(Id,
                                     EndPoint,
                                     StartPoint,
                                     angleToXAxisAtStartPoint,
                                     angleToXAxisAtEndPoint,
                                     RunDirection,
                                     Length,
                                     IsUnknown);
        }
    }
}