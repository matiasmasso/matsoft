using System;

namespace DTO
{
    public class DTOImportUrl
    {
        public String Url { get; set; }
        public Object Content { get; set; }
        public Cods Cod { get; set; }
        public enum Cods
        {
            None,
            AmazonSellerOrder
        }


    }
}
