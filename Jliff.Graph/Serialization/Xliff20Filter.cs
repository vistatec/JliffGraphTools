using System;
using System.IO;
using System.Xml;

namespace Localization.Jliff.Graph
{
    public class Xliff20Filter
    {
        private static XmlReader xmlReader;
        private static string currentLqiRef = string.Empty;

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
                        OnXlfRoot(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("file"):
                        // Using XElement.FromReader() would have upset the balance of reader.Read()
                        OnXlfFile(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("skeleton"):
                        OnXlfSkeleton(this, new FilterEventArgs());
                        break;
                    case XmlReader r when r.Name.Equals("unit"):
                        FilterEventArgs f = FilterEventArgs.FromReader(r);
                        //f.Id = "adada";
                        //f.sourceOrTarget = sourceOrTarget;
                        //f.NodeType = r.NodeType.ToString();
                        OnXlfUnit(this, f);
                        break;
                    case XmlReader r when r.Name.Equals("group"):
                        OnXlfGroup(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("segment"):
                        OnXlfSegment(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("ignorable"):
                        OnXlfIgnorable(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("source"):
                        sourceOrTarget = "source";
                        break;
                    case XmlReader r when r.Name.Equals("target"):
                        sourceOrTarget = "target";
                        break;
                    case XmlReader r when r.NodeType == XmlNodeType.Text:
                        FilterEventArgs tArgs = FilterEventArgs.FromReader(r);
                        tArgs.Text = r.Value;
                        tArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfText(this, tArgs);
                        break;
                    case XmlReader r when r.Name.Equals("pc"):
                        FilterEventArgs pcArgs = FilterEventArgs.FromReader(r);
                        pcArgs.sourceOrTarget = sourceOrTarget;
                        if (r.NodeType == XmlNodeType.Element)
                            OnXlfScElement(this, pcArgs);
                        else
                            OnXlfEcElement(this, pcArgs);
                        break;
                    case XmlReader r when r.Name.Equals("sc"):
                        FilterEventArgs scArgs = FilterEventArgs.FromReader(r);
                        scArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfScElement(this, scArgs);
                        break;
                    case XmlReader r when r.Name.Equals("ec"):
                        FilterEventArgs ecArgs = FilterEventArgs.FromReader(r);
                        ecArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfEcElement(this, ecArgs);
                        break;
                    case XmlReader r when r.Name.Equals("sm"):
                        FilterEventArgs smArgs = FilterEventArgs.FromReader(r);
                        smArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfSmElment(this, smArgs);
                        break;
                    case XmlReader r when r.Name.Equals("em"):
                        FilterEventArgs emArgs = FilterEventArgs.FromReader(r);
                        emArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfEmElement(this, emArgs);
                        break;
                    case XmlReader r when r.Name.Equals("ph"):
                        FilterEventArgs phArgs = FilterEventArgs.FromReader(r);
                        phArgs.sourceOrTarget = sourceOrTarget;
                        OnXlfPhElement(this, phArgs);
                        break;
                    case XmlReader r when r.Name.Equals("mrk"):
                        FilterEventArgs mrkArgs = FilterEventArgs.FromReader(r);
                        mrkArgs.sourceOrTarget = sourceOrTarget;
                        if (mrkArgs.NodeType.Equals("Element"))
                            OnXlfSmElment(this, mrkArgs);
                        else
                            OnXlfEmElement(this, mrkArgs);
                        break;
                    case XmlReader r when r.Name.Equals("gls:glossEntry"):
                        OnModGlossaryEntry(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("gls:definition"):
                        FilterEventArgs defArgs = FilterEventArgs.FromReader(r);
                        r.Read();
                        defArgs.Text = r.Value;
                        OnModGlossDefinition(this, defArgs);
                        break;
                    case XmlReader r when r.Name.Equals("gls:term"):
                        FilterEventArgs termArgs = FilterEventArgs.FromReader(r);
                        r.Read();
                        termArgs.Text = r.Value;
                        OnModGlossTerm(this, termArgs);
                        break;
                    case XmlReader r when r.Name.Equals("gls:translation"):
                        FilterEventArgs transArgs = FilterEventArgs.FromReader(r);
                        r.Read();
                        transArgs.Text = r.Value;
                        OnModGlossTranslation(this, transArgs);
                        break;
                    case XmlReader r when r.Name.Equals("md:metadata"):
                        OnModMetadata(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("md:metaGroup"):
                        OnModMetaGroup(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("md:meta"):
                        FilterEventArgs metaArgs = FilterEventArgs.FromReader(r);
                        r.Read();
                        metaArgs.Text = r.Value;
                        OnModMetaitem(this, metaArgs);
                        break;
                    case XmlReader r when r.Name.Equals("mtc:match"):
                        OnModTransCandMatch(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("its:locQualityIssues"):
                        FilterEventArgs lqiFilterEventArgs = FilterEventArgs.FromReader(r);
                        if (lqiFilterEventArgs.Attributes.Count > 0)
                            currentLqiRef = lqiFilterEventArgs?.Attributes["xml:id"];
                        break;
                    case XmlReader r when r.Name.Equals("its:locQualityIssue"):
                        FilterEventArgs lqiArgs = FilterEventArgs.FromReader(r);
                        lqiArgs.Attributes.Add("its:locQualityIssuesRef", currentLqiRef);
                        OnItsLocQualityIssue(this, lqiArgs);
                        break;
                    case XmlReader r when r.Name.Equals("res:resourceData"):
                        OnModResourceData(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("res:resourceItem"):
                        OnModResourceItem(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("res:source"):
                        OnModResourceSource(this, FilterEventArgs.FromReader(r));
                        break;
                    default:
                        break;
                }

                // Try to use Read() here. Using XElement.ReadFrom() reads the whole element and also advances the reader.
                xmlReader.Read();
            }
        }

        public event EventHandler<FilterEventArgs> ModGlossaryEntryEvent;
        public event EventHandler<FilterEventArgs> ModGlossDefinitionEvent;
        public event EventHandler<FilterEventArgs> ModGlossTermEvent;
        public event EventHandler<FilterEventArgs> ModGlossTranslationEvent;
        public event EventHandler<FilterEventArgs> ModItsLocQualityIssue;
        public event EventHandler<FilterEventArgs> ModMetadataEvent;
        public event EventHandler<FilterEventArgs> ModMetaGroupEvent;
        public event EventHandler<FilterEventArgs> ModMetaitemEvent;
        public event EventHandler<FilterEventArgs> ModResourceDataEvent;
        public event EventHandler<FilterEventArgs> ModResourceItemEvent;
        public event EventHandler<FilterEventArgs> ModResourceSourceEvent;
        public event EventHandler<FilterEventArgs> ModTransCandMatchEvent;

        public virtual void OnItsLocQualityIssue(object sender, FilterEventArgs filterEventArgs)
        {
            ModItsLocQualityIssue?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnModGlossaryEntry(object sender, FilterEventArgs filterEventArgs)
        {
            ModGlossaryEntryEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnModGlossDefinition(object sender, FilterEventArgs filterEventArgs)
        {
            ModGlossDefinitionEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnModGlossTerm(object sender, FilterEventArgs filterEventArgs)
        {
            ModGlossTermEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnModGlossTranslation(object sender, FilterEventArgs filterEventArgs)
        {
            ModGlossTranslationEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnModMetadata(object sender, FilterEventArgs filterEventArgs)
        {
            ModMetadataEvent?.Invoke(sender, filterEventArgs);
        }

        private void OnModMetaGroup(object sender, FilterEventArgs filterEventArgs)
        {
            ModMetaGroupEvent?.Invoke(sender, filterEventArgs);
        }

        private void OnModMetaitem(object sender, FilterEventArgs filterEventArgs)
        {
            ModMetaitemEvent?.Invoke(sender, filterEventArgs);
        }

        private void OnModResourceData(object sender, FilterEventArgs filterEventArgs)
        {
            ModResourceDataEvent?.Invoke(sender, filterEventArgs);
        }

        private void OnModResourceItem(object sender, FilterEventArgs filterEventArgs)
        {
            ModResourceItemEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnModResourceSource(object sender, FilterEventArgs filterEventArgs)
        {
            ModResourceSourceEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnModTransCandMatch(object sender, FilterEventArgs filterEventArgs)
        {
            ModTransCandMatchEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfEcElement(object sender, FilterEventArgs filterEventArgs)
        {
            XlfEcElementEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfEmElement(object sender, FilterEventArgs filterEventArgs)
        {
            XlfEmElementEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfFile(object sender, FilterEventArgs filterEventArgs)
        {
            // Ideally this filter would have done the mapping to the File object but the JliffBuilder needs to know if 
            // its a start or end element in order to build the object graph
            XlfFileEvent?.Invoke(sender, filterEventArgs);
        }

        private void OnXlfGroup(object sender, FilterEventArgs filterEventArgs)
        {
            XlfGroupEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfIgnorable(object sender, FilterEventArgs filterEventArgs)
        {
            XlfIgnorableEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfOriginalData(object sender, FilterEventArgs filterEventArgs)
        {
            XlfOriginalDataEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfPhElement(object sender, FilterEventArgs fliterEventArgs)
        {
            XlfPhElementEvent?.Invoke(sender, fliterEventArgs);
        }

        public virtual void OnXlfRoot(object sender, FilterEventArgs filterEventArgs)
        {
            XlfRootEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfScElement(object sender, FilterEventArgs filterEventArgs)
        {
            XlfScElementEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfSegment(object sender, FilterEventArgs filterEventArgs)
        {
            XlfSegmentEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfSkeleton(object sender, FilterEventArgs filterEventArgs)
        {
            XlfSkeletonEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfSmElment(object sender, FilterEventArgs filterEventArgs)
        {
            XlfSmElementEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfSource(object sender, FilterEventArgs filterEventArgs)
        {
            XlfSourceEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfTarget(object sender, FilterEventArgs filterEventArgs)
        {
            XlfTargetEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfText(object sender, FilterEventArgs filterEventArgs)
        {
            XlfTextEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfUnit(object sender, FilterEventArgs filterEventArgs)
        {
            XlfUnitEvent?.Invoke(sender, filterEventArgs);
        }

        public event EventHandler<FilterEventArgs> XlfEcElementEvent;
        public event EventHandler<FilterEventArgs> XlfEmElementEvent;
        public event EventHandler<FilterEventArgs> XlfFileEvent;
        public event EventHandler<FilterEventArgs> XlfGroupEvent;
        public event EventHandler<FilterEventArgs> XlfIgnorableEvent;
        public event EventHandler<FilterEventArgs> XlfOriginalDataEvent;
        public event EventHandler<FilterEventArgs> XlfPhElementEvent;
        public event EventHandler<FilterEventArgs> XlfRootEvent;
        public event EventHandler<FilterEventArgs> XlfScElementEvent;
        public event EventHandler<FilterEventArgs> XlfSegmentEvent;
        public event EventHandler<FilterEventArgs> XlfSkeletonEvent;
        public event EventHandler<FilterEventArgs> XlfSmElementEvent;
        public event EventHandler<FilterEventArgs> XlfSourceEvent;
        public event EventHandler<FilterEventArgs> XlfTargetEvent;
        public event EventHandler<FilterEventArgs> XlfTextEvent;
        public event EventHandler<FilterEventArgs> XlfUnitEvent;
    }
}