using System.Diagnostics.CodeAnalysis;

namespace Selkie.Geometry
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class Constants
    {
        #region CircleOrigin enum

        public enum CircleOrigin
        {
            Start,
            Finish,
            Unknown
        }

        #endregion

        #region CircleSide enum

        public enum CircleSide
        {
            Port,
            Starboard,
            Unknown
        }

        #endregion

        #region LineDirection enum

        public enum LineDirection
        {
            Forward,
            Reverse,
            Unknown
        }

        #endregion

        #region Origin enum

        public enum Origin
        {
            Start,
            Finish,
            Unknown
        }

        #endregion

        #region Side enum

        public enum Side
        {
            Port,
            Starboard,
            Unknown
        }

        #endregion

        #region TurnDirection enum

        public enum TurnDirection
        {
            Clockwise,
            Counterclockwise,
            Unknown
        }

        #endregion

        public const double EpsilonRadians = 1E-10;
        public const double EpsilonDegrees = 1E-10;
        public const double EpsilonPointXy = 1E-2;
        public const double EpsilonDistance = 1E-2;
        public const double Epsilon = 0.01;
    }

    //ncrunch: no coverage end
}