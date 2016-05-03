using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;

namespace Selkie.Geometry.Tests.Surveying.NUnit
{
    [TestFixture]
    internal sealed class SurveyFeatureTests
    {
        [TestFixture]
        [ExcludeFromCodeCoverage]
        internal sealed class SurveyFeatureForTests
        {
            [SetUp]
            public void Setup()
            {
                m_Id = 1;
                m_StartPoint = new Point(0.0,
                                         0.0);
                m_EndPoint = new Point(10.0,
                                       10.0);
                m_AngleToXAxisAtStartPoint = Angle.For45Degrees;
                m_AngleToXAxisAtEndPoint = Angle.For225Degrees;
                m_RunDirection = Constants.LineDirection.Forward;
                m_Length = 123.0;

                m_Sut = new SurveyFeature(m_Id,
                                          m_StartPoint,
                                          m_EndPoint,
                                          m_AngleToXAxisAtStartPoint,
                                          m_AngleToXAxisAtEndPoint,
                                          m_RunDirection,
                                          m_Length);
            }

            private SurveyFeature m_Sut;
            private Point m_StartPoint;
            private Point m_EndPoint;
            private Angle m_AngleToXAxisAtStartPoint;
            private Angle m_AngleToXAxisAtEndPoint;
            private int m_Id;
            private Constants.LineDirection m_RunDirection;
            private double m_Length;

            [Test]
            public void AngleToXAxisAtEndPoint_ReturnsAngle_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(m_AngleToXAxisAtEndPoint,
                                m_Sut.AngleToXAxisAtEndPoint);
            }

            [Test]
            public void AngleToXAxisAtStartPoint_ReturnsAngle_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(m_AngleToXAxisAtStartPoint,
                                m_Sut.AngleToXAxisAtStartPoint);
            }

            [Test]
            public void EndPoint_ReturnsPoint_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(m_EndPoint,
                                m_Sut.EndPoint);
            }

            [Test]
            public void Id_ReturnsId_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(m_Id,
                                m_Sut.Id);
            }

            [Test]
            public void IsUnknown_ReturnsFalse_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.False(m_Sut.IsUnknown);
            }

            [Test]
            public void IsUnknown_ReturnsIsUnknown_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.False(m_Sut.IsUnknown);
            }

            [Test]
            public void Length_ReturnsLength_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(m_Length,
                                m_Sut.Length);
            }

            [Test]
            public void Reverse_ReturnsFeature_WhenCalled()
            {
                // Arrange
                // Act
                ISurveyFeature actual = m_Sut.Reverse();

                // Assert
                Assert.AreEqual(actual.Id,
                                m_Sut.Id,
                                "Id");
                Assert.AreEqual(m_StartPoint,
                                actual.EndPoint,
                                "EndPoint");
                Assert.AreEqual(m_EndPoint,
                                actual.StartPoint,
                                "StartPoint");
                Assert.AreEqual(Angle.For225Degrees,
                                actual.AngleToXAxisAtStartPoint,
                                "AngleToXAxisAtStartPoint");
                Assert.AreEqual(Angle.For45Degrees,
                                actual.AngleToXAxisAtEndPoint,
                                "AngleToXAxisAtEndPoint");
                Assert.AreEqual(Constants.LineDirection.Forward,
                                actual.RunDirection,
                                "RunDirection");
                Assert.AreEqual(m_Length,
                                actual.Length,
                                "Length");
            }

            [Test]
            public void RunDirection_ReturnsRunDirection_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(m_RunDirection,
                                m_Sut.RunDirection);
            }

            [Test]
            public void StartPoint_ReturnsPoint_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(m_StartPoint,
                                m_Sut.StartPoint);
            }
        }

        [TestFixture]
        [ExcludeFromCodeCoverage]
        internal sealed class SurveyFeatureForUnknownTests
        {
            [SetUp]
            public void Setup()
            {
                m_Sut = SurveyFeature.Unknown;
            }

            private SurveyFeature m_Sut;

            [Test]
            public void AngleToXAxisAtEndPoint_ReturnsUnknown_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(Angle.Unknown,
                                m_Sut.AngleToXAxisAtEndPoint);
            }

            [Test]
            public void AngleToXAxisAtStartPoint_ReturnsUnknown_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(Angle.Unknown,
                                m_Sut.AngleToXAxisAtStartPoint);
            }

            [Test]
            public void EndPoint_ReturnsUnknown_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(Point.Unknown,
                                m_Sut.EndPoint);
            }

            [Test]
            public void Id_ReturnsUnknown_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(SurveyFeature.UnknownId,
                                m_Sut.Id);
            }

            [Test]
            public void IsUnknown_ReturnsTrue_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.True(m_Sut.IsUnknown);
            }

            [Test]
            public void Length_ReturnsUnknown_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(SurveyFeature.UnknownLength,
                                m_Sut.Length);
            }

            [Test]
            public void Reverse_ReturnsThis_WhenCalled()
            {
                // Arrange
                // Act
                ISurveyFeature actual = m_Sut.Reverse();

                // Assert
                Assert.AreEqual(actual,
                                m_Sut);
            }

            [Test]
            public void RunDirection_ReturnsUnknown_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(Constants.LineDirection.Unknown,
                                m_Sut.RunDirection);
            }

            [Test]
            public void StartPoint_ReturnsUnknown_WhenCalled()
            {
                // Arrange
                // Act
                // Assert
                Assert.AreEqual(Point.Unknown,
                                m_Sut.StartPoint);
            }
        }
    }
}