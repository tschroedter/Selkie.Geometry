using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Selkie.Geometry.ThreeD.Converters;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.ThreeD.Primitives
{
    public class SphericalCoordinatesHelper
    {
        public static void AssertSphericalCoordinates(
            IEnumerable <SphericalCoordinates> expected,
            IEnumerable <SphericalCoordinates> actual)
        {
            SphericalCoordinates[] expectedArray = expected as SphericalCoordinates[] ?? expected.ToArray();
            SphericalCoordinates[] actualArray = actual as SphericalCoordinates[] ?? actual.ToArray();

            Assert.AreEqual(expectedArray.Length,
                            actualArray.Length,
                            "Count");

            for ( var i = 0 ; i < expectedArray.Length ; i++ )
            {
                AssertSphericalCoordinates(expectedArray [ i ],
                                           actualArray [ i ]);
            }
        }

        public static void AssertSphericalCoordinates(
            SphericalCoordinates expected,
            SphericalCoordinates actual)
        {
            Console.WriteLine("Expected: {0}".Inject(ConvertToSTring(expected)));
            Console.WriteLine("Actual:   {0}".Inject(ConvertToSTring(expected)));

            NUnitHelper.AssertIsEquivalent(expected.Radius,
                                           actual.Radius,
                                           "Radius");
            Assert.AreEqual(expected.Phi,
                            actual.Phi,
                            "Phi");
            Assert.AreEqual(expected.Theta,
                            actual.Theta,
                            "Theta");
        }

        public static string ConvertToSTring(
            SphericalCoordinates coordinates)
        {
            var converter = new SphericalCoordinatesToStringConverter
                            {
                                Coordinates = coordinates
                            };

            converter.Convert();

            return converter.String;
        }
    }
}