using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Media
    {
        [JsonIgnore]
        public Byte[]? Data { get; set; }
        public MimeCods Mime { get; set; }

        public enum MimeCods
        {
            NotSet,
            Jpg,
            Gif,
            Zip,
            Pdf,
            Xps,
            Xml,
            Xls,
            Xlsx,
            Mpg,
            Rtf,
            Eps,
            Ai,
            Wmv,
            Txt,
            Pla,
            Wav,
            Cer,
            Doc,
            Docx,
            Tif,
            Tiff,
            Bmp,
            Png,
            Csv,
            EPub,
            Ppt,
            Pptx,
            Mov,
            Mp4,
            Xlsm,
            _3Gp,
            Svg,
            Pfx
        }

        public Media() { }
        public Media(MimeCods mime, byte[]? data = null)
        {
            Data = data;
            Mime = mime;
        }
        public Media(string? contentType, byte[]? data = null)
        {
            Data = data;
            Mime = string.IsNullOrEmpty(contentType) ? MimeCods.NotSet : MimeCodFromContentType(contentType);
        }

        public static MimeCods MimeCod(string? filename = null)
        {
            MimeCods retval = MimeCods.NotSet;
            if (filename != null)
            {
                var lastDot = filename.LastIndexOf('.');
                if (lastDot != -1)
                {
                    var extension = filename.Substring(lastDot + 1);
                    retval = extension.ToEnum<MimeCods>();
                }
            }
            return retval;
        }

        public string ContentType() => Media.ContentType((MimeCods)Mime);
        public static string? ContentType(string? filename = null) => ContentType(MimeCod(filename));
        public static string ContentType(MimeCods mime)
        {
            var retval = string.Empty;
            switch (mime)
            {
                case MimeCods.Jpg:
                    retval = "image/jpeg";
                    break;
                case MimeCods.Gif:
                    retval = "image/gif";
                    break;
                case MimeCods.Png:
                    retval = "image/png";
                    break;
                case MimeCods.Svg:
                    retval = "image/svg+xml";
                    break;
                case MimeCods.Pdf:
                    retval = "application/pdf";
                    break;
                case MimeCods.Doc:
                case MimeCods.Docx:
                    retval = "application/msword";
                    break;
                case MimeCods.Pfx:
                    retval = "application/x-pkcs12";
                    break;
                case MimeCods.Mp4:
                    retval = "video/mp4";
                    break;
                case MimeCods.Mpg:
                    retval = "video/mpeg";
                    break;
                case MimeCods.Mov:
                    retval = "video/quicktime";
                    break;
                case MimeCods.Wmv:
                    retval = "video/x-ms-wmv";
                    break;
                case MimeCods._3Gp:
                    retval = "video/3gpp";
                    break;
                case MimeCods.Xlsx:
                    retval = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                default:
                    retval = "application/pdf";
                    break;
            }
            return retval;
        }

        public static MimeCods MimeCodFromContentType(string contentType)
        {
            //".mpg,.wmv,.mov,.mp4"
            MimeCods retval = MimeCods.NotSet;
            switch (contentType)
            {
                case "text/plain":
                    retval = MimeCods.Txt;
                    break;
                case "application/pdf":
                    retval = MimeCods.Pdf;
                    break;
                case "image/jpeg":
                case "image/jpg":
                    retval = MimeCods.Jpg;
                    break;
                case "image/png":
                    retval = MimeCods.Png;
                    break;
                case "video/mp4":
                    retval = MimeCods.Mp4;
                    break;
                case "video/quicktime":
                    retval = MimeCods.Mov;
                    break;
                case "image/bmp":
                    retval = MimeCods.Bmp;
                    break;
                case "image/gif":
                    retval = MimeCods.Gif;
                    break;
                case "image/tif":
                    retval = MimeCods.Tif;
                    break;
                case "image/tiff":
                    retval = MimeCods.Tiff;
                    break;
                case "video/mpeg":
                    retval = MimeCods.Mpg;
                    break;
                case "application/x-pkcs12":
                    retval = MimeCods.Pfx;
                    break;
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    retval = MimeCods.Xlsx;
                    break;
            }
            return retval;
        }

        public string? DataUrl() => Data == null ? null : $"data:{ContentType()};base64,{Convert.ToBase64String(Data)}";

        public MemoryStream? Stream() => Data == null ? null : new MemoryStream(Data);

        [SupportedOSPlatform("windows")]
        public System.Drawing.Image? Image()
        {
            System.Drawing.Image? retval = null;
            if(Data != null)
            {
                using (MemoryStream ms = new MemoryStream(Data))
                using (System.Drawing.Image? image = System.Drawing.Image.FromStream(ms, true, true))
                {
                    retval = (System.Drawing.Image?)image?.Clone();
                }
            }
            return retval;
        }
    }
}
