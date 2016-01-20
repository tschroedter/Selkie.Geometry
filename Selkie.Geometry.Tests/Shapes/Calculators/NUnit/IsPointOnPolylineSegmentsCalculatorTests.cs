using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Shapes.Calculators;

namespace Selkie.Geometry.Tests.Shapes.Calculators.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class IsPointOnPolylineSegmentsCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            m_Segments = new[]
                         {
                             new Line(0.0,
                                      0.0,
                                      100.0,
                                      100.0),
                             new Line(100.0,
                                      100.0,
                                      200.0,
                                      0.0)
                         };

            m_Sut = new IsPointOnPolylineSegmentsCalculator(m_Segments);
        }

        private IsPointOnPolylineSegmentsCalculator m_Sut;
        private Line[] m_Segments;

        [Test]
        public void IsOnLine_ReturnsFalse_ForPointNotOnLine()
        {
            // Arrange
            var point = new Point(-1234.0,
                                  0.0);

            // Act
            bool actual = m_Sut.IsOnLine(point);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsOnLine_ReturnsTrue_ForEndPoint()
        {
            // Arrange
            var point = new Point(200.0,
                                  0.0);

            // Act
            bool actual = m_Sut.IsOnLine(point);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void IsOnLine_ReturnsTrue_ForIntersectionPoint()
        {
            // Arrange
            var point = new Point(100.0,
                                  100.0);

            // Act
            bool actual = m_Sut.IsOnLine(point);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void IsOnLine_ReturnsTrue_ForStartPoint()
        {
            // Arrange
            var point = new Point(0.0,
                                  0.0);

            // Act
            bool actual = m_Sut.IsOnLine(point);

            // Assert
            Assert.True(actual);
        }
    }
}