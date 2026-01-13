using MatHelperStd;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTORepProduct : DTOBaseGuid
    {
        public DTORep Rep { get; set; }
        public object Product { get; set; }
        public object Area { get; set; }
        public DTODistributionChannel DistributionChannel { get; set; }
        public Cods Cod { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public decimal ComStd { get; set; }
        public decimal ComRed { get; set; }


        public enum Cods
        {
            notSet,
            included,
            excluded
        }

        public DTORepProduct() : base()
        {
        }

        public DTORepProduct(Guid oGuid) : base(oGuid)
        {
        }

        public static DTORepProduct Factory(DTOProduct oProduct, DTORep oRep = null/* TODO Change to default(_) if this is not a reference type */, DTOArea oArea = null/* TODO Change to default(_) if this is not a reference type */, DTODistributionChannel oDistributionChannel = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTORepProduct retval = new DTORepProduct();
            {
                var withBlock = retval;
                withBlock.Product = oProduct;
                withBlock.Area = oArea;
                withBlock.DistributionChannel = oDistributionChannel;
                withBlock.FchFrom = DTO.GlobalVariables.Today();
                if (oRep != null)
                {
                    withBlock.Rep = oRep;
                    withBlock.ComStd = oRep.ComisionStandard;
                    withBlock.ComRed = oRep.ComisionReducida;
                }
                withBlock.Cod = DTORepProduct.Cods.included;
                withBlock.IsNew = true;
            }
            return retval;
        }

        public void RestoreObjects()
        {
            this.Product = DTOProduct.fromJObject(this.Product as JObject);
            this.Area = DTOArea.fromJObject(this.Area as JObject);
        }


        public static bool IsActive(DTORepProduct oRepProduct)
        {
            bool retval = oRepProduct.FchFrom <= DTO.GlobalVariables.Today();
            if (oRepProduct.FchTo != default(DateTime))
            {
                if (oRepProduct.FchTo < DTO.GlobalVariables.Today())
                    retval = false;
            }
            return retval;
        }

        public static DTORepProduct Clon(DTORepProduct src)
        {
            var retval = DTORepProduct.Factory((DTOProduct)src.Product, src.Rep, (DTOArea)src.Area);
            {
                var withBlock = retval;
                withBlock.Cod = src.Cod;
                withBlock.FchFrom = src.FchFrom;
                withBlock.FchTo = src.FchTo;
                withBlock.ComStd = src.ComStd;
                withBlock.ComRed = src.ComRed;
            }
            return retval;
        }

        public new string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (Rep != null)
            {
                string sNom = Rep.Nom == "" ? Rep.FullNom : Rep.Nom;
                sb.Append("rep: " + sNom + " ");
            }
            if (Product != null)
                sb.Append("product: " + ((DTOProduct)Product).Nom.Esp + " ");
            if (Area != null)
            {
                if (Area is DTOZona)
                {
                    sb.Append("area: ");
                    DTOCountry oCountry = ((DTOZona)Area).Country;
                    if (oCountry != null)
                        sb.Append(oCountry.LangNom.Esp + "/");
                    sb.Append(((DTOZona)Area).Nom + " ");
                }
            }
            if (DistributionChannel != null)
                sb.Append("canal: " + DistributionChannel.LangText.Esp + " ");
            sb.Append("(" + Cod.ToString() + ")");
            string retval = sb.ToString();
            return retval;
        }

        public static List<DTORepProduct> Match(List<DTORepProduct> oRepProducts, DTODistributionChannel oChannel, DTOZip oZip, DTOProductSku oSku, DateTime DtFch)
        {
            List<DTORepProduct> retval = new List<DTORepProduct>();

            // filtra per data
            List<DTORepProduct> Step1 = oRepProducts.FindAll(x => x.FchFrom <= DtFch & (x.FchTo == default(DateTime) | x.FchTo >= DtFch)).ToList();
            // filtra per zona
            List<DTORepProduct> Step2 = Step1.FindAll(x => (((DTOArea)x.Area).Guid.Equals(oZip.Guid) | ((DTOArea)x.Area).Guid.Equals(oZip.Location.Guid) | ((DTOArea)x.Area).Guid.Equals(oZip.Location.Zona.Guid) | ((DTOArea)x.Area).Guid.Equals(oZip.Location.Zona.Country.Guid))).ToList();
            // filtra per producte
            List<DTORepProduct> Step3 = Step2.FindAll(x => (((DTOProduct)x.Product).Guid.Equals(oSku.Guid) | ((DTOProduct)x.Product).Guid.Equals(oSku.Category.Guid) | ((DTOProduct)x.Product).Guid.Equals(oSku.Category.Brand.Guid))).ToList();

            // filtra per canal de distribució o surt si el destinatari no está assignat a cap canal
            List<DTORepProduct> Step4 = new List<DTORepProduct>();
            if (oChannel != null)
            {
                Step4 = Step3.FindAll(x => x.DistributionChannel.Guid.Equals(oChannel.Guid)).ToList();

                // identifica els exclosos
                List<DTORep> oRepsToRemove = new List<DTORep>();
                foreach (DTORepProduct item in Step4.FindAll(x => x.Cod == DTORepProduct.Cods.excluded))
                    oRepsToRemove.Add(item.Rep);

                // llista els inclosos que el seu rep no ha estat exclos
                foreach (DTORepProduct item in Step4.FindAll(x => x.Cod == DTORepProduct.Cods.included))
                {
                    if (!oRepsToRemove.Exists(x => x.Guid.Equals(item.Rep.Guid)))
                        retval.Add(item);
                }
            }
            return retval;
        }


        public static MatHelper.Excel.Sheet Excel(List<DTORepProduct> items)
        {
            bool ColProducts = items.Exists(x => x.Product != null);
            bool ColFchFrom = items.Exists(x => x.FchFrom != default(DateTime));

            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            {
                var withBlock = retval;
                withBlock.AddColumn("zona");
                withBlock.AddColumn("representante");
                if (ColProducts)
                    withBlock.AddColumn("producto");
                if (ColFchFrom)
                    withBlock.AddColumn("desde");
                withBlock.AddColumn("movil");
            }

            foreach (DTORepProduct item in items)
            {
                if (DTORepProduct.IsActive(item))
                {
                    MatHelper.Excel.Row oRow = retval.AddRow();
                    {
                        var withBlock = item;
                        oRow.AddCell(DTOArea.nomOrDefault((DTOArea)withBlock.Area));
                        oRow.AddCell(withBlock.Rep.NickName);
                        if (ColProducts)
                            oRow.AddCell(DTOProduct.GetNom((DTOProduct)withBlock.Product));
                        if (ColFchFrom)
                            oRow.AddCell(withBlock.FchFrom);
                        oRow.AddCell(withBlock.Rep.Telefon.Replace(" ", ""));
                    }
                }
            }

            return retval;
        }
    }
}
