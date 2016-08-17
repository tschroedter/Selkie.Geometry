using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Windsor.Extensions;

namespace Selkie.Geometry.Surveying
{
    public class SurveyFeature : ISurveyFeature
    {
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
                             [NotNull] Point startPoint,
                             [NotNull] Point endPoint,
                             [NotNull] Angle angleToXAxisAtStartPoint,
                             [NotNull] Angle angleToXAxisAtEndPoint,
                             Constants.LineDirection runDirection,
                             double length,
                             bool isUnknown = false,
                             string geoJson = "")
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

        public SurveyFeature([NotNull] SurveyFeatureData data)
        {
            Id = data.Id;
            IsUnknown = data.IsUnknown;
            StartPoint = data.StartPoint;
            EndPoint = data.EndPoint;
            AngleToXAxisAtStartPoint = data.AngleToXAxisAtStartPoint;
            AngleToXAxisAtEndPoint = data.AngleToXAxisAtEndPoint;
            RunDirection = data.RunDirection;
            Length = data.Length;
        }

        internal const int UnknownId = -1;
        internal const double UnknownLength = double.MaxValue;
        public static SurveyFeature Unknown = new SurveyFeature();

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

        public override string ToString()
        {
            string id = "[Id: {0}, IsUnknown: {1}]".Inject(Id,
                                                           IsUnknown);

            string points = "[{0:F2},{1:F2}] - [{2:F2},{3:F2}]".Inject(StartPoint.X,
                                                                       StartPoint.Y,
                                                                       EndPoint.X,
                                                                       EndPoint.Y);

            string angles = "[AngleAtStartPoint:{0:F2}, AngleAtEndPoint:{1:F2}]".Inject(
                                                                                        AngleToXAxisAtStartPoint.Degrees,
                                                                                        AngleToXAxisAtEndPoint.Degrees);

            string length = "[Length:{0:F2}]".Inject(Length);

            string runDirection = "[RunDirection:{0}]".Inject(RunDirection);

            return "{0} {1} {2} {3} {4}".Inject(id,
                                                points,
                                                angles,
                                                length,
                                                runDirection);
        }
    }
}