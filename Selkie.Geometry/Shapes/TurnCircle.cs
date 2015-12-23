using System;
using JetBrains.Annotations;
using Selkie.Geometry.Primitives;
using Selkie.Windsor;

namespace Selkie.Geometry.Shapes
{
    [ProjectComponent(Lifestyle.Transient)]
    public class TurnCircle
        : ITurnCircle,
          IEquatable <TurnCircle>
    {
        public static TurnCircle Unknown = new TurnCircle();

        private TurnCircle()
        {
            Circle = Shapes.Circle.Unknown;
            Side = Constants.CircleSide.Unknown;
            Origin = Constants.CircleOrigin.Unknown;
            TurnDirection = Constants.TurnDirection.Unknown;
            Radius = Distance.Unknown;
            IsUnknown = true;
        }

        public TurnCircle([NotNull] ICircle circle,
                          Constants.CircleSide side,
                          Constants.CircleOrigin origin,
                          Constants.TurnDirection turnDirection)
        {
            Circle = circle;
            Side = side;
            Origin = origin;
            TurnDirection = turnDirection;
            Radius = new Distance(Circle.Radius);
        }

        #region IEquatable<TurnCircle> Members

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(TurnCircle other)
        {
            if ( ReferenceEquals(null,
                                 other) )
            {
                return false;
            }
            if ( ReferenceEquals(this,
                                 other) )
            {
                return true;
            }
            return Equals(other.Circle,
                          Circle) && Equals(other.Side,
                                              Side) && Equals(other.Origin,
                                                                Origin) && Equals(other.TurnDirection,
                                                                                    TurnDirection);
        }

        #endregion

        public bool IsUnknown { get; private set; }

        // ReSharper disable once CodeAnnotationAnalyzer
        public override bool Equals(object obj)
        {
            if ( ReferenceEquals(null,
                                 obj) )
            {
                return false;
            }
            if ( ReferenceEquals(this,
                                 obj) )
            {
                return true;
            }
            if ( obj.GetType() != typeof ( TurnCircle ) )
            {
                return false;
            }
            return Equals(( TurnCircle ) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Circle.GetHashCode();
                result = ( result * 397 ) ^ Side.GetHashCode();
                result = ( result * 397 ) ^ Origin.GetHashCode();
                result = ( result * 397 ) ^ TurnDirection.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(TurnCircle left,
                                       TurnCircle right)
        {
            return Equals(left,
                          right);
        }

        public static bool operator !=(TurnCircle left,
                                       TurnCircle right)
        {
            return !Equals(left,
                           right);
        }

        #region ITurnCircle Members

        public ICircle Circle { get; private set; }

        public Point CentrePoint
        {
            get
            {
                return Circle.CentrePoint;
            }
        }

        public Distance Radius { get; private set; }

        public Constants.CircleSide Side { get; private set; }

        public Constants.CircleOrigin Origin { get; private set; }

        public bool IsPointOnCircle(Point point)
        {
            return Circle.IsPointOnCircle(point);
        }

        public Constants.TurnDirection TurnDirection { get; private set; }

        #endregion
    }
}