namespace DTO
{
    using MatHelperStd;
    using Newtonsoft.Json;
    
    using System;
    using System.Collections.Generic;

    public class DTOBaseHash
    {
        public string Hash { get; set; }
        public MimeCods Mime { get; set; }
        public double Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HRes { get; set; }
        public int VRes { get; set; }
        public int Pags { get; set; }
        [JsonIgnore]
        public byte[] Thumbnail { get; set; }
        [JsonIgnore]
        public byte[] Stream { get; set; }

        public DateTime Fch { get; set; }

        public DTOUsrLog UsrLog { get; set; }

        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        [JsonConstructor]
        public DTOBaseHash() : base()
        {
            UsrLog = new DTOUsrLog();
        }

        //public DTOBaseHash(DTOUser user, Byte[] bytes) : base()
        //{
        //    Stream = bytes;
        //    Length = bytes.Length;
        //    Hash = CryptoHelper.HashMD5(bytes);
        //    UsrLog = DTOUsrLog.Factory(user);
        //    IsNew = true;
        //}

        //public DTOBaseHash(string hash) : base()
        //{
        //    Hash = hash;
        //    UsrLog = new DTOUsrLog();
        //    IsNew = false;
        //}

        public static string Features(DTOBaseHash value, bool BlShort = false)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (value.Mime)
            {
                case MimeCods.Jpg:
                case MimeCods.Gif:
                case MimeCods.Bmp:
                case MimeCods.Tif:
                case MimeCods.Tiff:
                case MimeCods.Png:
                    {
                        sb.AppendFormat("{0} {1} {2}", value.Mime.ToString(), FeatureFileSize(value), FeatureImgDimensions(value));
                        break;
                    }

                case MimeCods.Pdf:
                case MimeCods.Xps:
                    {
                        sb.AppendFormat("{0} {1} {2} {3}", value.Mime.ToString(), FeatureFileSize(value), FeaturePags(value), FeaturePagDimensions(value));
                        break;
                    }

                case MimeCods.Xls:
                case MimeCods.Xlsx:
                case MimeCods.Csv:
                    {
                        sb.AppendFormat("Excel {0} {1} cols x {2} filas", FeatureFileSize(value), value.Width, value.Height);
                        break;
                    }

                case MimeCods.Doc:
                case MimeCods.Docx:
                    {
                        sb.AppendFormat("Word {0}", FeatureFileSize(value));
                        break;
                    }

                case MimeCods.Mpg:
                    {
                        sb.AppendFormat("Video MPG {0}", FeatureFileSize(value));
                        break;
                    }

                case MimeCods.Wmv:
                    {
                        sb.AppendFormat("Wmv (video) {0}", FeatureFileSize(value));
                        break;
                    }

                case MimeCods.Mp4:
                    {
                        sb.AppendFormat("Mp4 (video) {0}", FeatureFileSize(value));
                        break;
                    }

                case MimeCods.Pla:
                case MimeCods.Txt:
                    {
                        sb.AppendFormat("Txt {0}", FeatureFileSize(value));
                        break;
                    }

                case MimeCods.Wav:
                    {
                        sb.AppendFormat("Wav (audio) {0}", FeatureFileSize(value));
                        break;
                    }

                case MimeCods.Cer:
                    {
                        sb.AppendFormat("Cer (certificat) {0}", FeatureFileSize(value));
                        break;
                    }

                default:
                    {
                        if (value.Mime != MimeCods.NotSet)
                            sb.Append(value.Mime.ToString() + ' ');
                        if (value.Length > 0)
                            sb.Append(FeatureFileSize(value));
                        break;
                    }
            }

            //if (value is DTODocFile)
            //{
            //    DTODocFile docfile = (DTODocFile)value;
            //    if (docfile.FchCreated != DateTime.MinValue)
            //        sb.AppendFormat(" {0:dd/MM/yy}", docfile.FchCreated);
            //}


            string retval = sb.ToString();
            return retval;
        }

        public string Features(bool blShort = false)
        {
            return Features(this, blShort);
        }


        public static string FeaturePags(DTOBaseHash value)
        {
            string retval = "";
            switch (value.Pags)
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
                        retval = string.Format("{0} pags.", value.Pags);
                        break;
                    }
            }
            return retval;
        }

        public static string FeaturePagDimensions(DTOBaseHash value)
        {
            string retval = "";
            if (value.Width == 210 & value.Height == 297)
                retval = "DIN A4";
            else
                retval = string.Format("{0}x{1} mm", value.Width, value.Height);
            return retval;
        }

        public static string FeatureImgDimensions(DTOBaseHash value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("{0}x{1} px", value.Width, value.Height);
            if (value.HRes != 0)
            {
                sb.AppendFormat(" {0}", value.HRes);
                if (value.VRes != value.HRes)
                    sb.AppendFormat("x{0}", value.VRes);
                sb.Append(" ppp");
            }
            var retval = sb.ToString();
            return retval;
        }

        public static string FeatureFchCreated(DTODocFile oDocfile)
        {
            return oDocfile.FchCreated.ToString("dd/MM/yy HH:mm");
        }

        public static string FeatureFileSize(DTOBaseHash value)
        {
            return FeatureFileSize(value.Length);
        }

        public static string FeatureFileSize(double dblBytes)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (dblBytes > 1000000)
                sb.Append(String.Format("{0:#,##0.0} Gb", dblBytes / 1000000));
            else if (dblBytes > 1000)
                sb.Append(String.Format("{0:#,##0.0} Mb", dblBytes / 1000));
            else if (dblBytes > 0)
                sb.Append(String.Format("{0:0} Kb", dblBytes));
            else
                sb.Append(String.Format("{0:0} bytes", dblBytes * 1000));
            var retval = sb.ToString();
            return retval;
        }

        public List<string> FeaturesList()
        {
            List<string> retval = new List<string>();
            if (this.Length > 0) retval.Add(FeatureFileSize(this.Length));
            return retval;

        }
    }

}
