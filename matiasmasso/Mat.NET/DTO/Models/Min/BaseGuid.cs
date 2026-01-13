namespace DTO.Models.Min
{
    public class BaseGuid : Minifiable
    {
        public static BaseGuid Factory(DTOBaseGuid value)
        {
            BaseGuid retval = new BaseGuid();
            retval.Add("0", value.Guid.ToString());
            return retval;
        }

    }
}
