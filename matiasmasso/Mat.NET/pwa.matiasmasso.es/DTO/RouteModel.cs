using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RouteModel : BaseGuid
    {
        public string? Segment { get; set; }
        public Srcs Src { get; set; }
        public LangDTO? Lang { get; set; }
        public enum Srcs
        {
            NotSet,
            Brand,
            Dept,
            Category,
            Sku,
            Content
        }

        public RouteModel() : base() { }
        public RouteModel(Guid guid) : base(guid) { }

        public override string ToString() => Segment ?? "route?";

        public class Collection : List<RouteModel>
        {
            public Collection() { }
            public Collection(List<RouteModel> routes)
            {
                base.AddRange(routes);
            }

            public LangTextModel? LangText(Guid? guid)
            {
                var routes = guid == null ? new() : base.FindAll(x => x.Guid == guid);
                return new LangTextModel
                {
                    Guid = guid,
                    Esp = routes.FirstOrDefault(x => x.Lang!.IsEsp())?.Segment,
                    Cat = routes.FirstOrDefault(x => x.Lang!.IsCat())?.Segment,
                    Eng = routes.FirstOrDefault(x => x.Lang!.IsEng())?.Segment,
                    Por = routes.FirstOrDefault(x => x.Lang!.IsPor())?.Segment
                };
            }

            public RouteModel AddItem(Srcs src, string langTag, string segment)
            {
                var retval = new RouteModel
                {
                    Src = src,
                    Lang = new LangDTO(langTag),
                    Segment = segment
                };
                base.Add(retval);
                return retval;
            }
        }
    }
}
