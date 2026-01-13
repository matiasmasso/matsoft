using System;

namespace DTO
{
    public class DTOBrandArea
    {
        public Guid Guid { get; set; }
        public DTOProductBrand Brand { get; set; }
        public DTOArea Area { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }

        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        public static DTOBrandArea Factory(DTOProductBrand oBrand)
        {
            DTOBrandArea retval = null;
            if (oBrand != null)
            {
                retval = new DTOBrandArea();
                {
                    var withBlock = retval;
                    withBlock.Guid = System.Guid.NewGuid();
                    withBlock.IsNew = true;
                    withBlock.Brand = oBrand;
                    withBlock.FchFrom = DTO.GlobalVariables.Today();
                }
            }
            return retval;
        }
    }
}
