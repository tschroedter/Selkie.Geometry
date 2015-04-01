using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Tests.Calculators.NUnit
{
    [ExcludeFromCodeCoverage]
    internal class InnerTangentLinesIntersectionPointCalculatorTests
    {
        [TestFixture]
        internal sealed class RadiusOfOneIsBiggerAndNumberOfTangentsIsFourTests
        {
            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(3.0, -5.0), 4.0);
                m_Two = new Circle(new Point(-2.0, 2.0), 1.0);
                m_Pair = new CirclePair(m_One, m_Two);

                m_Calculator = new InnerTangentLinesIntersectionPointCalculator(m_Pair);
            }

            private CirclePair m_Pair;
            private Circle m_One;
            private Circle m_Two;
            private InnerTangentLinesIntersectionPointCalculator m_Calculator;

            [Test]
            public void InnerTangentLinesIntersectionPointTest()
            {
                var expected = new Point(-1.0, 0.6);
                Point actual = m_Calculator.IntersectionPoint;

                Assert.AreEqual(actual, expected, "IntersectionPoint");
            }

            [Test]
            public void IsUnknownTest()
            {
                Assert.False(m_Calculator.IsUnknown);
            }

            [Test]
            public void UnknownTest()
            {
                InnerTangentLinesIntersectionPointCalculator calculator = InnerTangentLinesIntersectionPointCalculator.Unknown;

                Assert.True(calculator.IsUnknown, "IsUnknown");
                Assert.AreEqual(Point.Unknown, calculator.IntersectionPoint, "IntersectionPoint");
            }
        }
    }
}