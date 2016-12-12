using System.Diagnostics;

namespace Selkie.Geometry.ThreeD.Primitives
{
    [DebuggerDisplay("Radius = {Radius}, AzimuthalAngle = {AzimuthalAngle}, PolarAngle = {PolarAngle}")]
    public struct SphericalCoordinates
    {
        public bool IsUnknown;
        public double Radius;
        public PolarAngle PolarAngle; // => phi [0° ≤ θ ≤ 180° (π rad)]
        public AzimuthAngle AzimuthalAngle; // => theta [0° ≤ φ < 360° (2π rad)]
    }
}