namespace DTO
{
    public class DTOBoxItem
    {
        public string Title { get; set; }
        public string Footer { get; set; }
        public string ImageUrl { get; set; }
        public string NavigateUrl { get; set; }

        public DTOBaseGuid Tag { get; set; }


        public static string Html(DTOBoxItem oBoxItem)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("\t" + "<div class='BoxItemContainer'>");
            sb.AppendLine("\t" + "\t" + "<div class='BoxItemTitle'>" + oBoxItem.Title + "</div>");
            sb.AppendLine("\t" + "\t" + "<div class='BoxItemImgContainer' >");
            sb.AppendLine("\t" + "\t" + "\t" + "<a href='" + oBoxItem.NavigateUrl + "'>");
            sb.AppendLine("\t" + "\t" + "\t" + "\t" + "<img src='" + oBoxItem.ImageUrl + "'/>");
            sb.AppendLine("\t" + "\t" + "\t" + "</a>");
            sb.AppendLine("\t" + "\t" + "</div>");
            sb.AppendLine("\t" + "\t" + "<div class='BoxItemFooter'>" + oBoxItem.Footer + "</div>");
            sb.AppendLine("\t" + "</div>");
            string retval = sb.ToString();
            return retval;
        }
    }
}
