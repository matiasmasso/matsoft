using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Helpers;

namespace DTO
{
    public class MediaResourceModel:BaseGuid
    {
        public string Hash { get; set; }
        public string? Filename { get; set; }
        public LangTextModel Description { get; set; }
        public Media.MimeCods Mime { get; set; }
        public Cods Cod { get; set; }
        public LangDTO? Lang { get; set; }
        public LangDTO.Set LangSet { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public int HRes { get; set; }
        public int VRes { get; set; }
        public int Pags { get; set; }
        public int Ord { get; set; }

        public List<Guid> Targets { get; set; } = new();
        public bool Obsoleto { get; set; }
        public UsrLogModel UsrLog { get; set; }

        private const string SERVERFOLDER = $"C:\\Public\\Matsoft\\recursos";

        public enum Cods
        {
            NotSet,
            Product,
            Features,
            LifeStyle,
            Banner,
            RedesSociales,
            Logo,
            Video
        }

        public MediaResourceModel() { }
        public MediaResourceModel(Guid guid):base(guid) { }
        public MediaResourceModel(string hash) { 
            Hash = hash; }

        public string ServerPath() => string.Format("{0}\\{1}", SERVERFOLDER, ServerFilename());
        public string ServerFilename() => string.Format("{0}{1}", CryptoHelper.UrlFriendlyBase64(Hash), Extension());

        public string Extension()=>string.Format(".{0}", Mime.ToString());

       public string ThumbnailUrl() => Globals.ApiUrl("mediaresource/thumbnail" ,Filename ?? Guid.ToString());

        public string? DownloadUrl() => "mediaResource/download/" + Filename;

        public string LandingPageUrl()
        {
            var segment = Filename ?? Guid.ToString();
            return string.Format("/mediaResource/{0}", segment);
        }


        #region Features
        public string Features()
        {
            var texts = new List<string?>() { MimeText(), PagesText(), DimensionsText(), WeightText() };
            var cleanTexts = texts.Where(x => !string.IsNullOrEmpty(x)).Select(x=>x.Trim()).ToArray();
            var retval = string.Join(' ', cleanTexts);
            return retval;
        }

        public string? MimeText()
        {
            string? retval = null;
            var mime = (Media.MimeCods)Mime;
            if (mime != Media.MimeCods.NotSet)
                retval =mime.ToString();
            return retval;
        }

        public string? PagesText()
        {
            string? retval = null;
            if (Pags == 1)
                retval = ("1 pag.");
            else if (Pags > 1)
                retval = string.Format("{0:N0} pags.", Pags);
            return retval;
        }

        public string? DimensionsText()
        {
            string? retval = null;
            if(Height+Width != 0)
            retval = string.Format("{0:N0} x {1:N0} px", Width, Height);
            return retval;
        }
        public string WeightText()
        {
            string retval;
            if (Size > 1000000000)
                retval = string.Format("{0:N1} Gb ", Size / 1000000000);
            else if (Size > 1000000)
                retval = string.Format("{0:N1} Mb ", Size / 1000000);
            else if (Size > 1000)
                retval = string.Format("{0:N0} Kb ", Size / 1000);
            else
                retval = string.Format("{0:N0} Bytes ", Size);
            return retval;
        }

        #endregion

        public override string ToString()
        {
            return Filename ?? Hash;
        }

    }
}
