using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Surveying;

namespace Selkie.Geometry.Tests.Surveying
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SurveyGeoJsonFeatureTests
    {
        [SetUp]
        public void Setup()
        {
            m_SurveyFeature = Substitute.For <ISurveyFeature>();
            m_GeoJson = "GeoJson";

            m_Sut = new SurveyGeoJsonFeature(m_SurveyFeature,
                                             m_GeoJson);
        }

        private ISurveyFeature m_SurveyFeature;
        private SurveyGeoJsonFeature m_Sut;
        private string m_GeoJson;

        [Test]
        public void Constructor_SetsSurveyFeature_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_SurveyFeature,
                            m_Sut.SurveyFeature);
        }

        [Test]
        public void Constructor_SetsSurveyFeatureAsGeoJson_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_GeoJson,
                            m_Sut.SurveyFeatureAsGeoJson);
        }

        [Test]
        public void Id_ReturnsSurveyFeatureId_WhenCalled()
        {
            // Arrange
            m_SurveyFeature.Id.Returns(1);

            // Act
            // Assert
            Assert.AreEqual(1,
                            m_Sut.Id);
        }

        [Test]
        public void Id_ReturnsSurveyFeatureIsUnknown_WhenCalled()
        {
            // Arrange
            m_SurveyFeature.IsUnknown.Returns(true);

            // Act
            // Assert
            Assert.True(m_Sut.IsUnknown);
        }

        [Test]
        public void IsUnknown_ReturnsTrue_ForUnknown()
        {
            // Arrange
            // Act
            SurveyGeoJsonFeature sut = SurveyGeoJsonFeature.Unknown;

            // Assert
            Assert.True(sut.IsUnknown);
        }

        [Test]
        public void SurveyFeature_ReturnsUnknown_ForUnknown()
        {
            // Arrange
            // Act
            SurveyGeoJsonFeature sut = SurveyGeoJsonFeature.Unknown;

            // Assert
            Assert.True(sut.SurveyFeature.IsUnknown);
        }


        [Test]
        public void SurveyFeatureAsGeoJson_ReturnsEmpty_ForUnknown()
        {
            // Arrange
            // Act
            SurveyGeoJsonFeature sut = SurveyGeoJsonFeature.Unknown;

            // Assert
            Assert.AreEqual(string.Empty,
                            sut.SurveyFeatureAsGeoJson);
        }

        [Test]
        public void ToString_ReturnsString_WhenCalled()
        {
            // Arrange
            const string expected = "Castle.Proxies.ISurveyFeatureProxy " +
                                    "[GeoJson:GeoJson]";

            // Act
            string actual = m_Sut.ToString();

            // Assert
            Assert.AreEqual(expected,
                            actual);
        }
    }
}