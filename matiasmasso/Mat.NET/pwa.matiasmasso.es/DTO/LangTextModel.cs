using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DTO
{
    public class LangTextModel
    {
        public Guid? Guid { get; set; }
        public Srcs? Src { get; set; }

        public string? Esp { get; set; }
        public string? Cat { get; set; }
        public string? Eng { get; set; }
        public string? Por { get; set; }

        public DateTime? FchEsp { get; set; }
        public DateTime? FchCat { get; set; }
        public DateTime? FchEng { get; set; }
        public DateTime? FchPor { get; set; }

        public LangTextModel() { }
        public LangTextModel(Guid guid, Srcs src)
        {
            Guid = guid;
            Src = src;
        }
        public LangTextModel(Guid guid, Srcs src, string? esp=null, string? cat = null, string? eng = null, string? por = null)
        {
            Guid = guid;
            Src = src;
            Esp = esp;
            Cat = cat;
            Eng = eng;
            Por = por;
        }

        public LangItem Item(LangDTO lang)
        {
            return new LangItem
            {
                Guid = (Guid)Guid!,
                Src = (int)Src,
                Lang = lang,
                Text = Text(lang),
                FchCreated = FchCreated(lang)
            };
        }

        public bool HasValue() => !string.IsNullOrEmpty(Esp) || !string.IsNullOrEmpty(Cat) || !string.IsNullOrEmpty(Eng) || !string.IsNullOrEmpty(Por);


        public class LangItem
        {
            public Guid Guid { get; set; }
            public int? Src { get; set; }
            public string? Text { get; set; }
            public LangDTO? Lang { get; set; }
            public DateTime? FchCreated { get; set; }
        }

        public LangTextModel(string? esp, string? cat = null, string? eng = null, string? por = null)
        {
            Esp = esp;
            Cat = cat;
            Eng = eng;
            Por = por;
        }

        public enum Srcs : int
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

        public void Load(string? esp = "", string? cat = "", string? eng = "", string? por = "")
        {
            Esp = esp;
            Cat = cat;
            Eng = eng;
            Por = por;
        }

        public string? Text(LangDTO lang)
        {
            var retval = "";
            if (lang.IsPor())
                retval = Por;
            else if (lang.IsEng())
                retval = Eng;
            else if (lang.IsCat())
                retval = Cat;
            else
                retval = Esp;
            return retval;
        }

        public void SetText(LangDTO lang, string text)
        {
            if (lang.IsPor())
                Por = text;
            else if (lang.IsEng())
                Eng = text;
            else if (lang.IsCat())
                Cat = text;
            else if (lang.IsEsp())
                Esp = text;
        }

        public DateTime? FchCreated(LangDTO lang)
        {
            DateTime? retval = null;
            if (lang.IsPor())
                retval = FchPor;
            else if (lang.IsEng())
                retval = FchEng;
            else if (lang.IsCat())
                retval = FchCat;
            else
                retval = FchEsp;
            
            return retval;
        }

        public bool Contains(string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return true;
            else
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);

                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;

                if (Esp != null && searchTerms.All(x => compareInfo.IndexOf(Esp, x, options) >= 0))
                    return true;
                else if (Cat != null && searchTerms.All(x => compareInfo.IndexOf(Cat, x, options) >= 0))
                    return true;
                else if (Eng != null && searchTerms.All(x => compareInfo.IndexOf(Eng, x, options) >= 0))
                    return true;
                else if (Por != null && searchTerms.All(x => compareInfo.IndexOf(Por, x, options) >= 0))
                    return true;
                else
                    return false;
            }
        }

        public string Tradueix(LangDTO? lang) => lang?.Tradueix(Esp, Cat, Eng, Por) ?? Esp ?? "";
        public string Html(CacheDTO cache, LangDTO lang)
        {
            var retval = "";
            if (!string.IsNullOrEmpty(Esp))
            {
                string html = lang.Tradueix(Esp, Cat, Eng, Por).Html();
                retval = cache.ExpandPlugins(html, lang);

                //var lines = textWithNewlines.GetLines();
                //for (var i = 1; i < lines.Count(); i++)
                //{
                //    var line = lines[i];
                //    if (line.StartsWith("<td") ||
                //        line.StartsWith("<tr") ||
                //        line.StartsWith("</td") ||
                //        line.StartsWith("</tr") ||
                //        line.StartsWith("</table")) { }
                //    else
                //        lines[i - 1] += "<br/>";
                //}
                //var sb = new StringBuilder();
                //foreach (var line in lines)
                //{
                //    sb.AppendLine(line);
                //}
                //var src = sb.ToString();
                //retval = cache.ExpandPlugins(src, lang);

            }
            return retval;
        }

        public bool Matches(string searchTerm)
        {
            if (string.Equals(Esp, searchTerm, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else if (string.Equals(Cat, searchTerm, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else if (string.Equals(Eng, searchTerm, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else if (string.Equals(Por, searchTerm, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else
                return false;
        }

        /// <summary>
        /// return if Spanish language has been modified after saving foreign language
        /// </summary>
        /// <returns></returns>
        public bool IsOutdated()
        {
            bool retval = false;
            if (!string.IsNullOrEmpty(Esp))
            {
                if (IsOutdated(LangDTO.Cat())) retval = true;
                if (IsOutdated(LangDTO.Eng())) retval = true;
                if (IsOutdated(LangDTO.Por())) retval = true;
            }
            return retval;
        }

        public bool IsOutdated(LangDTO lang)
        {
            var retval = false;

            if (!string.IsNullOrEmpty(Text(lang)))
            {
                TimeSpan ts = (DateTime)FchEsp! - (DateTime)FchCreated(lang)!;
                if (ts.TotalMinutes > 10) retval = true;
            }
            return retval;
        }
        public override string ToString() => Esp ?? "";

    }
}
