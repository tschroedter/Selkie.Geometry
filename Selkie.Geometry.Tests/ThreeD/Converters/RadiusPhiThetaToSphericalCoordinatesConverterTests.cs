using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Tests.ThreeD.Primitives;
using Selkie.Geometry.ThreeD.Converters;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.Tests.ThreeD.Converters
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class RadiusPhiThetaToSphericalCoordinatesConverterTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = new RadiusPhiThetaToSphericalCoordinatesConverter();
        }

        private RadiusPhiThetaToSphericalCoordinatesConverter m_Sut;

        private IEnumerable <SphericalCoordinates> CreateArrayOfThreeSphericalCoordinates()
        {
            var one = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.ForZeroDegrees,
                          Theta = Angle.For180Degrees
                      };
            var two = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.For45Degrees,
                          Theta = Angle.For225Degrees
                      };
            var three = new SphericalCoordinates
                        {
                            Radius = 1.0,
                            Phi = Angle.For90Degrees,
                            Theta = Angle.For270Degrees
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <Angle> CreateArrayOfThreePhiAngles()
        {
            return new[]
                   {
                       Angle.ForZeroDegrees,
                       Angle.For45Degrees,
                       Angle.For90Degrees
                   };
        }

        private IEnumerable <Angle> CreateArrayOfThreeThetaAngles()
        {
            return new[]
                   {
                       Angle.For180Degrees,
                       Angle.For225Degrees,
                       Angle.For270Degrees
                   };
        }

        private IEnumerable <Angle> CreateArrayOfOnePhiAngles()
        {
            return new[]
                   {
                       Angle.For90Degrees
                   };
        }

        private IEnumerable <Angle> CreateArrayOfOneThetaAngles()
        {
            return new[]
                   {
                       Angle.For270Degrees
                   };
        }

        [Test]
        public void Constructor_SetsPhiAngles_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(m_Sut.PhiAngles);
        }

        [Test]
        public void Constructor_SetsSphericalCoordinates_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(m_Sut.SphericalCoordinates);
        }

        [Test]
        public void Constructor_SetsThetaAngles_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(m_Sut.ThetaAngles);
        }

        [Test]
        public void Convert_SetsSphericalCoordinates_ForGivenValues()
        {
            // Arrange
            IEnumerable <SphericalCoordinates> expected = CreateArrayOfThreeSphericalCoordinates();

            m_Sut.Radius = 1.0;
            m_Sut.PhiAngles = CreateArrayOfThreePhiAngles();
            m_Sut.ThetaAngles = CreateArrayOfThreeThetaAngles();

            // Act
            m_Sut.Convert();

            // Assert
            SphericalCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.SphericalCoordinates);
        }
    }
}