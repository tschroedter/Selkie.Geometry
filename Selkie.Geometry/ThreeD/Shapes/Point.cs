using System.Diagnostics;
using JetBrains.Annotations;
using Selkie.Geometry.ThreeD.Calculators;
using Selkie.Geometry.ThreeD.Converters;
using Selkie.Geometry.ThreeD.Interfaces.Calculators;
using Selkie.Geometry.ThreeD.Interfaces.Converters;
using Selkie.Geometry.ThreeD.Primitives;

namespace Selkie.Geometry.ThreeD.Shapes
{
    [DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}")]
    public class Point
    {
        private Point()
        {
            var data = new CartesianCoordinates
                       {
                           X = double.NaN,
                           Y = double.NaN,
                           Z = double.NaN,
                           IsUnknown = true
                       };

            Coordinates = data;
        }

        public Point(double x,
                     double y,
                     double z)
        {
            var data = new CartesianCoordinates
                       {
                           X = x,
                           Y = y,
                           Z = z,
                           IsUnknown = false
                       };

            Coordinates = data;
        }

        public static readonly Point Unknown = new Point();

        private CartesianCoordinates Coordinates { get; set; }

        public double Z
        {
            get
            {
                return Coordinates.Z;
            }
        }

        public double Y
        {
            get
            {
                return Coordinates.Y;
            }
        }

        public double X
        {
            get
            {
                return Coordinates.X;
            }
        }

        public bool IsUnknown
        {
            get
            {
                return Coordinates.IsUnknown;
            }
        }

        private readonly IPointDistanceCalculator m_DistanceCalculator = new PointDistanceCalculator();
        // todo move into constructor

        private readonly ICartesianCoordinatesToStringConverter m_ToStringConverter =
            new CartesianCoordinatesToStringConverter();

        // todo move into constructor

        public double DistanceTo([NotNull] Point other)
        {
            m_DistanceCalculator.FromPoint = this;
            m_DistanceCalculator.ToPoint = other;
            m_DistanceCalculator.Calculate();

            return m_DistanceCalculator.Distance;
        }

        public override string ToString()
        {
            m_ToStringConverter.Coordinates = Coordinates;
            m_ToStringConverter.Convert();

            return m_ToStringConverter.String;
        }

        // todo add missing methods to Point
    }
}