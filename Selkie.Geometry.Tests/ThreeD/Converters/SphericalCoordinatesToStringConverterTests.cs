using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Converters;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.Tests.ThreeD.Converters
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SphericalCoordinatesToStringConverterTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = new SphericalCoordinatesToStringConverter();
        }

        private SphericalCoordinatesToStringConverter m_Sut;

        [Test]
        public void Convert_SetsString_ForGivenData()
        {
            // Arrange
            var data = new SphericalCoordinates
                       {
                           Radius = 1.0,
                           Phi = Angle.ForZeroDegrees,
                           Theta = Angle.For45Degrees,
                           IsUnknown = true
                       };

            m_Sut.Coordinates = data;

            // Act
            m_Sut.Convert();

            // Assert
            Assert.AreEqual("[1,[Radians: 0.00 Degrees: 0.00],[Radians: 0.79 Degrees: 45.00]]",
                            m_Sut.String);
        }
    }
}