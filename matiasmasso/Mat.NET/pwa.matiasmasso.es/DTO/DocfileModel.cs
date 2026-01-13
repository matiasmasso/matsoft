using DTO.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DTO
{
    public class DocfileModel:IModel
    {
        public string? Hash { get; set; }

        public Guid Guid { get; set; } // fake to implement IModel
        public Media? Document { get; set; }
        public Media? Thumbnail { get; set; }

        //public Media.MimeCods StreamMime { get; set; } = Media.MimeCods.Pdf;
        //public Media.MimeCods ThumbnailMime { get; set; } = Media.MimeCods.Jpg;

        //[JsonIgnore] public Byte[]? Document { get; set; }
        //[JsonIgnore] public Byte[]? Thumbnail { get; set; }
        public string? Filename { get; set; }

        public string? Nom { get; set; }
        public DateTime? Fch { get; set; } = DateTime.Today;

        public int? Pags { get; set; }
        public long? Size { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? HRes { get; set; }
        public int? VRes { get; set; }
        public DateTime FchCreated { get; set; } = DateTime.Now;

        public const int THUMB_WIDTH = 350;
        public const int THUMB_HEIGHT = 400;
        public const string PDF2JPG_URL = @"https://www.matiasmasso.es/pdf/thumbnail";
        //public const string PDF2JPG_URL = @"https://localhost:44332/pdf/thumbnail"; 

        public enum Cods
        {
            notset,
            correspondencia, // 1 ok
            assentament, // 2

            hisenda, // 3 ok

            pdc, // 4

            old_selloutcsv, // 5 deprecated

            selloutexcel, // 6

            clidoc,
            free8,
            download, // 9

            logovectorial,
            incidenciadoc,
            cmr, // 12 a revisar

            pdcconfirm, // a revisar (obsolet des de 2017.09.28)

            praddoc, // 14 Deprecated

            prinsercio, // 15 Deprecated

            prordredecompra, // 16 Deprecated

            contracte, // 17 ok

            escriptura, // 18 ok
            extractebancari,
            artpromo,
            liniatelconsum,
            revistaportada, // 22

            flota,
            telmissatge,
            domiciliaciobancaria, // 25

            tutorialcertificat,
            tpaepubbook,
            dua,
            productdownload,
            statsellout, // 30

            alb,
            incidenciesexcel,
            ibanmandato,
            tarifaexcel,
            tarifacsv,
            openorders,
            purchaseorderexcel,
            llibrediari,
            llibremajor,
            maybornsalesexcel,
            transmisioalbarans,
            repcertretencio,
            jsonsalepoints,
            stocksexcel,
            rawdatalast12monthscsv,
            vehicle,
            rankingclients,
            mediaresource,
            rankingproducts,
            zipgallerydownloads,
            ibanmandatomanual,
            forecast,
            selloutperchannel,
            excelsalepoints,
            xmlsalepoints,
            skurefs,
            excelcustomerdeliveries3yearsdetail,
            mediaresourcethumbnail,
            mem,
            transportLabels,
            deliveryAttachment
        }
        public DocfileModel() { }
        public DocfileModel(string hash)
        {
            this.Hash = hash;
        }

        public string? ThumbnailUrl() => Hash == null ? null : Globals.ApiUrl("DocFile/thumbnail", CryptoHelper.UrlFriendlyBase64(Hash));

        public string? DownloadUrl() => Hash == null ? null : Globals.ApiUrl("DocFile", CryptoHelper.UrlFriendlyBase64(Hash));

        public string? ContentType() => Document?.ContentType(); // Media.ContentType((Media.MimeCods)StreamMime);
        public string? ThumbnailDataUrl() => Thumbnail?.DataUrl(); // new Media((DTO.Media.MimeCods)ThumbnailMime, Thumbnail).DataUrl();
        #region Features
        public string Features() => Features(MimeText(), PagesText(), DimensionsText(), WeightText());

        public string Features(params string?[] texts)
        {
            var cleanTexts = texts.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).ToArray();
            var retval = string.Join(' ', cleanTexts);
            return retval;
        }

        public string BuildFilename() => string.Format("{0}.{1}", Nom ?? "document", Document?.Mime.ToString() ?? "notSet"); // ((Media.MimeCods)StreamMime).ToString());

        public string? MimeText()
        {
            string? retval = null;
            var mime = Document?.Mime ?? Media.MimeCods.NotSet; // (Media.MimeCods)StreamMime;
            if (mime != Media.MimeCods.NotSet)
                retval = mime.ToString();
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
            if (Height + Width != 0)
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

        public string HashFilename() => $"file_{Hash}";
        public string HashThumbnailname() => $"thumbnail_{Hash}";

        public static string? CalcHash(byte[]? bytes) => bytes==null ? null : CryptoHelper.HashMD5(bytes);


        #endregion

        public bool Matches(string? searchterm)
        {
            return Nom?.Contains(searchterm ?? "") ?? false;
        }

        public override string ToString()
        {
            return Features();
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
