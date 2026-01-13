using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO
{
    public class Media
    {
        [JsonIgnore] public Byte[]? Data { get; set; }
        public Media.MimeCods Mime { get; set; }

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
            Svg
        }

        public Media() { }
        public Media(MimeCods mime, byte[]? data = null)
        {
            Data = data;
            Mime = mime;
        }

        public Media(string contentType, byte[]? data = null)
        {
            Data = data;
            Mime = string.IsNullOrEmpty(contentType) ? MimeCods.NotSet : MimeCodFromContentType(contentType);
        }

        private static readonly Dictionary<string, string> _map = new(StringComparer.OrdinalIgnoreCase)
    {
        // Images
        { "image/jpeg", ".jpg" },
        { "image/png", ".png" },
        { "image/gif", ".gif" },
        { "image/webp", ".webp" },
        { "image/bmp", ".bmp" },
        { "image/tiff", ".tif" },
        { "image/svg+xml", ".svg" },

        // Documents
        { "application/pdf", ".pdf" },
        { "application/msword", ".doc" },
        { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx" },
        { "application/vnd.ms-excel", ".xls" },
        { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ".xlsx" },
        { "application/vnd.ms-powerpoint", ".ppt" },
        { "application/vnd.openxmlformats-officedocument.presentationml.presentation", ".pptx" },
        { "text/plain", ".txt" },
        { "text/html", ".html" },
        { "application/json", ".json" },
        { "application/xml", ".xml" },

        // Archives
        { "application/zip", ".zip" },
        { "application/x-7z-compressed", ".7z" },
        { "application/x-rar-compressed", ".rar" },
        { "application/gzip", ".gz" },
        { "application/x-tar", ".tar" },

        // Audio
        { "audio/mpeg", ".mp3" },
        { "audio/wav", ".wav" },
        { "audio/ogg", ".ogg" },
        { "audio/flac", ".flac" },
        { "audio/aac", ".aac" },

        // Video
        { "video/mp4", ".mp4" },
        { "video/x-msvideo", ".avi" },
        { "video/x-matroska", ".mkv" },
        { "video/webm", ".webm" },
        { "video/quicktime", ".mov" },
        { "video/mpeg", ".mpeg" }
    };

        public static string GetExtension(string contentType)
            => _map.TryGetValue(contentType, out var ext) ? ext : string.Empty;


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
                case MimeCods.Mov:
                    retval = "video/quicktime";
                    break;
                case MimeCods.Mp4:
                    retval = "video/mp4";
                    break;
                default:
                    retval = "application/pdf";
                    break;
            }
            return retval;
        }

        public static MimeCods MimeCodFromContentType(string contentType)
        {
            MimeCods retval = MimeCods.NotSet;
            switch (contentType)
            {
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
            }
            return retval;
        }

        public string? DataUrl() => Data == null ? null: $"data:{ContentType()};base64,{Convert.ToBase64String(Data)}";

    }
}
