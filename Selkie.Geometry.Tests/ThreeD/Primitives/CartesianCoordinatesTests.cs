using NUnit.Framework;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.Tests.ThreeD.Primitives
{
    [TestFixture]
    internal sealed class CartesianCoordinatesTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = new CartesianCoordinates
                    {
                        X = 1.0,
                        Y = 2.0,
                        Z = 3.0,
                        IsUnknown = true
                    };
        }

        private CartesianCoordinates m_Sut;

        [Test]
        public void IsUnknown_ReturnsValue_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.True(m_Sut.IsUnknown);
        }

        [Test]
        public void X_ReturnsValue_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(1.0,
                            m_Sut.X);
        }

        [Test]
        public void Y_ReturnsValue_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(2.0,
                            m_Sut.Y);
        }

        [Test]
        public void Z_ReturnsValue_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(3.0,
                            m_Sut.Z);
        }
    }
}