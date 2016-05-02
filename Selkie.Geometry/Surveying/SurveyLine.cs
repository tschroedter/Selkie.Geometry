using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public class SurveyLine : ISurveyLine
    {
        public SurveyLine([NotNull] ILine line)
        {
            IsUnknown = line.IsUnknown;
            StartPoint = new Point(line.StartPoint.X,
                                   line.StartPoint.Y);
            EndPoint = new Point(line.EndPoint.X,
                                 line.EndPoint.Y);
            AngleToXAxisAtStartPoint = line.AngleToXAxis;
            AngleToXAxisAtEndPoint = line.AngleToXAxis;
            RunDirection = line.RunDirection;
            Id = line.Id;
            Length = line.Length;
        }

        public bool IsUnknown { get; private set; }
        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }
        public Angle AngleToXAxisAtStartPoint { get; private set; }
        public Angle AngleToXAxisAtEndPoint { get; private set; }
        public Constants.LineDirection RunDirection { get; private set; }
        public int Id { get; private set; }
        public double Length { get; private set; }
    }
}