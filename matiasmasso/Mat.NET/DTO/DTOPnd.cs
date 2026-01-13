using iText.Kernel.Pdf.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOPnd : DTOBaseGuid
    {

        // Partides pendents de comptabilitat

        public DTOEmp Emp { get; set; }
        public int Id { get; set; }
        public DTOContact Contact { get; set; }
        public FormasDePagament Cfp { get; set; }
        public string Fpg { get; set; }
        public DTOAmt Amt { get; set; }
        public DateTime Vto { get; set; }
        public Codis Cod { get; set; } = Codis.NotSet;
        public int Yef { get; set; }
        public string FraNum { get; set; } = "";
        public DTOInvoice Invoice { get; set; }
        public DateTime Fch { get; set; }
        public DTOPgcCta Cta { get; set; }
        public DTOCca Cca { get; set; }
        public DTOCca CcaVto { get; set; }
        public DTOCsb Csb { get; set; }
        public StatusCod Status { get; set; } = StatusCod.notSet;

        public enum Codis
        {
            NotSet,
            Deutor,
            Creditor
        }

        public enum StatusCod
        {
            notSet = -1,
            pendent = 0,
            enCartera = 1,
            enCirculacio = 2,
            saldat = 10,
            compensat = 11
        }

        public enum FormasDePagament
        {
            notSet = 0,
            rebut = 1,
            reposicioFons = 2,
            comptat = 3,
            xerocopia = 4,
            domiciliacioBancaria = 5,
            transferencia = 6,
            aNegociar = 9,
            efteAndorra = 10,
            transfPrevia = 11,
            diposit = 12
        }

        public DTOPnd() : base()
        {
        }

        public DTOPnd(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOPnd Factory(DTOEmp oEmp)
        {
            DTOPnd retval = new DTOPnd();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
            }
            return retval;
        }

        public static DTOPnd Factory(DTOInvoice oInvoice, DTOPgcCta oCta)
        {
            DTOPnd retval = null;
            if (oInvoice != null)
            {
                retval = DTOPnd.Factory(oInvoice.Emp);
                {
                    var withBlock = retval;
                    withBlock.Fch = oInvoice.Fch;
                    withBlock.Cod = DTOPnd.Codis.Deutor;
                    withBlock.Cfp = (DTOPnd.FormasDePagament)oInvoice.Cfp;
                    withBlock.Amt = oInvoice.Total;
                    withBlock.Contact =(oInvoice.Deutor == null || oInvoice.Deutor == Guid.Empty) ? oInvoice.Customer : new DTOContact(oInvoice.Deutor);
                    withBlock.Yef = oInvoice.Fch.Year;
                    withBlock.FraNum = oInvoice.Num.ToString();
                    withBlock.Invoice = oInvoice;
                    withBlock.Vto = oInvoice.Vto;
                    withBlock.Fpg = oInvoice.Fpg;
                    withBlock.Cca = oInvoice.Cca;
                    withBlock.Cta = oCta;
                    withBlock.Status = DTOPnd.StatusCod.pendent;
                }
            }
            return retval;
        }

        public DTOPnd clon()
        {
            DTOPnd retval = this;
            base.renewGuid();
            return retval;
        }

        public static string Concepte(List<DTOPnd> oPnds)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oPnds != null)
            {
                if (oPnds.Count > 0)
                {
                    foreach (DTOPnd oPnd in oPnds)
                    {
                        if (oPnd.FraNum.isNotEmpty())
                        {
                            if (oPnd.UnEquals(oPnds.First()))
                                sb.Append(",");
                            sb.Append(oPnd.FraNum);
                        }
                    }
                    DTOLang oLang = oPnds.First().Contact.Lang;
                    if (oLang == null)
                        oLang = DTOLang.ESP();
                    switch (oPnds.Count)
                    {
                        case 1:
                            {
                                sb.Insert(0, oLang.Tradueix("Factura ", "Factura ", "Invoice "));
                                break;
                            }

                        default:
                            {
                                sb.Insert(0, oLang.Tradueix("Facturas ", "Facturas ", "Invoices "));
                                break;
                            }
                    }
                }
            }
            string retval = sb.ToString();
            return retval;
        }

        public static string FacturaText(DTOPnd oPnd, DTOLang oLang)
        {
            string retval = string.Format("{0} {1}", oLang.Tradueix("Factura", "Factura", "Invoice"), oPnd.FraNum);
            return retval;
        }

        public static string GetFpg(DTOPnd.FormasDePagament oCfp)
        {
            string retval = "";
            switch (oCfp)
            {
                case DTOPnd.FormasDePagament.notSet:
                    {
                        retval = "(sense forma de pago)";
                        break;
                    }

                default:
                    {
                        retval = oCfp.ToString();
                        break;
                    }
            }
            return retval;
        }

        public static double DueDays(DTOPnd oPnd)
        {
            TimeSpan oSpan = DTO.GlobalVariables.Today() - oPnd.Vto;
            double retval = oSpan.TotalDays;
            return retval;
        }

        public static decimal EurDeutor(DTOPnd oPnd)
        {
            decimal retval = 0;
            if (oPnd.Amt != null)
                retval = oPnd.Cod == DTOPnd.Codis.Deutor ? oPnd.Amt.Eur : -oPnd.Amt.Eur;
            return retval;
        }

        public static DTOAmt Sum(List<DTOPnd> oPnds, DTOPnd.Codis oCod)
        {
            var retval = DTOAmt.Empty();
            foreach (DTOPnd item in oPnds)
            {
                if (oCod == item.Cod)
                    retval.Add(item.Amt);
                else
                    retval.Substract(item.Amt);
            }
            return retval;
        }

        public static MatHelper.Excel.Sheet Excel(List<DTOPnd> oPnds, string sTitle, DTOPnd.Codis oCod, DTOLang oLang)
        {
            DTOCur FirstCur = oPnds.First().Amt.Cur;
            bool ShowCurAmt = oPnds.Any(x => x.Amt.Cur.UnEquals(FirstCur));
            bool ShowCurTag = ShowCurAmt | FirstCur.UnEquals(DTOCur.Eur());

            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(sTitle);
            {
                var withBlock = retval;
                withBlock.AddColumn("Venciment", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("Deutor/Creditor", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Eur", MatHelper.Excel.Cell.NumberFormats.Euro);
                if (ShowCurTag)
                    withBlock.AddColumn("Divisa", MatHelper.Excel.Cell.NumberFormats.W50);
                if (ShowCurAmt)
                    withBlock.AddColumn("Import", MatHelper.Excel.Cell.NumberFormats.Decimal2Digits);
                withBlock.AddColumn("Compte", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Factura", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("Status", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Observacions", MatHelper.Excel.Cell.NumberFormats.PlainText);
            }

            if (oPnds.Count > 0)
            {
                foreach (DTOPnd item in oPnds)
                {
                    var oRow = retval.AddRow();
                    {
                        var withBlock = oRow;
                        withBlock.AddCell(item.Vto);
                        withBlock.AddCell(item.Contact.Nom);
                        withBlock.AddCell(item.Amt.Eur * (item.Cod == oCod ? 1 : -1));
                        if (ShowCurTag)
                            withBlock.AddCell(item.Amt.Cur.Tag);
                        if (ShowCurAmt)
                            withBlock.AddCell(item.Amt.Val * (item.Cod == oCod ? 1 : -1));
                        withBlock.AddCell(DTOPgcCta.FullNom(item.Cta, oLang));
                        withBlock.AddCell(item.FraNum);
                        withBlock.AddCell(item.Fch);
                        withBlock.AddCell(item.Status.ToString());
                        withBlock.AddCell(item.Fpg);
                    }
                }
            }

            return retval;
        }
    }
}
