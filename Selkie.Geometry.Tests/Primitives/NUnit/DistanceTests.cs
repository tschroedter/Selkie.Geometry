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

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsUnknownTest()
        {
            Assert.False(new Distance(100.0).IsUnknown);
        }

        [Test]
        public void LengthTest()
        {
            Assert.AreEqual(100.0, new Distance(100.0).Length);
        }

        [Test]
        public void OperatorEqualsReturnsFalseForDifferentValuesTest()
        {
            var one = new Distance(2.0);
            var two = new Distance(3.0);

            Assert.False(one == two);
        }

        [Test]
        public void OperatorEqualsReturnsFalseForInsideEpsilonTest()
        {
            var one = new Distance(3.0);
            var two = new Distance(3.0 + SelkieConstants.EpsilonDistance);

            Assert.False(one != two);
        }

        [Test]
        public void OperatorEqualsReturnsTrueForDifferentValuesTest()
        {
            var one = new Distance(2.0);
            var two = new Distance(3.0);

            Assert.True(one != two);
        }

        [Test]
        public void OperatorEqualsReturnsTrueForInsideEpsilonTest()
        {
            var one = new Distance(3.0);
            var two = new Distance(3.0 + SelkieConstants.EpsilonDistance);

            Assert.True(one == two);
        }

        [Test]
        public void OperatorEqualsReturnsTrueForSameValueTest()
        {
            var one = new Distance(3.0);
            var two = new Distance(3.0);

            Assert.True(one == two);
        }


        [Test]
        public void OperatorGreaterOrEqualReturnsFalseForOneIsGreaterThanTwoNegativeEpsilonTest()
        {
            var one = new Distance(2.0 - SelkieConstants.EpsilonDistance);
            var two = new Distance(2.0);

            Assert.False(one >= two);
        }

        [Test]
        public void OperatorGreaterOrEqualReturnsTrueForOneIsGreaterThanTwoEpsilonTest()
        {
            var one = new Distance(2.0 + SelkieConstants.EpsilonDistance);
            var two = new Distance(2.0);

            Assert.True(one >= two);
        }

        [Test]
        public void OperatorGreaterOrEqualReturnsTrueForOneIsGreaterThanTwoTest()
        {
            var one = new Distance(3.0);
            var two = new Distance(2.0);

            Assert.True(one >= two);
        }

        [Test]
        public void OperatorGreaterOrEqualReturnsTrueForSameValueTest()
        {
            var one = new Distance(3.0);
            var two = new Distance(3.0);

            Assert.True(one >= two);
        }

        [Test]
        public void OperatorGreaterReturnsFalseForOneIsLessThanTwoTest()
        {
            var one = new Distance(1.0);
            var two = new Distance(2.0);

            Assert.False(one > two);
        }

        [Test]
        public void OperatorGreaterReturnsTrueForOneIsGreaterThanTwoTest()
        {
            var one = new Distance(3.0);
            var two = new Distance(2.0);

            Assert.True(one > two);
        }

        [Test]
        public void OperatorLessOrEqualReturnsFalseForOneIsGreaterThanTwoNegativeEpsilonTest()
        {
            var one = new Distance(2.0 - SelkieConstants.EpsilonDistance);
            var two = new Distance(2.0);

            Assert.True(one <= two);
        }

        [Test]
        public void OperatorLessOrEqualReturnsTrueForOneIsGreaterThanTwoEpsilonTest()
        {
            var one = new Distance(2.0 + SelkieConstants.EpsilonDistance);
            var two = new Distance(2.0);

            Assert.False(one <= two);
        }

        [Test]
        public void OperatorLessOrEqualReturnsTrueForOneIsGreaterThanTwoTest()
        {
            var one = new Distance(1.0);
            var two = new Distance(2.0);

            Assert.True(one <= two);
        }

        [Test]
        public void OperatorLessOrEqualReturnsTrueForSameValueTest()
        {
            var one = new Distance(3.0);
            var two = new Distance(3.0);

            Assert.True(one <= two);
        }

        [Test]
        public void OperatorLessReturnsFalseForOneIsGreaterThanTwoTest()
        {
            var one = new Distance(3.0);
            var two = new Distance(2.0);

            Assert.False(one < two);
        }

        [Test]
        public void OperatorLessReturnsTrueForOneIsLessThanTwoTest()
        {
            var one = new Distance(1.0);
            var two = new Distance(2.0);

            Assert.True(one < two);
        }

        [Test]
        public void OperatorMinusTest()
        {
            var one = new Distance(1.0);
            var two = new Distance(2.0);

            var expected = new Distance(-1.0);
            Distance actual = one - two;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OperatorNotEqualsReturnsFalseForSameValueTest()
        {
            var one = new Distance(3.0);
            var two = new Distance(3.0);

            Assert.False(one != two);
        }

        [Test]
        public void OperatorPlusTest()
        {
            var one = new Distance(1.0);
            var two = new Distance(2.0);

            var expected = new Distance(3.0);
            Distance actual = one + two;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToStringTest()
        {
            const string expected = "Length: 100.00";
            string actual = new Distance(100.0).ToString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UnknownTest()
        {
            Assert.True(Distance.Unknown.IsUnknown, "IsUnknown");
            Assert.AreEqual(0.0, Distance.Unknown.Length, "Length");
        }

        [Test]
        public void ZeroTest()
        {
            Assert.AreEqual(0.0, Distance.Zero.Length);
        }
    }
}