using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Surveying
{
    public class SurveyLine : ISurveyLine
    {
        private readonly ILine m_Line;

        public SurveyLine([NotNull] ILine line)
        {
            m_Line = line;
        }

        public bool IsUnknown
        {
            get
            {
                return m_Line.IsUnknown;
            }
        }

        public Angle AngleToXAxisAtStartPoint
        {
            get
            {
                return m_Line.AngleToXAxis;
            }
        }

        public Angle AngleToXAxisAtEndPoint
        {
            get
            {
                return m_Line.AngleToXAxis;
            }
        }

        public Constants.LineDirection RunDirection
        {
            get
            {
                return m_Line.RunDirection;
            }
        }

        public int Id
        {
            get
            {
                return m_Line.Id;
            }
        }

        public double Length
        {
            get
            {
                return m_Line.Length;
            }
        }

        public Point StartPoint
        {
            get
            {
                return m_Line.StartPoint;
            }
        }

        public Point EndPoint
        {
            get
            {
                return m_Line.EndPoint;
            }
        }
    }
}