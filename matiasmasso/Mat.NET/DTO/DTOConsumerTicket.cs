using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTOConsumerTicket : DTOBaseGuid
    {
        public int Id { get; set; }
        public DateTime Fch { get; set; }
        public DTOLang Lang { get; set; }
        public string Nom { get; set; }
        public string Cognom1 { get; set; }
        public string Cognom2 { get; set; }
        public string Tel { get; set; }
        public string EmailAddress { get; set; }
        public DTOAddress Address { get; set; }
        public DTOPurchaseOrder.Codis Op { get; set; }
        public DTOMarketPlace MarketPlace { get; set; }
        public string OrderId { get; set; }
        public DateTime FchTrackingNotified { get; set; }
        public DTOGuidNom UsrTrackingNotified { get; set; }
        public DateTime FchDelivered { get; set; }
        public DTOGuidNom UsrDelivered { get; set; }
        public DateTime FchReviewRequest { get; set; }
        public DTOGuidNom UsrReviewRequest { get; set; }
        public DTODelivery Delivery { get; set; }
        public DTOPurchaseOrder PurchaseOrder { get; set; }
        public DTOCca Cca { get; set; }
        public DTOSpv Spv { get; set; }

        public string Nif { get; set; }
        public String FraNom { get; set; }
        public DTOAddress FraAddress { get; set; }
        public DTOUsrLog UsrLog { get; set; }

        public DTOConsumerTicket() : base()
        {
            Address = new DTOAddress();
        }

        public DTOConsumerTicket(Guid oGuid) : base(oGuid)
        {
            Address = new DTOAddress();
        }

        public static DTOConsumerTicket Factory(DTOUser user, DTOMarketPlace marketPlace = null, string orderId = "")
        {
            DTOConsumerTicket retval = new DTOConsumerTicket();
            retval.Fch = DTO.GlobalVariables.Now();
            retval.MarketPlace = marketPlace;
            retval.OrderId = orderId;
            retval.UsrLog = DTOUsrLog.Factory(user);
            retval.PurchaseOrder = DTOPurchaseOrder.Factory(retval);
            return retval;
        }


        public string FullNom()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Cognom1.isNotEmpty())
            {
                sb.Append(this.Cognom1);
                if (this.Cognom2.isNotEmpty())
                    sb.Append(" " + this.Cognom2);
            }
            if (this.Nom.isNotEmpty())
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(this.Nom);
            }
            string retval = sb.ToString();
            return retval;
        }

        public string RaoSocial()
        {
            string retval = this.FraNom.isNotEmpty() ? this.FraNom : this.FullNom();
            return retval;
        }

        public DTOAddress FiscalAddress()
        {
            DTOAddress retval = (this.FraAddress == null || this.FraAddress.Zip == null) ? this.Address : this.FraAddress;
            return retval;
        }
        public string NomAndCognom()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Nom.isNotEmpty())
                sb.Append(this.Nom);
            if (this.Cognom1.isNotEmpty())
            {
                sb.Append(" ");
                sb.Append(this.Cognom1);
            }
            string retval = sb.ToString();
            return retval;
        }

        public DTODelivery Deliver(DTOUser user, DTOCustomer.CashCodes cashCod)
        {
            DTORepCom repCom = null;
            DTODelivery retval = DTODelivery.Factory(user, DTOPurchaseOrder.Codis.client, DTOCustomer.Wellknown(DTOCustomer.Wellknowns.consumidor), DTO.GlobalVariables.Today());
            retval.Emp = PurchaseOrder.Emp.Trimmed();
            retval.Fch = PurchaseOrder.Fch;
            retval.CashCod = cashCod;
            retval.Facturable = true;
            retval.Nom = this.FullNom();
            retval.Address = this.Address;
            retval.Tel = this.Tel;
            if (this.PurchaseOrder.DocFile != null)
            {
                retval.CustomerDocURL = this.PurchaseOrder.DocFile.DownloadUrl(true);
            }
            foreach (DTOPurchaseOrderItem item in this.PurchaseOrder.Items)
            {
                DTODeliveryItem dItem = DTODeliveryItem.Factory(item, item.Qty, item.Price);
                dItem.RepCom = repCom;
                retval.Items.Add(dItem);
            }
            retval.Import = retval.totalCash();
            this.Delivery = retval;
            return retval;
        }


        public void SetCca()
        {
            if (this.MarketPlace != null)
            {
                this.Cca = DTOCca.Factory(this.PurchaseOrder.Fch, this.UsrLog.UsrCreated, DTOCca.CcdEnum.AlbaraBotiga);
                this.Cca.Concept = string.Format("Ticket @@0 de {0} per {1}", this.NomAndCognom(), this.MarketPlace.Nom);
            }
        }

        public void CompleteCca(DTOPgcCta.Collection allCtas)
        {
            if (this.PurchaseOrder != null && this.PurchaseOrder.DocFile != null)
                this.Cca.DocFile = this.PurchaseOrder.DocFile;
            this.Cca.Concept = this.Cca.Concept.Replace("@@0", this.Id.ToString());
            DTOPgcCta ctaDebit = allCtas.Cta(DTOPgcPlan.Ctas.Marketplaces);
            DTOPgcCta ctaCredit = allCtas.Cta(DTOPgcPlan.Ctas.Clients);
            this.Cca.AddCredit(this.Delivery.totalCash(), ctaCredit, DTOCustomer.Wellknown(DTOCustomer.Wellknowns.consumidor));
            this.Cca.AddSaldo(ctaDebit, this.MarketPlace.Contact());
        }

        public string ToMultiline(DTOLang lang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("Ticket #{0} del {1:dd/MM/yy}", this.Id, this.Fch));
            sb.AppendLine(string.Format("Consumidor: {0}", this.FullNom()));
            sb.AppendLine(this.Address.ToMultilineString(lang));
            return sb.ToString();
        }
        public class Collection : List<DTOConsumerTicket>
        {

        }

        public class Return : DTOBaseGuid
        {
            public DateTime Fch { get; set; }
            public DTODocFile Docfile { get; set; }
            public String Num { get; set; }
            public DTOAmt Amount { get; set; }
        }
    }
}
