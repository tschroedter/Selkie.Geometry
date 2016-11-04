using System.Diagnostics;

namespace Selkie.Geometry.ThreeD.Primitives
{
    [DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}")]
    public struct CartesianCoordinates
    {
        public bool IsUnknown;
        public double Z;
        public double Y;
        public double X;
    }
}