using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOMenu : DTOBaseGuid
    {
        public DTOApp.AppTypes App { get; set; }
        public Cods Cod { get; set; }

        public DTOLangText Caption { get; set; }
        public DTOLangText LangUrl { get; set; }
        public string Ord { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public List<DTORol> Rols { get; set; }
        public DTOMenu Parent { get; set; }
        public List<DTOMenu> Items { get; set; }

        public enum Cods
        {
            NotSet,
            Queries,
            Forms,
            Profile,
            Customer,
            Rep,
            Comercial,
            Staff,
            Product
        }

        public DTOMenu() : base()
        {
            Caption = new DTOLangText(base.Guid, DTOLangText.Srcs.WebMenuItem);
            Rols = new List<DTORol>();
            Items = new List<DTOMenu>();
        }

        public DTOMenu(Guid oGuid) : base(oGuid)
        {
            Caption = new DTOLangText(base.Guid, DTOLangText.Srcs.WebMenuItem);
            Rols = new List<DTORol>();
            Items = new List<DTOMenu>();
        }

        public static DTOMenu Factory(DTOLangText caption, string url = "")
        {
            DTOMenu retval = new DTOMenu();
            retval.Caption = caption;
            retval.LangUrl = new DTOLangText(url);
            return retval;
        }

        public static DTOMenu Factory(string nom, string url = "") // TO DEPRECATE
        {
            DTOMenu retval = new DTOMenu();
            {
                var withBlock = retval;
                withBlock.Caption.Esp = nom;
                withBlock.LangUrl = new DTOLangText(url);
            }
            return retval;
        }

        public static DTOMenu Factory(DTOLangText nom, DTOLangText url)
        {
            DTOMenu retval = new DTOMenu();
            retval.Caption = nom;
            retval.LangUrl = url;
            return retval;
        }

        public DTOMenu AddChild(DTOLangText nom, string url)
        {
            var retval = DTOMenu.Factory(nom, url);
            Items.Add(retval);
            return retval;
        }

        public DTOMenu AddChild(DTOLangText nom, DTOLangText url)
        {
            var retval = DTOMenu.Factory(nom, url);
            Items.Add(retval);
            return retval;
        }


        //public static string SegmentUrl(DTOMenu oMenu, params string[] oParams)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append(oMenu.Url);
        //    foreach (string sParam in oParams)
        //        sb.Append("/" + sParam);
        //    string retval = sb.ToString();
        //    return retval;
        //}

        public override string ToString()
        {
            return string.Format("DTOMenu: {0}", Caption.Esp);
        }

        public class Collection : List<DTOMenu>
        {
            public DTOMenu Add(DTOLangText nom, DTOLangText url)
            {
                var retval = DTOMenu.Factory(nom, url);
                base.Add(retval);
                return retval;
            }
        }
    }

}