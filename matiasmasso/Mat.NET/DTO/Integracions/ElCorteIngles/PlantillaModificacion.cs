using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Integracions.ElCorteIngles
{
    public class PlantillaModificacion
    {
        public List<Item> Items { get; set; }

        public enum Cols : int
        {
            Dpto = 0,
            Familia,
            Barra,
            Talla,
            Descripcion = 5,
            Marca = 6,
            NumProveidor = 7,
            RazonSocial = 8,
            ModFabr = 10,
            Color = 11,
            EAN = 14,
            TG = 19,
            UE = 20,
            Pedido = 25,
            LugarEntrega = 26,
            FchEntrega = 27,
            FchTope = 28,
            Qty = 29,
            Coste = 30,
            Pvp = 31,
            PvpFchFrom = 32,
            Obs = 39
        }




        public static PlantillaModificacion Factory(DateTime fchEntrega, List<DTOPurchaseOrder> orders)
        {
            PlantillaModificacion retval = new PlantillaModificacion();
            retval.Items = new List<Item>();
            foreach (DTOPurchaseOrder order in orders)
            {
                PlantillaModificacion.Item item = PlantillaModificacion.Item.Factory(order, fchEntrega);

                retval.Items.Add(item);
            }
            return retval;
        }
        public static PlantillaModificacion Factory(DateTime fchEntrega, List<DTODeliveryItem> deliveryItems, List<DTOCustomerProduct> customerProducts)
        {
            PlantillaModificacion retval = new PlantillaModificacion();
            retval.Items = new List<Item>();
            foreach (DTODeliveryItem deliveryItem in deliveryItems)
            {
                PlantillaModificacion.Item item = PlantillaModificacion.Item.Factory(deliveryItem, customerProducts, fchEntrega);

                retval.Items.Add(item);
            }
            return retval;
        }


        public DTOCsv Csv()
        {
            DTOCsvRow row = new DTOCsvRow();
            DTOCsv retval = new DTOCsv("PLANTILLA MODIFICACION", "PLANTILLA MODIFICACION");


            row = retval.AddRow();
            row.AddCell("Dept");
            //row.AddCell(item.Familia);
            //row.AddCell(item.Barra);
            //row.AddCell(item.Talla);
            //row.AddCell(item.Descripcion);
            //row.AddCell(item.Marca);
            row.AddCell("Proveedor");
            row.AddCell("Fabricante");
            //row.AddCell(item.ModFabr);
            //row.AddCell(item.Color);
            //row.AddCell(item.EAN);
            //row.AddCell(item.TG);
            //row.AddCell(item.UE);
            row.AddCell("NºPedido");
            //row.AddCell(item.LugarEntrega);
            row.AddCell("Fecha entrega");
            row.AddCell("Fecha tope");
            //row.AddCell(item.Qty.ToString());
            //row.AddCell(item.Coste.ToString("N2"));
            //row.AddCell(item.Pvp.ToString("N2"));
            //row.AddCell(item.PvpFchFrom.ToString("dd/MM/yyyy"));
            row.AddCell("Observaciones");

            foreach (PlantillaModificacion.Item item in this.Items)
            {
                row = retval.AddRow();
                row.AddCell(item.Dpto);
                //row.AddCell(item.Familia);
                //row.AddCell(item.Barra);
                //row.AddCell(item.Talla);
                //row.AddCell(item.Descripcion);
                //row.AddCell(item.Marca);
                row.AddCell(item.NumProveidor);
                row.AddCell(item.RazonSocial);
                //row.AddCell(item.ModFabr);
                //row.AddCell(item.Color);
                //row.AddCell(item.EAN);
                //row.AddCell(item.TG);
                //row.AddCell(item.UE);
                row.AddCell(item.Pedido);
                //row.AddCell(item.LugarEntrega);
                row.AddCell(item.FchEntrega.ToString("dd/MM/yyyy"));
                row.AddCell(item.FchTope.ToString("dd/MM/yyyy"));
                //row.AddCell(item.Qty.ToString());
                //row.AddCell(item.Coste.ToString("N2"));
                //row.AddCell(item.Pvp.ToString("N2"));
                //row.AddCell(item.PvpFchFrom.ToString("dd/MM/yyyy"));
                row.AddCell(item.Obs);
            }

            return retval;
        }



        public MatHelper.Excel.Sheet ExcelSheet()
        {

            MatHelper.Excel.Row row;
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("PLANTILLA MODIFICACION", "PLANTILLA MODIFICACION");
            for (int i = (int)Cols.Dpto; i < (int)Cols.Obs + 1; i++)
            {
                switch ((Cols)i)
                {
                    case Cols.FchEntrega:
                    case Cols.FchTope:
                        retval.AddColumn("", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                        break;
                    default:
                        retval.AddColumn();
                        break;
                };
            }

            retval.AddRow();
            retval.AddRow();
            row = retval.AddRowWithCells("DPTO", "FAMILIA", "BARRA", "TALLA", "MATERIAL", "DESCRIPCION", "MARCA", "Nº PROVEEDOR", "Nº FABRICANTE", "SERIE", "MOD. FAB.", "COLOR", "TAMAHOR", "DIBUJO", "EAN (ASOCIAR)", "C2", "C3", "C5", "C6", "TG", "UE", "US", "ATUE", "FIS", "TIPO DE PEDIDO", "Nº PEDIDO", "LUGAR DE ENTREGA", "FECHA DE ENTREGA", "FECHA TOPE", "UNIDADES", "COSTE", "PVP", "FECHA ENTRADA", "EN VIGOR", "Porcentaje de fibra 1 (componente 1)", "Código de fibras para textiles 1 (componente 1)", "Porcentaje de fibra 1 (componente 2)", "Código de fibras para textiles 1 (componente 2)", "Porcentaje de fibra 1 (componente 3)", "Código de fibras para textiles 1 (componente 3)", "OBSERVACIONES");


            foreach (PlantillaModificacion.Item item in this.Items)
            {
                row = retval.AddRowWithEmptyCells(40);
                row.Cells[(int)Cols.Dpto].Content = item.Dpto;
                row.Cells[(int)Cols.Familia].Content = item.Familia;
                row.Cells[(int)Cols.Barra].Content = item.Barra;
                row.Cells[(int)Cols.Talla].Content = item.Talla;
                row.Cells[(int)Cols.Descripcion].Content = item.Descripcion;
                row.Cells[(int)Cols.Marca].Content = item.Marca;
                row.Cells[(int)Cols.NumProveidor].Content = item.NumProveidor;
                row.Cells[(int)Cols.RazonSocial].Content = item.RazonSocial;
                row.Cells[(int)Cols.ModFabr].Content = item.ModFabr;
                row.Cells[(int)Cols.Color].Content = item.Color;
                row.Cells[(int)Cols.EAN].Content = item.EAN;
                row.Cells[(int)Cols.TG].Content = item.TG;
                row.Cells[(int)Cols.UE].Content = item.UE;
                row.Cells[(int)Cols.Pedido].Content = item.Pedido;
                row.Cells[(int)Cols.LugarEntrega].Content = item.LugarEntrega;
                row.Cells[(int)Cols.FchEntrega].Content = (DateTime)item.FchEntrega;
                if ((DateTime)item.FchTope != DateTime.MinValue)
                {
                    row.Cells[(int)Cols.FchTope].Content = (DateTime)item.FchTope;
                }
                if (item.Qty != 0)
                {
                    row.Cells[(int)Cols.Qty].Content = item.Qty;
                }
                if (item.Coste != 0)
                {
                    row.Cells[(int)Cols.Coste].Content = item.Coste;
                }
                if (item.Pvp != 0)
                {
                    row.Cells[(int)Cols.Pvp].Content = item.Pvp;
                }
                if (item.PvpFchFrom != DateTime.MinValue)
                {
                    row.Cells[(int)Cols.PvpFchFrom].Content = item.PvpFchFrom;
                }
                row.Cells[(int)Cols.Obs].Content = item.Obs;
            }

            return retval;
        }


        public class Item
        {
            public string Dpto { get; set; }
            public string Familia { get; set; }
            public string Barra { get; set; }
            public string Talla { get; set; }
            public string Descripcion { get; set; }
            public string Marca { get; set; }
            public string NumProveidor { get; set; }
            public string RazonSocial { get; set; }
            public string ModFabr { get; set; }
            public string Color { get; set; }
            public string EAN { get; set; }
            public string TG { get; set; }
            public string UE { get; set; }
            public string Pedido { get; set; }
            public string LugarEntrega { get; set; }
            public DateTime FchEntrega { get; set; }
            public DateTime FchTope { get; set; }
            public int Qty { get; set; }
            public decimal Coste { get; set; }
            public decimal Pvp { get; set; }
            public DateTime PvpFchFrom { get; set; }
            public string Obs { get; set; }

            public static Item Factory(DTOPurchaseOrder order, DateTime fchEntrega)
            {
                Item retval = new Item();
                retval.Dpto = ElCorteIngles.Globals.Depto(order);
                retval.NumProveidor = ElCorteIngles.Globals.NumProveidor(order);
                retval.RazonSocial = "MATIAS MASSO, S.A.";
                retval.Pedido = ElCorteIngles.Globals.ComandaNum(order);
                retval.FchEntrega = fchEntrega;
                retval.FchTope = order.FchDeliveryMin;
                retval.Obs = "";
                return retval;
            }
            public static Item Factory(DTODeliveryItem deliveryItem, List<DTOCustomerProduct> customerProducts, DateTime fchEntrega)
            {
                DTOCustomerProduct customerProduct = customerProducts.FirstOrDefault(x => x.Sku.Guid.Equals(deliveryItem.Sku.Guid));
                Item retval = new Item();
                DTOPurchaseOrder order = deliveryItem.PurchaseOrderItem.PurchaseOrder;
                retval.Dpto = DTO.Integracions.ElCorteIngles.Globals.Depto(order);
                if (customerProduct != null)
                {
                    retval.Familia = ElCorteIngles.Globals.ProductFamilia(customerProduct);
                    retval.Barra = ElCorteIngles.Globals.ProductBarra(customerProduct);
                }
                retval.Descripcion = deliveryItem.Sku.RefYNomLlarg().Esp;
                retval.Marca = DTOProductSku.BrandNom(deliveryItem.Sku);
                retval.NumProveidor = ElCorteIngles.Globals.NumProveidor(order);
                retval.EAN = DTOProductSku.Ean(deliveryItem.Sku);
                retval.Pedido = ElCorteIngles.Globals.ComandaNum(order);
                retval.LugarEntrega = deliveryItem.Delivery.Platform.FullNom;
                retval.FchEntrega = fchEntrega;
                retval.FchTope = deliveryItem.PurchaseOrderItem.PurchaseOrder.FchDeliveryMin;
                retval.Qty = deliveryItem.Qty;
                retval.Coste = DTOAmt.EurOrDefault(deliveryItem.Price);
                retval.Pvp = DTOAmt.EurOrDefault(deliveryItem.Sku.Rrpp);
                retval.PvpFchFrom = DateTime.MinValue;
                retval.Obs = "";
                return retval;
            }

        }
    }

}
