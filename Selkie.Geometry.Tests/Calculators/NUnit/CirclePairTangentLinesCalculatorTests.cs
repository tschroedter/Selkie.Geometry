using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Calculators;
using Selkie.Geometry.Shapes;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.Calculators.NUnit
{
    // ReSharper disable once ClassTooBig
    [ExcludeFromCodeCoverage]
    internal sealed class CirclePairTangentLinesCalculatorTests
    {
        [TestFixture]
        internal sealed class CirclePairGeneralTests
        {
            [SetUp]
            public void Setup()
            {
                m_Calculator = new CirclePairTangentLinesCalculator(Substitute.For <IOuterTangentsCalculator>(),
                                                                    Substitute.For <IInnerTangentsCalculator>(),
                                                                    Substitute
                                                                        .For <ICirclesIntersectionPointsCalculator>());
            }

            private CirclePairTangentLinesCalculator m_Calculator;

            [Test]
            public void CirclePairDefaultTest()
            {
                Assert.True(m_Calculator.CirclePair.IsUnknown);
            }

            [Test]
            public void CirclePairRoundtripTest()
            {
                var pair = Substitute.For <ICirclePair>();

                m_Calculator.CirclePair = pair;

                Assert.AreEqual(pair,
                                m_Calculator.CirclePair);
            }
        }

        [TestFixture]
        internal sealed class CirclePairTangentLinesCalculatorCaseOneTests
        {
            [SetUp]
            // ReSharper disable once MethodTooLong
            public void Setup()
            {
                m_OuterCircleZeroTangentPointOne = new Point(1.0,
                                                             2.0);
                m_OuterCircleZeroTangentPointTwo = new Point(3.0,
                                                             4.0);
                m_OuterCircleOneTangentPointOne = new Point(5.0,
                                                            6.0);
                m_OuterCircleOneTangentPointTwo = new Point(7.0,
                                                            8.0);

                m_InnerCircleZeroTangentPointOne = new Point(9.0,
                                                             10.0);
                m_InnerCircleZeroTangentPointTwo = new Point(11.0,
                                                             12.0);
                m_InnerCircleOneTangentPointOne = new Point(13.0,
                                                            14.0);
                m_InnerCircleOneTangentPointTwo = new Point(15.0,
                                                            16.0);

                m_OuterTangentsCalculator = Substitute.For <IOuterTangentsCalculator>();
                m_OuterTangentsCalculator.CircleZeroTangentPointOne.Returns(m_OuterCircleZeroTangentPointOne);
                m_OuterTangentsCalculator.CircleZeroTangentPointTwo.Returns(m_OuterCircleZeroTangentPointTwo);
                m_OuterTangentsCalculator.CircleOneTangentPointOne.Returns(m_OuterCircleOneTangentPointOne);
                m_OuterTangentsCalculator.CircleOneTangentPointTwo.Returns(m_OuterCircleOneTangentPointTwo);

                m_InnerTangentsCalculator = Substitute.For <IInnerTangentsCalculator>();
                m_InnerTangentsCalculator.CircleZeroTangentPointOne.Returns(m_InnerCircleZeroTangentPointOne);
                m_InnerTangentsCalculator.CircleZeroTangentPointTwo.Returns(m_InnerCircleZeroTangentPointTwo);
                m_InnerTangentsCalculator.CircleOneTangentPointOne.Returns(m_InnerCircleOneTangentPointOne);
                m_InnerTangentsCalculator.CircleOneTangentPointTwo.Returns(m_InnerCircleOneTangentPointTwo);

                m_IntersectionPointOne = new Point(17.01,
                                                   18.01);
                m_IntersectionPointTwo = new Point(17.02,
                                                   18.02);

                m_CirclesIntersectionPointsCalculator = Substitute.For <ICirclesIntersectionPointsCalculator>();
                m_CirclesIntersectionPointsCalculator.IntersectionPointOne.Returns(m_IntersectionPointOne);
                m_CirclesIntersectionPointsCalculator.IntersectionPointTwo.Returns(m_IntersectionPointTwo);

                m_CirclePair = Substitute.For <ICirclePair>();
                m_CirclePair.NumberOfTangents.Returns(0);

                m_LinesCalculator = new CirclePairTangentLinesCalculator(m_OuterTangentsCalculator,
                                                                         m_InnerTangentsCalculator,
                                                                         m_CirclesIntersectionPointsCalculator)
                                    {
                                        CirclePair = m_CirclePair
                                    };

                m_LinesCalculator.Calculate();
            }

            private CirclePairTangentLinesCalculator m_LinesCalculator;
            private ICirclePair m_CirclePair;
            private IOuterTangentsCalculator m_OuterTangentsCalculator;
            private IInnerTangentsCalculator m_InnerTangentsCalculator;
            private Point m_OuterCircleZeroTangentPointOne;
            private Point m_OuterCircleZeroTangentPointTwo;
            private Point m_OuterCircleOneTangentPointOne;
            private Point m_OuterCircleOneTangentPointTwo;
            private Point m_InnerCircleZeroTangentPointOne;
            private Point m_InnerCircleZeroTangentPointTwo;
            private Point m_InnerCircleOneTangentPointOne;
            private Point m_InnerCircleOneTangentPointTwo;
            private ICirclesIntersectionPointsCalculator m_CirclesIntersectionPointsCalculator;
            private Point m_IntersectionPointOne;
            private Point m_IntersectionPointTwo;

            [Test]
            public void CreateFourTangentsTest()
            {
                var line1 = new Line(m_OuterCircleZeroTangentPointOne,
                                     m_OuterCircleOneTangentPointOne);
                var line2 = new Line(m_OuterCircleZeroTangentPointTwo,
                                     m_OuterCircleOneTangentPointTwo);

                var line3 = new Line(m_InnerCircleZeroTangentPointOne,
                                     m_InnerCircleOneTangentPointOne);
                var line4 = new Line(m_InnerCircleZeroTangentPointTwo,
                                     m_InnerCircleOneTangentPointTwo);

                m_LinesCalculator.CreateFourTangents();

                var expectedOuter = new List <ILine>
                                    {
                                        line1,
                                        line2
                                    };
                var expectedInner = new List <ILine>
                                    {
                                        line3,
                                        line4
                                    };
                var expectedAll = new List <ILine>
                                  {
                                      line1,
                                      line2,
                                      line3,
                                      line4
                                  };

                NUnitHelper.AssertSequenceEqual(expectedOuter,
                                                m_LinesCalculator.OuterTangents,
                                                "OuterTangents");
                NUnitHelper.AssertSequenceEqual(expectedInner,
                                                m_LinesCalculator.InnerTangents,
                                                "InnerTangents");
                NUnitHelper.AssertSequenceEqual(expectedAll,
                                                m_LinesCalculator.Tangents,
                                                "Tangents");
            }

            [Test]
            public void CreateInnerTangentsTest()
            {
                var line1 = new Line(m_InnerCircleZeroTangentPointOne,
                                     m_InnerCircleOneTangentPointOne);
                var line2 = new Line(m_InnerCircleZeroTangentPointTwo,
                                     m_InnerCircleOneTangentPointTwo);

                var expected = new List <ILine>
                               {
                                   line1,
                                   line2
                               };
                IEnumerable <ILine> actual = m_LinesCalculator.CreateInnerTangents();

                NUnitHelper.AssertSequenceEqual(expected,
                                                actual,
                                                "OuterTangents");
            }

            [Test]
            public void CreateOneTangentTest()
            {
                var line = new Line(m_IntersectionPointOne,
                                    m_IntersectionPointTwo);

                m_LinesCalculator.CreateOneTangent();

                var expectedInner = new List <ILine>
                                    {
                                        line
                                    };

                NUnitHelper.AssertSequenceEqual(expectedInner,
                                                m_LinesCalculator.InnerTangents,
                                                "InnerTangents");
                NUnitHelper.AssertSequenceEqual(expectedInner,
                                                m_LinesCalculator.Tangents,
                                                "Tangents");
                Assert.AreEqual(0,
                                m_LinesCalculator.OuterTangents.Count(),
                                "InnerTangents");
            }

            [Test]
            public void CreateOuterTangentsTest()
            {
                var line1 = new Line(m_OuterCircleZeroTangentPointOne,
                                     m_OuterCircleOneTangentPointOne);
                var line2 = new Line(m_OuterCircleZeroTangentPointTwo,
                                     m_OuterCircleOneTangentPointTwo);

                var expected = new List <ILine>
                               {
                                   line1,
                                   line2
                               };
                IEnumerable <ILine> actual = m_LinesCalculator.CreateOuterTangents();

                NUnitHelper.AssertSequenceEqual(expected,
                                                actual,
                                                "OuterTangents");
            }

            [Test]
            public void CreateThreeTangentTest()
            {
                var line1 = new Line(m_OuterCircleZeroTangentPointOne,
                                     m_OuterCircleOneTangentPointOne);
                var line2 = new Line(m_OuterCircleZeroTangentPointTwo,
                                     m_OuterCircleOneTangentPointTwo);
                var line3 = new Line(m_IntersectionPointOne,
                                     m_IntersectionPointTwo);

                m_LinesCalculator.CreateThreeTangents();

                var expectedOuter = new List <ILine>
                                    {
                                        line1,
                                        line2
                                    };
                var expectedInner = new List <ILine>
                                    {
                                        line3
                                    };
                var expectedTangents = new List <ILine>
                                       {
                                           line3,
                                           line1,
                                           line2
                                       };

                NUnitHelper.AssertSequenceEqual(expectedInner,
                                                m_LinesCalculator.InnerTangents,
                                                "InnerTangents");
                NUnitHelper.AssertSequenceEqual(expectedOuter,
                                                m_LinesCalculator.OuterTangents,
                                                "OuterTangents");
                NUnitHelper.AssertSequenceEqual(expectedTangents,
                                                m_LinesCalculator.Tangents,
                                                "Tangents");
            }

            [Test]
            public void CreateTwoTangentsTest()
            {
                var line1 = new Line(m_OuterCircleZeroTangentPointOne,
                                     m_OuterCircleOneTangentPointOne);
                var line2 = new Line(m_OuterCircleZeroTangentPointTwo,
                                     m_OuterCircleOneTangentPointTwo);

                m_LinesCalculator.CreateTwoTangents();

                var expectedOuter = new List <ILine>
                                    {
                                        line1,
                                        line2
                                    };

                NUnitHelper.AssertSequenceEqual(expectedOuter,
                                                m_LinesCalculator.OuterTangents,
                                                "OuterTangents");
                NUnitHelper.AssertSequenceEqual(expectedOuter,
                                                m_LinesCalculator.Tangents,
                                                "Tangents");
                Assert.AreEqual(0,
                                m_LinesCalculator.InnerTangents.Count(),
                                "InnerTangents");
            }

            [Test]
            public void DefaultInnerTangentsTest()
            {
                Assert.AreEqual(0,
                                m_LinesCalculator.InnerTangents.Count());
            }

            [Test]
            public void DefaultOuterTangentsTest()
            {
                Assert.AreEqual(0,
                                m_LinesCalculator.OuterTangents.Count());
            }

            [Test]
            public void DefaultTangentsTest()
            {
                Assert.AreEqual(0,
                                m_LinesCalculator.Tangents.Count());
            }

            [Test]
            public void NumberOfTangentsFourTest()
            {
                m_CirclePair.NumberOfTangents.Returns(4);

                var lines = new CirclePairTangentLinesCalculator(m_OuterTangentsCalculator,
                                                                 m_InnerTangentsCalculator,
                                                                 m_CirclesIntersectionPointsCalculator)
                            {
                                CirclePair = m_CirclePair
                            };

                lines.Calculate();

                Assert.AreEqual(2,
                                lines.InnerTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(2,
                                lines.OuterTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(4,
                                lines.Tangents.Count(),
                                "Tangents");
            }

            [Test]
            public void NumberOfTangentsOneTest()
            {
                m_CirclePair.NumberOfTangents.Returns(1);

                var lines = new CirclePairTangentLinesCalculator(m_OuterTangentsCalculator,
                                                                 m_InnerTangentsCalculator,
                                                                 m_CirclesIntersectionPointsCalculator)
                            {
                                CirclePair = m_CirclePair
                            };

                lines.Calculate();

                Assert.AreEqual(1,
                                lines.InnerTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(0,
                                lines.OuterTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(1,
                                lines.Tangents.Count(),
                                "Tangents");
            }

            [Test]
            public void NumberOfTangentsThreeTest()
            {
                m_CirclePair.NumberOfTangents.Returns(3);

                var lines = new CirclePairTangentLinesCalculator(m_OuterTangentsCalculator,
                                                                 m_InnerTangentsCalculator,
                                                                 m_CirclesIntersectionPointsCalculator)
                            {
                                CirclePair = m_CirclePair
                            };

                lines.Calculate();

                Assert.AreEqual(1,
                                lines.InnerTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(2,
                                lines.OuterTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(3,
                                lines.Tangents.Count(),
                                "Tangents");
            }

            [Test]
            public void NumberOfTangentsTwoTest()
            {
                m_CirclePair.NumberOfTangents.Returns(2);

                var lines = new CirclePairTangentLinesCalculator(m_OuterTangentsCalculator,
                                                                 m_InnerTangentsCalculator,
                                                                 m_CirclesIntersectionPointsCalculator)
                            {
                                CirclePair = m_CirclePair
                            };

                lines.Calculate();

                Assert.AreEqual(0,
                                lines.InnerTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(2,
                                lines.OuterTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(2,
                                lines.Tangents.Count(),
                                "Tangents");
            }

            [Test]
            public void NumberOfTangentsZeroTest()
            {
                m_CirclePair.NumberOfTangents.Returns(0);

                var lines = new CirclePairTangentLinesCalculator(Substitute.For <IOuterTangentsCalculator>(),
                                                                 Substitute.For <IInnerTangentsCalculator>(),
                                                                 Substitute.For <ICirclesIntersectionPointsCalculator>())
                            {
                                CirclePair = m_CirclePair
                            };

                lines.Calculate();

                Assert.AreEqual(0,
                                lines.InnerTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(0,
                                lines.OuterTangents.Count(),
                                "InnerTangents");
                Assert.AreEqual(0,
                                lines.Tangents.Count(),
                                "Tangents");
            }
        }

        [TestFixture]
        internal sealed class CirclePairTangentLinesCalculatorCaseTwoTests
        {
            [SetUp]
            public void Setup()
            {
                m_One = new Circle(new Point(10.0,
                                             14.0),
                                   2.5);
                m_Two = new Circle(new Point(11.0,
                                             2.5),
                                   2.5);
                m_Pair = new CirclePair(m_One,
                                        m_Two);

                m_Calculator = new CirclePairTangentLinesCalculator(m_Pair);

                m_Calculator.Calculate();
            }

            private CirclePairTangentLinesCalculator m_Calculator;
            private Circle m_One;
            private CirclePair m_Pair;
            private Circle m_Two;

            [Test]
            public void DefaultInnerTangentsTest()
            {
                Assert.AreEqual(2,
                                m_Calculator.InnerTangents.Count());
            }

            [Test]
            public void DefaultOuterTangentsTest()
            {
                Assert.AreEqual(2,
                                m_Calculator.OuterTangents.Count());
            }

            [Test]
            public void DefaultTangentsTest()
            {
                Assert.AreEqual(4,
                                m_Calculator.Tangents.Count());
            }

            [Test]
            public void InnerTangentLineOneTest()
            {
                var expected = new Line(new Point(13.15,
                                                  3.77),
                                        new Point(7.85,
                                                  12.73));

                ILine actual = m_Calculator.InnerTangents.First();

                Assert.AreEqual(expected.StartPoint,
                                actual.StartPoint,
                                "StartPoint");
                Assert.AreEqual(expected.EndPoint,
                                actual.EndPoint,
                                "EndPoint");
            }

            [Test]
            public void InnerTangentLineTwoTest()
            {
                var expected = new Line(new Point(8.66,
                                                  3.38),
                                        new Point(12.34,
                                                  13.12));

                ILine actual = m_Calculator.InnerTangents.Last();

                Assert.AreEqual(expected.StartPoint,
                                actual.StartPoint,
                                "StartPoint");
                Assert.AreEqual(expected.EndPoint,
                                actual.EndPoint,
                                "EndPoint");
            }

            [Test]
            public void OuterTangentLineOneTest()
            {
                var expected = new Line(new Point(13.49,
                                                  2.72),
                                        new Point(12.49,
                                                  14.22));

                ILine actual = m_Calculator.OuterTangents.First();

                Assert.AreEqual(expected.StartPoint,
                                actual.StartPoint,
                                "StartPoint");
                Assert.AreEqual(expected.EndPoint,
                                actual.EndPoint,
                                "EndPoint");
            }

            [Test]
            public void OuterTangentLineTwoTest()
            {
                var expected = new Line(new Point(8.51,
                                                  2.28),
                                        new Point(7.51,
                                                  13.78));

                ILine actual = m_Calculator.OuterTangents.Last();

                Assert.AreEqual(expected.StartPoint,
                                actual.StartPoint,
                                "StartPoint");
                Assert.AreEqual(expected.EndPoint,
                                actual.EndPoint,
                                "EndPoint");
            }
        }
    }
}