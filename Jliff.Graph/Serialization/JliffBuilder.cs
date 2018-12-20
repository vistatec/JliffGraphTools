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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using Jliff.Graph.Core;
using Jliff.Graph.Modules.ChangeTrack;
using Jliff.Graph.Modules.ITS;
using Jliff.Graph.Modules.Matches;
using Jliff.Graph.Serialization;
using Localization.Jliff.Graph.Interfaces;
using Localization.Jliff.Graph.Modules.Metadata;
using Localization.Jliff.Graph.Modules.ResourceData;

namespace Localization.Jliff.Graph
{
    public class JliffBuilder : ISubfileBuilder, ISubUnitBuilder, IElementBuilder
    {
        private static int filesIdx = -1;
        private static JliffDocument jliff;
        private static readonly Stack<object> stack = new Stack<object>();
        private readonly Dictionary<string, Unit> unitsWithLqi = new Dictionary<string, Unit>();
        private IMapper mapper;

        public JliffBuilder()
        {
        }

        public JliffBuilder(string srcLang, string trgLang)
        {
            CreateMaps();

            jliff = new JliffDocument(srcLang, trgLang);
            stack.Push(jliff);
        }

        public JliffDocument Jliff => jliff;

        public void ChangeTrack(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Unit u:
                        ChangeTrack ct = mapper.Map<ChangeTrack>(args);
                        u.ChangeTrack = ct;
                        stack.Push(ct);
                        break;
                }
            }
        }

        private void CreateMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<XlfEventArgs, File>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s => s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<string, Nmtoken>()
                    .ConstructUsing(i => new Nmtoken(i))
                    .ForMember(m => m.Token,
                        o => o.MapFrom(s =>
                            s));

                cfg.CreateMap<XlfEventArgs, ChangeTrack>()
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, Revisions>()
                    .ForMember(m => m.AppliesTo,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("appliesTo")).Value))
                    .ForMember(m => m.CurrentVersion,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("currentVersion")).Value))
                    .ForMember(m => m.Ref,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("ref")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, Revision>()
                    .ForMember(m => m.Author,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("author")).Value))
                    .ForMember(m => m.DateTime,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("datetime")).Value))
                    .ForMember(m => m.Version,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("version")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, RevisionItem>()
                    .ForMember(m => m.Property,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("property")).Value))
                    .ForMember(m => m.Text,
                        o => o.MapFrom(s => s.Text))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, Unit>()
                    //.ForMember(m => m.Id, opt => opt.Condition(src => src.Attributes.First().Value != null))
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s => s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    //.ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                    //.ForMember(m => m.LocQualityIssues, opt => opt.Condition(src => src.Attributes["its:locQualityIssuesRef"] != null));
                    .ForAllOtherMembers(opt => opt.Ignore());

                cfg.CreateMap<XlfEventArgs, Group>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(o => o.Ignore());

                cfg.CreateMap<XlfEventArgs, Metadata>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(o => o.Ignore());

                cfg.CreateMap<XlfEventArgs, MetaGroup>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(o => o.Ignore());

                //cfg.CreateMap<XlfEventArgs, KeyValuePair<string, string>>()
                //    .ConstructUsing(i => new KeyValuePair<string, string>(
                //        i.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Key,
                //        i.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value));

                //cfg.CreateMap<XlfEventArgs, Dictionary<string, string>>()
                //    .ConstructUsing()

                cfg.CreateMap<XlfEventArgs, Metaitem>()
                    .ConstructUsing(i => new Metaitem(
                        i.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value,
                        i.Text))
                    .ForAllOtherMembers(o => o.Ignore());

                cfg.CreateMap<XlfEventArgs, LocQualityIssue>()
                    .ForMember(m => m.LocQualityIssueComment,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("locQualityIssueComment")).Value))
                    .ForMember(m => m.LocQualityIssueType,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("locQualityIssueType")).Value))
                    .ForMember(m => m.LocQualityIssueSeverity,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("locQualityIssueSeverity")).Value))
                    .ForMember(m => m.LocQualityIssueEnabled,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("locQualityIssueEnabled")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, LocQualityIssues>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("id")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, Match>()
                    .ForMember(m => m.MatchQuality,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("matchQuality")).Value))
                    .ForMember(m => m.Origin,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("origin")).Value))
                    .ForMember(m => m.Similarity,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("similarity")).Value))
                    .ForMember(m => m.Type,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, GlossaryEntry>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForMember(m => m.Ref,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("ref")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, Definition>()
                    .ForMember(m => m.Source,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("source")).Value))
                    .ForMember(m => m.Text,
                        o => o.MapFrom(s =>
                            s.Text))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, Segment>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, Term>()
                    .ForMember(m => m.Source,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("source")).Value))
                    .ForMember(m => m.Text,
                        o => o.MapFrom(s =>
                            s.Text))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, Translation>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForMember(m => m.Text,
                        o => o.MapFrom(s =>
                            s.Text))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, ResourceData>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, ResourceItem>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, Source>()
                    .ForMember(m => m.Href,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("href")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, PhElement>()
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<XlfEventArgs, SmElement>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForMember(m => m.LocQualityIssueComment,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("locQualityIssueComment")).Value))
                    .ForMember(m => m.LocQualityIssueType,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("locQualityIssueType")).Value))
                    .ForMember(m => m.LocQualityIssueSeverity,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("locQualityIssueSeverity")).Value))
                    .ForMember(m => m.LocQualityIssueEnabled,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("locQualityIssueEnabled")).Value))
                    .ForMember(m => m.LocQualityIssuesRef,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("locQualityIssuesRef")).Value))
                    //.ForMember(m => m.ProvenanceRecordsRef,
                    //    o => o.ResolveUsing<ProvenanceRecordsRefValueResolver>())
                    //.ForMember(m => m.ProvenanceRecordsRef,
                    //    o => o.Condition(s =>
                    //        s.Attributes.SingleOrDefault(a => a.Key.EndsWith("provenanceRecordsRef")).Value != null))
                    .ForMember(m => m.ProvenanceRecordsRef,
                        o => o.ResolveUsing(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("provenanceRecordsRef"))))
                    .ForMember(m => m.Type,
                        o => o.MapFrom(s => s.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value))
                    .ForAllOtherMembers(m => m.Ignore());
            });
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();
        }

        public void Definition(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                //stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case GlossaryEntry ge:
                        Definition d = mapper.Map<Definition>(args);
                        ge.Definition = d;
                        //stack.Push(e);
                        break;
                }
            }
        }

        public void EcElement(XlfEventArgs args)
        {
            object parent = stack.Peek();
            switch (parent)
            {
                case Segment s:
                    EcElement ecElement = new EcElement();
                    ecElement.Attributes = args.Attributes;
                    if (args.sourceOrTarget.Equals("source"))
                        s.Source.Add(ecElement);
                    else
                        s.Target.Add(ecElement);

                    break;
                default:
                    break;
            }
        }

        public IElementBuilder AddEmElement(string id, bool forSource)
        {
            XlfEventArgs args = new XlfEventArgs();
            args.Attributes["id"] = id;
            if (forSource)
                args.sourceOrTarget = "source";
            else
                args.sourceOrTarget = "target";
            EmElement(args);
            return this;
        }


        public void EmElement(XlfEventArgs args)
        {
            object parent = stack.Peek();
            switch (parent)
            {
                case Segment s:
                    EmElement emElement = new EmElement();
                    emElement.Attributes = args.Attributes;
                    if (args.sourceOrTarget.Equals("source"))
                        s.Source.Add(emElement);
                    else
                        s.Target.Add(emElement);

                    break;
                default:
                    break;
            }
        }

        public ISubfileBuilder AddFile(XlfEventArgs args)
        {
            File(args);
            return this;
        }

        public ISubfileBuilder AddFile(Dictionary<string, string> properties)
        {
            File(new XlfEventArgs(String.Empty, String.Empty, properties));
            return this;
        }

        public void File(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                if (stack.Peek() is JliffDocument parent)
                {
                    File file = mapper.Map<File>(args);
                    stack.Push(file);
                    parent.Files.Add(file);
                }
                else
                {
                    throw new Exception("Was expecting a Jliff object.");
                }
            }
        }

        public void GlossaryEntry(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Unit u:
                        GlossaryEntry e = mapper.Map<GlossaryEntry>(args);
                        u.Glossary.Add(e);
                        stack.Push(e);
                        break;
                }
            }
        }

        public ISubfileBuilder AddGroup(XlfEventArgs args)
        {
            Group(args);
            return this;
        }

        public void Group(XlfEventArgs args)
        {
            if (args.NodeType.Equals("EndElement"))
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Group g:
                        Group newg1 = mapper.Map<Group>(args);
                        g.Subgroups.Add(newg1);
                        stack.Push(newg1);
                        break;
                    case File f:
                        Group newg2 = mapper.Map<Group>(args);
                        f.Subfiles.Add(newg2);
                        stack.Push(newg2);
                        break;
                    default:
                        throw new Exception("Was expecting either a Unit or Group object.");
                }
            }
        }

        public void Ignorable(XlfEventArgs args)
        {
            if (args.NodeType.Equals("EndElement"))
            {
                stack.Pop();
            }
            else
            {
                Unit parent = stack.Peek() as Unit;
                if (parent != null)
                {
                    Ignorable ignorable = new Ignorable();
                    parent.Subunits.Add(ignorable);
                    stack.Push(ignorable);
                }
                else
                {
                    throw new Exception("Was expecting a Unit object.");
                }
            }
        }

        public void LocQualityIssues(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                LocQualityIssues lqis = mapper.Map<LocQualityIssues>(args);

                object parent = stack.Peek();
                switch (parent)
                {
                    case Unit u:
                        u.LocQualityIssues = lqis;
                        stack.Push(lqis);
                        break;
                }
            }
        }

        public void LocQualityIssue(XlfEventArgs args)
        {
            LocQualityIssue lqi = mapper.Map<LocQualityIssue>(args);

            object parent = stack.Peek();
            switch (parent)
            {
                case LocQualityIssues lqis:
                    lqis.Items.Add(lqi);
                    break;
            }
        }

        public void Match(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Unit u:
                        Match m = mapper.Map<Match>(args);
                        u.Matches.Add(m);
                        stack.Push(m);
                        break;
                }
            }
        }

        public void Metadata(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Unit u:
                        Metadata m = mapper.Map<Metadata>(args);
                        u.Metadata = m;
                        stack.Push(m);
                        break;
                }
            }
        }

        public void MetaGroup(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Metadata m:
                        MetaGroup mg = mapper.Map<MetaGroup>(args);
                        m.Groups.Add(mg);
                        stack.Push(mg);
                        break;
                }
            }
        }

        public void Metaitem(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                //stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case MetaGroup mg:
                        Metaitem mi = mapper.Map<Metaitem>(args);
                        mg.Meta.Add(mi);
                        //stack.Push(mg);
                        break;
                }
            }
        }

        public void OriginalData(XlfEventArgs args)
        {
        }

        public void PhElement(XlfEventArgs args)
        {
            if (!args.NodeType.Equals("EndElement"))
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Segment s:
                        PhElement source1 = new PhElement();
                        source1.Attributes = args.Attributes;
                        if (args.sourceOrTarget.Equals("source"))
                            s.Source.Add(source1);
                        else
                            s.Target.Add(source1);
                        break;
                    case Ignorable i:
                        PhElement source2 = new PhElement();
                        if (args.sourceOrTarget.Equals("target"))
                            i.Source.Add(source2);
                        else
                            i.Target.Add(source2);
                        break;
                    default:
                        throw new Exception("Was expecting a Segment or Ignorable object.");
                        break;
                }
            }
        }

        public virtual void ResourceData(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case File f:
                        ResourceData rd = mapper.Map<ResourceData>(args);
                        f.ResourceData = rd;
                        stack.Push(rd);
                        break;
                }
            }
        }

        public virtual void ResourceItem(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case ResourceData rd:
                        ResourceItem ri = mapper.Map<ResourceItem>(args);
                        rd.ResourceItems.Add(ri);
                        stack.Push(ri);
                        break;
                }
            }
        }

        public void ResourceSource(XlfEventArgs args)
        {
            if (!args.IsEndElement)
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case ResourceItem ri:
                        Source s = mapper.Map<Source>(args);
                        ri.Source = s;
                        break;
                }
            }
        }

        public void Revision(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Revisions r:
                        Revision ri = mapper.Map<Revision>(args);
                        r.Items.Add(ri);
                        stack.Push(ri);
                        break;
                }
            }
        }

        public void RevisionItem(XlfEventArgs args)
        {
            if (!args.IsEndElement)
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Revision r:
                        RevisionItem ri = mapper.Map<RevisionItem>(args);
                        r.Item = ri;
                        break;
                }
            }
        }

        public void Revisions(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case ChangeTrack ct:
                        Revisions r = mapper.Map<Revisions>(args);
                        ct.Revisions = r;
                        stack.Push(r);
                        break;
                }
            }
        }

        public void ScElement(XlfEventArgs args)
        {
            if (!args.NodeType.Equals("EndElement"))
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Segment s:
                        ScElement scElement = new ScElement();
                        scElement.Attributes = args.Attributes;
                        if (args.sourceOrTarget.Equals("source"))
                            s.Source.Add(scElement);
                        else
                            s.Target.Add(scElement);

                        break;
                    default:
                        break;
                }
            }
        }

        public ISubUnitBuilder AddSegment(XlfEventArgs args)
        {
            Segment(args);
            return this;
        }

        public void Segment(XlfEventArgs args)
        {
            if (args.NodeType.Equals("EndElement"))
            {
                stack.Pop();
            }
            else
            {
                Unit parent = stack.Peek() as Unit;
                if (parent != null)
                {
                    Segment segment = mapper.Map<Segment>(args);
                    parent.Subunits.Add(segment);
                    stack.Push(segment);
                }
                else
                {
                    throw new Exception("Was expecting a Unit object.");
                }
            }
        }

        public void Serialize(string filename)
        {
            Converter.Serialize(filename, jliff);
        }

        public void Skeleton(XlfEventArgs filterEventArgs)
        {
            object parent = stack.Peek();
            switch (parent)
            {
                case File f:
                    Skeleton skeleton = new Skeleton("");
                    f.Skeleton = skeleton;
                    break;
                default:
                    break;
            }
        }

        public IElementBuilder AddSmElement(string id, bool forSource)
        {
            XlfEventArgs args = new XlfEventArgs();
            args.Attributes["id"] = id;
            if (forSource)
                args.sourceOrTarget = "source";
            else
                args.sourceOrTarget = "target";
            SmElement(args);
            return this;
        }

        public void SmElement(XlfEventArgs args)
        {
            if (!args.NodeType.Equals("EndElement"))
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Segment s:
                        //SmElement smElement = new SmElement();
                        SmElement smElement = mapper.Map<SmElement>(args);
                        //smElement.Attributes = args.Attributes;
                        if (args.sourceOrTarget.Equals("source"))
                            s.Source.Add(smElement);
                        else
                            s.Target.Add(smElement);

                        break;
                    default:
                        break;
                }
            }
        }

        public IElementBuilder AddSource(XlfEventArgs args)
        {
            args.sourceOrTarget = "source";
            Source(args);
            stack.Pop();
            return this;
        }

        public IElementBuilder AddSource(string text)
        {
            XlfEventArgs args = new XlfEventArgs();
            args.Text = text;
            args.sourceOrTarget = "source";
            Source(args);
            stack.Pop();
            return this;
        }

        public void Source(XlfEventArgs args)
        {
            if (args.NodeType.Equals("EndElement"))
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Segment s:
                        TextElement source1 = new TextElement(args.Text);
                        s.Source.Add(source1);
                        stack.Push(source1);
                        break;
                    case Ignorable i:
                        TextElement source2 = new TextElement(args.Text);
                        i.Source.Add(source2);
                        stack.Push(source2);
                        break;
                    default:
                        throw new Exception("Was expecting a Segment object.");
                        break;
                }
            }
        }

        public IElementBuilder AddTarget(XlfEventArgs args)
        {
            args.sourceOrTarget = "target";
            stack.Pop();
            Target(args);
            stack.Pop();
            return this;
        }

        public IElementBuilder AddTarget(string text)
        {
            XlfEventArgs args = new XlfEventArgs();
            args.Text = text;
            args.sourceOrTarget = "target";
            //stack.Pop();
            Target(args);
            stack.Pop();
            return this;
        }

        public void Target(XlfEventArgs args)
        {
            if (args.NodeType.Equals("EndElement"))
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Segment s:
                        TextElement source1 = new TextElement(args.Text);
                        s.Target.Add(source1);
                        stack.Push(source1);
                        break;
                    case Ignorable i:
                        TextElement source2 = new TextElement(args.Text);
                        i.Target.Add(source2);
                        stack.Push(source2);
                        break;
                    default:
                        throw new Exception("Was expecting a Segment object.");
                        break;
                }
            }
        }

        public void Term(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                //stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case GlossaryEntry ge:
                        Term t = mapper.Map<Term>(args);
                        ge.Term = t;
                        //stack.Push(e);
                        break;
                }
            }
        }

        public void Text(XlfEventArgs args)
        {
            object parent = stack.Peek();
            switch (parent)
            {
                case Segment s:
                    TextElement source1 = new TextElement(args.Text);
                    if (args.sourceOrTarget.Equals("source"))
                        s.Source.Add(source1);
                    else
                        s.Target.Add(source1);
                    break;
                case Ignorable i:
                    TextElement source2 = new TextElement(args.Text);
                    if (args.sourceOrTarget.Equals("source"))
                        i.Source.Add(source2);
                    else
                        i.Target.Add(source2);
                    break;
                case Match m:
                    TextElement source3 = new TextElement(args.Text);
                    if (args.sourceOrTarget.Equals("source"))
                        m.Source = source3;
                    else
                        m.Target = source3;
                    break;
                default:
                    //throw new Exception("Was expecting a Segment object.");
                    break;
            }
        }

        public void Translation(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                //stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case GlossaryEntry ge:
                        Translation t = mapper.Map<Translation>(args);
                        ge.Translations.Add(t);
                        //stack.Push(e);
                        break;
                }
            }
        }

        public ISubfileBuilder AddUnit(XlfEventArgs args)
        {
            Unit(args);
            return this;
        }

        public void Unit(XlfEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                object parent = stack.Peek();
                switch (parent)
                {
                    case Group g:
                        Unit unit1 = mapper.Map<Unit>(args);
                        g.Subgroups.Add(unit1);
                        stack.Push(unit1);
                        break;
                    case File f:
                        Unit unit2 = mapper.Map<Unit>(args);
                        f.Subfiles.Add(unit2);
                        stack.Push(unit2);
                        break;
                    default:
                        throw new Exception("Was expecting a File or Group object.");
                }
            }
        }

        public void XlfRoot(XlfEventArgs args)
        {
            //if (args.Attributes.Count > 0 && args.Attributes != null)
            //    if (!args.Attributes["version"].Equals("2.0"))
            //        throw new ArgumentException("Expected version 2.0 XLIFF.");
        }

        public ISubUnitBuilder EndSubFiles()
        {
            return this;
        }

        public IElementBuilder EndSubUnits()
        {
            return this;
        }

        public JliffDocument Build()
        {
            return jliff;
        }

        public ISubUnitBuilder MoreSubUnits()
        {
            stack.Pop();
            return this;
        }
    }
}