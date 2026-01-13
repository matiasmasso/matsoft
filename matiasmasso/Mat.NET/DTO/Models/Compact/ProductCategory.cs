using System;
using System.Collections.Generic;

namespace DTO.Models.Compact
{
    public class ProductCategory
    {
        public Guid Guid { get; set; }
        public Compact.LangText Nom { get; set; }
        public bool Obsoleto { get; set; }
        public List<ProductSku> Skus { get; set; }

        public static ProductCategory Factory(DTOProductCategory value, DTOLang lang)
        {
            ProductCategory retval = new ProductCategory();
            retval.Guid = value.Guid;
            retval.Nom = new LangText();
            retval.Nom.Esp = value.Nom.Tradueix(lang);
            retval.Obsoleto = value.obsoleto;
            retval.Skus = new List<ProductSku>();
            return retval;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Guid.ToString(), Nom.Esp);
        }


    }
}
