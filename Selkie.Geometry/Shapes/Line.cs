using System;
using JetBrains.Annotations;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Primitives;
using Selkie.Windsor.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public class Line
        : ILine,
          IEquatable <Line>
    {
        public const int UnknownId = int.MinValue;
        public static readonly Line Unknown = new Line();

        private readonly ILineDirectionCalculator m_Calculator = new LineDirectionCalculator();

        private Line()
            : this(UnknownId,
                   new Point(double.MaxValue,
                             double.MaxValue),
                   new Point(double.MaxValue,
                             double.MaxValue),
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
            : this(id,
                   new Point(x1,
                             y1),
                   new Point(x2,
                             y2),
                   runDirection,
                   isUnknown)
        {
        }

        // ReSharper disable once TooManyDependencies
        public Line(double x1,
                    double y1,
                    double x2,
                    double y2)
            : this(UnknownId,
                   new Point(x1,
                             y1),
                   new Point(x2,
                             y2),
                   Constants.LineDirection.Forward,
                   false)
        {
        }

        public Line(int id,
                    [NotNull] Point startPoint,
                    [NotNull] Point endPoint)
            : this(id,
                   startPoint,
                   endPoint,
                   Constants.LineDirection.Forward,
                   false)
        {
        }

        public Line([NotNull] Point startPoint,
                    [NotNull] Point endPoint)
            : this(UnknownId,
                   startPoint,
                   endPoint,
                   Constants.LineDirection.Forward,
                   false)
        {
        }

        // ReSharper disable once TooManyDependencies
        public Line(int id,
                    [NotNull] Point startPoint,
                    [NotNull] Point endPoint,
                    Constants.LineDirection lineDirection)
            : this(id,
                   startPoint,
                   endPoint,
                   lineDirection,
                   false)
        {
        }

        // ReSharper disable once TooManyDependencies
        public Line([NotNull] Point startPoint,
                    [NotNull] Point endPoint,
                    Constants.LineDirection lineDirection)
            : this(UnknownId,
                   startPoint,
                   endPoint,
                   lineDirection,
                   false)
        {
        }

        // ReSharper disable once TooManyDependencies
        public Line([NotNull] Point startPoint,
                    [NotNull] Point endPoint,
                    Constants.LineDirection lineDirection,
                    bool isUnknown)
            : this(UnknownId,
                   startPoint,
                   endPoint,
                   lineDirection,
                   isUnknown)
        {
        }

        // ReSharper disable once TooManyDependencies
        internal Line(int id,
                      [NotNull] Point startPoint,
                      [NotNull] Point endPoint,
                      Constants.LineDirection lineDirection,
                      bool isUnknown)
        {
            Id = id;
            StartPoint = startPoint;
            EndPoint = endPoint;
            IsUnknown = isUnknown;
            RunDirection = lineDirection;
            Length = CalculateLength();
            AngleToXAxis = CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                  endPoint,
                                                                  lineDirection);
        }

        #region IEquatable<Line> Members

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(Line other)
        {
            if ( ReferenceEquals(null,
                                 other) )
            {
                return false;
            }
            if ( ReferenceEquals(this,
                                 other) )
            {
                return true;
            }
            return Equals(other.EndPoint,
                          EndPoint) && Equals(other.StartPoint,
                                              StartPoint) && Equals(other.RunDirection,
                                                                    RunDirection) &&
                   other.IsUnknown.Equals(IsUnknown);
        }

        #endregion

        public bool IsOnLine(Point point)
        {
            if ( StartPoint == point ||
                 EndPoint == point )
            {
                return true;
            }

            double deltaYCheckAndEnd = point.Y - StartPoint.Y;
            double deltaXCheckAndEnd = point.X - StartPoint.X;

            double deltaYEnd1AndEnd2 = EndPoint.Y - StartPoint.Y;
            double deltaXEnd1AndEnd2 = EndPoint.X - StartPoint.X;

            return Math.Abs(deltaYCheckAndEnd / deltaXCheckAndEnd - deltaYEnd1AndEnd2 / deltaXEnd1AndEnd2) <
                   SelkieConstants.EpsilonDistance;
        }

        public Constants.TurnDirection TurnDirectionToPoint(Point point)
        {
            // todo testing
            var calculator = new LineDirectionCalculator
                             {
                                 Line = this,
                                 Point = point
                             };

            calculator.Calculate();

            return calculator.Direction;
        }

        private double CalculateLength()
        {
            double x1 = StartPoint.X;
            double y1 = StartPoint.Y;

            double x2 = EndPoint.X;
            double y2 = EndPoint.Y;

            double distance = Math.Sqrt(Math.Pow(x2 - x1,
                                                 2) + Math.Pow(y2 - y1,
                                                               2));

            return distance;
        }

        [NotNull]
        internal Angle CalculateAngleInRadiansRelativeToXAxis([NotNull] Point one,
                                                              [NotNull] Point two,
                                                              Constants.LineDirection lineDirection)
        {
            double deltaX = two.X - one.X;
            double deltaY = two.Y - one.Y;

            double radians = IsDeltaXOrDeltaYLessThanEpsilon(deltaX,
                                                             deltaY)
                                 ? DetermineRadiansDependingOnDeltaX(one,
                                                                     two,
                                                                     deltaX)
                                 : CalculateRadians(deltaY,
                                                    deltaX);

            // todo check/fix linedirection causes more trouble than worth,
            // todo use always forward and don't use reverse here
            if ( lineDirection == Constants.LineDirection.Reverse )
            {
                radians += Angle.RadiansFor180Degrees;
            }

            if ( Math.Abs(Angle.RadiansFor360Degrees - radians) < SelkieConstants.EpsilonRadians )
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
            double radians = Math.Atan2(deltaY,
                                        deltaX);

            if ( radians < 0.0 )
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
            if ( Math.Abs(deltaX) < SelkieConstants.EpsilonRadians )
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
            return "[{0:F2},{1:F2}] - [{2:F2},{3:F2}]".Inject(StartPoint.X,
                                                              StartPoint.Y,
                                                              EndPoint.X,
                                                              EndPoint.Y);
        }

        // ReSharper disable once CodeAnnotationAnalyzer
        public override bool Equals(object obj)
        {
            if ( ReferenceEquals(null,
                                 obj) )
            {
                return false;
            }
            if ( ReferenceEquals(this,
                                 obj) )
            {
                return true;
            }
            if ( obj.GetType() != typeof ( Line ) )
            {
                return false;
            }
            return Equals(( Line ) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = EndPoint.GetHashCode();
                result = ( result * 397 ) ^ StartPoint.GetHashCode();
                result = ( result * 397 ) ^ RunDirection.GetHashCode();
                result = ( result * 397 ) ^ IsUnknown.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(Line left,
                                       Line right)
        {
            return Equals(left,
                          right);
        }

        public static bool operator !=(Line left,
                                       Line right)
        {
            return !Equals(left,
                           right);
        }

        #region ILine Members

        public int Id { get; private set; }

        public Constants.LineDirection RunDirection { get; private set; }

        public Angle AngleToXAxis { get; private set; }

        public Point StartPoint { get; private set; }

        public Point EndPoint { get; private set; }

        public double Length { get; private set; }

        public IPolylineSegment Reverse()
        {
            var reverse = new Line(Id,
                                   EndPoint,
                                   StartPoint);

            return reverse;
        }

        public double X1
        {
            get
            {
                return StartPoint.X;
            }
        }

        public double Y1
        {
            get
            {
                return StartPoint.Y;
            }
        }

        public double X2
        {
            get
            {
                return EndPoint.X;
            }
        }

        public double Y2
        {
            get
            {
                return EndPoint.Y;
            }
        }

        public Constants.TurnDirection IsPointInsideCircle(Point point)
        {
            // todo testing
            // todo IoC Calculator testing
            m_Calculator.Line = this;
            m_Calculator.Point = point;

            m_Calculator.Calculate();

            return m_Calculator.Direction;
        }

        public bool IsUnknown { get; private set; }

        public bool Equals([NotNull] ILine other)
        {
            return Equals(other as Line);
        }

        public int CompareTo([NotNull] ILine other)
        {
            return Id.CompareTo(other.Id);
        }

        #endregion
    }
}