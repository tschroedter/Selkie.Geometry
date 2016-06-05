using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;

namespace Selkie.Geometry.Tests.Surveying
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SurveyPolylineTests
    {
        [SetUp]
        public void Setup()
        {
            m_Polyline = Substitute.For <IPolyline>();

            m_Polyline.Id.Returns(1);
            m_Polyline.StartPoint.Returns(new Point(1.0,
                                                    2.0));
            m_Polyline.EndPoint.Returns(new Point(3.0,
                                                  4.0));
            m_Polyline.AngleToXAxisAtStartPoint.Returns(Angle.For45Degrees);
            m_Polyline.AngleToXAxisAtEndPoint.Returns(Angle.For180Degrees);
            m_Polyline.RunDirection.Returns(Constants.LineDirection.Forward);
            m_Polyline.Length.Returns(123.0);
            m_Polyline.IsUnknown.Returns(false);

            m_Sut = new SurveyPolyline(m_Polyline);
        }

        private IPolyline m_Polyline;
        private SurveyPolyline m_Sut;

        [Test]
        public void AngleToXAxisAtEndPoint_ReturnsLinesAngleToXAxis_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Polyline.AngleToXAxisAtEndPoint,
                            m_Sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void AngleToXAxisAtStartPoint_ReturnsLinesAngleToXAxis_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Polyline.AngleToXAxisAtStartPoint,
                            m_Sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void Constructor_SetsLine_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Polyline,
                            m_Sut.Polyline);
        }

        [Test]
        public void EndPoint_ReturnsLinesEndPoint_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Polyline.EndPoint,
                            m_Sut.EndPoint);
        }

        [Test]
        public void Id_ReturnsLinesId_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Polyline.Id,
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
            Assert.AreEqual(m_Polyline.Length,
                            m_Sut.Length);
        }

        [Test]
        public void Reverse_ReturnsReversedFeature_WhenCalled()
        {
            // Arrange
            // Act
            ISurveyFeature actual = m_Sut.Reverse();

            // Assert
            Assert.AreEqual(actual.Id,
                            m_Sut.Id,
                            "Id");
            Assert.AreEqual(m_Polyline.EndPoint,
                            actual.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_Polyline.StartPoint,
                            actual.EndPoint,
                            "EndPoint");
            Assert.AreEqual(Angle.For225Degrees,
                            actual.AngleToXAxisAtStartPoint,
                            "AngleToXAxisAtStartPoint");
            Assert.AreEqual(Angle.For360Degrees,
                            actual.AngleToXAxisAtEndPoint,
                            "AngleToXAxisAtEndPoint");
            Assert.AreEqual(Constants.LineDirection.Forward,
                            actual.RunDirection,
                            "RunDirection");
            Assert.AreEqual(m_Polyline.Length,
                            actual.Length,
                            "Length");
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
            Assert.AreEqual(m_Polyline.StartPoint,
                            m_Sut.StartPoint);
        }

        [Test]
        public void ToString_ReturnsString_WhenCalled()
        {
            // Arrange
            const string expected = "[Id: 1, IsUnknown: False] " +
                                    "[1.00,2.00] - [3.00,4.00] " +
                                    "[AngleAtStartPoint:45.00, AngleAtEndPoint:180.00] " +
                                    "[Length:123.00] " +
                                    "[RunDirection:Forward]";

            // Act
            string actual = m_Sut.ToString();

            // Assert
            Assert.AreEqual(expected,
                            actual);
        }
    }
}