using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Shapes.Calculators;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Tests.Shapes.Calculators.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class IsPointOnArcSegmentCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            m_Circle = new Circle(2.0,
                                  3.0,
                                  4.0);
            m_StartPoint = new Point(2.0,
                                     7.0);
            m_EndPoint = new Point(2.0,
                                   -1.0);
            m_TurnDirection = Constants.TurnDirection.Counterclockwise;

            m_ArcSegment = new ArcSegment(m_Circle,
                                          m_StartPoint,
                                          m_EndPoint,
                                          m_TurnDirection);
        }

        private Circle m_Circle;
        private Point m_StartPoint;
        private Point m_EndPoint;
        private Constants.TurnDirection m_TurnDirection;
        private ArcSegment m_ArcSegment;

        private static Point PointOnCircle([NotNull] IArcSegment segment,
                                           double angleInDegreesRelativeToYAxisCounterclockwise)
        {
            return PointOnCircle(segment.CentrePoint,
                                 segment.Radius,
                                 angleInDegreesRelativeToYAxisCounterclockwise);
        }

        private static Point PointOnCircle([NotNull] Point centrePoint,
                                           double radius,
                                           double angleInDegreesRelativeToYAxisCounterclockwise)
        {
            Angle angle = Angle.FromDegrees(angleInDegreesRelativeToYAxisCounterclockwise);
            var point = new Point(0.0,
                                  0.0);
            point = point.Move(radius,
                               angle.Radians);

            point = new Point(point.X + centrePoint.X,
                              point.Y + centrePoint.Y);

            return point;
        }

        [Test]
        public void Calculate_DoesNothing_ForArcSegmentIsUnknown()
        {
            // Arrange
            var sut = new IsPointOnArcSegmentCalculator
                      {
                          ArcSegment = ArcSegment.Unknown,
                          Point = m_StartPoint
                      };

            // Act
            sut.Calculate();

            // Assert
            Assert.False(sut.IsPointOnArcSegment);
        }

        [Test]
        public void Calculate_DoesNothing_ForPointIsUnknown()
        {
            // Arrange
            var sut = new IsPointOnArcSegmentCalculator
                      {
                          ArcSegment = m_ArcSegment,
                          Point = Point.Unknown
                      };

            // Act
            sut.Calculate();

            // Assert
            Assert.False(sut.IsPointOnArcSegment);
        }

        [Test]
        public void IsPointOnArcSegment_ReturnsFalse_ForHalfwayPointPlusDeltaTwoTimes()
        {
            // Arrange
            const double doubleDelta = SelkieConstants.EpsilonDistance * 2;
            var halfway = new Point(6.0 + doubleDelta,
                                    3.0);
            var sut = new IsPointOnArcSegmentCalculator
                      {
                          ArcSegment = m_ArcSegment,
                          Point = halfway
                      };

            // Act
            sut.Calculate();

            // Assert
            Assert.False(sut.IsPointOnArcSegment);
        }

        [Test]
        public void IsPointOnArcSegment_ReturnsFalse_ForPointAfterEndPoint()
        {
            // Arrange
            Point point = PointOnCircle(m_ArcSegment.CentrePoint,
                                        m_ArcSegment.Radius,
                                        180.5);
            // Act
            var sut = new IsPointOnArcSegmentCalculator
                      {
                          ArcSegment = m_ArcSegment,
                          Point = point
                      };

            // Assert
            Assert.False(sut.IsPointOnArcSegment);
        }

        [Test]
        public void IsPointOnArcSegment_ReturnsFalse_ForPointBeforeStartPoint()
        {
            // Arrange
            Point point = PointOnCircle(m_ArcSegment,
                                        359.5);

            var sut = new IsPointOnArcSegmentCalculator
                      {
                          ArcSegment = m_ArcSegment,
                          Point = point
                      };

            // Act
            sut.Calculate();

            // Assert
            Assert.False(sut.IsPointOnArcSegment);
        }

        [Test]
        public void IsPointOnArcSegment_ReturnsTrue_ForEndPoint()
        {
            // Arrange
            var sut = new IsPointOnArcSegmentCalculator
                      {
                          ArcSegment = m_ArcSegment,
                          Point = m_EndPoint
                      };

            // Act
            sut.Calculate();

            // Assert
            Assert.True(sut.IsPointOnArcSegment);
        }

        [Test]
        public void IsPointOnArcSegment_ReturnsTrue_ForHalfwayPoint()
        {
            // Arrange
            var halfway = new Point(6.0,
                                    3.0);
            var sut = new IsPointOnArcSegmentCalculator
                      {
                          ArcSegment = m_ArcSegment,
                          Point = halfway
                      };

            // Act
            sut.Calculate();

            // Assert
            Assert.True(sut.IsPointOnArcSegment);
        }

        [Test]
        public void IsPointOnArcSegment_ReturnsTrue_ForStartPoint()
        {
            // Arrange
            var sut = new IsPointOnArcSegmentCalculator
                      {
                          ArcSegment = m_ArcSegment,
                          Point = m_StartPoint
                      };

            // Act
            sut.Calculate();

            // Assert
            Assert.True(sut.IsPointOnArcSegment);
        }
    }
}