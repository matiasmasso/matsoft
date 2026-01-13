using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;

namespace DTO.Integracions.ElCorteIngles
{
    public class Cataleg
    {
        public Guid Guid { get; set; }
        public DTOProductSku Sku { get; set; }
        public string Ref { get; set; }
        public DTO.Models.Base.GuidNom Dept { get; set; }
        public DateTime? FchDescatalogado { get; set; }

        public DTOCustomerProduct CustomerProduct()
        {
            DTOCustomerProduct retval = new DTOCustomerProduct(Guid);
            retval.Sku = Sku;
            retval.Ref = Ref;
            if(Dept != null)
            {
                retval.EciDept = new DTO.Integracions.ElCorteIngles.Dept(Dept.Guid);
                retval.EciDept.Nom = Dept.Nom;
            }
            return retval;
        }

        public bool Matches(string searchterm)
        {
            bool retval = Ref.Contains(searchterm);
            if (!retval & Sku != null)
                retval = Sku.Matches(searchterm);
            return retval;
        }

        public override string ToString() => string.Format("{0} {1}", Ref, Sku?.NomLlarg?.Esp ?? "");

        public static MatHelper.Excel.Sheet ExcelDescatalogats(List<Cataleg> items)
        {
            var retval = new MatHelper.Excel.Sheet("Descatalogados ECI");
            retval.AddRow();
            
            var colHeaders = retval.AddRowWithCells("DPTO", "FAMILIA", "BARRA", "TALLA", "MATERIAL", "DESCRIPCION", "MARCA", "Nº PROVEEDOR", "Nº FABRICANTE", "SERIE", "MOD.FAB.","COLOR", "TAMAHOR", "DIBUJO", "EAN(ASOCIAR)", "C2", "C3", "C5", "C6", "TG", "UE", "US", "ATUE", "FIS", "TIPO DE PEDIDO", "Nº PEDIDO", "LUGAR DE ENTREGA", "FECHA DE ENTREGA", "FECHA TOPE", "UNIDADES", "COSTE", "PVP", "FECHA ENTRADA EN VIGOR", "Porcentaje de fibra 1(componente 1)", "Código de fibras para textiles 1(componente 1)", "Porcentaje de fibra 1(componente 2)", "Código de fibras para textiles 1(componente 2)", "Porcentaje de fibra 1(componente 3)", "Código de fibras para textiles 1(componente 3)", "OBSERVACIONES");
            colHeaders.Forecolor = "#FFFFFF";
            colHeaders.Backcolor= "#00B050";
            foreach (var item in items)
            {
                var row = retval.AddRow();
                row.AddCell(item.Dept?.Nom);
                row.AddCell(item.Ref?.Length > 2 ? item.Ref.Substring(0, 3) : "");
                row.AddCell(item.Ref?.Length > 3 ? item.Ref.Substring(3) : "");
                row.AddCell();
                row.AddCell();
                row.AddCell(item.Sku.RefYNomLlarg().Esp);
                row.AddCell(item.Sku.Category?.Brand?.Nom?.Esp);
                row.AddCell("1030825");
                row.AddCell();
                row.AddCell();
                row.AddCell();
                row.AddCell();
                row.AddCell();
                row.AddCell();
                row.AddCell( item.Sku.Ean13?.Value);
                for (int i = 0; i < 24; i++)
                {
                    row.AddCell();
                }
                row.AddCell("Descatalogada");
            }
            return retval;
        }
    }

}
