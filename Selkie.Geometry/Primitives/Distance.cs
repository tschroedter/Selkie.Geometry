using System;
using Selkie.Windsor.Extensions;
using SelkieConstants = Selkie.Geometry.Constants;

namespace Selkie.Geometry.Primitives
{
    public class Distance : IEquatable <Distance>
    {
        private Distance()
        {
            IsUnknown = true;
        }

        public Distance(double length)
        {
            Length = length;
        }

        public static readonly Distance Unknown = new Distance();
        public static readonly Distance Zero = new Distance(0.0);

        public bool IsUnknown { get; }

        public double Length { get; }

        #region IEquatable<Distance> Members

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(Distance other)
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
            return Math.Abs(Length - other.Length) < SelkieConstants.EpsilonDistance;
        }

        #endregion

        public static Distance operator +(Distance one,
                                          Distance two)
        {
            double length = one.Length + two.Length;

            return new Distance(length);
        }

        public static bool operator ==(Distance left,
                                       Distance right)
        {
            return Equals(left,
                          right);
        }

        public static bool operator >(Distance one,
                                      Distance two)
        {
            return one.Length > two.Length;
        }

        public static bool operator >=(Distance one,
                                       Distance two)
        {
            return one.Length >= two.Length;
        }

        public static bool operator !=(Distance left,
                                       Distance right)
        {
            return !Equals(left,
                           right);
        }

        public static bool operator <(Distance one,
                                      Distance two)
        {
            return one.Length < two.Length;
        }

        public static bool operator <=(Distance one,
                                       Distance two)
        {
            return one.Length <= two.Length;
        }

        public static Distance operator -(Distance one,
                                          Distance two)
        {
            double length = one.Length - two.Length;

            return new Distance(length);
        }

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
            if ( obj.GetType() != typeof ( Distance ) )
            {
                return false;
            }
            return Equals(( Distance ) obj);
        }

        public override int GetHashCode()
        {
            return Length.GetHashCode();
        }

        public override string ToString()
        {
            return "Length: {0:F2}".Inject(Length);
        }
    }
}