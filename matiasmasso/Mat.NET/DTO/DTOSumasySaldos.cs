using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOSumasYSaldos
    {
        public DateTime Fch { get; set; }
        public List<DTOSumasYSaldosItem> Items { get; set; }

        public DTOSumasYSaldos(DateTime DtFch) : base()
        {
            Fch = DtFch;
            Items = new List<DTOSumasYSaldosItem>();
        }

        public static MatHelper.Excel.Sheet Excel(DTOEmp oEmp, DTOSumasYSaldos value, DTOPgcCta.Digits oDigits, ProgressBarHandler ShowProgress)
        {
            DTOLang oLang = DTOLang.CAT();
            bool CancelRequest = false;
            // ShowProgress(0, value.items.Count, 0, "redactant excel", CancelRequest)

            List<DTOSumasYSaldosItem> items = null;
            string sfilename = "";

            switch (oDigits)
            {
                case DTOPgcCta.Digits.Digits3:
                    {
                        sfilename = string.Format("{0}.{1:yyyy.MM.dd} Balanç 3 digits.xlsx", oEmp.Org.PrimaryNifValue(), value.Fch);
                        items = value.Items.GroupBy(g => new { g.Digits3, g.Nom, g.Act }).Select(group => new DTOSumasYSaldosItem()
                        {
                            Id = group.Key.Digits3,
                            Nom = group.Key.Nom,
                            Act = group.Key.Act,
                            SdoInicial = group.Sum(a => a.SdoInicial),
                            Debe = group.Sum(a => a.Debe),
                            Haber = group.Sum(a => a.Haber),
                            SdoFinal = group.Sum(a => a.SdoFinal)
                        }).ToList();
                        break;
                    }

                case DTOPgcCta.Digits.Digits4:
                    {
                        sfilename = string.Format("{0}.{1:yyyy.MM.dd} Balanç 4 digits.xlsx", oEmp.Org.PrimaryNifValue(), value.Fch);
                        items = value.Items.GroupBy(g => new { g.Digits4, g.Nom, g.Act }).Select(group => new DTOSumasYSaldosItem()
                        {
                            Id = group.Key.Digits4,
                            Nom = group.Key.Nom,
                            Act = group.Key.Act,
                            SdoInicial = group.Sum(a => a.SdoInicial),
                            Debe = group.Sum(a => a.Debe),
                            Haber = group.Sum(a => a.Haber),
                            SdoFinal = group.Sum(a => a.SdoFinal)
                        }).ToList();
                        break;
                    }

                case DTOPgcCta.Digits.Digits5:
                    {
                        sfilename = string.Format("{0}.{1:yyyy.MM.dd} Balanç 5 digits.xlsx", oEmp.Org.PrimaryNifValue(), value.Fch);
                        items = value.Items.GroupBy(g => new { g.Id, g.Nom, g.Act }).Select(group => new DTOSumasYSaldosItem()
                        {
                            Id = group.Key.Id,
                            Nom = group.Key.Nom,
                            Act = group.Key.Act,
                            SdoInicial = group.Sum(a => a.SdoInicial),
                            Debe = group.Sum(a => a.Debe),
                            Haber = group.Sum(a => a.Haber),
                            SdoFinal = group.Sum(a => a.SdoFinal)
                        }).ToList();
                        break;
                    }

                default:
                    {
                        sfilename = string.Format("{0}.{1:yyyy.MM.dd} Balanç tots els digits.xlsx", oEmp.Org.PrimaryNifValue(), value.Fch);
                        items = value.Items;
                        break;
                    }
            }


            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Sumes i saldos", sfilename);
            {
                var withBlock = retval;
                withBlock.AddColumn("compte", MatHelper.Excel.Cell.NumberFormats.W50);
                switch (oDigits)
                {
                    case DTOPgcCta.Digits.Digits5:
                    case DTOPgcCta.Digits.Full:
                        {
                            withBlock.AddColumn("descripció", MatHelper.Excel.Cell.NumberFormats.PlainText);
                            break;
                        }
                }
                withBlock.AddColumn("saldo inicial", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("deure", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("haver", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("saldo final", MatHelper.Excel.Cell.NumberFormats.Euro);
            }

            foreach (DTOSumasYSaldosItem item in items)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                switch (oDigits)
                {
                    case DTOPgcCta.Digits.Full:
                        {
                            oRow.AddCell(DTOPgcCta.formatAccountId(item, item.Contact));
                            oRow.AddCell(DTOPgcCta.formatAccountDsc(item, item.Contact, oLang));
                            break;
                        }

                    case DTOPgcCta.Digits.Digits5:
                        {
                            oRow.AddCell(item.Id);
                            oRow.AddCell(item.Nom.Tradueix(oLang));
                            break;
                        }

                    default:
                        {
                            oRow.AddCell(item.Id);
                            break;
                        }
                }
                oRow.AddCell(item.SdoInicial);
                oRow.AddCell(item.Debe);
                oRow.AddCell(item.Haber);
                oRow.AddCell(item.SdoFinal);

                // ShowProgress(0, value.items.Count, 0, "redactant excel", CancelRequest)
                if (CancelRequest)
                    break;
            }
            return retval;
        }
    }

    public class DTOSumasYSaldosItem : DTOPgcCta
    {
        public DTOContact Contact { get; set; }
        public decimal SdoInicial { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public decimal SdoFinal { get; set; }

        public decimal SdoFinalCreditor
        {
            get
            {
                decimal retval = -SdoFinal;
                return retval;
            }
        }

        public DTOSumasYSaldosItem() : base()
        {
        }

        public DTOSumasYSaldosItem(Guid oGuid) : base(oGuid)
        {
        }
    }
}
