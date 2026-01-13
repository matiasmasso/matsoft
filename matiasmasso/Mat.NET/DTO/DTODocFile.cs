using DocumentFormat.OpenXml.Wordprocessing;
using MatHelperStd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;

namespace DTO
{
    public class DTODocFile : iMedia
    {
        public class Compact
        {
            public string Hash { get; set; }

            public static Compact Factory(String hash)
            {
                Compact retval = new Compact();
                retval.Hash = hash;
                return retval;
            }
        }

        public string Hash { get; set; }
        public string Sha256 { get; set; }
        public MimeCods Mime { get; set; }
        public double Length { get; set; }
        public Size Size { get; set; } 
        public int HRes { get; set; }
        public int VRes { get; set; }
        public int Pags { get; set; }
        [JsonIgnore]
        public byte[] Thumbnail { get; set; }
        [JsonIgnore]
        public byte[] Stream { get; set; }
        public string Filename { get; set; }
        public DateTime Fch { get; set; }
        public string Nom { get; set; }
        public bool Obsolet { get; set; }
        public DateTime FchCreated { get; set; }
        public int LogCount { get; set; }

        public const int THUMB_WIDTH = 350;
        public const int THUMB_HEIGHT = 400;

        public enum Cods
        {
            notset,
            correspondencia // 1 ok
    ,
            assentament // 2
    ,
            hisenda // 3 ok
    ,
            pdc // 4
    ,
            old_selloutcsv // 5 deprecated
    ,
            selloutexcel // 6
    ,
            clidoc,
            free8,
            download // 9
    ,
            logovectorial,
            incidenciadoc,
            cmr // 12 a revisar
    ,
            pdcconfirm // a revisar (obsolet des de 2017.09.28)
    ,
            praddoc // 14 Deprecated
    ,
            prinsercio // 15 Deprecated
    ,
            prordredecompra // 16 Deprecated
    ,
            contracte // 17 ok
    ,
            escriptura // 18 ok
    ,
            extractebancari,
            artpromo,
            liniatelconsum,
            revistaportada // 22
    ,
            flota,
            telmissatge,
            domiciliaciobancaria // 25
    ,
            tutorialcertificat,
            tpaepubbook,
            dua,
            productdownload,
            statsellout // 30
    ,
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

        public DTODocFile() : base()
        {
        }

        public DTODocFile(string sHash) : base()
        {
            Hash = sHash;
        }


        public bool IsVideo()
        {
            return MimeHelper.IsVideo(this.Mime);
        }



        public string DownloadUrl(DTOWebDomain domain)
        {
            //return domain.Url("doc", ((int)DTODocFile.Cods.download).ToString(), CryptoHelper.StringToHexadecimal(Hash));
            return $"api.matiasmasso.es/doc/{Hash}";

        }

        public string DownloadUrl(bool AbsoluteUrl = false)
        {
            //return downloadUrl(Hash, AbsoluteUrl);
            return $"api.matiasmasso.es/doc/{Hash}";
        }

        public static string downloadUrl(string hash, bool AbsoluteUrl = false)
        {
            return $"api.matiasmasso.es/doc/{hash}";
            //string retval = "";
            //if (!string.IsNullOrEmpty(hash))
            //    retval = MmoUrl.Factory(AbsoluteUrl, "doc", ((int)DTODocFile.Cods.download).ToString(), CryptoHelper.StringToHexadecimal(hash));
            //return retval;
        }

        public string ThumbnailUrl(bool AbsoluteUrl = false)
        {
            return $"api.matiasmasso.es/doc/thumbnail/{Hash}";
            //string retval = MmoUrl.ApiUrl("docfile/thumbnail", CryptoHelper.UrlFriendlyBase64(this.Hash));
            //return retval;
        }


        public string Features(bool blShort = false)
        {
            return DTODocFile.Features(this, blShort);
        }

        public static string Features(iMedia oMedia, bool BlShort = false)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (oMedia.Mime)
            {
                case MimeCods.Zip:
                    {
                        sb.AppendFormat("{0} {1}", oMedia.Mime.ToString(), FeatureFileSize(oMedia));
                        break;
                    }

                case MimeCods.Jpg:
                case MimeCods.Gif:
                case MimeCods.Bmp:
                case MimeCods.Tif:
                case MimeCods.Tiff:
                case MimeCods.Png:
                    {
                        sb.AppendFormat("{0} {1} {2}", oMedia.Mime.ToString(), FeatureFileSize(oMedia), FeatureImgDimensions(oMedia));
                        break;
                    }

                case MimeCods.Pdf:
                case MimeCods.Xps:
                    {
                        sb.AppendFormat("{0} {1} {2} {3}", oMedia.Mime.ToString(), FeatureFileSize(oMedia), FeaturePags(oMedia), FeaturePagDimensions(oMedia));
                        break;
                    }

                case MimeCods.Xls:
                case MimeCods.Xlsx:
                case MimeCods.Csv:
                    {
                        sb.AppendFormat("Excel {0} {1} cols x {2} filas", FeatureFileSize(oMedia), oMedia.Size.Width, oMedia.Size.Height);
                        break;
                    }

                case MimeCods.Doc:
                case MimeCods.Docx:
                    {
                        sb.AppendFormat("Word {0}", FeatureFileSize(oMedia));
                        break;
                    }

                case MimeCods.Mpg:
                    {
                        sb.AppendFormat("Video MPG {0}", FeatureFileSize(oMedia));
                        break;
                    }

                case MimeCods.Wmv:
                    {
                        sb.AppendFormat("Wmv (video) {0}", FeatureFileSize(oMedia));
                        break;
                    }

                case MimeCods.Mp4:
                    {
                        sb.AppendFormat("Mp4 (video) {0}", FeatureFileSize(oMedia));
                        break;
                    }

                case MimeCods.Pla:
                case MimeCods.Txt:
                    {
                        sb.AppendFormat("Txt {0}", FeatureFileSize(oMedia));
                        break;
                    }

                case MimeCods.Wav:
                    {
                        sb.AppendFormat("Wav (audio) {0}", FeatureFileSize(oMedia));
                        break;
                    }

                case MimeCods.Cer:
                    {
                        sb.AppendFormat("Cer (certificat) {0}", FeatureFileSize(oMedia));
                        break;
                    }

                default:
                    {
                        if (oMedia.Mime != MimeCods.NotSet)
                            sb.Append(oMedia.Mime.ToString() + ' ');
                        if (oMedia.Length > 0)
                            sb.Append(FeatureFileSize(oMedia));
                        break;
                    }
            }

            if (oMedia is DTODocFile)
            {
                DTODocFile docfile = (DTODocFile)oMedia;
                if (docfile.FchCreated != DateTime.MinValue)
                    sb.AppendFormat(" {0:dd/MM/yy}", docfile.FchCreated);
            }


            string retval = sb.ToString();
            return retval;
        }

        public static string FeaturePags(iMedia oMedia)
        {
            string retval = "";
            switch (oMedia.Pags)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        retval = string.Format("1 pag.");
                        break;
                    }

                default:
                    {
                        retval = string.Format("{0} pags.", oMedia.Pags);
                        break;
                    }
            }
            return retval;
        }

        public static string FeaturePagDimensions(iMedia oMedia)
        {
            string retval = "";
            if (oMedia.Size.Width == 210 & oMedia.Size.Height == 297)
                retval = "DIN A4";
            else
                retval = string.Format("{0}x{1} mm", oMedia.Size.Width, oMedia.Size.Height);
            return retval;
        }

        public static string FeatureImgDimensions(iMedia oMedia)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("{0}x{1} px", oMedia.Size.Width, oMedia.Size.Height);
            if (oMedia.HRes != 0)
            {
                sb.AppendFormat(" {0}", oMedia.HRes);
                if (oMedia.VRes != oMedia.HRes)
                    sb.AppendFormat("x{0}", oMedia.VRes);
                sb.Append(" ppp");
            }
            var retval = sb.ToString();
            return retval;
        }

        public static string FeatureFchCreated(DTODocFile oDocfile)
        {
            return oDocfile.FchCreated.ToString("dd/MM/yy HH:mm");
        }

        public static string FeatureFileSize(iMedia oMedia)
        {
            return FeatureFileSize(oMedia.Length);
        }

        public static string FeatureFileSize(double dblBytes)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (dblBytes > 1000000000)
                sb.Append(String.Format("{0:#,##0.0} Gb", dblBytes / 1000000000));
            else if (dblBytes > 1000000)
                sb.Append(String.Format("{0:#,##0.0} Mb", dblBytes / 1000000));
            else if (dblBytes > 1000)
                sb.Append(String.Format("{0:0} Kb", dblBytes / 1000));
            else
                sb.Append(String.Format("{0:0} bytes", dblBytes));
            var retval = sb.ToString();
            return retval;
        }

        public List<string> FeaturesList()
        {
            List<string> retval = new List<string>();
            if (this.Length > 0) retval.Add(FeatureFileSize(this.Length));
            return retval;

        }

        /// <summary>
        /// Used to rasterize a Pdf on the website
        /// </summary>
        public class Pdf
        {
            public int Pags { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string ThumbnailDataUrl { get; set; }

        }
    }
}
