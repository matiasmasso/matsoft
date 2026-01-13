using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOIntrastat : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public Flujos Flujo { get; set; }
        public int Yea { get; set; }
        public int Mes { get; set; }
        public int Ord { get; set; }
        public int PartidasCount { get; set; }
        public int Units { get; set; }
        public decimal Kg { get; set; }
        public DTOAmt Amt { get; set; }
        public string Csv { get; set; }
        public DTODocFile DocFile { get; set; }

        public List<DTOImportacioItem> Items { get; set; }
        public List<DTOIntrastat.Partida> Partidas { get; set; }

        public List<DTOProductSku> ExceptionSkus { get; set; }

        public enum Flujos
        {
            introduccion,
            expedicion
        }

        public new DTOIntrastat Trimmed()
        {
            DTOIntrastat retval = new DTOIntrastat(base.Guid);
            {
                var withBlock = retval;
                withBlock.Emp = new DTOEmp(Emp.Id);
                withBlock.Flujo = Flujo;
                withBlock.Yea = Yea;
                withBlock.Mes = Mes;
                withBlock.Ord = Ord;
                withBlock.PartidasCount = PartidasCount;
                withBlock.Units = Units;
                withBlock.Kg = Kg;
                withBlock.Amt = Amt;
                withBlock.Csv = Csv;
                withBlock.DocFile = DocFile;
                withBlock.Partidas = new List<DTOIntrastat.Partida>();
                foreach (DTOIntrastat.Partida partida in Partidas)
                    withBlock.Partidas.Add(partida.Trimmed());
            }
            return retval;
        }

        public DTOYearMonth YearMonth
        {
            get
            {
                return new DTOYearMonth(Yea, (DTOYearMonth.Months)Mes);
            }
            set
            {
                Yea = value.Year;
                Mes = (int)value.Month;
            }
        }

        public DTOIntrastat() : base()
        {
            Items = new List<DTOImportacioItem>();
        }

        public DTOIntrastat(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTOImportacioItem>();
        }

        public static string Caption(DTOIntrastat oIntrastat, DTOLang oLang)
        {
            string retval = "";
            if (oIntrastat != null)
                retval = oLang.MesAbr(oIntrastat.Mes) + "/" + oIntrastat.Yea;
            return retval;
        }

        public static string DefaultFileName(DTOIntrastat oIntrastat)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("M+O.Intrastat.");
            sb.Append(oIntrastat.Yea.ToString());
            sb.Append(".");
            sb.Append(String.Format("{0:00}", oIntrastat.Mes));
            sb.Append(".");
            switch (oIntrastat.Flujo)
            {
                case DTOIntrastat.Flujos.introduccion:
                    {
                        sb.Append("import");
                        break;
                    }

                case DTOIntrastat.Flujos.expedicion:
                    {
                        sb.Append("export");
                        break;
                    }
            }
            sb.Append(".");
            sb.Append(oIntrastat.Ord);
            sb.Append(".txt");
            return sb.ToString();
        }

        public static string FileStringBuilder(DTOIntrastat oIntrastat)
        {
            // se importará la información de las partidas desde un fichero de texto generado al efecto, 
            // conteniendo un registro por cada partida de la declaración. 
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var oPartida in oIntrastat.Partidas)
                sb.AppendLine(DTOIntrastat.Partida.Row(oPartida, oIntrastat.Flujo));
            return sb.ToString();
        }

        public static List<DTOProductSku> SkusDeclarables(DTOIntrastat oIntrastat)
        {
            List<DTOProductSku> oAllSkus = null;
            switch (oIntrastat.Flujo)
            {
                case DTOIntrastat.Flujos.introduccion:
                    {
                        var oDeliveries = oIntrastat.Partidas.Select(x => (DTODelivery)x.Tag).Distinct().ToList();
                        oAllSkus = oDeliveries.SelectMany(y => y.Items).Select(z => z.Sku).ToList();
                        break;
                    }

                case DTOIntrastat.Flujos.expedicion:
                    {
                        var oInvoices = oIntrastat.Partidas.Select(x => (DTOInvoice)x.Tag).Distinct().ToList();
                        oAllSkus = oInvoices.SelectMany(x => x.Deliveries).SelectMany(y => y.Items).Select(z => z.Sku).ToList();
                        break;
                    }
            }
            var retval = oAllSkus.Where(p => DTOIntrastat.SkuIsDeclarable(p)).ToList();
            return retval;
        }

        public static bool SkuIsDeclarable(DTOProductSku oSku)
        {
            bool retval = false;
            switch (oSku.Category.Codi)
            {
                case DTOProductCategory.Codis.standard:
                case DTOProductCategory.Codis.accessories:
                    {
                        retval = true;
                        if (oSku.IsBundle)
                            retval = false;
                        break;
                    }
            }
            if (oSku.NoStk)
                retval = false;
            return retval;
        }

        public static decimal SkuKg(DTOProductSku oSku)
        {
            return DTOProductSku.kgNetOrInheritedOrBrut(oSku);
        }

        public static MatHelper.Excel.Book ExcelExport(DTOIntrastat oIntrastat)
        {
            string sFilename = string.Format("Intrastat {0:0000}{1:00}", oIntrastat.Yea, oIntrastat.Mes);
            MatHelper.Excel.Book retval = new MatHelper.Excel.Book(sFilename);
            {
                var withBlock = retval.Sheets;
                withBlock.Add(ExcelPartides(oIntrastat));
                withBlock.Add(ExcelSkus(oIntrastat));
            }
            return retval;
        }

        public static MatHelper.Excel.Sheet ExcelPartides(DTOIntrastat oIntrastat)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Partides");
            {
                var withBlock = retval;
                withBlock.AddColumn("data", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("factura", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("client", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("pais", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("incoterm", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("codi", MatHelper.Excel.Cell.NumberFormats.W50);
                if (oIntrastat.Flujo == DTOIntrastat.Flujos.introduccion)
                    withBlock.AddColumn("made in", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("pes net", MatHelper.Excel.Cell.NumberFormats.Kg);
                withBlock.AddColumn("import", MatHelper.Excel.Cell.NumberFormats.Euro);
            }

            switch (oIntrastat.Flujo)
            {
                case DTOIntrastat.Flujos.expedicion:
                    {
                        foreach (var item in oIntrastat.Partidas)
                        {
                            DTOInvoice oInvoice = (DTOInvoice)item.Tag;
                            MatHelper.Excel.Row oRow = retval.AddRow();
                            {
                                var withBlock = oRow;
                                withBlock.AddCell(oInvoice.Fch);
                                withBlock.AddCell(oInvoice.Num);
                                withBlock.AddCell(oInvoice.Customer.Nom);
                                withBlock.AddCell(DTOAddress.Country(oInvoice.Customer.Address).ISO);
                                withBlock.AddCell(item.Incoterm.Id.ToString());
                                withBlock.AddCell(item.CodiMercancia.Id);
                                // .AddCell(item.MadeIn.ISO)
                                withBlock.AddCell(item.Kg);
                                withBlock.AddCell(item.ImporteFacturado);
                            }
                        }

                        break;
                    }

                case DTOIntrastat.Flujos.introduccion:
                    {
                        foreach (var item in oIntrastat.Partidas)
                        {
                            DTODelivery oDelivery = (DTODelivery)item.Tag;
                            MatHelper.Excel.Row oRow = retval.AddRow();
                            {
                                var withBlock = oRow;
                                withBlock.AddCell(oDelivery.Fch);
                                withBlock.AddCell(oDelivery.Id);
                                withBlock.AddCell(oDelivery.Contact.Nom);
                                withBlock.AddCell(DTOAddress.Country(oDelivery.Address).ISO);
                                withBlock.AddCell(item.Incoterm.Id.ToString());
                                withBlock.AddCell(item.CodiMercancia.Id);
                                withBlock.AddCell(item.MadeIn.ISO);
                                withBlock.AddCell(item.Kg);
                                withBlock.AddCell(item.ImporteFacturado);
                            }
                        }

                        break;
                    }
            }

            return retval;
        }

        public static MatHelper.Excel.Sheet ExcelSkus(DTOIntrastat oIntrastat)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Productes");
            {
                var withBlock = retval;
                withBlock.AddColumn("marca comercial", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("categoria", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("producte", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("codi", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("made in", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("pes net", MatHelper.Excel.Cell.NumberFormats.Kg);
            }

            var oSkus = DTOIntrastat.SkusDeclarables(oIntrastat);

            foreach (var item in oSkus)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                {
                    var withBlock = oRow;
                    withBlock.AddCell(item.Category.Brand.Nom.Esp);
                    withBlock.AddCell(item.Category.Nom.Esp);
                    withBlock.AddCell(item.Nom.Esp);
                    withBlock.AddCell(item.CodiMercancia.Id);
                    withBlock.AddCell(item.MadeIn.ISO);
                    withBlock.AddCell(DTOProductSku.kgNetOrInheritedOrBrut(item));
                }
            }
            return retval;
        }


        public class Partida
        {
            public int Lin { get; set; }
            public DTOBaseGuid Tag { get; set; }
            public DTOCountry Country { get; set; }
            public DTOAreaProvincia Provincia { get; set; }
            public DTOIncoterm Incoterm { get; set; }
            public NaturalezasTransaccion NaturalezaTransaccion { get; set; } = NaturalezasTransaccion.compraEnFirme;
            public CodisTransport CodiTransport { get; set; } = CodisTransport.carretera;
            public string Port { get; set; } = "";
            public DTOCodiMercancia CodiMercancia { get; set; }
            public DTOCountry MadeIn { get; set; }
            public RegimenesEstadisticos RegimenEstadistico { get; set; } = RegimenesEstadisticos.destinoFinalEuropa;
            public decimal UnidadesSuplementarias { get; set; } = 0;
            public decimal Kg { get; set; }
            public decimal ImporteFacturado { get; set; }
            public decimal ValorEstadistico { get; set; }
            public string NifContraparte { get; set; }
            public List<DTOIntrastat.Exception> Exceptions { get; set; }


            public enum NaturalezasTransaccion
            {
                notSet,
                compraEnFirme = 11
            }

            public enum CodisTransport
            {
                notSet,
                maritimo,
                ferrocarril,
                carretera,
                aereo,
                envioPostal
            }

            public enum RegimenesEstadisticos
            {
                notSet,
                destinoFinalEuropa
            }

            public static List<DTOIntrastat.Partida> Factory(DTOEmp oEmp, DTODelivery oDelivery)
            {
                var oProveidor = oDelivery.Proveidor;
                var oCountry = DTOAddress.Country(oDelivery.Address);
                var oProvincia = DTOAddress.Provincia(oEmp.Org.Address);
                var oIncoterm = oDelivery.Incoterm;

                var items = oDelivery.Items.Where(y => DTOIntrastat.SkuIsDeclarable(y.Sku)).ToList();

                DTOCodiMercancia oNoCodiMercancia = new DTOCodiMercancia("");
                DTOCountry oNoMadein = new DTOCountry(Guid.Empty, "");
                foreach (var item in items)
                {
                    // If item.bundle.Count > 0 Then Stop
                    if (item.Sku.CodiMercancia == null)
                        item.Sku.CodiMercancia = oNoCodiMercancia;
                    if (item.Sku.MadeIn == null)
                        item.Sku.MadeIn = oNoMadein;
                }

                var retval = items.Where(z => z.Sku.NoStk == false & z.Price != null && z.Price.Eur > 0).GroupBy(g => new { g.Sku.CodiMercancia.Id, g.Sku.MadeIn.Guid, g.Sku.MadeIn.ISO }).Select(group => new DTOIntrastat.Partida()
                {
                    Country = oCountry,
                    Provincia = oProvincia,
                    Incoterm = oIncoterm,
                    CodiMercancia = new DTOCodiMercancia(group.Key.Id),
                    MadeIn = new DTOCountry(group.Key.Guid, group.Key.ISO),
                    Kg = group.Sum(x => x.Qty * DTOIntrastat.SkuKg(x.Sku)),
                    UnidadesSuplementarias = group.Sum(x => x.Qty),
                    ImporteFacturado = group.Sum(x => DTOAmt.import(x.Qty, x.Price, x.Dto).Eur),
                    ValorEstadistico = group.Sum(x => DTOAmt.import(x.Qty, x.Price, x.Dto).Eur),
                    Tag = oDelivery
                }).ToList();

                return retval;
            }


            public static List<DTOIntrastat.Partida> Factory(DTOEmp oEmp, DTOInvoice oInvoice)
            {
                var oCustomer = oInvoice.Customer;
                var oCountry = DTOAddress.Country(oInvoice.Deliveries.First().Address);
                var oIncoterm = oInvoice.Incoterm;
                var oProvincia = DTOAddress.Provincia(oEmp.Org.Address);
                //var oIncoterm =  oCustomer.Incoterm == null ? DTOIncoterm.Factory(DTOIncoterm.Ids.CIF) : oCustomer.Incoterm;


                var items = oInvoice.Deliveries.SelectMany(x => x.Items).Where(y => DTOIntrastat.SkuIsDeclarable(y.Sku)).ToList();

                DTOCodiMercancia oNoCodiMercancia = new DTOCodiMercancia("");
                //DTOCountry oNoMadein = new DTOCountry(Guid.Empty, "");
                foreach (var item in items)
                {
                    if (item.Sku.CodiMercancia == null)
                        item.Sku.CodiMercancia = oNoCodiMercancia;
                    // If item.Sku.MadeIn Is Nothing Then item.Sku.MadeIn = oNoMadein
                    //item.Sku.MadeIn = oNoMadein; // Para flujo expedición no cumplimentar país de origen. Instruccio retrocedida per mantenir la integritat referencial
                }

                var retval = items.Where(z => z.Sku.NoStk == false & z.Price != null && z.Price.Eur > 0).GroupBy(g => new { g.Sku.CodiMercancia.Id, g.Sku.MadeIn.Guid, g.Sku.MadeIn.ISO }).Select(group => new DTOIntrastat.Partida()
                {
                    Country = oCountry,
                    Provincia = oProvincia,
                    Incoterm = oIncoterm,
                    CodiMercancia = new DTOCodiMercancia(group.Key.Id),
                    MadeIn = new DTOCountry(group.Key.Guid, group.Key.ISO),
                    Kg = group.Sum(x => x.Qty * DTOIntrastat.SkuKg(x.Sku)),
                    UnidadesSuplementarias = group.Sum(x => x.Qty),
                    ImporteFacturado = group.Sum(x => DTOAmt.import(x.Qty, x.Price, x.Dto).Eur),
                    ValorEstadistico = group.Sum(x => DTOAmt.import(x.Qty, x.Price, x.Dto).Eur),
                    Tag = oInvoice,
                    NifContraparte = oCustomer.PrimaryNifValue()
                }).ToList();

                return retval;
            }

            public static bool Warn(DTOIntrastat.Flujos Flujo, decimal Kg, DTOCodiMercancia CodiMercancia, DTOCountry MadeIn)
            {
                bool retval = false;
                if (Kg == 0 | CodiMercancia == null)
                    retval = true;
                //if (Flujo == DTOIntrastat.Flujos.introduccion & MadeIn == null)
                //retval = true;
                if (MadeIn == null)
                    retval = true;
                return retval;
            }

            public DTOIntrastat.Partida Trimmed()
            {
                DTOIntrastat.Partida retval = new DTOIntrastat.Partida();
                {
                    var withBlock = retval;
                    withBlock.Tag = new DTOBaseGuid(Tag.Guid);
                    if (Country != null)
                        withBlock.Country = new DTOCountry(Country.Guid);
                    if (Provincia != null)
                        withBlock.Provincia = new DTOAreaProvincia(Provincia.Guid);
                    withBlock.Incoterm = Incoterm;
                    withBlock.NaturalezaTransaccion = NaturalezaTransaccion;
                    withBlock.CodiTransport = CodiTransport;
                    if (CodiMercancia != null)
                        withBlock.CodiMercancia = CodiMercancia;
                    if (MadeIn != null)
                        withBlock.MadeIn = new DTOCountry(MadeIn.Guid);
                    withBlock.RegimenEstadistico = RegimenEstadistico;
                    withBlock.UnidadesSuplementarias = UnidadesSuplementarias;
                    withBlock.Kg = Kg;
                    withBlock.ImporteFacturado = ImporteFacturado;
                    withBlock.ValorEstadistico = ValorEstadistico;
                }
                return retval;
            }

            public static string Row(DTOIntrastat.Partida oPartida, DTOIntrastat.Flujos oFlujo)
            {

                // Cada registro contendrá los siguientes campos, y en el siguiente orden, separados por el carácter ‘;’ (punto y coma):

                // 1. E M Procedencia/Destino Código ISO del Estado Miembro de Procedencia/Destino de la mercancía, en formato alfanumérico de dos posiciones. 
                // 2. Provincia de Origen/Destino Código de la Provincia española de Origen/Destino de la mercancía, en formato numérico de dos posiciones. 
                // 3. Cond. Entrega Código de las Condiciones de Entrega, en formato alfanumérico de tres posiciones. 
                // 4. Nat. Transacción Código de la Naturaleza de la Transacción, en formato numérico de dos posiciones. 
                // 5. Modalidad de Transporte Código del Modo de Transporte, en formato numérico de una posición. 
                // 6. Puerto/Aeropuerto  Carg/Desca Código del Puerto/Aeropuerto español de Carga/Descarga de la mercancía, en formato numérico de cuatro posiciones. 
                // 7. Código Mercancia Código de Nomenclatura Combinada correspondiente a la mercancía, en formato numérico de ocho posiciones. 
                // 8. País Origen Código ISO del país de origen, en formato alfanumérico de dos posiciones. 
                // 9. Régimen Estadístico Código del Régimen Estadístico, en formato numérico de una posición. 
                // 10. Masa Neta Masa Neta de la mercancía, expresado en kilogramos. Formato numérico, máximo doce enteros y tres decimales separados por coma. 
                // 11. Unidades Suplementarias Cantidad de Unidades Suplementarias. Formato numérico, máximo doce enteros y tres decimales separados por coma. 
                // 12. Importe Factura Importe de Factura de la mercancía, expresado en euros con dos decimales. Formato numérico, máximo trece enteros y dos decimales separados por coma. 
                // 13. Valor Estadístico Valor Estadístico de la mercancía, expresado en euros con dos decimales. Formato numérico, máximo trece enteros y dos decimales separados por coma. 

                // No todos los campos son obligatorios,  pero incluso cuando no tenga valor un campo concreto, se pondrá el carácter separador (;). Veamos algunos ejemplos:

                // FR;31;FOB;11;3;;85182190;CN;1;115;162;15,37;15,37
                // DE;28;CIF;11;1;0811;85182190;US;1;2459;1982;4589,46;4589,46
                // IT;12;FOB;11;3;;02012030;;1;800;;987,00;890,45


                string s = "";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // 1. Codigo Iso del Estado Miembro de Procedencia
                sb.Append(oPartida.Country.ISO);
                sb.Append(";");

                // 2. Provincia de destino
                sb.Append(oPartida.Provincia.Intrastat);
                sb.Append(";");

                // 3. Condiciones de entrega
                if (oPartida.Incoterm != null)
                    sb.Append(oPartida.Incoterm.Id.ToString());
                sb.Append(";");

                // 4. Naturaleza de la transaccion (cod. 2 digitos)
                // 11 compraventa en firme
                sb.Append((int)oPartida.NaturalezaTransaccion);
                sb.Append(";");

                // 5. Modo de transporte (3->carretera)
                sb.Append((int)oPartida.CodiTransport);
                sb.Append(";");

                // 6. Puerto/aeropuerto de descarga
                sb.Append(";");

                // 7. Código Mercancia (8 digitos)
                sb.Append(oPartida.CodiMercancia.Id);
                sb.Append(";");

                // 8. País Origen Código ISO del país de origen, en formato alfanumérico de dos posiciones. 
                // sb.Append("") 'oPartida.MadeIn.ISO no cal informar a exportacions
                // 08/04/2021 la Monica de Bufete Escura diu que no cal informar tampoc a les importacions
                // 08/02/2022 a partir de Gener 2022 cal informar-lo nomes a les exportacions
                if (oFlujo == Flujos.expedicion)
                    sb.Append(oPartida.MadeIn.ISO); // no cal informar a exportacions
                sb.Append(";");

                // 9. Régimen Estadístico Código del Régimen Estadístico, en formato numérico de una posición. 
                sb.Append("1"); // 1-> destino final España
                sb.Append(";");

                // 10. Masa Neta Masa Neta de la mercancía, expresado en kilogramos. Formato numérico, máximo doce enteros y tres decimales separados por coma. 
                sb.Append(VbUtilities.Format(oPartida.Kg, "#0.00"));
                sb.Append(";");

                // 11. Unidades Suplementarias Cantidad de Unidades Suplementarias. Formato numérico, máximo doce enteros y tres decimales separados por coma. 
                sb.Append(VbUtilities.Format(oPartida.UnidadesSuplementarias, "0"));
                sb.Append(";");

                // 12. Importe Factura Importe de Factura de la mercancía, expresado en euros con dos decimales. Formato numérico, máximo trece enteros y dos decimales separados por coma. 
                sb.Append(VbUtilities.Format(oPartida.ImporteFacturado, "#0.00"));
                sb.Append(";");

                // 13. Valor Estadístico Valor Estadístico de la mercancía, expresado en euros con dos decimales. Formato numérico, máximo trece enteros y dos decimales separados por coma. 
                sb.Append(VbUtilities.Format(oPartida.ValorEstadistico, "#0.00"));

                // 14. NIF IVA de la contraparte(A2X(2 a 14)) (nomes exportacions)  
                if (oFlujo == Flujos.expedicion)
                    sb.Append(string.Format(";{0}", oPartida.NifContraparte));

                s = sb.ToString();

                return s;
            }
        }

        public class Exception : System.Exception
        {
            private DTOProductSku _Sku;
            private Cods _Cod;

            public Exception(DTOProductSku oSku, Cods oCod) : base()
            {
                _Sku = oSku;
                _Cod = oCod;
            }
            public enum Cods
            {
                None,
                MissingMadeIn,
                MissimgCodiMercancia,
                MissingKg
            }
        }
    }
}
