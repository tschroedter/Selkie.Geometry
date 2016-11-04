using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Tests.ThreeD.Primitives;
using Selkie.Geometry.ThreeD.Calculators;
using Selkie.Geometry.ThreeD.Converters;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.Tests.ThreeD.Calculators
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class CartesianSphericalArcCalculatorIntegrationTests
    {
        [SetUp]
        public void Setup()
        {
            var angleIntervallCalculator = new AngleIntervallCalculator();
            var angelInterpolationCalculator = new AngelInterpolationCalculator(angleIntervallCalculator);
            var sphericalCoordinatesIntervallCalculator =
                new SphericalCoordinatesIntervallCalculator(angelInterpolationCalculator);
            var radiusPhiThetaToSphericalCoordinatesConverter = new RadiusPhiThetaToSphericalCoordinatesConverter();

            var sphericalArcCalculator = new SphericalArcCalculator(sphericalCoordinatesIntervallCalculator,
                                                                    radiusPhiThetaToSphericalCoordinatesConverter);

            var cartesianToSphericalCalculator = new CartesianToSphericalCalculator();
            var sphericalToCartesianCalculator = new SphericalToCartesianCalculator();

            m_Sut = new CartesianSphericalArcCalculator(cartesianToSphericalCalculator,
                                                        sphericalToCartesianCalculator,
                                                        sphericalArcCalculator);
        }

        private CartesianSphericalArcCalculator m_Sut;

        private IEnumerable <CartesianCoordinates> CreateCoordinatesCaseOneClockwise()
        {
            var one = new CartesianCoordinates
                      {
                          X = 0.0,
                          Y = 0.0,
                          Z = 1.0
                      };
            var two = new CartesianCoordinates
                      {
                          X = 0.707106781186547,
                          Y = 0.0,
                          Z = 0.707106781186547
                      };
            var three = new CartesianCoordinates
                        {
                            X = 1.0,
                            Y = 0.0,
                            Z = 0.0
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <CartesianCoordinates> CreateCoordinatesCaseOneCounterClockwise()
        {
            var one = new CartesianCoordinates
                      {
                          X = 0.0,
                          Y = 0.0,
                          Z = 1.0
                      };
            var two = new CartesianCoordinates
                      {
                          X = -0.707106781186547,
                          Y = 0.0,
                          Z = -0.707106781186547
                      };
            var three = new CartesianCoordinates
                        {
                            X = 1.0,
                            Y = 0.0,
                            Z = 0.0
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <CartesianCoordinates> CreateCoordinatesCaseTwoClockwise()
        {
            var one = new CartesianCoordinates
                      {
                          X = +0.0,
                          Y = +0.0,
                          Z = -1.0
                      };
            var two = new CartesianCoordinates
                      {
                          X = -1.0,
                          Y = +0.0,
                          Z = +0.0
                      };
            var three = new CartesianCoordinates
                        {
                            X = +0.0,
                            Y = +0.0,
                            Z = +1.0
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <CartesianCoordinates> CreateCoordinatesCaseTwoCounterClockwise()
        {
            var one = new CartesianCoordinates
                      {
                          X = +0.0,
                          Y = +0.0,
                          Z = -1.0
                      };
            var two = new CartesianCoordinates
                      {
                          X = +1.0,
                          Y = +0.0,
                          Z = +0.0
                      };
            var three = new CartesianCoordinates
                        {
                            X = +0.0,
                            Y = +0.0,
                            Z = +1.0
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <CartesianCoordinates> CreateCoordinatesCaseThreeClockwise()
        {
            var one = new CartesianCoordinates
                      {
                          X = 1.0,
                          Y = 1.0,
                          Z = 1.0
                      };
            var two = new CartesianCoordinates
                      {
                          X = -1.22474487139159,
                          Y = +1.22474487139159,
                          Z = +0.0
                      };
            var three = new CartesianCoordinates
                        {
                            X = -1.0,
                            Y = -1.0,
                            Z = -1.0
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <CartesianCoordinates> CreateCoordinatesCaseThreeCounterClockwise()
        {
            var one = new CartesianCoordinates
                      {
                          X = 1.0,
                          Y = 1.0,
                          Z = 1.0
                      };
            var two = new CartesianCoordinates
                      {
                          X = -1.22474487139159,
                          Y = +1.22474487139159,
                          Z = +0.0
                      };
            var three = new CartesianCoordinates
                        {
                            X = -1.0,
                            Y = -1.0,
                            Z = -1.0
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <CartesianCoordinates> CreateCoordinatesCaseFourClockwise()
        {
            var circle = new Circle(0.0,
                                    0.0,
                                    1.0);
            Point from = circle.PointOnCircle(Angle.For315Degrees);
            Point to = circle.PointOnCircle(Angle.For45Degrees);
            Point middle = circle.PointOnCircle(Angle.ForZeroDegrees);

            var one = new CartesianCoordinates
                      {
                          X = from.X,
                          Y = from.Y,
                          Z = 0.0
                      };
            var two = new CartesianCoordinates
                      {
                          X = middle.X,
                          Y = middle.Y,
                          Z = 0.0
                      };
            var three = new CartesianCoordinates
                        {
                            X = to.X,
                            Y = to.Y,
                            Z = 0.0
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        private IEnumerable <CartesianCoordinates> CreateCoordinatesCaseFourCounterClockwise()
        {
            var circle = new Circle(0.0,
                                    0.0,
                                    1.0);
            Point from = circle.PointOnCircle(Angle.For315Degrees);
            Point to = circle.PointOnCircle(Angle.For45Degrees);
            Point middle = circle.PointOnCircle(Angle.For180Degrees);

            var one = new CartesianCoordinates
                      {
                          X = from.X,
                          Y = from.Y,
                          Z = 0.0
                      };
            var two = new CartesianCoordinates
                      {
                          X = middle.X,
                          Y = middle.Y,
                          Z = 0.0
                      };
            var three = new CartesianCoordinates
                        {
                            X = to.X,
                            Y = to.Y,
                            Z = 0.0
                        };

            return new[]
                   {
                       one,
                       two,
                       three
                   };
        }

        [Test]
        public void Calculate_SetsCartesianCoordinates_CaseFourCounterClockwise()
        {
            // Arrange
            IEnumerable <CartesianCoordinates> expected = CreateCoordinatesCaseFourClockwise();

            var circle = new Circle(0.0,
                                    0.0,
                                    1.0);
            Point from = circle.PointOnCircle(Angle.For315Degrees);
            Point to = circle.PointOnCircle(Angle.For45Degrees);

            var coordinatesZeroZeroOne = new CartesianCoordinates
                                         {
                                             X = from.X,
                                             Y = from.Y,
                                             Z = 0.0
                                         };

            var coordinatesOneZeroZero = new CartesianCoordinates
                                         {
                                             X = to.X,
                                             Y = to.Y,
                                             Z = 0.0
                                         };

            m_Sut.FromCoordinates = coordinatesZeroZeroOne;
            m_Sut.ToCoordinates = coordinatesOneZeroZero;
            m_Sut.TurnDirection = Constants.TurnDirection.Clockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            CartesianCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.CartesianCoordinates);
        }

        [Test]
        public void Calculate_SetsCartesianCoordinates_CaseFourCounterCounterClockwise()
        {
            // Arrange
            IEnumerable <CartesianCoordinates> expected = CreateCoordinatesCaseFourCounterClockwise();

            var circle = new Circle(0.0,
                                    0.0,
                                    1.0);
            Point from = circle.PointOnCircle(Angle.For315Degrees);
            Point to = circle.PointOnCircle(Angle.For45Degrees);

            var coordinatesZeroZeroOne = new CartesianCoordinates
                                         {
                                             X = from.X,
                                             Y = from.Y,
                                             Z = 0.0
                                         };

            var coordinatesOneZeroZero = new CartesianCoordinates
                                         {
                                             X = to.X,
                                             Y = to.Y,
                                             Z = 0.0
                                         };

            m_Sut.FromCoordinates = coordinatesZeroZeroOne;
            m_Sut.ToCoordinates = coordinatesOneZeroZero;
            m_Sut.TurnDirection = Constants.TurnDirection.Counterclockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            CartesianCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.CartesianCoordinates);
        }

        [Test]
        public void Calculate_SetsCartesianCoordinates_CaseOneClockwise()
        {
            // Arrange
            IEnumerable <CartesianCoordinates> expected = CreateCoordinatesCaseOneClockwise();

            var coordinatesZeroZeroOne = new CartesianCoordinates
                                         {
                                             X = 0.0,
                                             Y = 0.0,
                                             Z = 1.0
                                         };

            var coordinatesOneZeroZero = new CartesianCoordinates
                                         {
                                             X = 1.0,
                                             Y = 0.0,
                                             Z = 0.0
                                         };

            m_Sut.FromCoordinates = coordinatesZeroZeroOne;
            m_Sut.ToCoordinates = coordinatesOneZeroZero;
            m_Sut.TurnDirection = Constants.TurnDirection.Clockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            CartesianCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.CartesianCoordinates);
        }

        [Test]
        public void Calculate_SetsCartesianCoordinates_CaseOneCounterClockwise()
        {
            // Arrange
            IEnumerable <CartesianCoordinates> expected = CreateCoordinatesCaseOneCounterClockwise();

            var coordinatesZeroZeroOne = new CartesianCoordinates
                                         {
                                             X = 0.0,
                                             Y = 0.0,
                                             Z = 1.0
                                         };

            var coordinatesOneZeroZero = new CartesianCoordinates
                                         {
                                             X = 1.0,
                                             Y = 0.0,
                                             Z = 0.0
                                         };

            m_Sut.FromCoordinates = coordinatesZeroZeroOne;
            m_Sut.ToCoordinates = coordinatesOneZeroZero;
            m_Sut.TurnDirection = Constants.TurnDirection.Counterclockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            CartesianCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.CartesianCoordinates);
        }

        [Test]
        public void Calculate_SetsCartesianCoordinates_CaseThreeClockwise()
        {
            // Arrange
            IEnumerable <CartesianCoordinates> expected = CreateCoordinatesCaseThreeClockwise();

            var coordinatesZeroZeroOne = new CartesianCoordinates
                                         {
                                             X = 1.0,
                                             Y = 1.0,
                                             Z = 1.0
                                         };

            var coordinatesOneZeroZero = new CartesianCoordinates
                                         {
                                             X = -1.0,
                                             Y = -1.0,
                                             Z = -1.0
                                         };

            m_Sut.FromCoordinates = coordinatesZeroZeroOne;
            m_Sut.ToCoordinates = coordinatesOneZeroZero;
            m_Sut.TurnDirection = Constants.TurnDirection.Clockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            CartesianCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.CartesianCoordinates);
        }

        [Test]
        public void Calculate_SetsCartesianCoordinates_CaseThreeCounterClockwise()
        {
            // Arrange
            IEnumerable <CartesianCoordinates> expected = CreateCoordinatesCaseThreeCounterClockwise();

            var coordinatesZeroZeroOne = new CartesianCoordinates
                                         {
                                             X = 1.0,
                                             Y = 1.0,
                                             Z = 1.0
                                         };

            var coordinatesOneZeroZero = new CartesianCoordinates
                                         {
                                             X = -1.0,
                                             Y = -1.0,
                                             Z = -1.0
                                         };

            m_Sut.FromCoordinates = coordinatesZeroZeroOne;
            m_Sut.ToCoordinates = coordinatesOneZeroZero;
            m_Sut.TurnDirection = Constants.TurnDirection.Counterclockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            CartesianCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.CartesianCoordinates);
        }

        [Test]
        public void Calculate_SetsCartesianCoordinates_CaseTwoClockwise()
        {
            // Arrange
            IEnumerable <CartesianCoordinates> expected = CreateCoordinatesCaseTwoClockwise();

            var coordinatesZeroZeroOne = new CartesianCoordinates
                                         {
                                             X = 0.0,
                                             Y = 0.0,
                                             Z = -1.0
                                         };

            var coordinatesOneZeroZero = new CartesianCoordinates
                                         {
                                             X = 0.0,
                                             Y = 0.0,
                                             Z = 1.0
                                         };

            m_Sut.FromCoordinates = coordinatesZeroZeroOne;
            m_Sut.ToCoordinates = coordinatesOneZeroZero;
            m_Sut.TurnDirection = Constants.TurnDirection.Clockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            CartesianCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.CartesianCoordinates);
        }

        [Test]
        public void Calculate_SetsCartesianCoordinates_CaseTwoCounterClockwise()
        {
            // Arrange
            IEnumerable <CartesianCoordinates> expected = CreateCoordinatesCaseTwoCounterClockwise();

            var coordinatesZeroZeroOne = new CartesianCoordinates
                                         {
                                             X = 0.0,
                                             Y = 0.0,
                                             Z = -1.0
                                         };

            var coordinatesOneZeroZero = new CartesianCoordinates
                                         {
                                             X = 0.0,
                                             Y = 0.0,
                                             Z = 1.0
                                         };

            m_Sut.FromCoordinates = coordinatesZeroZeroOne;
            m_Sut.ToCoordinates = coordinatesOneZeroZero;
            m_Sut.TurnDirection = Constants.TurnDirection.Counterclockwise;
            m_Sut.Steps = 3;

            // Act
            m_Sut.Calculate();

            // Assert
            CartesianCoordinatesHelper.AssertSphericalCoordinates(expected,
                                                                  m_Sut.CartesianCoordinates);
        }
    }
}