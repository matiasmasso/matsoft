using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DTO
{
    public class DTOLangText : DTOBaseGuid
    {

        public Srcs Src { get; set; }
        public string Esp { get; set; } = "";
        public string Cat { get; set; } = "";
        public string Eng { get; set; } = "";
        public string Por { get; set; } = "";
        public Outdateds Outdated { get; set; } = Outdateds.NotSet;



        public enum Props
        {
            Esp,
            Cat,
            Eng,
            Por,
        }

        [Flags]
        public enum Outdateds
        {
            NotSet = 0,
            None = 1,
            Cat= 2,
            Eng = 4,
            Por = 8
        }
        public enum Srcs
        {
            notset,
            WebMenuGroup,
            WebMenuItem,
            WinMenuItem,
            Category,
            Sku,
            Noticia,
            SepaText,
            Filter,
            FilterItem,
            Dept,
            CnapShort,
            CnapLong,
            DeptContent,
            SubscriptionNom,
            SubscriptionDsc,
            ContentTitle,
            ContentExcerpt,
            ContentText,
            ContentUrl,
            BlogTitle,
            BlogExcerpt,
            BlogText,
            BlogUrl,
            IncentiuTitle,
            IncentiuExcerpt,
            IncentiuBases,
            SkuNomLlarg,
            ProductNom,
            ProductExcerpt,
            ProductText,
            ProductUrl,
            CondicioTitle,
            CondicioText,
            CondicioCapitolTitle,
            CondicioCapitolText,
            Cods,
            PurchaseOrderConceptShortcut,
            SeoTitle,
            MenuItem,
            StaffPos,
            Shop4momsUrl,
            Shop4momsText,
            Shop4momsProductExcerpt,
            Shop4momsProductText,
            MediaResource,
            YouTubeNom,
            YouTubeExcerpt,
            ClaimNom,
            ClaimDescription,
            PgcClass
        }

        public DTOLangText() : base()
        {
        }
        public DTOLangText(Guid guid, Srcs src, string sEsp = "", object oCat = null, object oEng = null, object oPor = null) : base()
        {
            base.Guid = guid;
            Src = src;
            Load(sEsp, oCat, oEng, oPor);
        }

        public DTOLangText(string sEsp = "", object oCat = null, object oEng = null, object oPor = null) : base()
        {
            Load(sEsp, oCat, oEng, oPor);
        }

        public static DTOLangText Factory(string esp = "", string cat = "", string eng = "", string por = null)
        {
            DTOLangText retval = new DTOLangText(esp, cat, eng, por);
            return retval;
        }

        public DTOLangText Clone()=> new DTOLangText(Guid, Src, Esp, Cat, Eng, Por);

        public void Load(object oEsp = null, object oCat = null, object oEng = null, object oPor = null)
        {
            string sEsp = (oEsp == null) ? "" : (String)oEsp;
            string sCat = (oCat == null) ? "" : (String)oCat;
            string sEng = (oEng == null) ? "" : (String)oEng;
            string sPor = (oPor == null) ? "" : (String)oPor;
            Esp = string.IsNullOrEmpty(sEsp) ? "" : sEsp;
            Cat = string.IsNullOrEmpty(sCat) ? "" : sCat;
            Eng = string.IsNullOrEmpty(sEng) ? "" : sEng;
            Por = string.IsNullOrEmpty(sPor) ? "" : sPor;
        }

        public void Clear()
        {
            Esp = "";
            Cat = "";
            Eng = "";
            Por = "";
        }


        public bool Contains(string searchTerm)
        {
            var searchTerms = searchTerm.Split('+');


            if (Esp != null && searchTerms.All(x => Esp.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0))
                return true;
            else if (Cat != null && searchTerms.All(x => Cat.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0))
                return true;
            else if (Eng != null && searchTerms.All(x => Eng.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0))
                return true;
            else if (Por != null && searchTerms.All(x => Por.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0))
                return true;
            else
                return false;
        }



        public string Text(DTOLang oLang)
        {
            string retval = "";
            switch (oLang.id)
            {
                case DTOLang.Ids.ESP:
                    {
                        retval = Esp;
                        break;
                    }

                case DTOLang.Ids.CAT:
                    {
                        retval = Cat;
                        break;
                    }

                case DTOLang.Ids.ENG:
                    {
                        retval = Eng;
                        break;
                    }

                case DTOLang.Ids.POR:
                    {
                        retval = Por;
                        break;
                    }
            }
            return retval;
        }

        public List<DTOLang> Langs()
        {
            List<DTOLang> retval = new List<DTOLang>();
            if (!string.IsNullOrEmpty(this.Esp)) retval.Add(DTOLang.ESP());
            if (!string.IsNullOrEmpty(this.Cat)) retval.Add(DTOLang.CAT());
            if (!string.IsNullOrEmpty(this.Eng)) retval.Add(DTOLang.ENG());
            if (!string.IsNullOrEmpty(this.Por)) retval.Add(DTOLang.POR());
            return retval;
        }

        public void SetText(DTOLang oLang, string text)
        {
            if (oLang == null)
                Esp = text;
            else
            {
                switch (oLang.id)
                {
                    case DTOLang.Ids.ESP:
                        {
                            Esp = text;
                            break;
                        }

                    case DTOLang.Ids.CAT:
                        {
                            Cat = text;
                            break;
                        }

                    case DTOLang.Ids.ENG:
                        {
                            Eng = text;
                            break;
                        }

                    case DTOLang.Ids.POR:
                        {
                            Por = text;
                            break;
                        }
                }
            }
        }

        public string Tradueix(DTOLang oLang)
        {
            if (oLang == null)
                oLang = DTOLang.ESP();
            string retval = oLang.Tradueix(Esp, Cat, Eng, Por);
            return retval;
        }

        public string Html(DTOLang lang)
        {
            return MatHelperStd.TextHelper.Html(Tradueix(lang));
        }

        public bool isEmpty()
        {
            bool retval = false;
            if (Esp == "" && Cat == "" && Eng == "" && Por == "")
                retval = true;
            return retval;
        }

        public bool HasContent(DTOWebDomain domain)
        {
            bool retval = false;
            switch (domain.Id)
            {
                case DTOWebDomain.Ids.matiasmasso_es:
                    retval = !string.IsNullOrEmpty(this.Esp);
                    break;
                case DTOWebDomain.Ids.matiasmasso_pt:
                    retval = !string.IsNullOrEmpty(this.Por);
                    break;
            }
            return retval;
        }

        public bool IsMultiLang()
        {
            bool retval = (!string.IsNullOrEmpty(Cat + Eng + Por));
            return retval;
        }


        public static void SetText(DTOLangText oLangText, DTOLang oLang, string text)
        {
            if (oLangText == null)
                oLangText = new DTOLangText();
            switch (oLang.id)
            {
                case DTOLang.Ids.ESP:
                    {
                        oLangText.Esp = text;
                        break;
                    }

                case DTOLang.Ids.CAT:
                    {
                        oLangText.Cat = text;
                        break;
                    }

                case DTOLang.Ids.ENG:
                    {
                        oLangText.Eng = text;
                        break;
                    }

                case DTOLang.Ids.POR:
                    {
                        oLangText.Por = text;
                        break;
                    }
            }
        }

        public static DTOLangText Replace(DTOLangText olangText, string SearchString, string ReplacementString)
        {
            DTOLangText retval = new DTOLangText();
            {
                var withBlock = retval;
                if (olangText.Esp.isNotEmpty())
                {
                    retval.Esp = olangText.Esp.Replace(SearchString, ReplacementString);
                }
                if (olangText.Cat.isNotEmpty())
                {
                    retval.Cat = olangText.Cat.Replace(SearchString, ReplacementString);
                }
                if (olangText.Eng.isNotEmpty())
                {
                    retval.Eng = olangText.Eng.Replace(SearchString, ReplacementString);
                }
                if (olangText.Por.isNotEmpty())
                {
                    retval.Por = olangText.Por.Replace(SearchString, ReplacementString);
                }
            }
            return retval;
        }

        public static DTOLangText RemoveAccents(DTOLangText oLangText)
        {
            // the normalization to FormD splits accented letters in accents+letters
            DTOLangText retval = new DTOLangText();
            {
                var withBlock = retval;
                if (oLangText.Esp.isNotEmpty()) { retval.Esp = MatHelperStd.TextHelper.RemoveAccents(oLangText.Esp); };
                if (oLangText.Cat.isNotEmpty()) { retval.Cat = MatHelperStd.TextHelper.RemoveAccents(oLangText.Cat); };
                if (oLangText.Eng.isNotEmpty()) { retval.Eng = MatHelperStd.TextHelper.RemoveAccents(oLangText.Eng); };
                if (oLangText.Por.isNotEmpty()) { retval.Por = MatHelperStd.TextHelper.RemoveAccents(oLangText.Por); };
            }
            return retval;
        }

        public string ToMultiline()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(Esp);
            if (Cat.isNotEmpty()) { sb.AppendLine(Cat); }
            if (Eng.isNotEmpty()) { sb.AppendLine(Eng); }
            if (Por.isNotEmpty()) { sb.AppendLine(Por); }
            return sb.ToString();
        }

        public DTOLangText Urify()
        {
            DTOLangText retval = new DTOLangText(this.Esp.Urlfy(), this.Cat.Urlfy(), this.Eng.Urlfy(), this.Por.Urlfy());
            return retval;
        }

        public bool Matches(string src)
        {
            bool retval = false;
            if (src.isNotEmpty())
            {
                string[] array = new string[] { this.Esp, this.Cat, this.Eng, this.Por };
                //retval = array.Any(x => x.Equals(src, StringComparison.InvariantCultureIgnoreCase));
                retval = array.Any(x => String.Compare(x, src, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0);

            }
            return retval;
        }




        public bool Equals(DTOLangText src)
        {
            bool retval = false;
            if (src != null)
            {
                retval = Guid.Equals(src.Guid) & Src == src.Src & Esp == src.Esp & Cat == src.Cat & Eng == src.Eng & Por == src.Por;
            }
            return retval;
        }

        public class Collection : List<DTOLangText>
        {

        }


        public class Compact
        {
            public string Esp { get; set; } = "";

            public static Compact Factory(DTOLang lang, Object esp = null, Object cat = null, Object eng = null, Object por = null)
            {
                Compact retval = new Compact();
                string sEsp = (esp == null) ? "" : esp.ToString();
                string sCat = (cat == null) ? "" : cat.ToString();
                string sEng = (eng == null) ? "" : eng.ToString();
                string sPor = (por == null) ? "" : por.ToString();
                retval.Esp = lang.Tradueix(sEsp, sCat, sEng, sPor);
                return retval;
            }

            public DTOLangText toLangText()
            {
                DTOLangText retval = new DTOLangText(Esp);
                return retval;
            }
        }

        //minify object to increase network speed
        public Dictionary<string, Object> Minified()
        {
            Dictionary<string, Object> retval = new Dictionary<string, Object>();
            retval.Add(((int)Props.Esp).ToString(), Esp);
            if (!string.IsNullOrEmpty(Cat) & Cat != Esp)
                retval.Add(((int)Props.Cat).ToString(), Cat);
            if (!string.IsNullOrEmpty(Eng) & Eng != Esp)
                retval.Add(((int)Props.Eng).ToString(), Eng);
            if (!string.IsNullOrEmpty(Por) & Por != Esp)
                retval.Add(((int)Props.Por).ToString(), Por);
            return retval;
        }

        //return original object from minified version
        public static DTOLangText Expand(Object jObject)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

            var retval = new DTOLangText();
            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                var prop = (Props)x.Key.toInteger();
                if (prop == Props.Esp)
                    retval.Esp = x.Value.ToString();
                else if (prop == Props.Cat)
                    retval.Cat = x.Value.ToString();
                else if (prop == Props.Eng)
                    retval.Eng = x.Value.ToString();
                else if (prop == Props.Por)
                    retval.Por = x.Value.ToString();
            }

            return retval;
        }

        public static DTOLangText Concatenate(params DTOLangText[] items)
        {
            var retval = new DTOLangText();
            foreach (DTOLangText item in items)
            {
                retval.Esp += item.Tradueix(DTOLang.ESP());
                retval.Cat += item.Tradueix(DTOLang.CAT());
                retval.Eng += item.Tradueix(DTOLang.ENG());
                retval.Por += item.Tradueix(DTOLang.POR());
            }
            return retval;
        }

        public bool IsOutdated()
        {
            bool retval = false;
            if (!string.IsNullOrEmpty(Cat))
                retval = Outdated.HasFlag(Outdateds.Cat);
            else if (!string.IsNullOrEmpty(Eng))
                retval = Outdated.HasFlag(Outdateds.Eng);
            else if (!string.IsNullOrEmpty(Por))
                retval = Outdated.HasFlag(Outdateds.Por);
            return retval;
        }
    }

}
