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
        private readonly ICircle m_Circle;
        private readonly Constants.CircleSide m_Side;
        private readonly Distance m_Radius;
        private readonly Constants.CircleOrigin m_Origin;
        private readonly Constants.TurnDirection m_TurnDirection;

        private TurnCircle()
        {
            m_Circle = Shapes.Circle.Unknown;
            m_Side = Constants.CircleSide.Unknown;
            m_Origin = Constants.CircleOrigin.Unknown;
            m_TurnDirection = Constants.TurnDirection.Unknown;
            m_Radius = Distance.Unknown;
            IsUnknown = true;
        }

        public TurnCircle([NotNull] ICircle circle,
                          Constants.CircleSide side,
                          Constants.CircleOrigin origin,
                          Constants.TurnDirection turnDirection)
        {
            m_Circle = circle;
            m_Side = side;
            m_Origin = origin;
            m_TurnDirection = turnDirection;
            m_Radius = new Distance(Circle.Radius);
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

        public ICircle Circle
        {
            get
            {
                return m_Circle;
            }
        }

        public Point CentrePoint
        {
            get
            {
                return Circle.CentrePoint;
            }
        }

        public Distance Radius
        {
            get
            {
                return m_Radius;
            }
        }

        public Constants.CircleSide Side
        {
            get
            {
                return m_Side;
            }
        }

        public Constants.CircleOrigin Origin
        {
            get
            {
                return m_Origin;
            }
        }

        public bool IsPointOnCircle(Point point)
        {
            return Circle.IsPointOnCircle(point);
        }

        public Constants.TurnDirection TurnDirection
        {
            get
            {
                return m_TurnDirection;
            }
        }

        #endregion
    }
}