using DocumentFormat.OpenXml.Drawing.Diagrams;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOIncidencia : DTOBaseGuidNumFch
    {
        public class Compact
        {
            public Guid Guid { get; set; }
            public int Num { get; set; }
            public string Asin { get; set; }
            public DateTime Fch { get; set; }
            public DTOGuidNom.Compact Contact { get; set; }
            public DTOGuidNom.Compact Product { get; set; }
            public string SRef { get; set; }
            public bool ExistImages { get; set; }
            public bool ExistTickets { get; set; }
            public bool ExistVideos { get; set; }
            public bool IsOpen { get; set; }
            public string UsrCreated { get; set; }
        }

        public string Asin { get; set; }

        public Srcs src { get; set; }
        public DTOIncidenciaCod Codi { get; set; }

        public DTOCustomer Customer { get; set; }
        public ContactTypes ContactType { get; set; }
        public string CustomerNameAddress { get; set; }
        public string ContactPerson { get; set; }
        public string EmailAdr { get; set; }
        public string Tel { get; set; }
        public string CustomerRef { get; set; }
        public Procedencias Procedencia { get; set; }
        public Nullable<DateTime> FchCompra { get; set; }

        public string BoughtFrom { get; set; }
        public object Product { get; set; }
        public string SerialNumber { get; set; }
        public string ManufactureDate { get; set; }
        public string Description { get; set; }

        public List<DTODocFile> DocFileImages { get; set; }
        public bool ExistImages { get; set; }
        public List<DTODocFile> PurchaseTickets { get; set; }
        public bool ExistTickets { get; set; }
        public List<DTODocFile> Videos { get; set; }
        public bool ExistVideos { get; set; }

        public DTOProductCatalog Catalog { get; set; }
        public Guid BrandGuid { get; set; }
        public Guid CategoryGuid { get; set; }
        public Guid SkuGuid { get; set; }
        public bool AcceptedConditions { get; set; }

        public DTOSpv Spv { get; set; }
        public DateTime FchClose { get; set; }
        public DTOIncidenciaCod Tancament { get; set; }
        public DTOTracking.Collection Trackings { get; set; }
        public DTOUsrLog UsrLog { get; set; }

        public string Url { get; set; }

        public enum Srcs
        {
            notSet,
            Producte,
            transport
        }

        public enum ContactTypes
        {
            notSet,
            professional,
            consumidor
        }

        public enum AttachmentCods
        {
            ticket,
            imatge,
            video
        }

        public enum Procedencias
        {
            notSet,
            myShop,
            otherShops,
            expo
        }

        public DTOIncidencia() : base()
        {
            base.Fch = DTO.GlobalVariables.Now();
            DocFileImages = new List<DTODocFile>();
            PurchaseTickets = new List<DTODocFile>();
            Videos = new List<DTODocFile>(); //to deprecate, entrats per la operadora des de un email
            Trackings = new DTOTracking.Collection();
            UsrLog = new DTOUsrLog();
            Asin = DTO.Helpers.CryptoHelper.RandomString(10);
        }

        public DTOIncidencia(Guid oGuid) : base(oGuid)
        {
            DocFileImages = new List<DTODocFile>();
            PurchaseTickets = new List<DTODocFile>();
            Videos = new List<DTODocFile>(); //to deprecate, entrats per la operadora des de un email
            Trackings = new DTOTracking.Collection();
            UsrLog = new DTOUsrLog();
        }

        public static DTOIncidencia Factory(DTOIncidencia.ContactTypes oContactType, DTOIncidencia.Srcs oSrc = DTOIncidencia.Srcs.Producte)
        {
            DTOIncidencia retval = new DTOIncidencia();
            {
                var withBlock = retval;
                withBlock.ContactType = oContactType;
                withBlock.src = oSrc;
            }
            return retval;
        }

        public static DTOIncidencia Factory(DTOCustomer oCustomer, DTOIncidencia.Srcs oSrc)
        {
            var retval = DTOIncidencia.Factory(DTOIncidencia.ContactTypes.professional, oSrc);
            {
                var withBlock = retval;
                if (oCustomer != null)
                {
                    withBlock.Customer = oCustomer;
                    withBlock.CustomerNameAddress = DTOContact.htmlNameAndAddress(oCustomer);
                }
                withBlock.DocFileImages = new List<DTODocFile>();
                withBlock.PurchaseTickets = new List<DTODocFile>();
                withBlock.Fch = DTO.GlobalVariables.Now();
            }
            return retval;
        }

        public string Docfilename(DTODocFile docfile)
        {
            string qualifier = "";
            int index = 0;
            string mime = "";

            if (this.PurchaseTickets.Any(x => x.Hash == docfile.Hash))
            {
                qualifier = "ticket";
                index = this.PurchaseTickets.IndexOf(docfile) + 1;
                mime = MatHelperStd.MediaHelper.GetExtensionFromMime(docfile.Mime);
            }
            else if (this.DocFileImages.Any(x => x.Hash == docfile.Hash))
            {
                qualifier = "imagen";
                index = this.DocFileImages.IndexOf(docfile) + 1;
                mime = MatHelperStd.MediaHelper.GetExtensionFromMime(docfile.Mime);
            }
            else if (this.Videos.Any(x => x.Hash == docfile.Hash))
            {
                qualifier = "video";
                index = this.Videos.IndexOf(docfile) + 1;
                mime = MatHelperStd.MediaHelper.GetExtensionFromMime(docfile.Mime);
            }

            string retval = string.Format("incidencia.{0}.{1}.{2:00}{3}", this.AsinOrNum(), qualifier, index, mime);
            return retval;
        }

        public void restoreObjects()
        {
            JObject jProduct = this.Product as JObject;
            if (jProduct != null)
            {
                DTOProduct oProduct = jProduct.ToObject<DTOProduct>();
                switch (oProduct.SourceCod)
                {
                    case DTOProduct.SourceCods.Brand:
                        this.Product = jProduct.ToObject<DTOProductBrand>();
                        break;
                    case DTOProduct.SourceCods.Category:
                        this.Product = jProduct.ToObject<DTOProductCategory>();
                        break;
                    case DTOProduct.SourceCods.Sku:
                        this.Product = jProduct.ToObject<DTOProductSku>();
                        break;
                    default:
                        this.Product = jProduct.ToObject<DTOProduct>();
                        break;
                }
            }
            foreach (DTOTracking tracking in this.Trackings)
            {
                tracking.Target = this.ToGuidNom();
            }
        }

        public string AsinOrNum()
        {
            return string.IsNullOrEmpty(Asin) ? Num.ToString() : Asin;
        }

        public string AsinAndNum()
        {
            string retval = "";

            if( string.IsNullOrEmpty(Asin))
                retval =Num.ToString();
            else
                retval = string.Format("{0} ({1})",Asin,Num.ToString());
            return retval;
        }

        public string NomRemitent()
        {
            if (ContactType == ContactTypes.consumidor)
                return ContactPerson;
            else
                return Customer.NomComercialOrDefault();
        }

        public List<DTODocFile> Attachments()
        {
            List<DTODocFile> retval = new List<DTODocFile>();
            retval.AddRange(PurchaseTickets);
            retval.AddRange(DocFileImages);
            retval.AddRange(Videos);
            return retval;
        }

        public List<DTODocFile> Attachments(DTOIncidencia.AttachmentCods[] attachmentCods)
        {
            List<DTODocFile> retval = new List<DTODocFile>();
            if (attachmentCods.Contains(DTOIncidencia.AttachmentCods.ticket))
                retval.AddRange(PurchaseTickets);
            if (attachmentCods.Contains(DTOIncidencia.AttachmentCods.imatge))
                retval.AddRange(DocFileImages);
            if (attachmentCods.Contains(DTOIncidencia.AttachmentCods.video))
                retval.AddRange(Videos);
            return retval;
        }

        public List<DTODocFile> NonVideoAttachments()
        {
            List<DTODocFile> retval = new List<DTODocFile>();
            retval.AddRange(PurchaseTickets);
            retval.AddRange(DocFileImages);
            return retval;
        }

        public static string CodiNom(DTOIncidenciaCod oCod, DTOLang oLang)
        {
            string retval = "";
            if (oCod != null)
                retval = oCod.nom.Tradueix(oLang);
            return retval;
        }

        public static string MultilineText(DTOIncidencia oIncidencia)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("Incidencia #" + oIncidencia.AsinOrNum() + " del " + VbUtilities.Format(oIncidencia.Fch, "dd/MM/yy"));
            sb.AppendLine(oIncidencia.Customer.FullNom);
            sb.AppendLine(((DTOProduct)oIncidencia.Product).FullNom(DTOLang.ESP()));
            string retval = sb.ToString();
            return retval;
        }

        public string UrlSegment()
        {
            return base.urlSegment("incidencia");
        }



        public static MatHelper.Excel.Sheet ExcelReposicions(List<DTOIncidencia> items)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            {
                var withBlock = retval;
                withBlock.AddColumn("registre", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("data", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("client", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("marca", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("categoria", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Producte", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("cost", MatHelper.Excel.Cell.NumberFormats.Euro);
            }

            foreach (DTOIncidencia item in items)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                {
                    var withBlock = item;
                    oRow.AddCell(withBlock.AsinOrNum(), withBlock.UrlSegment());
                    oRow.AddCell(withBlock.Fch);
                    oRow.AddCell(withBlock.Customer.FullNom);
                    oRow.AddCell(DTOProduct.BrandNom((DTOProduct)withBlock.Product));
                    oRow.AddCell(DTOProduct.CategoryNom((DTOProduct)withBlock.Product));
                    oRow.AddCell(DTOProduct.SkuNom((DTOProduct)withBlock.Product));
                    oRow.AddCell(DTOProduct.SkuCostEur((DTOProduct)withBlock.Product));
                }
            }
            return retval;
        }

        public DTOTracking TrackRead(DTOUser user)
        {
            DTOTracking retval = DTOTracking.Factory(DTOCod.Wellknowns.Read, user);
            retval.Target = this.ToGuidNom();
            return retval;
        }

        public bool isAlreadyRead()
        {
            bool retval = this.Trackings.isAlreadyRead();
            return retval;
        }

        public DTOGuidNom ToGuidNom()
        {
            string nom = "";
            if (this.Customer != null)
            {
                nom = this.Customer.FullNom;
            }
            DTOGuidNom retval = DTOGuidNom.Factory(this.Guid, string.Format("Incidencia {0} de {1}", this.AsinOrNum(), this.Customer));
            return retval;
        }

        public class HeaderModel
        {
            public Guid Guid { get; set; }
            public int Num { get; set; }
            public DateTime Fch { get; set; }
            public DTOGuidNom.Compact Contact { get; set; }
            public string Product { get; set; }
            public string SRef { get; set; }

            public bool HasTickets { get; set; }
            public bool HasImages { get; set; }
            public bool HasVideos { get; set; }
            public bool IsOpen { get; set; }
            public string UsrCreated { get; set; }
        }
    }

    public class DTOIncidenciaQuery
    {
        public DTOEmp emp { get; set; }
        public DTOLang Lang { get; set; }
        public DTOProductCatalog Catalog { get; set; }
        public DTOProduct Product { get; set; }
        public DTOCustomer Customer { get; set; }
        public DTOManufacturer Manufacturer { get; set; }
        public DTOIncidencia.Srcs Src { get; set; }
        public DTOIncidenciaCod Codi { get; set; }
        public DTOIncidenciaCod Tancament { get; set; }
        public bool IncludeClosed { get; set; }
        public int Year { get; set; }
        public List<DTOIncidencia> result { get; set; }
        public bool Unauthorized { get; set; }

        public static DTOIncidenciaQuery Factory(DTOUser oUser, DTOLang oLang = null, int year = 0)
        {
            DTOIncidenciaQuery retval = new DTOIncidenciaQuery();
            {
                var withBlock = retval;
                withBlock.emp = oUser.Emp;
                withBlock.Lang = (oLang == null) ? oUser.Lang : oLang;

                withBlock.Year = (year == 0) ? DTO.GlobalVariables.Today().Year : year;
                withBlock.Src = DTOIncidencia.Srcs.notSet;
                if (oUser.Rol.isStaff())
                    withBlock.IncludeClosed = false;
                else
                {
                    withBlock.Src = DTOIncidencia.Srcs.Producte;
                    switch (oUser.Rol.id)
                    {
                        case DTORol.Ids.cliFull:
                        case DTORol.Ids.cliLite:
                            {
                                withBlock.IncludeClosed = false;
                                withBlock.Customer = new DTOCustomer(oUser.Contact.Guid);
                                break;
                            }

                        case DTORol.Ids.manufacturer:
                            {
                                withBlock.IncludeClosed = true;
                                withBlock.Manufacturer = new DTOManufacturer(oUser.Contact.Guid);
                                withBlock.Src = DTOIncidencia.Srcs.Producte;
                                break;
                            }

                        default:
                            {
                                withBlock.Unauthorized = true;
                                break;
                            }
                    }
                }
            }
            return retval;
        }
    }
}
