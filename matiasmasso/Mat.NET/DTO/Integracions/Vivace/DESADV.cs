using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Integracions.Vivace
{
    public class DESADV : MatHelperStd.FlatFile
    {
        public enum FieldIds
        {
            Expedicion,
            Fch,
            Bultos,
            Bulto,
            Pedido,
            Articulo,
            Talla,
            Color,
            Cantidad,
            Cod_SSCC,
            Pedido_ECI,
            Origen,
            Vendedor,
            Destino_ECI,
            Entrega,
            Centro_destino,
            Depto_destino,
            Comprador,
            Tipo_embalaje,
            Descr_articulo,
            INSDES,
            Cargo //,
                  //BultosxPallet,
                  //DUN14,
                  //UdsxCaja
        }



        public static DESADV Factory(List<Exception> exs, string filename)
        {
            DESADV retval = new DESADV();

            List<int> fieldLengths = (new int[] { 20, 12, 5, 5, 20, 25, 25, 25, 5, 35, 10, 17, 17, 17, 17, 4, 5, 17, 15, 50, 20, 1 }).ToList();
            retval.LoadSegmentsFromFilename(filename, fieldLengths);
            return retval;
        }


        public List<string> EdiMessages()
        {
            List<DTOEdiDesadv> oEdiDesadvs = new List<DTOEdiDesadv>();
            string supplier = "8435316500005";
            string deliveryFrom = "8435316500005"; //camviar a Vivace'
            string buyer = "8435316508810";
            string expedicion = "FakeExpedicion";
            DTOEdiDesadv oEdiDesadv = null;
            bool firstTime = true;

            foreach (Segment segment in Segments)
            {
                if (expedicion != segment.FieldValue((int)FieldIds.Expedicion))
                {
                    if (firstTime)
                    {
                        firstTime = false;
                    }
                    else
                    {
                        string sFch = segment.FieldValue((int)FieldIds.Expedicion);
                        DateTime deliveryFch = new DateTime(sFch.Substring(0, 4).toInteger(), sFch.Substring(4, 2).toInteger(), sFch.Substring(6, 2).toInteger());
                        string deliveryNum = string.Format("{0:0000}{1:000000}", deliveryFch.Year, segment.FieldValue((int)FieldIds.Expedicion));
                        oEdiDesadv = DTOEdiDesadv.Factory(supplier, buyer, deliveryFrom, buyer, deliveryNum, deliveryFch);
                        oEdiDesadvs.Add(oEdiDesadv);
                    }
                }

                string ean13 = segment.FieldValue((int)FieldIds.Articulo);
                string description = segment.FieldValue((int)FieldIds.Descr_articulo); //Product description
                int qty = segment.FieldValue((int)FieldIds.Cantidad).toInteger();
                //oEdiDesadv.AddItem(ean13, description, qty);
            }

            List<string> retval = new List<string>();
            foreach (DTOEdiDesadv item in oEdiDesadvs)
            {
                retval.Add(item.EdiMessage());
            }
            return retval;
        }

    }
}


