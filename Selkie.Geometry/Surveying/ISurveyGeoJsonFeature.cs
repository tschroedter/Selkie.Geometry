using JetBrains.Annotations;

namespace Selkie.Geometry.Surveying
{
    public interface ISurveyGeoJsonFeature
    {
        [NotNull]
        ISurveyFeature SurveyFeature { get; }

        [NotNull]
        string SurveyFeatureAsGeoJson { get; }

        int Id { get; }
        bool IsUnknown { get; }
    }
}