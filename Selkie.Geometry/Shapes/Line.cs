using System;
using JetBrains.Annotations;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Primitives;
using Selkie.Windsor.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public class Line : ILine, IEquatable<Line>
    {
        public const int UnknownId = int.MinValue;
        public static readonly Line Unknown = new Line();

        private readonly Angle m_AngleToXAxis;
        private readonly Point m_EndPoint;
        private readonly int m_Id;
        private readonly bool m_IsUnknown;
        private readonly double m_Length;
        private readonly Constants.LineDirection m_LineDirection;
        private readonly Point m_StartPoint;

        private Line()
            : this(UnknownId,
                   new Point(double.MaxValue, double.MaxValue),
                   new Point(double.MaxValue, double.MaxValue),
                   Constants.LineDirection.Unknown,
                   true)
        {
        }

        // ReSharper disable once TooManyDependencies
        public Line(int id,
                    double x1,
                    double y1,
                    double x2,
                    double y2,
                    bool isUnknown = false,
                    Constants.LineDirection runDirection = Constants.LineDirection.Forward)
            : this(id, new Point(x1, y1), new Point(x2, y2), runDirection, isUnknown)
        {
        }

        // ReSharper disable once TooManyDependencies
        public Line(double x1,
                    double y1,
                    double x2,
                    double y2)
            : this(UnknownId, new Point(x1, y1), new Point(x2, y2), Constants.LineDirection.Forward, false)
        {
        }

        public Line(int id,
                    [NotNull] Point startPoint,
                    [NotNull] Point endPoint)
            : this(id, startPoint, endPoint, Constants.LineDirection.Forward, false)
        {
        }

        public Line([NotNull] Point startPoint,
                    [NotNull] Point endPoint)
            : this(UnknownId, startPoint, endPoint, Constants.LineDirection.Forward, false)
        {
        }

        // ReSharper disable once TooManyDependencies
        public Line(int id,
                    [NotNull] Point startPoint,
                    [NotNull] Point endPoint,
                    Constants.LineDirection lineDirection)
            : this(id, startPoint, endPoint, lineDirection, false)
        {
        }

        // ReSharper disable once TooManyDependencies
        public Line([NotNull] Point startPoint,
                    [NotNull] Point endPoint,
                    Constants.LineDirection lineDirection)
            : this(UnknownId, startPoint, endPoint, lineDirection, false)
        {
        }

        // ReSharper disable once TooManyDependencies
        public Line([NotNull] Point startPoint,
                    [NotNull] Point endPoint,
                    Constants.LineDirection lineDirection,
                    bool isUnknown)
            : this(UnknownId, startPoint, endPoint, lineDirection, isUnknown)
        {
        }

        // ReSharper disable once TooManyDependencies
        internal Line(int id,
                      [NotNull] Point startPoint,
                      [NotNull] Point endPoint,
                      Constants.LineDirection lineDirection,
                      bool isUnknown)
        {
            m_Id = id;
            m_StartPoint = startPoint;
            m_EndPoint = endPoint;
            m_IsUnknown = isUnknown;
            m_LineDirection = lineDirection;
            m_Length = CalculateLength();
            m_AngleToXAxis = CalculateAngleInRadiansRelativeToXAxis(startPoint, endPoint, lineDirection);
        }

        #region IEquatable<Line> Members

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(Line other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.m_EndPoint, m_EndPoint) && Equals(other.m_StartPoint, m_StartPoint) &&
                   Equals(other.m_LineDirection, m_LineDirection) && other.m_IsUnknown.Equals(m_IsUnknown);
        }

        #endregion

        #region ILine Members

        public int Id
        {
            get { return m_Id; }
        }

        public Constants.LineDirection RunDirection
        {
            get { return m_LineDirection; }
        }

        public Angle AngleToXAxis
        {
            get { return m_AngleToXAxis; }
        }

        public Point StartPoint
        {
            get { return m_StartPoint; }
        }

        public Point EndPoint
        {
            get { return m_EndPoint; }
        }

        public double Length
        {
            get { return m_Length; }
        }

        public IPolylineSegment Reverse()
        {
            var reverse = new Line(m_Id, m_EndPoint, m_StartPoint);

            return reverse;
        }

        public double X1
        {
            get { return StartPoint.X; }
        }

        public double Y1
        {
            get { return StartPoint.Y; }
        }

        public double X2
        {
            get { return EndPoint.X; }
        }

        public double Y2
        {
            get { return EndPoint.Y; }
        }

        public Constants.TurnDirection TurnDirection(Point point)
        {
            var calculator = new LineDirectionCalculator(this, point);

            return calculator.Direction;
        }

        public bool IsUnknown
        {
            get { return m_IsUnknown; }
        }

        public bool Equals([NotNull] ILine other)
        {
            return Equals(other as Line);
        }

        public int CompareTo([NotNull] ILine other)
        {
            return m_Id.CompareTo(other.Id);
        }

        #endregion

        public bool IsOnLine(Point point)
        {
            if (m_StartPoint == point ||
                m_EndPoint == point)
            {
                return true;
            }

            double deltaYCheckAndEnd = point.Y - m_StartPoint.Y;
            double deltaXCheckAndEnd = point.X - m_StartPoint.X;

            double deltaYEnd1AndEnd2 = m_EndPoint.Y - m_StartPoint.Y;
            double deltaXEnd1AndEnd2 = m_EndPoint.X - m_StartPoint.X;

            return Math.Abs(deltaYCheckAndEnd/deltaXCheckAndEnd - deltaYEnd1AndEnd2/deltaXEnd1AndEnd2) < SelkieConstants.EpsilonDistance;
        }

        private double CalculateLength()
        {
            double x1 = StartPoint.X;
            double y1 = StartPoint.Y;

            double x2 = EndPoint.X;
            double y2 = EndPoint.Y;

            double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

            return distance;
        }

        [NotNull]
        internal Angle CalculateAngleInRadiansRelativeToXAxis([NotNull] Point one,
                                                              [NotNull] Point two,
                                                              Constants.LineDirection lineDirection)
        {
            double deltaX = two.X - one.X;
            double deltaY = two.Y - one.Y;

            double radians = IsDeltaXOrDeltaYLessThanEpsilon(deltaX, deltaY)
                ? DetermineRadiansDependingOnDeltaX(one, two, deltaX)
                : CalculateRadians(deltaY, deltaX);

            // todo check/fix linedirection causes more trouble than worth,
            // todo use always forward and don't use reverse here
            if (lineDirection == Constants.LineDirection.Reverse)
            {
                radians += Angle.RadiansFor180Degrees;
            }

            if (Math.Abs(Angle.RadiansFor360Degrees - radians) < SelkieConstants.EpsilonRadians)
            {
                radians = Angle.RadiansForZeroDegrees;
            }

            return Angle.FromRadians(radians);
        }

        private static bool IsDeltaXOrDeltaYLessThanEpsilon(double deltaX,
                                                            double deltaY)
        {
            return Math.Abs(deltaX) < SelkieConstants.EpsilonRadians ||
                   Math.Abs(deltaY) < SelkieConstants.EpsilonRadians;
        }

        private static double CalculateRadians(double deltaY,
                                               double deltaX)
        {
            double radians = Math.Atan2(deltaY, deltaX);

            if (radians < 0.0)
            {
                radians = Angle.NormalizeRadians(radians);
            }
            return radians;
        }

        private static double DetermineRadiansDependingOnDeltaX([NotNull] Point one,
                                                                [NotNull] Point two,
                                                                double deltaX)
        {
            double radians;
            if (Math.Abs(deltaX) < SelkieConstants.EpsilonRadians)
            {
                radians = one.Y < two.Y
                    ? Angle.RadiansFor90Degrees
                    : Angle.RadiansFor270Degrees;
            }
            else
            {
                radians = one.X < two.X
                    ? Angle.RadiansForZeroDegrees
                    : Angle.RadiansFor180Degrees;
            }
            return radians;
        }

        public override string ToString()
        {
            return "[{0:F2},{1:F2}] - [{2:F2},{3:F2}]".Inject(m_StartPoint.X,
                                                              m_StartPoint.Y,
                                                              m_EndPoint.X,
                                                              m_EndPoint.Y);
        }

        // ReSharper disable once CodeAnnotationAnalyzer
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof (Line))
            {
                return false;
            }
            return Equals((Line) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (m_EndPoint != null ? m_EndPoint.GetHashCode() : 0);
                result = (result*397) ^ (m_StartPoint != null ? m_StartPoint.GetHashCode() : 0);
                result = (result*397) ^ m_LineDirection.GetHashCode();
                result = (result*397) ^ m_IsUnknown.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(Line left,
                                       Line right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Line left,
                                       Line right)
        {
            return !Equals(left, right);
        }
    }
}