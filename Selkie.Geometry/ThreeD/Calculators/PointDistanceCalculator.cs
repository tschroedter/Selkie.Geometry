using System;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;
using Selkie.Geometry.ThreeD.Shapes;
using Selkie.Windsor;

namespace Selkie.Geometry.ThreeD.Calculators
{
    [ProjectComponent(Lifestyle.Transient)]
    public class PointDistanceCalculator
        : IPointDistanceCalculator
    {
        public PointDistanceCalculator()
        {
            FromPoint = Point.Unknown;
            ToPoint = Point.Unknown;
            Distance = double.NaN;
        }

        public Point FromPoint { get; set; }

        public Point ToPoint { get; set; }

        public double Distance { get; private set; }

        public void Calculate()
        {
            double deltaX = ToPoint.X - FromPoint.X;
            double x = deltaX * deltaX;

            double deltaY = ToPoint.Y - FromPoint.Y;
            double y = deltaY * deltaY;

            double deltaZ = ToPoint.Z - FromPoint.Z;
            double z = deltaZ * deltaZ;

            Distance = Math.Sqrt(x + y + z);
        }
    }
}