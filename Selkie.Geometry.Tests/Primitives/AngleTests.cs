using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.NUnit.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Tests.Primitives
{
    // ReSharper disable once ClassTooBig
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class AngleTests
    {
        private void AssertRadiansRelativeToYAxisCounterClockwise(double degrees,
                                                                  double expectedDegrees)
        {
            Angle angle = Angle.FromDegrees(degrees);
            Angle expectedRadians = Angle.FromDegrees(expectedDegrees);

            Angle actual = Angle.RelativeToYAxisCounterclockwise(angle);

            Assert.AreEqual(expectedRadians,
                            actual);
        }

        private void AssertRelativeToXAxisCountertclockwise(double degrees,
                                                            double expectedDegrees)
        {
            Angle angle = Angle.FromDegrees(degrees);
            Angle expectedRadians = Angle.FromDegrees(expectedDegrees);

            Angle actual = Angle.RelativeToXAxisCountertclockwise(angle);

            Assert.AreEqual(expectedRadians,
                            actual);
        }

        private void AssertRadiansInverseForDegrees(double originalDgrees,
                                                    double expectedDegrees)
        {
            Angle expected = Angle.FromDegrees(expectedDegrees);
            Angle actual = Angle.Inverse(Angle.FromDegrees(originalDgrees));

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void ConvertDegreesToRadiansCallsNormalizeRadiansTest()
        {
            double actual = Angle.ConvertDegreesToRadians(45.0 + 360.0);

            NUnitHelper.AssertDegrees(Angle.RadiansFor45Degrees,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansFor45DegreesTest()
        {
            double actual = Angle.ConvertDegreesToRadians(45.0);

            NUnitHelper.AssertDegrees(Angle.RadiansFor45Degrees,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansForZeroMinusEpsilonTest()
        {
            const double expected = Angle.RadiansFor360Degrees;
            double actual = Angle.ConvertDegreesToRadians(0.0 - Angle.EpsilonDegrees);

            NUnitHelper.AssertDegrees(expected,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansForZeroMinusHalfEpsilonTest()
        {
            const double degrees = 0.0 - Angle.EpsilonDegrees / 2.0;
            double actual = Angle.ConvertDegreesToRadians(degrees);

            NUnitHelper.AssertDegrees(0.0,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansForZeroPlusEpsilonTest()
        {
            const double expected = 0.0 + Angle.EpsilonDegrees;
            double actual = Angle.ConvertDegreesToRadians(expected);

            NUnitHelper.AssertDegrees(expected,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansForZeroPlusHalfEpsilonTest()
        {
            const double degrees = 0.0 + Angle.EpsilonDegrees / 2.0;
            double actual = Angle.ConvertDegreesToRadians(degrees);

            NUnitHelper.AssertDegrees(0.0,
                                      actual);
        }

        [Test]
        public void ConvertDegreesToRadiansForZeroTest()
        {
            double actual = Angle.ConvertDegreesToRadians(0.0);

            NUnitHelper.AssertDegrees(Angle.RadiansForZeroDegrees,
                                      actual);
        }

        [Test]
        public void ConvertRadiansToDegreesCallsNormalizeRadiansTest()
        {
            double actual = Angle.ConvertRadiansToDegrees(Angle.RadiansFor45Degrees + Angle.RadiansFor360Degrees);

            NUnitHelper.AssertDegrees(45.0,
                                      actual);
        }

        [Test]
        public void ConvertRadiansToDegreesFor45DegreesTest()
        {
            double actual = Angle.ConvertRadiansToDegrees(Angle.RadiansFor45Degrees);

            NUnitHelper.AssertDegrees(45.0,
                                      actual);
        }

        [Test]
        public void ConvertRadiansToDegreesForZeroTest()
        {
            double actual = Angle.ConvertRadiansToDegrees(Angle.RadiansForZeroDegrees);

            NUnitHelper.AssertDegrees(0.0,
                                      actual);
        }

        [Test]
        public void DegreesTest()
        {
            Angle actual = Angle.For45Degrees;

            NUnitHelper.AssertRadians(45.0,
                                      actual.Degrees);
        }

        [Test]
        public void EpsilonDegreesTest()
        {
            NUnitHelper.AssertRadians(SelkieConstants.EpsilonDegrees,
                                      Angle.EpsilonDegrees);
        }

        [Test]
        public void EpsilonRadiansTest()
        {
            NUnitHelper.AssertRadians(SelkieConstants.EpsilonRadians,
                                      Angle.EpsilonRadians);
        }

        [Test]
        public void EqualsOperatorReturnsFalseForDifferentValueTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For90Degrees;

            Assert.False(angle1 == angle2);
        }

        [Test]
        public void EqualsOperatorReturnsTrueForSameValueTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For45Degrees;

            Assert.True(angle1 == angle2);
        }

        [Test]
        public void EqualsReturnsFalseForNullTest()
        {
            Angle angle = Angle.For45Degrees;

            Assert.False(angle.Equals(null));
        }

        [Test]
        public void EqualsReturnsFalseForOtherClassTest()
        {
            Angle angle1 = Angle.For45Degrees;

            Assert.False(angle1.Equals(new object()));
        }

        [Test]
        public void EqualsReturnsFalseForSameValuePlusEpsilonTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For45Degrees + Angle.FromRadians(Angle.EpsilonRadians);

            Assert.False(angle1.Equals(angle2));
        }

        [Test]
        public void EqualsReturnsTrueFor360AndZeroTest()
        {
            Angle angle1 = Angle.For360Degrees;
            Angle angle2 = Angle.ForZeroDegrees;

            Assert.True(angle1.Equals(angle2));
        }

        [Test]
        public void EqualsReturnsTrueForSameTest()
        {
            Angle angle = Angle.For45Degrees;

            Assert.True(angle.Equals(angle));
        }

        [Test]
        public void EqualsReturnsTrueForSameValuePlusHalfEpsilonTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For45Degrees + Angle.FromRadians(Angle.EpsilonRadians / 2.0);

            Assert.True(angle1.Equals(angle2));
        }

        [Test]
        public void EqualsReturnsTrueForSameValueTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For45Degrees;

            Assert.True(angle1.Equals(angle2));
        }

        [Test]
        public void EqualsReturnsTrueForZeroAnd360Test()
        {
            Angle angle1 = Angle.ForZeroDegrees;
            Angle angle2 = Angle.For360Degrees;

            Assert.True(angle1.Equals(angle2));
        }

        [Test]
        public void For135DegreesTest()
        {
            const double expected = Math.PI / 2.0 + Math.PI / 4.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.For135Degrees.Radians);
        }

        [Test]
        public void For180DegreesTest()
        {
            const double expected = Math.PI;

            NUnitHelper.AssertRadians(expected,
                                      Angle.For180Degrees.Radians);
        }

        [Test]
        public void For225DegreesTest()
        {
            const double expected = Math.PI + Math.PI / 4.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.For225Degrees.Radians);
        }

        [Test]
        public void For270DegreesTest()
        {
            const double expected = 2.0 * Math.PI * 0.75;

            NUnitHelper.AssertRadians(expected,
                                      Angle.For270Degrees.Radians);
        }

        [Test]
        public void For315DegreesTest()
        {
            const double expected = 2.0 * Math.PI * 0.75 + Math.PI / 4.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.For315Degrees.Radians);
        }

        [Test]
        public void For360DegreesTest()
        {
            const double expected = Angle.RadiansFor360Degrees;

            NUnitHelper.AssertRadians(expected,
                                      Angle.For360Degrees.Radians);
        }

        [Test]
        public void For45DegreesTest()
        {
            const double expected = Math.PI / 4.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.For45Degrees.Radians);
        }

        [Test]
        public void For90DegreesTest()
        {
            const double expected = Math.PI / 2.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.For90Degrees.Radians);
        }

        [Test]
        public void ForZeroDegreesTest()
        {
            const double expected = 0.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.ForZeroDegrees.Radians);
        }

        [Test]
        public void FromDegreesForDegreesGreaterThan360Test()
        {
            Angle actual = Angle.FromDegrees(180.0 * 3.0);

            NUnitHelper.AssertRadians(Angle.RadiansFor180Degrees,
                                      actual.Radians);
            NUnitHelper.AssertDegrees(180.0,
                                      actual.Degrees);
        }

        [Test]
        public void FromDegreesForDegreesLessThan360Test()
        {
            Angle actual = Angle.FromDegrees(-180.0);

            NUnitHelper.AssertRadians(Angle.RadiansFor180Degrees,
                                      actual.Radians);
            NUnitHelper.AssertDegrees(180.0,
                                      actual.Degrees);
        }

        [Test]
        public void FromDegreesTest()
        {
            Angle actual = Angle.FromDegrees(180.0);

            NUnitHelper.AssertRadians(Angle.RadiansFor180Degrees,
                                      actual.Radians);
            NUnitHelper.AssertDegrees(180.0,
                                      actual.Degrees);
        }

        [Test]
        public void FromRadiansForRadiansGreaterThan2PiTest()
        {
            Angle actual = Angle.FromRadians(Angle.RadiansFor180Degrees * 3.0);

            NUnitHelper.AssertRadians(Angle.RadiansFor180Degrees,
                                      actual.Radians);
            NUnitHelper.AssertDegrees(180.0,
                                      actual.Degrees);
        }

        [Test]
        public void FromRadiansForRadiansLessThanZeroTest()
        {
            Angle actual = Angle.FromRadians(-Angle.RadiansFor180Degrees);

            NUnitHelper.AssertRadians(Angle.RadiansFor180Degrees,
                                      actual.Radians);
            NUnitHelper.AssertDegrees(180.0,
                                      actual.Degrees);
        }

        [Test]
        public void FromRadiansTest()
        {
            Angle actual = Angle.FromRadians(Angle.RadiansFor180Degrees);

            NUnitHelper.AssertRadians(Angle.RadiansFor180Degrees,
                                      actual.Radians);
            NUnitHelper.AssertDegrees(180.0,
                                      actual.Degrees);
        }

        [Test]
        public void GetHashCodeTest()
        {
            Angle angle1 = Angle.For45Degrees;

            int expected = Angle.RadiansFor45Degrees.GetHashCode();
            int actual = angle1.GetHashCode();

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void GreaterOperatorReturnsFalseForALessBTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For90Degrees;

            Assert.False(angle1 > angle2);
        }

        [Test]
        public void GreaterOperatorReturnsFalseForSameValueTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For45Degrees;

            Assert.False(angle1 > angle2);
        }

        [Test]
        public void GreaterOperatorReturnsTrueForAGreaterBTest()
        {
            Angle angle1 = Angle.For90Degrees;
            Angle angle2 = Angle.For45Degrees;

            Assert.True(angle1 > angle2);
        }

        [Test]
        public void GreaterOrEqualOperatorReturnsFalseForALessBTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For90Degrees;

            Assert.False(angle1 >= angle2);
        }

        [Test]
        public void GreaterOrEqualOperatorReturnsTrueForAEqualsBTest()
        {
            Angle angle1 = Angle.For90Degrees;
            Angle angle2 = Angle.For90Degrees;

            Assert.True(angle1 >= angle2);
        }

        [Test]
        public void GreaterOrEqualOperatorReturnsTrueForAGreaterBTest()
        {
            Angle angle1 = Angle.For90Degrees;
            Angle angle2 = Angle.For45Degrees;

            Assert.True(angle1 >= angle2);
        }

        [Test]
        public void LessOperatorReturnsFalseForSameValueTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For45Degrees;

            Assert.False(angle1 < angle2);
        }

        [Test]
        public void LessOperatorReturnsTrueForAGreaterBTest()
        {
            Angle angle1 = Angle.For90Degrees;
            Angle angle2 = Angle.For45Degrees;

            Assert.False(angle1 < angle2);
        }

        [Test]
        public void LessOperatorReturnsTrueForALessBTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For90Degrees;

            Assert.True(angle1 < angle2);
        }

        [Test]
        public void LessOrEqualOperatorReturnsFalseForAGreaterBTest()
        {
            Angle angle1 = Angle.For90Degrees;
            Angle angle2 = Angle.For45Degrees;

            Assert.False(angle1 <= angle2);
        }

        [Test]
        public void LessOrEqualOperatorReturnsTrueForAEqualsBTest()
        {
            Angle angle1 = Angle.For90Degrees;
            Angle angle2 = Angle.For90Degrees;

            Assert.True(angle1 <= angle2);
        }

        [Test]
        public void LessOrEqualOperatorReturnsTrueForALessBTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For90Degrees;

            Assert.True(angle1 <= angle2);
        }

        [Test]
        public void MinusOperatorResultIsZeroTest()
        {
            Angle one = Angle.FromDegrees(180.0);
            Angle two = Angle.FromDegrees(180.0);

            Angle expected = Angle.FromDegrees(0.0);
            Angle actual = one - two;

            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }

        [Test]
        public void MinusOperatorResultLessZeroTest()
        {
            Angle one = Angle.FromDegrees(180.0);
            Angle two = Angle.FromDegrees(270.0);

            Angle expected = Angle.FromDegrees(270.0);
            Angle actual = one - two;

            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }

        [Test]
        public void MinusOperatorTest()
        {
            Angle one = Angle.FromDegrees(180.0);
            Angle two = Angle.FromDegrees(90.0);

            Angle expected = Angle.FromDegrees(90.0);
            Angle actual = one - two;

            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }

        [Test]
        public void NormalizeRadiansForNegative405Test()
        {
            double actual = Angle.NormalizeRadians(Angle.RadiansFor45Degrees + Angle.RadiansFor360Degrees);

            NUnitHelper.AssertRadians(Angle.RadiansFor45Degrees,
                                      actual);
        }

        [Test]
        public void NormalizeRadiansForNegative45Test()
        {
            double actual = Angle.NormalizeRadians(-Angle.RadiansFor45Degrees);

            NUnitHelper.AssertRadians(Angle.RadiansFor315Degrees,
                                      actual);
        }

        [Test]
        public void NormalizeRadiansForZeroMinusEpsilonTest()
        {
            const double expected = Angle.RadiansFor360Degrees - Angle.EpsilonDegrees;
            double actual = Angle.NormalizeRadians(Angle.RadiansForZeroDegrees - Angle.EpsilonDegrees);

            NUnitHelper.AssertDegrees(expected,
                                      actual);
        }

        [Test]
        public void NormalizeRadiansForZeroMinusHalfEpsilonTest()
        {
            const double expected = Angle.RadiansFor360Degrees - Angle.EpsilonRadians / 2.0;
            double actual = Angle.NormalizeRadians(0.0 - Angle.EpsilonRadians / 2.0);

            NUnitHelper.AssertRadians(expected,
                                      actual);
        }

        [Test]
        public void NormalizeRadiansForZeroPlusEpsilonTest()
        {
            const double expected = 0.0 + Angle.EpsilonRadians;
            double actual = Angle.NormalizeRadians(expected);

            NUnitHelper.AssertRadians(expected,
                                      actual);
        }

        [Test]
        public void NormalizeRadiansForZeroPlusHalfEpsilonTest()
        {
            const double degrees = 0.0 + Angle.EpsilonRadians / 2.0;
            double actual = Angle.NormalizeRadians(degrees);

            NUnitHelper.AssertRadians(0.0,
                                      actual);
        }

        [Test]
        public void NormalizeRadiansForZeroTest()
        {
            double actual = Angle.NormalizeRadians(Angle.RadiansForZeroDegrees);

            NUnitHelper.AssertRadians(Angle.RadiansForZeroDegrees,
                                      actual);
        }

        [Test]
        public void NotEqualsOperatorReturnsFalseForSameValueTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For45Degrees;

            Assert.False(angle1 != angle2);
        }

        [Test]
        public void NotEqualsOperatorReturnsTrueForDifferentValueTest()
        {
            Angle angle1 = Angle.For45Degrees;
            Angle angle2 = Angle.For90Degrees;

            Assert.True(angle1 != angle2);
        }

        [Test]
        public void PlusOperatorResultBigger360Test()
        {
            Angle one = Angle.FromDegrees(180.0);
            Angle two = Angle.FromDegrees(181.0);

            Angle expected = Angle.FromDegrees(1.0);
            Angle actual = one + two;

            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }

        [Test]
        public void PlusOperatorResultIs360Test()
        {
            Angle one = Angle.FromDegrees(180.0);
            Angle two = Angle.FromDegrees(180.0);

            Angle expected = Angle.FromDegrees(360.0);
            Angle actual = one + two;

            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }

        [Test]
        public void PlusOperatorTest()
        {
            Angle one = Angle.FromDegrees(180.0);
            Angle two = Angle.FromDegrees(90.0);

            Angle expected = Angle.FromDegrees(270.0);
            Angle actual = one + two;

            NUnitHelper.AssertDegrees(expected.Degrees,
                                      actual.Degrees);
        }

        [Test]
        public void RadiansFor135DegreesTest()
        {
            const double expected = Math.PI / 2.0 + Math.PI / 4.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.RadiansFor135Degrees);
        }

        [Test]
        public void RadiansFor180DegreesTest()
        {
            const double expected = Math.PI;

            NUnitHelper.AssertRadians(expected,
                                      Angle.RadiansFor180Degrees);
        }

        [Test]
        public void RadiansFor225DegreesTest()
        {
            const double expected = Math.PI + Math.PI / 4.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.RadiansFor225Degrees);
        }

        [Test]
        public void RadiansFor270DegreesTest()
        {
            const double expected = 2.0 * Math.PI * 0.75;

            NUnitHelper.AssertRadians(expected,
                                      Angle.RadiansFor270Degrees);
        }

        [Test]
        public void RadiansFor315DegreesTest()
        {
            const double expected = 2.0 * Math.PI * 0.75 + Math.PI / 4.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.RadiansFor315Degrees);
        }

        [Test]
        public void RadiansFor360DegreesTest()
        {
            const double expected = 2.0 * Math.PI;

            NUnitHelper.AssertRadians(expected,
                                      Angle.RadiansFor360Degrees);
        }

        [Test]
        public void RadiansFor45DegreesTest()
        {
            const double expected = Math.PI / 4.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.RadiansFor45Degrees);
        }

        [Test]
        public void RadiansFor90DegreesTest()
        {
            const double expected = Math.PI / 2.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.RadiansFor90Degrees);
        }

        [Test]
        public void RadiansForZeroDegreesTest()
        {
            const double expected = 0.0;

            NUnitHelper.AssertRadians(expected,
                                      Angle.RadiansForZeroDegrees);
        }

        [Test]
        public void RadiansInverseFor135Test()
        {
            AssertRadiansInverseForDegrees(135.0,
                                           225.0);
        }

        [Test]
        public void RadiansInverseFor180Test()
        {
            AssertRadiansInverseForDegrees(180.0,
                                           180.0);
        }

        [Test]
        public void RadiansInverseFor225Test()
        {
            AssertRadiansInverseForDegrees(225.0,
                                           135.0);
        }

        [Test]
        public void RadiansInverseFor270Test()
        {
            AssertRadiansInverseForDegrees(270.0,
                                           90.0);
        }

        [Test]
        public void RadiansInverseFor315Test()
        {
            AssertRadiansInverseForDegrees(315.0,
                                           45);
        }

        [Test]
        public void RadiansInverseFor360Test()
        {
            AssertRadiansInverseForDegrees(360.0,
                                           0.0);
        }

        [Test]
        public void RadiansInverseFor45Test()
        {
            AssertRadiansInverseForDegrees(45.0,
                                           315.0);
        }

        [Test]
        public void RadiansInverseFor90Test()
        {
            AssertRadiansInverseForDegrees(90.0,
                                           270.0);
        }

        [Test]
        public void RadiansInverseForZeroTest()
        {
            AssertRadiansInverseForDegrees(0.0,
                                           0.0);
        }

        [Test]
        public void RadiansRelativeToXAxisCountertclockwiseFor135DegreesTest()
        {
            AssertRelativeToXAxisCountertclockwise(135.0,
                                                   315.0);
        }

        [Test]
        public void RadiansRelativeToXAxisCountertclockwiseFor180DegreesTest()
        {
            AssertRelativeToXAxisCountertclockwise(180.0,
                                                   270.0);
        }

        [Test]
        public void RadiansRelativeToXAxisCountertclockwiseFor225DegreesTest()
        {
            AssertRelativeToXAxisCountertclockwise(225.0,
                                                   225.0);
        }

        [Test]
        public void RadiansRelativeToXAxisCountertclockwiseFor270DegreesTest()
        {
            AssertRelativeToXAxisCountertclockwise(270.0,
                                                   180.0);
        }

        [Test]
        public void RadiansRelativeToXAxisCountertclockwiseFor315DegreesTest()
        {
            AssertRelativeToXAxisCountertclockwise(315.0,
                                                   135.0);
        }

        [Test]
        public void RadiansRelativeToXAxisCountertclockwiseFor360DegreesTest()
        {
            AssertRelativeToXAxisCountertclockwise(360.0,
                                                   90.0);
        }

        [Test]
        public void RadiansRelativeToXAxisCountertclockwiseFor45DegreesTest()
        {
            AssertRelativeToXAxisCountertclockwise(45.0,
                                                   45.0);
        }

        [Test]
        public void RadiansRelativeToXAxisCountertclockwiseFor90DegreesTest()
        {
            AssertRelativeToXAxisCountertclockwise(90.0,
                                                   0.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseFor0DegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(0.0,
                                                         90.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseFor135DegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(135.0,
                                                         315.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseFor180DegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(180.0,
                                                         270.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseFor225DegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(225.0,
                                                         225.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseFor270DegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(270.0,
                                                         180.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseFor315DegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(315.0,
                                                         135.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseFor360DegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(360.0,
                                                         90.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseFor45DegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(45.0,
                                                         45.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseFor90DegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(90.0,
                                                         0.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseForZeroDegreesTest()
        {
            AssertRadiansRelativeToYAxisCounterClockwise(0.0,
                                                         90.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseForZeroPlusEpsilonDegreesTest()
        {
            Angle angle = Angle.FromRadians(0.0 + SelkieConstants.EpsilonRadians);
            AssertRadiansRelativeToYAxisCounterClockwise(angle.Degrees,
                                                         90.0);
        }

        [Test]
        public void RadiansRelativeToYAxisCounterClockwiseForZeroPlusHalfEpsilonDegreesTest()
        {
            Angle angle = Angle.For360Degrees + Angle.FromRadians(SelkieConstants.EpsilonRadians / 2.0);
            AssertRadiansRelativeToYAxisCounterClockwise(angle.Degrees,
                                                         90.0);
        }

        [Test]
        public void RadiansTest()
        {
            Angle actual = Angle.For45Degrees;

            NUnitHelper.AssertRadians(Angle.RadiansFor45Degrees,
                                      actual.Radians);
        }

        [Test]
        public void ToStringTest()
        {
            Angle one = Angle.FromDegrees(180.0);

            const string expected = "[Radians: 3.14 Degrees: 180.00]";
            string actual = one.ToString();

            Assert.AreEqual(expected,
                            actual);
        }
    }
}