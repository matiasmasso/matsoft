using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DTO
{
    public class DTOImportacio : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public int Id { get; set; }
        public int Yea { get; set; }
        public DTOProveidor Proveidor { get; set; }
        public DTOContact PlataformaDeCarga { get; set; }
        public DateTime FchETD { get; set; } = DateTime.MinValue;
        public DateTime FchETA { get; set; } = DateTime.MinValue;
        public DateTime FchAvisTrp { get; set; }
        public DTOTransportista Transportista { get; set; }
        public int Bultos { get; set; }
        public decimal Kg { get; set; }
        public decimal M3 { get; set; }
        public string Obs { get; set; }
        public DTOAmt Amt { get; set; }
        public decimal Goods { get; set; }
        public DTOIncoterm IncoTerm { get; set; }
        public DTOCountry CountryOrigen { get; set; }
        public string Matricula { get; set; }
        public int Week { get; set; }
        public bool Arrived { get; set; }
        public bool Disabled { get; set; }
        public bool Exists { get; set; }


        public List<DTOImportacioItem> Items { get; set; }

        public DTOAmt TrpCost { get; set; }

        public List<DTOImportPrevisio> Previsions { get; set; }
        public List<DTOImportValidacio> Validacions { get; set; }

        public List<DTODelivery> Deliveries { get; set; }

        public DTOImportacio() : base()
        {
            Previsions = new List<DTOImportPrevisio>();
        }

        public DTOImportacio(Guid oGuid) : base(oGuid)
        {
            Previsions = new List<DTOImportPrevisio>();
        }

        public static DTOImportacio Factory(DTOEmp oEmp, DTOProveidor oProveidor = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOImportacio retval = new DTOImportacio();
            List<Exception> exs = new List<Exception>();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                withBlock.FchETD = DTO.GlobalVariables.Today();
                withBlock.FchETA = DTO.GlobalVariables.Today();
                withBlock.Yea = DTO.GlobalVariables.Today().Year;
                withBlock.Week = DTOImportacio.AvailableWeek(withBlock.FchETA);
                withBlock.Amt = DTOAmt.Empty();
                if (oProveidor != null)
                {
                    withBlock.Proveidor = oProveidor;
                    withBlock.IncoTerm = oProveidor.IncoTerm;
                    withBlock.CountryOrigen = DTOAddress.Country(oProveidor.Address);
                }
            }
            return retval;
        }

        public string FormattedId() => FormattedId(this);

        public static string FormattedId(DTOImportacio oimportacio) => string.Format("{0:0000}{1:0000}", oimportacio.Yea, oimportacio.Id);

        public static DTOContact PlataformaDeCargaOProveidor(DTOImportacio oImportacio)
        {
            var retval = oImportacio.PlataformaDeCarga;
            if (retval == null)
                retval = oImportacio.Proveidor;
            return retval;
        }

        public static DTOAmt CostMercancia(DTOImportacio oImportacio)
        {
            var retval = DTOAmt.Empty();
            foreach (DTOImportacioItem item in oImportacio.Items)
            {
                if (item.SrcCod == DTOImportacioItem.SourceCodes.fra)
                    retval.Add(item.Amt);
            }
            return retval;
        }

        public static DTOAmt CostTransport(DTOImportacio oImportacio)
        {
            var retval = DTOAmt.Empty();
            foreach (DTOImportacioItem item in oImportacio.Items)
            {
                if (item.SrcCod == DTOImportacioItem.SourceCodes.fraTrp)
                    retval.Add(item.Amt);
            }
            return retval;
        }

        public static int AvailableWeek(DateTime DtArrivalFch)
        {
            DayOfWeek oWeekday = DtArrivalFch.DayOfWeek;
            int retval = DtArrivalFch.weekOfYear();
            if ((int)oWeekday > 3)
                retval += 1;
            return retval;
        }

        public static MatHelper.Excel.Sheet Excel(List<DTOImportacio> oImportacions, DTOLang oLang)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Remeses de Importacio");
            {
                var withBlock = retval;
                withBlock.AddColumn(oLang.Tradueix("remesa", "remesa", "Id"));
                withBlock.AddColumn("ETD", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("ETA", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn(oLang.Tradueix("valor", "valor", "value"), MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn(oLang.Tradueix("bultos", "bultos", "packages"));
                withBlock.AddColumn(oLang.Tradueix("peso", "pes", "weight"), MatHelper.Excel.Cell.NumberFormats.Kg);
                withBlock.AddColumn(oLang.Tradueix("volumen", "volum", "volume"), MatHelper.Excel.Cell.NumberFormats.m3D2);
                withBlock.AddColumn(oLang.Tradueix("transporte", "transport", "transport"), MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn(oLang.Tradueix("matricula", "matricula", "plate"), MatHelper.Excel.Cell.NumberFormats.Euro);
            }
            foreach (var item in oImportacions)
            {
                DTOAmt oCostMercancia = DTOImportacio.CostMercancia(item);
                DTOAmt oCostTransport = DTOImportacio.CostTransport(item);

                MatHelper.Excel.Row oRow = retval.AddRow();
                {
                    var withBlock = oRow;
                    withBlock.AddCell(item.Id);
                    withBlock.AddCell(item.FchETD);
                    withBlock.AddCell(item.FchETA);
                    withBlock.AddCell(oCostMercancia.Eur);
                    withBlock.AddCell(item.Bultos);
                    withBlock.AddCell(item.Kg);
                    withBlock.AddCell(item.M3);
                    withBlock.AddCell(oCostTransport.Eur);
                    withBlock.AddCell(item.Matricula);
                }
            }

            return retval;
        }

        public void restoreObjects()
        {
            if (Items != null)
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    DTOImportacioItem item = Items[i];
                    item.Parent = this;
                    if (item.Tag != null)
                    {
                        JObject jTag = item.Tag as JObject;
                        switch (item.SrcCod)
                        {
                            case DTOImportacioItem.SourceCodes.alb:
                                item.Tag = jTag.ToObject<DTODelivery>();
                                break;
                            case DTOImportacioItem.SourceCodes.fra:
                            case DTOImportacioItem.SourceCodes.fraTrp:
                                item.Tag = jTag.ToObject<DTOCca>();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public class Confirmation
        {
            public DTOUser User { get; set; }
            public DTOImportacio Importacio { get; set; }
            public List<DTOInvoiceReceived.Item> Items { get; set; }

            public static Confirmation Factory(List<Exception> exs, XDocument doc, DTOUser user)
            {
                Confirmation retval = new Confirmation();
                retval.User = user;
                if (doc.Root.Attribute("REMESA") == null)
                    exs.Add(new Exception("falta l'identificador de remesa de importacio al fitxer"));
                else
                {
                    //Guid importacioGuid = null;
                    if (Guid.TryParse(doc.Root.Attribute("REMESA").Value, out var importacioGuid))
                    {
                        retval.Importacio = new DTOImportacio(importacioGuid);
                        IEnumerable<XElement> xItems =
                              (from itm in doc.Descendants("ITEM")
                               select (XElement)itm).ToList();
                        retval.Items = new List<DTOInvoiceReceived.Item>();
                        foreach (XElement xItem in xItems)
                        {
                            DTOInvoiceReceived.Item item = new DTOInvoiceReceived.Item();
                            XAttribute oLin = xItem.Attribute("LIN");
                            if (oLin == null)
                                exs.Add(new Exception(string.Format("falta segment LIN a la linia {0}", xItems.ToList().IndexOf(xItem))));
                            else
                            {
                                string sLin = oLin.Value;
                                Guid oGuid;

                                if (string.IsNullOrEmpty(sLin))
                                    exs.Add(new Exception(string.Format("falta identificador LIN a la linia {0}", xItems.ToList().IndexOf(xItem))));
                                else if (Guid.TryParse(sLin, out oGuid))
                                {
                                    item.Guid = Guid.Parse(xItem.Attribute("LIN").Value);
                                    XAttribute confirmed = xItem.Attribute("CONFIRMADO");
                                    if (confirmed == null)
                                        exs.Add(new Exception("falta confirmar la quantitat a la linia " + (retval.Items.Count + 1)));
                                    else
                                    {
                                        item.QtyConfirmed = confirmed.Value.toInteger();
                                        item.Qty = xItem.Attribute("QTY").Value.toInteger();
                                    }
                                }
                                else
                                    exs.Add(new Exception(string.Format("identificador LIN no reconegut a la linia {0}", xItems.ToList().IndexOf(xItem))));
                            }

                            retval.Items.Add(item);
                        }
                    }
                    else
                        exs.Add(new Exception("identificador de remesa de importacio incorrecte"));
                }


                return retval;
            }

            public static Confirmation Factory(List<Exception> exs, DTOImportacio importacio, DTOUser user)
            {
                Confirmation retval = new Confirmation();
                retval.User = user;
                retval.Importacio = importacio;
                retval.Items = new List<DTOInvoiceReceived.Item>();

                foreach (DTOImportPrevisio previsio in importacio.Previsions)
                {
                    if (previsio.InvoiceReceivedItem != null)
                    {
                        DTOInvoiceReceived.Item item = new DTOInvoiceReceived.Item();
                        item.Guid = previsio.InvoiceReceivedItem.Guid;
                        item.Qty = previsio.Qty;
                        item.QtyConfirmed = previsio.Qty;
                        retval.Items.Add(item);
                    }
                }
                return retval;
            }

        }
    }


    public class DTOImportacioItem : DTOBaseGuid
    {
        public SourceCodes SrcCod { get; set; }

        public DTOAmt Amt { get; set; }
        public DTODocFile DocFile { get; set; }
        public string Descripcio { get; set; }
        public DTOImportacio Parent { get; set; }

        public object Tag { get; set; }

        public enum SourceCodes
        {
            notSet,
            alb,
            fra,
            cmr,
            fraTrp,
            packingList,
            proforma
        }

        public DTOImportacioItem() : base()
        {
        }

        public DTOImportacioItem(Guid oGuid) : base(oGuid)
        {
        }


        public static DTOImportacioItem Factory(DTOImportacio oImportacio, DTOImportacioItem.SourceCodes oSrcCod, Guid oGuid = default(Guid))
        {
            DTOImportacioItem retval = null;

            if (oGuid == default(Guid))
                retval = new DTOImportacioItem();
            else
                retval = new DTOImportacioItem(oGuid);
            {
                var withBlock = retval;
                withBlock.Parent = oImportacio;
                withBlock.SrcCod = oSrcCod;
            }
            return retval;
        }


        public static string GetConcept(DTOImportacioItem oImportacioItem, DTOLang lang)
        {
            string s = "";
            switch (oImportacioItem.SrcCod)
            {
                case DTOImportacioItem.SourceCodes.cmr:
                    {
                        s = "CMR";
                        break;
                    }

                case DTOImportacioItem.SourceCodes.alb:
                    {
                        // Dim oAlb As DTODelivery = FEBL.Delivery.Find(oImportacioItem.Guid)
                        if (oImportacioItem.Tag == null)
                            s = "(albarà eliminat)";
                        else
                        {
                            DTODelivery oAlb = (DTODelivery)oImportacioItem.Tag;
                            s = DTODelivery.caption(oAlb, lang);
                        }

                        break;
                    }

                case DTOImportacioItem.SourceCodes.fra:
                    {
                        if (oImportacioItem.Tag == null)
                            s = "(error)";
                        else
                        {
                            DTOCca oCca = null/* TODO Change to default(_) if this is not a reference type */;
                            var oTag = oImportacioItem.Tag;
                            if (oTag is DTOCca)
                                oCca = (DTOCca)oTag;
                            else
                                oCca = (DTOCca)oTag; //TODO Translate ToObject
                            if (oCca == null)
                                s = "(factura eliminada)";
                            else
                                s = oCca.Concept;
                        }

                        break;
                    }

                case DTOImportacioItem.SourceCodes.fraTrp:
                    {
                        DTOCca oCca = null/* TODO Change to default(_) if this is not a reference type */;
                        var oTag = oImportacioItem.Tag;
                        oCca = (DTOCca)oTag;
                        if (oCca == null)
                            s = "(factura de transport eliminada)";
                        else
                            s = "transport: " + oCca.Concept;
                        break;
                    }
            }
            return s;
        }
    }
}
