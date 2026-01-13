namespace DTO.Models.Compact
{
    public class Ean
    {
        public string Value { get; set; }
        public static Ean Factory(DTOEan value)
        {
            Ean retval = null;
            if (value != null)
            {
                retval = new Ean();
                retval.Value = value.Value;
            }
            return retval;
        }

        public override string ToString()
        {
            return string.Format("Ean {0}", Value);
        }
    }

}
