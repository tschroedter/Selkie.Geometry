using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Tests.Calculators.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class CoordinatePairCalculatorTests
    {
        private CoordinatePairCalculator m_Calculator;
        private ICircle m_Circle;
        private Point m_Point180;
        private Point m_Point45;
        private Tuple <double, double> m_Xt1And2;
        private Tuple <double, double> m_Yt1And2;

        [SetUp]
        public void Setup()
        {
            m_Circle = new Circle(new Point(3.0,
                                            -5.0),
                                  4.0);

            m_Point45 = m_Circle.PointOnCircle(Angle.For45Degrees);
            m_Point180 = m_Circle.PointOnCircle(Angle.For180Degrees);

            m_Xt1And2 = new Tuple <double, double>(m_Point45.X,
                                                   m_Point180.X);
            m_Yt1And2 = new Tuple <double, double>(m_Point45.Y,
                                                   m_Point180.Y);

            m_Calculator = new CoordinatePairCalculator(m_Circle,
                                                        m_Xt1And2,
                                                        m_Yt1And2);
        }

        [Test]
        public void CircleTest()
        {
            Assert.AreEqual(m_Circle,
                            m_Calculator.Circle);
        }

        [Test]
        public void ConstructorEmptyTest()
        {
            CoordinatePairCalculator calculator = new CoordinatePairCalculator();

            Assert.AreEqual(Circle.Unknown,
                            calculator.Circle,
                            "Circle");
            Assert.True(double.IsNaN(calculator.Xt1And2.Item1),
                        "Xt1And2.Item1");
            Assert.True(double.IsNaN(calculator.Xt1And2.Item2),
                        "Xt1And2.Item2");
            Assert.True(double.IsNaN(calculator.Yt1And2.Item1),
                        "Yt1And2.Item1");
            Assert.True(double.IsNaN(calculator.Yt1And2.Item2),
                        "Yt1And2.Item2");
        }

        [Test]
        public void DeterminePointsForCircleIsUnknownTest()
        {
            Tuple <Point, Point> actual = m_Calculator.Calculate(Circle.Unknown,
                                                                 m_Xt1And2,
                                                                 m_Yt1And2);

            Assert.AreEqual(Point.Unknown,
                            actual.Item1,
                            "Item1");
            Assert.AreEqual(Point.Unknown,
                            actual.Item2,
                            "Item2");
        }

        [Test]
        public void DeterminePointsForX1IsNaNTest()
        {
            Tuple <double, double> tuple = new Tuple <double, double>(double.NaN,
                                                                      2.0);

            Tuple <Point, Point> actual = m_Calculator.Calculate(m_Circle,
                                                                 tuple,
                                                                 m_Yt1And2);

            Assert.AreEqual(Point.Unknown,
                            actual.Item1,
                            "Item1");
            Assert.AreEqual(Point.Unknown,
                            actual.Item2,
                            "Item2");
        }

        [Test]
        public void DeterminePointsForX2IsNaNTest()
        {
            Tuple <double, double> tuple = new Tuple <double, double>(1.0,
                                                                      double.NaN);

            Tuple <Point, Point> actual = m_Calculator.Calculate(m_Circle,
                                                                 tuple,
                                                                 m_Yt1And2);

            Assert.AreEqual(Point.Unknown,
                            actual.Item1,
                            "Item1");
            Assert.AreEqual(Point.Unknown,
                            actual.Item2,
                            "Item2");
        }

        [Test]
        public void DeterminePointsForY1IsNaNTest()
        {
            Tuple <double, double> tuple = new Tuple <double, double>(double.NaN,
                                                                      2.0);

            Tuple <Point, Point> actual = m_Calculator.Calculate(m_Circle,
                                                                 m_Xt1And2,
                                                                 tuple);

            Assert.AreEqual(Point.Unknown,
                            actual.Item1,
                            "Item1");
            Assert.AreEqual(Point.Unknown,
                            actual.Item2,
                            "Item2");
        }

        [Test]
        public void DeterminePointsForY2IsNaNTest()
        {
            Tuple <double, double> tuple = new Tuple <double, double>(1.0,
                                                                      double.NaN);

            Tuple <Point, Point> actual = m_Calculator.Calculate(m_Circle,
                                                                 m_Xt1And2,
                                                                 tuple);

            Assert.AreEqual(Point.Unknown,
                            actual.Item1,
                            "Item1");
            Assert.AreEqual(Point.Unknown,
                            actual.Item2,
                            "Item2");
        }

        [Test]
        public void DeterminePointsThrowsForPointX1Y1NotOnCirclesTest()
        {
            Point onCircle = m_Circle.PointOnCircle(Angle.For90Degrees);

            Point point1 = new Point(1000.0,
                                     1000.0);
            Point point2 = new Point(onCircle.X,
                                     onCircle.Y);

            Tuple <double, double> tuple1 = new Tuple <double, double>(point1.X,
                                                                       point2.X);
            Tuple <double, double> tuple2 = new Tuple <double, double>(point1.Y,
                                                                       point2.Y);

            Assert.Throws <ArgumentException>(() => m_Calculator.Calculate(m_Circle,
                                                                           tuple1,
                                                                           tuple2));
        }

        [Test]
        public void DeterminePointsThrowsForPointX2Y2NotOnCirclesTest()
        {
            Point onCircle = m_Circle.PointOnCircle(Angle.For90Degrees);

            Point point1 = new Point(onCircle.X,
                                     onCircle.Y);
            Point point2 = new Point(1000.0,
                                     1000.0);

            Tuple <double, double> tuple1 = new Tuple <double, double>(point1.X,
                                                                       point2.X);
            Tuple <double, double> tuple2 = new Tuple <double, double>(point1.Y,
                                                                       point2.Y);

            Assert.Throws <ArgumentException>(() => m_Calculator.Calculate(m_Circle,
                                                                           tuple1,
                                                                           tuple2));
        }

        [Test]
        public void Xt1And2Test()
        {
            Assert.AreEqual(m_Xt1And2.Item1,
                            m_Calculator.Xt1And2.Item1,
                            "Item1");
            Assert.AreEqual(m_Xt1And2.Item2,
                            m_Calculator.Xt1And2.Item2,
                            "Item2");
        }

        [Test]
        public void Yt1And2Test()
        {
            Assert.AreEqual(m_Yt1And2.Item1,
                            m_Calculator.Yt1And2.Item1,
                            "Item1");
            Assert.AreEqual(m_Yt1And2.Item2,
                            m_Calculator.Yt1And2.Item2,
                            "Item2");
        }
    }
}