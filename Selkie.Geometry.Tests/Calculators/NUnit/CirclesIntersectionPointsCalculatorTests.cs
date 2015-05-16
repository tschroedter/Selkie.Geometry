using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Tests.Calculators.NUnit
{
    // ReSharper disable once ClassTooBig
    [ExcludeFromCodeCoverage]
    internal sealed class CirclesIntersectionPointsCalculatorTests
    {
        #region Nested type: CalculatePointsForSameYTests

        [TestFixture]
        internal sealed class CalculatePointsForSameYTests
        {
            [SetUp]
            public void Setup()
            {
                m_Two = new Circle(1.0,
                                   20.0,
                                   3.0);
                m_One = new Circle(10.0,
                                   20.0,
                                   1.0);
                m_CirclePair = new CirclePair(m_One,
                                              m_Two);

                m_Calculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
            }

            private CirclesIntersectionPointsCalculator m_Calculator;
            private CirclePair m_CirclePair;
            private Circle m_One;
            private Circle m_Two;

            [Test]
            public void ZeroXBiggerThanOneXTest()
            {
                var circle = new Circle(new Point(10.0,
                                                  20.0),
                                        1.0);
                var pair = new CirclePair(m_One,
                                          circle);

                var expected = new Tuple <Point, Point>(new Point(9.0,
                                                                  20.0),
                                                        new Point(11.0,
                                                                  20));
                Tuple <Point, Point> actual = m_Calculator.CalculatePointsForSameY(pair);

                Assert.AreEqual(expected,
                                actual);
            }

            [Test]
            public void ZeroXLessThanOneXTest()
            {
                var expected = new Tuple <Point, Point>(new Point(4.0,
                                                                  20.0),
                                                        new Point(9.0,
                                                                  20));
                Tuple <Point, Point> actual = m_Calculator.CalculatePointsForSameY(m_CirclePair);

                Assert.AreEqual(expected,
                                actual);
            }
        }

        #endregion

        #region Nested type: CalculatePointsForSpecialCasesTests

        [TestFixture]
        internal sealed class CalculatePointsForSpecialCasesTests
        {
            [SetUp]
            public void Setup()
            {
                m_CircleOne = new Circle(1.0,
                                         -2.0,
                                         3.0);
                m_CircleZero = new Circle(3.0,
                                          -2.0,
                                          4.0);
                m_CirclePair = new CirclePair(m_CircleZero,
                                              m_CircleOne);

                m_Calculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
            }

            private CirclesIntersectionPointsCalculator m_Calculator;
            private Circle m_CircleOne;
            private CirclePair m_CirclePair;
            private Circle m_CircleZero;

            [Test]
            public void DeltaXMinusDoubleRadiusLessInsideEpsilonTest()
            {
                var one = new Circle(1.0,
                                     -2.0,
                                     3.0);
                var two = new Circle(7.0,
                                     -2.0,
                                     3.0);
                var pair = new CirclePair(one,
                                          two);

                var expected = new Tuple <Point, Point>(new Point(4.0,
                                                                  -2.0),
                                                        new Point(4.0,
                                                                  -2.0));
                Tuple <Point, Point> actual = m_Calculator.CalculatePointsForSpecialCases(pair);

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPointOne");
            }

            [Test]
            public void DeltaYMinusDoubleRadiusLessInsideEpsilonTest()
            {
                var one = new Circle(1.0,
                                     -1.0,
                                     3.0);
                var two = new Circle(1.0,
                                     5.0,
                                     3.0);
                var pair = new CirclePair(one,
                                          two);

                var expected = new Tuple <Point, Point>(new Point(1.0,
                                                                  2.0),
                                                        new Point(1.0,
                                                                  2.0));
                Tuple <Point, Point> actual = m_Calculator.CalculatePointsForSpecialCases(pair);

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPointOne");
            }
        }

        #endregion

        #region Nested type: CalculatePointsSameXTests

        [TestFixture]
        internal sealed class CalculatePointsSameXTests
        {
            [SetUp]
            public void Setup()
            {
                m_Two = new Circle(1.0,
                                   2.0,
                                   3.0);
                m_One = new Circle(1.0,
                                   20.0,
                                   1.0);
                m_CirclePair = new CirclePair(m_One,
                                              m_Two);

                m_Calculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
            }

            private CirclesIntersectionPointsCalculator m_Calculator;
            private CirclePair m_CirclePair;
            private Circle m_One;
            private Circle m_Two;

            [Test]
            public void ZeroYBiggerThanOneYTest()
            {
                var circle = new Circle(new Point(10.0,
                                                  20.0),
                                        1.0);
                var pair = new CirclePair(m_One,
                                          circle);

                var expected = new Tuple <Point, Point>(new Point(10.0,
                                                                  19.0),
                                                        new Point(1.0,
                                                                  21));
                Tuple <Point, Point> actual = m_Calculator.CalculatePointsSameX(pair);

                Assert.AreEqual(expected,
                                actual);
            }

            [Test]
            public void ZeroYLessThanOneYTest()
            {
                var expected = new Tuple <Point, Point>(new Point(1.0,
                                                                  5.0),
                                                        new Point(1.0,
                                                                  17));
                Tuple <Point, Point> actual = m_Calculator.CalculatePointsSameX(m_CirclePair);

                Assert.AreEqual(expected,
                                actual);
            }
        }

        #endregion

        #region Nested type: CircleInsideCircleNoIntersectionPointsTests

        [TestFixture]
        internal sealed class CircleInsideCircleNoIntersectionPointsTests
        {
            [SetUp]
            public void Setup()
            {
                m_CircleOne = new Circle(1.0,
                                         2.0,
                                         3.0);
                m_CircleZero = new Circle(1.0,
                                          2.0,
                                          1.0);
                m_CirclePair = new CirclePair(m_CircleZero,
                                              m_CircleOne);

                m_Calculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
            }

            private CirclesIntersectionPointsCalculator m_Calculator;
            private Circle m_CircleOne;
            private CirclePair m_CirclePair;
            private Circle m_CircleZero;

            [Test]
            public void HasIntersectionPointsTest()
            {
                Assert.False(m_Calculator.HasIntersectionPoints);
            }

            [Test]
            public void IntersectionPointOneTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.IntersectionPointOne;

                Assert.AreEqual(actual,
                                expected,
                                "IntersectionPointOne");
            }

            [Test]
            public void IntersectionPointTwoTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.IntersectionPointTwo;

                Assert.AreEqual(actual,
                                expected,
                                "IntersectionPointTwo");
            }

            [Test]
            public void IsCirclesAreSameTest()
            {
                Assert.False(m_Calculator.IsCirclesAreSame);
            }

            [Test]
            public void IsCirclesTouchAtSinglePointTest()
            {
                Assert.False(m_Calculator.IsCirclesTouchAtSinglePoint);
            }
        }

        #endregion

        #region Nested type: CircleInsideCircleOneIntersectionPointsTests

        [TestFixture]
        internal sealed class CircleInsideCircleOneIntersectionPointsTests
        {
            [SetUp]
            public void Setup()
            {
                m_CircleOne = new Circle(1.0,
                                         2.0,
                                         3.0);
                m_CircleZero = new Circle(2.0,
                                          2.0,
                                          2.0);
                m_CirclePair = new CirclePair(m_CircleZero,
                                              m_CircleOne);

                m_Calculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
            }

            private CirclesIntersectionPointsCalculator m_Calculator;
            private Circle m_CircleOne;
            private CirclePair m_CirclePair;
            private Circle m_CircleZero;

            [Test]
            public void HasIntersectionPointsTest()
            {
                Assert.True(m_Calculator.HasIntersectionPoints);
            }

            [Test]
            public void IntersectionPointOneTest()
            {
                var expected = new Point(4.0,
                                         2.0);
                Point actual = m_Calculator.IntersectionPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPointOne");
            }

            [Test]
            public void IntersectionPointTwoTest()
            {
                var expected = new Point(4.0,
                                         2.0);
                Point actual = m_Calculator.IntersectionPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPointTwo");
            }

            [Test]
            public void IsCirclesAreSameTest()
            {
                Assert.False(m_Calculator.IsCirclesAreSame);
            }

            [Test]
            public void IsCirclesTouchAtSinglePointTest()
            {
                Assert.True(m_Calculator.IsCirclesTouchAtSinglePoint);
            }
        }

        #endregion

        #region Nested type: CircleOutsideCircleNoIntersectionPointsTests

        [TestFixture]
        internal sealed class CircleOutsideCircleNoIntersectionPointsTests
        {
            [SetUp]
            public void Setup()
            {
                m_CircleOne = new Circle(1.0,
                                         2.0,
                                         3.0);
                m_CircleZero = new Circle(1.0,
                                          20.0,
                                          1.0);
                m_CirclePair = new CirclePair(m_CircleZero,
                                              m_CircleOne);

                m_Calculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
            }

            private CirclesIntersectionPointsCalculator m_Calculator;
            private Circle m_CircleOne;
            private CirclePair m_CirclePair;
            private Circle m_CircleZero;

            [Test]
            public void HasIntersectionPointsTest()
            {
                Assert.False(m_Calculator.HasIntersectionPoints);
            }

            [Test]
            public void IntersectionPointOneTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.IntersectionPointOne;

                Assert.AreEqual(actual,
                                expected,
                                "IntersectionPointOne");
            }

            [Test]
            public void IntersectionPointTwoTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.IntersectionPointTwo;

                Assert.AreEqual(actual,
                                expected,
                                "IntersectionPointTwo");
            }

            [Test]
            public void IsCirclesAreSameTest()
            {
                Assert.False(m_Calculator.IsCirclesAreSame);
            }

            [Test]
            public void IsCirclesTouchAtSinglePointTest()
            {
                Assert.False(m_Calculator.IsCirclesTouchAtSinglePoint);
            }
        }

        #endregion

        #region Nested type: CircleOutsideCircleOneIntersectionPointsTests

        [TestFixture]
        internal sealed class CircleOutsideCircleOneIntersectionPointsTests
        {
            [SetUp]
            public void Setup()
            {
                m_CircleOne = new Circle(1.0,
                                         2.0,
                                         3.0);
                m_CircleZero = new Circle(7.0,
                                          2.0,
                                          3.0);
                m_CirclePair = new CirclePair(m_CircleZero,
                                              m_CircleOne);

                m_Calculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
            }

            private CirclesIntersectionPointsCalculator m_Calculator;
            private Circle m_CircleOne;
            private CirclePair m_CirclePair;
            private Circle m_CircleZero;

            [Test]
            public void HasIntersectionPointsTest()
            {
                Assert.True(m_Calculator.HasIntersectionPoints);
            }

            [Test]
            public void IntersectionPointOneTest()
            {
                var expected = new Point(4.0,
                                         2.0);
                Point actual = m_Calculator.IntersectionPointOne;

                Assert.AreEqual(actual,
                                expected,
                                "IntersectionPointOne");
            }

            [Test]
            public void IntersectionPointTwoTest()
            {
                var expected = new Point(4.0,
                                         2.0);
                Point actual = m_Calculator.IntersectionPointTwo;

                Assert.AreEqual(actual,
                                expected,
                                "IntersectionPointTwo");
            }

            [Test]
            public void IsCirclesAreSameTest()
            {
                Assert.False(m_Calculator.IsCirclesAreSame);
            }

            [Test]
            public void IsCirclesTouchAtSinglePointTest()
            {
                Assert.True(m_Calculator.IsCirclesTouchAtSinglePoint);
            }
        }

        #endregion

        #region Nested type: SameCirclesTests

        [TestFixture]
        internal sealed class SameCirclesTests
        {
            [SetUp]
            public void Setup()
            {
                m_CircleOne = new Circle(1.0,
                                         2.0,
                                         3.0);
                m_CircleZero = new Circle(1.0,
                                          2.0,
                                          3.0);
                m_CirclePair = new CirclePair(m_CircleZero,
                                              m_CircleOne);

                m_Calculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
            }

            private CirclesIntersectionPointsCalculator m_Calculator;
            private Circle m_CircleOne;
            private CirclePair m_CirclePair;
            private Circle m_CircleZero;

            [Test]
            public void HasIntersectionPointsTest()
            {
                Assert.False(m_Calculator.HasIntersectionPoints);
            }

            [Test]
            public void IntersectionPointOneTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.IntersectionPointOne;

                Assert.AreEqual(actual,
                                expected,
                                "IntersectionPointOne");
            }

            [Test]
            public void IntersectionPointTwoTest()
            {
                Point expected = Point.Unknown;
                Point actual = m_Calculator.IntersectionPointTwo;

                Assert.AreEqual(actual,
                                expected,
                                "IntersectionPointTwo");
            }

            [Test]
            public void IsCirclesAreSameTest()
            {
                Assert.True(m_Calculator.IsCirclesAreSame);
            }

            [Test]
            public void IsCirclesTouchAtSinglePointTest()
            {
                Assert.False(m_Calculator.IsCirclesTouchAtSinglePoint);
            }
        }

        #endregion

        #region Nested type: TwoIntersectionPointsTests

        [TestFixture]
        internal sealed class TwoIntersectionPointsTests
        {
            [SetUp]
            public void Setup()
            {
                m_CircleOne = new Circle(1.0,
                                         2.0,
                                         3.0);
                m_CircleZero = new Circle(3.0,
                                          -1.0,
                                          4.0);
                m_CirclePair = new CirclePair(m_CircleZero,
                                              m_CircleOne);

                m_Calculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
            }

            private CirclesIntersectionPointsCalculator m_Calculator;
            private Circle m_CircleOne;
            private CirclePair m_CirclePair;
            private Circle m_CircleZero;

            [Test]
            public void HasIntersectionPointsTest()
            {
                Assert.True(m_Calculator.HasIntersectionPoints);
            }

            [Test]
            public void IntersectionPointOneTest()
            {
                var expected = new Point(3.86,
                                         2.91);
                Point actual = m_Calculator.IntersectionPointOne;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPointOne");
            }

            [Test]
            public void IntersectionPointTwoTest()
            {
                var expected = new Point(-0.38,
                                         0.078);
                Point actual = m_Calculator.IntersectionPointTwo;

                Assert.AreEqual(expected,
                                actual,
                                "IntersectionPointTwo");
            }

            [Test]
            public void IsCirclesAreSameTest()
            {
                Assert.False(m_Calculator.IsCirclesAreSame);
            }

            [Test]
            public void IsCirclesTouchAtSinglePointTest()
            {
                Assert.False(m_Calculator.IsCirclesTouchAtSinglePoint);
            }
        }

        #endregion
    }
}