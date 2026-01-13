using MatHelperStd;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;

namespace DTO
{
    public class DTOMediaResource : DTOBaseGuid
    {
        public const int THUMBWIDTH = 140;
        public const int THUMBHEIGHT = 140;
        public const int MAXNOMLENGTH = 80;
        public const string PATH = @"C:\Public\Matsoft\Recursos\";
        public const string VIRTUALPATH = "~/Recursos/";

        public string Hash { get; set; }
        public int Ord { get; set; }
        public MimeCods Mime { get; set; }
        public double Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HRes { get; set; }
        public int VRes { get; set; }
        public int Pags { get; set; }
        public string Filename { get; set; }
        public string Nom { get; set; }
        public Cods Cod { get; set; }
        public DTOLang Lang { get; set; }
        public DTOLang.Set LangSet { get; set; }
        public DTOLangText Description { get; set; }
        public List<Models.Base.GuidNom> Products { get; set; }
        public DateTime Fch { get; set; }

        public DTOUsrLog UsrLog { get; set; }

        public bool Obsolet { get; set; }

        [JsonIgnore] public byte[] Thumbnail { get; set; }
        [JsonIgnore] public byte[] Stream { get; set; }

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

        [JsonConstructor]
        public DTOMediaResource() : base()
        {
            Products = new List<Models.Base.GuidNom>();
            LangSet = new DTOLang.Set();
            Description = new DTOLangText(Guid, DTOLangText.Srcs.MediaResource );
            UsrLog = new DTOUsrLog();
        }

        //public DTOMediaResource(string hash) : base(hash)
        //{
        //    Products = new List<Models.Base.GuidNom>();
        //    LangSet = new DTOLang.Set();
        //    Description = new DTOLangText(Guid, DTOLangText.Srcs.MediaResource);
        //    UsrLog = new DTOUsrLog();
        //}
        public DTOMediaResource(Guid guid) : base(guid)
        {
            Products = new List<Models.Base.GuidNom>();
            LangSet = new DTOLang.Set();
            Description = new DTOLangText(Guid, DTOLangText.Srcs.MediaResource);
            UsrLog = new DTOUsrLog();
        }

        public DTOMediaResource(DTOUser user, Byte[] bytes) : base()
        {
            Stream = bytes;
            Length = bytes.Length;
            Hash = CryptoHelper.HashMD5(bytes);

            Products = new List<Models.Base.GuidNom>();
            LangSet = DTOLang.Set.Default();
            Description = new DTOLangText(Guid, DTOLangText.Srcs.MediaResource);
            UsrLog = DTOUsrLog.Factory(user);
            IsNew = true;
        }


        public Model ViewModel()
        {
            Model retval = new Model();
            //retval.Product = Product;
            retval.Filename = Filename;
            retval.Cod = Cod;
            retval.Lang = Lang;
            return retval;
        }

        public class Model
        {
            public Guid Guid { get; set; }
            public string Filename { get; set; }
            public string Nom { get; set; }
            public Cods Cod { get; set; }
            public DTOLang Lang { get; set; }
            public DTOProduct Product { get; set; }
        }


        public static string CodTitle(DTOMediaResource value, DTOLang oLang)
        {
            string retval = "";
            switch (value.Cod)
            {
                case DTOMediaResource.Cods.Product:
                    {
                        retval = oLang.Tradueix("Imágenes de producto", "Imatges de producte", "Product images");
                        break;
                    }

                case DTOMediaResource.Cods.Features:
                    {
                        retval = oLang.Tradueix("Imágenes de detalle", "Imatges de detall", "Features images");
                        break;
                    }

                case DTOMediaResource.Cods.LifeStyle:
                    {
                        retval = oLang.Tradueix("Imágenes lifestyle", "Imatges lifestyle", "Lifestyle images");
                        break;
                    }

                case DTOMediaResource.Cods.RedesSociales:
                    {
                        retval = oLang.Tradueix("Redes sociales", "Xarxes socials", "Social networks");
                        break;
                    }

                case DTOMediaResource.Cods.Banner:
                    {
                        retval = "Banners";
                        break;
                    }

                case DTOMediaResource.Cods.Logo:
                    {
                        retval = "Logos";
                        break;
                    }

                case DTOMediaResource.Cods.Video:
                    {
                        retval = "Vídeos";
                        break;
                    }
            }
            return retval;
        }

        public static string TargetFilename(DTOMediaResource oMediaResource)
        {
            string sName = CryptoHelper.UrlFriendlyBase64(oMediaResource.Hash);
            string sExtension = MimeHelper.GetExtensionFromMime(oMediaResource.Mime);
            string retval = string.Format("{0}{1}", sName, sExtension);
            return retval;
        }

        public static string HashFromFilename(string filename)
        {
            int iPos = filename.LastIndexOfAny(new char[] { '/', '\\' });
            if (iPos > 0)
                filename = filename.Substring(iPos + 1);
            iPos = filename.LastIndexOf(".");
            if (iPos > 0)
                filename = filename.Substring(0, iPos);
            string retval = CryptoHelper.FromUrFriendlyBase64(filename);
            return retval;
        }

        public static string FriendlyName(DTOMediaResource oMediaResource)
        {
            string retval = "";
            string sNom = oMediaResource.Nom;
            if (sNom == "")
                retval = string.Format("M+O recurso.{0}", oMediaResource.Mime.ToString());
            else
            {
                int iPos = sNom.LastIndexOf(".");
                if (iPos >= 0)
                {
                    string sExtension = sNom.Substring(iPos + 1);
                    if (sExtension.ToLower() == oMediaResource.Mime.ToString().ToLower())
                        retval = sNom;
                    else
                        retval = string.Format("{0}.{1}", sNom, oMediaResource.Mime.ToString());
                }
                else
                    retval = string.Format("{0}.{1}", sNom, oMediaResource.Mime.ToString());
            }
            return retval;
        }


        public bool UnEquals(DTOMediaResource oCandidate)
        {
            bool retval = !Equals(oCandidate);
            return retval;
        }

        public bool Equals(DTOMediaResource oCandidate)
        {
            bool retval = false;
            if (oCandidate != null)
                retval = Hash == oCandidate.Hash;
            return retval;
        }

        public string Features()
        {
            string retval = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (Mime != MimeCods.NotSet)
                sb.Append(Mime.ToString() + " ");
            if (Width > 0 & Height > 0)
                sb.Append(Width + "x" + Height + " ");
            if (Length > 0)
                sb.Append(FileSize());
            retval = sb.ToString();
            return retval;
        }

        public string FileSize()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (Length > 1000000000)
                sb.Append(string.Format("{0:#,##0.#} Gb", Length / 1000000000));
            else if (Length > 1000000)
                sb.Append(string.Format("{0:#,##0.#} Mb", Length / 1000000));
            else if (Length > 0)
                sb.Append(string.Format("{0:#,##0.#} Kb", Length));
            var retval = sb.ToString();
            return retval;
        }

        public class Collection : List<DTOMediaResource>
        {
        }

        public override string ToString()
        {
            return Nom ?? base.ToString();
        }
    }
}
