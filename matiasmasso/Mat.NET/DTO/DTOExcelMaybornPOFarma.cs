using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOExcelMaybornPOFarma : MatHelper.Excel.Sheet
    {
        public enum Cols
        {
            Customer,
            Delivery_Sequence,
            Name,
            Customer_Item_Ref,
            Item,
            Description,
            Order_Number,
            Customer_Order_Ref,
            Order_Line,
            Quantity_Outstanding,
            Date_Delivery_Required,
            Order_Value,
            Allocated,
            Delivery_Address,
            Despatch_Date,
            Quantity_Allocated,
            Date,
            Validation
        }

        public static DTOExcelMaybornPOFarma Factory(MatHelper.Excel.Sheet oSheet)
        {
            DTOExcelMaybornPOFarma retval = new DTOExcelMaybornPOFarma();
            // Dim iLastCol = [Enum].ToObject(GetType(Cols), [Enum].GetValues(GetType(Cols)).Cast(Of Integer).Last)
            MatHelper.Excel.Column oColumn = null/* TODO Change to default(_) if this is not a reference type */;
            foreach (var oRow in oSheet.Rows)
            {
                try
                {
                    if (oRow.Equals(oSheet.Rows.First()))
                    {
                        for (var iCol = 0; iCol <= (int)Cols.Validation; iCol++)
                        {
                            switch (iCol)
                            {
                                case (int)Cols.Quantity_Allocated:
                                    {
                                        oColumn = new MatHelper.Excel.Column(oRow.Cells[iCol].Content.ToString(), MatHelper.Excel.Cell.NumberFormats.Integer);
                                        break;
                                    }

                                case (int)Cols.Validation:
                                    {
                                        oColumn = new MatHelper.Excel.Column("Validation", MatHelper.Excel.Cell.NumberFormats.PlainText);
                                        break;
                                    }

                                default:
                                    {
                                        oColumn = new MatHelper.Excel.Column(oRow.Cells[iCol].Content.ToString(), MatHelper.Excel.Cell.NumberFormats.NotSet);
                                        break;
                                    }
                            }
                            retval.Columns.Add(oColumn);
                        }
                    }
                    else
                    {
                        oRow.Cells.Add(new MatHelper.Excel.Cell()); // Add validation cell
                        retval.Rows.Add(oRow);
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
            return retval;
        }

        public DTOExcelMaybornPOFarma Validate(List<DTOProductSku> oInventari, List<DTOPrvCliNum> oCustomers)
        {
            foreach (var oRow in base.Rows)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (!oCustomers.Any(x => x.CliNum == oRow.GetString((int)Cols.Customer)))
                    sb.Append("Unknown customer. ");
                var oSku = this.Sku(oRow, oInventari);
                if (oSku == null)
                    sb.Append("Unknown Sku. ");
                else
                {
                    var iQty = oRow.GetInt((int)Cols.Quantity_Outstanding);
                    var iStock = DTOProductSku.StockAvailable(oSku);
                    var iAvailable = Math.Min(iQty, iStock);
                    if (iAvailable == 0)
                        sb.Append("Not available");
                    else if (iAvailable == iQty)
                        oRow.Cells[(int)Cols.Quantity_Allocated].Content = iAvailable;
                    else
                        sb.Append("Partial quantity");

                    var iMoq = DTOProductSku.OuterPackOrInherited(oSku);
                    if (iMoq == 0)
                        iMoq = DTOProductSku.InnerPackOrInherited(oSku);
                    if (iQty % iMoq != 0)
                        sb.Append(string.Format("Quantity not multiple of {0}", iMoq));
                }

                if (sb.ToString() == "")
                    oRow.Cells[(int)Cols.Validation].Content = "Ok";
                else
                    oRow.Cells[(int)Cols.Validation].Content = sb.ToString();
            }
            return this;
        }


        public List<DTOPurchaseOrder> PurchaseOrders(List<DTOProductSku> oInventari, List<DTOPrvCliNum> oPrvCliNums, DTOUser oUser)
        {
            List<DTOPurchaseOrder> retval = new List<DTOPurchaseOrder>();
            foreach (var oFarmaOrder in this.FarmaOrders())
            {
                var oPurchaseOrder = PurchaseOrder(oFarmaOrder, oInventari, oPrvCliNums, oUser);
                retval.Add(oPurchaseOrder);
            }
            return retval;
        }

        private DTOPurchaseOrder PurchaseOrder(FarmaOrder oFarmaOrder, List<DTOProductSku> oInventari, List<DTOPrvCliNum> oPrvCliNums, DTOUser oUser)
        {
            var oPrvCliNum = oPrvCliNums.FirstOrDefault(x => x.CliNum == oFarmaOrder.CustomerNum);
            var sConcept = string.Format("{0} (Mayborn #{1})", oFarmaOrder.CustomerOrderNum, oFarmaOrder.MaybornOrderNum);
            var retval = DTOPurchaseOrder.Factory(oPrvCliNum.Customer, oUser, oFarmaOrder.OrderDate, DTOPurchaseOrder.Sources.ExcelMayborn, sConcept);
            foreach (var oRow in FarmaOrderRows(oFarmaOrder.MaybornOrderNum))
            {
                var item = PurchaseOrderItem(oRow, oInventari);
                if (item.Qty > 0)
                    retval.Items.Add(PurchaseOrderItem(oRow, oInventari));
            }
            return retval;
        }

        private DTOPurchaseOrderItem PurchaseOrderItem(MatHelper.Excel.Row oRow, List<DTOProductSku> oInventari)
        {
            DTOPurchaseOrderItem retval = new DTOPurchaseOrderItem();
            {
                var withBlock = retval;
                withBlock.Sku = Sku(oRow, oInventari);
                withBlock.Qty = oRow.GetInt((int)Cols.Quantity_Allocated);
                withBlock.Pending = withBlock.Qty;
                if (withBlock.Sku != null)
                    withBlock.Price = withBlock.Sku.Cost;
            }
            return retval;
        }

        private DTOProductSku Sku(MatHelper.Excel.Row oRow, List<DTOProductSku> oInventari)
        {
            return oInventari.FirstOrDefault(x => x.RefProveidor == oRow.GetString((int)Cols.Item));
        }


        public List<FarmaOrder> FarmaOrders()
        {
            List<FarmaOrder> retval = new List<FarmaOrder>();
            foreach (var oRow in base.Rows)
            {
                var oFarmaOrder = DTOExcelMaybornPOFarma.FarmaOrder.Factory(oRow.GetString((int)Cols.Order_Number), oRow.GetString((int)Cols.Customer_Order_Ref), oRow.GetString((int)Cols.Customer), oRow.GetFchSpain((int)Cols.Date));
                if (!string.IsNullOrEmpty(oFarmaOrder.MaybornOrderNum))
                {
                    if (!retval.Any(x => x.MaybornOrderNum == oFarmaOrder.MaybornOrderNum))
                        retval.Add(oFarmaOrder);
                }
            }
            return retval;
        }

        public List<MatHelper.Excel.Row> FarmaOrderRows(string MaybornOrderNum)
        {
            var retval = base.Rows.Where(x => x.GetString((int)Cols.Order_Number) == MaybornOrderNum).ToList();
            return retval;
        }

        public class FarmaOrder
        {
            public string MaybornOrderNum { get; set; }
            public string CustomerOrderNum { get; set; }
            public string CustomerNum { get; set; }
            public DateTime OrderDate { get; set; }

            public static FarmaOrder Factory(string MaybornOrderNum, string CustomerOrderNum, string CustomerNum, DateTime OrderDate)
            {
                FarmaOrder retval = new FarmaOrder();
                {
                    var withBlock = retval;
                    withBlock.MaybornOrderNum = MaybornOrderNum;
                    withBlock.CustomerOrderNum = CustomerOrderNum;
                    withBlock.CustomerNum = CustomerNum;
                    withBlock.OrderDate = OrderDate;
                }
                return retval;
            }
        }
    }
}
