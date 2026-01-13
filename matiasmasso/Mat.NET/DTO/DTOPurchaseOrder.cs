using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DTOPurchaseOrder : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }

        public DateTime Fch { get; set; }
        public int Num { get; set; }
        public DateTime FchDeliveryMax { get; set; }
        public DateTime FchDeliveryMin { get; set; }
        public DTOCustomer Customer { get; set; }
        public DTOProveidor Proveidor { get; set; }
        public DTOCustomer FacturarA { get; set; }
        public DTOCustomerPlatform Platform { get; set; }
        public string Concept { get; set; }
        public string NumComandaProveidor { get; set; }
        public DTOContact DeliverTo { get; set; }

        //Edi order message sender may require a DESDADV Edi message with shipping details
        //This DESADV should be sent to same receiver who sent the original order
        //So NADMS is recorded here to use it as NADMR in DESADV
        public string NADMS { get; set; } 

        public string Obs { get; set; }
        public string CustomerDocUrl { get; set; }
        public Codis Cod { get; set; }
        public DTOPaymentTerms PaymentTerms { get; set; }
        public Sources Source { get; set; }
        public bool Pot { get; set; }
        public bool BlockStock { get; set; }
        public bool TotJunt { get; set; }
        public DTOCur Cur { get; set; }
        public DTOIncentiu Incentiu { get; set; }
        public DTOAmt SumaDeImports { get; set; }

        public DTODocFile DocFile { get; set; }
        public List<DTOPurchaseOrderItem> Items { get; set; }

        public bool Hide { get; set; } //Ocultar al destinatari a la extranet
        public bool IsOpenOrder { get; set; }

        public DTOUsrLog UsrLog { get; set; }

        public List<string> ProformaObs { get; set; }

        public DTODocFile EtiquetesTransport { get; set; }
        public DTOIncoterm Incoterm { get; set; }

        public DTOInvoice.ExportCods ExportCod { get; set; }

        public enum Codis
        {
            notSet,
            proveidor,
            client,
            traspas,
            reparacio
        }

        public enum Sources
        {
            no_Especificado,
            telefonico,
            fax,
            eMail,
            representante,
            representante_por_Web,
            cliente_por_Web,
            matPocket,
            fira,
            cliente_XML,
            edi,
            garantia,
            iPhone,
            cliente_por_WebApi,
            ExcelMayborn,
            MarketPlace
        }

        public enum ShippingStatusCods
        {
            unShipped,
            halfShipped,
            fullyShipped,
            emptyOrder
        }


        public DTOPurchaseOrder() : base()
        {
            Items = new List<DTOPurchaseOrderItem>();
            ExportCod = DTOInvoice.ExportCods.nacional;
            UsrLog = new DTOUsrLog();
        }

        public DTOPurchaseOrder(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTOPurchaseOrderItem>();
            ExportCod = DTOInvoice.ExportCods.nacional;
            UsrLog = new DTOUsrLog();
        }

        public static DTOPurchaseOrder Factory(DTOCustomer oCustomer, DTOUser oUser, DateTime DtFch = default(DateTime), DTOPurchaseOrder.Sources oSource = DTOPurchaseOrder.Sources.no_Especificado, string sConcept = "")
        {
            if (DtFch == default(DateTime))
                DtFch = DTO.GlobalVariables.Today();
            DTOPurchaseOrder retval = new DTOPurchaseOrder();
            {
                var withBlock = retval;
                withBlock.Emp = oUser.Emp;
                withBlock.Cod = DTOPurchaseOrder.Codis.client;
                withBlock.Fch = DtFch;
                withBlock.Source = oSource;
                if (oCustomer.OrdersToCentral && oCustomer.Ccx != null)
                    withBlock.Customer = oCustomer.Ccx;
                else
                    withBlock.Customer = oCustomer;
                withBlock.UsrLog = DTOUsrLog.Factory(oUser);
                withBlock.Concept = sConcept;
                withBlock.Cur = DTOCur.Eur();
                withBlock.Items = new List<DTOPurchaseOrderItem>();
            }
            return retval;
        }

        public static DTOPurchaseOrder Factory(DTOConsumerTicket consumerTicket)
        {
            DTOPurchaseOrder retval = DTOPurchaseOrder.Factory(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.consumidor), consumerTicket.UsrLog.UsrCreated, consumerTicket.Fch, Sources.cliente_por_Web);
            return retval;
        }

        public static DTOPurchaseOrder Factory(DTOEmp oEmp, DTOProveidor oProveidor, DTOUser oUser, DateTime DtFch = default(DateTime), DTOPurchaseOrder.Sources oSource = DTOPurchaseOrder.Sources.no_Especificado, string sConcept = "")
        {
            if (DtFch == default(DateTime))
                DtFch = DTO.GlobalVariables.Today();
            DTOPurchaseOrder retval = new DTOPurchaseOrder();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                withBlock.Cod = DTOPurchaseOrder.Codis.proveidor;
                withBlock.Fch = DtFch;
                withBlock.FchDeliveryMin = withBlock.Fch.AddDays(21);
                withBlock.Source = oSource;
                withBlock.Proveidor = oProveidor;
                withBlock.Concept = sConcept;
                if (oProveidor != null)
                    withBlock.Cur = oProveidor.Cur;
                withBlock.DeliverTo = oEmp.Mgz;
                withBlock.Items = new List<DTOPurchaseOrderItem>();
                withBlock.UsrLog = DTOUsrLog.Factory(oUser);
            }
            return retval;
        }

        public DTOContact Contact()
        {
            DTOContact retval = default(DTOContact);
            switch (Cod)
            {
                case Codis.notSet:
                    {
                        retval = (DTOContact)Proveidor ?? (DTOContact)Customer;
                        break;
                    }

                case Codis.proveidor:
                    {
                        retval = Proveidor;
                        break;
                    }

                default:
                    {
                        retval = Customer;
                        break;
                    }
            }
            return retval;
        }

        public static DTOPurchaseOrder clon(DTOPurchaseOrder oSrc)
        {
            DTOPurchaseOrder retval = new DTOPurchaseOrder();
            {
                var withBlock = retval;
                withBlock.Emp = oSrc.Emp;
                withBlock.Cod = oSrc.Cod;
                withBlock.Fch = oSrc.Fch;
                withBlock.Customer = oSrc.Customer;
                withBlock.Proveidor = oSrc.Proveidor;
                withBlock.Concept = oSrc.Concept;
                withBlock.Pot = oSrc.Pot;
                withBlock.TotJunt = oSrc.TotJunt;
                withBlock.FchDeliveryMin = oSrc.FchDeliveryMin;
                withBlock.FchDeliveryMax = oSrc.FchDeliveryMax;
                withBlock.DeliverTo = oSrc.DeliverTo;
                withBlock.Obs = oSrc.Obs;
                withBlock.CustomerDocUrl = oSrc.CustomerDocUrl;
                withBlock.Source = oSrc.Source;
                withBlock.Cur = oSrc.Cur;
                withBlock.Platform = oSrc.Platform;
                foreach (DTOPurchaseOrderItem oItem in oSrc.Items)
                {
                    var ClonedItem = DTOPurchaseOrderItem.clon(oItem, retval);
                    withBlock.Items.Add(ClonedItem);
                }
            }
            return retval;
        }

        public string TotalsText()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(String.Format("Base imponible: {0}", DTOAmt.CurFormatted(Suma())));
            return sb.ToString();
        }

        public DTOAmt Suma()
        {
            DTOAmt retval = DTOAmt.Factory();
            foreach (DTOPurchaseOrderItem item in Items)
            {
                retval.Add(item.Amount());
            }
            return retval;
        }

        public decimal VolumeM3()
        {
            decimal retval = this.Items.Sum(x => x.VolumeM3());
            return retval;
        }

        public static string formattedId(DTOPurchaseOrder oPurchaseOrder)
        {
            string retval = string.Format("{0:0000}{1:000000}", oPurchaseOrder.Fch.Year, oPurchaseOrder.Num);
            return retval;
        }

        public static string filename(DTOPurchaseOrder oPurchaseOrder, MimeCods oMime)
        {
            string retval = "M+O pedido " + oPurchaseOrder.formattedId() + "." + oMime.ToString();
            return retval;
        }

        public string url(bool AbsoluteUrl = false)
        {
            return MmoUrl.Factory(AbsoluteUrl, "pedido", base.Guid.ToString());
        }

        public static string title(DTOPurchaseOrder oPurchaseOrder, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOApp.Current.Lang;
            string sConcept = oLang.Tradueix("Pedido", "Comanda", "Order");
            string sFrom = oLang.Tradueix("del", "del", "from");
            string retval = string.Format("{0} {1} {2} {3:dd/MM/yy}", sConcept, oPurchaseOrder.Concept, sFrom, oPurchaseOrder.Fch);
            return retval;
        }

        public static string title(List<DTOPurchaseOrder> Items, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */, bool BlProforma = false)
        {
            string retval;
            if (Items.Count == 0)
                retval = DTOApp.Current.Lang.Tradueix("(ningún pedido)", "(cap comanda)", "(none orders)");
            else
            {
                string sDoc = oLang.Tradueix("pedido", "comanda", "order");
                string sDocs = oLang.Tradueix("pedidos", "comandes", "orders");
                if (BlProforma)
                {
                    sDoc = "proforma";
                    sDocs = "proformas";
                }

                string sFirstDoc = Items.First().Fch.Year.ToString() + "." + Items.First().Num;
                string sLastDoc = Items.Last().Fch.Year.ToString() + "." + Items.Last().Num;

                switch (Items.Count)
                {
                    case 1:
                        {
                            retval = sDoc + " " + sFirstDoc;
                            break;
                        }

                    default:
                        {
                            if (consecutivos(Items))
                                retval = sDocs + " " + sFirstDoc + "-" + sLastDoc;
                            else
                            {
                                int i;
                                retval = sDocs + " ";
                                for (i = 0; i <= Items.Count - 1; i++)
                                {
                                    if (i > 2)
                                    {
                                        retval = retval + ",...";
                                        break;
                                    }
                                    if (i > 0)
                                        retval = retval + ",";
                                    retval = retval + Items[i].Fch.Year.ToString() + "." + Items[i].Num.ToString();
                                }
                            }

                            break;
                        }
                }
            }

            return retval;
        }

        public static string fullConcepte(DTOPurchaseOrder oPurchaseOrder, DTOLang oLang, bool IncludeFch = true)
        {
            string retval = "";
            switch (oPurchaseOrder.Cod)
            {
                case DTOPurchaseOrder.Codis.client:
                case DTOPurchaseOrder.Codis.proveidor:
                    {
                        switch (oPurchaseOrder.Cod)
                        {
                            case DTOPurchaseOrder.Codis.proveidor:
                                {
                                    retval = oLang.Tradueix("Nuestro pedido", "Comanda", "Our order");
                                    retval = retval + " " + oPurchaseOrder.Num;
                                    break;
                                }

                            default:
                                {
                                    retval = oLang.Tradueix("Su pedido", "Comanda", "Your order");
                                    break;
                                }
                        }
                        if (oPurchaseOrder.Concept.isNotEmpty())
                            retval = retval + " " + oPurchaseOrder.Concept;
                        if (IncludeFch)
                            retval = retval + " " + oLang.Tradueix("del", "del", "from") + " " + VbUtilities.Format(oPurchaseOrder.Fch, "dd/MM/yy");
                        break;
                    }
            }
            return retval;
        }


        public static bool consecutivos(List<DTOPurchaseOrder> oList)
        {
            bool retVal = true;
            int i;
            if (oList.Count > 2)
            {
                for (i = 0; i <= oList.Count - 2; i++)
                {
                    if (oList[i + 1].Num != oList[i].Num + 1)
                        retVal = false;
                }
            }
            return retVal;
        }

        public static string Nums(List<DTOPurchaseOrder> values)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (values.Count == 1)
                sb.Append(values.First().Num);
            else if (DTOPurchaseOrder.consecutivos(values))
                sb.Append(values.First().Num + "-" + values[values.Count - 1].Num);
            else
                for (int i = 0; i <= values.Count - 1; i++)
                {
                    if (i > 0)
                        sb.Append(",");
                    sb.Append(values[i].Num);
                }
            string retval = sb.ToString();
            return retval;
        }

        public static DTOMailMessage mailMessageConfirmation(DTOPurchaseOrder oPurchaseOrder, List<string> sRecipients = null, List<string> validationErrors = null)
        {
            DTOUser oUser = oPurchaseOrder.UsrLog.UsrCreated;

            if (sRecipients == null)
            {
                sRecipients = new List<string>();
                sRecipients.Add(oUser.EmailAddress);
            }

            var retval = DTOMailMessage.Factory(sRecipients);
            {
                retval.Bcc = new string[] { DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info) };
                DTOLang oLang = DTOLang.ESP(); //TODO
                string sWithErrs = "";
                string sErrs = "";
                if (validationErrors != null && validationErrors.Count != 0)
                {
                    sWithErrs = oLang.Tradueix("con errores", "amb errors", "with errors");
                    sErrs = string.Join("<br/>", validationErrors.ToArray());
                }
                retval.Subject = string.Format("{0} #{1} {2} {3} {4}", oLang.Tradueix("Pedido", "Comanda", "Order", "Encomenda"), oPurchaseOrder.Num, sWithErrs, oLang.Tradueix("de", "de", "from", "de"), oPurchaseOrder.Contact().FullNom);
                retval.BodyUrl = MmoUrl.BodyTemplateUrl(DTODefault.MailingTemplates.customerPurchaseOrder, oPurchaseOrder.Guid.ToString());
            }
            return retval;
        }

        public static DTOMailMessage mailMessageRepConfirmation(DTOPurchaseOrder oPurchaseOrder, bool ccToUserCreated = false)
        {
            DTOUser oUser = oPurchaseOrder.UsrLog.UsrCreated;
            DTOLang oLang = oPurchaseOrder.Contact().Lang;
            string sTo = DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info);
            if (ccToUserCreated)
                sTo = oPurchaseOrder.UsrLog.UsrCreated.EmailAddress;

            List<string> sRecipients = new List<string>();
            var retval = DTOMailMessage.Factory(sTo);
            {
                if (ccToUserCreated)
                    retval.Bcc = new string[]
                    {
                    DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info)
                };
                retval.Subject = string.Format("{0} #{1} {2} {3} {4} {5}", oLang.Tradueix("Pedido", "Comanda", "Order", "Encomenda"), oPurchaseOrder.Num, oLang.Tradueix("de", "de", "from", "de"), DTOUser.NicknameOrElse(oUser), oLang.Tradueix("para", "per", "for"), oPurchaseOrder.Contact().FullNom);
                retval.BodyUrl = MmoUrl.BodyTemplateUrl(DTODefault.MailingTemplates.repPurchaseOrder, oPurchaseOrder.Guid.ToString());
            }
            return retval;
        }

        public static string pdfFilename(List<DTOPurchaseOrder> oPurchaseOrders, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOLang.ESP();
            string sCaption = "";
            switch (oPurchaseOrders.Count)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        sCaption = oLang.Tradueix("pedido", "comanda", "order");
                        break;
                    }

                default:
                    {
                        sCaption = oLang.Tradueix("pedidos", "comandes", "orders");
                        break;
                    }
            }
            string sNums = DTOPurchaseOrder.Nums(oPurchaseOrders).Replace(",", " ");
            string retval = string.Format("M+O {0} {1}.pdf", sCaption, sNums);
            return retval;
        }


        public DTOPurchaseOrderItem addItem(DTOProductSku oSku, int qty, DTOAmt price = null/* TODO Change to default(_) if this is not a reference type */, decimal dto = 0)
        {
            if (Items == null)
                Items = new List<DTOPurchaseOrderItem>();

            DTOPurchaseOrderItem item = new DTOPurchaseOrderItem();
            item.PurchaseOrder = this;
            item.Sku = oSku;
            item.Qty = qty;
            item.Pending = qty;
            item.Price = price;
            item.Dto = dto;

            if (oSku == null)
            {
                item.ErrCod = DTOPurchaseOrderItem.ErrCods.UnknownProduct;
                item.ErrDsc = "Producto desconocido";
            }
            else
            {
                switch (oSku.CodExclusio)
                {
                    case DTO.DTOProductSku.CodisExclusio.Inclos:
                        break;
                    case DTO.DTOProductSku.CodisExclusio.Obsolet:
                    case DTO.DTOProductSku.CodisExclusio.OutOfCatalog:
                        if (item.Sku.stockAvailable() == 0)
                        {
                            item.ErrCod = DTOPurchaseOrderItem.ErrCods.Obsolet;
                            item.ErrDsc = "Producto obsoleto";
                        }
                        break;
                    case DTO.DTOProductSku.CodisExclusio.Canal:
                        item.ErrCod = DTOPurchaseOrderItem.ErrCods.ChannelExcluded;
                        item.ErrDsc = "Distribución no autorizada en este canal";
                        break;
                    case DTO.DTOProductSku.CodisExclusio.Exclusives:
                        item.ErrCod = DTOPurchaseOrderItem.ErrCods.CustomerExcluded;
                        item.ErrDsc = "Producto de distribución selectiva";
                        break;
                    case DTO.DTOProductSku.CodisExclusio.PremiumLine:
                        item.ErrCod = DTOPurchaseOrderItem.ErrCods.PremiumLineExcluded;
                        item.ErrDsc = "Producto exclusivo de distribución con contrato";
                        break;

                    default:
                        break;
                }
            }

            Items.Add(item);
            return item;
        }

        public DTOAmt SumaDeImportes()
        {
            var retval = DTOAmt.Factory();
            if (Items != null)
            {
                foreach (DTOPurchaseOrderItem item in Items)
                {
                    DTOAmt oAmt = item.Amount();
                    retval.Add(oAmt);
                }
            }
            return retval;
        }

        public List<DTOTaxBaseQuota> ivaBaseQuotas()
        {
            List<DTOTaxBaseQuota> retval = new List<DTOTaxBaseQuota>();
            if (Cod != Codis.proveidor)
            {
                DTOCustomer oCcx = this.Customer.CcxOrMe();
                if (oCcx.Iva)
                {
                    DTOAmt oBaseImponible = this.SumaDeImportes();
                    DTOTax oIva = DTOTax.closest(DTOTax.Codis.iva_Standard, Fch);
                    DTOTaxBaseQuota oQuotaIva = new DTOTaxBaseQuota(oIva, oBaseImponible);
                    retval.Add(oQuotaIva);

                    if (oCcx.Req)
                    {
                        DTOTax oReq = DTOTax.closest(DTOTax.Codis.recarrec_Equivalencia_Standard, Fch);
                        DTOTaxBaseQuota oQuotaReq = new DTOTaxBaseQuota(oReq, oBaseImponible);
                        retval.Add(oQuotaReq);
                    }
                }
            }
            return retval;
        }

        public static bool devengaIva(DTOPurchaseOrder value)
        {
            bool retval = false;
            switch (value.Cod)
            {
                case DTOPurchaseOrder.Codis.client:
                    {
                        retval = value.Customer.CcxOrMe().Iva;
                        break;
                    }

                case DTOPurchaseOrder.Codis.proveidor:
                    {
                        break;
                    }
            }
            return retval;
        }

        public static bool devengaReq(DTOPurchaseOrder value)
        {
            bool retval = false;
            switch (value.Cod)
            {
                case DTOPurchaseOrder.Codis.client:
                    {
                        retval = value.Customer.CcxOrMe().Req;
                        break;
                    }

                case DTOPurchaseOrder.Codis.proveidor:
                    {
                        break;
                    }
            }
            return retval;
        }

        public static decimal ivaPct(DTOPurchaseOrder oPurchaseOrder)
        {
            decimal retval = DTOTax.closest(DTOTax.Codis.iva_Standard, oPurchaseOrder.Fch).tipus;
            return retval;
        }

        public static DTOAmt ivaAmt(DTOPurchaseOrder oPurchaseOrder)
        {
            decimal dcIvaPct = ivaPct(oPurchaseOrder);
            DTOAmt retval = oPurchaseOrder.SumaDeImportes().Percent(dcIvaPct);
            return retval;
        }

        public static decimal reqPct(DTOPurchaseOrder oPurchaseOrder)
        {
            decimal retval = DTOTax.closest(DTOTax.Codis.recarrec_Equivalencia_Standard, oPurchaseOrder.Fch).tipus;
            return retval;
        }

        public static DTOAmt reqAmt(DTOPurchaseOrder oPurchaseOrder)
        {
            decimal dcIvaReq = reqPct(oPurchaseOrder);
            DTOAmt retval = oPurchaseOrder.SumaDeImportes().Percent(dcIvaReq);
            return retval;
        }

        public static DTOAmt taxAmt(DTOPurchaseOrder oPurchaseOrder)
        {
            DTOAmt retval = DTOAmt.Factory();
            retval.Add(ivaAmt(oPurchaseOrder));
            retval.Add(reqAmt(oPurchaseOrder));
            return retval;
        }

        public static DTOAmt totalIvaInclos(DTOPurchaseOrder oPurchaseOrder)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            DTOAmt oBaseImponible = oPurchaseOrder.SumaDeImportes();
            if (devengaIva(oPurchaseOrder))
            {
                DTOAmt oIva = ivaAmt(oPurchaseOrder);
                if (devengaReq(oPurchaseOrder))
                {
                    DTOAmt oReq = reqAmt(oPurchaseOrder);
                    retval = DTOAmt.Factory(oBaseImponible, oIva, oReq);
                }
                else
                    retval = DTOAmt.Factory(oBaseImponible, oIva);
            }
            else
                retval = oBaseImponible;
            return retval;
        }


        public string formattedId()
        {
            string retval = string.Format("{0:0000}{1:000000}", Fch.Year, Num);
            return retval;
        }

        public string caption(DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null && this.Contact() != null)
                oLang = this.Contact().Lang;
            if (oLang == null)
                oLang = DTOLang.ESP();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (Cod)
            {
                case DTOPurchaseOrder.Codis.client:
                    {
                        sb.Append(oLang.Tradueix("Su pedido ", "La seva comanda ", "Your order "));
                        break;
                    }

                case DTOPurchaseOrder.Codis.proveidor:
                    {
                        sb.Append(oLang.Tradueix("Nuestro pedido ", "La nostre comanda ", "Our order "));
                        sb.Append(Num.ToString());
                        sb.Append(" ");
                        break;
                    }
            }
            if (Concept.isNotEmpty())
                sb.Append(Concept + " ");
            sb.Append(oLang.Tradueix("del ", "del ", "from ") + VbUtilities.Format(Fch, "dd/MM/yy"));
            string retval = sb.ToString();
            return retval;
        }
        public string FullNomAndCaption(DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null & this.Contact() != null)
                oLang = this.Contact().Lang;
            if (oLang == null)
                oLang = DTOLang.ESP();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(oLang.Tradueix("Pedido ", "Comanda ", "Order "));
            sb.Append(Num.ToString());
            sb.Append(" ");
            sb.Append(this.Contact().FullNom);
            if (Concept.isNotEmpty())
                sb.Append(Concept + " ");
            sb.Append(oLang.Tradueix("del ", "del ", "from ") + VbUtilities.Format(Fch, "dd/MM/yy"));
            string retval = sb.ToString();
            return retval;
        }

        public DTOAmt deliverableAmt()
        {
            var retval = DTOAmt.Factory(Items.Sum(x => x.deliverableAmt().Eur));
            return retval;
        }

        public static bool discountsExist(DTOPurchaseOrder value)
        {
            bool retval = false;
            if (value.Items != null)
                retval = value.Items.Exists(x => x.Dto != 0);
            return retval;
        }

        public static bool pendentsExist(DTOPurchaseOrder value)
        {
            bool retval = false;
            if (value.Items != null)
                retval = value.Items.Any(x => x.Pending != x.Qty);
            return retval;
        }

        public static decimal volumeM3(DTOPurchaseOrder oOrder)
        {
            decimal retval = oOrder.Items.Sum(x => x.Qty * DTOProductSku.volumeM3OrInherited(x.Sku));
            return retval;
        }

        public static decimal Eur(DTOPurchaseOrder oOrder)
        {
            decimal retval = oOrder.Items.Sum(x => x.Qty * x.Price.Eur * (100 - x.Dto) / 100);
            return retval;
        }

        public static DTOPurchaseOrder.ShippingStatusCods shippingStatus(DTOPurchaseOrder oOrder)
        {
            DTOPurchaseOrder.ShippingStatusCods retval;
            if (oOrder.Items.Count == 0)
                retval = DTOPurchaseOrder.ShippingStatusCods.emptyOrder;
            else if (oOrder.Items.All(x => x.Pending == 0))
                retval = DTOPurchaseOrder.ShippingStatusCods.fullyShipped;
            else if (oOrder.Items.All(x => x.Qty == x.Pending))
                retval = DTOPurchaseOrder.ShippingStatusCods.unShipped;
            else
                retval = DTOPurchaseOrder.ShippingStatusCods.halfShipped;
            return retval;
        }

        public static DTOMailMessage confirmationMailMessage(DTOEmp oEmp, DTOPurchaseOrder oPurchaseOrder, List<string> sRecipients, DTOLang oLang)
        {
            DTOMailMessage retval = DTOMailMessage.Factory(sRecipients);
            {
                retval.Subject = string.Format("{0} #{1} {2} {3}", oLang.Tradueix("Pedido", "Comanda", "Order", "Encomenda"), oPurchaseOrder.Num, oLang.Tradueix("de", "de", "from", "de"), oPurchaseOrder.Contact().FullNom);
                retval.BodyUrl = oEmp.absoluteUrl("mail", DTODefault.MailingTemplates.customerPurchaseOrder.ToString(), oPurchaseOrder.Guid.ToString());
            }
            return retval;
        }


        public class ConceptShortcut : DTOBaseGuid
        {
            public string Searchkey { get; set; }
            public DTOLangText Concept { get; set; }
            public DTOPurchaseOrder.Sources Src { get; set; }

            public ConceptShortcut() : base()
            {
                Concept = new DTOLangText(base.Guid, DTOLangText.Srcs.PurchaseOrderConceptShortcut);
            }
            public ConceptShortcut(Guid guid) : base(guid)
            {
                Concept = new DTOLangText(base.Guid, DTOLangText.Srcs.PurchaseOrderConceptShortcut);
            }
        }


        public class HeaderModel
        {
            public Guid Guid { get; set; }
            public int Num { get; set; }
            public DateTime Fch { get; set; }

            public DTOGuidNom.Compact Contact { get; set; }
            public string Concept { get; set; }
            public Decimal Eur { get; set; }

            public DTOPurchaseOrder.Codis Cod { get; set; }

            public DTOPurchaseOrder.Sources Src { get; set; }
            public bool IsOpen { get; set; }
            public string Hash { get; set; }
            public string UsrCreated { get; set; }
        }
    }

    public class DTOPurchaseOrderItem : DTOBaseGuid
    {
        public DTOPurchaseOrder PurchaseOrder { get; set; }
        public int Qty { get; set; }
        public int Pending { get; set; }
        public DTOAmt Price { get; set; }
        public decimal Dto { get; set; }
        public decimal Dt2 { get; set; }
        public ChargeCods ChargeCod { get; set; }
        public DTOProductSku Sku { get; set; }
        public DTORepCom RepCom { get; set; }
        public int Lin { get; set; }
        public int CustomLin { get; set; }
        public ErrCods ErrCod { get; set; }
        public string ErrDsc { get; set; }
        public List<DTODeliveryItem> Deliveries { get; set; }

        public int DeliveredQty { get; set; }
        public List<DTOIncentiu> Incentius { get; set; }
        //public List<DTOPurchaseOrderItem> PackChildren { get; set; }
        //public DTOPurchaseOrderItem PackParent { get; set; }
        public DateTime ETD { get; set; } // Estimated Delivery Time
        public List<DTOPurchaseOrderItem> Bundle { get; set; }

        public ShippingResults ShippingResult;

        public enum ChargeCods
        {
            chargeable,
            FOC // free of charge
        }

        public enum ShippingResults
        {
            Dispatched = 12,
            NoLongerAvailable = 182,
            TemporarilyOutOfStock = 185
        }

        public enum ErrCods
        {
            Success,
            ChannelExcluded,
            CustomerExcluded,
            PremiumLineExcluded,
            Obsolet,
            UnknownProduct,
            MoqNotMet,
            OutOfStock
        }

        public string ErrCodText(DTOLang lang)
        {
            switch (ErrCod)
            {
                case ErrCods.Success:
                    return lang.Tradueix("Satisfactorio", "Satisfactori", "Success");
                case ErrCods.ChannelExcluded:
                    return lang.Tradueix("Canal no autorizado", "Canal no autoritzat", "Unallowed Channel");
                case ErrCods.CustomerExcluded:
                    return lang.Tradueix("Cliente excluido", "Client exclos", "Customer excluded");
                case ErrCods.PremiumLineExcluded:
                    return lang.Tradueix("Sin contrato Premium Line", "Sense contracte Premium Line", "Missing Premium Line Agreement");
                case ErrCods.Obsolet:
                    return lang.Tradueix("Producto obsoleto", "Producte obsolet", "Outdated product");
                case ErrCods.UnknownProduct:
                    return lang.Tradueix("Producto desconocido", "Producte desconegut", "Unknown product");
                case ErrCods.MoqNotMet:
                    return lang.Tradueix("Cantidad por debajo del mínimo", "Quantitat per sota dels minims", "MOQ not met");
                case ErrCods.OutOfStock:
                    return lang.Tradueix("No disponible temporalmente", "No disponible temporalment", "Temporarily unavailable");
                default:
                    return ErrCod.ToString();
            }
        }

        public DTOPurchaseOrderItem() : base()
        {
            Bundle = new List<DTOPurchaseOrderItem>();
        }

        public DTOPurchaseOrderItem(Guid oGuid) : base(oGuid)
        {
            Bundle = new List<DTOPurchaseOrderItem>();
        }

        public static DTOPurchaseOrderItem Factory(DTOPurchaseOrder oOrder, DTOProductSku oSku, int iQty, DTOAmt oPrice, decimal DcDto, DTORepCom oRepCom = null/* TODO Change to default(_) if this is not a reference type */)
        {
            //To deprecate due to new dt2 for Eroski
            DTOPurchaseOrderItem retval = new DTOPurchaseOrderItem();
            {
                var withBlock = retval;
                withBlock.PurchaseOrder = oOrder;
                withBlock.Sku = oSku;
                withBlock.Qty = iQty;
                withBlock.Price = oPrice;
                withBlock.Dto = DcDto;
                withBlock.Pending = withBlock.Qty;
                withBlock.ChargeCod = DTOPurchaseOrderItem.ChargeCods.chargeable;
                withBlock.RepCom = oRepCom;
            }
            return retval;
        }

        public static DTOPurchaseOrderItem Factory(DTOPurchaseOrder oOrder, DTOProductSku oSku, int iQty, DTOAmt oPrice, decimal DcDto, decimal DcDt2, DTORepCom oRepCom = null)
        {
            DTOPurchaseOrderItem retval = new DTOPurchaseOrderItem();
            {
                var withBlock = retval;
                withBlock.PurchaseOrder = oOrder;
                withBlock.Sku = oSku;
                withBlock.Qty = iQty;
                withBlock.Price = oPrice;
                withBlock.Dto = DcDto;
                withBlock.Pending = withBlock.Qty;
                withBlock.ChargeCod = DTOPurchaseOrderItem.ChargeCods.chargeable;
                withBlock.RepCom = oRepCom;
            }
            return retval;
        }

        public static DTOPurchaseOrderItem Factory(DTOBasketLine value)
        {
            DTOPurchaseOrderItem retval = new DTOPurchaseOrderItem();
            {
                var withBlock = retval;
                withBlock.Qty = value.qty;
                withBlock.Pending = withBlock.Qty;
                withBlock.Sku = new DTOProductSku(value.sku);
                withBlock.Sku.NomLlarg.Esp = value.nom;
                withBlock.Sku.Stock = value.availableStock;
                withBlock.Sku.Category = new DTOProductCategory(value.category); // important per assignar el representant
                withBlock.Sku.Category.Brand = new DTOProductBrand(value.brand);
                withBlock.Price = DTOAmt.Factory(value.price);
                withBlock.Dto = value.dto;
            }
            return retval;
        }


        public static DTOPurchaseOrderItem clon(DTOPurchaseOrderItem oSrc, DTOPurchaseOrder oClonedPurchaseOrder)
        {
            DTOPurchaseOrderItem retval = new DTOPurchaseOrderItem();
            {
                var withBlock = retval;
                withBlock.PurchaseOrder = oClonedPurchaseOrder;
                withBlock.ChargeCod = oSrc.ChargeCod;
                withBlock.Dto = oSrc.Dto;
                withBlock.Dt2 = oSrc.Dt2;
                withBlock.Qty = oSrc.Qty;
                withBlock.Pending = oSrc.Qty;
                withBlock.Sku = oSrc.Sku;
                withBlock.RepCom = oSrc.RepCom;
                withBlock.Price = oSrc.Price;
            }
            return retval;
        }

        public void loadBundleItems()
        {
            Bundle = new List<DTOPurchaseOrderItem>();
            foreach (DTOSkuBundle oSkuBundle in Sku.BundleSkus)
            {
                var oPrice = DTOAmt.Factory();
                if (oSkuBundle.Sku.Price != null)
                    oPrice = oSkuBundle.Sku.Price.DeductPercent(Sku.BundleDto);
                var item = DTOPurchaseOrderItem.Factory(PurchaseOrder, oSkuBundle.Sku, oSkuBundle.Qty * Qty, oPrice, Dto, Dt2);
                Bundle.Add(item);
            }
        }

        public bool isBundleParent()
        {
            bool retval = false;
            if (Bundle != null)
                retval = Bundle.Count > 0;
            return retval;
        }

        public static DTOPurchaseOrderItem bundleItemFactory(DTOSkuBundle oSkuBundle, DTOPurchaseOrderItem item, DTOEmp oEmp, List<DTOPricelistItemCustomer> oCustomCosts, List<DTOCustomerTarifaDto> oTarifaDtos, List<DTOCliProductDto> oCliProductDtos, DTORepCom oRepCom)
        {
            var oOrder = item.PurchaseOrder;
            var oSku = item.Sku;

            decimal DcDto = 0;
            DTOAmt oPrice = null/* TODO Change to default(_) if this is not a reference type */;
            if (oOrder.Cod != DTOPurchaseOrder.Codis.proveidor)
                oPrice = DTOProductSku.getCustomerCost(oSkuBundle.Sku, oCustomCosts, oTarifaDtos);

            if (oPrice == null)
                oPrice = DTOAmt.Factory();
            else if (oOrder.Cod == DTOPurchaseOrder.Codis.client)
            {
                DTOCliProductDto oDto = oSku.CliProductDto(oCliProductDtos);
                if (oDto != null)
                    DcDto = oDto.Dto;
            }

            var retval = DTOPurchaseOrderItem.Factory(oOrder, oSkuBundle.Sku, item.Qty, oPrice, DcDto, oRepCom);
            return retval;
        }


        public DTOAmt preuNet()
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (Price != null)
                retval = Price.Clone().Substract(Price.Percent(Dto));
            return retval;
        }

        public DTOAmt Amount()
        {
            DTOAmt retval = DTOAmt.FromQtyPriceDto(Qty, Price, Dto);
            return retval;
        }

        public DTOAmt pendingAmount()
        {
            DTOAmt retval = DTOAmt.FromQtyPriceDto(Pending, Price, Dto);
            return retval;
        }

        public int deliverableQty()
        {
            var stk = Sku.stockAvailable();
            var retval = Math.Min(Pending, stk);
            return retval;
        }

        public DTOAmt deliverableAmt()
        {
            var retval = DTOAmt.Factory(preuNet().Eur * deliverableQty());
            return retval;
        }

        public static string multilineFullText(DTOPurchaseOrderItem value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(value.PurchaseOrder.Contact().FullNom);
            sb.AppendLine(DTOPurchaseOrder.fullConcepte(value.PurchaseOrder, value.PurchaseOrder.Contact().Lang));
            sb.AppendLine(string.Format("{0} {1} x {2}", value.Sku.NomLlarg.Esp, value.Qty, value.Price.Formatted()));
            string retval = sb.ToString();
            return retval;
        }

        public static string formattedDto(DTOPurchaseOrderItem item)
        {
            string retval;
            if (item.Dto == 0)
                retval = "&nbsp;";
            else
                retval = item.Dto + "%";
            return retval;
        }

        public decimal VolumeM3()
        {
            decimal retval = this.Qty * this.Sku.VolumeM3OrInherited();
            return retval;
        }
        public static List<DTOProductBrand> catalog(List<DTOPurchaseOrderItem> Items)
        {
            var oSkus = Items.GroupBy(x => x.Sku.Guid).Select(y => y.First()).Select(z => z.Sku).ToList();
            var oCategories = oSkus.GroupBy(x => x.Category.Guid).Select(y => y.First()).Select(z => z.Category).ToList();
            var retval = oCategories.GroupBy(x => x.Brand.Guid).Select(y => y.First()).Select(z => z.Brand).ToList();
            foreach (var oBrand in retval)
            {
                foreach (var oCategory in oCategories.Where(x => x.Brand.Equals(oBrand)))
                {
                    oBrand.Categories.Add(oCategory);
                    foreach (var oSku in oSkus.Where(x => x.Category.Equals(oCategory)))
                        oCategory.Skus.Add(oSku);
                }
            }
            return retval;
        }

        public static Result Success(int lin, DTOPurchaseOrderItem item)
        {
            Result retval = new Result();
            retval.Lin = lin;
            retval.Cod = Result.Cods.Success;
            retval.Item = item;
            return retval;
        }

        public static Result Fail(int lin, Result.Cods cod, object tag = null, string obs = "")
        {
            Result retval = new Result();
            retval.Lin = lin;
            retval.Cod = cod;
            retval.Tag = tag;
            retval.Obs = obs;
            return retval;
        }

        public class Result
        {
            public DTOPurchaseOrderItem Item;
            public int Lin;
            public Object Tag;
            public Cods Cod;
            public string Obs;

            public enum Cods
            {
                Success,
                BadFormat,
                UnknownProduct,
                ObsoletProduct,
                UnAllowedProduct,
                Moq,
                OutOfStock,
                NoStockEnough
            }

            public static DTOMailMessage mailMessage(List<Result> results, List<String> recipients)
            {
                DTOMailMessage retval = DTOMailMessage.Factory(recipients, "Validación de su pedido", HtmlTable(results));
                return retval;
            }

            private static string HtmlTable(List<Result> results)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<p>Pasamos relación de incidencias encontradas al validar su pedido:</p>");
                sb.AppendLine("<table border='1' style='border:0.5px solid lightgray;border-collapse: collapse; padding:5px 20px; margin:20px;'>");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td style='text-align:right'>Linea</td><td>Observaciones</td>");
                sb.AppendLine();
                sb.AppendLine("</tr>");
                foreach (Result result in results)
                {
                    sb.AppendLine("<tr>");
                    sb.AppendFormat("<td style='text-align:right'>{0}</td><td>{1}</td>", result.Lin, result.Obs);
                    sb.AppendLine();
                    sb.AppendLine("</tr>");
                }
                sb.AppendLine("</table>");
                string retval = sb.ToString();
                return retval;
            }

        }
    }
}
