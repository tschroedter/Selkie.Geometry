using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Shapes.Calculators;

namespace Selkie.Geometry.Tests.Shapes.Calculators.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class CircleCentrePointToPointCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            m_CentrePoint = new Point(3.0, 4.0);
            m_StartPoint = new Point(3.0, 6.0);
            m_EndPoint = new Point(5.0, 4.0);

            m_Calculator = new CircleCentrePointToPointCalculator(m_CentrePoint,
                                                                  m_StartPoint,
                                                                  m_EndPoint);
        }

        private Point m_CentrePoint;
        private Point m_StartPoint;
        private Point m_EndPoint;
        private CircleCentrePointToPointCalculator m_Calculator;

        [Test]
        public void CentrePointTest()
        {
            Assert.AreEqual(m_CentrePoint, m_Calculator.CentrePoint);
        }

        [Test]
        public void EndPointTest()
        {
            Assert.AreEqual(m_EndPoint, m_Calculator.EndPoint);
        }

        [Test]
        public void RadiansBetweenPointsCounterClockwiseForAngleTwoBiggerThanAngleOneTest()
        {
            var centrePoint = new Point(3.0, 4.0);
            var startPoint = new Point(1.0, 4.0);
            var endPoint = new Point(3.0, 6.0);


            Angle expected = Angle.For270Degrees;
            Angle actual = m_Calculator.RadiansBetweenPointsCounterClockwise(centrePoint,
                                                                             startPoint,
                                                                             endPoint);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RadiansClockwiseTest()
        {
            Angle expected = Angle.FromDegrees(90.0);
            Angle actual = m_Calculator.AngleClockwise;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RadiansCounterClockwiseTest()
        {
            Angle expected = Angle.FromDegrees(270.0);
            Angle actual = m_Calculator.RadiansCounterClockwise;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StartPointTest()
        {
            Assert.AreEqual(m_StartPoint, m_Calculator.StartPoint);
        }
    }
}