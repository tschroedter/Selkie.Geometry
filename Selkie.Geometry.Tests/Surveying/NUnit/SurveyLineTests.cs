using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;

namespace Selkie.Geometry.Tests.Surveying.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SurveyLineTests
    {
        [SetUp]
        public void Setup()
        {
            m_Line = new Line(1,
                              2.0,
                              3.0,
                              4.0,
                              5.0);

            m_Sut = new SurveyLine(m_Line);
        }

        private Line m_Line;
        private SurveyLine m_Sut;

        [Test]
        public void AngleToXAxisAtEndPoint_ReturnsLinesAngleToXAxis_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Line.AngleToXAxis,
                            m_Sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void AngleToXAxisAtStartPoint_ReturnsLinesAngleToXAxis_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Line.AngleToXAxis,
                            m_Sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void EndPoint_ReturnsLinesEndPoint_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Line.EndPoint,
                            m_Sut.EndPoint);
        }

        [Test]
        public void Id_ReturnsLinesId_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Line.Id,
                            m_Sut.Id);
        }

        [Test]
        public void IsUnknown_ReturnsLineRunDirection_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.False(m_Sut.IsUnknown);
        }

        [Test]
        public void Length_ReturnsLinesLength_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Line.Length,
                            m_Sut.Length);
        }

        [Test]
        public void Reverse_ReturnsReversedFeature_WhenCalled()
        {
            // Arrange
            // Act
            ISurveyFeature actual = m_Sut.Reverse();

            // Assert
            Assert.AreEqual(m_Line.Id,
                            actual.Id,
                            "Id");
            Assert.AreEqual(m_Line.EndPoint,
                            actual.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_Line.StartPoint,
                            actual.EndPoint,
                            "EndPoint");
        }

        [Test]
        public void RunDirection_ReturnsLineRunDirection_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Constants.LineDirection.Forward,
                            m_Sut.RunDirection);
        }

        [Test]
        public void StartPoint_ReturnsLinesStartPoint_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Line.StartPoint,
                            m_Sut.StartPoint);
        }
    }
}