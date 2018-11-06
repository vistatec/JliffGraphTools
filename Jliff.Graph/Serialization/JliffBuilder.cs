using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Jliff.Graph.Modules.LocQualityIssue;
using Localization.Jliff.Graph.Modules.Metadata;
using Localization.Jliff.Graph.Modules.ResourceData;

namespace Localization.Jliff.Graph
{
    public class JliffBuilder
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

        private void CreateMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<FilterEventArgs, File>()
                //.ForMember(m => m.AnnotatorsRef, opt => opt.Condition(src => src.Attributes["its:annotatorsRef"] != null))
                //.ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                //.ForAllOtherMembers(opt => opt.Ignore());

                //cfg.CreateMap<string, Uri>().ConvertUsing<StringToUriConverter>();

                cfg.CreateMap<FilterEventArgs, Unit>()
                    //.ForMember(m => m.Id, opt => opt.Condition(src => src.Attributes.First().Value != null))
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s => s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    //.ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                    //.ForMember(m => m.LocQualityIssues, opt => opt.Condition(src => src.Attributes["its:locQualityIssuesRef"] != null));
                    .ForAllOtherMembers(opt => opt.Ignore());

                cfg.CreateMap<FilterEventArgs, Group>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(o => o.Ignore());

                cfg.CreateMap<FilterEventArgs, Metadata>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(o => o.Ignore());

                cfg.CreateMap<FilterEventArgs, MetaGroup>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(o => o.Ignore());

                //cfg.CreateMap<FilterEventArgs, KeyValuePair<string, string>>()
                //    .ConstructUsing(i => new KeyValuePair<string, string>(
                //        i.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Key,
                //        i.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value));

                //cfg.CreateMap<FilterEventArgs, Dictionary<string, string>>()
                //    .ConstructUsing()

                cfg.CreateMap<FilterEventArgs, Metaitem>()
                    .ConstructUsing(i => new Metaitem(
                        i.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value,
                        i.Text))
                    .ForAllOtherMembers(o => o.Ignore());

                cfg.CreateMap<FilterEventArgs, LocQualityIssue>()
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

                cfg.CreateMap<FilterEventArgs, GlossaryEntry>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForMember(m => m.Ref,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("ref")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<FilterEventArgs, Definition>()
                    .ForMember(m => m.Source,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("source")).Value))
                    .ForMember(m => m.Text,
                        o => o.MapFrom(s =>
                            s.Text))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<FilterEventArgs, Term>()
                    .ForMember(m => m.Source,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("source")).Value))
                    .ForMember(m => m.Text,
                        o => o.MapFrom(s =>
                            s.Text))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<FilterEventArgs, Translation>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForMember(m => m.Text,
                        o => o.MapFrom(s =>
                            s.Text))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<FilterEventArgs, ResourceData>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<FilterEventArgs, ResourceItem>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<FilterEventArgs, Source>()
                    .ForMember(m => m.Href,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.EndsWith("href")).Value))
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<FilterEventArgs, PhElement>()
                    .ForAllOtherMembers(m => m.Ignore());

                cfg.CreateMap<FilterEventArgs, SmElement>()
                    .ForMember(m => m.Id,
                        o => o.MapFrom(s =>
                            s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
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

        public void EcElement(object sender, FilterEventArgs args)
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

        public void EmElement(object sender, FilterEventArgs args)
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


        public void File(object sender, FilterEventArgs args)
        {
            if (args.IsEndElement)
            {
                stack.Pop();
            }
            else
            {
                JliffDocument parent = stack.Peek() as JliffDocument;
                if (parent != null)
                {
                    File file = new File("");
                    //File file = mapper.Map<File>(args);
                    stack.Push(file);
                    parent.Files.Add(file);
                }
                else
                {
                    throw new Exception("Was expecting a Jliff object.");
                }
            }
        }

        public void Group(object sender, FilterEventArgs args)
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

        public void Ignorable(object sender, FilterEventArgs args)
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

        public void LocQualityIssue(object sender, FilterEventArgs args)
        {
            LocQualityIssue lqi = mapper.Map<LocQualityIssue>(args);

            object parent = stack.Peek();
            switch (parent)
            {
                case Unit u:
                    u.LocQualityIssues.Add(lqi);
                    break;
            }
        }

        public void Metadata(object sender, FilterEventArgs args)
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

        public void MetaGroup(object sender, FilterEventArgs args)
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

        public void Metaitem(object sender, FilterEventArgs args)
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

        public void PhElement(object sender, FilterEventArgs args)
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

        public virtual void ResourceData(object sender, FilterEventArgs args)
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

        public virtual void ResourceItem(object sender, FilterEventArgs args)
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

        public void GlossaryEntry(object sender, FilterEventArgs args)
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

        public void Definition(object sender, FilterEventArgs args)
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

        public void Term(object sender, FilterEventArgs args)
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

        public void Translation(object sender, FilterEventArgs args)
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

        public void ResourceSource(object sender, FilterEventArgs args)
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

        public void ScElement(object sender, FilterEventArgs args)
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

        public void Segment(object sender, FilterEventArgs args)
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
                    Segment segment = new Segment();
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

        public void Skeleton(object sender, FilterEventArgs filterEventArgs)
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

        public void SmElement(object sender, FilterEventArgs args)
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

        public void Source(object sender, FilterEventArgs args)
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

        public void Target(object sender, FilterEventArgs args)
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

        public void Text(object sender, FilterEventArgs args)
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
                default:
                    //throw new Exception("Was expecting a Segment object.");
                    break;
            }
        }

        public void Unit(object sender, FilterEventArgs args)
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

        public void XlfRoot(object sender, FilterEventArgs args)
        {
            if (args.Attributes.Count > 0 && args.Attributes != null)
                if (!args.Attributes["version"].Equals("2.0"))
                    throw new ArgumentException("Expected version 2.0 XLIFF.");
        }
    }
}