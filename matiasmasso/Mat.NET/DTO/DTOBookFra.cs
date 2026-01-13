using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOBookFra
    {
        public DTOCca Cca { get; set; }
        public DTOPgcCta Cta { get; set; }
        public DTOContact Contact { get; set; }
        public string FraNum { get; set; }
        public DTOSiiLog SiiLog { get; set; }
        // Property SiiErrCod As Integer
        public int SiiEstadoCuadre { get; set; }
        public DateTime SiiTimestampEstadoCuadre { get; set; }
        public DateTime SiiTimestampUltimaModificacion { get; set; }

        public List<DTOBaseQuota> IvaBaseQuotas { get; set; }
        public DTOBaseQuota IrpfBaseQuota { get; set; }
        public string ClaveExenta { get; set; } = "";
        public string TipoFra { get; set; }
        public string ClaveRegimenEspecialOTrascendencia { get; set; }
        public string Dsc { get; set; }
        public DTOInvoice.ExportCods Import { get; set; }

        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        public enum Modes
        {
            all,
            onlyIva,
            onlyIRPF
        }

        public DTOBookFra(DTOCca oCca) : base()
        {
            Cca = oCca;
            IvaBaseQuotas = new List<DTOBaseQuota>();
        }

        public static DTOBookFra Factory(DTOCca oCca, DTOProveidor oProveidor)
        {
            DTOBookFra retval = new DTOBookFra(oCca);
            {
                var withBlock = retval;
                withBlock.IsNew = true;
                withBlock.Contact = oProveidor;
                withBlock.Import = DTOBookFra.importCod(retval);
            }
            return retval;
        }



        public DTOAmt baseDevengada()
        {
            DTOAmt retval = DTOAmt.Factory();
            foreach (DTOBaseQuota item in IvaBaseQuotas)
                retval.Add(item.baseImponible);
            return retval;
        }

        public DTOAmt total()
        {
            DTOAmt retval = DTOAmt.Factory();
            foreach (DTOBaseQuota item in IvaBaseQuotas)
            {
                retval.Add(item.baseImponible);
                retval.Add(item.quota);
            }
            if (IrpfBaseQuota != null)
                retval.Substract(IrpfBaseQuota.quota);
            return retval;
        }
        public DTOAmt TotalSinIrpf()
        {
            DTOAmt retval = DTOAmt.Factory();
            foreach (DTOBaseQuota item in IvaBaseQuotas)
            {
                retval.Add(item.baseImponible);
                retval.Add(item.quota);
            }
            return retval;
        }

        public DTOBaseQuota baseQuotaIvaSoportat()
        {
            DTOBaseQuota retval = IvaBaseQuotas.FirstOrDefault(x => x.tipus != 0);
            return retval;
        }

        public DTOBaseQuota baseQuotaIvaExenta()
        {
            DTOBaseQuota retval = IvaBaseQuotas.FirstOrDefault(x => x.tipus == 0);
            return retval;
        }

        public DTOAmt quotaDeducible(List<DTOTax> oTaxes)
        {
            var retval = DTOAmt.Factory();
            if (ClaveRegimenEspecialOTrascendencia == "09")
            {
                var oTaxIva = oTaxes.FirstOrDefault(x => x.codi == DTOTax.Codis.iva_Standard);
                if (oTaxIva != null)
                {
                    decimal dcTaxIvaTipus = oTaxIva.tipus;
                    retval = baseDevengada().Percent(dcTaxIvaTipus);
                }
            }
            else
                foreach (DTOBaseQuota item in IvaBaseQuotas)
                    retval.Add(item.quota);
            return retval;
        }

        public DTOYearMonth yearMonth()
        {
            DTOYearMonth retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (Cca != null)
                retval = new DTOYearMonth(Cca.Fch.Year, (DTOYearMonth.Months)Cca.Fch.Month);
            return retval;
        }

        public static DTOInvoice.ExportCods importCod(DTOBookFra oBookFra)
        {
            DTOInvoice.ExportCods retval = DTOContact.ExportCod(oBookFra.Contact);
            return retval;
        }

        public static List<KeyValuePair<string, string>> causasExencion()
        {
            List<KeyValuePair<string, string>> retval = new List<KeyValuePair<string, string>>();
            retval.Add(new KeyValuePair<string, string>("", "(sel·leccionar causa exempció)"));
            retval.Add(new KeyValuePair<string, string>("E1", "E1 (Art.20)"));
            retval.Add(new KeyValuePair<string, string>("E2", "E2 (Art.21) Extracomunitari"));
            retval.Add(new KeyValuePair<string, string>("E3", "E3 (Art.22)"));
            retval.Add(new KeyValuePair<string, string>("E4", "E4 (Art.24)"));
            retval.Add(new KeyValuePair<string, string>("E5", "E5 (Art.25) Intracomunitari"));
            retval.Add(new KeyValuePair<string, string>("E6", "E6-Altres"));
            return retval;
        }


        public static List<KeyValuePair<string, string>> TiposFra()
        {
            List<KeyValuePair<string, string>> retval = new List<KeyValuePair<string, string>>();
            retval.Add(new KeyValuePair<string, string>("", "(sel·leccionar tipus factura)"));
            retval.Add(new KeyValuePair<string, string>("F1", "F1 Factura"));
            retval.Add(new KeyValuePair<string, string>("F2", "F2 Factura Simplificada (ticket)"));
            retval.Add(new KeyValuePair<string, string>("F3", "F3 Factura emitida en sustitución de facturas simplificadas facturadas y declaradas"));
            retval.Add(new KeyValuePair<string, string>("F4", "F4 Asiento resumen de facturas"));
            retval.Add(new KeyValuePair<string, string>("F5", "F5 Importaciones (DUA)"));
            retval.Add(new KeyValuePair<string, string>("F6", "F6 Justificantes contables"));
            retval.Add(new KeyValuePair<string, string>("R1", "R1 Factura Rectificativa (art. 80 tres LIVA)"));
            retval.Add(new KeyValuePair<string, string>("R2", "R2 Factura Rectificativa (art. 80 cuatro LIVA - concurso)"));
            retval.Add(new KeyValuePair<string, string>("R3", "R3 Factura Rectificativa (Resto art. 80 uno y dos) – deuda incobrable"));
            retval.Add(new KeyValuePair<string, string>("R4", "R4 Factura Rectificativa (Resto)"));
            retval.Add(new KeyValuePair<string, string>("R5", "R5 Factura Rectificativa en facturas simplificadas"));
            return retval;
        }

        public static List<KeyValuePair<string, string>> regEspOTrascs()
        {
            List<KeyValuePair<string, string>> retval = new List<KeyValuePair<string, string>>();
            retval.Add(new KeyValuePair<string, string>("", "(sel·leccionar tipus Reg.Esp.O Trascendencia)"));
            retval.Add(new KeyValuePair<string, string>("01", "01 Operación de Régimen General"));
            retval.Add(new KeyValuePair<string, string>("02", "02 Exportación"));
            retval.Add(new KeyValuePair<string, string>("03", "03 Bienes usados, arte, antigüedades, colección"));
            retval.Add(new KeyValuePair<string, string>("04", "04 Oro de inversión"));
            retval.Add(new KeyValuePair<string, string>("05", "05 Régimen especial de Agencias de Viajes"));
            retval.Add(new KeyValuePair<string, string>("08", "08 Operaciones sujetas al IPSI / IGIC"));
            retval.Add(new KeyValuePair<string, string>("09", "09 Adquisiciones intracomunitarias de bienes y prestaciones de servicios"));
            retval.Add(new KeyValuePair<string, string>("12", "12 Operaciones de arrendamiento de local de negocio"));
            retval.Add(new KeyValuePair<string, string>("13", "13 Factura correspondiente a una importación (informada sin asociar a un DUA)"));
            retval.Add(new KeyValuePair<string, string>("16", "16 Primer semestre de 2017"));
            return retval;
        }

        public static string Caption(DTOBookFra oBookFra)
        {
            string retval = "Fra." + oBookFra.FraNum + " del " + VbUtilities.Format(oBookFra.Cca.Fch, "dd/MM/yy");
            return retval;
        }


        public static string Filename(DTOExercici oExercici, int iMes = 0, DateTime ToFch = default(DateTime))
        {
            string retval = "";
            if (ToFch == default(DateTime) & iMes == 0)
                retval = string.Format("{0}.{1} Llibre de Factures rebudes.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year);
            else if (iMes > 0)
                retval = string.Format("{0}.{1}.{2} Llibre de Factures rebudes.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year, iMes);
            else if (ToFch.Day == 31 & ToFch.Month == 3)
                retval = string.Format("{0}.{1}.Q1 Llibre de Factures rebudes.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year);
            else
                retval = string.Format("{0}.{1:yyyyMMdd} Llibre de Factures rebudes.xlsx", oExercici.Emp.Org.PrimaryNifValue(), ToFch);

            return retval;
        }
    }
}
