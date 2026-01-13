using EdiHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTODelivery : DTOBaseGuid
    {
        public class Compact : DTOBaseGuid
        {
            public int Id { get; set; }
            public DateTime Fch { get; set; }
            public DTOGuidNom Customer { get; set; }
            public DTOGuidNom Transportista { get; set; }
            public string Tracking { get; set; }
            public DTOAmt.Compact Import { get; set; }
            public DTOInvoice.Compact Invoice { get; set; }
            public DTOPurchaseOrder.Codis Cod { get; set; }
            public DTOCustomer.PortsCodes PortsCod { get; set; }
            public DTOCustomer.CashCodes CashCod { get; set; }
            public DTODelivery.RetencioCods RetencioCod { get; set; }
            public DTOAmt.Compact ImportAdicional { get; set; } // Import adicional que es carrega sobre el de l'albarà per cobrar imports pendents a l'hora
            public DateTime FchCobroReembolso { get; set; }
            public bool Valorado { get; set; }
            public bool Facturable { get; set; }

            public string ObsTransp { get; set; }
            public DTOTransmisio.Compact Transmisio { get; set; }
            public DTOUsrLog2 UsrLog { get; set; }

            public Compact() : base()
            {
            }

            public Compact(Guid oGuid) : base(oGuid)
            {
            }



        }


        public DTOEmp Emp { get; set; }
        public int Id { get; set; }
        public DTOPurchaseOrder.Codis Cod { get; set; }
        public DateTime Fch { get; set; }
        public DTOAmt Import { get; set; }
        public decimal Kg { get; set; }
        public decimal M3 { get; set; }
        public int Bultos { get; set; }

        public DTOMgz Mgz { get; set; }
        public DTOCustomerPlatform Platform { get; set; }
        public DTOTransportista Transportista { get; set; }
        public string Tracking { get; set; }
        public string TrackingUrl { get; set; }
        public int CubicKg { get; set; }
        public DTOAmt Ports { get; set; }
        public bool IvaExempt { get; set; }
        public DTOAmt Recarrec { get; set; }
        public string CustomerDocURL { get; set; }
        public DTOTransmisio Transmisio { get; set; }

        public string NADMR { get; set; } //NADMS Operational point who sent the Edi order message, to send him the DESADV as NADMR
        public DTOInvoice Invoice { get; set; }

        public DTOCustomer Customer { get; set; }
        public DTOCustomer FacturarA { get; set; }
        public Guid Deutor { get; set; }
        public DTOProveidor Proveidor { get; set; }
        public string Nom { get; set; }
        public DTOAddress Address { get; set; }
        public string Tel { get; set; }

        public DTOCustomer.CashCodes CashCod { get; set; }
        public DTOCustomer.PortsCodes PortsCod { get; set; }
        public DTODelivery.RetencioCods RetencioCod { get; set; }
        public DTOAmt ImportAdicional { get; set; } // Import adicional que es carrega sobre el de l'albarà per cobrar imports pendents a l'hora
        public DateTime FchCobroReembolso { get; set; }
        public bool Valorado { get; set; }
        public bool Facturable { get; set; }
        public decimal Dto { get; set; }
        public decimal Dpp { get; set; }
        public DTOPaymentTerms PaymentTerms { get; set; }
        public DTOImportacio Importacio { get; set; }
        public DTOInvoice.ExportCods ExportCod { get; set; }
        public DTOIncoterm Incoterm { get; set; }

        public JustificanteCodes Justificante { get; set; }
        public DateTime FchJustificante { get; set; }
        public string Fpg { get; set; }
        public string Obs { get; set; }
        public string ObsTransp { get; set; }

        public DTOUsrLog2 UsrLog { get; set; }

        public List<DTODeliveryItem> Items { get; set; }
        public List<DTODelivery.Pallet> Pallets { get; set; }
        public List<DTODelivery.Package> Packages { get; set; }

        public DTO.Integracions.Vivace.Trace Trace { get; set; }
        public List<DTOPurchaseOrder> PurchaseOrders { get; set; }
        public DTODocFile EtiquetesTransport { get; set; }

        public DTOConsumerTicket ConsumerTicket { get; set; }

        public enum CodsValorat
        {
            Inherit,
            ForceTrue,
            ForceFalse
        }


        public DTODelivery() : base()
        {
            Items = new List<DTODeliveryItem>();
            Pallets = new List<DTODelivery.Pallet>();
            Packages = new List<DTODelivery.Package>();
        }

        public DTODelivery(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTODeliveryItem>();
            Pallets = new List<DTODelivery.Pallet>();
            Packages = new List<DTODelivery.Package>();
        }

        public void RestoreObjects()
        {
            foreach (DTODeliveryItem item in Items)
            {
                if (item.Spv != null)
                {
                    item.Spv.restoreObjects();
                }
            }
        }

        public enum RetencioCods
        {
            notSet = -1,
            free = 0,
            altres = 1,
            transferencia = 2,
            VISA = 3
        }

        public enum PortsQualification
        {
            notSet,
            qualified,
            lowVolume,
            notApplicable
        }

        public enum JustificanteCodes
        {
            none,
            solicitado,
            recibido
        }


        public static DTODelivery FactoryElCorteIngles(DTOPurchaseOrder oPurchaseOrder, DTOMgz oMgz, DateTime DtFch, DTOUser oUser)
        {
            DTODelivery retval = new DTODelivery();
            // evita loads per no enlentir carregues de grup

            DTOCustomer oCustomer = oPurchaseOrder.Customer;
            // If oCustomer Is Nothing Then oCustomer = DTOCustomer.FromContact(oPurchaseOrder.Contact)
            DTOCustomer oCcx = oCustomer.Ccx;
            {
                var withBlock = retval;
                withBlock.Emp = oUser.Emp;
                withBlock.Mgz = oMgz;
                withBlock.Fch = DtFch;

                withBlock.Cod = DTOPurchaseOrder.Codis.client;
                withBlock.Customer = oCustomer;
                withBlock.Nom = oCustomer.NomComercialOrDefault();
                withBlock.Fch = DTO.GlobalVariables.Today();
                withBlock.ExportCod = oCustomer.ExportCod;

                withBlock.CashCod = oCcx.CashCod;
                withBlock.Facturable = true;
                withBlock.RetencioCod = DTODelivery.RetencioCods.free;
                withBlock.Valorado = oCcx.AlbValorat;

                withBlock.PortsCod = oCcx.PortsCod;
                if (oPurchaseOrder.Platform != null)
                {
                    withBlock.Platform = oPurchaseOrder.Platform;
                    withBlock.Address = withBlock.Platform.Address;
                    withBlock.Tel = withBlock.Platform.Telefon;
                }

                withBlock.UsrLog = DTOUsrLog2.Factory(oUser);
                withBlock.Items = new List<DTODeliveryItem>();
            }
            return retval;
        }

        public DTOContact Contact
        {
            get
            {
                DTOContact retval = null/* TODO Change to default(_) if this is not a reference type */;
                if (Customer != null)
                    retval = Customer;
                else if (Proveidor != null)
                    retval = Proveidor;
                return retval;
            }
        }

        public DTOAmt Liquid
        {
            get
            {
                DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
                if (Import != null)
                    retval = Import;
                if (ImportAdicional != null)
                {
                    if (retval == null)
                        retval = ImportAdicional;
                    else
                        retval.Add(ImportAdicional);
                }
                return retval;
            }
        }

        public string followUpUrl(DTOLang lang, bool absoluteUrl)
        {
            string retval = DTOWebDomain.Factory(lang, absoluteUrl).Url("delivery/tracking", this.Guid.ToString());
            return retval;
        }

        public static DTODelivery Factory(DTOUser oUser, DTOPurchaseOrder.Codis oCod, DTOContact oContact, DateTime DtFch = default(DateTime))
        {
            // Traspas de magatzem
            if (DtFch == default(DateTime))
                DtFch = DTO.GlobalVariables.Today();
            DTODelivery retval = new DTODelivery();
            {
                retval.Cod = oCod;
                if (retval.Cod == DTOPurchaseOrder.Codis.proveidor)
                {
                    DTOProveidor proveidor = (DTOProveidor)oContact;
                    retval.Proveidor = proveidor;
                    retval.Nom = proveidor.Nom;
                    retval.Address = proveidor.Address;
                    retval.Incoterm = proveidor.IncoTerm;
                    retval.ExportCod = proveidor.Address.ExportCod();

                }
                else
                    retval.Customer = (DTOCustomer)oContact;
                retval.Fch = DtFch;
                retval.UsrLog = DTOUsrLog2.Factory(oUser);
            }
            return retval;
        }

        public List<DTOSpv> Spvs()
        {
            List<DTOSpv> retval = new List<DTOSpv>();
            foreach (var item in Items)
            {
                if (item.Spv != null)
                {
                    if (!retval.Any(x => x.Equals(item.Spv)))
                        retval.Add(item.Spv);
                }
            }
            return retval;
        }

        public string GetTrackingUrl()
        {
            string retval = "";
            if (this.Transportista != null && this.Transportista.TrackingUrlTemplate.isNotEmpty())
            {
                retval = this.Transportista.TrackingUrlTemplate.Replace("@01", this.Formatted());
            }
            else
            {
                retval = DTO.Integracions.Vivace.Vivace.TrackingUrl(this);
            }
            return retval;
        }

        public List<DTOPurchaseOrder> getPurchaseOrders()
        {
            List<DTOPurchaseOrder> retval = new List<DTOPurchaseOrder>();
            if (Items != null)
                retval = Items.Where(x => x.PurchaseOrderItem != null).GroupBy(y => y.PurchaseOrderItem.PurchaseOrder.Guid).Select(z => z.First().PurchaseOrderItem.PurchaseOrder).ToList();
            return retval;
        }


        public static string caption(DTODelivery oDelivery, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOApp.Current.Lang;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(oLang.Tradueix("Albarán ", "Albarà ", "Delivery note "));
            sb.Append(oDelivery.Id + " ");
            sb.Append(oLang.Tradueix("del ", "del ", "from ") + oDelivery.Fch.ToShortDateString());
            string retval = sb.ToString();
            return retval;
        }

        public string Formatted()
        {
            string retVal = VbUtilities.Format(Fch.Year, "0000") + VbUtilities.Format(Id, "000000");
            return retVal;
        }

        public bool hasSameAddressOf(DTODelivery oCandidate)
        {
            bool retval = false;
            if (Nom == oCandidate.Nom)
            {
                if (Address.Equals(oCandidate.Address))
                    retval = true;
            }
            return retval;
        }

        public static DateTime pickUpDeadline(DateTime DtPickupfchfrom)
        {
            DateTime retval;
            switch (DtPickupfchfrom.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    {
                        retval = DtPickupfchfrom.AddDays(3);
                        break;
                    }

                default:
                    {
                        retval = DtPickupfchfrom.AddDays(1);
                        break;
                    }
            }
            return retval;
        }

        public static DTOEan deliveryGLN(DTODelivery oDelivery)
        {
            DTOEan retVal = null/* TODO Change to default(_) if this is not a reference type */;
            if (oDelivery.Platform == null)
                retVal = oDelivery.Customer.GLN;
            else
                retVal = oDelivery.Platform.GLN;
            return retVal;
        }


        public void EnumerateLines()
        {
            int lin = 0;
            if (this.Items.All(x => x.Lin == 0))
            {
                foreach (DTODeliveryItem oItem in this.Items)
                {
                    lin += 1;
                    oItem.Lin = lin;
                    foreach (DTODeliveryItem oBundleItem in oItem.Bundle)
                    {
                        lin += 1;
                        oBundleItem.Lin = lin;
                    }
                }
            }
            else if (this.Items.Any(x => x.Lin == 0))
            {
                lin = this.Items.Max(x => x.Lin);
                foreach (DTODeliveryItem oItem in this.Items.Where(x => x.Lin == 0))
                {
                    lin += 1;
                    oItem.Lin = lin;
                    foreach (DTODeliveryItem oBundleItem in oItem.Bundle)
                    {
                        lin += 1;
                        oBundleItem.Lin = lin;
                    }
                }
            }
        }


        public static DTOAmt BaseImponible(DTODelivery oDelivery)
        {
            DTOAmt retval = DTODelivery.sumaDeImports(oDelivery);
            return retval;
        }

        public List<DTOTaxBaseQuota> taxBaseQuotas(List<DTOTax> oTaxes) // TO DEPRECATE
        {
            List<DTOTaxBaseQuota> retval = new List<DTOTaxBaseQuota>();
            bool BlIva = ExportCod == DTOInvoice.ExportCods.nacional;
            bool BlReq = Cod != DTOPurchaseOrder.Codis.proveidor && Customer.CcxOrMe().Req;
            if (BlIva)
            {
                if (Items != null)
                {
                    foreach (DTODeliveryItem oItm in Items)
                    {
                        DTOTaxBaseQuota oQuota = retval.Find(x => x.tax.codi == oItm.IvaCod);
                        if (oQuota == null)
                        {
                            DTOTax oTax = DTOTax.closest(oItm.IvaCod, Fch);
                            oQuota = new DTOTaxBaseQuota(oTax, oItm.Import());
                            retval.Add(oQuota);
                            if (BlReq)
                            {
                                DTOTax.Codis oTaxReqCodi = DTOTax.reqForIvaCod(oQuota.tax.codi);
                                DTOTax oTaxReq = DTOTax.closest(oTaxReqCodi, Fch);
                                DTOTaxBaseQuota oQuotaReq = new DTOTaxBaseQuota(oTaxReq, oItm.Import());
                                retval.Add(oQuotaReq);
                            }
                        }
                        else
                        {
                            oQuota.addBase(oItm.Import());
                            // oQuota.Base.Add(oItm.Import())
                            if (BlReq)
                            {
                                DTOTax.Codis oTaxReqCodi = DTOTax.reqForIvaCod(oQuota.tax.codi);
                                DTOTaxBaseQuota oQuotaReq = retval.Find(x => x.tax.codi == oTaxReqCodi);
                                // oQuotaReq.Base.Add(oItm.Import())
                                oQuotaReq.addBase(oItm.Import());
                            }
                        }
                    }
                }
            }
            return retval;
        }
        public List<DTOTaxBaseQuota> taxBaseQuotas()
        {
            List<DTOTaxBaseQuota> retval = new List<DTOTaxBaseQuota>();
            bool BlIva = ExportCod == DTOInvoice.ExportCods.nacional;
            if (BlIva)
            {
                if (Items != null)
                {
                    bool BlReq = false;
                    if (Cod != DTOPurchaseOrder.Codis.proveidor)
                    {
                        DTOCustomer ccx = Customer.CcxOrMe();
                        BlReq = (Cod != DTOPurchaseOrder.Codis.proveidor) && (ccx.Req);
                    }

                    foreach (DTODeliveryItem oItm in Items)
                    {
                        DTOTaxBaseQuota oQuota = retval.Find(x => x.tax.codi == oItm.IvaCod);
                        if (oQuota == null)
                        {
                            if (oItm.Price != null)
                            {
                                DTOTax oTax = DTOTax.closest(oItm.IvaCod, Fch);
                                oQuota = new DTOTaxBaseQuota(oTax, oItm.Import());
                                retval.Add(oQuota);
                                if (BlReq)
                                {
                                    DTOTax.Codis oTaxReqCodi = DTOTax.reqForIvaCod(oQuota.tax.codi);
                                    DTOTax oTaxReq = DTOTax.closest(oTaxReqCodi, Fch);
                                    DTOTaxBaseQuota oQuotaReq = new DTOTaxBaseQuota(oTaxReq, oItm.Import());
                                    retval.Add(oQuotaReq);
                                }
                            }
                        }
                        else
                        {
                            oQuota.addBase(oItm.Import());
                            // oQuota.Base.Add(oItm.Import())
                            if (BlReq)
                            {
                                DTOTax.Codis oTaxReqCodi = DTOTax.reqForIvaCod(oQuota.tax.codi);
                                DTOTaxBaseQuota oQuotaReq = retval.Find(x => x.tax.codi == oTaxReqCodi);
                                // oQuotaReq.Base.Add(oItm.Import())
                                oQuotaReq.addBase(oItm.Import());
                            }
                        }
                    }
                }
            }
            return retval;
        }

        public static List<DTOTaxBaseQuota> getIvaBaseQuotas(List<DTODeliveryItem> items, List<DTOTax> Taxes, bool Iva, bool Req = false)
        {
            List<DTOTaxBaseQuota> retval = new List<DTOTaxBaseQuota>();
            if (Iva)
            {
                foreach (DTODeliveryItem oItm in items)
                {
                    DTOTaxBaseQuota oIvaBaseQuota = retval.Find(x => x.tax.codi == oItm.IvaCod);
                    if (oIvaBaseQuota == null)
                    {
                        DTOTax oTax = Taxes.Find(x => x.codi == oItm.IvaCod);
                        oIvaBaseQuota = new DTOTaxBaseQuota(oTax, oItm.Import());
                        retval.Add(oIvaBaseQuota);
                        if (Req)
                        {
                            DTOTax.Codis oTaxReqCodi = DTOTax.reqForIvaCod(oIvaBaseQuota.tax.codi);
                            DTOTax oTaxReq = Taxes.Find(x => x.codi == oTaxReqCodi);
                            DTOTaxBaseQuota oReqBaseQuota = new DTOTaxBaseQuota(oTaxReq, oItm.Import());
                            retval.Add(oReqBaseQuota);
                        }
                    }
                    else
                    {
                        oIvaBaseQuota.addBase(oItm.Import());
                        if (Req)
                        {
                            DTOTax.Codis oTaxReqCodi = DTOTax.reqForIvaCod(oIvaBaseQuota.tax.codi);
                            DTOTaxBaseQuota oReqBaseQuota = retval.Find(x => x.tax.codi == oTaxReqCodi);
                            oReqBaseQuota.addBase(oItm.Import());
                        }
                    }
                }
            }
            return retval;
        }

        public static decimal ivaTipus(DTODelivery oDelivery)
        {
            decimal retval = 0;
            if (!oDelivery.IvaExempt)
            {
                DTOCustomer oCustomer = oDelivery.Customer;
                if (oCustomer.Iva)
                    retval = DTOTax.closestTipus(DTOTax.Codis.iva_Standard);
            }
            return retval;
        }

        public static DTOAmt ivaAmt(DTODelivery oDelivery)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (!oDelivery.IvaExempt)
            {
                DTOCustomer oCustomer = oDelivery.Customer;
                if (oCustomer.Iva)
                {
                    decimal DcTipus = DTOTax.closestTipus(DTOTax.Codis.iva_Standard);
                    retval = sumaDeImports(oDelivery).Percent(DcTipus);
                }
            }
            return retval;
        }

        public static decimal reqTipus(DTODelivery oDelivery)
        {
            decimal retval = 0;
            if (!oDelivery.IvaExempt)
            {
                DTOCustomer oCustomer = oDelivery.Customer;
                if (oCustomer.Req)
                    retval = DTOTax.closestTipus(DTOTax.Codis.recarrec_Equivalencia_Standard);
            }
            return retval;
        }

        public static DTOAmt reqAmt(DTODelivery oDelivery)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (!oDelivery.IvaExempt)
            {
                DTOCustomer oCustomer = oDelivery.Customer;
                if (oCustomer.Iva)
                {
                    decimal DcTipus = DTOTax.closestTipus(DTOTax.Codis.recarrec_Equivalencia_Standard);
                    retval = DTODelivery.sumaDeImports(oDelivery).Percent(DcTipus);
                }
            }
            return retval;
        }

        public static DTOAmt sumaDeImports(DTODelivery oDelivery)
        {
            DTOAmt retval = DTOAmt.Empty();
            foreach (DTODeliveryItem item in oDelivery.Items)
                retval.Add(item.Import());
            return retval;
        }



        public static DTOAmt baseImponible(List<DTODeliveryItem> items, DTOCur oCur = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oCur == null)
                oCur = DTOApp.Current.Cur;
            var retval = DTOAmt.Empty(oCur);
            foreach (var item in items)
                retval.Add(item.Import());
            return retval;
        }

        public static DTOAmt baseImponible(List<DTODelivery> oDeliveries)
        {
            var retval = DTOAmt.Empty();
            foreach (DTODelivery oItem in oDeliveries)
                retval.Add(oItem.baseImponible());
            return retval;
        }

        public DTOAmt baseImponible()
        {
            DTOCur oCur = null/* TODO Change to default(_) if this is not a reference type */;
            switch (Cod)
            {
                case DTOPurchaseOrder.Codis.proveidor:
                    {
                        DTOProveidor oProveidor = Proveidor;
                        oCur = Proveidor.Cur;
                        break;
                    }

                default:
                    {
                        oCur = DTOCur.Eur();
                        break;
                    }
            }
            DTOAmt retval = baseImponible(Items, oCur);
            return retval;
        }

        public DTOAmt totalCash()
        {
            DTOAmt retval;
            bool Iva;
            bool Req;

            switch (Cod)
            {
                case DTOPurchaseOrder.Codis.proveidor:
                    {
                        DTOProveidor oProveidor = Proveidor;
                        retval = DTOAmt.Empty(Proveidor.Cur);
                        var oExportCod = oProveidor.Address.ExportCod();
                        Iva = (oExportCod == DTOInvoice.ExportCods.nacional);
                        Req = false;
                        break;
                    }

                default:
                    {
                        DTOCustomer oCcx = Customer.CcxOrMe();
                        retval = DTOAmt.Empty(DTOCur.Eur());
                        Iva = oCcx.Iva; //deprecated?
                        Req = oCcx.Req; // deprecated?
                        break;
                    }
            }

            retval = this.baseImponible().Clone();
            switch (ExportCod)
            {
                case DTOInvoice.ExportCods.nacional:
                    {
                        var oQuotas = taxBaseQuotas();
                        foreach (var oQuota in oQuotas)
                        {
                            switch (oQuota.tax.codi)
                            {
                                case DTOTax.Codis.iva_Standard:
                                case DTOTax.Codis.iva_Reduit:
                                case DTOTax.Codis.iva_SuperReduit:
                                    {
                                        retval.Add(oQuota.quota);
                                        break;
                                    }

                                case DTOTax.Codis.recarrec_Equivalencia_Standard:
                                case DTOTax.Codis.recarrec_Equivalencia_Reduit:
                                case DTOTax.Codis.recarrec_Equivalencia_SuperReduit:
                                    {
                                        retval.Add(oQuota.quota);
                                        break;
                                    }
                            }
                        }

                        break;
                    }
            }

            return retval;
        }

        public static decimal volumeM3(List<DTODeliveryItem> oItems)
        {
            decimal retval = oItems.Sum(x => DTODeliveryItem.volumeM3(x));
            return retval;
        }

        public static decimal weightKg(List<DTODeliveryItem> oItems)
        {
            decimal retval = oItems.Sum(x => DTODeliveryItem.weightKg(x));
            return retval;
        }

        public static string deliveryLocationNom(DTODelivery oDelivery)
        {
            string retVal = "";
            DTOAddress oAddress = null/* TODO Change to default(_) if this is not a reference type */;
            if (oDelivery.Platform == null)
                oAddress = oDelivery.Address;
            else
                oAddress = oDelivery.Platform.Address;
            if (oAddress != null)
            {
                if (oAddress.Zip != null)
                {
                    if (oAddress.Zip.Location != null)
                        retVal = oAddress.Zip.Location.Nom;
                }
            }
            return retVal;
        }

        public static string FileName(DTODelivery oDelivery, bool BlProforma = false)
        {
            var oDeliveries = new DTODelivery[]
            {
            oDelivery
        }.ToList();
            return FileName(oDeliveries, BlProforma);
        }

        public static string FileName(List<DTODelivery> oDeliveries, bool BlProforma = false)
        {
            string retval = "";
            if (oDeliveries.Count > 0)
                retval = string.Format("M+O.{0}.pdf", labelAndNumsText(oDeliveries, BlProforma));
            return retval;
        }

        public static string labelAndNumsText(List<DTODelivery> oDeliveries, bool BlProforma)
        {
            string retval = "";
                string sAlbLabel = label(oDeliveries, BlProforma);
            if (oDeliveries.Count > 0)
            {
                var sNums = numsText(oDeliveries);
                retval = string.Format("{0} {1}", sAlbLabel, sNums);
            } 
            return retval;
        }

        public static string numsText(List<DTODelivery> oDeliveries)
        {
            string retval = "";
             if (oDeliveries.Count == 1)
            {
                retval =  oDeliveries.First().Formatted();

            }
            else if (oDeliveries.Count > 0)
            {
                if (DTODelivery.areConsecutive(oDeliveries))
                    retval = string.Format("{0}-{1}", oDeliveries.First().Formatted(), oDeliveries.Last().Formatted());
                else if (oDeliveries.Count < 4)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (var oDelivery in oDeliveries)
                    {
                        if (oDelivery.UnEquals(oDeliveries.First()))
                            sb.Append("-");
                        sb.Append(oDelivery.Formatted());
                    }
                    retval = sb.ToString();
                }
            }
            return retval;
        }

        public static string label(List<DTODelivery> oDeliveries, bool BlProforma)
        {
            string retval = "";
            DTOLang olang = lang(oDeliveries.First());
            if (BlProforma)
            {
                if (oDeliveries.Count == 1)
                    retval = "proforma";
                else
                    retval = "proformas";
            }
            else if (oDeliveries.Count == 1)
                retval = olang.Tradueix("albarán", "albarà", "delivery", "nota de entrega");
            else
                retval = olang.Tradueix("albaranes", "albarans", "deliveries", "notas de entrega");
            return retval;
        }

        public static string invoiceText(DTODelivery oDelivery, DTOLang oLang)
        {
            string retval = "";
            if (oDelivery.Invoice != null)
                retval = string.Format("{0} {1} {2} {3}", oLang.Tradueix("factura", "factura", "invoice"), oDelivery.Invoice.Num, oLang.Tradueix("del", "del", "from"), VbUtilities.Format(oDelivery.Fch, "dd/MM/yy"));
            return retval;
        }

        public static bool areConsecutive(List<DTODelivery> oDeliveries)
        {
            bool retval = true;
            if (oDeliveries.Count > 1)
            {
                var sortedNums = DTODelivery.sorted(oDeliveries).Select(x => x.Id).ToList();
                for (var i = 1; i <= sortedNums.Count - 1; i++)
                {
                    if (sortedNums[i] != sortedNums[i - 1] + 1)
                    {
                        retval = false;
                        break;
                    }
                }
            }
            return retval;
        }

        public static List<DTODelivery> sorted(List<DTODelivery> oDeliveries)
        {
            var retval = oDeliveries.OrderBy(x => x.Id).OrderBy(y => y.Fch.Year).ToList();
            return retval;
        }

        public static string Formatted(DTODelivery oDelivery)
        {
            string retVal = VbUtilities.Format(oDelivery.Fch.Year, "0000") + VbUtilities.Format(oDelivery.Id, "000000");
            return retVal;
        }

        public static bool sameCustomer(List<DTODelivery> oDeliveries)
        {
            bool retval = oDeliveries.All(x => x.Customer.Equals(oDeliveries.First().Customer));
            return retval;
        }

        public static bool discountExists(DTODelivery oDelivery)
        {
            return oDelivery.Items.Any(x => x.Dto != 0);
        }

        public static bool loadMadeIns(ref DTODelivery oDelivery, List<DTOCountry> oCountries, List<Exception> exs)
        {
            bool retval = true;
            foreach (DTODeliveryItem item in oDelivery.Items)
            {
                DTOProductSku oSku = item.Sku;
                DTOCountry oCountry = DTOProductSku.madeInOrInherited(oSku);
                if (oCountry != null)
                    oSku.MadeIn = oCountries.FirstOrDefault(x => x.Equals(oCountry));
            }
            return retval;
        }

        public static bool loadCustomEans(ref DTODelivery oDelivery, List<DTOEdiOrder> EdiOrders, List<Exception> exs)
        {
            bool retval = true;
            foreach (DTODeliveryItem item in oDelivery.Items)
            {
            }
            return retval;
        }

        public static DTOLang lang(DTODelivery oDelivery)
        {
            DTOLang retval = DTOLang.ESP();
            if (oDelivery != null)
            {
                if (oDelivery.Customer != null)
                {
                    if (oDelivery.Customer.Lang != null)
                        retval = oDelivery.Customer.Lang;
                }
            }
            return retval;
        }

        public static string FullNom(DTODelivery oDelivery)
        {
            DTOLang olang = DTODelivery.lang(oDelivery);
            string sAlbLabel = olang.Tradueix("albaran", "albara", "delivery");
            string retval = string.Format("{0} {1} del {2:dd/MM/yy} a {3}", sAlbLabel, Formatted(oDelivery), oDelivery.Fch, oDelivery.Customer.FullNom);
            return retval;
        }

        public static string DeliveryTerms(DTODelivery oDelivery)
        {
            string retval = "";
            switch (oDelivery.PortsCod)
            {
                case DTOCustomer.PortsCodes.altres:
                    {
                        retval = "(altres)";
                        break;
                    }

                case DTOCustomer.PortsCodes.entregatEnMa:
                    {
                        retval = "(entregat en ma)";
                        break;
                    }

                case DTOCustomer.PortsCodes.pagats:
                    {
                        DTOTransportista oTransportista = oDelivery.Transportista;
                        if (oTransportista == null)
                            retval = "(pagats)";
                        else
                            retval = oTransportista.Abr;
                        break;
                    }

                case DTOCustomer.PortsCodes.reculliran:
                    {
                        retval = "(reculliran)";
                        break;
                    }
            }
            return retval;
        }

        public static DTODelivery.PortsQualification GetPortsQualification(DTODelivery oDelivery)
        {
            DTODelivery.PortsQualification retval = DTODelivery.PortsQualification.notApplicable;
            DTOAmt oSumaDeImports = DTODelivery.sumaDeImports(oDelivery);
            DTOCustomer oCustomer = oDelivery.Customer;
            DTOPortsCondicio oPortsCondicio = oCustomer.PortsCondicions;
            DTOAmt oUnitsMinPreu = oCustomer.PortsCondicions.UnitsMinPreu;
            if (oSumaDeImports.IsGreaterOrEqualThan(oUnitsMinPreu))
                retval = DTODelivery.PortsQualification.qualified;
            else
                retval = DTODelivery.PortsQualification.lowVolume;

            if (oPortsCondicio != null)
            {
                switch (oPortsCondicio.Cod)
                {
                    case DTOPortsCondicio.Cods.reculliran:
                        retval = DTODelivery.PortsQualification.notApplicable;
                        break;
                    case DTOPortsCondicio.Cods.portsDeguts:
                        retval = DTODelivery.PortsQualification.notApplicable;
                        break;
                    case DTOPortsCondicio.Cods.portsPagats:
                        break;
                    case DTOPortsCondicio.Cods.carrecEnFactura:
                        break;
                }
            }

            //Dim oBaseImponible As DTOAmt = DTODeliveryItem.baseImponible(oItms)
            //Dim retval As Boolean = oBaseImponible.IsGreaterOrEqualThan(oPortsCondicio.PdcMinVal)

            //switch (oCustomer.PortsCondicio)
            //{
            //    case DTOCustomer.PortsCondicions.peninsulaBalears:
            //        {
            //            retval = DTODelivery.PortsQualification.lowVolume;
            //            if (DcSumaDeImports > 300)
            //                retval = DTODelivery.PortsQualification.qualified;
            //            else
            //          {
            //              decimal DcSumaTommeeTippee = oDelivery.Items.Where(x => x.PurchaseOrderItem.Sku.Category.Brand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.TommeeTippee))).Sum(x => x.Qty * x.Price.Eur * (100 - x.Dto) / 100);
            //if (DcSumaTommeeTippee > 150)
            //retval = DTODelivery.PortsQualification.qualified;
            //}

            //break;
            //}

            //case DTOCustomer.PortsCondicions.canaries:
            //{
            //retval = DcSumaDeImports > 400 ? DTODelivery.PortsQualification.qualified : DTODelivery.PortsQualification.lowVolume;
            //break;
            //}

            //case DTOCustomer.PortsCondicions.andorra:
            //{
            //retval = DcSumaDeImports > 400 ? DTODelivery.PortsQualification.qualified : DTODelivery.PortsQualification.lowVolume;
            //break;
            //}

            //case DTOCustomer.PortsCondicions.resteDelMon:
            //{
            //retval = DTODelivery.PortsQualification.notApplicable;
            //break;
            //}

            //case DTOCustomer.PortsCondicions.eCom:
            //{
            //retval = DTODelivery.PortsQualification.notApplicable;
            //break;
            //}
            //}
            return retval;
        }

        public static string DocSpvText(DTODelivery oDelivery, DTOSpv oSpv)
        {
            DTOLang oLang;
            oLang = oDelivery.Customer.Lang;
            string StDoc = oLang.Tradueix("Reparación ", "Reparació ", "Item repaired ");
            string StFrom = oLang.Tradueix("de fecha", "de data", "from");
            string retval = StDoc + oSpv.id + ": " + DTOProduct.GetNom((DTOProduct)oSpv.product);
            return retval;
        }

        public static int YearFromFormattedId(string src)
        {
            int retval = 0;
            if (src.Length >= 4)
            {
                retval = src.Substring(0, 4).toInteger();
            }
            return retval;
        }

        public static int IdFromFormattedId(string src)
        {
            int retval = 0;
            if (src.Length >= (4 + 6))
            {
                retval = src.Substring(4, 6).toInteger();
            }
            return retval;
        }

        public bool IsConsumer()
        {
            bool retval = this.Contact.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.consumidor));
            return retval;
        }


        public DTOEdiDesadv Desadv(List<Exception> exs)
        {
            DTOEdiDesadv retval = null;
            string supplier = Emp.Org.GLN.Value;
            if (Customer.GLN == null)
            {
                exs.Add(new Exception("Aquest client no te cap GLN configurat"));
            }
            else
            {
                string buyer = Customer.GLN.Value;
                string deliveryFrom = Mgz.GLN.Value;
                string deliveryTo = Customer.GLN.Value;
                if (this.Platform != null)
                    deliveryTo = Platform.GLN.Value;

                string deliveryNum = DTODelivery.Formatted(this);
                retval = DTOEdiDesadv.Factory(supplier, buyer, deliveryFrom, deliveryTo, deliveryNum, Fch);
                retval.DeliveryFromZip = Mgz.ZipCod();
                retval.DeliveryFromCountryISO = Mgz.CountryISO();


                if (Pallets.Count > 0)
                {
                    foreach (DTODelivery.Pallet pallet in Pallets)
                    {
                        DTOEdiDesadv.Pallet desadvPallet = retval.AddPallet(pallet.Cps, pallet.SSCC);
                        foreach (DTODelivery.Package package in pallet.Packages)
                        {
                            DTOEdiDesadv.Package bulto = desadvPallet.AddPackage(package.Cps, package.SSCC);
                            foreach (DTODelivery.Package.Item item in package.Items)
                            {
                                if (!item.DeliveryItem.Sku.NoStk)
                                {
                                    string orderNum = item.DeliveryItem.PurchaseOrderItem.PurchaseOrder.Concept;
                                    bulto.AddItem(orderNum, item.DeliveryItem.Sku.Ean13.Value, item.DeliveryItem.Sku.NomLlarg.Esp, item.QtyInPackage);
                                }
                            }
                        }
                    }
                }

                if (Packages.Count > 0)
                {
                    foreach (DTODelivery.Package package in Packages)
                    {
                        DTOEdiDesadv.Package bulto = retval.AddPackage(package.Cps, package.SSCC);
                        foreach (DTODelivery.Package.Item item in package.Items)
                        {
                            if (!item.DeliveryItem.Sku.NoStk && item.DeliveryItem.Sku.Ean13 != null)
                            {
                                string orderNum = item.DeliveryItem.PurchaseOrderItem.PurchaseOrder.Concept;
                                string ean = "0000000000000";
                                if (item.DeliveryItem.Sku.Ean13 != null)
                                    ean = item.DeliveryItem.Sku.Ean13.Value;
                                bulto.AddItem(orderNum, ean, item.DeliveryItem.Sku.NomLlarg.Esp, item.QtyInPackage);
                            }
                        }
                    }
                }
                else //To Deprecate
                {
                    foreach (DTODeliveryItem item in Items)
                    {
                        if (!item.Sku.NoStk)
                        {
                            string orderNum = item.PurchaseOrderItem.PurchaseOrder.Concept;
                            retval.AddItem(orderNum, item.Sku.Ean13.Value, item.Sku.NomLlarg.Esp, item.Qty);
                        }
                    }

                }

            }


            return retval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EdiHelperStd.Desadv Desadv2(DTOContact sender)
        {
            string supplier = sender.GLN.Value;
            string buyer = Customer.GLN.Value;
            string deliveryFrom = Mgz.GLN.Value;
            string deliveryTo = Customer.GLN.Value;
            if (this.Platform != null)
                deliveryTo = Platform.GLN.Value;

            string deliveryNum = DTODelivery.Formatted(this);
            int hierarchicalMumber = 0;
            EdiHelperStd.Desadv retval = EdiHelperStd.Desadv.Factory(supplier, buyer, deliveryTo, deliveryNum, this.Fch, this.Fch);


            if (Pallets.Count > 0)
            {
                foreach (DTODelivery.Pallet pallet in Pallets)
                {
                    hierarchicalMumber += 1;
                    Desadv.ShippingContainer palletContainer = new Desadv.ShippingContainer(hierarchicalMumber);
                    palletContainer.SSCC = pallet.SSCC;
                    palletContainer.Packaging = EdiHelperStd.Desadv.ShippingContainer.Packagings.ReturnablePallet;
                    retval.ShippingContainers.Add(palletContainer);
                    foreach (DTODelivery.Package package in pallet.Packages)
                    {
                        hierarchicalMumber += 1;
                        Desadv.ShippingContainer packageContainer = new Desadv.ShippingContainer(hierarchicalMumber);
                        packageContainer.SSCC = package.SSCC;
                        packageContainer.Packaging = EdiHelperStd.Desadv.ShippingContainer.Packagings.Package;
                        palletContainer.ShippingContainers.Add(packageContainer);
                        foreach (DTODeliveryItem item in Items)
                        {
                            if (!item.Sku.NoStk)
                            {
                                string orderNum = item.PurchaseOrderItem.PurchaseOrder.Concept;
                                Desadv.Item shippingItem = EdiHelperStd.Desadv.Item.Factory(orderNum, item.Sku.Ean13.Value, item.Sku.NomLlarg.Esp, item.Qty);
                                packageContainer.Items.Add(shippingItem);
                            }
                        }
                    }
                }
            }

            if (Packages.Count > 0)
            {
                foreach (DTODelivery.Package package in Packages)
                {
                    hierarchicalMumber += 1;
                    Desadv.ShippingContainer packageContainer = new Desadv.ShippingContainer(hierarchicalMumber);
                    packageContainer.SSCC = package.SSCC;
                    packageContainer.Packaging = EdiHelperStd.Desadv.ShippingContainer.Packagings.Package;
                    retval.ShippingContainers.Add(packageContainer);
                    foreach (DTODeliveryItem item in Items)
                    {
                        if (!item.Sku.NoStk)
                        {
                            string orderNum = item.PurchaseOrderItem.PurchaseOrder.Concept;
                            Desadv.Item shippingItem = EdiHelperStd.Desadv.Item.Factory(orderNum, item.Sku.Ean13.Value, item.Sku.NomLlarg.Esp, item.Qty);
                            packageContainer.Items.Add(shippingItem);
                        }
                    }
                }
            }
            else //To Deprecate
            {
                foreach (DTODeliveryItem item in Items)
                {
                    if (!item.Sku.NoStk)
                    {
                        string orderNum = item.PurchaseOrderItem.PurchaseOrder.Concept;
                        Desadv.Item shippingItem = EdiHelperStd.Desadv.Item.Factory(orderNum, item.Sku.Ean13.Value, item.Sku.NomLlarg.Esp, item.Qty);
                        retval.Items.Add(shippingItem);
                    }
                }

            }
            return retval;
        }



        public class Collection : List<DTODelivery>
        {

        }

        public class Shipment
        {
            public DTODelivery delivery { get; set; }
            public string Expedition { get; set; }
            public DateTime Fch { get; set; }
            public string ForwarderNif { get; set; }
            public DTOContact Forwarder { get; set; }
            public string Tracking { get; set; }
            public int CubicKg { get; set; }
            public decimal Weight { get; set; }
            public decimal Volume { get; set; }
            public decimal Cost { get; set; }
            public DTOJsonLog ShipmentLog { get; set; }
            public DTOJsonLog TrackingLog { get; set; }

            public Pallet.Collection Pallets { get; set; }
            public Package.Collection Packages { get; set; }



            public class Collection : List<Shipment>
            {

            }
        }

        public class Pallet
        {
            public string SSCC { get; set; }
            public int Cps { get; set; } // Hierarchical number
            public string Expedition { get; set; }
            public string Fch { get; set; }
            public List<Package> Packages { get; set; }

            public Pallet(int cps, string sscc)
            {
                this.Cps = cps;
                this.SSCC = sscc;
                Packages = new List<Package>();
            }

            public Package AddPackage(int cps, string sscc)
            {
                Package retval = new Package(cps, sscc);
                return retval;
            }

            public class Collection : List<Pallet>
            {

            }
        }

        public class Package
        {
            public string SSCC { get; set; } //License plate (matricula del bulto)
            public int Cps { get; set; } //Hierarchical number (numero consecutivo de la handling unit
            public string Expedition { get; set; } //Orden de Salida de Vivace
            public string Packaging { get; set; }
            public int Num { get; set; }
            public int Length { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

            public DateTime Fch { get; set; } // Shipping date
            public List<Item> Items { get; set; }

            public Package(int cps, string sscc)
            {
                this.Cps = cps;
                this.SSCC = sscc;
                Items = new List<Item>();
            }

            public Item AddItem(int qtyInPackage, DTODeliveryItem deliveryItem)
            {
                Item retval = new Item();
                retval.QtyInPackage = qtyInPackage;
                retval.DeliveryItem = deliveryItem;
                this.Items.Add(retval);
                return retval;
            }

            public string Dimensions()
            {
                string retval = "";
                if (this.HasDimensions())
                    retval = string.Format("{0}mm x {1}mm x {2}mm", this.Length, this.Width, this.Height);
                return retval;
            }

            public bool HasDimensions()
            {
                bool retval = (this.Length + this.Width + this.Height > 0);
                return retval;
            }

            public class Item
            {
                public int QtyInPackage { get; set; }
                public DTODeliveryItem DeliveryItem { get; set; }

                public class Collection : List<Item>
                {

                }

            }

            public class Collection : List<Package>
            {

            }


        }
    }

    public class DTODeliveryItem : DTOBaseGuid
    {
        public DTODelivery Delivery { get; set; }
        public int Lin { get; set; }
        public DTOPurchaseOrderItem PurchaseOrderItem { get; set; }
        public DTOMgz Mgz { get; set; }
        public DTORepLiq RepLiq { get; set; }
        public List<DTORepLiq> RepLiqs { get; set; } = new List<DTORepLiq>();
        public DTORepCom RepCom { get; set; }
        public DTORepComLiquidable RepComLiquidable { get; set; }
        public DTOSpv Spv { get; set; }
        public int Qty { get; set; }
        public DTOProductSku Sku { get; set; }
        public DTOAmt Price { get; set; }
        public decimal Dto { get; set; }
        public decimal Dt2 { get; set; }
        public decimal Pmc { get; set; }
        public Cods Cod { get; set; }
        public List<DTOIncentiu> Incentius { get; set; }
        public DTOIncentiu Incentiu { get; set; }
        public DTOTax.Codis IvaCod { get; set; } = DTOTax.Codis.iva_Standard;

        public List<DTODeliveryItem> Bundle { get; set; }

        public enum Cods
        {
            NotSet = 0,
            Entrada = 11,
            TraspasEntrada = 12,
            Unknown13 = 13,
            Unknown21 = 21,
            Unknown31 = 31,
            Unknown32 = 32,
            Sortida = 51,
            TraspasSortida = 52,
            Unknown53 = 53,
            Reparacio = 61,
            Unknown62 = 62,
            Unknown73 = 73,
            Unknown81 = 81,
            Unknown82 = 82,
            Unknown91 = 91
        }

        public DTODeliveryItem() : base()
        {
            Bundle = new List<DTODeliveryItem>();
        }

        public DTODeliveryItem(Guid oGuid) : base(oGuid)
        {
            Bundle = new List<DTODeliveryItem>();
        }

        public static DTODeliveryItem Factory(DTOPurchaseOrderItem oPurchaseOrderItem, int iQty, DTOAmt oPrice = null/* TODO Change to default(_) if this is not a reference type */, Nullable<decimal> DcDto = default(Decimal?), Nullable<decimal> DcDt2 = default(Decimal?))
        {
            DTODeliveryItem retval = new DTODeliveryItem();
            {
                retval.PurchaseOrderItem = oPurchaseOrderItem;
                retval.Qty = iQty;
                if (oPurchaseOrderItem != null)
                {
                    retval.Sku = oPurchaseOrderItem.Sku;
                    retval.Price = oPrice == null ? oPurchaseOrderItem.Price : oPrice;
                    retval.Dto = DcDto ?? oPurchaseOrderItem.Dto;
                    retval.Dt2 = DcDt2 ?? oPurchaseOrderItem.Dt2;
                    foreach (var pnc in oPurchaseOrderItem.Bundle)
                    {
                        DTODeliveryItem arc = new DTODeliveryItem();
                        {
                            var withBlock = arc;
                            withBlock.PurchaseOrderItem = pnc;
                            withBlock.PurchaseOrderItem.PurchaseOrder = oPurchaseOrderItem.PurchaseOrder;
                            withBlock.Qty = iQty;
                            withBlock.Price = pnc.Price;
                            withBlock.Dto = pnc.Dto;
                            withBlock.Dt2 = pnc.Dt2;
                        }
                        retval.Bundle.Add(arc);
                    }
                }
            }

            return retval;
        }

        public static DTODeliveryItem Factory(DTOSpv oSpv, DTOProductSku oSku, int iQty = 1, DTOAmt oPrice = null/* TODO Change to default(_) if this is not a reference type */, decimal DcDto = 0)
        {
            DTODeliveryItem retval = new DTODeliveryItem();
            {
                var withBlock = retval;
                withBlock.Spv = oSpv;
                withBlock.Sku = oSku;
                withBlock.Qty = iQty;
                withBlock.Price = oPrice;
                withBlock.Dto = DcDto;
            }
            return retval;
        }

        public DTOAmt netPrice()
        {
            DTOAmt retval = Price.Clone();
            if (Dto != 0)
                retval.DeductPercent(Dto);
            return retval;
        }


        public DTOAmt Import()
        {
            DTOAmt retval = DTOAmt.import(Qty, Price, Dto);
            return retval;
        }

        public static decimal volumeM3(DTODeliveryItem oItem)
        {
            decimal DcVolumeM3 = oItem.Sku.VolumeM3OrInherited();
            decimal retval = oItem.Qty * DcVolumeM3;
            return retval;
        }

        public static decimal weightKg(DTODeliveryItem oItem)
        {
            decimal iWeightKg = oItem.Sku.weightKgOrInherited();
            decimal retval = oItem.Qty * iWeightKg;
            return retval;
        }

        public static int Bultos(DTODeliveryItem oItem)
        {
            int retval;
            int innerPack = oItem.Sku.innerPackOrInherited();
            if (innerPack <= 0)
                retval = 1;
            else if (oItem.Qty <= innerPack)
                retval = 1;
            else if (oItem.Qty % innerPack == 0)
                retval = oItem.Qty / innerPack;
            else
                retval = oItem.Qty / innerPack + 1;
            return retval;
        }

        public static decimal volumeM3(List<DTODeliveryItem> oItems)
        {
            decimal retval = oItems.Sum(x => DTODeliveryItem.volumeM3(x));
            return retval;
        }

        public static decimal weightKg(List<DTODeliveryItem> oItems)
        {
            decimal retval = oItems.Sum(x => DTODeliveryItem.weightKg(x));
            return retval;
        }

        public static DTOAmt baseImponible(List<DTODeliveryItem> oItems)
        {
            var retval = DTOAmt.Empty();
            foreach (var item in oItems)
                retval.Add(item.Import());
            return retval;
        }


        public static DTOAmt comValue(DTODeliveryItem item)
        {
            var retval = DTOAmt.Empty();
            if (item.RepCom != null)
                retval = item.Import().Percent(item.RepCom.Com);
            return retval;
        }


        public static MatHelper.Excel.Sheet Excel(List<Models.SkuInOutModel.Item> oItems, string sFilename)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(sFilename);
            {
                var withBlock = retval;
                withBlock.AddColumn("Transmisió", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Albarà", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("Procedencia/Destinació", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Entrades", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Sortides", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Stock", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Preu", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("Dte", MatHelper.Excel.Cell.NumberFormats.Percent);
                withBlock.AddColumn("Preu Mig de Compra", MatHelper.Excel.Cell.NumberFormats.Euro);
            }

            var oSortedItems = oItems.OrderBy(x => x.DeliveryId).OrderBy(y => y.Fch);
            foreach (var item in oSortedItems)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                {
                    var withBlock = oRow;
                    if (item.TransmId == 0)
                        oRow.AddCell();
                    else
                        withBlock.AddCell(item.TransmId);
                    oRow.AddCell(item.DeliveryId);
                    oRow.AddCell(item.Fch);
                    oRow.AddCell(item.Nom);
                    if ((int)item.Cod < 50)
                    {
                        oRow.AddCell(item.Qty);
                        oRow.AddCell();
                    }
                    else
                    {
                        oRow.AddCell();
                        oRow.AddCell(item.Qty);
                    }
                    oRow.AddFormula("IF(ISNUMBER(R[-1]C),R[-1]C,0)+RC[-2]-RC[-1]");
                    oRow.AddCell(item.Eur);
                    oRow.AddCell(item.Dto);
                    oRow.AddCell(item.Pmc);
                }
            }
            return retval;
        }


    }

}


