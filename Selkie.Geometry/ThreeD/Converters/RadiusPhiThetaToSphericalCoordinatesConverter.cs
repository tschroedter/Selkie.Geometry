using System.Collections.Generic;
using System.Linq;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.ThreeD.Interfaces.Converters;
using Selkie.Geometry.ThreeD.Primitives;
using Selkie.Windsor;

namespace Selkie.Geometry.ThreeD.Converters
{
    [ProjectComponent(Lifestyle.Transient)]
    public class RadiusPhiThetaToSphericalCoordinatesConverter
        : IRadiusPhiThetaToSphericalCoordinatesConverter
    {
        public RadiusPhiThetaToSphericalCoordinatesConverter()
        {
            PhiAngles = new Angle[0];
            ThetaAngles = new Angle[0];
            SphericalCoordinates = new SphericalCoordinates[0];
        }

        public double Radius { get; set; }
        public IEnumerable <Angle> PhiAngles { get; set; }
        public IEnumerable <Angle> ThetaAngles { get; set; }
        public IEnumerable <SphericalCoordinates> SphericalCoordinates { get; private set; }

        public void Convert()
        {
            SphericalCoordinates = ConvertToSphericalCoordinates();
        }

        private IEnumerable <SphericalCoordinates> ConvertToSphericalCoordinates()
        {
            var list = new List <SphericalCoordinates>();

            Angle[] phiArray = PhiAngles as Angle[] ?? PhiAngles.ToArray();
            Angle[] thetaArray = ThetaAngles as Angle[] ?? ThetaAngles.ToArray();

            for ( var i = 0 ; i < phiArray.Length ; i++ )
            {
                var coordinates = new SphericalCoordinates
                                  {
                                      Radius = Radius,
                                      Phi = phiArray [ i ],
                                      Theta = thetaArray [ i ]
                                  };

                list.Add(coordinates);
            }

            return list;
        }
    }
}