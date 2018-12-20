/*
 * Copyright (C) 2018, Vistatec or third-party contributors as indicated
 * by the @author tags or express copyright attribution statements applied by
 * the authors. All third-party contributions are distributed under license by
 * Vistatec.
 *
 * This file is part of JliffGraphTools.
 *
 * JliffGraphTools is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * JliffGraphTools is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program. If not, write to:
 *
 *     Free Software Foundation, Inc.
 *     51 Franklin Street, Fifth Floor
 *     Boston, MA 02110-1301
 *     USA
 *
 * Also, see the full LGPL text here: <http://www.gnu.org/copyleft/lesser.html>
 */


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