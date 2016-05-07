using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Tests.Shapes.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class PolyineTests
    {
        [SetUp]
        public void Setup()
        {
            m_DoesNotMatter = new Point(0.0,
                                        0.0);

            m_StartPointOne = new Point(-10.0,
                                        -10.0);
            m_EndPointOne = new Point(10.0,
                                      10.0);
            m_StartPointTwo = new Point(-50.0,
                                        -50.0);
            m_EndPointTwo = new Point(100.0,
                                      100.0);

            m_Segment1 = Substitute.For <IPolylineSegment>();
            m_Segment1.StartPoint.Returns(m_StartPointOne);
            m_Segment1.EndPoint.Returns(m_EndPointOne);
            m_Segment1.AngleToXAxisAtStartPoint.Returns(Angle.For45Degrees);
            m_Segment1.Length.Returns(1.0);

            m_Segment2 = Substitute.For <IPolylineSegment>();
            m_Segment2.Length.Returns(2.0);
            m_Segment2.StartPoint.Returns(m_StartPointTwo);
            m_Segment2.EndPoint.Returns(m_EndPointTwo);
            m_Segment1.AngleToXAxisAtEndPoint.Returns(Angle.For180Degrees);

            m_Sut = new Polyline(123,
                                 Constants.LineDirection.Forward);
        }

        private Point m_EndPointOne;
        private Point m_EndPointTwo;
        private Polyline m_Sut;
        private IPolylineSegment m_Segment1;
        private IPolylineSegment m_Segment2;
        private Point m_StartPointOne;
        private Point m_StartPointTwo;
        private Point m_DoesNotMatter;

        [Test]
        public void AddTest()
        {
            // Arrange
            // Act
            m_Sut.AddSegment(m_Segment1);

            // Assert
            Assert.AreEqual(1,
                            m_Sut.Segments.Count(),
                            "Count");
            Assert.AreEqual(m_Segment1,
                            m_Sut.Segments.First(),
                            "First");
            Assert.AreEqual(1.0,
                            m_Sut.Length,
                            "Length");
            Assert.AreEqual(m_StartPointOne,
                            m_Sut.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_EndPointOne,
                            m_Sut.EndPoint,
                            "EndtPoint");
        }

        [Test]
        public void AddTwiceTest()
        {
            // Arrange
            // Act
            m_Sut.AddSegment(m_Segment1);
            m_Sut.AddSegment(m_Segment2);

            // Assert
            Assert.AreEqual(2,
                            m_Sut.Segments.Count(),
                            "Count");
            Assert.AreEqual(m_Segment1,
                            m_Sut.Segments.First(),
                            "First");
            Assert.AreEqual(m_Segment2,
                            m_Sut.Segments.Last(),
                            "Last");
            Assert.AreEqual(3.0,
                            m_Sut.Length,
                            "Length");
            Assert.AreEqual(m_StartPointOne,
                            m_Sut.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_EndPointTwo,
                            m_Sut.EndPoint,
                            "EndtPoint");
        }

        [Test]
        public void AngleToXAxisAtEndPoint_ReturnsAngle_WhenSegmentAreAdded()
        {
            // Arrange
            // Act
            m_Sut.AddSegment(m_Segment1);

            // Assert
            Assert.AreEqual(m_Segment1.AngleToXAxisAtEndPoint,
                            m_Sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void AngleToXAxisAtEndPoint_ReturnsAngle_WhenSegmentsAreAdded()
        {
            // Arrange
            m_Sut.AddSegment(m_Segment1);

            // Act
            m_Sut.AddSegment(m_Segment2);

            // Assert
            Assert.AreEqual(m_Segment1.AngleToXAxisAtEndPoint,
                            m_Sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void AngleToXAxisAtEndPoint_ReturnsUnknown_AsDefault()
        {
            // Arrange
            // Act
            var sut = new Polyline(1,
                                   Constants.LineDirection.Forward);

            // Assert
            Assert.AreEqual(Angle.Unknown,
                            sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void AngleToXAxisAtStartPoint_ReturnsAngle_WhenSegmentAreAdded()
        {
            // Arrange
            // Act
            m_Sut.AddSegment(m_Segment1);

            // Assert
            Assert.AreEqual(m_Segment1.AngleToXAxisAtStartPoint,
                            m_Sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void AngleToXAxisAtStartPoint_ReturnsAngle_WhenSegmentsAreAdded()
        {
            // Arrange
            m_Sut.AddSegment(m_Segment1);

            // Act
            m_Sut.AddSegment(m_Segment2);

            // Assert
            Assert.AreEqual(m_Segment1.AngleToXAxisAtStartPoint,
                            m_Sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void AngleToXAxisAtStartPoint_ReturnsUnknown_AsDefault()
        {
            // Arrange
            // Act
            var sut = new Polyline(1,
                                   Constants.LineDirection.Forward);

            // Assert
            Assert.AreEqual(Angle.Unknown,
                            sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void Constructor_SetsId_WhenCalled()
        {
            Assert.AreEqual(123,
                            m_Sut.Id);
        }

        [Test]
        public void Constructor_SetsIsUnknownToFalse_WhenCalled()
        {
            Assert.AreEqual(false,
                            m_Sut.IsUnknown);
        }

        [Test]
        public void Constructor_SetsLength_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.AddSegment(m_Segment1);
            m_Sut.AddSegment(m_Segment2);

            // Assert
            Assert.AreEqual(3.0,
                            m_Sut.Length);
        }

        [Test]
        public void Constructor_SetsRunDirectionToForward_WhenCalled()
        {
            Assert.AreEqual(Constants.LineDirection.Forward,
                            m_Sut.RunDirection);
        }

        [Test]
        public void DefaultEndPointTest()
        {
            Assert.AreEqual(Point.Unknown,
                            m_Sut.EndPoint);
        }

        [Test]
        public void DefaultSegmentsTest()
        {
            Assert.AreEqual(0,
                            m_Sut.Segments.Count());
        }

        [Test]
        public void DefaultStartPointTest()
        {
            Assert.AreEqual(Point.Unknown,
                            m_Sut.StartPoint);
        }

        [Test]
        public void DetermineEndPointReturnsPointUnknownForLastIsNullTest()
        {
            // Arrange
            // Act
            Point actual = m_Sut.DetermineEndPoint(new List <IPolylineSegment>());

            // Assert
            Assert.AreEqual(Point.Unknown,
                            actual);
        }

        [Test]
        public void DetermineStartPointReturnsPointUnknownForLastIsNullTest()
        {
            // Arrange
            // Act
            Point actual = m_Sut.DetermineStartPoint(new List <IPolylineSegment>());

            // Assert
            Assert.AreEqual(Point.Unknown,
                            actual);
        }

        [Test]
        public void IsOnLine_CallsIsOnlineOnSegments_WhenCalled()
        {
            // Arrange
            m_Sut.AddSegment(m_Segment1);
            m_Sut.AddSegment(m_Segment2);

            // Act
            m_Sut.IsOnLine(m_StartPointOne);

            // Assert
            m_Segment1.Received().IsOnLine(m_StartPointOne);
            m_Segment2.Received().IsOnLine(m_StartPointOne);
        }

        [Test]
        public void IsOnLine_ReturnsTrue_ForPointIsOnLine()
        {
            // Arrange
            m_Segment1.IsOnLine(m_StartPointOne).Returns(true);

            m_Sut.AddSegment(m_Segment1);
            m_Sut.AddSegment(m_Segment2);

            // Act
            bool actual = m_Sut.IsOnLine(m_StartPointOne);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ReverseCallsReverseOnSegmentsTest()
        {
            // Arrange
            m_Sut.AddSegment(m_Segment1);
            m_Sut.AddSegment(m_Segment2);

            // Act
            m_Sut.Reverse();

            // Assert
            m_Segment1.Received().Reverse();
            m_Segment2.Received().Reverse();
        }

        [Test]
        public void TurnDirectionToPoint_ReturnsUnknown_ForNoSegments()
        {
            // Arrange
            // Act
            Constants.TurnDirection actual = m_Sut.TurnDirectionToPoint(m_DoesNotMatter);

            // Assert
            Assert.AreEqual(Constants.TurnDirection.Unknown,
                            actual);
        }

        [Test]
        public void Unknown_AngleToXAxisAtEndPointReturnsUnknown_WhenCreated()
        {
            // Arrange
            // Act
            Polyline sut = Polyline.Unknown;

            // Assert
            Assert.AreEqual(Angle.Unknown,
                            sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void Unknown_AngleToXAxisAtStartPointReturnsUnknown_WhenCreated()
        {
            // Arrange
            // Act
            Polyline sut = Polyline.Unknown;

            // Assert
            Assert.AreEqual(Angle.Unknown,
                            sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void Unknown_EndPointReturnsUnknown_WhenCreated()
        {
            // Arrange
            // Act
            Polyline sut = Polyline.Unknown;

            // Assert
            Assert.AreEqual(Point.Unknown,
                            sut.EndPoint);
        }

        [Test]
        public void Unknown_HasUnknownId_WhenCreated()
        {
            // Arrange
            // Act
            Polyline sut = Polyline.Unknown;

            // Assert
            Assert.AreEqual(int.MinValue,
                            sut.Id);
        }

        [Test]
        public void Unknown_ReturnsTrueForIsUnknown_WhenCreated()
        {
            // Arrange
            // Act
            Polyline sut = Polyline.Unknown;

            // Assert
            Assert.True(sut.IsUnknown);
        }

        [Test]
        public void Unknown_RunDirectionReturnsUnknown_WhenCreated()
        {
            // Arrange
            // Act
            Polyline sut = Polyline.Unknown;

            // Assert
            Assert.AreEqual(Constants.LineDirection.Unknown,
                            sut.RunDirection);
        }

        [Test]
        public void Unknown_StartPointReturnsUnknown_WhenCreated()
        {
            // Arrange
            // Act
            Polyline sut = Polyline.Unknown;

            // Assert
            Assert.AreEqual(Point.Unknown,
                            sut.StartPoint);
        }
    }
}