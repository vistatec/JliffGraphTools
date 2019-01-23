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


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Localization.Jliff.Graph;

namespace Jliff.Graph.Conversion
{
    public class XlfConverter
    {
        public JliffDocument ConvertXlf20Files(Stream stream)
        {
            try
            {
                JliffBuilder builder = new JliffBuilder("en-US", "fr-FR");
                Xliff20Filter xliff20Filter = new Xliff20Filter();
                xliff20Filter.XlfRootEvent += builder.XlfRoot;
                xliff20Filter.XlfFileEvent += builder.File;
                xliff20Filter.XlfUnitEvent += builder.Unit;
                xliff20Filter.XlfGroupEvent += builder.Group;
                xliff20Filter.XlfSegmentEvent += builder.Segment;
                xliff20Filter.XlfSourceEvent += builder.Source;
                xliff20Filter.XlfTargetEvent += builder.Target;
                xliff20Filter.XlfIgnorableEvent += builder.Ignorable;
                xliff20Filter.XlfPhElementEvent += builder.PhElement;
                xliff20Filter.XlfSkeletonEvent += builder.Skeleton;
                xliff20Filter.XlfTextEvent += builder.Text;
                xliff20Filter.XlfSmElementEvent += builder.SmElement;
                xliff20Filter.XlfEmElementEvent += builder.EmElement;
                xliff20Filter.XlfScElementEvent += builder.ScElement;
                xliff20Filter.XlfEcElementEvent += builder.EcElement;
                xliff20Filter.XlfNote += builder.Note;
                xliff20Filter.XlfDataEvent += builder.Data;
                xliff20Filter.ModCtrChangeTrackEvent += builder.ChangeTrack;
                xliff20Filter.ModCtrRevisionsEvent += builder.Revisions;
                xliff20Filter.ModCtrRevisionEvent += builder.Revision;
                xliff20Filter.ModCtrRevisionItemEvent += builder.RevisionItem;
                xliff20Filter.ModGlsEntryEvent += builder.GlossaryEntry;
                xliff20Filter.ModGlsTermEvent += builder.Term;
                xliff20Filter.ModGlsTranslationEvent += builder.Translation;
                xliff20Filter.ModGlsDefinitionEvent += builder.Definition;
                xliff20Filter.ModItsLocQualityIssues += builder.LocQualityIssues;
                xliff20Filter.ModItsLocQualityIssue += builder.LocQualityIssue;
                xliff20Filter.ModMdaMetadataEvent += builder.Metadata;
                xliff20Filter.ModMdaMetaGroupEvent += builder.MetaGroup;
                xliff20Filter.ModMdaMetaitemEvent += builder.Metaitem;
                xliff20Filter.ModMtcMatchEvent += builder.Match;
                xliff20Filter.ModResResourceDataEvent += builder.ResourceData;
                xliff20Filter.ModResResourceItemEvent += builder.ResourceItem;
                xliff20Filter.ModResSourceEvent += builder.ResourceSource;
                xliff20Filter.Filter(new StreamReader(stream, Encoding.UTF8));
                return builder.Jliff;
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }
        }

        public JliffDocument ConvertXlf20Subunits(Stream stream)
        {
            try
            {
                JliffBuilder builder = new JliffBuilder("en-US", "fr-FR");
                Xliff20Filter xliff20Filter = new Xliff20Filter();
                xliff20Filter.XlfRootEvent += builder.XlfRoot;
                xliff20Filter.XlfFileEvent += builder.File;
                xliff20Filter.XlfUnitEvent += builder.Unit;
                xliff20Filter.XlfGroupEvent += builder.Group;
                xliff20Filter.XlfSegmentEvent += builder.Segment;
                xliff20Filter.XlfSourceEvent += builder.Source;
                xliff20Filter.XlfTargetEvent += builder.Target;
                xliff20Filter.XlfIgnorableEvent += builder.Ignorable;
                xliff20Filter.XlfPhElementEvent += builder.PhElement;
                xliff20Filter.XlfSkeletonEvent += builder.Skeleton;
                xliff20Filter.XlfTextEvent += builder.Text;
                xliff20Filter.XlfSmElementEvent += builder.SmElement;
                xliff20Filter.XlfEmElementEvent += builder.EmElement;
                xliff20Filter.XlfScElementEvent += builder.ScElement;
                xliff20Filter.XlfEcElementEvent += builder.EcElement;
                xliff20Filter.ModItsLocQualityIssue += builder.LocQualityIssue;
                xliff20Filter.Filter(new StreamReader(stream, Encoding.UTF8));
                JliffDocument jd = builder.Jliff;
                List<Segment> segments = jd.Segments;
                jd.Subunits.AddRange(segments);
                JliffDocument jd2 = new JliffDocument("en", "fr");
                jd2.Subunits.AddRange(segments);
                // This is a cludge to avoid deep copying
                ///TODO: Implement proper deep copying
                string tempSerialization = Converter.Serialize(jd2);
                JliffDocument jd3 = Converter.Deserialize(tempSerialization);
                return jd3;
                // https://stackoverflow.com/questions/5713556/copy-object-to-object-with-automapper
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }
        }
    }
}