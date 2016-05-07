using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.Shapes.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class TurnCircleArcSegmentTests
    {
        [SetUp]
        public void Setup()
        {
            m_Circle = new Circle(0.0,
                                  0.0,
                                  100);
            m_StartPoint = new Point(100.0,
                                     0.0);
            m_EndPoint = new Point(0.0,
                                   100.0);

            m_Sut = new TurnCircleArcSegment(m_Circle,
                                             Constants.TurnDirection.Clockwise,
                                             Constants.CircleOrigin.Start,
                                             m_StartPoint,
                                             m_EndPoint);
        }

        private TurnCircleArcSegment m_Sut;
        private ICircle m_Circle;
        private Point m_StartPoint;
        private Point m_EndPoint;

        [Test]
        public void AngleToXAxisAtEndPoint_ReturnsAngle_ForClockwise()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Angle.ForZeroDegrees,
                            m_Sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void AngleToXAxisAtEndPoint_ReturnsAngle_ForCounterclockwiseTest()
        {
            // Arrange
            // Act
            var sut = new TurnCircleArcSegment(m_Circle,
                                               Constants.TurnDirection.Counterclockwise,
                                               Constants.CircleOrigin.Start,
                                               m_StartPoint,
                                               m_EndPoint);

            // Assert
            Assert.AreEqual(Angle.For180Degrees,
                            sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void AngleToXAxisAtStartPoint_ReturnsAngle_ForClockwise()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Angle.For270Degrees,
                            m_Sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void AngleToXAxisAtStartPoint_ReturnsAngle_ForCounterclockwise()
        {
            // Arrange
            // Act
            var sut = new TurnCircleArcSegment(m_Circle,
                                               Constants.TurnDirection.Counterclockwise,
                                               Constants.CircleOrigin.Start,
                                               m_StartPoint,
                                               m_EndPoint);

            // Assert
            Assert.AreEqual(Angle.For90Degrees,
                            sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void Constructor_CreatesArcSegment_WhenCalled()
        {
            // Arrange
            Angle expectedAngle = Angle.For270Degrees;

            // Act
            IArcSegment actual = m_Sut.ArcSegment;

            // Assert
            Assert.AreEqual(m_Circle.CentrePoint,
                            actual.CentrePoint,
                            "CentrePoint");
            Assert.AreEqual(m_StartPoint,
                            actual.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_EndPoint,
                            actual.EndPoint,
                            "EndPoint");
            Assert.AreEqual(expectedAngle,
                            actual.AngleClockwise,
                            "AngleClockwise");
        }

        [Test]
        public void Constructor_SetsAngle_WhenCalled()
        {
            Assert.AreEqual(Angle.For270Degrees,
                            m_Sut.Angle);
        }

        [Test]
        public void Constructor_SetsAngleToXAxisAtEndPoint_ForInternal()
        {
            // Arrange
            var arcSegment = Substitute.For <IArcSegment>();
            arcSegment.AngleToXAxisAtEndPoint.Returns(Angle.For45Degrees);
            var sut = new TurnCircleArcSegment(arcSegment,
                                               Constants.TurnDirection.Clockwise,
                                               Constants.CircleOrigin.Start);

            // Act
            sut.IsOnLine(m_StartPoint);

            // Assert
            Assert.AreEqual(arcSegment.AngleToXAxisAtEndPoint,
                            sut.AngleToXAxisAtEndPoint);
        }

        [Test]
        public void Constructor_SetsAngleToXAxisAtStartPoint_ForInternal()
        {
            // Arrange
            var arcSegment = Substitute.For <IArcSegment>();
            arcSegment.AngleToXAxisAtStartPoint.Returns(Angle.For45Degrees);
            var sut = new TurnCircleArcSegment(arcSegment,
                                               Constants.TurnDirection.Clockwise,
                                               Constants.CircleOrigin.Start);

            // Act
            sut.IsOnLine(m_StartPoint);

            // Assert
            Assert.AreEqual(arcSegment.AngleToXAxisAtStartPoint,
                            sut.AngleToXAxisAtStartPoint);
        }

        [Test]
        public void Constructor_SetsCentrePoint_WhenCalled()
        {
            Assert.AreEqual(m_Circle.CentrePoint,
                            m_Sut.CentrePoint);
        }

        [Test]
        public void Constructor_SetsIsUnknown_WhenCalled()
        {
            Assert.False(m_Sut.IsUnknown);
        }

        [Test]
        public void Constructor_SetsIsUnknown_WhenCalledDirectionTest()
        {
            Assert.AreEqual(Constants.TurnDirection.Clockwise,
                            m_Sut.TurnDirection);
        }

        [Test]
        public void Constructor_SetsLength_WhenCalled()
        {
            NUnitHelper.AssertIsEquivalent(471.23889803846896,
                                           m_Sut.Length,
                                           Constants.EpsilonDistance,
                                           "Length");
        }

        [Test]
        public void Constructor_SetsLengthClockwise_WhenCalled()
        {
            NUnitHelper.AssertIsEquivalent(471.23889803846896,
                                           m_Sut.LengthClockwise,
                                           Constants.EpsilonDistance,
                                           "LengthClockwise");
        }

        [Test]
        public void Constructor_SetsLengthCounterClockwise_WhenCalled()
        {
            NUnitHelper.AssertIsEquivalent(157.07963267948966,
                                           m_Sut.LengthCounterClockwise,
                                           Constants.EpsilonDistance,
                                           "LengthCounterClockwise");
        }

        [Test]
        public void Constructor_SetsOrigin_WhenCalled()
        {
            Assert.AreEqual(Constants.CircleOrigin.Start,
                            m_Sut.CircleOrigin);
        }

        [Test]
        public void Constructor_SetsRadius_WhenCalled()
        {
            Assert.AreEqual(100.0,
                            m_Sut.Radius);
        }

        [Test]
        public void Constructor_SetsStartPoint_WhenCalled()
        {
            Assert.AreEqual(m_StartPoint,
                            m_Sut.StartPoint);
        }

        [Test]
        public void IsOnLine_CallsArcSegment_WhenCalled()
        {
            // Arrange
            var arcSegment = Substitute.For <IArcSegment>();
            var sut = new TurnCircleArcSegment(arcSegment,
                                               Constants.TurnDirection.Clockwise,
                                               Constants.CircleOrigin.Start);

            // Act
            sut.IsOnLine(m_StartPoint);

            // Assert
            arcSegment.Received().IsOnLine(m_StartPoint);
        }

        [Test]
        public void IsOnLine_ReturnsValueFromCallingArcSegment_ForStartPoint()
        {
            // Arrange
            var arcSegment = Substitute.For <IArcSegment>();
            arcSegment.IsOnLine(m_StartPoint).Returns(true);

            var sut = new TurnCircleArcSegment(arcSegment,
                                               Constants.TurnDirection.Clockwise,
                                               Constants.CircleOrigin.Start);

            // Act
            bool actual = sut.IsOnLine(m_StartPoint);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void Reverse_ReversesPolyline_WhenCalled()
        {
            // Arrange
            // Act
            var actual = m_Sut.Reverse() as ITurnCircleArcSegment;

            // Assert
            Assert.NotNull(actual);
            Assert.AreEqual(m_Sut.LengthCounterClockwise,
                            actual.Length,
                            "Length");
            Assert.AreEqual(m_Sut.EndPoint,
                            actual.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_Sut.StartPoint,
                            actual.EndPoint,
                            "EndPoint");
            Assert.AreEqual(m_Sut.AngleCounterClockwise,
                            actual.AngleClockwise,
                            "AngleClockwise");
            Assert.AreEqual(m_Sut.TurnDirection,
                            actual.TurnDirection,
                            "Direction");
        }

        [Test]
        public void ToString_ReturnsString_WhenCalled()
        {
            // Arrange
            const string expected = "CentrePoint: [0,0] StartPoint: [100,0] EndPoint: [0,100] Direction: Clockwise";

            // Act
            string actual = m_Sut.ToString();

            // Assert
            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void Unknown_SetsAngleToXAxisAtEndPointToUnknown_WhenCreated()
        {
            // Arrange
            // Act
            ITurnCircleArcSegment actual = TurnCircleArcSegment.Unknown;

            // Assert
            Assert.AreEqual(Angle.Unknown,
                            actual.AngleToXAxisAtEndPoint,
                            "AngleToXAxisAtEndPoint");
        }

        [Test]
        public void Unknown_SetsAngleToXAxisAtStartPointToUnknown_WhenCreated()
        {
            // Arrange
            // Act
            ITurnCircleArcSegment actual = TurnCircleArcSegment.Unknown;

            // Assert
            Assert.AreEqual(Angle.Unknown,
                            actual.AngleToXAxisAtStartPoint,
                            "AngleToXAxisAtStartPoint");
        }

        [Test]
        public void Unknown_SetsArcSegmentIsUnknownToTrue_WhenCreated()
        {
            // Arrange
            // Act
            ITurnCircleArcSegment actual = TurnCircleArcSegment.Unknown;

            // Assert
            Assert.True(actual.ArcSegment.IsUnknown,
                        "ArcSegment.IsUnknown");
        }

        [Test]
        public void Unknown_SetsIsUnknownToTrue_WhenCreated()
        {
            // Arrange
            // Act
            ITurnCircleArcSegment actual = TurnCircleArcSegment.Unknown;

            // Assert
            Assert.True(actual.IsUnknown,
                        "IsUnknown");
        }
    }
}