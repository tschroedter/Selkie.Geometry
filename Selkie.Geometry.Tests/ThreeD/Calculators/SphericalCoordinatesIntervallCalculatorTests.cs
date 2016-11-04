using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Calculators;

namespace Selkie.Geometry.Tests.ThreeD.Calculators
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SphericalCoordinatesIntervallCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            m_FromAngle = Angle.ForZeroDegrees;
            m_ToAngle = Angle.For90Degrees;

            m_Calculator = Substitute.For <IAngelInterpolationCalculator>();
            m_Calculator.Angles.Returns(m_PhiAngles,
                                        m_ThetaAngles);

            m_Sut = new SphericalCoordinatesIntervallCalculator(m_Calculator);
        }

        private IAngelInterpolationCalculator m_Calculator;
        private SphericalCoordinatesIntervallCalculator m_Sut;
        private Angle m_FromAngle;
        private Angle m_ToAngle;

        private readonly Angle[] m_PhiAngles =
        {
            Angle.ForZeroDegrees
        };

        private readonly Angle[] m_ThetaAngles =
        {
            Angle.For45Degrees
        };

        [Test]
        public void Calculate_SetsPhiAngles_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Calculate();

            // Assert
            Assert.AreEqual(m_PhiAngles,
                            m_Sut.PhiAngles);
        }

        [Test]
        public void Calculate_SetsThetaAngles_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Calculate();

            // Assert
            Assert.AreEqual(m_ThetaAngles,
                            m_Sut.ThetaAngles);
        }

        [Test]
        public void CalculateIntervallAngles_CallsCalculate_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.CalculateIntervallAngles(m_FromAngle,
                                           m_ToAngle);

            // Assert
            m_Calculator.Received().Calculate();
        }

        [Test]
        public void CalculateIntervallAngles_SetsFromAngle_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.CalculateIntervallAngles(m_FromAngle,
                                           m_ToAngle);

            // Assert
            Assert.AreEqual(m_FromAngle,
                            m_Calculator.FromAngle);
        }

        [Test]
        public void CalculateIntervallAngles_SetsSteps_WhenCalled()
        {
            // Arrange
            m_Sut.Steps = 10;

            // Act
            m_Sut.CalculateIntervallAngles(m_FromAngle,
                                           m_ToAngle);

            // Assert
            Assert.AreEqual(m_Sut.Steps,
                            m_Calculator.Steps);
        }

        [Test]
        public void CalculateIntervallAngles_SetsToAngle_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.CalculateIntervallAngles(m_FromAngle,
                                           m_ToAngle);

            // Assert
            Assert.AreEqual(m_ToAngle,
                            m_Calculator.ToAngle);
        }

        [Test]
        public void CalculateIntervallAngles_SetsTurnDirection_WhenCalled()
        {
            // Arrange
            m_Sut.TurnDirection = Constants.TurnDirection.Counterclockwise;

            // Act
            m_Sut.CalculateIntervallAngles(m_FromAngle,
                                           m_ToAngle);

            // Assert
            Assert.AreEqual(m_Sut.TurnDirection,
                            m_Calculator.TurnDirection);
        }

        [Test]
        public void Constructor_SetsDefaultPhiAngles_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(m_Sut.PhiAngles);
        }

        [Test]
        public void Constructor_SetsDefaultRadius_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(0.0,
                            m_Sut.Radius);
        }

        [Test]
        public void Constructor_SetsDefaultSteps_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(3,
                            m_Sut.Steps);
        }

        [Test]
        public void Constructor_SetsDefaultThetaAngles_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(m_Sut.ThetaAngles);
        }

        [Test]
        public void Constructor_SetsDefaultTurnDirection_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(Constants.TurnDirection.Clockwise,
                            m_Sut.TurnDirection);
        }
    }
}