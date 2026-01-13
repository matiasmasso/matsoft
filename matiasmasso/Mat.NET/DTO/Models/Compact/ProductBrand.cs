using System;
using System.Collections.Generic;

namespace DTO.Models.Compact
{
    public class ProductBrand
    {
        public Guid Guid { get; set; }
        public Compact.LangText Nom { get; set; }
        public bool Obsoleto { get; set; }
        public List<ProductCategory> Categories { get; set; }

        public static ProductBrand Factory(DTOProductBrand value, DTOLang lang)
        {
            ProductBrand retval = new ProductBrand();
            retval.Guid = value.Guid;
            retval.Nom = new LangText();
            retval.Nom.Esp = value.Nom.Tradueix(lang);
            retval.Obsoleto = value.obsoleto;
            retval.Categories = new List<ProductCategory>();
            return retval;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Guid.ToString(), Nom.Esp);
        }
    }
}
