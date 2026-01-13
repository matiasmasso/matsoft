using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DocSrcModel : BaseGuid, IModel
    {
        public Guid? Parent { get; set; }
        public Guid? Repo { get; set; }
        public string? Nom { get; set; }
        public string? NomLlarg { get; set; }
        public int Ord { get; set; }
        public string? Url { get; set; }
        public string? Obs { get; set; }
        public DocfileModel? DocFile { get; set; }

        public DocSrcModel() : base() { }
        public DocSrcModel(Guid guid) : base(guid) { }

        public static string CollectionPageUrl() => Globals.PageUrl("DocSrcs");
        public string CreatePageUrl() => Globals.PageUrl("DocSrc");
        public string AddNewChildPageUrl() => Globals.PageUrl("DocSrc/fromParent", Guid.ToString());
        public string PropertyPageUrl() => Globals.PageUrl("DocSrc", Guid.ToString());
        public string Caption() => NomLlarg ?? Nom ?? "?"; //To implement iModel Interface for property grid selectors

        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom+" "+NomLlarg ?? "";
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;
                retval = searchTerms.All(x => compareInfo.IndexOf(searchTarget, x, options) >= 0);
            }
            return retval;
        }

        public int DocsCount(List<DocSrcModel> allDocSrcs, List<DocModel> allDocs) => DocsCount(this, allDocSrcs, allDocs);

        static int DocsCount(DocSrcModel item, List<DocSrcModel> allDocSrcs, List<DocModel> allDocs)
        {
            var retval = allDocs.Count(x => x.Src == item.Guid);
            foreach (var child in allDocSrcs.Where(x => x.Parent == item.Guid))
            {
                retval += DocsCount(child, allDocSrcs, allDocs);
            }
            return retval;
        }


        public List<DocModel> Docs(List<DocSrcModel>? allDocSrcs, List<DocModel>? allDocs) {
            List<DocModel> retval = new();
            if(allDocSrcs != null & allDocs != null)
            retval = Docs(this, allDocSrcs!, allDocs!)
                .OrderBy(x=>x.Fch)
                .ToList();
            return retval;
        }

        static List<DocModel> Docs(DocSrcModel item, List<DocSrcModel> allDocSrcs, List<DocModel> allDocs)
        {
            var retval = allDocs.Where(x => x.Src == item.Guid).ToList();
            foreach (var child in allDocSrcs.Where(x => x.Parent == item.Guid))
            {
                retval.AddRange( Docs(child, allDocSrcs, allDocs));
            }
            return retval;
        }

        public DocModel DocFactory()
        {
            var retval = DocModel.Factory();
            retval.Src = Guid;
            retval.Tit = $"(nou document de {NomLlarg})";
            return retval;
        }
        public override string ToString() => Caption();

    }
}
