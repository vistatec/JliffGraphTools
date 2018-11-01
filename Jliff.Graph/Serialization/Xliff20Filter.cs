using System;
using System.IO;
using System.Xml;

namespace Localization.Jliff.Graph
{
    public class Xliff20Filter
    {
        private static XmlReader xmlReader;

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
                        OnXlfFile(this, FilterEventArgs.FromReader(r));
                        break;
                    case XmlReader r when r.Name.Equals("skeleton"):
                        OnXlfSkeleton(this, new FilterEventArgs());
                        break;
                    case XmlReader r when r.Name.Equals("unit"):
                        OnXlfUnit(this, FilterEventArgs.FromReader(r));
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
                    default:
                        break;
                }

                // Try to use Read() here. Using XElement.ReadFrom() reads the whole element and also advances the reader.
                xmlReader.Read();
            }
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
            XlfFileEvent?.Invoke(sender, filterEventArgs);
        }

        public virtual void OnXlfIgnorable(object sender, FilterEventArgs filterEventArgs)
        {
            XlfIgnorableEvent?.Invoke(sender, filterEventArgs);
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

        private void OnXlfText(object sender, FilterEventArgs filterEventArgs)
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