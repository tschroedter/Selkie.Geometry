using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Shapes;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.Shapes.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class CirclePairTests
    {
        [SetUp]
        public void Setup()
        {
            m_Smaller = new Circle(3.0,
                                   4.0,
                                   5.0);
            m_Bigger = new Circle(30.0,
                                  40.0,
                                  20.0);
            m_ThreeIntersectingTwo = new Circle(45.0,
                                                40.0,
                                                10.0);
            m_ThreeInsideTwoIntersectingTwoPoints = new Circle(45.0,
                                                               40.0,
                                                               5.0);
            m_ThreeInsideTwo = new Circle(30.0,
                                          40.0,
                                          5.0);
            m_ThreeInsideTwoIntersectingOnePoint = new Circle(55.0,
                                                              40.0,
                                                              5.0);
        }

        private ICircle m_Bigger;
        private ICircle m_Smaller;
        private Circle m_ThreeInsideTwo;
        private Circle m_ThreeInsideTwoIntersectingOnePoint;
        private Circle m_ThreeInsideTwoIntersectingTwoPoints;
        private Circle m_ThreeIntersectingTwo;

        [Test]
        public void DistanceTest()
        {
            var pair = new CirclePair(m_Smaller,
                                      m_Bigger);

            NUnitHelper.AssertIsEquivalent(45.0,
                                           pair.Distance,
                                           0.01,
                                           "Distance");
        }

        [Test]
        public void IsUnknownReturnsFalseForKnownTest()
        {
            var pair = new CirclePair(m_Smaller,
                                      m_Bigger);

            Assert.False(pair.IsUnknown);
        }

        [Test]
        public void IsUnknownReturnsTrueForUnknownTest()
        {
            ICirclePair pair = CirclePair.Unknown;

            Assert.True(pair.IsUnknown);
        }

        [Test]
        public void NumberOfTangentsForCircleInsideOtherTest()
        {
            var pair = new CirclePair(m_Bigger,
                                      m_ThreeInsideTwo);

            const int expected = 0;
            int actual = pair.NumberOfTangents;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void NumberOfTangentsForCircleInsideOtherTouchingTest()
        {
            var pair = new CirclePair(m_Bigger,
                                      m_ThreeInsideTwoIntersectingTwoPoints);

            const int expected = 1;
            int actual = pair.NumberOfTangents;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void NumberOfTangentsForCircleNotIntersectingTest()
        {
            var pair = new CirclePair(m_Smaller,
                                      m_Bigger);

            const int expected = 4;
            int actual = pair.NumberOfTangents;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void NumberOfTangentsForIntersectingInOnePointTest()
        {
            var pair = new CirclePair(m_Bigger,
                                      m_ThreeInsideTwoIntersectingOnePoint);

            const int expected = 3;
            int actual = pair.NumberOfTangents;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void NumberOfTangentsForIntersectingTest()
        {
            var pair = new CirclePair(m_Bigger,
                                      m_ThreeIntersectingTwo);

            const int expected = 2;
            int actual = pair.NumberOfTangents;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void OneForFirstIsGreaterTest()
        {
            var pair = new CirclePair(m_Bigger,
                                      m_Smaller);

            Assert.AreEqual(m_Bigger,
                            pair.Zero);
        }

        [Test]
        public void OneForSecondIsGreaterTest()
        {
            var pair = new CirclePair(m_Smaller,
                                      m_Bigger);

            Assert.AreEqual(m_Bigger,
                            pair.Zero);
        }

        [Test]
        public void OneIsUnknownForUnknownTest()
        {
            ICirclePair pair = CirclePair.Unknown;

            Assert.True(pair.One.IsUnknown);
        }

        [Test]
        public void OneTest()
        {
            var pair = new CirclePair(m_Smaller,
                                      m_Bigger);

            Assert.AreEqual(m_Smaller,
                            pair.One);
        }

        [Test]
        public void RadiusZeroForFirstIsGreaterTest()
        {
            var pair = new CirclePair(m_Bigger,
                                      m_Smaller);

            Assert.AreEqual(m_Bigger.Radius,
                            pair.RadiusZero);
        }

        [Test]
        public void RadiusZeroForSecondIsGreaterTest()
        {
            var pair = new CirclePair(m_Smaller,
                                      m_Bigger);

            Assert.AreEqual(m_Bigger.Radius,
                            pair.RadiusZero);
        }

        [Test]
        public void ZeroForFirstIsGreaterTest()
        {
            var pair = new CirclePair(m_Bigger,
                                      m_Smaller);

            Assert.AreEqual(m_Bigger,
                            pair.Zero);
        }

        [Test]
        public void ZeroForSecondIsGreaterTest()
        {
            var pair = new CirclePair(m_Smaller,
                                      m_Bigger);

            Assert.AreEqual(m_Bigger,
                            pair.Zero);
        }

        [Test]
        public void ZeroIsUnknownForUnknownTest()
        {
            ICirclePair pair = CirclePair.Unknown;

            Assert.True(pair.Zero.IsUnknown);
        }

        [Test]
        public void ZeroTest()
        {
            var pair = new CirclePair(m_Smaller,
                                      m_Bigger);

            Assert.AreEqual(m_Smaller,
                            pair.One);
        }
    }
}