﻿/*
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


using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Modules.Metadata;
using Newtonsoft.Json;

namespace Jliff.Graph.Modules.Matches
{
    public class Match
    {
        public string Domains { get; set; }
        public string Id { get; set; }
        [JsonProperty("its_locQualityRatingProfileRef")]
        public string LocQualityRatingProfileRef { get; set; }
        [JsonProperty("its_locQualityRatingScore")]
        public float LocQualityRatingScore { get; set; }
        [JsonProperty("its_locQualityRatingScoreThreshold")]
        public float LocQualityRatingScoreThreshold { get; set; }
        [JsonProperty("its_locQualityRatingVote")]
        public int LocQualityRatingScoreVote { get; set; }
        [JsonProperty("its_locQualityRatingVoteThreshold")]
        public int LocQualityRatingScoreVoteThreshold { get; set; }
        public int MatchQuality { get; set; }
        public int MatchSuitability { get; set; }
        public Metadata Metadata { get; set; }
        public string Origin { get; set; }
        public string OriginalData { get; set; }
        public string Ref { get; set; }
        public Enumerations.YesNo Reference { get; set; }
        public int Similarity { get; set; }
        public IElement Source { get; set; }
        public string SubType { get; set; }
        public IElement Target { get; set; }
        public Enumerations.YesNo Translate { get; set; }
        public Enumerations.MatchType Type { get; set; }
    }
}