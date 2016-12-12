using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.Surveying
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SurveyFeatureDataTests
    {
        [SetUp]
        public void Setup()
        {
            m_StartPoint = new Point(1.0,
                                     2.0);
            m_EndPoint = new Point(3.0,
                                   4.0);

            m_Sut = new SurveyFeatureData(
                                          1,
                                          m_StartPoint,
                                          m_EndPoint,
                                          Angle.For45Degrees,
                                          Angle.For90Degrees,
                                          Constants.LineDirection.Forward,
                                          12.34,
                                          true);
        }

        private Point m_StartPoint;
        private Point m_EndPoint;
        private SurveyFeatureData m_Sut;

        [Test]
        public void Constructor_SetsAngleToXAxisAtEndPoint_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Angle.For90Degrees,
                            m_Sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void Constructor_SetsAngleToXAxisAtStartPoint_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Angle.For45Degrees,
                            m_Sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void Constructor_SetsEndPoint_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_EndPoint,
                            m_Sut.EndPoint);
        }

        [Test]
        public void Constructor_SetsId_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(1,
                            m_Sut.Id);
        }

        [Test]
        public void Constructor_SetsIsUnknown_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.True(m_Sut.IsUnknown);
        }

        [Test]
        public void Constructor_SetsLength_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            NUnitHelper.AssertIsEquivalent(12.34,
                                           m_Sut.Length);
        }

        [Test]
        public void Constructor_SetsRunDirection_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Constants.LineDirection.Forward,
                            m_Sut.RunDirection);
        }

        [Test]
        public void Constructor_SetsStartPoint_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_StartPoint,
                            m_Sut.StartPoint);
        }
    }
}