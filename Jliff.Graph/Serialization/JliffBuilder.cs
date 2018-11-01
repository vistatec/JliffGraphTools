using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Jliff.Graph.Modules.LocQualityIssue;

namespace Localization.Jliff.Graph
{
    public class JliffBuilder
    {
        private static int filesIdx = -1;
        private static JliffDocument jliff;
        private static readonly Stack<object> stack = new Stack<object>();
        private IMapper mapper;
        private Dictionary<string, Unit> unitsWithLqi = new Dictionary<string, Unit>();

        public JliffBuilder()
        {
        }

        public JliffBuilder(string srcLang, string trgLang)
        {
            CreateMaps();

            jliff = new JliffDocument(srcLang, trgLang);
            stack.Push(jliff);
        }

        private void CreateMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FilterEventArgs, File>()
                    .ForMember(m => m.AnnotatorsRef, opt => opt.Condition(src => src.Attributes["its:annotatorsRef"] != null))
                    .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id));

                cfg.CreateMap<FilterEventArgs, Unit>()
                    .ForMember(m => m.LocQualityIssues, opt => opt.Condition(src => src.Attributes["its:locQualityIssuesRef"] != null));
            });
            mapper = config.CreateMapper();
        }

        public JliffDocument Jliff => jliff;

        public void LocQualityIssue(object sender, FilterEventArgs args)
        {
            if (unitsWithLqi.ContainsKey(args.Attributes["locQualityIssuesRef"]))
            {
                LocQualityIssue lqi = mapper.Map<LocQualityIssue>(args);
                unitsWithLqi[args.Attributes["locQualityIssuesRef"]].LocQualityIssues.Add(lqi);
            }
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
                    //File file = new File(args.Id);
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
                        Group newg1 = new Group(args.Id);
                        g.Subgroups.Add(newg1);
                        stack.Push(newg1);
                        break;
                    case File f:
                        Group newg2 = new Group(args.Id);
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
                        SmElement smElement = new SmElement();
                        smElement.Attributes = args.Attributes;
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
                File parent = stack.Peek() as File;
                if (parent != null)
                {
                    //Unit unit = new Unit(args.Id);
                    Unit unit = mapper.Map<Unit>(args);
                    parent.Subfiles.Add(unit);
                    stack.Push(unit);
                    unitsWithLqi.Add(unit.LocQualityIssuesRef, unit);
                }
                else
                {
                    throw new Exception("Was expecting a File object.");
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