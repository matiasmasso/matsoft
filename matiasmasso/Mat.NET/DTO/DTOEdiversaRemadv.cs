using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOEdiversaRemadv : DTOBaseGuid
    {
        public string DocNum { get; set; }
        public DateTime FchDoc { get; set; }
        public DateTime FchVto { get; set; }
        public string DocRef { get; set; }
        public DTOContact EmisorPago { get; set; }
        public DTOContact ReceptorPago { get; set; }
        public DTOAmt Amt { get; set; }
        public Guid Result { get; set; }
        public List<DTOEdiversaRemadvItem> Items { get; set; }
        public List<Exception> Exceptions { get; set; }

        public enum MediosDePago
        {
            Cheque = 20,
            Transferencia = 31,
            CtaBancaria = 42,
            Pagare = 60
        }

        public DTOEdiversaRemadv(Guid value) : base(value)
        {
            Items = new List<DTOEdiversaRemadvItem>();
        }

        public DTOEdiversaRemadv() : base()
        {
            Items = new List<DTOEdiversaRemadvItem>();
        }

        public bool Cuadra()
        {
            bool retval = true;
            foreach (DTOEdiversaRemadvItem oItem in Items)
            {
                if (oItem.Pnd == null)
                {
                    retval = false;
                    break;
                }
            }
            return retval;
        }
    }



    public class DTOEdiversaRemadvItem
    {
        public DTOEdiversaRemadv Parent { get; set; }
        public Types Type { get; set; }
        public string Nom { get; set; }
        public string Num { get; set; }
        public DateTime Fch { get; set; }
        public DTOAmt Amt { get; set; }
        public DTOPnd Pnd { get; set; }
        public int Idx { get; set; }

        public enum Types
        {
            Relacion_de_facturas = 49,
            Nota_de_credito = 83,
            Nota_de_debito = 84,
            Factura_Comercial = 380,
            Nota_de_abono = 381,
            Nota_de_cargo = 383,
            Factura_rectificativa = 384,
            Factura_consolidada = 385,
            Factura_de_anticipo = 386,
            Autofactura = 389
        }

        public DTOEdiversaRemadvItem(DTOEdiversaRemadv value) : base()
        {
            Parent = value;
        }
    }
}
