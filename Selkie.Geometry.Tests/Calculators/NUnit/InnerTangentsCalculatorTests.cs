using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Tests.Calculators.NUnit
{
    // ReSharper disable once ClassTooBig
    [ExcludeFromCodeCoverage]
    internal sealed class InnerTangentsCalculatorTests
    {
        #region Nested type: CaseOneTests

        [TestFixture]
        internal sealed class CaseOneTests
        {
            private InnerTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

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

                m_Calculator = new InnerTangentsCalculator(m_Pair);
            }

            [Test]
            public void CircleOneInnerTangentPointOneTest()
            {
                Point expected = new Point(7.85,
                                           12.73);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneInnerTangentPointTwoTest()
            {
                Point expected = new Point(12.34,
                                           13.12);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointOneTest()
            {
                Point expected = new Point(13.15,
                                           3.77);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointTwoTest()
            {
                Point expected = new Point(8.66,
                                           3.38);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void HasTangentPointPointsTest()
            {
                Assert.True(m_Calculator.HasTangentPoints);
            }

            [Test]
            public void InnerTangentLinesIntersectionPointTest()
            {
                Point expected = new Point(10.5,
                                           8.25);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPoint");
            }
        }

        #endregion

        #region Nested type: CaseTwoTests

        [TestFixture]
        internal sealed class CaseTwoTests
        {
            private InnerTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(11.0,
                                             0.0),
                                   2.5);
                m_Two = new Circle(new Point(16.0,
                                             2.5),
                                   2.5);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new InnerTangentsCalculator(m_Pair);
            }

            [Test]
            public void CircleOneInnerTangentPointOneTest()
            {
                Point expected = new Point(13.5,
                                           0.0);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneInnerTangentPointTwoTest()
            {
                Point expected = new Point(12.5,
                                           2.0);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointOneTest()
            {
                Point expected = new Point(13.5,
                                           2.5);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointTwoTest()
            {
                Point expected = new Point(14.5,
                                           0.5);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void HasTangentPointPointsTest()
            {
                Assert.True(m_Calculator.HasTangentPoints);
            }

            [Test]
            public void InnerTangentLinesIntersectionPointTest()
            {
                Point expected = new Point(13.5,
                                           1.25);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPoint");
            }
        }

        #endregion

        #region Nested type: CircleOneInsideZeroNumberOfTangentsIsOneTests

        [TestFixture]
        internal sealed class CircleOneInsideZeroNumberOfTangentsIsOneTests
        {
            private InnerTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(2.0,
                                             4.0),
                                   4.0);
                m_Two = new Circle(new Point(3.0,
                                             4.0),
                                   3.0);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new InnerTangentsCalculator(m_Pair);
            }

            [Test]
            public void CircleOneInnerTangentPointOneTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneInnerTangentPointTwoTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointOneTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointTwoTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void HasTangentPointPointsTest()
            {
                Assert.False(m_Calculator.HasTangentPoints);
            }

            [Test]
            public void InnerTangentLinesIntersectionPointTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPoint");
            }
        }

        #endregion

        #region Nested type: NumberOfTangentsIsThreeTests

        [TestFixture]
        internal sealed class NumberOfTangentsIsThreeTests
        {
            private InnerTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(2.0,
                                             4.0),
                                   4.0);
                m_Two = new Circle(new Point(10.0,
                                             4.0),
                                   4.0);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new InnerTangentsCalculator(m_Pair);
            }

            [Test]
            public void CircleOneInnerTangentPointOneTest()
            {
                Point expected = new Point(6.0,
                                           4.0);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneInnerTangentPointTwoTest()
            {
                Point expected = new Point(6.0,
                                           4.0);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointOneTest()
            {
                Point expected = new Point(6.0,
                                           4.0);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointTwoTest()
            {
                Point expected = new Point(6.0,
                                           4.0);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void HasTangentPointPointsTest()
            {
                Assert.True(m_Calculator.HasTangentPoints);
            }

            [Test]
            public void InnerTangentLinesIntersectionPointTest()
            {
                Point expected = new Point(6.0,
                                           4.0);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPoint");
            }
        }

        #endregion

        #region Nested type: RadiusOfOneIsBiggerAndNumberOfTangentsIsFourTests

        [TestFixture]
        internal sealed class RadiusOfOneIsBiggerAndNumberOfTangentsIsFourTests
        {
            private InnerTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

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

                m_Calculator = new InnerTangentsCalculator(m_Pair);
            }

            [Test]
            public void CircleOneInnerTangentPointOneTest()
            {
                Point expected = new Point(-2.32,
                                           1.05);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneInnerTangentPointTwoTest()
            {
                Point expected = new Point(-1.0,
                                           2.0);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointOneTest()
            {
                Point expected = new Point(4.3,
                                           -1.22);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointTwoTest()
            {
                Point expected = new Point(-1.0,
                                           -5);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void HasTangentPointPointsTest()
            {
                Assert.True(m_Calculator.HasTangentPoints);
            }

            [Test]
            public void InnerTangentLinesIntersectionPointTest()
            {
                Point expected = new Point(-1.0,
                                           0.6);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPoint");
            }
        }

        #endregion

        #region Nested type: RadiusOfTwoIsBiggerAndNumberOfTangentsIsFourTests

        [TestFixture]
        internal sealed class RadiusOfTwoIsBiggerAndNumberOfTangentsIsFourTests
        {
            private InnerTangentsCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

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

                m_Calculator = new InnerTangentsCalculator(m_Pair);
            }

            [Test]
            public void CircleOneInnerTangentPointOneTest()
            {
                Point expected = new Point(3.32,
                                           -4.05);
                Point actual = m_Calculator.CircleOneTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleOneInnerTangentPointTwoTest()
            {
                Point expected = new Point(2.0,
                                           -5.0);
                Point actual = m_Calculator.CircleOneTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointOneTest()
            {
                Point expected = new Point(-3.3,
                                           -1.78);
                Point actual = m_Calculator.CircleZeroTangentPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void CircleZeroInnerTangentPointTwoTest()
            {
                Point expected = new Point(2.0,
                                           2.0);
                Point actual = m_Calculator.CircleZeroTangentPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "OuterTangentLinesIntersectionPoint");
            }

            [Test]
            public void HasTangentPointPointsTest()
            {
                Assert.True(m_Calculator.HasTangentPoints);
            }

            [Test]
            public void InnerTangentLinesIntersectionPointTest()
            {
                Point expected = new Point(2.0,
                                           -3.6);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPoint");
            }
        }

        #endregion
    }
}