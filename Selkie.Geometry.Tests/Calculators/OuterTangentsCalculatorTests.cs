using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Tests.Calculators
{
    // ReSharper disable once ClassTooBig
    [ExcludeFromCodeCoverage]
    internal sealed class OuterTangentsCalculatorTests
    {
        [TestFixture]
        internal sealed class RadiusOfOneIsBiggerAndNumberOfTangentsIsFourTests
        {
            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(3.0,
                                             -5.0),
                                   4.0);
                m_Two = new Circle(new Point(-2.0,
                                             2.0),
                                   1.0);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentsCalculator(m_Pair);
            }

            private OuterTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [Test]
            public void CircleOneOuterTangentPointOneTest()
            {
                var expected = new Point(-1.44,
                                         2.83);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneOuterTangentPointTwoTest()
            {
                var expected = new Point(-2.97,
                                         1.74);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointOneTest()
            {
                var expected = new Point(5.24,
                                         -1.69);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointTwoTest()
            {
                var expected = new Point(-0.86,
                                         -6.04);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void OuterTangentLinesIntersectionPointTest()
            {
                var expected = new Point(-3.67,
                                         4.33);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPoint");
            }
        }

        [TestFixture]
        internal sealed class RadiusOfTwoIsBiggerAndNumberOfTangentsIsFourTests
        {
            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(3.0,
                                             -5.0),
                                   1.0);
                m_Two = new Circle(new Point(-2.0,
                                             2.0),
                                   4.0);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentsCalculator(m_Pair);
            }

            private OuterTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [Test]
            public void CircleOneOuterTangentPointOneTest()
            {
                var expected = new Point(2.44,
                                         -5.83);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneOuterTangentPointTwoTest()
            {
                var expected = new Point(3.97,
                                         -4.74);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointOneTest()
            {
                var expected = new Point(-4.24,
                                         -1.31);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointTwoTest()
            {
                var expected = new Point(1.86,
                                         3.04);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void OuterTangentLinesIntersectionPointTest()
            {
                var expected = new Point(4.67,
                                         -7.33);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPoint");
            }
        }

        [TestFixture]
        internal sealed class RadiusSameAndOneTangentTests
        {
            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(11.0,
                                             2.5),
                                   2.5);
                m_Two = new Circle(new Point(12.0,
                                             2.5),
                                   1.5);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentsCalculator(m_Pair);
            }

            private OuterTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [Test]
            public void CircleOneOuterTangentPointOneTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneOuterTangentPointTwoTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointOneTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointTwoTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void IntersectionPointTest()
            {
                Point actual = m_Calculator.IntersectionPoint;

                Assert.True(actual.IsUnknown);
            }
        }

        [TestFixture]
        internal sealed class RadiusSameCaseFourTests
        {
            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(11.0,
                                             2.5),
                                   2.5);
                m_Two = new Circle(new Point(10.0,
                                             0.0),
                                   2.5);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentsCalculator(m_Pair);
            }

            private OuterTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [Test]
            public void CircleOneOuterTangentPointOneTest()
            {
                var expected = new Point(8.68,
                                         3.42);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneOuterTangentPointTwoTest()
            {
                var expected = new Point(13.32,
                                         1.57);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointOneTest()
            {
                var expected = new Point(12.32,
                                         -0.93);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointTwoTest()
            {
                var expected = new Point(7.68,
                                         0.93);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void IntersectionPointTest()
            {
                Point actual = m_Calculator.IntersectionPoint;

                Assert.True(double.IsInfinity(actual.X),
                            "X");
                Assert.True(double.IsInfinity(actual.Y),
                            "Y");
            }
        }

        [TestFixture]
        internal sealed class RadiusSameCaseOneTests
        {
            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(2.0,
                                             2.0),
                                   2.0);
                m_Two = new Circle(new Point(7.0,
                                             6.0),
                                   2.0);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentsCalculator(m_Pair);
            }

            private OuterTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [Test]
            public void CalculateTangenPointsForOneBothSameRadiusTest()
            {
                var one = new Circle(new Point(2.0,
                                               2.0),
                                     2.0);
                var two = new Circle(new Point(7.0,
                                               6.0),
                                     2.0);
                var pair = new CirclePair(one,
                                          two);

                Tuple <Point, Point> actual = m_Calculator.CalculateTangenPointsForOneBothSameRadius(pair);

                Assert.AreEqual(new Point(0.75,
                                          3.56),
                                actual.Item1,
                                "Item1");
                Assert.AreEqual(new Point(3.25,
                                          0.44),
                                actual.Item2,
                                "Item2");
            }

            [Test]
            public void CalculateTangenPointsForZeroBothSameRadiusTest()
            {
                var one = new Circle(new Point(2.0,
                                               2.0),
                                     2.0);
                var two = new Circle(new Point(7.0,
                                               6.0),
                                     2.0);
                var pair = new CirclePair(one,
                                          two);

                Tuple <Point, Point> actual = m_Calculator.CalculateTangenPointsForZeroBothSameRadius(pair);

                Assert.AreEqual(new Point(5.75,
                                          7.56),
                                actual.Item1,
                                "Item1");
                Assert.AreEqual(new Point(8.25,
                                          4.44),
                                actual.Item2,
                                "Item2");
            }
        }

        [TestFixture]
        internal sealed class RadiusSameCaseThreeTests
        {
            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(16.0,
                                             2.5),
                                   2.5);
                m_Two = new Circle(new Point(11.0,
                                             0.0),
                                   2.5);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentsCalculator(m_Pair);
            }

            private OuterTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [Test]
            public void CircleOneOuterTangentPointOneTest()
            {
                var expected = new Point(14.88,
                                         4.74);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneOuterTangentPointTwoTest()
            {
                var expected = new Point(17.12,
                                         0.26);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointOneTest()
            {
                var expected = new Point(12.12,
                                         -2.24);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointTwoTest()
            {
                var expected = new Point(9.88,
                                         2.24);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void IntersectionPointTest()
            {
                Point actual = m_Calculator.IntersectionPoint;

                Assert.True(double.IsInfinity(actual.X),
                            "X");
                Assert.True(double.IsInfinity(actual.Y),
                            "Y");
            }
        }

        [TestFixture]
        internal sealed class RadiusSameCaseTwoTests
        {
            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(10.0,
                                             14.0),
                                   2.5);
                m_Two = new Circle(new Point(11.0,
                                             2.5),
                                   2.5);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentsCalculator(m_Pair);
            }

            private OuterTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [Test]
            public void CircleOneOuterTangentPointOneTest()
            {
                var expected = new Point(7.51,
                                         13.78);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneOuterTangentPointTwoTest()
            {
                var expected = new Point(12.49,
                                         14.22);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointOneTest()
            {
                var expected = new Point(13.49,
                                         2.72);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroOuterTangentPointTwoTest()
            {
                var expected = new Point(8.51,
                                         2.28);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void IntersectionPointTest()
            {
                Point actual = m_Calculator.IntersectionPoint;

                Assert.True(double.IsInfinity(actual.X),
                            "X");
                Assert.True(double.IsInfinity(actual.Y),
                            "Y");
            }
        }
    }
}