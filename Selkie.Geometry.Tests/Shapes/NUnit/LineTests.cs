using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.Shapes.NUnit
{
    // ReSharper disable once ClassTooBig
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class LineTests
    {
        [SetUp]
        public void Setup()
        {
            m_StartPoint = new Point(10.0,
                                     10.0);
            m_EndPoint = new Point(40.0,
                                   50.0);

            m_Line = new Line(1,
                              m_StartPoint,
                              m_EndPoint);
        }

        private Point m_EndPoint;
        private Line m_Line;
        private Point m_StartPoint;

        [Test]
        public void AngleToXAxisAtEndPointTest()
        {
            Angle expected = Angle.FromDegrees(53.13010235415598);
            Angle actual = m_Line.AngleToXAxisAtEndPoint;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleToXAxisAtStartPointTest()
        {
            Angle expected = Angle.FromDegrees(53.13010235415598);
            Angle actual = m_Line.AngleToXAxisAtStartPoint;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleToXAxisForDirectionReverseTest()
        {
            var line = new Line(m_StartPoint,
                                m_EndPoint,
                                Constants.LineDirection.Reverse);

            Angle expected = Angle.FromDegrees(233.13010235415598); // 53.13 + 180.0
            Angle actual = line.AngleToXAxis;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleToXAxisTest()
        {
            Angle expected = Angle.FromDegrees(53.13010235415598);
            Angle actual = m_Line.AngleToXAxis;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToXAxisForCase180Test()
        {
            var startPoint = new Point(750.0,
                                       1420.0);
            var endPoint = new Point(550.0,
                                     1420.0);

            const double expected = Angle.RadiansFor180Degrees;
            Angle actual = m_Line.CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                         endPoint,
                                                                         Constants.LineDirection.Forward);

            NUnitHelper.AssertRadians(expected,
                                      actual.Radians);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToXAxisForFor135DegreesTest()
        {
            var startPoint = new Point(0.0,
                                       0.0);
            var endPoint = new Point(-1.0,
                                     1.0);

            const double expected = Angle.RadiansFor135Degrees;
            Angle actual = m_Line.CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                         endPoint,
                                                                         Constants.LineDirection.Forward);

            NUnitHelper.AssertRadians(expected,
                                      actual.Radians);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToXAxisForFor180DegreeLineInReverseTest()
        {
            var startPoint = new Point(0.0,
                                       10.0);
            var endPoint = new Point(100.0,
                                     10.0);

            const double expected = Angle.RadiansFor180Degrees;
            Angle actual = m_Line.CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                         endPoint,
                                                                         Constants.LineDirection.Reverse);

            NUnitHelper.AssertRadians(expected,
                                      actual.Radians);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToXAxisForFor225DegreesTest()
        {
            var startPoint = new Point(0.0,
                                       0.0);
            var endPoint = new Point(-1.0,
                                     -1.0);

            const double expected = Angle.RadiansFor225Degrees;
            Angle actual = m_Line.CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                         endPoint,
                                                                         Constants.LineDirection.Forward);

            NUnitHelper.AssertRadians(expected,
                                      actual.Radians);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToXAxisForFor315DegreesTest()
        {
            var startPoint = new Point(0.0,
                                       0.0);
            var endPoint = new Point(1.0,
                                     -1.0);

            const double expected = Angle.RadiansFor315Degrees;
            Angle actual = m_Line.CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                         endPoint,
                                                                         Constants.LineDirection.Forward);

            NUnitHelper.AssertRadians(expected,
                                      actual.Radians);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToXAxisForFor45DegreesTest()
        {
            var startPoint = new Point(0.0,
                                       0.0);
            var endPoint = new Point(1.0,
                                     1.0);

            const double expected = Angle.RadiansFor45Degrees;
            Angle actual = m_Line.CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                         endPoint,
                                                                         Constants.LineDirection.Forward);

            NUnitHelper.AssertRadians(expected,
                                      actual.Radians);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToXAxisForFor90DegreeLineInReverseTest()
        {
            var startPoint = new Point(0.0,
                                       10.0);
            var endPoint = new Point(100.0,
                                     10.0);

            const double expected = Angle.RadiansFor180Degrees;
            Angle actual = m_Line.CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                         endPoint,
                                                                         Constants.LineDirection.Reverse);

            NUnitHelper.AssertRadians(expected,
                                      actual.Radians);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToXAxisForForZeroDegreeLineInReverseTest()
        {
            var startPoint = new Point(100.0,
                                       10.0);
            var endPoint = new Point(0.0,
                                     10.0);

            const double expected = Angle.RadiansForZeroDegrees;
            Angle actual = m_Line.CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                         endPoint,
                                                                         Constants.LineDirection.Reverse);

            NUnitHelper.AssertRadians(expected,
                                      actual.Radians);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToXAxisForForZeroDegreesTest()
        {
            var startPoint = new Point(0.0,
                                       0.0);
            var endPoint = new Point(0.0,
                                     1.0);

            const double expected = Angle.RadiansFor90Degrees;
            Angle actual = m_Line.CalculateAngleInRadiansRelativeToXAxis(startPoint,
                                                                         endPoint,
                                                                         Constants.LineDirection.Forward);

            NUnitHelper.AssertRadians(expected,
                                      actual.Radians);
        }

        [Test]
        public void CaseOneForwardTest()
        {
            //         var line1 = new Line(1, new Point(150, 610), new Point(350, 610));
            var startPoint = new Point(350,
                                       520);
            var endPoint = new Point(150,
                                     520);

            var line = new Line(0,
                                startPoint,
                                endPoint);

            Assert.AreEqual(0,
                            line.Id,
                            "Ïd");
            Assert.AreEqual(startPoint,
                            line.StartPoint,
                            "StartPoint");
            Assert.AreEqual(endPoint,
                            line.EndPoint,
                            "EndPoint");
            Assert.AreEqual(Angle.For180Degrees,
                            line.AngleToXAxis,
                            "AngleToXAxis");
        }

        [Test]
        public void CaseOneReverseTest()
        {
            //         var line1 = new Line(1, new Point(150, 610), new Point(350, 610));
            var startPoint = new Point(350,
                                       520);
            var endPoint = new Point(150,
                                     520);

            var line = new Line(0,
                                startPoint,
                                endPoint).Reverse() as ILine;

            Assert.NotNull(line);
            Assert.AreEqual(0,
                            line.Id,
                            "Ïd");
            Assert.AreEqual(endPoint,
                            line.StartPoint,
                            "StartPoint");
            Assert.AreEqual(startPoint,
                            line.EndPoint,
                            "EndPoint");
            Assert.AreEqual(Angle.ForZeroDegrees,
                            line.AngleToXAxis,
                            "AngleToXAxis");
        }

        [Test]
        public void CaseOneTest()
        {
            var startPoint = new Point(10.0,
                                       2.5);
            var endPoint = new Point(16.0,
                                     2.5);

            var line = new Line(startPoint,
                                endPoint);

            const double expected = Angle.RadiansForZeroDegrees;
            double actual = line.AngleToXAxis.Radians;

            NUnitHelper.AssertRadians(expected,
                                      actual);
        }

        [Test]
        public void CaseOneTwoForwardTest()
        {
            var startPoint = new Point(150,
                                       610);
            var endPoint = new Point(350,
                                     610);

            var line = new Line(0,
                                startPoint,
                                endPoint);

            Assert.AreEqual(0,
                            line.Id,
                            "Ïd");
            Assert.AreEqual(startPoint,
                            line.StartPoint,
                            "StartPoint");
            Assert.AreEqual(endPoint,
                            line.EndPoint,
                            "EndPoint");
            Assert.AreEqual(Angle.ForZeroDegrees,
                            line.AngleToXAxis,
                            "AngleToXAxis");
        }

        [Test]
        public void CaseTwoReverseTest()
        {
            var startPoint = new Point(150,
                                       610);
            var endPoint = new Point(350,
                                     610);

            var line = new Line(0,
                                startPoint,
                                endPoint).Reverse() as ILine;

            Assert.NotNull(line);
            Assert.AreEqual(0,
                            line.Id,
                            "Ïd");
            Assert.AreEqual(endPoint,
                            line.StartPoint,
                            "StartPoint");
            Assert.AreEqual(startPoint,
                            line.EndPoint,
                            "EndPoint");
            Assert.AreEqual(Angle.For180Degrees,
                            line.AngleToXAxis,
                            "AngleToXAxis");
        }

        [Test]
        public void CaseTwoTest()
        {
            var startPoint = new Point(11.0,
                                       2.5);
            var endPoint = new Point(10.0,
                                     14.0);

            var line = new Line(startPoint,
                                endPoint);

            const double expected = 1.6575346654708818d;
            double actual = line.AngleToXAxis.Radians;

            NUnitHelper.AssertRadians(expected,
                                      actual);
        }

        [Test]
        public void CompareToForLineWithHigherIdTest()
        {
            var other = new Line(2,
                                 m_StartPoint,
                                 m_EndPoint);

            Assert.True(m_Line.CompareTo(other) == -1);
        }

        [Test]
        public void CompareToForLineWithLowerIdTest()
        {
            var other = new Line(0,
                                 m_StartPoint,
                                 m_EndPoint);

            Assert.True(m_Line.CompareTo(other) == 1);
        }

        [Test]
        public void CompareToForSameLineTest()
        {
            Assert.True(m_Line.CompareTo(m_Line) == 0);
        }

        [Test]
        public void ConstructorX1Y1X2Y2Test()
        {
            var line = new Line(1.0,
                                2.0,
                                3.0,
                                4.0);

            Assert.AreEqual(1.0,
                            line.X1,
                            "X1");
            Assert.AreEqual(2.0,
                            line.Y1,
                            "Y1");
            Assert.AreEqual(3.0,
                            line.X2,
                            "X2");
            Assert.AreEqual(4.0,
                            line.Y2,
                            "Y2");
            Assert.AreEqual(Constants.LineDirection.Forward,
                            line.RunDirection,
                            "RunDirection");
            Assert.False(line.IsUnknown);
        }

        [Test]
        public void EndPointTest()
        {
            Assert.AreEqual(m_StartPoint,
                            m_Line.StartPoint);
        }

        [Test]
        public void EqualsOperatorReturnsFalseForDifferentValuesTest()
        {
            var line1 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));
            var line2 = new Line(new Point(1.0,
                                           2.0),
                                 new Point(3.0,
                                           4.0));

            Assert.False(line1 == line2);
        }

        [Test]
        public void EqualsOperatorReturnsTrueForSameValuesTest()
        {
            var line1 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));
            var line2 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));

            Assert.True(line1 == line2);
        }

        [Test]
        public void EqualsReturnsFalseForDifferentDirectionTest()
        {
            var line1 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));
            var line2 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0),
                                 Constants.LineDirection.Reverse);

            Assert.False(line1.Equals(line2));
        }

        [Test]
        public void EqualsReturnsFalseForDifferentUnknownTest()
        {
            var line1 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));
            var line2 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0),
                                 Constants.LineDirection.Forward,
                                 true);

            Assert.False(line1.Equals(line2));
        }

        [Test]
        public void EqualsReturnsFalseForDifferentValuesTest()
        {
            var line1 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));
            var line2 = new Line(new Point(1.0,
                                           2.0),
                                 new Point(3.0,
                                           4.0));

            Assert.False(line1.Equals(line2));
        }

        [Test]
        public void EqualsReturnsFalseForNullTest()
        {
            var line = new Line(new Point(0.0,
                                          0.0),
                                new Point(10.0,
                                          10.0));

            Assert.False(line.Equals(null));
        }

        [Test]
        public void EqualsReturnsFalseForOtherTypeTest()
        {
            var line = new Line(new Point(0.0,
                                          0.0),
                                new Point(10.0,
                                          10.0));

            Assert.False(line.Equals(new object()));
        }

        [Test]
        public void EqualsReturnsTrueForSameTest()
        {
            var line = new Line(new Point(0.0,
                                          0.0),
                                new Point(10.0,
                                          10.0));

            Assert.True(line.Equals(line));
        }

        [Test]
        public void EqualsReturnsTrueForSameValuesTest()
        {
            var line1 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));
            var line2 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));

            Assert.True(line1.Equals(line2));
        }

        [Test]
        public void GetHashCodeTest()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.DoesNotThrow(() => m_Line.GetHashCode());
        }

        [Test]
        public void IsOnlineReturnsFalseForPointNotOnLineTest()
        {
            var line = new Line(0.0,
                                0.0,
                                10.0,
                                10.0);
            var point = new Point(100.0,
                                  -100.0);

            Assert.False(line.IsOnLine(point));
        }

        [Test]
        public void IsOnlineReturnsTrueForEndPointTest()
        {
            var line = new Line(0.0,
                                0.0,
                                10.0,
                                10.0);

            Assert.True(line.IsOnLine(line.EndPoint));
        }

        [Test]
        public void IsOnlineReturnsTrueForPointOnLineTest()
        {
            var line = new Line(0.0,
                                0.0,
                                10.0,
                                10.0);
            var point = new Point(5.0,
                                  5.0);

            Assert.True(line.IsOnLine(point));
        }

        [Test]
        public void IsOnlineReturnsTrueForStartPointTest()
        {
            var line = new Line(0.0,
                                0.0,
                                10.0,
                                10.0);

            Assert.True(line.IsOnLine(line.StartPoint));
        }

        [Test]
        public void IsUnknownLineTest()
        {
            Line actual = Line.Unknown;

            Assert.AreEqual(double.MaxValue,
                            actual.X1,
                            "X1");
            Assert.AreEqual(double.MaxValue,
                            actual.Y1,
                            "Y1");
            Assert.AreEqual(double.MaxValue,
                            actual.X2,
                            "X2");
            Assert.AreEqual(double.MaxValue,
                            actual.Y2,
                            "Y2");
            Assert.AreEqual(Angle.Unknown,
                            actual.AngleToXAxisAtEndPoint,
                            "AngleToXAxisAtEndPoint");
            Assert.AreEqual(Angle.Unknown,
                            actual.AngleToXAxisAtStartPoint,
                            "AngleToXAxisAtStartPoint");
        }

        [Test]
        public void IsUnknownTest()
        {
            Line actual = Line.Unknown;

            Assert.True(actual.IsUnknown);
        }

        [Test]
        public void LengthTest()
        {
            Assert.AreEqual(50.0,
                            m_Line.Length);
        }

        [Test]
        public void NotEqualsOperatorReturnsFalseForSameValuesTest()
        {
            var line1 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));
            var line2 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));

            Assert.False(line1 != line2);
        }

        [Test]
        public void NotEqualsOperatorReturnsTrueForDifferentValuesTest()
        {
            var line1 = new Line(new Point(0.0,
                                           0.0),
                                 new Point(10.0,
                                           10.0));
            var line2 = new Line(new Point(1.0,
                                           2.0),
                                 new Point(3.0,
                                           4.0));

            Assert.True(line1 != line2);
        }

        [Test]
        public void ReverseTest()
        {
            IPolylineSegment actual = m_Line.Reverse();

            Assert.AreEqual(m_Line.Length,
                            actual.Length,
                            "Length");
            Assert.AreEqual(m_Line.EndPoint,
                            actual.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_Line.StartPoint,
                            actual.EndPoint,
                            "EndPoint");
        }

        [Test]
        public void StartPointTest()
        {
            Assert.AreEqual(m_StartPoint,
                            m_Line.StartPoint);
        }

        [Test]
        public void SurveyDirectionDefaulTest()
        {
            Assert.AreEqual(Constants.LineDirection.Forward,
                            m_Line.RunDirection);
        }

        [Test]
        public void SurveyDirectionTest()
        {
            var line = new Line(m_StartPoint,
                                m_EndPoint,
                                Constants.LineDirection.Reverse);

            Assert.AreEqual(Constants.LineDirection.Reverse,
                            line.RunDirection);
        }

        [Test]
        public void ToStringTest()
        {
            string expected = "[{0:F2},{1:F2}] - [{2:F2},{3:F2}]".Inject(m_StartPoint.X,
                                                                         m_StartPoint.Y,
                                                                         m_EndPoint.X,
                                                                         m_EndPoint.Y);
            string actual = m_Line.ToString();

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void TurnDirectionToPointTest()
        {
            Constants.TurnDirection actual = m_Line.TurnDirectionToPoint(new Point(20.0,
                                                                                   20.0));

            Assert.AreEqual(Constants.TurnDirection.Clockwise,
                            actual);
        }

        [Test]
        public void Unknown_AngleToXAxisAtEndPointReturnsUnknown_WhenCalled()
        {
            Line actual = Line.Unknown;

            Assert.AreEqual(Angle.Unknown,
                            actual.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void Unknown_AngleToXAxisAtStartPointReturnsUnknown_WhenCalled()
        {
            Line actual = Line.Unknown;

            Assert.AreEqual(Angle.Unknown,
                            actual.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void Unknown_AngleToXAxisReturnsUnknown_WhenCalled()
        {
            Line actual = Line.Unknown;

            Assert.AreEqual(Angle.Unknown,
                            actual.AngleToXAxis);
        }

        [Test]
        public void X1Test()
        {
            Assert.AreEqual(m_StartPoint.X,
                            m_Line.X1);
        }

        [Test]
        public void X2Test()
        {
            Assert.AreEqual(m_EndPoint.X,
                            m_Line.X2);
        }

        [Test]
        public void Y1Test()
        {
            Assert.AreEqual(m_StartPoint.Y,
                            m_Line.Y1);
        }

        [Test]
        public void Y2Test()
        {
            Assert.AreEqual(m_EndPoint.Y,
                            m_Line.Y2);
        }
    }
}