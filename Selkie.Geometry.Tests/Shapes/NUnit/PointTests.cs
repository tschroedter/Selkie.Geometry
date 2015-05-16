using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.NUnit.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Tests.Shapes.NUnit
{
    // ReSharper disable once ClassTooBig
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class PointTests
    {
        [SetUp]
        public void Setup()
        {
            m_Point = new Point(3.0,
                                4.0);
        }

        private Point m_Point;

        [Test]
        public void AngleBetweenPointsFor135DegreesCaseOneTest()
        {
            var centre = new Point(10.0,
                                   10.0);
            var one = new Point(10.0,
                                20.0);
            var two = new Point(20.0,
                                0.0);

            Angle expected = Angle.For135Degrees;
            Angle actual = centre.AngleBetweenPoints(one,
                                                     two);

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleBetweenPointsFor135DegreesCaseTwoTest()
        {
            var centre = new Point(10.0,
                                   10.0);
            var one = new Point(10.0,
                                20.0);
            var two = new Point(0.0,
                                0.0);

            Angle expected = Angle.For135Degrees;
            Angle actual = centre.AngleBetweenPoints(one,
                                                     two);

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleBetweenPointsFor180DegreesCaseOneTest()
        {
            var centre = new Point(10.0,
                                   10.0);
            var one = new Point(10.0,
                                20.0);
            var two = new Point(10.0,
                                0.0);

            Angle expected = Angle.For180Degrees;
            Angle actual = centre.AngleBetweenPoints(one,
                                                     two);

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleBetweenPointsFor45DegreesCaseOneTest()
        {
            var centre = new Point(10.0,
                                   10.0);
            var one = new Point(10.0,
                                20.0);
            var two = new Point(20.0,
                                20.0);

            Angle expected = Angle.For45Degrees;
            Angle actual = centre.AngleBetweenPoints(one,
                                                     two);

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleBetweenPointsFor45DegreesCaseTwoTest()
        {
            var centre = new Point(10.0,
                                   10.0);
            var one = new Point(10.0,
                                20.0);
            var two = new Point(0.0,
                                20.0);

            Angle expected = Angle.For45Degrees;
            Angle actual = centre.AngleBetweenPoints(one,
                                                     two);

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleBetweenPointsFor90DegreesCaseOneTest()
        {
            var centre = new Point(10.0,
                                   10.0);
            var one = new Point(10.0,
                                20.0);
            var two = new Point(20.0,
                                10.0);

            Angle expected = Angle.For90Degrees;
            Angle actual = centre.AngleBetweenPoints(one,
                                                     two);

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void AngleBetweenPointsFor90DegreesCaseTwoTest()
        {
            var centre = new Point(10.0,
                                   10.0);
            var one = new Point(10.0,
                                20.0);
            var two = new Point(0.0,
                                10.0);

            Angle expected = Angle.For90Degrees;
            Angle actual = centre.AngleBetweenPoints(one,
                                                     two);

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void DistanceTest()
        {
            var other = new Point(3.0,
                                  8.0);

            const double expected = 4.0;
            double actual = m_Point.DistanceTo(other);

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           "Distance");
        }

        [Test]
        public void EqualsOperatorReturnsTrueForSameTest()
        {
            var point = new Point(1.0,
                                  2.0);

            // ReSharper disable EqualExpressionComparison
            Assert.True(point == point);
            // ReSharper restore EqualExpressionComparison
        }

        [Test]
        public void EqualsReturnsFalseForNullTest()
        {
            var point = new Point(1.0,
                                  2.0);

            Assert.False(point.Equals(null));
        }

        [Test]
        public void EqualsReturnsFalseForOtherTypeTest()
        {
            var point = new Point(1.0,
                                  2.0);

            Assert.False(point.Equals(new object()));
        }

        [Test]
        public void EqualsReturnsFalseForSameValueWithDeltaTest()
        {
            var point1 = new Point(1.0,
                                   2.0);
            var point2 = new Point(1.0 + SelkieConstants.EpsilonPointXy,
                                   2.0 + SelkieConstants.EpsilonPointXy);

            Assert.False(point1.Equals(point2));
        }

        [Test]
        public void EqualsReturnsForForDifferentValueTest()
        {
            var point1 = new Point(1.0,
                                   2.0);
            var point2 = new Point(2.0,
                                   3.0);

            Assert.False(point1 == point2);
        }

        [Test]
        public void EqualsReturnsTrueForSameTest()
        {
            var point = new Point(1.0,
                                  2.0);

            Assert.True(point.Equals(point));
        }

        [Test]
        public void EqualsReturnsTrueForSameValueTest()
        {
            var point1 = new Point(1.0,
                                   2.0);
            var point2 = new Point(1.0,
                                   2.0);

            Assert.True(point1.Equals(point2));
        }

        [Test]
        public void EqualsReturnsTrueForSameValueWitHalfhDeltaTest()
        {
            var point1 = new Point(1.0,
                                   2.0);
            var point2 = new Point(1.0 + SelkieConstants.EpsilonPointXy / 2.0,
                                   2.0 + SelkieConstants.EpsilonPointXy / 2.0);

            Assert.True(point1.Equals(point2));
        }

        [Test]
        public void GetHashCodeTest()
        {
            var point = new Point(1.0,
                                  2.0);

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.DoesNotThrow(() => point.GetHashCode());
        }

        [Test]
        public void Move180DegreesTest()
        {
            const double distance = 10.0;
            const double radians = Angle.RadiansFor180Degrees;

            Point moved = m_Point.Move(distance,
                                       radians);

            NUnitHelper.AssertIsEquivalent(m_Point.X,
                                           moved.X,
                                           "X");
            NUnitHelper.AssertIsEquivalent(-6.0,
                                           moved.Y,
                                           "Y");
        }

        [Test]
        public void Move225DegreesTest()
        {
            const double distance = 10.0;
            const double radians = Angle.RadiansFor225Degrees;

            Point moved = m_Point.Move(distance,
                                       radians);

            NUnitHelper.AssertIsEquivalent(-4.07,
                                           moved.X,
                                           "X");
            NUnitHelper.AssertIsEquivalent(-3.07,
                                           moved.Y,
                                           "Y");
        }

        [Test]
        public void Move45DegreesTest()
        {
            const double distance = 10.0;
            const double radians = Angle.RadiansFor45Degrees;

            Point moved = m_Point.Move(distance,
                                       radians);

            NUnitHelper.AssertIsEquivalent(10.07,
                                           moved.X,
                                           "X");
            NUnitHelper.AssertIsEquivalent(11.07,
                                           moved.Y,
                                           "Y");
        }

        [Test]
        public void Move90DegreesTest()
        {
            const double distance = 10.0;
            const double radians = Angle.RadiansFor90Degrees;

            Point moved = m_Point.Move(distance,
                                       radians);

            NUnitHelper.AssertIsEquivalent(m_Point.X + 10.0,
                                           moved.X,
                                           "X");
            NUnitHelper.AssertIsEquivalent(m_Point.Y,
                                           moved.Y,
                                           "Y");
        }

        [Test]
        public void NotEqualsOperatorReturnsFalseForSameTest()
        {
            var point = new Point(1.0,
                                  2.0);

            // ReSharper disable EqualExpressionComparison
            Assert.False(point != point);
            // ReSharper restore EqualExpressionComparison
        }

        [Test]
        public void NotEqualsReturnsForTrueForDifferentValueTest()
        {
            var point1 = new Point(1.0,
                                   2.0);
            var point2 = new Point(2.0,
                                   3.0);

            Assert.True(point1 != point2);
        }

        [Test]
        public void RelativeToForSamePointTest()
        {
            var point = new Point(1.0,
                                  2.0);

            Point actual = point.RelativeTo(point);

            Assert.AreEqual(0.0,
                            actual.X,
                            "X");
            Assert.AreEqual(0.0,
                            actual.Y,
                            "Y");
        }

        [Test]
        public void RelativeToQuadrantFourCaseOneTest()
        {
            var point = new Point(1.0,
                                  2.0);
            var other = new Point(-2.0,
                                  4.0);

            Point actual = point.RelativeTo(other);

            Assert.AreEqual(3.0,
                            actual.X,
                            "X");
            Assert.AreEqual(-2.0,
                            actual.Y,
                            "Y");
        }

        [Test]
        public void RelativeToQuadrantFourCaseTwoTest()
        {
            var point = new Point(-5.0,
                                  10.0);
            var other = new Point(-2.0,
                                  4.0);

            Point actual = point.RelativeTo(other);

            Assert.AreEqual(-3.0,
                            actual.X,
                            "X");
            Assert.AreEqual(6.0,
                            actual.Y,
                            "Y");
        }

        [Test]
        public void RelativeToQuadrantOneCaseOneTest()
        {
            var point = new Point(1.0,
                                  2.0);
            var other = new Point(2.0,
                                  4.0);

            Point actual = point.RelativeTo(other);

            Assert.AreEqual(-1.0,
                            actual.X,
                            "X");
            Assert.AreEqual(-2.0,
                            actual.Y,
                            "Y");
        }

        [Test]
        public void RelativeToQuadrantOneCaseTwoTest()
        {
            var point = new Point(3.0,
                                  6.0);
            var other = new Point(2.0,
                                  4.0);

            Point actual = point.RelativeTo(other);

            Assert.AreEqual(1.0,
                            actual.X,
                            "X");
            Assert.AreEqual(2.0,
                            actual.Y,
                            "Y");
        }

        [Test]
        public void RelativeToQuadrantThreeCaseOneTest()
        {
            var point = new Point(1.0,
                                  2.0);
            var other = new Point(-2.0,
                                  -4.0);

            Point actual = point.RelativeTo(other);

            Assert.AreEqual(3.0,
                            actual.X,
                            "X");
            Assert.AreEqual(6.0,
                            actual.Y,
                            "Y");
        }

        [Test]
        public void RelativeToQuadrantThreeCaseTwoTest()
        {
            var point = new Point(-5.0,
                                  -10.0);
            var other = new Point(-2.0,
                                  -4.0);

            Point actual = point.RelativeTo(other);

            Assert.AreEqual(-3.0,
                            actual.X,
                            "X");
            Assert.AreEqual(-6.0,
                            actual.Y,
                            "Y");
        }

        [Test]
        public void RelativeToQuadrantTwoCaseOneTest()
        {
            var point = new Point(1.0,
                                  2.0);
            var other = new Point(2.0,
                                  -4.0);

            Point actual = point.RelativeTo(other);

            Assert.AreEqual(-1.0,
                            actual.X,
                            "X");
            Assert.AreEqual(6.0,
                            actual.Y,
                            "Y");
        }

        [Test]
        public void RelativeToQuadrantTwoCaseTwoTest()
        {
            var point = new Point(3.0,
                                  -10.0);
            var other = new Point(2.0,
                                  -4.0);

            Point actual = point.RelativeTo(other);

            Assert.AreEqual(1.0,
                            actual.X,
                            "X");
            Assert.AreEqual(-6.0,
                            actual.Y,
                            "Y");
        }

        [Test]
        public void ToStringTest()
        {
            const string expected = "[3,4]";
            string actual = m_Point.ToString();

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void UnknownInstanceTest()
        {
            Point point = Point.Unknown;

            Assert.True(point.IsUnknown,
                        "IsUnknown");
            Assert.AreEqual(double.NaN,
                            point.X,
                            "X");
            Assert.AreEqual(double.NaN,
                            point.Y,
                            "Y");
        }

        [Test]
        public void UnknownTest()
        {
            Assert.False(m_Point.IsUnknown);
        }

        [Test]
        public void XTest()
        {
            Assert.AreEqual(3.0,
                            m_Point.X);
        }

        [Test]
        public void YTest()
        {
            Assert.AreEqual(4.0,
                            m_Point.Y);
        }
    }
}