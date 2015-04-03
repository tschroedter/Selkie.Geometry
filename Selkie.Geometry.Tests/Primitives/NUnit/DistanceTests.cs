using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Tests.Primitives.NUnit
{
    // ReSharper disable once ClassTooBig
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class DistanceTests
    {
        [Test]
        public void GetHashCodeTest()
        {
            int expected = 100.0.GetHashCode();
            int actual = new Distance(100.0).GetHashCode();

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void IsUnknownTest()
        {
            Assert.False(new Distance(100.0).IsUnknown);
        }

        [Test]
        public void LengthTest()
        {
            Assert.AreEqual(100.0,
                            new Distance(100.0).Length);
        }

        [Test]
        public void OperatorEqualsReturnsFalseForDifferentValuesTest()
        {
            Distance one = new Distance(2.0);
            Distance two = new Distance(3.0);

            Assert.False(one == two);
        }

        [Test]
        public void OperatorEqualsReturnsFalseForInsideEpsilonTest()
        {
            Distance one = new Distance(3.0);
            Distance two = new Distance(3.0 + SelkieConstants.EpsilonDistance);

            Assert.False(one != two);
        }

        [Test]
        public void OperatorEqualsReturnsTrueForDifferentValuesTest()
        {
            Distance one = new Distance(2.0);
            Distance two = new Distance(3.0);

            Assert.True(one != two);
        }

        [Test]
        public void OperatorEqualsReturnsTrueForInsideEpsilonTest()
        {
            Distance one = new Distance(3.0);
            Distance two = new Distance(3.0 + SelkieConstants.EpsilonDistance);

            Assert.True(one == two);
        }

        [Test]
        public void OperatorEqualsReturnsTrueForSameValueTest()
        {
            Distance one = new Distance(3.0);
            Distance two = new Distance(3.0);

            Assert.True(one == two);
        }

        [Test]
        public void OperatorGreaterOrEqualReturnsFalseForOneIsGreaterThanTwoNegativeEpsilonTest()
        {
            Distance one = new Distance(2.0 - SelkieConstants.EpsilonDistance);
            Distance two = new Distance(2.0);

            Assert.False(one >= two);
        }

        [Test]
        public void OperatorGreaterOrEqualReturnsTrueForOneIsGreaterThanTwoEpsilonTest()
        {
            Distance one = new Distance(2.0 + SelkieConstants.EpsilonDistance);
            Distance two = new Distance(2.0);

            Assert.True(one >= two);
        }

        [Test]
        public void OperatorGreaterOrEqualReturnsTrueForOneIsGreaterThanTwoTest()
        {
            Distance one = new Distance(3.0);
            Distance two = new Distance(2.0);

            Assert.True(one >= two);
        }

        [Test]
        public void OperatorGreaterOrEqualReturnsTrueForSameValueTest()
        {
            Distance one = new Distance(3.0);
            Distance two = new Distance(3.0);

            Assert.True(one >= two);
        }

        [Test]
        public void OperatorGreaterReturnsFalseForOneIsLessThanTwoTest()
        {
            Distance one = new Distance(1.0);
            Distance two = new Distance(2.0);

            Assert.False(one > two);
        }

        [Test]
        public void OperatorGreaterReturnsTrueForOneIsGreaterThanTwoTest()
        {
            Distance one = new Distance(3.0);
            Distance two = new Distance(2.0);

            Assert.True(one > two);
        }

        [Test]
        public void OperatorLessOrEqualReturnsFalseForOneIsGreaterThanTwoNegativeEpsilonTest()
        {
            Distance one = new Distance(2.0 - SelkieConstants.EpsilonDistance);
            Distance two = new Distance(2.0);

            Assert.True(one <= two);
        }

        [Test]
        public void OperatorLessOrEqualReturnsTrueForOneIsGreaterThanTwoEpsilonTest()
        {
            Distance one = new Distance(2.0 + SelkieConstants.EpsilonDistance);
            Distance two = new Distance(2.0);

            Assert.False(one <= two);
        }

        [Test]
        public void OperatorLessOrEqualReturnsTrueForOneIsGreaterThanTwoTest()
        {
            Distance one = new Distance(1.0);
            Distance two = new Distance(2.0);

            Assert.True(one <= two);
        }

        [Test]
        public void OperatorLessOrEqualReturnsTrueForSameValueTest()
        {
            Distance one = new Distance(3.0);
            Distance two = new Distance(3.0);

            Assert.True(one <= two);
        }

        [Test]
        public void OperatorLessReturnsFalseForOneIsGreaterThanTwoTest()
        {
            Distance one = new Distance(3.0);
            Distance two = new Distance(2.0);

            Assert.False(one < two);
        }

        [Test]
        public void OperatorLessReturnsTrueForOneIsLessThanTwoTest()
        {
            Distance one = new Distance(1.0);
            Distance two = new Distance(2.0);

            Assert.True(one < two);
        }

        [Test]
        public void OperatorMinusTest()
        {
            Distance one = new Distance(1.0);
            Distance two = new Distance(2.0);

            Distance expected = new Distance(-1.0);
            Distance actual = one - two;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void OperatorNotEqualsReturnsFalseForSameValueTest()
        {
            Distance one = new Distance(3.0);
            Distance two = new Distance(3.0);

            Assert.False(one != two);
        }

        [Test]
        public void OperatorPlusTest()
        {
            Distance one = new Distance(1.0);
            Distance two = new Distance(2.0);

            Distance expected = new Distance(3.0);
            Distance actual = one + two;

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void ToStringTest()
        {
            const string expected = "Length: 100.00";
            string actual = new Distance(100.0).ToString();

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void UnknownTest()
        {
            Assert.True(Distance.Unknown.IsUnknown,
                        "IsUnknown");
            Assert.AreEqual(0.0,
                            Distance.Unknown.Length,
                            "Length");
        }

        [Test]
        public void ZeroTest()
        {
            Assert.AreEqual(0.0,
                            Distance.Zero.Length);
        }
    }
}