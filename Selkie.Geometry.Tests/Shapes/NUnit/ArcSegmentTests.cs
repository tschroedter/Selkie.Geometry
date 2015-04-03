using System;
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
    internal sealed class ArcSegmentTests
    {
        private Circle m_Circle;
        private Point m_EndPoint;
        private ArcSegment m_Segment;
        private Point m_StartPoint;
        private Constants.TurnDirection m_TurnDirection;

        [SetUp]
        public void Setup()
        {
            m_Circle = new Circle(2.0,
                                  3.0,
                                  4.0);
            m_StartPoint = new Point(2.0,
                                     7.0);
            m_EndPoint = new Point(2.0,
                                   -1.0);
            m_TurnDirection = Constants.TurnDirection.Counterclockwise;

            m_Segment = new ArcSegment(m_Circle,
                                       m_StartPoint,
                                       m_EndPoint,
                                       m_TurnDirection);
        }

        private void AssertCalculateLengthForDegrees(double degrees)
        {
            const double radius = 100.0;
            const double circumference = 2.0 * Math.PI * radius;

            double expected = circumference / ( 360.0 / degrees );
            Angle radians = Angle.FromDegrees(degrees);

            double actual = m_Segment.CalculateLength(radians,
                                                      radius);

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.01,
                                           "Length");
        }

        [Test]
        public void Azimuth0DegreesTest()
        {
            Point endPoint = new Point(2.0,
                                       7.0);

            ArcSegment segment = new ArcSegment(m_Circle,
                                                m_StartPoint,
                                                endPoint);

            Angle expected = Angle.ForZeroDegrees;
            Angle actual = segment.AngleClockwise;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void Azimuth180DegreesTest()
        {
            Point endPoint = new Point(2.0,
                                       -1.0);

            ArcSegment segment = new ArcSegment(m_Circle,
                                                m_StartPoint,
                                                endPoint);

            Angle expected = Angle.For180Degrees;
            Angle actual = segment.AngleClockwise;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void Azimuth270DegreesTest()
        {
            Point endPoint = new Point(6.0,
                                       3.0);

            ArcSegment segment = new ArcSegment(m_Circle,
                                                m_StartPoint,
                                                endPoint);

            Angle expected = Angle.For270Degrees;
            Angle actual = segment.AngleCounterClockwise;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void Azimuth90DegreesTest()
        {
            Point endPoint = new Point(6.0,
                                       3.0);

            ArcSegment segment = new ArcSegment(m_Circle,
                                                m_StartPoint,
                                                endPoint);

            Angle expected = Angle.For90Degrees;
            Angle actual = segment.AngleClockwise;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AzimuthFor180DegreesTest()
        {
            Circle circle = new Circle(0.0,
                                       0.0,
                                       100.0);
            Point startPoint = new Point(100.0,
                                         0.0);
            Point endPoint = new Point(-100.0,
                                       0.0);

            ArcSegment segment = new ArcSegment(circle,
                                                startPoint,
                                                endPoint);

            Angle expected = Angle.For180Degrees;
            Angle actual = segment.AngleClockwise;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AzimuthFor270DegreesTest()
        {
            Circle circle = new Circle(0.0,
                                       0.0,
                                       100.0);
            Point startPoint = new Point(100.0,
                                         0.0);
            Point endPoint = new Point(0.0,
                                       100.0);

            ArcSegment segment = new ArcSegment(circle,
                                                startPoint,
                                                endPoint);

            Angle expected = Angle.For270Degrees;
            Angle actual = segment.AngleClockwise;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToYAxisClockwiseCallsCalculatorTest()
        {
            Angle expected = Angle.For180Degrees;
            Angle actual = m_Segment.AngleClockwise;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void CalculateAngleInRadiansRelativeToYAxisCounterClockwiseCallsCalculatorTest()
        {
            Angle expected = Angle.For180Degrees;
            Angle actual = m_Segment.AngleCounterClockwise;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void CalculateLengthFor135DegreesTest()
        {
            AssertCalculateLengthForDegrees(135.0);
        }

        [Test]
        public void CalculateLengthFor180DegreesTest()
        {
            AssertCalculateLengthForDegrees(180.0);
        }

        [Test]
        public void CalculateLengthFor225DegreesTest()
        {
            AssertCalculateLengthForDegrees(225.0);
        }

        [Test]
        public void CalculateLengthFor270DegreesTest()
        {
            AssertCalculateLengthForDegrees(270.0);
        }

        [Test]
        public void CalculateLengthFor315DegreesTest()
        {
            AssertCalculateLengthForDegrees(315.0);
        }

        [Test]
        public void CalculateLengthFor360DegreesTest()
        {
            AssertCalculateLengthForDegrees(360.0);
        }

        [Test]
        public void CalculateLengthFor45DegreesTest()
        {
            AssertCalculateLengthForDegrees(45.0);
        }

        [Test]
        public void CalculateLengthFor90DegreesTest()
        {
            AssertCalculateLengthForDegrees(90.0);
        }

        [Test]
        public void CalculateLengthForZeroDegreesTest()
        {
            AssertCalculateLengthForDegrees(0.0);
        }

        [Test]
        public void CaseOneTest()
        {
            Circle circle = new Circle(-25.0,
                                       156.13,
                                       100.0);
            Point startPoint = new Point(-87.5,
                                         78.06);
            Point endPoint = new Point(37.5,
                                       78.06);

            ArcSegment segment = new ArcSegment(circle,
                                                startPoint,
                                                endPoint);

            Angle expected = Angle.FromRadians(4.9330162985305783);
            Angle actual = segment.AngleClockwise;

            Assert.AreEqual(expected,
                            actual,
                            "RadiansClockwise");

            expected = Angle.FromRadians(1.3501690086490079);
            actual = segment.AngleCounterClockwise;

            Assert.AreEqual(expected,
                            actual,
                            "RadiansCounterClockwise");
        }

        [Test]
        public void CentrePointTest()
        {
            Assert.AreEqual(m_Circle.CentrePoint,
                            m_Segment.CentrePoint);
        }

        [Test]
        public void EndPointTest()
        {
            Assert.AreEqual(m_EndPoint,
                            m_Segment.EndPoint);
        }

        [Test]
        public void IsUnknonTest()
        {
            Assert.False(m_Segment.IsUnknown);
        }

        [Test]
        public void Length0DegreesTest()
        {
            Point endPoint = new Point(2.0,
                                       7.0);

            ArcSegment segment = new ArcSegment(m_Circle,
                                                m_StartPoint,
                                                endPoint);

            double actual = segment.LengthClockwise;

            NUnitHelper.AssertIsEquivalent(0.0,
                                           actual,
                                           0.01,
                                           "Distance");
        }

        [Test]
        public void Length180DegreesTest()
        {
            Point endPoint = new Point(2.0,
                                       -1.0);

            ArcSegment segment = new ArcSegment(m_Circle,
                                                m_StartPoint,
                                                endPoint);

            double actual = segment.LengthClockwise;

            NUnitHelper.AssertIsEquivalent(12.57,
                                           actual,
                                           0.01,
                                           "Distance");
        }

        [Test]
        public void Length270DegreesTest()
        {
            Point endPoint = new Point(6.0,
                                       3.0);

            ArcSegment segment = new ArcSegment(m_Circle,
                                                m_StartPoint,
                                                endPoint);

            double actual = segment.LengthCounterClockwise;

            NUnitHelper.AssertIsEquivalent(18.85,
                                           actual,
                                           0.01,
                                           "Azimuth");
        }

        [Test]
        public void Length90DegreesTest()
        {
            Point endPoint = new Point(6.0,
                                       3.0);

            ArcSegment segment = new ArcSegment(m_Circle,
                                                m_StartPoint,
                                                endPoint);

            double actual = segment.LengthClockwise;

            NUnitHelper.AssertIsEquivalent(6.28,
                                           actual,
                                           0.01,
                                           "Distance");
        }

        [Test]
        public void LengthCounterClockwiseTest()
        {
            const double expected = 12.57;
            double actual = m_Segment.LengthCounterClockwise;

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.01,
                                           "Length");
        }

        [Test]
        public void LengthTest()
        {
            const double expected = 12.57;
            double actual = m_Segment.Length;

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.01,
                                           "Length");
        }

        [Test]
        public void RadiansCounterClockwiseTest()
        {
            Angle expected = Angle.For180Degrees;
            Angle actual = m_Segment.AngleCounterClockwise;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void RadiusTest()
        {
            Assert.AreEqual(m_Circle.Radius,
                            m_Segment.Radius);
        }

        [Test]
        public void ReverseTest()
        {
            IArcSegment actual = m_Segment.Reverse() as IArcSegment;

            Assert.NotNull(actual);
            Assert.AreEqual(m_Segment.LengthClockwise,
                            actual.Length,
                            "Length");
            Assert.AreEqual(m_Segment.EndPoint,
                            actual.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_Segment.StartPoint,
                            actual.EndPoint,
                            "EndPoint");
            Assert.AreEqual(Constants.TurnDirection.Counterclockwise,
                            actual.TurnDirection,
                            "TurnDirection");
            Assert.AreEqual(m_Segment.AngleClockwise,
                            actual.AngleClockwise,
                            "RadiansClockwise");
        }

        [Test]
        public void StartPointTest()
        {
            Assert.AreEqual(m_StartPoint,
                            m_Segment.StartPoint);
        }

        [Test]
        public void TurnDirectionTest()
        {
            Assert.AreEqual(m_TurnDirection,
                            m_Segment.TurnDirection);
        }

        [Test]
        public void UnknonTest()
        {
            IArcSegment segment = ArcSegment.Unknown;

            Assert.True(segment.IsUnknown,
                        "IsUnknown");
            Assert.AreEqual(Point.Unknown,
                            segment.CentrePoint,
                            "CentrePoint");
            Assert.AreEqual(Point.Unknown,
                            segment.StartPoint,
                            "StartPoint");
            Assert.AreEqual(Point.Unknown,
                            segment.EndPoint,
                            "EndPoint");
            Assert.AreEqual(Constants.TurnDirection.Unknown,
                            segment.TurnDirection,
                            "TurnDirection");
        }

        [Test]
        public void ValidatePointReturnsFalseForOnCircleTest()
        {
            Circle circle = new Circle(0.0,
                                       0.0,
                                       100.0);
            Point startPoint = new Point(170.71,
                                         170.71);

            Assert.False(m_Segment.ValidatePoint(circle,
                                                 startPoint));
        }

        [Test]
        public void ValidatePointReturnsTrueForOnCircleTest()
        {
            Circle circle = new Circle(0.0,
                                       0.0,
                                       100.0);
            Point startPoint = new Point(70.71,
                                         70.71);

            Assert.True(m_Segment.ValidatePoint(circle,
                                                startPoint));
        }

        [Test]
        public void ValidateStartAndEndPointThrowsForEndPointNotOnCircleTest()
        {
            Circle circle = new Circle(0.0,
                                       0.0,
                                       100.0);
            Point startPoint = new Point(0.0,
                                         100.0);
            Point endPoint = new Point(170.71,
                                       170.71);

            Assert.Throws <ArgumentException>(() => m_Segment.ValidateStartAndEndPoint(circle,
                                                                                       startPoint,
                                                                                       endPoint));
        }

        [Test]
        public void ValidateStartAndEndPointThrowsForStartPointNotOnCircleTest()
        {
            Circle circle = new Circle(0.0,
                                       0.0,
                                       100.0);
            Point startPoint = new Point(100.0,
                                         100.0);
            Point endPoint = new Point(170.71,
                                       170.71);

            Assert.Throws <ArgumentException>(() => m_Segment.ValidateStartAndEndPoint(circle,
                                                                                       startPoint,
                                                                                       endPoint));
        }
    }
}