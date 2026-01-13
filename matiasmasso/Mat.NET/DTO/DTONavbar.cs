using System.Collections.Generic;

namespace DTO
{
    public class DTONavbar : DTONavbarItem
    {
        public Formats Format { get; set; }

        public enum Formats
        {
            horizontal,
            vertical
        }

        public static DTONavbar Factory(DTONavbar.Formats oFormat, string sId = "")
        {
            DTONavbar retval = new DTONavbar();
            {
                var withBlock = retval;
                withBlock.Id = sId;
                withBlock.Format = oFormat;
                withBlock.Items = new List<DTONavbarItem>();
            }
            return retval;
        }

        public new string Html()
        {
            System.Text.StringBuilder navAttributes = new System.Text.StringBuilder();
            navAttributes.AppendLine(" class='" + this.Format.ToString() + "'");
            if (!string.IsNullOrEmpty(base.Id))
                navAttributes.AppendLine(" id='" + base.Id + "'");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<nav" + navAttributes.ToString() + ">");
            sb.Append("<ul>");
            foreach (DTONavbarItem oItem in base.Items)
                sb.Append(oItem.Html());
            sb.AppendLine("</ul>");
            sb.AppendLine("</nav>");
            string retval = sb.ToString();
            return retval;
        }
    }

    public class DTONavbarItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string NavigateTo { get; set; }
        public string Tooltip { get; set; }
        public bool Selected { get; set; }

        public List<DTONavbarItem> Items { get; set; }

        public DTONavbarItem() : base()
        {
            Items = new List<DTONavbarItem>();
        }

        public static DTONavbarItem Factory(string sTitle, string sNavigateTo = "#", bool BlSelected = false)
        {
            DTONavbarItem retval = new DTONavbarItem();
            {
                var withBlock = retval;
                withBlock.Title = sTitle;
                withBlock.NavigateTo = sNavigateTo;
                withBlock.Selected = BlSelected;
                withBlock.Items = new List<DTONavbarItem>();
            }
            return retval;
        }

        public DTONavbarItem AddItem(string sTitle, string sNavigateTo = "#", bool BlSelected = false)
        {
            var retval = DTONavbarItem.Factory(sTitle, sNavigateTo, BlSelected);
            Items.Add(retval);
            return retval;
        }

        public string Html()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<li");
            sb.Append(Selected ? " class='selected'" : "");
            sb.Append(string.IsNullOrEmpty(Id) ? "" : " id='" + Id + "'");
            sb.Append(">");
            sb.Append("<a href='" + NavigateTo + "' ");
            sb.Append(string.IsNullOrEmpty(Id) ? "" : "data-ref='" + Id + "'");
            sb.Append(">");
            sb.Append(Title);
            sb.Append("</a>");

            if (Items.Count > 0)
            {
                sb.Append("<ul>");
                foreach (DTONavbarItem oItem in Items)
                    sb.Append(oItem.Html());
                sb.AppendLine("</ul>");
            }
            sb.Append("</li>");
            string retval = sb.ToString();
            return retval;
        }
    }
}
