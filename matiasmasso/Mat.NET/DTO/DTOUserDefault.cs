namespace DTO
{
    public class DTOUserDefault
    {
        public DTOUser User { get; set; }
        public Cods Cod { get; set; }
        public string Value { get; set; }

        public enum Cods
        {
            _NotSet,
            ProductCategoriesOrder
        }

        public static DTOUserDefault Factory(DTOUser oUser, Cods oCod, string sValue)
        {
            DTOUserDefault retval = new DTOUserDefault();
            {
                var withBlock = retval;
                withBlock.User = oUser;
                withBlock.Cod = oCod;
                withBlock.Value = sValue;
            }
            return retval;
        }
    }
}
