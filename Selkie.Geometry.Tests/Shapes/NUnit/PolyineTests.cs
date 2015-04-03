using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Tests.Shapes.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class PolyineTests
    {
        private Point m_EndPointOne;
        private Point m_EndPointTwo;
        private Polyline m_Polyline;
        private IPolylineSegment m_Segment1;
        private IPolylineSegment m_Segment2;
        private Point m_StartPointOne;
        private Point m_StartPointTwo;

        [SetUp]
        public void Setup()
        {
            m_StartPointOne = new Point(-10.0,
                                        -10.0);
            m_EndPointOne = new Point(10.0,
                                      10.0);
            m_StartPointTwo = new Point(-50.0,
                                        -50.0);
            m_EndPointTwo = new Point(100.0,
                                      100.0);

            m_Segment1 = Substitute.For <IPolylineSegment>();
            m_Segment1.StartPoint.Returns(m_StartPointOne);
            m_Segment1.EndPoint.Returns(m_EndPointOne);
            m_Segment1.Length.Returns(1.0);
            m_Segment2 = Substitute.For <IPolylineSegment>();
            m_Segment2.Length.Returns(2.0);
            m_Segment2.StartPoint.Returns(m_StartPointTwo);
            m_Segment2.EndPoint.Returns(m_EndPointTwo);

            m_Polyline = new Polyline();
        }

        [Test]
        public void AddTest()
        {
            m_Polyline.AddSegment(m_Segment1);

            Assert.AreEqual(1,
                            m_Polyline.Segments.Count(),
                            "Count");
            Assert.AreEqual(m_Segment1,
                            m_Polyline.Segments.First(),
                            "First");
            Assert.AreEqual(1.0,
                            m_Polyline.Length,
                            "Length");
            Assert.AreEqual(m_StartPointOne,
                            m_Polyline.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_EndPointOne,
                            m_Polyline.EndPoint,
                            "EndtPoint");
        }

        [Test]
        public void AddTwiceTest()
        {
            m_Polyline.AddSegment(m_Segment1);
            m_Polyline.AddSegment(m_Segment2);

            Assert.AreEqual(2,
                            m_Polyline.Segments.Count(),
                            "Count");
            Assert.AreEqual(m_Segment1,
                            m_Polyline.Segments.First(),
                            "First");
            Assert.AreEqual(m_Segment2,
                            m_Polyline.Segments.Last(),
                            "Last");
            Assert.AreEqual(3.0,
                            m_Polyline.Length,
                            "Length");
            Assert.AreEqual(m_StartPointOne,
                            m_Polyline.StartPoint,
                            "StartPoint");
            Assert.AreEqual(m_EndPointTwo,
                            m_Polyline.EndPoint,
                            "EndtPoint");
        }

        [Test]
        public void DefaultEndPointTest()
        {
            Assert.AreEqual(Point.Unknown,
                            m_Polyline.EndPoint);
        }

        [Test]
        public void DefaultSegmentsTest()
        {
            Assert.AreEqual(0,
                            m_Polyline.Segments.Count());
        }

        [Test]
        public void DefaultStartPointTest()
        {
            Assert.AreEqual(Point.Unknown,
                            m_Polyline.StartPoint);
        }

        [Test]
        public void DetermineEndPointReturnsPointUnknownForLastIsNullTest()
        {
            Point actual = m_Polyline.DetermineEndPoint(new List <IPolylineSegment>());

            Assert.AreEqual(Point.Unknown,
                            actual);
        }

        [Test]
        public void DetermineStartPointReturnsPointUnknownForLastIsNullTest()
        {
            Point actual = m_Polyline.DetermineStartPoint(new List <IPolylineSegment>());

            Assert.AreEqual(Point.Unknown,
                            actual);
        }

        [Test]
        public void LengthTest()
        {
            m_Polyline.AddSegment(m_Segment1);
            m_Polyline.AddSegment(m_Segment2);

            Assert.AreEqual(3.0,
                            m_Polyline.Length);
        }

        [Test]
        public void ReverseCallsReverseOnSegmentsTest()
        {
            m_Polyline.AddSegment(m_Segment1);
            m_Polyline.AddSegment(m_Segment2);

            m_Polyline.Reverse();

            m_Segment1.Received()
                      .Reverse();
            m_Segment2.Received()
                      .Reverse();
        }
    }
}