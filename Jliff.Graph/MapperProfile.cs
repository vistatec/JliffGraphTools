using System.Linq;
using AutoMapper;
using Jliff.Graph.Core;
using Jliff.Graph.Modules.ChangeTrack;
using Jliff.Graph.Modules.ITS;
using Jliff.Graph.Modules.Matches;
using Jliff.Graph.Serialization;
using Localization.Jliff.Graph;
using Localization.Jliff.Graph.Modules.Metadata;
using Localization.Jliff.Graph.Modules.ResourceData;

namespace Jliff.Graph
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<XlfEventArgs, File>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s => s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<string, Nmtoken>()
                .ConstructUsing(i => new Nmtoken(i))
                .ForMember(m => m.Token,
                    o => o.MapFrom(s =>
                        s));

            CreateMap<XlfEventArgs, ChangeTrack>()
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, Revisions>()
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

            CreateMap<XlfEventArgs, Revision>()
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

            CreateMap<XlfEventArgs, RevisionItem>()
                .ForMember(m => m.Property,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("property")).Value))
                .ForMember(m => m.Text,
                    o => o.MapFrom(s => s.Text))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, Unit>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s => s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<XlfEventArgs, Group>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<XlfEventArgs, Metadata>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<XlfEventArgs, MetaGroup>()
                .ForMember(m => m.AppliesTo,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("appliesTo")).Value))
                .ForMember(m => m.Category,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("category")).Value))
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<XlfEventArgs, Metaitem>()
                .ConstructUsing(i => new Metaitem(
                    i.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value,
                    i.Text))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<XlfEventArgs, Note>()
                .ForMember(m => m.AppliesTo,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("appliesTo")).Value))
                .ForMember(m => m.Category,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("category")).Value))
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForMember(m => m.Priority,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("priority")).Value))
                .ForMember(m => m.Text,
                    o => o.MapFrom(s =>
                        s.Text))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<XlfEventArgs, LocQualityIssue>()
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

            CreateMap<XlfEventArgs, LocQualityIssues>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.EndsWith("id")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, Match>()
                .ForMember(m => m.MatchQuality,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("matchQuality")).Value))
                .ForMember(m => m.Origin,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("origin")).Value))
                .ForMember(m => m.Ref,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("ref")).Value))
                .ForMember(m => m.Similarity,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("similarity")).Value))
                .ForMember(m => m.Type,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, GlossaryEntry>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForMember(m => m.Ref,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("ref")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, Definition>()
                .ForMember(m => m.Source,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("source")).Value))
                .ForMember(m => m.Text,
                    o => o.MapFrom(s =>
                        s.Text))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, Segment>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, Term>()
                .ForMember(m => m.Source,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("source")).Value))
                .ForMember(m => m.Text,
                    o => o.MapFrom(s =>
                        s.Text))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, Translation>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForMember(m => m.Text,
                    o => o.MapFrom(s =>
                        s.Text))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, ResourceData>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, ResourceItem>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, Source>()
                .ForMember(m => m.Href,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.EndsWith("href")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, PhElement>()
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<string, Iri>()
                .ConstructUsing(i => new Iri(i))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<XlfEventArgs, ScElement>()
                .ForMember(m => m.Id,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("id")).Value))
                .ForMember(m => m.DataRef,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("dataRefStart")).Value))
                .ForMember(m => m.Type,
                    o => o.MapFrom(s => s.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, EcElement>()
                .ForMember(m => m.DataRef,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.Equals("dataRef")).Value))
                .ForAllOtherMembers(m => m.Ignore());

            CreateMap<XlfEventArgs, SmElement>()
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
                .ForMember(m => m.ProvenanceRecordsRef,
                    o => o.MapFrom(s =>
                        s.Attributes.SingleOrDefault(a => a.Key.EndsWith("provenanceRecordsRef")).Value))
                .ForMember(m => m.Type,
                    o => o.MapFrom(s => s.Attributes.SingleOrDefault(a => a.Key.Equals("type")).Value))
                .ForAllOtherMembers(m => m.Ignore());
        }

        public override string ProfileName => "GeneralMappings";
    }
}