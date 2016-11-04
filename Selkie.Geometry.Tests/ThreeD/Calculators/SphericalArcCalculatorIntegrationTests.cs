using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Tests.ThreeD.Primitives;
using Selkie.Geometry.ThreeD.Calculators;
using Selkie.Geometry.ThreeD.Converters;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.Tests.ThreeD.Calculators
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SphericalArcCalculatorIntegrationTests
    {
        [SetUp]
        public void Setup()
        {
            var angleIntervallCalculator = new AngleIntervallCalculator();
            var angelInterpolationCalculator = new AngelInterpolationCalculator(angleIntervallCalculator);
            var sphericalCoordinatesIntervallCalculator =
                new SphericalCoordinatesIntervallCalculator(angelInterpolationCalculator);
            var radiusPhiThetaToSphericalCoordinatesConverter = new RadiusPhiThetaToSphericalCoordinatesConverter();

            m_Sut = new SphericalArcCalculator(sphericalCoordinatesIntervallCalculator,
                                               radiusPhiThetaToSphericalCoordinatesConverter);
        }

        private SphericalArcCalculator m_Sut;

        private IEnumerable <SphericalCoordinates> CreateArrayOfThreeSphericalCoordinatesForFixedPhiClockwise()
        {
            var one = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.ForZeroDegrees,
                          Theta = Angle.ForZeroDegrees
                      };
            var two = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.ForZeroDegrees,
                          Theta = Angle.For45Degrees
                      };
            var three = new SphericalCoordinates
                        {
                            Radius = 1.0,
                            Phi = Angle.ForZeroDegrees,
                            Theta = Angle.For90Degrees
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <SphericalCoordinates> CreateArrayOfThreeSphericalCoordinatesForFixedThetaClockwise()
        {
            var one = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.ForZeroDegrees,
                          Theta = Angle.ForZeroDegrees
                      };
            var two = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.For45Degrees,
                          Theta = Angle.ForZeroDegrees
                      };
            var three = new SphericalCoordinates
                        {
                            Radius = 1.0,
                            Phi = Angle.For90Degrees,
                            Theta = Angle.ForZeroDegrees
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <SphericalCoordinates> CreateArrayOfThreeSphericalCoordinatesForFixedPhiCounterClockwise()
        {
            var one = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.ForZeroDegrees,
                          Theta = Angle.ForZeroDegrees
                      };
            var two = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.ForZeroDegrees,
                          Theta = Angle.For225Degrees
                      };
            var three = new SphericalCoordinates
                        {
                            Radius = 1.0,
                            Phi = Angle.ForZeroDegrees,
                            Theta = Angle.For90Degrees
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <SphericalCoordinates> CreateArrayOfThreeSphericalCoordinatesForFixedThetaCounterClockwise()
        {
            var one = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.ForZeroDegrees,
                          Theta = Angle.ForZeroDegrees
                      };
            var two = new SphericalCoordinates
                      {
                          Radius = 1.0,
                          Phi = Angle.For225Degrees,
                          Theta = Angle.ForZeroDegrees
                      };
            var three = new SphericalCoordinates
                        {
                            Radius = 1.0,
                            Phi = Angle.For90Degrees,
                            Theta = Angle.ForZeroDegrees
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        [Test]
        public void Calculate_SetsSphericalCoordinates_FixedPhiClockwise()
        {
            // Arrange
            IEnumerable <SphericalCoordinates> expected = CreateArrayOfThreeSphericalCoordinatesForFixedPhiClockwise();

            var coordinatesZeroZero = new SphericalCoordinates
                                      {
                                          Radius = 1.0,
                                          Phi = Angle.ForZeroDegrees,
                                          Theta = Angle.ForZeroDegrees
                                      };

            var coordinatesZeroNintey = new SphericalCoordinates
                                        {
                                            Radius = 1.0,
                                            Phi = Angle.ForZeroDegrees,
                                            Theta = Angle.For90Degrees
                                        };

            m_Sut.FromCoordinates = coordinatesZeroZero;
            m_Sut.ToCoordinates = coordinatesZeroNintey;
            m_Sut.TurnDirection = Constants.TurnDirection.Clockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            SphericalCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.SphericalCoordinates);
        }

        [Test]
        public void Calculate_SetsSphericalCoordinates_FixedPhiCounterClockwise()
        {
            // Arrange
            IEnumerable <SphericalCoordinates> expected =
                CreateArrayOfThreeSphericalCoordinatesForFixedPhiCounterClockwise();

            var coordinatesZeroZero = new SphericalCoordinates
                                      {
                                          Radius = 1.0,
                                          Phi = Angle.ForZeroDegrees,
                                          Theta = Angle.ForZeroDegrees
                                      };

            var coordinatesZeroNintey = new SphericalCoordinates
                                        {
                                            Radius = 1.0,
                                            Phi = Angle.ForZeroDegrees,
                                            Theta = Angle.For90Degrees
                                        };

            m_Sut.FromCoordinates = coordinatesZeroZero;
            m_Sut.ToCoordinates = coordinatesZeroNintey;
            m_Sut.TurnDirection = Constants.TurnDirection.Counterclockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            SphericalCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.SphericalCoordinates);
        }

        [Test]
        public void Calculate_SetsSphericalCoordinates_FixedThetaClockwise()
        {
            // Arrange
            IEnumerable <SphericalCoordinates> expected = CreateArrayOfThreeSphericalCoordinatesForFixedThetaClockwise();

            var coordinatesZeroZero = new SphericalCoordinates
                                      {
                                          Radius = 1.0,
                                          Phi = Angle.ForZeroDegrees,
                                          Theta = Angle.ForZeroDegrees
                                      };

            var coordinatesZeroNintey = new SphericalCoordinates
                                        {
                                            Radius = 1.0,
                                            Phi = Angle.For90Degrees,
                                            Theta = Angle.ForZeroDegrees
                                        };

            m_Sut.FromCoordinates = coordinatesZeroZero;
            m_Sut.ToCoordinates = coordinatesZeroNintey;
            m_Sut.TurnDirection = Constants.TurnDirection.Clockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            SphericalCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.SphericalCoordinates);
        }

        [Test]
        public void Calculate_SetsSphericalCoordinates_FixedThetaCounterClockwise()
        {
            // Arrange
            IEnumerable <SphericalCoordinates> expected =
                CreateArrayOfThreeSphericalCoordinatesForFixedThetaCounterClockwise();

            var coordinatesZeroZero = new SphericalCoordinates
                                      {
                                          Radius = 1.0,
                                          Phi = Angle.ForZeroDegrees,
                                          Theta = Angle.ForZeroDegrees
                                      };

            var coordinatesZeroNintey = new SphericalCoordinates
                                        {
                                            Radius = 1.0,
                                            Phi = Angle.For90Degrees,
                                            Theta = Angle.ForZeroDegrees
                                        };

            m_Sut.FromCoordinates = coordinatesZeroZero;
            m_Sut.ToCoordinates = coordinatesZeroNintey;
            m_Sut.TurnDirection = Constants.TurnDirection.Counterclockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            SphericalCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.SphericalCoordinates);
        }
    }
}