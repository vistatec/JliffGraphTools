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


using System;
using System.IO;
using System.Xml;
using Jliff.Graph.Serialization;

namespace Localization.Jliff.Graph
{
    public class Xliff20Filter
    {
        public delegate void XlfEvent(XlfEventArgs args);

        private static XmlReader xmlReader;
        private static string currentLqiRef = string.Empty;
        private static string dataRef = String.Empty;

        public void Filter(TextReader reader)
        {
            var readerSettings = new XmlReaderSettings {IgnoreWhitespace = false};
            xmlReader = XmlReader.Create(reader, readerSettings);

            string sourceOrTarget = "source";
            while (xmlReader.ReadState != ReadState.EndOfFile)
            {
                switch (xmlReader)
                {
                    case XmlReader r when r.Name.Equals("xliff"):
                        OnXlfRoot(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("file"):
                        // Using XElement.FromReader() would have upset the balance of reader.Read()
                        OnXlfFile(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("skeleton"):
                        OnXlfSkeleton(new XlfEventArgs());
                        break;
                    case XmlReader r when r.Name.Equals("unit"):
                        XlfEventArgs f = XlfEventArgs.FromReader(r);
                        //f.Id = "adada";
                        //f.sourceOrTarget = sourceOrTarget;
                        //f.NodeType = r.NodeType.ToString();
                        OnXlfUnit(f);
                        break;
                    case XmlReader r when r.Name.Equals("group"):
                        OnXlfGroup(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("segment"):
                        OnXlfSegment(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("ignorable"):
                        OnXlfIgnorable(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("source"):
                        sourceOrTarget = "source";
                        break;
                    case XmlReader r when r.Name.Equals("target"):
                        sourceOrTarget = "target";
                        break;
                    case XmlReader r when r.NodeType == XmlNodeType.Text:
                        XlfEventArgs tArgs = XlfEventArgs.FromReader(r);
                        tArgs.Text = r.Value;
                        tArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfText(tArgs);
                        break;
                    case XmlReader r when r.Name.Equals("pc"):
                        XlfEventArgs pcArgs = XlfEventArgs.FromReader(r);
                        pcArgs.sourceOrTarget = sourceOrTarget;
                        if (!pcArgs.IsEndElement)
                        {
                            if (pcArgs.Attributes.ContainsKey("dataRefEnd"))
                                dataRef = pcArgs.Attributes["dataRefEnd"];
                            OnXlfScElement(pcArgs);
                        }
                        else
                        {
                            pcArgs.Attributes.Add("dataRef", dataRef);
                            OnXlfEcElement(pcArgs);
                        }
                        break;
                    case XmlReader r when r.Name.Equals("sc"):
                        XlfEventArgs scArgs = XlfEventArgs.FromReader(r);
                        scArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfScElement(scArgs);
                        break;
                    case XmlReader r when r.Name.Equals("ec"):
                        XlfEventArgs ecArgs = XlfEventArgs.FromReader(r);
                        ecArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfEcElement(ecArgs);
                        break;
                    case XmlReader r when r.Name.Equals("sm"):
                        XlfEventArgs smArgs = XlfEventArgs.FromReader(r);
                        smArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfSmElment(smArgs);
                        break;
                    case XmlReader r when r.Name.Equals("em"):
                        XlfEventArgs emArgs = XlfEventArgs.FromReader(r);
                        emArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfEmElement(emArgs);
                        break;
                    case XmlReader r when r.Name.Equals("ph"):
                        XlfEventArgs phArgs = XlfEventArgs.FromReader(r);
                        phArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfPhElement(phArgs);
                        break;
                    case XmlReader r when r.Name.Equals("mrk"):
                        XlfEventArgs mrkArgs = XlfEventArgs.FromReader(r);
                        mrkArgs.sourceOrTarget = sourceOrTarget;
                        if (mrkArgs.NodeType.Equals("Element"))
                            OnXlfSmElment(mrkArgs);
                        else
                            OnXlfEmElement(mrkArgs);
                        break;
                    case XmlReader r when r.Name.Equals("ctr:changeTrack"):
                        OnModCtrChangeTrack(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("ctr:revisions"):
                        OnModCtrRevisions(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("ctr:revision"):
                        OnModCtrRevision(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("ctr:item"):
                        XlfEventArgs itemArgs = XlfEventArgs.FromReader(r);
                        r.Read();
                        itemArgs.Text = r.Value;
                        OnModCtrRevisionItem(itemArgs);
                        break;
                    case XmlReader r when r.Name.Equals("gls:glossEntry"):
                        OnModGlossaryEntry(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("gls:definition"):
                        XlfEventArgs defArgs = XlfEventArgs.FromReader(r);
                        r.Read();
                        defArgs.Text = r.Value;
                        OnModGlossDefinition(defArgs);
                        break;
                    case XmlReader r when r.Name.Equals("gls:term"):
                        XlfEventArgs termArgs = XlfEventArgs.FromReader(r);
                        r.Read();
                        termArgs.Text = r.Value;
                        OnModGlossTerm(termArgs);
                        break;
                    case XmlReader r when r.Name.Equals("gls:translation"):
                        XlfEventArgs transArgs = XlfEventArgs.FromReader(r);
                        r.Read();
                        transArgs.Text = r.Value;
                        OnModGlossTranslation(transArgs);
                        break;
                    case XmlReader r when r.Name.Equals("mda:metadata"):
                        OnModMetadata(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("mda:metaGroup"):
                        OnModMetaGroup(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("mda:meta"):
                        XlfEventArgs metaArgs = XlfEventArgs.FromReader(r);
                        r.Read();
                        metaArgs.Text = r.Value;
                        OnModMetaitem(metaArgs);
                        break;
                    case XmlReader r when r.Name.Equals("mtc:match"):
                        OnModMtcMatch(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("its:locQualityIssues"):
                        XlfEventArgs lqiFilterEventArgs = XlfEventArgs.FromReader(r);
                        if (lqiFilterEventArgs.Attributes.Count > 0)
                            currentLqiRef = lqiFilterEventArgs?.Attributes["xml:id"];
                        OnItsLocQualityIssues(lqiFilterEventArgs);
                        break;
                    case XmlReader r when r.Name.Equals("its:locQualityIssue"):
                        XlfEventArgs lqiArgs = XlfEventArgs.FromReader(r);
                        lqiArgs.Attributes.Add("its:locQualityIssuesRef", currentLqiRef);
                        OnItsLocQualityIssue(lqiArgs);
                        break;
                    case XmlReader r when r.Name.Equals("res:resourceData"):
                        OnModResourceData(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("res:resourceItem"):
                        OnModResourceItem(XlfEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("res:source"):
                        OnModResourceSource(XlfEventArgs.FromReader(r));
                        break;
                    default:
                        break;
                }

                // Try to use Read() here. Using XElement.ReadFrom() reads the whole element and also advances the reader.
                xmlReader.Read();
            }
        }

        public event XlfEvent ModCtrChangeTrackEvent;
        public event XlfEvent ModCtrRevisionEvent;
        public event XlfEvent ModCtrRevisionItemEvent;
        public event XlfEvent ModCtrRevisionsEvent;
        public event XlfEvent ModGlsDefinitionEvent;
        public event XlfEvent ModGlsEntryEvent;
        public event XlfEvent ModGlsTermEvent;
        public event XlfEvent ModGlsTranslationEvent;
        public event XlfEvent ModItsLocQualityIssue;
        public event XlfEvent ModItsLocQualityIssues;
        public event XlfEvent ModMdaMetadataEvent;
        public event XlfEvent ModMdaMetaGroupEvent;
        public event XlfEvent ModMdaMetaitemEvent;
        public event XlfEvent ModResResourceDataEvent;
        public event XlfEvent ModResResourceItemEvent;
        public event XlfEvent ModResSourceEvent;
        public event XlfEvent ModMtcMatchEvent;

        public virtual void OnItsLocQualityIssue(XlfEventArgs xeArgs)
        {
            ModItsLocQualityIssue?.Invoke(xeArgs);
        }

        private void OnItsLocQualityIssues(XlfEventArgs xeArgs)
        {
            ModItsLocQualityIssues?.Invoke(xeArgs);
        }

        public void OnModCtrChangeTrack(XlfEventArgs xeArgs)
        {
            ModCtrChangeTrackEvent?.Invoke(xeArgs);
        }

        public void OnModCtrRevision(XlfEventArgs xeArgs)
        {
            ModCtrRevisionEvent?.Invoke(xeArgs);
        }

        public void OnModCtrRevisionItem(XlfEventArgs xeArgs)
        {
            ModCtrRevisionItemEvent?.Invoke(xeArgs);
        }

        public void OnModCtrRevisions(XlfEventArgs xeArgs)
        {
            ModCtrRevisionsEvent?.Invoke(xeArgs);
        }

        public virtual void OnModGlossaryEntry(XlfEventArgs xeArgs)
        {
            ModGlsEntryEvent?.Invoke(xeArgs);
        }

        public virtual void OnModGlossDefinition(XlfEventArgs xeArgs)
        {
            ModGlsDefinitionEvent?.Invoke(xeArgs);
        }

        public virtual void OnModGlossTerm(XlfEventArgs xeArgs)
        {
            ModGlsTermEvent?.Invoke(xeArgs);
        }

        public virtual void OnModGlossTranslation(XlfEventArgs xeArgs)
        {
            ModGlsTranslationEvent?.Invoke(xeArgs);
        }

        public virtual void OnModMetadata(XlfEventArgs xeArgs)
        {
            ModMdaMetadataEvent?.Invoke(xeArgs);
        }

        private void OnModMetaGroup(XlfEventArgs xeArgs)
        {
            ModMdaMetaGroupEvent?.Invoke(xeArgs);
        }

        private void OnModMetaitem(XlfEventArgs xeArgs)
        {
            ModMdaMetaitemEvent?.Invoke(xeArgs);
        }

        private void OnModResourceData(XlfEventArgs xeArgs)
        {
            ModResResourceDataEvent?.Invoke(xeArgs);
        }

        private void OnModResourceItem(XlfEventArgs xeArgs)
        {
            ModResResourceItemEvent?.Invoke(xeArgs);
        }

        public virtual void OnModResourceSource(XlfEventArgs xeArgs)
        {
            ModResSourceEvent?.Invoke(xeArgs);
        }

        public virtual void OnModMtcMatch(XlfEventArgs xeArgs)
        {
            ModMtcMatchEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfEcElement(XlfEventArgs xeArgs)
        {
            XlfEcElementEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfEmElement(XlfEventArgs xeArgs)
        {
            XlfEmElementEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfFile(XlfEventArgs xeArgs)
        {
            // Ideally this filter would have done the mapping to the File object but the JliffBuilder needs to know if 
            // its a start or end element in order to build the object graph
            XlfFileEvent?.Invoke(xeArgs);
        }

        private void OnXlfGroup(XlfEventArgs xeArgs)
        {
            XlfGroupEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfIgnorable(XlfEventArgs xeArgs)
        {
            XlfIgnorableEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfOriginalData(XlfEventArgs xeArgs)
        {
            XlfOriginalDataEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfPhElement(XlfEventArgs fliterEventArgs)
        {
            XlfPhElementEvent?.Invoke(fliterEventArgs);
        }

        public virtual void OnXlfRoot(XlfEventArgs xeArgs)
        {
            XlfRootEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfScElement(XlfEventArgs xeArgs)
        {
            XlfScElementEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfSegment(XlfEventArgs xeArgs)
        {
            XlfSegmentEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfSkeleton(XlfEventArgs xeArgs)
        {
            XlfSkeletonEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfSmElment(XlfEventArgs xeArgs)
        {
            XlfSmElementEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfSource(XlfEventArgs xeArgs)
        {
            XlfSourceEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfTarget(XlfEventArgs xeArgs)
        {
            XlfTargetEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfText(XlfEventArgs xeArgs)
        {
            XlfTextEvent?.Invoke(xeArgs);
        }

        public virtual void OnXlfUnit(XlfEventArgs xeArgs)
        {
            XlfUnitEvent?.Invoke(xeArgs);
        }

        public event XlfEvent XlfEcElementEvent;
        public event XlfEvent XlfEmElementEvent;
        public event XlfEvent XlfFileEvent;
        public event XlfEvent XlfGroupEvent;
        public event XlfEvent XlfIgnorableEvent;
        public event XlfEvent XlfOriginalDataEvent;
        public event XlfEvent XlfPhElementEvent;
        public event XlfEvent XlfRootEvent;
        public event XlfEvent XlfScElementEvent;
        public event XlfEvent XlfSegmentEvent;
        public event XlfEvent XlfSkeletonEvent;
        public event XlfEvent XlfSmElementEvent;
        public event XlfEvent XlfSourceEvent;
        public event XlfEvent XlfTargetEvent;
        public event XlfEvent XlfTextEvent;
        public event XlfEvent XlfUnitEvent;
    }
}