using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTORecollida
    {
        public int Id { get; set; }
        public DateTime Fch { get; set; } = DateTime.MinValue;
        public DTODelivery Delivery { get; set; }
        public string OrigenNom { get; set; }
        public string OrigenAdr { get; set; }
        public DTOZip OrigenZip { get; set; }
        public string OrigenTel { get; set; }
        public string OrigenContact { get; set; }
        public string DestiNom { get; set; }
        public string DestiAdr { get; set; }
        public DTOZip DestiZip { get; set; }
        public string DestiTel { get; set; }
        public string DestiContact { get; set; }
        public int Bultos { get; set; }
        public EstatsDeLaMercancia EstatDeLaMercancia { get; set; } = EstatsDeLaMercancia.Desconegut;
        public string Motiu { get; set; }
        public Carrecs Carrec { get; set; }
        public Accions Accio { get; set; }
        public List<DTOQtySku> Items { get; set; }

        public enum EstatsDeLaMercancia
        {
            Desconegut,
            Bo,
            Malmes
        }

        public enum Carrecs
        {
            Indeterminat,
            Nostre,
            Transportista,
            Magatzem
        }

        public enum Accions
        {
            Per_determinar,
            Entrar_en_stock,
            Tramitar_assegurança
        }


        public static DTORecollida Factory(DTODelivery oDelivery)
        {
            DTORecollida oRecollida = new DTORecollida();
            {
                var withBlock = oRecollida;
                withBlock.Delivery = oDelivery;
                withBlock.Fch = DTO.GlobalVariables.Today();
                withBlock.OrigenNom = oDelivery.Customer.NomComercialOrDefault();
                withBlock.OrigenAdr = oDelivery.Customer.Address.Text;
                withBlock.OrigenZip = oDelivery.Customer.Address.Zip;
                withBlock.OrigenTel = oDelivery.Tel;
                withBlock.DestiNom = oDelivery.Mgz.NomComercialOrDefault();
                DTOAddress oAddress = oDelivery.Mgz.Address;
                withBlock.DestiAdr = oAddress.Text;
                withBlock.DestiZip = oAddress.Zip;
                withBlock.DestiTel = oDelivery.Mgz.Telefon;
                withBlock.Items = new List<DTOQtySku>();
                foreach (var item in oDelivery.Items)
                    withBlock.Items.Add(new DTOQtySku(item.Qty, item.Sku));
            }
            return oRecollida;
        }
    }
}
