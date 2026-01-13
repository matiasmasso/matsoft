using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DTO
{
    public class AddressModel
    {
        public string? Text { get; set; }
        public ZipDTO? Zip { get; set; }

        public Guid? ZipGuid { get; set; }

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public Guid? Contact { get; set; }
        public Cods Cod { get; set; }

        public enum Cods
        {
            NotSet,
            Fiscal,
            Correspondencia,
            Entregas,
            FraConsumidor
        }

        public string FullNom(LangDTO? lang = null)
        {
            return string.Format("{0}, {1}",  Zip?.Location?.FullNom(lang ?? LangDTO.Default()), Text ?? "");

        }

        public static string LocationProvinceCountry(string locationNom, string? provinciaNom, string? countryIso)
        {
            var retval = locationNom;
            string? provincia = null;
            if (provinciaNom != null && locationNom != provinciaNom)
                provincia = provinciaNom;
            if(countryIso != null && countryIso != "ES")
            {
                if(provincia == null) 
                    retval = string.Format("{0} ({1})", locationNom, countryIso);
                else
                    retval = string.Format("{0} ({1}, {2})", locationNom, provincia, countryIso);
            }
            else if(provincia != null)
                retval = string.Format("{0} ({1})", locationNom, provincia);
            return retval;

        }

        public override string ToString() => FullNom();

    }

    
}
