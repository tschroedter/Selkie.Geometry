using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;

namespace Selkie.Geometry.Tests.Surveying.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SurveyFeatureTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = SurveyFeature.Unknown;
        }

        private SurveyFeature m_Sut;

        [Test]
        public void AngleToXAxisAtEndPoint_ReturnsUnknown_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Angle.Unknown,
                            m_Sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void AngleToXAxisAtStartPoint_ReturnsUnknown_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Angle.Unknown,
                            m_Sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void EndPoint_ReturnsUnknown_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Point.Unknown,
                            m_Sut.EndPoint);
        }

        [Test]
        public void Id_ReturnsUnknown_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(SurveyFeature.UnknownId,
                            m_Sut.Id);
        }

        [Test]
        public void IsUnknown_ReturnsTrue_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.True(m_Sut.IsUnknown);
        }

        [Test]
        public void Length_ReturnsUnknown_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(SurveyFeature.UnknownLength,
                            m_Sut.Length);
        }

        [Test]
        public void RunDirection_ReturnsUnknown_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Constants.LineDirection.Unknown,
                            m_Sut.RunDirection);
        }

        [Test]
        public void StartPoint_ReturnsUnknown_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Point.Unknown,
                            m_Sut.StartPoint);
        }
    }
}