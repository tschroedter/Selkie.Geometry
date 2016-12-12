using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.Tests.ThreeD.Primitives
{
    [TestFixture]
    internal sealed class SphericalCoordinatesTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = new SphericalCoordinates
                    {
                        Radius = 1.0,
                        PolarAngle = PolarAngle.FromRadians(Angle.For45Degrees.Radians),
                        AzimuthalAngle = AzimuthAngle.FromRadians(Angle.For90Degrees.Radians),
                        IsUnknown = true
                    };
        }

        private SphericalCoordinates m_Sut;

        [Test]
        public void AzimuthalAngleInRadians_ReturnsValue_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Angle.For90Degrees.Radians,
                            m_Sut.AzimuthalAngle.Radians);
        }

        [Test]
        public void IsUnknown_ReturnsValue_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.True(m_Sut.IsUnknown);
        }

        [Test]
        public void PolarInRadians_ReturnsValue_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Angle.For45Degrees.Radians,
                            m_Sut.PolarAngle.Radians);
        }

        [Test]
        public void Radius_ReturnsValue_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(1.0,
                            m_Sut.Radius);
        }
    }
}