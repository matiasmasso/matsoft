namespace DTO
{
    public class DTOGravatar
    {
        //private string _DefaultImageCod = "mm";
        public string EmailAddress { get; set; }


        public static DTOGravatar Factory(string emailAddress)
        {
            DTOGravatar retval = new DTOGravatar();
            {
                var withBlock = retval;
                withBlock.EmailAddress = emailAddress;
            }
            return retval;
        }
    }
}
