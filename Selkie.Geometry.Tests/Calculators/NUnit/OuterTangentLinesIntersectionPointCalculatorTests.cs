using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Tests.Calculators.NUnit
{
    [ExcludeFromCodeCoverage]
    internal sealed class OuterTangentLinesIntersectionPointCalculatorTests
    {
        #region Nested type: QuadrantFourAndTwoTests

        [TestFixture]
        internal sealed class QuadrantFourAndTwoTests
        {
            private OuterTangentLinesIntersectionPointCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(10.0,
                                             -10.0),
                                   5.0);
                m_Two = new Circle(new Point(-20.0,
                                             20.0),
                                   10.0);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentLinesIntersectionPointCalculator(m_Pair);
            }

            [Test]
            public void IntersectionPointTest()
            {
                Point expected = new Point(40.0,
                                           -40.0);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual);
            }

            [Test]
            public void IsUnknownTest()
            {
                Assert.False(m_Calculator.IsUnknown);
            }
        }

        #endregion

        #region Nested type: QuadrantOneAndThreeTests

        [TestFixture]
        internal sealed class QuadrantOneAndThreeTests
        {
            private OuterTangentLinesIntersectionPointCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(10.0,
                                             10.0),
                                   5.0);
                m_Two = new Circle(new Point(-20.0,
                                             -20.0),
                                   10.0);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentLinesIntersectionPointCalculator(m_Pair);
            }

            [Test]
            public void IntersectionPointTest()
            {
                Point expected = new Point(40.0,
                                           40.0);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual);
            }

            [Test]
            public void IsUnknownTest()
            {
                Assert.False(m_Calculator.IsUnknown);
            }
        }

        #endregion

        #region Nested type: QuadrantThreeAndOneTests

        [TestFixture]
        internal sealed class QuadrantThreeAndOneTests
        {
            private OuterTangentLinesIntersectionPointCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(-10.0,
                                             -10.0),
                                   5.0);
                m_Two = new Circle(new Point(20.0,
                                             20.0),
                                   10.0);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentLinesIntersectionPointCalculator(m_Pair);
            }

            [Test]
            public void IntersectionPointTest()
            {
                Point expected = new Point(-40.0,
                                           -40.0);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual);
            }

            [Test]
            public void IsUnknownTest()
            {
                Assert.False(m_Calculator.IsUnknown);
            }
        }

        #endregion

        #region Nested type: QuadrantTwoAndFourTests

        [TestFixture]
        internal sealed class QuadrantTwoAndFourTests
        {
            private OuterTangentLinesIntersectionPointCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(-10.0,
                                             10.0),
                                   5.0);
                m_Two = new Circle(new Point(20.0,
                                             -20.0),
                                   10.0);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new OuterTangentLinesIntersectionPointCalculator(m_Pair);
            }

            [Test]
            public void IntersectionPointTest()
            {
                Point expected = new Point(-40.0,
                                           40.0);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(expected,
                                actual);
            }

            [Test]
            public void IsUnknownTest()
            {
                Assert.False(m_Calculator.IsUnknown);
            }
        }

        #endregion

        #region Nested type: UnknownTests

        [TestFixture]
        internal sealed class UnknownTests
        {
            private OuterTangentLinesIntersectionPointCalculator m_Calculator;

            [SetUp]
            public void Setup()
            {
                m_Calculator = OuterTangentLinesIntersectionPointCalculator.Unknown;
            }

            [Test]
            public void UnknownTest()
            {
                Assert.True(m_Calculator.IsUnknown,
                            "IsUnknown");
                Assert.AreEqual(Point.Unknown,
                                m_Calculator.IntersectionPoint,
                                "IntersectionPoint");
            }
        }

        #endregion
    }
}