using JetBrains.Annotations;
using Selkie.Windsor.Extensions;

namespace Selkie.Geometry.Surveying
{
    public class SurveyGeoJsonFeature : ISurveyGeoJsonFeature
    {
        public static SurveyGeoJsonFeature Unknown = new SurveyGeoJsonFeature();

        private SurveyGeoJsonFeature()
        {
            SurveyFeature = Surveying.SurveyFeature.Unknown;
            SurveyFeatureAsGeoJson = string.Empty;
        }

        public SurveyGeoJsonFeature(
            [NotNull] ISurveyFeature surveyFeature,
            [NotNull] string surveyFeatureAsGeoJson)
        {
            SurveyFeature = surveyFeature;
            SurveyFeatureAsGeoJson = surveyFeatureAsGeoJson;
        }

        public int Id
        {
            get
            {
                return SurveyFeature.Id;
            }
        }

        public bool IsUnknown
        {
            get
            {
                return SurveyFeature.IsUnknown;
            }
        }

        public ISurveyFeature SurveyFeature { get; private set; }
        public string SurveyFeatureAsGeoJson { get; private set; }

        public override string ToString()
        {
            string surveryFeature = SurveyFeature.ToString();

            string geoJson = "[GeoJson:{0}]".Inject(SurveyFeatureAsGeoJson);

            return "{0} {1}".Inject(surveryFeature,
                                    geoJson);
        }
    }
}