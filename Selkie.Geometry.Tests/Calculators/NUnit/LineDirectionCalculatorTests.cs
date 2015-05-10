using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Shapes;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Tests.Calculators.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class LineDirectionCalculatorTests
    {
        private LineDirectionCalculator m_Calculator;
        private ILine m_Line;
        private Point m_Point;

        [SetUp]
        public void Setup()
        {
            m_Line = new Line(new Point(0.0,
                                        0.0),
                              new Point(10.0,
                                        10.0));
            m_Point = new Point(5.0,
                                0.0);

            m_Calculator = new LineDirectionCalculator(m_Line,
                                                       m_Point);
        }

        [Test]
        public void CalculateForLeftReturnsCounterclockwiseTest()
        {
            Point startPoint = new Point(1,
                                         0);
            Point endPoint = new Point(0,
                                       0);
            Line line = new Line(startPoint,
                                 endPoint);
            Point point = new Point(-1,
                                    -1);

            Constants.TurnDirection actual = m_Calculator.Calculate(line,
                                                                    point);

            Assert.AreEqual(Constants.TurnDirection.Counterclockwise,
                            actual);
        }

        [Test]
        public void CalculateForRightReturnsClockwiseTest()
        {
            Point startPoint = new Point(1,
                                         0);
            Point endPoint = new Point(0,
                                       0);
            Line line = new Line(startPoint,
                                 endPoint);
            Point point = new Point(1,
                                    1);

            Constants.TurnDirection actual = m_Calculator.Calculate(line,
                                                                    point);

            Assert.AreEqual(Constants.TurnDirection.Clockwise,
                            actual);
        }

        [Test]
        public void CalculateForZeroTest()
        {
            Point startPoint = new Point(1,
                                         0);
            Point endPoint = new Point(0,
                                       0);
            Line line = new Line(startPoint,
                                 endPoint);
            Point point = new Point(-1,
                                    0);

            Constants.TurnDirection actual = m_Calculator.Calculate(line,
                                                                    point);

            Assert.AreEqual(Constants.TurnDirection.Unknown,
                            actual);
        }

        [Test]
        public void DirectionForLeftReturnsCounterclockwiseTest()
        {
            Point startPoint = new Point(1,
                                         0);
            Point endPoint = new Point(0,
                                       0);
            Line line = new Line(startPoint,
                                 endPoint);
            Point point = new Point(-1,
                                    -1);

            LineDirectionCalculator calculator = new LineDirectionCalculator(line,
                                                                             point);

            Assert.AreEqual(Constants.TurnDirection.Counterclockwise,
                            calculator.Direction);
        }

        [Test]
        public void DirectionForRightReturnsClockwiseTest()
        {
            Point startPoint = new Point(1,
                                         0);
            Point endPoint = new Point(0,
                                       0);
            Line line = new Line(startPoint,
                                 endPoint);
            Point point = new Point(1,
                                    1);

            LineDirectionCalculator calculator = new LineDirectionCalculator(line,
                                                                             point);

            Assert.AreEqual(Constants.TurnDirection.Clockwise,
                            calculator.Direction);
        }

        [Test]
        public void DirectionForZeroTest()
        {
            Point startPoint = new Point(1,
                                         0);
            Point endPoint = new Point(0,
                                       0);
            Line line = new Line(startPoint,
                                 endPoint);
            Point point = new Point(-1,
                                    0);

            LineDirectionCalculator calculator = new LineDirectionCalculator(line,
                                                                             point);

            Assert.AreEqual(Constants.TurnDirection.Unknown,
                            calculator.Direction);
        }

        [Test]
        public void HorizontalTest()
        {
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(1,
                                                  -1,
                                                  10,
                                                  -1,
                                                  0,
                                                  0),
                            "horizontal 1");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(10,
                                                  -1,
                                                  1,
                                                  -1,
                                                  0,
                                                  0),
                            "horizontal 2");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(1,
                                                  -1,
                                                  10,
                                                  -1,
                                                  0,
                                                  -9),
                            "horizontal 3");
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(10,
                                                  -1,
                                                  1,
                                                  -1,
                                                  0,
                                                  -9),
                            "horizontal 4");
            Assert.AreEqual(LineDirectionCalculator.Side.Unknown,
                            m_Calculator.FindSide(-50,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0),
                            "horizontal 5");
            Assert.AreEqual(LineDirectionCalculator.Side.Unknown,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  -50,
                                                  0,
                                                  -50,
                                                  0),
                            "horizontal 6");
        }

        [Test]
        public void LessEpsilonSlopeTest()
        {
            const double halfEpsilon = SelkieConstants.EpsilonDistance / 2.0;

            Line line = new Line(new Point(0.0,
                                           0.0),
                                 new Point(halfEpsilon,
                                           halfEpsilon));
            Point point = new Point(5.0,
                                    0.0);

            m_Calculator = new LineDirectionCalculator(line,
                                                       point);
        }

        [Test]
        public void LineTest()
        {
            Assert.AreEqual(m_Line,
                            m_Calculator.Line);
        }

        [Test]
        public void MixedTest()
        {
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(0,
                                                  5,
                                                  1,
                                                  10,
                                                  10,
                                                  20),
                            "2");
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(0,
                                                  9.1,
                                                  1,
                                                  10,
                                                  10,
                                                  20),
                            "2.1");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(0,
                                                  5,
                                                  1,
                                                  10,
                                                  20,
                                                  10),
                            "2.2");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(0,
                                                  9.1,
                                                  1,
                                                  10,
                                                  20,
                                                  10),
                            "2.3");
        }

        [Test]
        public void NegativeOneTest()
        {
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(1,
                                                  0,
                                                  0,
                                                  0,
                                                  1,
                                                  1),
                            "-1");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(12,
                                                  0,
                                                  0,
                                                  0,
                                                  2,
                                                  1),
                            "-1.1");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(-25,
                                                  0,
                                                  0,
                                                  0,
                                                  -1,
                                                  -14),
                            "-1.2");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(1,
                                                  0.5,
                                                  0,
                                                  0,
                                                  1,
                                                  1),
                            "-1.3");
        }

        [Test]
        public void NegativeSlopeTest()
        {
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  -10,
                                                  10,
                                                  1,
                                                  2),
                            "negative slope 1");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  -10,
                                                  10,
                                                  1,
                                                  2),
                            "negative slope 2");
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  -10,
                                                  10,
                                                  -1,
                                                  -2),
                            "negative slope 3");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(-10,
                                                  10,
                                                  0,
                                                  0,
                                                  -1,
                                                  -2),
                            "negative slope 4");
        }

        [Test]
        public void OneTest()
        {
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(1,
                                                  0,
                                                  0,
                                                  0,
                                                  -1,
                                                  -1),
                            "1");
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(25,
                                                  0,
                                                  0,
                                                  0,
                                                  -1,
                                                  -14),
                            "1.1");
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(25,
                                                  20,
                                                  0,
                                                  20,
                                                  -1,
                                                  6),
                            "1.2");
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(24,
                                                  20,
                                                  -1,
                                                  20,
                                                  -2,
                                                  6),
                            "1.3");
        }

        [Test]
        public void PointTest()
        {
            Assert.AreEqual(m_Point,
                            m_Calculator.Point);
        }

        [Test]
        public void PositiveSlopeTest()
        {
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  10,
                                                  10,
                                                  1,
                                                  2),
                            "positive slope 1");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(10,
                                                  10,
                                                  0,
                                                  0,
                                                  1,
                                                  2),
                            "positive slope 2");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  10,
                                                  10,
                                                  1,
                                                  0),
                            "positive slope 3");
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(10,
                                                  10,
                                                  0,
                                                  0,
                                                  1,
                                                  0),
                            "positive slope 4");
        }

        [Test]
        public void VerticalTest()
        {
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(1,
                                                  1,
                                                  1,
                                                  10,
                                                  0,
                                                  0),
                            "vertical 1");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(1,
                                                  10,
                                                  1,
                                                  1,
                                                  0,
                                                  0),
                            "vertical 2");
            Assert.AreEqual(LineDirectionCalculator.Side.Right,
                            m_Calculator.FindSide(1,
                                                  1,
                                                  1,
                                                  10,
                                                  5,
                                                  0),
                            "vertical 3");
            Assert.AreEqual(LineDirectionCalculator.Side.Left,
                            m_Calculator.FindSide(1,
                                                  10,
                                                  1,
                                                  1,
                                                  5,
                                                  0),
                            "vertical 4");
            Assert.AreEqual(LineDirectionCalculator.Side.Unknown,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  -50,
                                                  0,
                                                  0,
                                                  0),
                            "horizontal 5");
            Assert.AreEqual(LineDirectionCalculator.Side.Unknown,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  -50,
                                                  0,
                                                  -50,
                                                  0),
                            "horizontal 6");
        }

        [Test]
        public void ZeroTest()
        {
            Assert.AreEqual(LineDirectionCalculator.Side.Unknown,
                            m_Calculator.FindSide(1,
                                                  0,
                                                  0,
                                                  0,
                                                  -1,
                                                  0),
                            "0");
            Assert.AreEqual(LineDirectionCalculator.Side.Unknown,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0),
                            "0.1");
            Assert.AreEqual(LineDirectionCalculator.Side.Unknown,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  0,
                                                  1,
                                                  0,
                                                  2),
                            "0.2");
            Assert.AreEqual(LineDirectionCalculator.Side.Unknown,
                            m_Calculator.FindSide(0,
                                                  0,
                                                  2,
                                                  0,
                                                  1,
                                                  0),
                            "0.3");
            Assert.AreEqual(LineDirectionCalculator.Side.Unknown,
                            m_Calculator.FindSide(1,
                                                  -2,
                                                  0,
                                                  0,
                                                  -1,
                                                  2),
                            "0.4");
        }
    }
}