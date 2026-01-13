namespace DTO
{
    public class DTOValueText
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public static DTOValueText Factory(string sValue, string sText)
        {
            DTOValueText retval = new DTOValueText();
            {
                var withBlock = retval;
                withBlock.Value = sValue;
                withBlock.Text = sText;
            }
            return retval;
        }
    }
}
