using DTO.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO
{
    public class DocfileModel
    {
        public string? Hash { get; set; }
        public string? Sha256 { get; set; }


        //public Media.MimeCods StreamMime { get; set; } = Media.MimeCods.Pdf;
        //public Media.MimeCods ThumbnailMime { get; set; } = Media.MimeCods.Jpg;

        public Media? Document { get; set; }
        public Media? Thumbnail { get; set; }
        public string? Filename { get; set; }

        public int? Pags { get; set; }
        public long? Size { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public DateTime? Fch { get; set; } = DateTime.Today;
        public DateTime FchCreated { get; set; }


        public const string PDF2JPG_URL = @"https://www.matiasmasso.es/pdf/thumbnail";
        //public const string PDF2JPG_URL = @"https://localhost:44332/pdf/thumbnail";
        public const int THUMB_WIDTH = 350;
        public const int THUMB_HEIGHT = 400;


        public bool HasValue() => !IsEmpty();
        public bool IsEmpty() => string.IsNullOrEmpty(Hash);
        public bool IsNew() => Document?.Data != null;
        public string? ThumbnailUrl()
        {
            string? retval = null;
            if(Hash!= null)
            {
                if(Thumbnail?.Data == null)
                    retval = Globals.ApiUrl("doc/thumbnail", Hash);
                else
                    retval = Thumbnail.DataUrl();
            }
            return retval;
        }

        public string? DownloadUrl()
        {
            string? retval = null;
            if (Document?.Data != null)
                retval = Document.DataUrl();
            else if (!string.IsNullOrEmpty(Hash))
                retval = Globals.ApiUrl("doc", Hash);
            return retval;
        }

        public string HashFilename() => $"file_{Hash}";
        public string HashThumbnailname() => $"thumbnail_{Hash}";

        public string Features()
        {
            var sb = new System.Text.StringBuilder();
            var mime = Document?.Mime ?? Media.MimeCods.NotSet;
            if (mime != Media.MimeCods.NotSet)
                sb.Append(mime.ToString() + " ");

            if (Pags == 1)
                sb.Append("1 pag. ");
            else if (Pags > 1)
                sb.AppendFormat("{0} pags. ", Pags);

            if (Size != null)
            {
                if (Size > 1000000000)
                    sb.AppendFormat("{0:N1} Gb ", Size / 1000000000);
                else if (Size > 1000000)
                    sb.AppendFormat("{0:N1} Mb ", Size / 1000000);
                else if (Size > 1000)
                    sb.AppendFormat("{0:N0} Kb ", Size / 1000);
                else
                    sb.AppendFormat("{0:N0} Bytes ", Size);
            }

            if(FchCreated > DateTime.MinValue)
                sb.AppendFormat(" {0:dd/MM/yy HH:mm} ", FchCreated);

            return sb.ToString().Trim();
        }


        /// <summary>
        /// for rasterizing
        /// </summary>
        public class Pdf
        {
            public int Pags { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string? ThumbnailDataUrl { get; set; }

            public Byte[]? ThumbnailBytes()
            {
                if (ThumbnailDataUrl != null)
                {
                    var matchGroups = Regex.Match(ThumbnailDataUrl, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
                    var base64Data = matchGroups["data"].Value;
                    var retval = Convert.FromBase64String(base64Data);
                    return retval;
                }
                else
                    return null;
            }
        }
    }
}
