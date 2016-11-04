using System.Diagnostics;
using Selkie.Geometry.Primitives;

namespace Selkie.Geometry.ThreeD.Primitives
{
    [DebuggerDisplay("Radius = {Radius}, Phi = {Phi}, Theta = {Theta}")]
    public struct SphericalCoordinates
    {
        public bool IsUnknown;
        public double Radius;
        public Angle Phi;
        public Angle Theta;
    }
}