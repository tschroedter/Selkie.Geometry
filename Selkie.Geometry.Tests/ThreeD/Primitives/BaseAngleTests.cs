using JetBrains.Annotations;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.NUnit.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Tests.ThreeD.Primitives
{
    [TestFixture]
    internal sealed class BaseTestBaseAngleTests
    {
        private class TestBaseAngle
            : BaseAngle
        {
            private TestBaseAngle(double radians)
            {
                Radians = radians;
                Degrees = ConvertRadiansToDegrees(radians);
            }

            [NotNull]
            public static TestBaseAngle FromDegrees(double degrees)
            {
                double radians = ConvertDegreesToRadians(degrees);

                return CreateFromRadians(radians);
            }

            [NotNull]
            public static TestBaseAngle FromRadians(double radians)
            {
                return CreateFromRadians(radians);
            }

            private static TestBaseAngle CreateFromRadians(double radians)
            {
                if ( radians < 0.0 )
                {
                    radians += RadiansFor360Degrees;
                }

                double remainder = radians % RadiansFor360Degrees;

                return new TestBaseAngle(remainder);
            }
        }

        [Test]
        public void ConvertDegreesToRadiansFor45DegreesTest()
        {
            double actual = BaseAngle.ConvertDegreesToRadians(45.0);

            NUnitHelper.AssertDegrees(BaseAngle.RadiansFor45Degrees,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansForZeroMinusHalfEpsilonTest()
        {
            const double degrees = 0.0 - SelkieConstants.EpsilonDegrees / 2.0;
            double actual = BaseAngle.ConvertDegreesToRadians(degrees);

            NUnitHelper.AssertDegrees(0.0,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansForZeroPlusEpsilonTest()
        {
            const double expected = 0.0 + SelkieConstants.EpsilonDegrees;
            double actual = BaseAngle.ConvertDegreesToRadians(expected);

            NUnitHelper.AssertDegrees(expected,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansForZeroPlusHalfEpsilonTest()
        {
            const double degrees = 0.0 + SelkieConstants.EpsilonDegrees / 2.0;
            double actual = BaseAngle.ConvertDegreesToRadians(degrees);

            NUnitHelper.AssertDegrees(0.0,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansForZeroTest()
        {
            double actual = BaseAngle.ConvertDegreesToRadians(0.0);

            NUnitHelper.AssertDegrees(BaseAngle.RadiansForZeroDegrees,
                                      actual);
        }

        [Test]
        public void ConvertRadiansToDegreesFor45DegreesTest()
        {
            double actual = BaseAngle.ConvertRadiansToDegrees(BaseAngle.RadiansFor45Degrees);

            NUnitHelper.AssertDegrees(45.0,
                                      actual);
        }

        [Test]
        public void ConvertRadiansToDegreesForZeroTest()
        {
            double actual = BaseAngle.ConvertRadiansToDegrees(BaseAngle.RadiansForZeroDegrees);

            NUnitHelper.AssertDegrees(0.0,
                                      actual);
        }

        [Test]
        public void EqualsReturnsFalseForNullTest()
        {
            TestBaseAngle angle = TestBaseAngle.FromRadians(BaseAngle.RadiansFor45Degrees);

            Assert.False(angle.Equals(null));
        }

        [Test]
        public void EqualsReturnsFalseForOtherClassTest()
        {
            TestBaseAngle angle1 = TestBaseAngle.FromRadians(BaseAngle.RadiansFor45Degrees);

            Assert.False(angle1.Equals(new object()));
        }

        [Test]
        public void EqualsReturnsFalseForSameValuePlusEpsilonTest()
        {
            TestBaseAngle angle1 = TestBaseAngle.FromRadians(BaseAngle.RadiansFor45Degrees);
            TestBaseAngle angle2 = TestBaseAngle.FromRadians(BaseAngle.RadiansFor45Degrees + Constants.EpsilonRadians);

            Assert.False(angle1.Equals(angle2));
        }

        [Test]
        public void EqualsReturnsTrueFor360AndZeroTest()
        {
            TestBaseAngle angle1 = TestBaseAngle.FromRadians(BaseAngle.RadiansFor360Degrees);
            TestBaseAngle angle2 = TestBaseAngle.FromRadians(BaseAngle.RadiansForZeroDegrees);

            Assert.True(angle1.Equals(angle2));
        }

        [Test]
        public void EqualsReturnsTrueForSameTest()
        {
            TestBaseAngle angle = TestBaseAngle.FromRadians(BaseAngle.RadiansFor45Degrees);

            Assert.True(angle.Equals(angle));
        }

        [Test]
        public void EqualsReturnsTrueForSameValuePlusHalfEpsilonTest()
        {
            TestBaseAngle angle1 = TestBaseAngle.FromRadians(BaseAngle.RadiansFor45Degrees);
            TestBaseAngle angle2 =
                TestBaseAngle.FromRadians(BaseAngle.RadiansFor45Degrees + Constants.EpsilonRadians / 2.0);

            Assert.True(angle1.Equals(angle2));
        }

        [Test]
        public void EqualsReturnsTrueForSameValueTest()
        {
            TestBaseAngle angle1 = TestBaseAngle.FromRadians(BaseAngle.RadiansFor45Degrees);
            TestBaseAngle angle2 = TestBaseAngle.FromRadians(BaseAngle.RadiansFor45Degrees);

            Assert.True(angle1.Equals(angle2));
        }

        [Test]
        public void EqualsReturnsTrueForZeroAnd360Test()
        {
            TestBaseAngle angle1 = TestBaseAngle.FromRadians(BaseAngle.RadiansForZeroDegrees);
            TestBaseAngle angle2 = TestBaseAngle.FromRadians(BaseAngle.RadiansFor360Degrees);

            Assert.True(angle1.Equals(angle2));
        }

        [Test]
        [TestCase(0.0, 0.0, true)]
        [TestCase(0.0 + SelkieConstants.EpsilonRadians, 0.0, true)]
        [TestCase(0.0 + SelkieConstants.EpsilonRadians * 100, 0.0, false)]
        [TestCase(0.0, 1.0, false)]
        [TestCase(1.0, 0.0, false)]
        [TestCase(1.0, 1.0, true)]
        public void OperatorEqual_ReturnsBool_ForGivenTestBaseAngles(
            double oneInDegrees,
            double twoInDegrees,
            bool expected)
        {
            // Arrange
            TestBaseAngle one = TestBaseAngle.FromDegrees(oneInDegrees);
            TestBaseAngle two = TestBaseAngle.FromDegrees(twoInDegrees);

            // Act
            // Assert
            Assert.AreEqual(expected,
                            one == two);
        }

        [Test]
        [TestCase(0.0, 0.0, true)]
        [TestCase(0.0, 1.0, false)]
        [TestCase(1.0, 0.0, true)]
        [TestCase(1.0, 1.0, true)]
        public void OperatorGreaterOrEqualThan_ReturnsBool_ForGivenTestBaseAngles(
            double oneInDegrees,
            double twoInDegrees,
            bool expected)
        {
            // Arrange
            TestBaseAngle one = TestBaseAngle.FromDegrees(oneInDegrees);
            TestBaseAngle two = TestBaseAngle.FromDegrees(twoInDegrees);

            // Act
            // Assert
            Assert.AreEqual(expected,
                            one >= two);
        }

        [Test]
        [TestCase(0.0, 0.0, false)]
        [TestCase(0.0, 1.0, false)]
        [TestCase(1.0, 0.0, true)]
        [TestCase(1.0, 1.0, false)]
        public void OperatorGreaterThan_ReturnsBool_ForGivenTestBaseAngles(
            double oneInDegrees,
            double twoInDegrees,
            bool expected)
        {
            // Arrange
            TestBaseAngle one = TestBaseAngle.FromDegrees(oneInDegrees);
            TestBaseAngle two = TestBaseAngle.FromDegrees(twoInDegrees);

            // Act
            // Assert
            Assert.AreEqual(expected,
                            one > two);
        }

        [Test]
        [TestCase(0.0, 0.0, true)]
        [TestCase(0.0, 1.0, true)]
        [TestCase(1.0, 0.0, false)]
        [TestCase(1.0, 1.0, true)]
        public void OperatorLessOrEqualThan_ReturnsBool_ForGivenTestBaseAngles(
            double oneInDegrees,
            double twoInDegrees,
            bool expected)
        {
            // Arrange
            TestBaseAngle one = TestBaseAngle.FromDegrees(oneInDegrees);
            TestBaseAngle two = TestBaseAngle.FromDegrees(twoInDegrees);

            // Act
            // Assert
            Assert.AreEqual(expected,
                            one <= two);
        }

        [Test]
        [TestCase(0.0, 0.0, false)]
        [TestCase(0.0, 1.0, true)]
        [TestCase(1.0, 0.0, false)]
        [TestCase(1.0, 1.0, false)]
        public void OperatorLessThan_ReturnsBool_ForGivenTestBaseAngles(
            double oneInDegrees,
            double twoInDegrees,
            bool expected)
        {
            // Arrange
            TestBaseAngle one = TestBaseAngle.FromDegrees(oneInDegrees);
            TestBaseAngle two = TestBaseAngle.FromDegrees(twoInDegrees);

            // Act
            // Assert
            Assert.AreEqual(expected,
                            one < two);
        }

        [Test]
        [TestCase(0.0, 0.0, false)]
        [TestCase(0.0 + SelkieConstants.EpsilonRadians, 0.0, false)]
        [TestCase(0.0 + SelkieConstants.EpsilonRadians * 100, 0.0, true)]
        [TestCase(0.0, 1.0, true)]
        [TestCase(1.0, 0.0, true)]
        [TestCase(1.0, 1.0, false)]
        public void OperatorNotEqual_ReturnsBool_ForGivenTestBaseAngles(
            double oneInDegrees,
            double twoInDegrees,
            bool expected)
        {
            // Arrange
            TestBaseAngle one = TestBaseAngle.FromDegrees(oneInDegrees);
            TestBaseAngle two = TestBaseAngle.FromDegrees(twoInDegrees);

            // Act
            // Assert
            Assert.AreEqual(expected,
                            one != two);
        }

        [Test]
        public void ToString_ReturnsString_WhenCalled()
        {
            // Arrange
            TestBaseAngle sut = TestBaseAngle.FromDegrees(BaseAngle.RadiansFor90Degrees);

            // Act
            string actual = sut.ToString();

            // Assert
            Assert.AreEqual("[Radians: 0.027416 Degrees: 1.570796]",
                            actual);
        }
    }
}