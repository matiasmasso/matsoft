using System;

namespace DTO.Models.Compact
{
    public class ProductSku
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public Ean Ean13 { get; set; }
        public Compact.LangText Nom { get; set; }
        public Compact.LangText NomLlarg { get; set; }
        public string RefProveidor { get; set; }
        public bool LastProduction { get; set; }
        public Amt Rrpp { get; set; }
        public int Stock { get; set; }
        public int Clients { get; set; } // totes les unitats pendents de servir a clients
        public int ClientsAlPot { get; set; } // unitats de comandes en standby a la espera indefinida de confirmació
        public int ClientsEnProgramacio { get; set; } // unitats en programació a mes de una setmana vista
        public int ClientsBlockStock { get; set; } // unitats de comandes amb stock reservat
        public int Proveidors { get; set; }
        public int Previsions { get; set; }
        public bool ImageExists { get; set; }
        public bool Obsoleto { get; set; }

        public static ProductSku Factory(DTOProductSku value, DTOLang lang)
        {
            ProductSku retval = new ProductSku();
            retval.Guid = value.Guid;
            retval.Id = value.Id;
            retval.RefProveidor = value.RefProveidor;
            retval.Ean13 = Ean.Factory(value.Ean13);
            if (value.Nom != null)
            {
                retval.Nom = new LangText();
                retval.Nom.Esp = value.Nom.Tradueix(lang);
            }
            if (value.NomLlarg != null)
            {
                retval.NomLlarg = new LangText();
                retval.NomLlarg.Esp = value.NomLlarg.Tradueix(lang);
            }
            retval.LastProduction = value.LastProduction;
            retval.Rrpp = Amt.Factory(value.Rrpp);
            retval.Stock = value.Stock;
            retval.Clients = value.Clients; // totes les unitats pendents de servir a clients
            retval.ClientsAlPot = value.ClientsAlPot; // unitats de comandes en standby a la espera indefinida de confirmació
            retval.ClientsEnProgramacio = value.ClientsEnProgramacio; // unitats en programació a mes de una setmana vista
            retval.ClientsBlockStock = value.ClientsBlockStock; // unitats de comandes amb stock reservat
            retval.Proveidors = value.Proveidors;
            retval.Previsions = value.Previsions;
            retval.ImageExists = value.ImageExists;
            retval.Obsoleto = value.obsoleto;
            return retval;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Guid.ToString(), Id, NomLlarg.Esp);
        }

    }
}
