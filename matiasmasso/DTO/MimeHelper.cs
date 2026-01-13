using System;
using System.Collections.Generic;
using System.Text;
using static DTO.Globals;

namespace DTO
{

    public class MimeHelper
    {

        //public enum Media.MimeCods
        //{
        //    NotSet,
        //    Jpg,
        //    Gif,
        //    Zip,
        //    Pdf,
        //    Xps,
        //    Xml,
        //    Xls,
        //    Xlsx,
        //    Mpg,
        //    Rtf,
        //    Eps,
        //    Ai,
        //    Wmv,
        //    Txt,
        //    Pla,
        //    Wav,
        //    Cer,
        //    Doc,
        //    Docx,
        //    Tif,
        //    Tiff,
        //    Bmp,
        //    Png,
        //    Csv,
        //    EPub,
        //    Ppt,
        //    Pptx,
        //    Mov,
        //    Mp4,
        //    Xlsm,
        //    _3Gp,
        //    Svg
        //}

        public static Media.MimeCods MimeCod(string? filename = null)
        {
            Media.MimeCods retval = Media.MimeCods.NotSet;
            if (filename != null)
            {
                var lastDot = filename.LastIndexOf('.');
                if (lastDot != -1)
                {
                    var extension = filename.Substring(lastDot + 1);
                    retval = extension.ToEnum<Media.MimeCods>();
                }
            }
            return retval;
        }
        public static string? ContentType(string? filename = null) => ContentType(MimeCod(filename));
        public static string ContentType(Media.MimeCods mime)
        {
            var retval=string.Empty;
            switch(mime)
            {
                case Media.MimeCods.Jpg:
                    retval = "image/jpeg";
                    break;
                case Media.MimeCods.Gif:
                    retval = "image/gif";
                    break;
                case Media.MimeCods.Png:
                    retval = "image/png";
                    break;
                case Media.MimeCods.Svg:
                    retval = "image/svg+xml";
                    break;
                case Media.MimeCods.Pdf:
                    retval = "application/pdf";
                    break;
                case Media.MimeCods.Doc:
                case Media.MimeCods.Docx:
                    retval = "application/msword";
                    break;
                case Media.MimeCods.Xlsx:
                    retval = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                default:
                    retval = "application/pdf";
                    break;
            }
            return retval;
        }
    }
}
