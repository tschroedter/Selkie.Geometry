using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.ThreeD.Shapes;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.ThreeD.Shapes
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class PointTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = new Point(3.0,
                              4.0,
                              5.0);
        }

        private Point m_Sut;

        [Test]
        public void DistanceTo_CalculateDistance_ToOtherPoint()
        {
            // Arrange
            const double expected = 1.7320508075688772;

            var to = new Point(4.0,
                               5.0,
                               6.0);

            // Act
            double actual = m_Sut.DistanceTo(to);

            // Assert
            NUnitHelper.AssertIsEquivalent(expected,
                                           actual);
        }

        [Test]
        public void ToStringTest()
        {
            const string expected = "[3,4,5]";
            string actual = m_Sut.ToString();

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
            Assert.AreEqual(double.NaN,
                            point.Z,
                            "Z");
        }

        [Test]
        public void UnknownTest()
        {
            Assert.False(m_Sut.IsUnknown);
        }

        [Test]
        public void XTest()
        {
            Assert.AreEqual(3.0,
                            m_Sut.X);
        }

        [Test]
        public void YTest()
        {
            Assert.AreEqual(4.0,
                            m_Sut.Y);
        }

        [Test]
        public void ZTest()
        {
            Assert.AreEqual(5.0,
                            m_Sut.Z);
        }
    }
}