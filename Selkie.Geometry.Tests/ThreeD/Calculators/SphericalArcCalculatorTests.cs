using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Calculators;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;
using Selkie.Geometry.ThreeD.Interfaces.Converters;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.Tests.ThreeD.Calculators
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SphericalArcCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            m_CoordinatesZeroZero = new SphericalCoordinates
                                    {
                                        Radius = 1.0,
                                        Phi = Angle.ForZeroDegrees,
                                        Theta = Angle.ForZeroDegrees
                                    };

            m_CoordinatesZeroNintey = new SphericalCoordinates
                                      {
                                          Radius = 1.0,
                                          Phi = Angle.ForZeroDegrees,
                                          Theta = Angle.For90Degrees
                                      };

            m_Calculator = Substitute.For <ISphericalCoordinatesIntervallCalculator>();
            m_Converter = Substitute.For <IRadiusPhiThetaToSphericalCoordinatesConverter>();

            m_Sut = new SphericalArcCalculator(m_Calculator,
                                               m_Converter);
        }

        private SphericalCoordinates m_CoordinatesZeroZero;
        private SphericalCoordinates m_CoordinatesZeroNintey;
        private ISphericalCoordinatesIntervallCalculator m_Calculator;
        private IRadiusPhiThetaToSphericalCoordinatesConverter m_Converter;
        private SphericalArcCalculator m_Sut;

        [Test]
        public void Calculate_CallsCalculate_WhenCalled()
        {
            // Arrange
            m_Sut.FromCoordinates = m_CoordinatesZeroZero;
            m_Sut.ToCoordinates = m_CoordinatesZeroNintey;

            // Act
            m_Sut.Calculate();

            // Assert
            m_Calculator.Received().Calculate();
        }

        [Test]
        public void Calculate_CallsConvert_WhenCalled()
        {
            // Arrange
            m_Sut.FromCoordinates = m_CoordinatesZeroZero;
            m_Sut.ToCoordinates = m_CoordinatesZeroNintey;

            // Act
            m_Sut.Calculate();

            // Assert
            m_Converter.Received().Convert();
        }

        [Test]
        public void Calculate_DoesNotThrowsException_ForRadiusInTolerance()
        {
            // Arrange
            var from = new SphericalCoordinates
                       {
                           Radius = 1.0
                       };
            var to = new SphericalCoordinates
                     {
                         Radius = 1.0 + SphericalArcCalculator.Tolerance
                     };

            m_Sut.FromCoordinates = from;
            m_Sut.ToCoordinates = to;

            // Act
            // Assert
            Assert.DoesNotThrow(() => m_Sut.Calculate());
        }

        [Test]
        public void Calculate_SetsFromCoordinatesInCalculator_WhenCalled()
        {
            // Arrange
            m_Sut.FromCoordinates = m_CoordinatesZeroZero;
            m_Sut.ToCoordinates = m_CoordinatesZeroNintey;

            // Act
            m_Sut.Calculate();

            // Assert
            Assert.AreEqual(m_Sut.FromCoordinates,
                            m_Calculator.FromCoordinates);
        }

        [Test]
        public void Calculate_SetsPhiAnglesForConverter_WhenCalled()
        {
            // Arrange
            m_Sut.FromCoordinates = m_CoordinatesZeroZero;
            m_Sut.ToCoordinates = m_CoordinatesZeroNintey;

            m_Calculator.PhiAngles.Returns(new Angle[0]);

            // Act
            m_Sut.Calculate();

            // Assert
            Assert.AreEqual(m_Calculator.PhiAngles,
                            m_Converter.PhiAngles);
        }

        [Test]
        public void Calculate_SetsRadiusForConverter_WhenCalled()
        {
            // Arrange
            m_Sut.FromCoordinates = m_CoordinatesZeroZero;
            m_Sut.ToCoordinates = m_CoordinatesZeroNintey;

            m_Calculator.Radius.Returns(1.0);

            // Act
            m_Sut.Calculate();

            // Assert
            Assert.AreEqual(m_Calculator.Radius,
                            m_Converter.Radius);
        }

        [Test]
        public void Calculate_SetsSphericalCoordinates_WhenCalled()
        {
            // Arrange
            m_Sut.FromCoordinates = m_CoordinatesZeroZero;
            m_Sut.ToCoordinates = m_CoordinatesZeroNintey;

            m_Converter.SphericalCoordinates.Returns(new SphericalCoordinates[0]);

            // Act
            m_Sut.Calculate();

            // Assert
            Assert.AreEqual(m_Converter.SphericalCoordinates,
                            m_Sut.SphericalCoordinates);
        }

        [Test]
        public void Calculate_SetsStepInCalculator_WhenCalled()
        {
            // Arrange
            var from = new SphericalCoordinates
                       {
                           Radius = 1.0
                       };
            var to = new SphericalCoordinates
                     {
                         Radius = 1.0 + SphericalArcCalculator.Tolerance
                     };

            m_Sut.FromCoordinates = from;
            m_Sut.ToCoordinates = to;

            // Act
            // Assert
            Assert.DoesNotThrow(() => m_Sut.Calculate());
        }

        [Test]
        public void Calculate_SetsStepsInCalculator_WhenCalled()
        {
            // Arrange
            m_Sut.Steps = 10;

            // Act
            m_Sut.Calculate();

            // Assert
            Assert.AreEqual(m_Sut.Steps,
                            m_Calculator.Steps);
        }

        [Test]
        public void Calculate_SetsThetaAnglesForConverter_WhenCalled()
        {
            // Arrange
            m_Sut.FromCoordinates = m_CoordinatesZeroZero;
            m_Sut.ToCoordinates = m_CoordinatesZeroNintey;

            m_Calculator.ThetaAngles.Returns(new Angle[0]);

            // Act
            m_Sut.Calculate();

            // Assert
            Assert.AreEqual(m_Calculator.ThetaAngles,
                            m_Converter.ThetaAngles);
        }

        [Test]
        public void Calculate_SetsToCoordinatesInCalculator_WhenCalled()
        {
            // Arrange
            m_Sut.FromCoordinates = m_CoordinatesZeroZero;
            m_Sut.ToCoordinates = m_CoordinatesZeroNintey;

            // Act
            m_Sut.Calculate();

            // Assert
            Assert.AreEqual(m_Sut.ToCoordinates,
                            m_Calculator.ToCoordinates);
        }

        [Test]
        public void Calculate_ThrowsException_ForDifferentRadius()
        {
            // Arrange
            var from = new SphericalCoordinates
                       {
                           Radius = 1.0
                       };
            var to = new SphericalCoordinates
                     {
                         Radius = 1.0 + SphericalArcCalculator.Tolerance * 2
                     };

            m_Sut.FromCoordinates = from;
            m_Sut.ToCoordinates = to;

            // Act
            // Assert
            Assert.Throws <ArgumentException>(() => m_Sut.Calculate());
        }

        [Test]
        public void Constructor_SetsDefaultArcPoints_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(m_Sut.SphericalCoordinates);
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