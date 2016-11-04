using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.ThreeD.Converters;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.Tests.ThreeD.Converters
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class CartesianCoordinatesToStringConverterTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = new CartesianCoordinatesToStringConverter();
        }

        private CartesianCoordinatesToStringConverter m_Sut;

        [Test]
        public void Convert_SetsString_ForGivenData()
        {
            // Arrange
            var data = new CartesianCoordinates
                       {
                           X = 1.0,
                           Y = 2.0,
                           Z = 3.0,
                           IsUnknown = true
                       };

            m_Sut.Coordinates = data;

            // Act
            m_Sut.Convert();

            // Assert
            Assert.AreEqual("[1,2,3]",
                            m_Sut.String);
        }
    }
}