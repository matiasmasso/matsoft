using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOInvoice : DTOBaseGuid
    {
        public class Compact
        {
            public Guid Guid { get; set; }
            public DTOInvoice.Series Serie { get; set; }
            public int Num { get; set; }
            public DTOAmt.Compact BaseImponible { get; set; }
            public DTOAmt.Compact Total { get; set; }
            public DateTime Fch { get; set; }
            public DTOGuidNom.Compact Customer { get; set; }
            public DTODocFile.Compact DocFile { get; set; }
        }

        public class HeaderModel
        {
            public Guid Guid { get; set; }
            public DTOInvoice.Series Serie { get; set; }
            public int Num { get; set; }
            public DateTime Fch { get; set; }
            public DTOGuidNom.Compact Customer { get; set; }
            public Decimal BaseImponible { get; set; }
            public Decimal Liquid { get; set; }
            public string Hash { get; set; }
        }


        public DTOEmp Emp { get; set; }
        public Series Serie { get; set; }
        public int Num { get; set; }
        public DateTime Fch { get; set; }
        public DTOLang Lang { get; set; }
        public DTOCustomer Customer { get; set; }
        public Guid Deutor { get; set; }
        public String Nom { get; set; }
        public String Adr { get; set; }
        public DTOZip Zip { get; set; }
        public DTONif.Collection Nifs { get; set; }
        public DTOAmt Total { get; set; }

        public DTOAmt BaseImponible { get; set; }
        public DTOAmt IvaAmt { get; set; }
        public DTOAmt ReqAmt { get; set; }

        public List<DTOTaxBaseQuota> IvaBaseQuotas { get; set; }

        public decimal Iva { get; set; }
        public decimal Req { get; set; }
        public bool InversionDelSujetoPasivo { get; set; }

        public List<DTODelivery> Deliveries { get; set; }
        public string Fpg { get; set; } = "";
        public string Ob1 { get; set; } = "";
        public string Ob2 { get; set; } = "";
        public string Ob3 { get; set; } = "";
        public DTOPaymentTerms.CodsFormaDePago Cfp { get; set; }

        public DateTime Vto { get; set; }
        public PrintModes PrintMode { get; set; }
        public DateTime FchLastPrinted { get; set; }
        public DTOUser UserLastPrinted { get; set; }
        public bool ExistPnds { get; set; }

        public DTOCca Cca { get; set; }
        public List<DTORepComLiquidable> RepComLiquidables { get; set; }
        public List<DTOInvoiceException> Exceptions { get; set; }

        public string TipoFactura { get; set; }
        public TiposSujeccionIva TipoSujeccionIva { get; set; }
        public DTODocFile DocFile { get; set; }
        public DTOSiiLog SiiLog { get; set; }
        public string SiiL9 { get; set; }
        public Conceptes Concepte { get; set; }

        public ExportCods ExportCod { get; set; }
        public DTOIncoterm Incoterm { get; set; }

        public string RegimenEspecialOTrascendencia { get; set; }


        public enum Series
        {
            standard,
            rectificativa,
            simplificada,
            inversionSujetoPasivo
        }

        public enum Conceptes
        {
            notSet,
            ventas,
            servicios,
            suplidos
        }

        public enum ExportCods
        {
            notSet,
            nacional,
            intracomunitari,
            extracomunitari
        }

        public enum SiiResults
        {
            notSet,
            correcto,
            aceptadoConErrores,
            incorrecto
        }


        public enum TiposSujeccionIva
        {
            notSet,
            sujetoNoExento,
            sujetoExento,
            noSujeto
        }
        public enum CausasExencionIva
        {
            notSet,
            articulo21_ExportOutOfCEE,
            articulo25_ExportInsideCEE
        }

        public enum TiposFactura
        {
            F1_Factura_Estandar,
            F2_Factura_Simplificada,
            F3_Sustitutiva,
            F4_Resumen,
            F5_Importaciones_DUA,
            F6_Justificantes_Contables,
            R1_Factura_Rectificativa1 // (Art 80.1 y 80.2 y Error fundado en derecho)
    ,
            R2_Factura_Rectificativa2 // (Art 80.3)
    ,
            R3_Factura_Rectificativa3 // (Art 80.4)
    ,
            R4_Factura_Resto,
            R5_Factura_Rectificativa_simplificadas
        }

        public enum PrintModes
        {
            pending,
            noPrint,
            printer,
            email,
            edi
        }

        public DTOInvoice() : base()
        {
            IvaBaseQuotas = new List<DTOTaxBaseQuota>();
            Exceptions = new List<DTOInvoiceException>();
            Deliveries = new List<DTODelivery>();
        }

        public DTOInvoice(Guid oGuid) : base(oGuid)
        {
            IvaBaseQuotas = new List<DTOTaxBaseQuota>();
            Exceptions = new List<DTOInvoiceException>();
            Deliveries = new List<DTODelivery>();
        }

        public static DTOInvoice Factory(DTODelivery oDelivery)
        {
            DTOInvoice retval = new DTOInvoice();
            DTOCustomer oCcx = oDelivery.Customer.CcxOrMe();
            retval.Emp = oCcx.Emp.Trimmed();
            retval.Nom = oCcx.Nom;
            retval.Nifs = oCcx.Nifs;
            retval.Adr = oCcx.Address.Text;
            retval.Zip = oCcx.Address.Zip;
            retval.Lang = oCcx.Lang;
            retval.Concepte = DTOInvoice.getConcepte(oDelivery);
            //retval.Concepte = (oDelivery.Cod == DTOPurchaseOrder.Codis.reparacio) ? DTOInvoice.Conceptes.servicios : DTOInvoice.Conceptes.ventas;
            retval.ExportCod = oDelivery.ExportCod;
            retval.Incoterm = oDelivery.Incoterm;
            retval.Deutor = oDelivery.Deutor;
            retval.Deliveries.Add(oDelivery);
            retval.TipoFactura = oCcx.IsConsumer() ? "F2" : "F1";
            retval.Serie = oCcx.IsConsumer() ? DTOInvoice.Series.simplificada : DTOInvoice.Series.standard;
            retval.Customer = oCcx;
            DTOInvoice.SetRegimenEspecialOTrascendencia(retval);
            retval.SiiL9 = DTOInvoice.GuessClauL9ExempcioIva(retval);
            return retval;
        }
        public static DTOInvoice Factory(DTOCustomer oCustomer)
        {
            DTOInvoice retval = new DTOInvoice();
            retval.Emp = oCustomer.Emp.Trimmed();
            retval.Nom = oCustomer.Nom;
            retval.Nifs = oCustomer.Nifs;
            retval.Adr = oCustomer.Address.Text;
            retval.Zip = oCustomer.Address.Zip;
            retval.Lang = oCustomer.Lang;
            retval.ExportCod = oCustomer.Address.ExportCod();

            retval.TipoFactura = oCustomer.IsConsumer() ? "F2" : "F1";
            retval.Serie = oCustomer.IsConsumer() ? DTOInvoice.Series.simplificada : DTOInvoice.Series.standard;
            retval.Concepte = DTOInvoice.Conceptes.ventas;
            retval.Customer = oCustomer;
            retval.Deliveries = new List<DTODelivery>();
            DTOInvoice.SetRegimenEspecialOTrascendencia(retval);
            retval.SiiL9 = DTOInvoice.GuessClauL9ExempcioIva(retval);
            return retval;
        }

        public Boolean IsSingleDelivery()
        {
            List<DTODeliveryItem> items = this.Deliveries.SelectMany(x => x.Items).ToList();
            DTODelivery firstDelivery = items.First().Delivery;
            Boolean retval = items.All(x => x.Delivery.Equals(firstDelivery));
            return retval;
        }
        public Boolean IsSingleOrder()
        {
            List<DTODeliveryItem> items = this.Deliveries.SelectMany(x => x.Items).ToList();
            DTOPurchaseOrder firstOrder = items.First().PurchaseOrderItem.PurchaseOrder;
            Boolean retval = items.All(x => x.PurchaseOrderItem.PurchaseOrder.Equals(firstOrder));
            return retval;
        }
        public Boolean IsEstrangerResident()
        {
            Boolean retval = false;
            if (this.Nifs != null)
                retval = this.Nifs.IsEstrangerResident();
            return retval;
        }

        public static string ediFileName(DTOInvoice oInvoice)
        {
            return string.Format("{0}.{1}.{2:yyyy.MM.dd.HH.mm}.txt", DTOInvoice.EdiversaTag(oInvoice).ToString(), DTOInvoice.formattedId(oInvoice), DTO.GlobalVariables.Now());
        }

        public static DTOEdiversaFile.Tags EdiversaTag(DTOInvoice oInvoice)
        {
            List<Exception> exs = new List<Exception>();
            DTOEdiversaFile.Tags retval;
            DTOCustomer oCustomer = oInvoice.Customer;
            DTOEan oGln = oCustomer.GLN;
            var oInterlocutor = DTOEdiversaFile.ReadInterlocutor(oGln);
            switch (oInterlocutor)
            {
                case DTOEdiversaFile.Interlocutors.sonae:
                case DTOEdiversaFile.Interlocutors.continente:
                    {
                        retval = DTOEdiversaFile.Tags.INVOIC_D_01B_UN_EAN010;
                        break;
                    }

                default:
                    {
                        retval = DTOEdiversaFile.Tags.INVOIC_D_93A_UN_EAN007;
                        break;
                    }
            }
            return retval;
        }
        public void AddException(DTOInvoiceException.Cods oCod, string sMessage = "")
        {
            DTOInvoiceException item = new DTOInvoiceException(oCod, sMessage);
            Exceptions.Add(item);
        }

        public static string Caption(List<DTOInvoice> oInvoices)
        {
            string retval = "";
            if (oInvoices.Count == 1)
                retval = string.Format("factura {0}", oInvoices.First().Num);
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("factures ");
                if (DTOInvoice.consecutivas(oInvoices))
                    sb.Append(string.Format("{0}-{1}", oInvoices.First().Num, oInvoices.Last().Num));
                else
                    foreach (DTOInvoice oinvoice in oInvoices)
                    {
                        if (oInvoices.IndexOf(oinvoice) > 0)
                            sb.Append(", ");
                        sb.Append(oinvoice.Num);
                        if (sb.Length > 30)
                        {
                            sb.Append("...");
                            break;
                        }
                    }
                retval = sb.ToString();
            }
            return retval;
        }

        public static string FullConcept(DTOInvoice oInvoice, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */, bool BlShowCliNom = true)
        {
            if (oLang == null)
                oLang = oInvoice.Lang;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (oInvoice.Serie)
            {
                case DTOInvoice.Series.rectificativa:
                    {
                        sb.Append(oLang.Tradueix("factura rectificativa", "factura rectificativa", "corrective invoice ", "fatura rectificativa"));
                        break;
                    }
                case DTOInvoice.Series.simplificada:
                    {
                        sb.Append(oLang.Tradueix("factura simplificada", "factura simplificada", "simplified invoice ", "fatura simplificada"));
                        break;
                    }
                case DTOInvoice.Series.inversionSujetoPasivo:
                    {
                        sb.Append(oLang.Tradueix("factura con Inversión del Sujeto Pasivo", "factura amb Inversió del Subjecte Passiu"));
                        break;
                    }

                default:
                    {
                        sb.Append(oLang.Tradueix("factura ", "factura ", "invoice ", "fatura "));
                        break;
                    }
            }
            sb.Append(oInvoice.Num + " ");
            sb.Append(oLang.Tradueix("del", "del", "from", "del") + " ");
            sb.Append(oInvoice.Fch.ToShortDateString() + " ");
            if (oInvoice.Total == null)
                sb.Append(oLang.Tradueix("sin cargo", "sense carrec", "free of charge", "sin cargo") + " ");
            else
            {
                sb.Append(oLang.Tradueix("por", "per", "for", "por") + " ");
                sb.Append(DTOAmt.CurFormatted(oInvoice.Total));
            }
            if (BlShowCliNom)
            {
                sb.Append(oLang.Tradueix(" de ", " de ", " for ", "de "));
                sb.Append(oInvoice.Customer.Nom);
            }
            return sb.ToString();
        }

        public static string CompactConcept(DTOInvoice oInvoice, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */, bool BlShowCliNom = true)
        {
            if (oLang == null)
                oLang = oInvoice.Lang;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (oInvoice.Serie)
            {
                case DTOInvoice.Series.rectificativa:
                    {
                        sb.Append(oLang.Tradueix("fra.R", "fra.R", "inv.R", "fra.R"));
                        break;
                    }
                case DTOInvoice.Series.simplificada:
                    {
                        sb.Append(oLang.Tradueix("fra.S", "fra.S", "inv.S", "fra.S"));
                        break;
                    }
                case DTOInvoice.Series.inversionSujetoPasivo:
                    {
                        sb.Append(oLang.Tradueix("fra.Y", "fra.Y", "inv.Y", "fra.Y"));
                        break;
                    }
                default:
                    {
                        sb.Append(oLang.Tradueix("fra ", "fra ", "inv ", "fra "));
                        break;
                    }
            }
            sb.Append(oInvoice.Num + " ");
            sb.Append(oLang.Tradueix("del", "del", "from", "del") + " ");
            sb.Append(oInvoice.Fch.ToShortDateString() + " ");
            return sb.ToString();
        }

        public static string DocAlbText(DTODelivery oDelivery, DTOLang oLang)
        {
            string StDoc = oLang.Tradueix("Albarán nº ", "Albarà numº ", "Our ref# ");
            string StFrom = oLang.Tradueix("de fecha", "de data", "from");
            string retval = StDoc + oDelivery.Id + " " + StFrom + " " + VbUtilities.Format(oDelivery.Fch, "dd/MM/yy");
            if (oDelivery.Customer.Ref.isNotEmpty())
                retval = retval + " a " + oDelivery.Customer.Ref;
            return retval;
        }

        public static string DocPdcText(DTOPurchaseOrder oPurchaseOrder, DTOLang oLang)
        {
            string StDoc = oLang.Tradueix("Pedido", "Comanda", "Order", "Encomenda");
            string StFrom = oLang.Tradueix("del", "del", "from", "del");
            string retval = StDoc + " " + oPurchaseOrder.Concept?.Trim() + " " + StFrom + " " + VbUtilities.Format(oPurchaseOrder.Fch, "dd/MM/yy");
            return retval;
        }


        public static string Filename(List<DTOInvoice> oInvoices, string sExtension = "pdf")
        {
            List<String> sNums = oInvoices.Select(x => string.Format("{0:yyyy}.{1:00000}", x.Fch, x.Num)).ToList();
            string retval = string.Format("M+O Facturas {0}.{1}", string.Join("-", sNums), sExtension);
            return retval;
        }

        public static Series SerieFromSerieyNum(string serieynum)
        {
            Series retval = Series.standard;
            if (serieynum.StartsWith("R"))
                retval = Series.rectificativa;
            else if (serieynum.StartsWith("S"))
                retval = Series.simplificada;
            return retval;
        }
        public static int NumFromSerieyNum(string serieynum)
        {
            int retval;
            if (VbUtilities.isNumeric(serieynum))
                retval = serieynum.toInteger();
            else
                retval = serieynum.Substring(1).toInteger();
            return retval;
        }

        public static string NumeroYSerie(int num, DTOInvoice.Series serie)
        {
            string retval = "";
            switch (serie)
            {
                case DTOInvoice.Series.rectificativa:
                    {
                        retval = string.Format("R{0}", num);
                        break;
                    }
                case DTOInvoice.Series.simplificada:
                    {
                        retval = string.Format("S{0}", num);
                        break;
                    }

                case DTOInvoice.Series.inversionSujetoPasivo:
                    {
                        retval = string.Format("Y{0}", num);
                        break;
                    }

                default:
                    {
                        retval = num.ToString();
                        break;
                    }
            }
            return retval;
        }

        public string NumeroYSerie()
        {
            string retval = DTOInvoice.NumeroYSerie(this.Num, this.Serie);
            return retval;
        }

        public static void setImports(ref DTOInvoice oInvoice, List<DTOTax> oTaxs)
        {
            {
                var withBlock = oInvoice;
                withBlock.BaseImponible = DTOInvoice.calcBaseImponible(oInvoice);
                withBlock.IvaBaseQuotas = DTOInvoice.getIvaBaseQuotas(oInvoice, oTaxs);

                withBlock.Total = withBlock.BaseImponible.Clone();

                foreach (DTOTaxBaseQuota oIvaBaseQuota in withBlock.IvaBaseQuotas)
                {
                    DTOAmt oAmt = DTOTax.quota(oIvaBaseQuota.baseImponible, oIvaBaseQuota.tax);
                    withBlock.Total.Add(oAmt);
                }
            }
        }

        public static bool isIVAExento(DTOInvoice oInvoice)
        {
            bool retval = false;
            if (oInvoice.IvaBaseQuotas != null)
                retval = oInvoice.IvaBaseQuotas.Count == 0;
            return retval;
        }

        public static void setRepComLiquidables(ref DTOInvoice oInvoice)
        {
            List<DTORepComLiquidable> retval = new List<DTORepComLiquidable>();
            foreach (DTODelivery oDelivery in oInvoice.Deliveries)
            {
                foreach (DTODeliveryItem item in oDelivery.Items)
                {
                    if (item.RepCom != null)
                    {
                        DTORepComLiquidable oRepComLiquidable = retval.Find(x => x.Rep.Equals(item.RepCom.Rep));
                        if (oRepComLiquidable == null)
                        {
                            oRepComLiquidable = new DTORepComLiquidable();
                            {
                                var withBlock = oRepComLiquidable;
                                withBlock.Rep = item.RepCom.Rep;
                                withBlock.Fra = oInvoice;
                                withBlock.BaseFras = DTOAmt.Empty();
                                withBlock.Comisio = DTOAmt.Empty();
                                withBlock.Items = new List<DTODeliveryItem>();
                            }
                            retval.Add(oRepComLiquidable);
                        }

                        {
                            var withBlock = oRepComLiquidable;
                            withBlock.BaseFras.Add(item.Import());
                            DTOAmt oComisio = item.Import().Percent(item.RepCom.Com);
                            withBlock.Comisio.Add(oComisio);
                            withBlock.Items.Add(item);
                        }
                    }
                }
            }
            oInvoice.RepComLiquidables = retval;
        }


        public static DTOPgcPlan.Ctas CtaDeb(DTOCustomer.CashCodes oCashCod)
        {
            DTOPgcPlan.Ctas retval = DTOPgcPlan.Ctas.Clients;
            switch (oCashCod)
            {
                case DTOCustomer.CashCodes.transferenciaPrevia:
                case DTOCustomer.CashCodes.visa:
                    {
                        retval = DTOPgcPlan.Ctas.Clients_Anticips;
                        break;
                    }

                case DTOCustomer.CashCodes.diposit:
                    {
                        retval = DTOPgcPlan.Ctas.DipositIrrevocableDeCompra;
                        break;
                    }
            }
            return retval;
        }

        public static DTOPgcPlan.Ctas ctaHab(DTOInvoice oInvoice)
        {
            DTOPgcPlan.Ctas retval = DTOPgcPlan.Ctas.Vendes;
            if (oInvoice.BaseImponible.IsNegative())
                retval = DTOPgcPlan.Ctas.DevolucionsDeVendes;
            return retval;
        }

        public static bool isCredit(DTOInvoice oInvoice)
        {
            bool retval = true;
            switch (oInvoice.Cfp)
            {
                case DTOPaymentTerms.CodsFormaDePago.comptat:
                case DTOPaymentTerms.CodsFormaDePago.diposit:
                case DTOPaymentTerms.CodsFormaDePago.transfPrevia:
                    {
                        retval = false;
                        break;
                    }
            }
            return retval;
        }

        public bool IsConsumer()
        {
            bool retval = this.Customer != null && this.Customer.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.consumidor));
            return retval;
        }

        public static void setNumberAndFch(ref DTOInvoice oInvoice, ref List<DTOInvoice> oPreviousInvoicesEachSerie, DateTime DtMinFch)
        {
            //DateTimeOffset DtLastDeliveryFch = oInvoice.Deliveries.Max(x => x.Fch);
            DateTime DtLastDeliveryFch = oInvoice.Deliveries.Max(x => x.Fch).Date;

            if (oInvoice.Serie == Series.inversionSujetoPasivo)
            {
                DTOInvoice oPreviousInvoice = oPreviousInvoicesEachSerie.FirstOrDefault(x => x.Serie == DTOInvoice.Series.inversionSujetoPasivo);
                if (oPreviousInvoice == null)
                {
                    oInvoice.Num = 1;
                    oInvoice.Fch = new[] { DtMinFch, DtLastDeliveryFch }.Max();
                    oPreviousInvoicesEachSerie.Add(oInvoice);
                }
                else
                {
                    oInvoice.Num = oPreviousInvoice.Num + 1;
                    oInvoice.Fch = new[] { DtMinFch, DtLastDeliveryFch, oPreviousInvoice.Fch }.Max();
                    //oPreviousInvoice = oInvoice;
                    int idx = oPreviousInvoicesEachSerie.IndexOf(oPreviousInvoice);
                    oPreviousInvoicesEachSerie[idx] = oInvoice;
                }
            }
            else if (oInvoice.BaseImponible.IsNegative())
            {
                oInvoice.Serie = DTOInvoice.Series.rectificativa;
                if (oInvoice.IsConsumer())
                    oInvoice.TipoFactura = "R5";

                DTOInvoice oPreviousInvoice = oPreviousInvoicesEachSerie.FirstOrDefault(x => x.Serie == DTOInvoice.Series.rectificativa);
                if (oPreviousInvoice == null)
                {
                    oInvoice.Num = 1;
                    oInvoice.Fch = new[] { DtMinFch, DtLastDeliveryFch }.Max();
                    oPreviousInvoicesEachSerie.Add(oInvoice);
                }
                else
                {
                    oInvoice.Num = oPreviousInvoice.Num + 1;
                    oInvoice.Fch = new[] { DtMinFch, DtLastDeliveryFch, oPreviousInvoice.Fch }.Max();
                    //oPreviousInvoice = oInvoice;
                    int idx = oPreviousInvoicesEachSerie.IndexOf(oPreviousInvoice);
                    oPreviousInvoicesEachSerie[idx] = oInvoice;

                }
            }
            else if (oInvoice.IsConsumer())
            {
                oInvoice.Serie = DTOInvoice.Series.simplificada;
                DTOInvoice oPreviousInvoice = oPreviousInvoicesEachSerie.FirstOrDefault(x => x.Serie == DTOInvoice.Series.simplificada);
                if (oPreviousInvoice == null)
                {
                    oInvoice.Num = 1;
                    oInvoice.Fch = new[] { DtMinFch, DtLastDeliveryFch }.Max();
                    oPreviousInvoicesEachSerie.Add(oInvoice);
                }
                else
                {
                    oInvoice.Num = oPreviousInvoice.Num + 1;
                    oInvoice.Fch = new[] { DtMinFch, DtLastDeliveryFch, oPreviousInvoice.Fch }.Max();
                    //oPreviousInvoice = oInvoice;
                    int idx = oPreviousInvoicesEachSerie.IndexOf(oPreviousInvoice);
                    oPreviousInvoicesEachSerie[idx] = oInvoice;
                }
            }
            else
            {
                oInvoice.Serie = DTOInvoice.Series.standard;
                DTOInvoice oPreviousInvoice = oPreviousInvoicesEachSerie.FirstOrDefault(x => x.Serie == DTOInvoice.Series.standard);
                if (oPreviousInvoice == null)
                {
                    oInvoice.Num = 1;
                    oInvoice.Fch = new[] { DtMinFch, DtLastDeliveryFch }.Max();
                    oPreviousInvoicesEachSerie.Add(oInvoice);
                }
                else
                {
                    oInvoice.Num = oPreviousInvoice.Num + 1;
                    oInvoice.Fch = new[] { DtMinFch, DtLastDeliveryFch, oPreviousInvoice.Fch }.Max();
                    int idx = oPreviousInvoicesEachSerie.IndexOf(oPreviousInvoice);
                    oPreviousInvoicesEachSerie[idx] = oInvoice;
                }
            }
        }

        public static DTOCustomer.CashCodes cashCod(DTOInvoice oInvoice)
        {
            DTOCustomer.CashCodes retval = DTOCustomer.CashCodes.notSet;
            if (oInvoice.Deliveries != null)
            {
                if (oInvoice.Deliveries.Count > 0)
                {
                    DTODelivery oFirstDelivery = oInvoice.Deliveries.First();
                    retval = oFirstDelivery.CashCod;
                }
            }
            return retval;
        }
        public static DTOAmt calcBaseImponible(DTOInvoice oInvoice)
        {
            decimal DcEur = oInvoice.Deliveries.Sum(x => x.Items.Sum(y => DTOAmt.import(y.Qty, y.Price, y.Dto).Eur));
            DTOAmt retval = DTOAmt.Factory(DcEur);
            return retval;
        }

        public static string GuessClauL9ExempcioIva(DTOInvoice oInvoice)
        {
            string retval = "";
            DTOInvoice.ExportCods ExportCods = DTOContact.ExportCod(oInvoice.Customer);
            switch (ExportCods)
            {
                case DTOInvoice.ExportCods.intracomunitari:
                    {
                        retval = "E5";
                        break;
                    }

                case DTOInvoice.ExportCods.extracomunitari:
                    {
                        retval = "E2";
                        break;
                    }
            }
            return retval;
        }

        public static void SetRegimenEspecialOTrascendencia(DTOInvoice oInvoice)
        {
            if (string.IsNullOrEmpty(oInvoice.RegimenEspecialOTrascendencia))
            {
                DateTime DtFch = oInvoice.Fch;
                if (DtFch.Year == 2017 & DtFch.Month <= 6)
                    oInvoice.RegimenEspecialOTrascendencia = "16";
                else
                {
                    DTOCustomer oCustomer = oInvoice.Customer;
                    switch (oInvoice.ExportCod)
                    {
                        case DTOInvoice.ExportCods.nacional:
                        case DTOInvoice.ExportCods.intracomunitari:
                            {
                                oInvoice.RegimenEspecialOTrascendencia = "01";
                                break;
                            }

                        case DTOInvoice.ExportCods.extracomunitari:
                            {
                                oInvoice.RegimenEspecialOTrascendencia = "02";
                                break;
                            }
                    }
                }
            }
        }

        public static List<DTOTaxBaseQuota> getIvaBaseQuotas(DTOInvoice oInvoice, List<DTOTax> oTaxs)
        {
            List<DTOTaxBaseQuota> retval = new List<DTOTaxBaseQuota>();

            {
                if (oInvoice.Customer.Iva && oInvoice.Serie != Series.inversionSujetoPasivo)
                {
                    oInvoice.TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.sujetoNoExento;
                    foreach (var oDelivery in oInvoice.Deliveries)
                    {
                        foreach (DTODeliveryItem oItm in oDelivery.Items)
                        {
                            DTOTaxBaseQuota oIvaBaseQuota = retval.Find(x => x.tax.codi == oItm.IvaCod);
                            if (oIvaBaseQuota == null)
                            {
                                DTOTax oTax = oTaxs.Find(x => x.codi == oItm.IvaCod);
                                oIvaBaseQuota = new DTOTaxBaseQuota(oTax, oItm.Import());
                                retval.Add(oIvaBaseQuota);
                                if (oInvoice.Customer.Req)
                                {
                                    DTOTax.Codis oTaxReqCodi = DTOTax.reqForIvaCod(oIvaBaseQuota.tax.codi);
                                    DTOTax oTaxReq = oTaxs.Find(x => x.codi == oTaxReqCodi);
                                    DTOTaxBaseQuota oReqBaseQuota = new DTOTaxBaseQuota(oTaxReq, oItm.Import());
                                    retval.Add(oReqBaseQuota);
                                }
                            }
                            else
                            {
                                oIvaBaseQuota.addBase(oItm.Import());
                                if (oInvoice.Customer.Req)
                                {
                                    DTOTax.Codis oTaxReqCodi = DTOTax.reqForIvaCod(oIvaBaseQuota.tax.codi);
                                    DTOTaxBaseQuota oReqBaseQuota = retval.Find(x => x.tax.codi == oTaxReqCodi);
                                    oReqBaseQuota.addBase(oItm.Import());
                                }
                            }
                        }
                    }
                }
                else
                {
                    oInvoice.TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.sujetoExento;
                    if (oInvoice.SiiL9 == "")
                        oInvoice.SiiL9 = DTOInvoice.GuessClauL9ExempcioIva(oInvoice);
                }
            }
            return retval;
        }

        public static void setVto(ref DTOInvoice oInvoice)
        {
            DTOCustomer oCcx = oInvoice.Customer.CcxOrMe();
            if (oCcx.PaymentTerms == null)
                oInvoice.AddException(DTOInvoiceException.Cods.missingPaymentTerms, "falta forma de pagament");
            else
                oInvoice.Vto = DTOPaymentTerms.Vto(oCcx.PaymentTerms, oInvoice.Fch);
        }

        public static bool consecutivas(List<DTOInvoice> oInvoices)
        {
            bool retval = true;
            if (oInvoices.Count <= 1)
                retval = true;
            else
            {
                List<DTOInvoice> oSortedInvoices = oInvoices.OrderBy(x => x.Num).ToList();
                int iNum = oSortedInvoices.First().Num;
                for (int i = 1; i <= oSortedInvoices.Count - 1; i++)
                {
                    iNum += 1;
                    if (oSortedInvoices[i].Num != iNum)
                    {
                        retval = false;
                        break;
                    }
                }
            }
            return retval;
        }

        public static bool sameCustomer(List<DTOInvoice> oInvoices)
        {
            bool retval = oInvoices.All(x => x.Customer.Equals(oInvoices.First().Customer));
            return retval;
        }

        public static DTOInvoice.Conceptes getConcepte(DTODelivery oDelivery)
        {
            DTOInvoice.Conceptes retval = DTOInvoice.Conceptes.notSet;
            switch (oDelivery.Cod)
            {
                case DTOPurchaseOrder.Codis.reparacio:
                    {
                        retval = DTOInvoice.Conceptes.servicios;
                        break;
                    }

                default:
                    {
                        retval = DTOInvoice.Conceptes.ventas;
                        break;
                    }
            }
            return retval;
        }

        public static string lastPrintedText(DTOInvoice oInvoice)
        {
            string retval = "";
            List<Exception> exs = new List<Exception>();
            switch (oInvoice.PrintMode)
            {
                case DTOInvoice.PrintModes.pending:
                    {
                        retval = "pendent de imprimir";
                        break;
                    }

                case DTOInvoice.PrintModes.noPrint:
                    {
                        retval = "no imprimible";
                        break;
                    }

                case DTOInvoice.PrintModes.printer:
                    {
                        retval = string.Format("impresa en paper el {0:dd/MM/yy} a les {0:hh:mm} per {1}", oInvoice.FchLastPrinted, DTOUser.NicknameOrElse(oInvoice.UserLastPrinted));
                        break;
                    }

                case DTOInvoice.PrintModes.email:
                    {
                        retval = string.Format("enviada per email el {0:dd/MM/yy} a les {0:hh:mm} per {1}", oInvoice.FchLastPrinted, DTOUser.NicknameOrElse(oInvoice.UserLastPrinted));
                        break;
                    }

                case DTOInvoice.PrintModes.edi:
                    {
                        retval = string.Format("enviada per EDI el {0:dd/MM/yy} a les {0:hh:mm} per {1}", oInvoice.FchLastPrinted, DTOUser.NicknameOrElse(oInvoice.UserLastPrinted));
                        break;
                    }
            }
            return retval;
        }

        public static string formattedId(DTOInvoice oInvoice)
        {
            string retval = string.Format("{0:yyyy}{1:00000}", oInvoice.Fch, oInvoice.Num);
            return retval;
        }

        public static string caption(DTOInvoice oInvoice, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOApp.Current.Lang;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(oLang.Tradueix("Factura ", "Factura ", "Invoice "));
            sb.Append(oInvoice.Num);
            sb.Append(oLang.Tradueix(" del ", " del ", " from ") + oInvoice.Fch.ToShortDateString());
            string retval = sb.ToString();
            return retval;
        }

        public static bool discountExists(DTOInvoice oInvoice)
        {
            bool retval = false;
            foreach (DTODelivery oDelivery in oInvoice.Deliveries)
            {
                foreach (DTODeliveryItem item in oDelivery.Items)
                {
                    if (item.Dto != 0)
                    {
                        retval = true;
                        break;
                    }
                }
            }
            return retval;
        }

        public static DTOAmt sumaDeImportes(DTOInvoice oInvoice)
        {
            var retval = DTOAmt.Empty();
            foreach (DTODelivery oDelivery in oInvoice.Deliveries)
            {
                foreach (DTODeliveryItem oItem in oDelivery.Items)
                    retval.Add(oItem.Import());
            }
            return retval;
        }


        public static DTOAmt getBaseImponible(DTOInvoice oInvoice)
        {
            DTOAmt retval = DTOInvoice.sumaDeImportes(oInvoice);
            // afegir descomptes globals
            // afegir recarrecs globals
            return retval;
        }

        public static DTOAmt ivaBase(DTOInvoice oInvoice)
        {
            // If oInvoice.Customer.Nif.StartsWith("PT") Then Stop
            var retval = DTOAmt.Empty();
            if (oInvoice.IvaBaseQuotas != null)
            {
                decimal DcEur = oInvoice.IvaBaseQuotas.Where(x => x.tax.codi == DTOTax.Codis.exempt | x.tax.codi == DTOTax.Codis.iva_Standard | x.tax.codi == DTOTax.Codis.iva_Reduit | x.tax.codi == DTOTax.Codis.iva_SuperReduit).Sum(y => y.baseImponible.Eur);
                retval = DTOAmt.Factory(DcEur);
            }
            return retval;
        }

        public static DTOAmt getIvaAmt(DTOInvoice oInvoice)
        {
            var retval = DTOAmt.Empty();
            if (oInvoice.IvaBaseQuotas != null)
            {
                decimal DcEur = oInvoice.IvaBaseQuotas.Where(x => x.tax.codi == DTOTax.Codis.iva_Standard | x.tax.codi == DTOTax.Codis.iva_Reduit | x.tax.codi == DTOTax.Codis.iva_SuperReduit).Sum(y => y.quota.Eur);
                retval = DTOAmt.Factory(DcEur);
            }
            return retval;
        }

        public static decimal ivaTipus(DTOInvoice oInvoice)
        {
            decimal retval = 0;
            if (oInvoice.IvaBaseQuotas != null)
            {
                List<DTOTaxBaseQuota> oQuotas = oInvoice.IvaBaseQuotas.Where(x => x.tax.codi == DTOTax.Codis.iva_Standard | x.tax.codi == DTOTax.Codis.iva_Reduit | x.tax.codi == DTOTax.Codis.iva_SuperReduit).ToList();

                if (oQuotas.Count > 0)
                {
                    bool MixedTaxes = oQuotas.Any(x => x.tax.codi != oQuotas.First().tax.codi);
                    if (!MixedTaxes)
                        retval = oQuotas.First().tax.tipus;
                }
            }
            return retval;
        }

        public static DTOAmt getReqAmt(DTOInvoice oInvoice)
        {
            var retval = DTOAmt.Empty();
            if (oInvoice.IvaBaseQuotas != null)
            {
                decimal DcEur = oInvoice.IvaBaseQuotas.Where(x => x.tax.codi == DTOTax.Codis.recarrec_Equivalencia_Standard | x.tax.codi == DTOTax.Codis.recarrec_Equivalencia_Reduit | x.tax.codi == DTOTax.Codis.recarrec_Equivalencia_SuperReduit).Sum(y => y.quota.Eur);
                retval = DTOAmt.Factory(DcEur);
            }
            return retval;
        }

        public static decimal reqTipus(DTOInvoice oInvoice)
        {
            decimal retval = 0;
            if (oInvoice.IvaBaseQuotas != null)
            {
                List<DTOTaxBaseQuota> oQuotas = oInvoice.IvaBaseQuotas.Where(x => x.tax.codi == DTOTax.Codis.recarrec_Equivalencia_Standard | x.tax.codi == DTOTax.Codis.recarrec_Equivalencia_Reduit | x.tax.codi == DTOTax.Codis.recarrec_Equivalencia_SuperReduit).ToList();

                if (oQuotas.Count > 0)
                {
                    bool MixedTaxes = oQuotas.Any(x => x.tax.codi != oQuotas.First().tax.codi);
                    if (!MixedTaxes)
                        retval = oQuotas.First().tax.tipus;
                }
            }
            return retval;
        }

        public static string taxText(DTOInvoice oInvoice, DTOTax.Codis oCod)
        {
            string retval = "";
            switch (oCod)
            {
                case DTOTax.Codis.iva_Standard:
                    {
                        retval = oInvoice.Lang.Tradueix("IVA", "IVA", "VAT") + " " + oInvoice.Iva + "%";
                        break;
                    }

                case DTOTax.Codis.recarrec_Equivalencia_Standard:
                    {
                        retval = oInvoice.Lang.Tradueix("Recargo de Equivalencia", "Recàrrec d'equivalència", "VAT Equivalence Surcharge") + " " + oInvoice.Req + "%";
                        break;
                    }
            }
            return retval;
        }

        public static DTOAmt taxAmt(DTOInvoice oInvoice, DTOTax.Codis oCod)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            DTOAmt oBase = DTOInvoice.getBaseImponible(oInvoice);
            switch (oCod)
            {
                case DTOTax.Codis.iva_Standard:
                    {
                        retval = oBase.Percent(oInvoice.Iva);
                        break;
                    }

                case DTOTax.Codis.recarrec_Equivalencia_Standard:
                    {
                        retval = oBase.Percent(oInvoice.Req);
                        break;
                    }
            }
            return retval;
        }

        public static DTOAmt sumaDeImpuestos(DTOInvoice oInvoice)
        {
            DTOAmt oBase = DTOInvoice.getBaseImponible(oInvoice);
            var retval = DTOAmt.Empty();

            if (oInvoice.Iva != 0)
            {
                DTOAmt oIva = oBase.Percent(oInvoice.Iva);
                retval.Add(oIva);
                if (oInvoice.Req != 0)
                {
                    DTOAmt oReq = oBase.Percent(oInvoice.Req);
                    retval.Add(oReq);
                }
            }
            return retval;
        }

        public static DTOAmt getTotal(DTOInvoice oInvoice)
        {
            DTOAmt retval = DTOInvoice.getBaseImponible(oInvoice);
            retval.Add(DTOInvoice.sumaDeImpuestos(oInvoice));
            return retval;
        }

        public static DTODoc doc(DTOInvoice oInvoice)
        {
            List<Exception> exs = new List<Exception>();
            // FEBL.Invoice.Load(oInvoice, exs)

            // Dim BlInsertEans As Boolean = Me.Client.Nom.Contains("AMAZON") 'to deprecate quan li factuirem via EDI

            var oLang = oInvoice.Lang;
            DTODoc oDoc = new DTODoc(DTODoc.Estilos.factura, oLang, oInvoice.Total.Cur);
            DTOCustomer oCustomer = oInvoice.Customer;
            {
                var withBlock = oDoc;
                if (oInvoice.Serie == DTOInvoice.Series.rectificativa)
                    oDoc.sideLabel = DTODoc.SideLabels.facturaRectificativa;

                oDoc.ExportCod = oInvoice.ExportCod;
                oDoc.incoterm = oInvoice.Incoterm;


                DTOAddress address = new DTOAddress();
                address.Text = oInvoice.Adr;
                address.Zip = oInvoice.Zip;
                oDoc.dest.Add(oInvoice.Nom);
                //if (oCustomer.NomComercial.isNotEmpty())
                //    oDoc.dest.Add(oCustomer.NomComercial);

                foreach (DTONif nif in oInvoice.Nifs)
                    oDoc.dest.Add(nif.QualifiedValue(oLang));

                oDoc.customLines = new List<string>();

                DTOZona oZona = DTOAddress.Zona(address);
                if (DTOZona.IsCanarias(oZona) && DTOInvoice.getBaseImponible(oInvoice).Eur <= 3000)
                    oDoc.customLines.Add("T2LF - MERCANCIA SIN DECLARACIÓN DE EXPEDICIÓN");

                foreach (string sl in address.Text.toLinesList())
                {
                    if (sl.isNotEmpty())
                        oDoc.dest.Add(sl.Trim());
                }

                oDoc.dest.Add(DTOAddress.ZipyCit(address));
                if (address.IsEsp())
                    oDoc.dest.Add(DTOAddress.ProvinciaOPais(oInvoice.Zip));


                oDoc.fch = oInvoice.Fch;
                oDoc.num = oInvoice.NumeroYSerie();
                oDoc.obs.Add(oInvoice.Fpg);

                if (oInvoice.Ob1.isNotEmpty())
                    oDoc.obs.Add(oInvoice.Ob1);
                if (oInvoice.Ob2.isNotEmpty())
                    oDoc.obs.Add(oInvoice.Ob2);
                if (oInvoice.Ob3.isNotEmpty())
                    oDoc.obs.Add(oInvoice.Ob3);
                if (oCustomer.SuProveedorNum.isNotEmpty())
                    oDoc.obs.Add(oLang.Tradueix("Proveedor num.", "Proveidor num.", "Supplier code ") + oCustomer.SuProveedorNum);
                if (oInvoice.ExportCod == ExportCods.extracomunitari)
                    oDoc.obs.Add(oLang.Tradueix("Exento de Iva (art. 21.2º Ley 37/1992)", "Exempt de Iva (art. 21.2º Llei 37/1992)", "VAT exemption (art. 21.2º Ley de IVA)"));
                if ((int)oInvoice.Cfp != 3 & oInvoice.Total != null)
                    oDoc.obs.Add(oLang.Tradueix("empresa asociada a ASNEF", "empresa asociada a ASNEF", "member of ASNEF"));
                //oDoc.incoterm = oCustomer.Incoterm;
                // .Dto = oInvoice.Dto
                // .PuntsQty = mPuntsQty
                // .PuntsTipus = mPuntsTipus
                // .PuntsBase = mPuntsBase
                // .Dpp = mDppPct
                oDoc.ivaBaseQuotas = oInvoice.IvaBaseQuotas;
                oDoc.recarrecEquivalencia = oDoc.ivaBaseQuotas.Exists(x => x.tax.codi == DTOTax.Codis.recarrec_Equivalencia_Standard);
                foreach (DTODelivery oDelivery in oInvoice.Deliveries)
                {
                    oDoc.itms.Add(new DTODocItm(IntMinLinesBeforeEndPage: 4));
                    oDoc.itms.Add(new DTODocItm(DTOInvoice.DocAlbText(oDelivery, oLang), DTODoc.FontStyles.bold));
                    DTOPurchaseOrder oPurchaseOrder = new DTOPurchaseOrder();
                    DTOSpv oSpv = new DTOSpv();
                    foreach (var itm in oDelivery.Items)
                    {
                        if (oDelivery.Cod == DTOPurchaseOrder.Codis.client)
                        {
                            if (!itm.PurchaseOrderItem.PurchaseOrder.Equals(oPurchaseOrder))
                            {
                                oPurchaseOrder = itm.PurchaseOrderItem.PurchaseOrder;
                                oDoc.itms.Add(new DTODocItm(DTOInvoice.DocPdcText(oPurchaseOrder, oLang), DTODoc.FontStyles.italic, IntLeftPadChars: 2, IntMinLinesBeforeEndPage: 2));
                            }
                        }
                        if (oDelivery.Cod == DTOPurchaseOrder.Codis.reparacio)
                        {
                            if (!itm.Spv.Equals(oSpv))
                            {
                                int i;
                                oSpv = itm.Spv;
                                List<string> oSpvTextArray = oSpv.lines(oLang);
                                int iSpvTextLines = oSpvTextArray.Count;
                                for (i = 0; i <= iSpvTextLines - 1; i++)
                                    oDoc.itms.Add(new DTODocItm(oSpvTextArray[i], DTODoc.FontStyles.italic, IntMinLinesBeforeEndPage: iSpvTextLines - i + 2));
                            }
                        }

                        string sSku = oLang.Equals(DTOLang.ENG()) ? DTOProductSku.refYNomPrv(itm.Sku) : itm.Sku.RefYNomLlarg().Tradueix(oLang);
                        // If BlInsertEans And itm.Art.Cbar IsNot Nothing Then
                        // sSku = itm.Art.Cbar.Value & " " & sSku
                        // End If

                        oDoc.itms.Add(new DTODocItm(sSku, DTODoc.FontStyles.regular, itm.Qty, itm.Price, itm.Dto, 0, 4, Hyperlink: DTOProductSku.urlSegment(itm.Sku)));

                        if (itm.Bundle.Count > 0)
                        {
                            oDoc.itms.Add(new DTODocItm(oLang.Tradueix("compuesto de los siguientes elementos:", "compost dels següents elements", "composed of the following elements:"), DTODoc.FontStyles.italic, IntMinLinesBeforeEndPage: 6));
                            foreach (var oChildItem in itm.Bundle)
                            {
                                var sSkuNom = oChildItem.Sku.RefYNomLlarg().Tradueix(oLang);
                                if (oDelivery.Cod == DTOPurchaseOrder.Codis.client && oLang.Equals(DTOLang.ENG()))
                                    sSkuNom = oChildItem.Sku.refYNomPrv();
                                var oDocItm = new DTODocItm(sSkuNom, IntMinLinesBeforeEndPage: 4);
                                oDocItm.LeftPadChars = 12;
                                oDoc.itms.Add(oDocItm);
                            }
                        }
                    }
                }
            }
            return oDoc;
        }

        public string EdiMessage(DTOEmp emp)
        {
            string departamento = "";
            DTODelivery firstDelivery = Deliveries.First();

            DTOContact oComprador = firstDelivery.Customer;
            oComprador.Address = firstDelivery.Address;

            DTOContact oFacturarA = Customer;
            oFacturarA.Address = new DTOAddress();
            oFacturarA.Nom = Nom;
            oFacturarA.Address.Text = Adr;
            oFacturarA.Address.Zip = Zip;
            oFacturarA.Nifs = Nifs;

            DTOContact oPlatform = firstDelivery.Platform == null ? oComprador : firstDelivery.Platform;
            DTOEdiInvoic ediInvoic = DTOEdiInvoic.Factory(emp.Org.GLN.Value, this.Customer.GLN.Value);
            ediInvoic.InvoiceNumber = NumeroYSerie();
            ediInvoic.InvoiceFch = Fch;
            ediInvoic.NadBy = Interlocutor(oComprador, "", departamento);
            ediInvoic.NadIv = Interlocutor(oFacturarA);
            ediInvoic.NadBco = Interlocutor(oFacturarA);
            ediInvoic.NadSu = Interlocutor(emp.Org, emp.DadesRegistrals);
            ediInvoic.NadSco = Interlocutor(emp.Org, emp.DadesRegistrals);
            ediInvoic.NadII = Interlocutor(emp.Org);
            ediInvoic.NadDp = Interlocutor(oPlatform);
            ediInvoic.IvaTipus = Iva;
            ediInvoic.RecarrecEquivalenciaTipus = Req;
            foreach (DTODelivery delivery in Deliveries)
            {
                foreach (DTODeliveryItem deliveryItem in delivery.Items)
                {
                    DTOPurchaseOrderItem pnc = deliveryItem.PurchaseOrderItem;
                    DTOEdiInvoic.Item item = new DTOEdiInvoic.Item();
                    item.DeliveryNumber = delivery.Formatted();
                    item.DeliveryFch = delivery.Fch;
                    item.PurchaseOrderNumber = pnc.PurchaseOrder.Concept;
                    item.PurchaseOrderFch = pnc.PurchaseOrder.Fch;
                    item.Ean = DTOEan.eanValue(deliveryItem.Sku.Ean13);
                    item.SkuId = deliveryItem.Sku.Id.ToString();
                    item.SkuNom = deliveryItem.Sku.RefYNomLlarg().Esp;
                    item.Qty = deliveryItem.Qty;
                    item.GrossPrice = deliveryItem.Price.Eur;
                    item.Dto = deliveryItem.Dto;
                    ediInvoic.Items.Add(item);
                }
            }
            string retval = ediInvoic.EdiMessage();
            return retval;
        }

        private DTOEdiInvoic.Interlocutor Interlocutor(DTOContact contact, string dadesRegistrals = "", string departamento = "")
        {
            DTOEdiInvoic.Interlocutor retval = new DTOEdiInvoic.Interlocutor();
            retval.Ean = DTOEan.eanValue(contact.GLN);
            retval.Nom = contact.Nom;
            retval.Address = contact.Address.Text;
            retval.Location = DTOAddress.LocationFullNom(contact.Address, DTOLang.ESP());
            retval.Zip = DTOAddress.ZipCod(contact.Address);
            retval.Nif = contact.PrimaryNifValue();
            retval.RegistroMercantil = dadesRegistrals;
            retval.Departamento = departamento;
            return retval;
        }
    }


    public class DTOInvoiceException : Exception
    {
        public Cods cod { get; set; }

        public enum Cods
        {
            _notSet,
            wrongNif,
            multipleDeliveries,
            missingPaymentTerms,
            missingIban,
            uncompleteBank,
            obsTooLong,
            missingConcept,
            missingTipoFactura,
            siiLogged
        }

        public DTOInvoiceException(Cods oCod, string sMessage = "") : base(sMessage)
        {
            cod = oCod;
        }

        public static string multilineString(List<DTOInvoiceException> oInvoiceExceptions)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DTOInvoiceException item in oInvoiceExceptions)
                sb.AppendLine(item.Message);
            string retval = sb.ToString();
            return retval;
        }


    }
}
