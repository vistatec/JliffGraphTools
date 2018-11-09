using Newtonsoft.Json;

namespace Jliff.Graph.Modules.ITS
{
    public class AnnotatorsRef
    {
        public string Translate { get; set; }
        [JsonProperty("localization-note")]
        public string LocalizationNote { get; set; }
        public string Terminology { get; set; }
        public string Directionality { get; set; }
        [JsonProperty("language-information")]
        public string LanguageInformation { get; set; }
        [JsonProperty("elements-within-text")]
        public string ElementsWithinText { get; set; }
        public string Domain { get; set; }
        [JsonProperty("text-analysis")]
        public string TextAnalysis { get; set; }
        [JsonProperty("locale-filter")]
        public string LocaleFilter { get; set; }
        public string Provenance { get; set; }
        [JsonProperty("external-resource")]
        public string ExternalResource { get; set; }
        [JsonProperty("target-pointer")]
        public string TargetPointer { get; set; }
        [JsonProperty("id-value")]
        public string IdValue { get; set; }
        [JsonProperty("preserve-space")]
        public string PreserveSpace { get; set; }
        [JsonProperty("localization-quality-issue")]
        public string LocalizationQualityIssue { get; set; }
        [JsonProperty("localization-quality-rating")]
        public string LocalizationQualityRating { get; set; }
        [JsonProperty("mt-confidence")]
        public string MtConfidence { get; set; }
        [JsonProperty("allowed-characters")]
        public string AllowedCharacters { get; set; }
        [JsonProperty("storage-size")]
        public string StorageSize { get; set; }
    }
}