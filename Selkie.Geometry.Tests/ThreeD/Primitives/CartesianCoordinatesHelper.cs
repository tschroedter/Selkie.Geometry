using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Selkie.Geometry.ThreeD.Converters;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.NUnit.Extensions;

namespace Selkie.Geometry.Tests.ThreeD.Primitives
{
    public class CartesianCoordinatesHelper
    {
        private const double Tolerance = 1E-6d;

        public static void AssertCartesianCoordinates(
            CartesianCoordinates expected,
            CartesianCoordinates actual)
        {
            Console.WriteLine("Comparing CartesianCoordinates");
            Console.WriteLine("Expected: {0}",
                              CoordinatesToString(expected));
            Console.WriteLine("Actual  : {0}",
                              CoordinatesToString(actual));

            NUnitHelper.AssertIsEquivalent(expected.X,
                                           actual.X,
                                           Tolerance,
                                           "X");
            NUnitHelper.AssertIsEquivalent(expected.Y,
                                           actual.Y,
                                           Tolerance,
                                           "Y");
            NUnitHelper.AssertIsEquivalent(expected.Z,
                                           actual.Z,
                                           Tolerance,
                                           "Z");
        }

        public static void AssertSphericalCoordinates(
            IEnumerable <CartesianCoordinates> expected,
            IEnumerable <CartesianCoordinates> actual)
        {
            CartesianCoordinates[] expectedArray = expected as CartesianCoordinates[] ?? expected.ToArray();
            CartesianCoordinates[] actualArray = actual as CartesianCoordinates[] ?? actual.ToArray();

            Assert.AreEqual(expectedArray.Length,
                            actualArray.Length,
                            "Count");

            for ( var i = 0 ; i < expectedArray.Length ; i++ )
            {
                AssertCartesianCoordinates(expectedArray [ i ],
                                           actualArray [ i ]);
            }
        }

        public static string CoordinatesToString(
            CartesianCoordinates coordinates)
        {
            var converter = new CartesianCoordinatesToStringConverter
                            {
                                Coordinates = coordinates
                            };

            converter.Convert();

            return converter.String;
        }
    }
}