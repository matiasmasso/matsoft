using System;
using System.Collections.Generic;

namespace DTO.Integracions.ElCorteIngles
{
    public class AlineamientoDisponibilidad : DTOBaseGuid
    {
        public DateTime Fch { get; set; }
        public string Text { get; set; }
        public List<Item> Items { get; set; }

        public AlineamientoDisponibilidad() : base()
        {
            Fch = DTO.GlobalVariables.Now();
            Items = new List<AlineamientoDisponibilidad.Item>();
        }

        public AlineamientoDisponibilidad(Guid guid) : base(guid)
        {
            Items = new List<Item>();
        }


        public class Item
        {
            public Guid SkuGuid { get; set; }
            public string SkuNom { get; set; }
            public string CategoryNom { get; set; }
            public string BrandNom { get; set; }
            public string Ean { get; set; }
            public string RefEci { get; set; }
            public int Stock { get; set; }
            public decimal Price { get; set; }
            public bool Obsoleto { get; set; }
            public string Uneco { get; set; }

            public string Line(DateTime fch)
            {
                List<String> fieldValues = new List<String>();
                fieldValues.Add("G"); //constant
                fieldValues.Add("0030825"); //supplier number
                fieldValues.Add(Ean); //product Ean number
                fieldValues.Add(""); //product custom ref precedit de l'Uneco; ECI prefereix que aquest camp es deixi buit.
                fieldValues.Add(Disponible());
                fieldValues.Add(Descatalogado());
                fieldValues.Add(Price.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                fieldValues.Add(fch.ToString("yyyyMMdd"));
                fieldValues.Add(fch.ToString("HHmm"));
                fieldValues.Add(Stock.ToString());
                fieldValues.Add(Uneco);
                string retval = string.Join(",", fieldValues);
                return retval;
            }

            public string Disponible()
            {
                return Stock > 0 ? "S" : "N";
            }
            public string Descatalogado()
            {
                return Obsoleto ? "S" : "N";
            }

            public bool Matches(string filter)
            {
                bool retval = true;
                string[] filters = filter.ToLower().Split('+');
                string fullTxt = String.Format("{0} {1} {2} {3} {4}", BrandNom, CategoryNom, SkuNom, RefEci, Ean).ToLower();
                foreach (string f in filters)
                {
                    if (!fullTxt.Contains(f))
                    {
                        retval = false;
                        break;
                    }
                }
                return retval;
            }

        }

    }

}
