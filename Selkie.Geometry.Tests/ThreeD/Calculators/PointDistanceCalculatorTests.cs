using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;
using Selkie.Geometry.ThreeD.Calculators;
using Selkie.Geometry.ThreeD.Shapes;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.ThreeD.Calculators
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class PointDistanceCalculatorTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Calculate_SetsDistance_ForGivenPoints(
            [NotNull] PointDistanceCalculator sut)
        {
            // Arrange
            const double expected = 1.7320508075688772;

            var from = new Point(3.0,
                                 4.0,
                                 5.0);
            var to = new Point(4.0,
                               5.0,
                               6.0);

            sut.FromPoint = from;
            sut.ToPoint = to;

            // Act
            sut.Calculate();

            // Assert
            NUnitHelper.AssertIsEquivalent(expected,
                                           sut.Distance);
        }

        private static PointDistanceCalculator CreateSut()
        {
            return new PointDistanceCalculator();
        }

        [Test]
        public void Constructor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(new PointDistanceCalculator());
        }

        [Test]
        public void Constructor_SetsDistance_WhenCalled()
        {
            // Arrange
            // Act
            PointDistanceCalculator sut = CreateSut();

            // Assert
            Assert.AreEqual(double.NaN,
                            sut.Distance);
        }

        [Test]
        public void Constructor_SetsFromPoint_WhenCalled()
        {
            // Arrange
            // Act
            PointDistanceCalculator sut = CreateSut();

            // Assert
            Assert.True(sut.FromPoint.IsUnknown);
        }

        [Test]
        public void Constructor_SetsToPoint_WhenCalled()
        {
            // Arrange
            // Act
            PointDistanceCalculator sut = CreateSut();

            // Assert
            Assert.True(sut.ToPoint.IsUnknown);
        }
    }
}