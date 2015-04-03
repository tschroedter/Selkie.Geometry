using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Shapes
{
    public interface ILine : IPolylineSegment,
                             IEquatable <ILine>,
                             IComparable <ILine>
    {
        double X1 { get; }
        double Y1 { get; }
        double X2 { get; }
        double Y2 { get; }
        bool IsUnknown { get; }

        [NotNull]
        Angle AngleToXAxis { get; }

        Constants.LineDirection RunDirection { get; }
        int Id { get; }
        Constants.TurnDirection TurnDirection([NotNull] Point point);
        bool IsOnLine([NotNull] Point point);
    }
}