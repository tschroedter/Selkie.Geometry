using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Shapes.Calculators;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Tests.Shapes.Calculators.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class CircleCentreToPointCalculatorTests
    {
        private CircleCentreToPointCalculator m_Calculator;
        private Point m_CentrePoint;
        private Point m_Point;
        private double m_RadiusOne;

        [SetUp]
        public void Setup()
        {
            m_CentrePoint = new Point(3.0,
                                      4.0);
            m_Point = new Point(5.0,
                                4.0);
            m_RadiusOne = 2.0;

            m_Calculator = new CircleCentreToPointCalculator(m_CentrePoint,
                                                             m_Point);
        }

        private void AssertAngleRelativeToXAxisCounterClockwiseForDegrees(double degrees)
        {
            Angle angle = Angle.FromDegrees(degrees);

            double x = m_CentrePoint.X + ( m_RadiusOne * Math.Cos(angle.Radians) );
            double y = m_CentrePoint.Y + ( m_RadiusOne * Math.Sin(angle.Radians) );

            Point point = new Point(x,
                                    y);

            Angle expected = angle;
            Angle actual = m_Calculator.CalculateAngleRelativeToXAxisCounterClockwise(m_CentrePoint,
                                                                                      point);

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseDeltaXDeltaYLessEpsilonTest()
        {
            Point centrePoint = new Point(-3.0,
                                          4.0);
            Point point = new Point(-5.0,
                                    2.0);

            Angle actual = m_Calculator.CalculateAngleRelativeToXAxisCounterClockwise(centrePoint,
                                                                                      point);

            Assert.AreEqual(Angle.For225Degrees,
                            actual);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseDeltaXNegativeAngleTest()
        {
            Point point = m_CentrePoint.Move(SelkieConstants.EpsilonDistance,
                                             SelkieConstants.EpsilonDistance);

            Angle actual = m_Calculator.CalculateAngleRelativeToXAxisCounterClockwise(m_CentrePoint,
                                                                                      point);

            Assert.AreEqual(Angle.ForZeroDegrees,
                            actual);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseFor135DegreesTest()
        {
            AssertAngleRelativeToXAxisCounterClockwiseForDegrees(135.0);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseFor180DegreesTest()
        {
            AssertAngleRelativeToXAxisCounterClockwiseForDegrees(180.0);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseFor225DegreesTest()
        {
            AssertAngleRelativeToXAxisCounterClockwiseForDegrees(225.0);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseFor270DegreesTest()
        {
            AssertAngleRelativeToXAxisCounterClockwiseForDegrees(270.0);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseFor315DegreesTest()
        {
            AssertAngleRelativeToXAxisCounterClockwiseForDegrees(315.0);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseFor360DegreesTest()
        {
            double x = m_CentrePoint.X + ( m_RadiusOne * Math.Cos(Angle.RadiansFor360Degrees) );
            double y = m_CentrePoint.Y + ( m_RadiusOne * Math.Sin(Angle.RadiansFor360Degrees) );

            Point point = new Point(x,
                                    y);

            Angle actual = m_Calculator.CalculateAngleRelativeToXAxisCounterClockwise(m_CentrePoint,
                                                                                      point);

            Assert.AreEqual(Angle.ForZeroDegrees,
                            actual);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseFor45DegreesTest()
        {
            AssertAngleRelativeToXAxisCounterClockwiseForDegrees(45.0);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseFor90DegreesTest()
        {
            AssertAngleRelativeToXAxisCounterClockwiseForDegrees(90.0);
        }

        [Test]
        public void AngleRelativeToXAxisCounterClockwiseForZeroDegreesTest()
        {
            AssertAngleRelativeToXAxisCounterClockwiseForDegrees(0.0);
        }

        [Test]
        public void CalculateForCentrePointUnknownTest()
        {
            CircleCentreToPointCalculator calculator = new CircleCentreToPointCalculator(Point.Unknown,
                                                                                         new Point(0.0,
                                                                                                   0.0));

            Assert.AreEqual(Angle.ForZeroDegrees,
                            calculator.AngleRelativeToYAxisCounterclockwise,
                            "RadiansRelativeToYAxisCounterclockwise");
            Assert.AreEqual(Angle.ForZeroDegrees,
                            calculator.AngleRelativeToYAxisClockwise,
                            "RadiansRelativeToYAxisClockwise");
            Assert.AreEqual(Angle.ForZeroDegrees,
                            calculator.AngleRelativeToYAxisCounterclockwise,
                            "RadiansRelativeToYAxisCounterclockwise");
        }

        [Test]
        public void CalculateForPointUnknownTest()
        {
            CircleCentreToPointCalculator calculator = new CircleCentreToPointCalculator(new Point(0.0,
                                                                                                   0.0),
                                                                                         Point.Unknown);

            Assert.AreEqual(Angle.ForZeroDegrees,
                            calculator.AngleRelativeToYAxisCounterclockwise,
                            "RadiansRelativeToYAxisCounterclockwise");
            Assert.AreEqual(Angle.ForZeroDegrees,
                            calculator.AngleRelativeToYAxisClockwise,
                            "RadiansRelativeToYAxisClockwise");
            Assert.AreEqual(Angle.ForZeroDegrees,
                            calculator.AngleRelativeToYAxisCounterclockwise,
                            "RadiansRelativeToYAxisCounterclockwise");
        }

        [Test]
        public void CentrePointTest()
        {
            Assert.AreEqual(m_CentrePoint,
                            m_Calculator.CentrePoint);
        }

        [Test]
        public void PointTest()
        {
            Assert.AreEqual(m_Point,
                            m_Calculator.Point);
        }

        [Test]
        public void RadiansRelativeToXAxisCounterClockwiseTest()
        {
            Assert.AreEqual(Angle.ForZeroDegrees,
                            m_Calculator.AngleRelativeToXAxisCounterClockwise);
        }

        [Test]
        public void RadiansRelativeToYAxisClockwiseTest()
        {
            Assert.AreEqual(Angle.For90Degrees,
                            m_Calculator.AngleRelativeToYAxisClockwise);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterclockwiseTest()
        {
            Assert.AreEqual(Angle.For270Degrees,
                            m_Calculator.AngleRelativeToYAxisCounterclockwise);
        }
    }
}