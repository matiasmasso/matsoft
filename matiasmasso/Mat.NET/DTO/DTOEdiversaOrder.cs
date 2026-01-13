using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOEdiversaOrder : DTOBaseGuid
    {
        public string DocNum { get; set; }
        public DateTime FchDoc { get; set; }
        public DateTime FchDeliveryMin { get; set; }
        public DateTime FchDeliveryMax { get; set; }
        public string Tipo { get; set; }
        public Funcions Funcion { get; set; }
        public DTOCur Cur { get; set; }
        public string Obs { get; set; }

        public DTOCustomer Comprador { get; set; }
        public DTOEan NADMS { get; set; }
        public DTOEan ProveedorEAN { get; set; }
        public DTOEan CompradorEAN { get; set; }
        public DTOEan FacturarAEAN { get; set; }
        public DTOEan ReceptorMercanciaEAN { get; set; }
        public DTOContact Proveedor { get; set; }
        public DTOCustomer Customer { get; set; }
        public DTOCustomer FacturarA { get; set; } // a qui es factura
        public DTOContact ReceptorMercancia { get; set; }

        public string Centro { get; set; }
        public string Departamento { get; set; }
        public string NumProveidor { get; set; }
        public DTOAmt Amt { get; set; }
        public DTOPurchaseOrder Result { get; set; }

        public DTOEdiversaFile EdiversaFile { get; set; }
        public List<DTOEdiversaOrderItem> Items { get; set; }
        public List<DTOEdiversaException> Exceptions { get; set; }

        public enum Funcions
        {
            NotSet
        }


        public DTOEdiversaOrder() : base()
        {
            Items = new List<DTOEdiversaOrderItem>();
            Exceptions = new List<DTOEdiversaException>();
        }

        public DTOEdiversaOrder(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTOEdiversaOrderItem>();
            Exceptions = new List<DTOEdiversaException>();
        }

        public static DTOEdiversaOrder Factory(DTOEdiversaFile file)
        {
            DTOEdiversaOrder retval = new DTOEdiversaOrder();
            try
            {
                retval.EdiversaFile = file;
                retval.Guid = retval.EdiversaFile.Guid;
                file.LoadSegments();

                DTOEdiversaOrderItem oItem = null;
                foreach (DTOEdiversaSegment segment in file.Segments)
                {
                    switch (segment.Tag())
                    {
                        case "ORD":
                            retval.DocNum = segment.Fields[1];
                            retval.Tipo = segment.Fields[2];
                            break;

                        case "DTM":
                            retval.FchDoc = DTOEdiversaFile.ParseFch(segment.Fields[1], retval.Exceptions);

                            if (segment.Fields.Count > 2)
                            {
                                retval.FchDeliveryMin = DTOEdiversaFile.ParseFch(segment.Fields[2], retval.Exceptions); // delivery date requested
                                if (segment.Fields.Count > 4)
                                {
                                    retval.FchDeliveryMax = DTOEdiversaFile.ParseFch(segment.Fields[4], retval.Exceptions);
                                    if (segment.Fields.Count > 5)
                                        retval.FchDeliveryMin = DTOEdiversaFile.ParseFch(segment.Fields[5], retval.Exceptions);
                                }
                            }
                            break;

                        case "FTX":
                            if (!string.IsNullOrEmpty(retval.Obs))
                                retval.Obs += Environment.NewLine;
                            retval.Obs += segment.Fields[3];
                            break;

                        case "NADMS": //message sender
                            retval.NADMS = DTOEan.Factory(segment.Fields[1]);
                            break;

                        case "NADSU": //proveidor
                            retval.ProveedorEAN = DTOEan.Factory(segment.Fields[1]);
                            break;

                        case "NADIV": //facturar a
                            retval.FacturarAEAN = DTOEan.Factory(segment.Fields[1]);
                            break;

                        case "NADBY": //buyer
                            retval.CompradorEAN = DTOEan.Factory(segment.Fields[1]);

                            DTOEdiversaFile.Interlocutors interlocutor = DTOEdiversaFile.ReadInterlocutor(retval.CompradorEAN);
                            if (interlocutor == DTOEdiversaFile.Interlocutors.ElCorteIngles)
                            {
                                if (segment.Fields.Count > 2)
                                    retval.Departamento = segment.Fields[2];
                                if (segment.Fields.Count > 4)
                                    retval.Centro = segment.Fields[4];
                            }
                            break;

                        case "NADDP": //receptor  de la mercancia
                            retval.ReceptorMercanciaEAN = DTOEan.Factory(segment.Fields[1]);
                            break;

                        case "CUX":
                            if (segment.Fields.Count > 1)
                                retval.Cur = DTOCur.Factory(segment.Fields[1]);
                            break;

                        case "LIN":
                            oItem = new DTOEdiversaOrderItem();
                            oItem.Parent = retval;
                            if (segment.Fields.Count > 3)
                            {
                                oItem.Ean = DTOEan.Factory(segment.Fields[1]);
                                oItem.Lin = segment.Fields[3].toInteger();
                            }
                            else
                                oItem.AddException(DTOEdiversaException.Cods.MissingSegmentFields, "segment PIALIN.IN (ref.client) amb menys de 3 camps");
                            retval.Items.Add(oItem);
                            break;

                        case "PIALIN":
                            switch (segment.Fields[1])
                            {
                                case "IN":
                                case "BP":
                                    if (segment.Fields.Count > 2)
                                        oItem.RefClient = segment.Fields[2];
                                    else
                                        oItem.AddException(DTOEdiversaException.Cods.MissingSegmentFields, "segment PIALIN.IN (ref.client) amb menys de 3 camps");
                                    break;
                                case "SA":
                                    if (segment.Fields.Count > 2)
                                        oItem.RefProveidor = segment.Fields[2];
                                    else
                                        oItem.AddException(DTOEdiversaException.Cods.MissingSegmentFields, "segment PIALIN.SA (ref.proveidor) amb menys de 3 camps");
                                    break;
                            }
                            break;

                        case "IMDLIN":
                            switch (segment.Fields[1])
                            {
                                case "F":
                                    if (segment.Fields.Count > 4)
                                        oItem.Dsc = segment.Fields[4];
                                    //else
                                        //oItem.AddException(DTOEdiversaException.Cods.MissingSegmentFields, "segment IMDLIN.F (Descripció) amb menys de 5 camps");
                                        //afegir una excepcio aqui invalidaria continuar amb la validacio de preu'
                                    break;
                            }
                            break;

                        case "QTYLIN":
                            switch (segment.Fields[1])
                            {
                                case "21": //Unidades pedidas del artículo
                                    if (segment.Fields.Count > 2)
                                        oItem.Qty = segment.Fields[2].toInteger();
                                    else
                                        oItem.AddException(DTOEdiversaException.Cods.MissingSegmentFields, "segment QTY.21 (unitats) amb menys de 3 camps");
                                    break;
                            }
                            break;

                        case "PRILIN":
                            switch (segment.Fields[1])
                            {
                                case "AAB": //Preu brut abans de descomptes
                                    if (segment.Fields.Count > 2)
                                        oItem.Preu = DTOEdiversaFile.ParseAmt(segment.Fields[2], oItem.Exceptions);
                                    else
                                        oItem.AddException(DTOEdiversaException.Cods.MissingSegmentFields, "segment PRILIN.AAB (preu brut) amb menys de 3 camps");
                                    break;
                                case "AAA": //Preu brut despres de descomptes pero abans de impostos (Amazon)
                                    if (segment.Fields.Count > 2)
                                        oItem.PreuNet = DTOEdiversaFile.ParseAmt(segment.Fields[2], oItem.Exceptions);
                                    else
                                        oItem.AddException(DTOEdiversaException.Cods.MissingSegmentFields, "segment PRILIN.AAA (preu net) amb menys de 3 camps");
                                    break;
                            }
                            break;

                        case "ALCLIN":
                            switch (segment.Fields[1])
                            {
                                case "A": //Descomptes
                                    if (segment.Fields.Count > 10)
                                        oItem.Dto = DTOEdiversaFile.ParseDecimal(segment.Fields[10], oItem.Exceptions);
                                    else
                                        oItem.AddException(DTOEdiversaException.Cods.MissingSegmentFields, "segment ALCLIN.A (descomptes) amb menys de 11 camps");
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                retval.AddException(DTOEdiversaException.Cods.NotSet, ex.Message);
            }
            return retval;
        }

        public bool isDuplicate()
        {
            bool retval = Exceptions.Any(x => x.Cod == DTOEdiversaException.Cods.DuplicatedOrder);
            return retval;
        }

        public void AddException(DTOEdiversaException.Cods oCod, string sMsg, DTOEdiversaException.TagCods oTagCod = DTOEdiversaException.TagCods.NotSet, DTOBaseGuid oTag = null/* TODO Change to default(_) if this is not a reference type */)
        {
            var oException = DTOEdiversaException.Factory(oCod, oTag, sMsg);
            oException.TagCod = oTagCod;
            Exceptions.Add(oException);
        }

        public string Report()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DTOEdiversaException ex in Exceptions)
                sb.AppendLine(ex.Msg);
            foreach (var ex in Items.SelectMany(x => x.Exceptions))
                sb.AppendLine(ex.Msg);
            return sb.ToString();
        }

        public void RestoreTagsToOriginalObjects()
        {
            foreach (var ex in Exceptions)
            {
                switch (ex.TagCod)
                {
                    case DTOEdiversaException.TagCods.EdiversaOrder:
                        {
                            ex.Tag = this;
                            break;
                        }
                }
            }
            foreach (var item in Items)
            {
                item.Parent = this;
                foreach (var ex in item.Exceptions)
                {
                    switch (ex.TagCod)
                    {
                        case DTOEdiversaException.TagCods.EdiversaOrder:
                            {
                                ex.Tag = this;
                                break;
                            }

                        case DTOEdiversaException.TagCods.EdiversaOrderItem:
                            {
                                ex.Tag = item;
                                break;
                            }
                    }
                }
            }
        }
    }


    public class DTOEdiversaOrderItem : DTOBaseGuid
    {
        public DTOEdiversaOrder Parent { get; set; }
        public int Lin { get; set; }
        public DTOEan Ean { get; set; }
        public string RefProveidor { get; set; }
        public string RefClient { get; set; }
        public string Dsc { get; set; }
        public DTOProductSku Sku { get; set; }
        public int Qty { get; set; }
        public DTOAmt Preu { get; set; }

        public DTOAmt PreuNet { get; set; }
        public decimal Dto { get; set; }
        public DTOUser SkipPreuValidationUser { get; set; }
        public DateTime SkipPreuValidationFch { get; set; }
        public DTOUser SkipDtoValidationUser { get; set; }
        public DateTime SkipDtoValidationFch { get; set; }
        public DTOUser SkipItemUser { get; set; }
        public DateTime SkipItemFch { get; set; }
        public List<DTOEdiversaException> Exceptions { get; set; }

        public DTOEdiversaOrderItem() : base()
        {
            Exceptions = new List<DTOEdiversaException>();
        }

        public DTOEdiversaOrderItem(Guid oGuid) : base(oGuid)
        {
            Exceptions = new List<DTOEdiversaException>();
        }

        public void AddException(DTOEdiversaException.Cods oCod, string sMsg, DTOEdiversaException.TagCods oTagCod = DTOEdiversaException.TagCods.NotSet, DTOBaseGuid oTag = null/* TODO Change to default(_) if this is not a reference type */)
        {
            var oException = DTOEdiversaException.Factory(oCod, oTag, sMsg);
            oException.TagCod = oTagCod;
            Exceptions.Add(oException);
        }

        public bool Validate(List<DTOProductSku> skus, List<DTOPricelistItemCustomer> customCosts, List<DTOCustomerTarifaDto> customerTarifaDtos, List<DTOCliProductDto> cliProductDtos)
        {
            bool retval = true;
            this.Sku = skus.FirstOrDefault(x => x.Ean13.Equals(this.Ean));
            if (this.Sku == null)
                AddException(DTOEdiversaException.Cods.SkuNotFound, String.Format("producte {0} desconegut", DTOEan.eanValue(this.Ean)));
            else
            {
                if (this.Parent.Customer != null)
                {
                    DTOAmt oPreu = this.Preu;
                    if (oPreu == null)
                    {
                        oPreu = this.PreuNet;
                        if (oPreu != null && this.Dto != 0)
                        {
                            oPreu = oPreu.DividedBy((100 - this.Dto) / 100);
                        }
                    }
                    Sku.Price = Sku.GetCustomerCost(customCosts, customerTarifaDtos);
                    if (oPreu == null)
                        AddException(DTOEdiversaException.Cods.MissingPrice, String.Format("falta preu a {0}", Sku.RefYNomLlarg().Esp));
                    else if (Sku.Price.unEquals(oPreu) && SkipPreuValidationUser == null)
                        AddException(DTOEdiversaException.Cods.WrongPrice, String.Format("{0} de preu {1} demanat per {2}", Sku.RefYNomLlarg().Esp, Sku.Price.Formatted(), oPreu.Formatted()));

                    DTOCliProductDto dto = Sku.CliProductDto(cliProductDtos);
                    Decimal dcDto = dto == null ? 0 : dto.Dto;

                    if (dcDto != Dto && SkipDtoValidationUser == null)
                        AddException(DTOEdiversaException.Cods.WrongDiscount, String.Format("Demanat amb descompte del {0}% en lloc del {1}%", this.Dto, dcDto));
                }
            }
            return retval;
        }
    }
}
