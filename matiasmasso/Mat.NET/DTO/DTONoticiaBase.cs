using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTONoticiaBase : DTOContent
    {
        public string urlFriendlySegment { get; set; }

        public DateTime destacarFrom { get; set; }
        public DateTime destacarTo { get; set; }

        public bool professional { get; set; }
        [JsonIgnore]
        public Byte[] Image265x150 { get; set; }
        public DateTime fchLastEdited { get; set; }

        public List<string> keywords { get; set; }
        public List<DTOCategoriaDeNoticia> categorias { get; set; }
        public List<DTODistributionChannel> distributionChannels { get; set; }


        public DTONoticiaBase() // per serialitzador Json
    : base()
        {
        }

        public DTONoticiaBase(Srcs oSrc) : base()
        {
            base.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentTitle);
            base.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentExcerpt);
            base.Text = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentText);
            base.Src = oSrc;
            distributionChannels = new List<DTODistributionChannel>();
        }

        public DTONoticiaBase(Guid oGuid) : base(oGuid)
        {
            base.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentTitle);
            base.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentExcerpt);
            base.Text = new DTOLangText(base.Guid, DTOLangText.Srcs.ContentText);
            distributionChannels = new List<DTODistributionChannel>();
        }


        public new string ThumbnailUrl(bool AbsoluteUrl = false)
        {
            var retval = MmoUrl.ApiUrl("Noticia/Image265x150", base.Guid.ToString());
            return retval;
        }

    }
}
