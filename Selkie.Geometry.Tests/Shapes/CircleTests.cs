using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.NUnit.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Tests.Shapes
{
    // ReSharper disable once ClassTooBig
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class CircleTests
    {
        [SetUp]
        public void Setup()
        {
            m_CentrePoint = new Point(3.0,
                                      4.0);
            m_RadiusOne = 2.0;
            m_One = new Circle(m_CentrePoint,
                               m_RadiusOne);
            m_Two = new Circle(5.0,
                               6.0,
                               1.0);
        }

        private Point m_CentrePoint;
        private Circle m_One;
        private double m_RadiusOne;
        private Circle m_Two;

        private void AssertGetAngleRelativeToXAxis(double degrees,
                                                   double expectedDegrees)
        {
            // ReSharper disable MaximumChainedReferences
            double radians = Angle.FromDegrees(degrees).Radians;
            double expectedRadians = Angle.FromDegrees(expectedDegrees).Radians;
            // ReSharper restore MaximumChainedReferences

            double x = m_CentrePoint.X + m_RadiusOne * Math.Cos(radians);
            double y = m_CentrePoint.Y + m_RadiusOne * Math.Sin(radians);

            var point = new Point(x,
                                  y);

            Angle actual = m_One.GetAngleRelativeToXAxis(point);

            NUnitHelper.AssertIsEquivalent(expectedRadians,
                                           actual.Radians,
                                           0.01,
                                           "Radians");
        }

        [Test]
        public void AngleBetweenPointsClockwiseTest()
        {
            var point1 = new Point(5.0,
                                   4.0);
            var point2 = new Point(3.0,
                                   6.0);

            Angle expected = Angle.For90Degrees;
            Angle actual = m_One.AngleBetweenPointsClockwise(point1,
                                                             point2);

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void CentrePointTest()
        {
            Assert.AreEqual(m_CentrePoint,
                            m_One.CentrePoint);
        }

        [Test]
        public void ConstructorDistanceTest()
        {
            const double radius = 2.0;

            var circle = new Circle(m_CentrePoint,
                                    radius);

            Assert.AreEqual(m_CentrePoint,
                            circle.CentrePoint,
                            "CentrePoint");
            Assert.AreEqual(radius,
                            circle.Radius,
                            "Radius");
        }

        [Test]
        public void DetermineIsUTurnRequiredReturnsFalseForTouchingCirclesTest()
        {
            var one = new Circle(0.0,
                                 0.0,
                                 100);
            var two = new Circle(100.0,
                                 0.0,
                                 100);

            bool actual = one.Intersects(two);

            Assert.True(actual);
        }

        [Test]
        public void DetermineIsUTurnRequiredReturnsFalseTest()
        {
            var one = new Circle(0.0,
                                 0.0,
                                 100);
            var two = new Circle(200.0,
                                 0.0,
                                 100);

            bool actual = one.Intersects(two);

            Assert.False(actual);
        }

        [Test]
        public void DetermineIsUTurnRequiredReturnsTrueTest()
        {
            var one = new Circle(0.0,
                                 0.0,
                                 100);
            var two = new Circle(50.0,
                                 0.0,
                                 100);

            bool actual = one.Intersects(two);

            Assert.True(actual);
        }

        [Test]
        public void DistanceTest()
        {
            const double expected = 2.83;
            double actual = m_One.Distance(m_Two);

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.01,
                                           "Distance");
        }

        [Test]
        public void EqualsOperatorReturnTrueForSameTest()
        {
            var other = new Circle(new Point(3.0,
                                             4.0),
                                   2.0);

            Assert.True(m_One == other);
        }

        [Test]
        public void EqualsReturnFalseForDifferentCentrePointTest()
        {
            var other = new Circle(new Point(5.0,
                                             6.0),
                                   2.0);

            Assert.False(m_One.Equals(other));
        }

        [Test]
        public void EqualsReturnFalseForDifferentRadiusTest()
        {
            var other = new Circle(new Point(3.0,
                                             4.0),
                                   5.0);

            Assert.False(m_One.Equals(other));
        }

        [Test]
        public void EqualsReturnFalseForNullTest()
        {
            Assert.False(m_One.Equals(null));
        }

        [Test]
        public void EqualsReturnFalseForOtherClassTest()
        {
            Assert.False(m_One.Equals(new object()));
        }

        [Test]
        public void EqualsReturnTrueForSameTest()
        {
            Assert.True(m_One.Equals(m_One));
        }

        [Test]
        public void EqualsReturnTrueForSameValuesTest()
        {
            var other = new Circle(new Point(3.0,
                                             4.0),
                                   2.0);

            Assert.True(m_One.Equals(other));
        }

        [Test]
        public void GetAngleRelativeToXAxisFor135DegreesTest()
        {
            AssertGetAngleRelativeToXAxis(135.0,
                                          135.0);
        }

        [Test]
        public void GetAngleRelativeToXAxisFor180DegreesTest()
        {
            AssertGetAngleRelativeToXAxis(180.0,
                                          180.0);
        }

        [Test]
        public void GetAngleRelativeToXAxisFor225DegreesTest()
        {
            AssertGetAngleRelativeToXAxis(225.0,
                                          225.0);
        }

        [Test]
        public void GetAngleRelativeToXAxisFor270DegreesTest()
        {
            AssertGetAngleRelativeToXAxis(270.0,
                                          270.0);
        }

        [Test]
        public void GetAngleRelativeToXAxisFor315DegreesTest()
        {
            AssertGetAngleRelativeToXAxis(315.0,
                                          315.0);
        }

        [Test]
        public void GetAngleRelativeToXAxisFor360DegreesTest()
        {
            AssertGetAngleRelativeToXAxis(360.0,
                                          0.0);
        }

        [Test]
        public void GetAngleRelativeToXAxisFor45DegreesTest()
        {
            AssertGetAngleRelativeToXAxis(45.0,
                                          45.0);
        }

        [Test]
        public void GetAngleRelativeToXAxisFor90DegreesTest()
        {
            AssertGetAngleRelativeToXAxis(90.0,
                                          90.0);
        }

        [Test]
        public void GetAngleRelativeToXAxisForZeroDegreesTest()
        {
            AssertGetAngleRelativeToXAxis(0.0,
                                          0.0);
        }

        [Test]
        public void GetHashCodeTest()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.DoesNotThrow(() => m_One.GetHashCode());
        }

        [Test]
        public void IsPointOnCircleReturnsFalseForPointInsideTest()
        {
            var point = new Point(3.0,
                                  5.0);

            Assert.False(m_One.IsPointOnCircle(point));
        }

        [Test]
        public void IsPointOnCircleReturnsFalseForPointOutsideTest()
        {
            var point = new Point(3.0,
                                  7.0);

            Assert.False(m_One.IsPointOnCircle(point));
        }

        [Test]
        public void IsPointOnCircleReturnsTrueForMinusEpsilonTest()
        {
            var point = new Point(3.0,
                                  6.0 - SelkieConstants.EpsilonDistance);

            Assert.True(m_One.IsPointOnCircle(point));
        }

        [Test]
        public void IsPointOnCircleReturnsTrueForPlusEpsilonTest()
        {
            var point = new Point(3.0,
                                  6.0 + SelkieConstants.EpsilonDistance);

            Assert.True(m_One.IsPointOnCircle(point));
        }

        [Test]
        public void IsPointOnCircleReturnsTrueForPlusHalfEpsilonTest()
        {
            var point = new Point(3.0,
                                  6.0 + SelkieConstants.EpsilonDistance / 2.0);

            Assert.True(m_One.IsPointOnCircle(point));
        }

        [Test]
        public void IsPointOnCircleReturnsTrueForPointOnCircleTest()
        {
            var point = new Point(3.0,
                                  6.0);

            Assert.True(m_One.IsPointOnCircle(point));
        }

        [Test]
        public void IsUnknownTest()
        {
            Assert.False(m_One.IsUnknown);
        }

        [Test]
        public void NotEqualsOperatorReturnTrueForOtherTest()
        {
            Assert.True(m_One != m_Two);
        }

        [Test]
        public void PointOnCircle180DegreesTest()
        {
            var expected = new Point(1.0,
                                     4.0);
            Point actual = m_One.PointOnCircle(Angle.For180Degrees);

            Assert.AreEqual(expected,
                            actual,
                            "CalculatePointOnCircle");
        }

        [Test]
        public void PointOnCircle270DegreesTest()
        {
            var expected = new Point(3.0,
                                     2.0);
            Point actual = m_One.PointOnCircle(Angle.For270Degrees);

            Assert.AreEqual(expected,
                            actual,
                            "CalculatePointOnCircle");
        }

        [Test]
        public void PointOnCircle360DegreesTest()
        {
            var expected = new Point(5.0,
                                     4.0);
            Point actual = m_One.PointOnCircle(Angle.For360Degrees);

            Assert.AreEqual(expected,
                            actual,
                            "CalculatePointOnCircle");
        }

        [Test]
        public void PointOnCircle90DegreesTest()
        {
            var expected = new Point(3.0,
                                     6.0);
            Point actual = m_One.PointOnCircle(Angle.For90Degrees);

            Assert.AreEqual(expected,
                            actual,
                            "CalculatePointOnCircle");
        }

        [Test]
        public void RadiansRelativeToXAxisCounterClockwiseTest()
        {
            Angle expected = Angle.ForZeroDegrees;
            Angle actual = m_One.RadiansRelativeToXAxisCounterClockwise(new Point(5.0,
                                                                                  4.0));

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void RadiusTest()
        {
            Assert.AreEqual(2.0,
                            m_One.Radius);
        }

        [Test]
        public void UnknownTest()
        {
            ICircle actual = Circle.Unknown;

            Assert.True(actual.IsUnknown,
                        "IsUnknown");
            Assert.True(actual.CentrePoint.IsUnknown,
                        "CentrePoint.IsUnknown");
        }

        [Test]
        public void XTest()
        {
            Assert.AreEqual(3.0,
                            m_One.X);
        }

        [Test]
        public void YTest()
        {
            Assert.AreEqual(4.0,
                            m_One.Y);
        }
    }
}