﻿/*
 * Copyright (C) 2018-2019, Vistatec or third-party contributors as indicated
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

namespace Localization.Jliff.Graph.Modules.ITS
{
    public class ProvenanceRecord
    {
        [JsonProperty("its_org")]
        public string Org { get; set; }

        [JsonProperty("its_orgRef")]
        public string OrgRef { get; set; }

        [JsonProperty("its_person")]
        public string Person { get; set; }

        [JsonProperty("its_personRef")]
        public string PersonRef { get; set; }

        [JsonProperty("its_revOrg")]
        public string RevOrg { get; set; }

        [JsonProperty("its_revOrgRef")]
        public string RevOrgRef { get; set; }

        [JsonProperty("its_revPerson")]
        public string RevPerson { get; set; }

        [JsonProperty("its_revPersonRef")]
        public string RevPersonRef { get; set; }

        [JsonProperty("its_revTool")]
        public string RevTool { get; set; }

        [JsonProperty("its_revToolRef")]
        public string RevToolRef { get; set; }

        [JsonProperty("its_tool")]
        public string Tool { get; set; }

        [JsonProperty("its_toolRef")]
        public string ToolRef { get; set; }
    }
}