using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;

namespace Selkie.Geometry.Calculators
{
    public class CirclePairTangentLinesCalculator : ICirclePairTangentLinesCalculator
    {
        private readonly ICirclesIntersectionPointsCalculator m_CirclesIntersectionPointsCalculator;
        private readonly List<ILine> m_InnerTangents = new List<ILine>();
        private readonly IInnerTangentsCalculator m_InnerTangentsCalculator;
        private readonly List<ILine> m_OuterTangents = new List<ILine>();
        private readonly IOuterTangentsCalculator m_OuterTangentsCalculator;
        private readonly List<ILine> m_Tangents = new List<ILine>();
        private ICirclePair m_CirclePair = Shapes.CirclePair.Unknown;

        public CirclePairTangentLinesCalculator([NotNull] ICirclePair circlePair)
        {
            m_CirclePair = circlePair;

            m_OuterTangentsCalculator = new OuterTangentsCalculator(m_CirclePair);
            m_InnerTangentsCalculator = new InnerTangentsCalculator(m_CirclePair);
            m_CirclesIntersectionPointsCalculator = new CirclesIntersectionPointsCalculator(m_CirclePair);
        }

        public CirclePairTangentLinesCalculator([NotNull] IOuterTangentsCalculator outerTangentsCalculator,
                                                [NotNull] IInnerTangentsCalculator innerTangentsCalculator,
                                                [NotNull] ICirclesIntersectionPointsCalculator circlesIntersectionPointsCalculator)
        {
            m_OuterTangentsCalculator = outerTangentsCalculator;
            m_InnerTangentsCalculator = innerTangentsCalculator;
            m_CirclesIntersectionPointsCalculator = circlesIntersectionPointsCalculator;
        }

        public ICirclePair CirclePair
        {
            get { return m_CirclePair; }
            set { m_CirclePair = value; }
        }

        // ReSharper disable once MethodTooLong
        public void Calculate()
        {
            switch (m_CirclePair.NumberOfTangents)
            {
                case 0:
                    break;

                case 1:
                    CreateOneTangent();
                    break;

                case 2:
                    CreateTwoTangents();
                    break;

                case 3:
                    CreateThreeTangents();
                    break;

                default:
                    CreateFourTangents();
                    break;
            }
        }

        public IEnumerable<ILine> OuterTangents
        {
            get { return m_OuterTangents; }
        }

        public IEnumerable<ILine> InnerTangents
        {
            get { return m_InnerTangents; }
        }

        public IEnumerable<ILine> Tangents
        {
            get { return m_Tangents; }
        }

        internal void CreateOneTangent()
        {
            ILine innerTangent = CreateInnerTangent();

            m_InnerTangents.Add(innerTangent);

            m_Tangents.AddRange(m_InnerTangents);
        }

        [NotNull]
        private ILine CreateInnerTangent()
        {
            var innerTangent = new Line(m_CirclesIntersectionPointsCalculator.IntersectionPointOne,
                                        m_CirclesIntersectionPointsCalculator.IntersectionPointTwo);

            return innerTangent;
        }

        internal void CreateTwoTangents()
        {
            IEnumerable<ILine> outerTangents = CreateOuterTangents();

            m_OuterTangents.AddRange(outerTangents);

            m_Tangents.AddRange(m_OuterTangents);
        }

        [NotNull]
        internal IEnumerable<ILine> CreateOuterTangents()
        {
            var outerTangentOne = new Line(m_OuterTangentsCalculator.CircleZeroTangentPointOne,
                                           m_OuterTangentsCalculator.CircleOneTangentPointOne);
            var outerTangentTwo = new Line(m_OuterTangentsCalculator.CircleZeroTangentPointTwo,
                                           m_OuterTangentsCalculator.CircleOneTangentPointTwo);

            var tangents = new List<ILine>
                           {
                               outerTangentOne,
                               outerTangentTwo,
                           };

            return tangents;
        }

        internal void CreateThreeTangents()
        {
            CreateOneTangent();
            CreateTwoTangents();
        }

        [NotNull]
        internal IEnumerable<ILine> CreateInnerTangents()
        {
            var innerTangentOne = new Line(m_InnerTangentsCalculator.CircleZeroTangentPointOne,
                                           m_InnerTangentsCalculator.CircleOneTangentPointOne);
            var innerTangentTwo = new Line(m_InnerTangentsCalculator.CircleZeroTangentPointTwo,
                                           m_InnerTangentsCalculator.CircleOneTangentPointTwo);

            var tangents = new List<ILine>
                           {
                               innerTangentOne,
                               innerTangentTwo
                           };

            return tangents;
        }

        internal void CreateFourTangents()
        {
            IEnumerable<ILine> outerTangents = CreateOuterTangents();
            IEnumerable<ILine> innerTangents = CreateInnerTangents();

            m_OuterTangents.AddRange(outerTangents);
            m_InnerTangents.AddRange(innerTangents);

            m_Tangents.AddRange(m_OuterTangents);
            m_Tangents.AddRange(m_InnerTangents);
        }
    }
}