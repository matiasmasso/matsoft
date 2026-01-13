using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DocModel : BaseGuid, IModel
    {
        public string? Tit { get; set; }
        public string? Fch { get; set; }
        public Guid? Cod { get; set; }
        public Guid? Src { get; set; }
        public string? SrcDetail { get; set; }
        public string? ExternalUrl { get; set; }
        public string? Transcripcio { get; set; }
        public DocfileModel? DocFile { get; set; }
        public DateTime FchCreated { get; set; }
        public DateTime FchLastEdited { get; set; }


        public DocModel() : base() { }
        public DocModel(Guid guid) : base(guid) { }

        public static DocModel Factory() => new DocModel { Tit = "(nou document)", FchCreated = DateTime.Now };


    public static string CollectionPageUrl() => Globals.PageUrl("docs");
        public string CreatePageUrl() => Globals.PageUrl("doc");
        public static string CreatePageUrlFromDocSrc(DocSrcModel? fromDocSrc) => Globals.PageUrl("doc/fromSrc", fromDocSrc?.Guid.ToString() ?? "");
        public static string CreatePageUrlFromDocCod(DocCodModel? fromDocCod) => Globals.PageUrl("doc/fromCod", fromDocCod?.Guid.ToString() ?? "");
        public string PropertyPageUrl() => Globals.PageUrl("doc", Guid.ToString());
        public string ThumbnailUrl() => Globals.ApiUrl("doc/thumbnail", Guid.ToString());
        public string DownloadUrl() => Globals.ApiUrl("doc/media", Guid.ToString());
        public string Year() => Fch == null ? "" : Fch.Truncate(4);
        public string FullNom() => String.Format("{0} {1}", Year(), Tit);

        public bool BelongsToAncestor(List<Guid>? ancestors, List<DocTargetModel>? docTargets)
        {
            bool retval = false;
            if (ancestors != null && docTargets != null)
                retval = docTargets.Any(x => x.Doc == Guid && ancestors.Any(y => y == x.Target));
            return retval;
        }

        public string Caption() => FullNom(); //To implement iModel Interface for property grid selectors

        public bool IsTestament() => Cod == DocCodModel.Wellknown(DocCodModel.Wellknowns.Testament)?.Guid;
        public bool IsDispensaMatrimonial() => Cod == DocCodModel.Wellknown(DocCodModel.Wellknowns.DispensaMatrimonial)?.Guid;
        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Tit ?? "";
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;
                retval = searchTerms.All(x => compareInfo.IndexOf(searchTarget, x, options) >= 0);
            }
            return retval;
        }

        public override string ToString() => Caption();

    }
}
