using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOForecast
    {
        public DTOCustomer Customer { get; set; }
        public DTOProductSku Sku { get; set; }
        public DTOYearMonth YearMonth { get; set; }
        public int Qty { get; set; }
        public int Sold { get; set; }
        public DTOUser UserCreated { get; set; }
    }

    public class DTOForecastProposal
    {
        public DTOProductSku Sku { get; set; }
        public List<DTOForecast> Forecasts { get; set; }
        public int Forecasted { get; set; }
        public int Suggested { get; set; }

        public int OptimizedQty { get; set; }
    }

    public class DTOProductSkuForecast : DTOProductSku
    {
        public new class Collection : List<DTOProductSkuForecast>
        {
            public FollowUp GetFollowUp(DTOProductBrand oBrand, DateTime fch)
            {
                FollowUp retval = new FollowUp(oBrand);
                foreach (DTOProductSkuForecast Item in this.Where(x => x.Category.Brand.Equals(oBrand)))
                {
                    var followUp = Item.GetFollowUp(fch);
                    {
                        var withBlock = retval;
                        withBlock.FcastQty += followUp.FcastQty;
                        withBlock.SoldQty += followUp.SoldQty;
                        withBlock.FcastAmt.Add(followUp.FcastAmt);
                        withBlock.SoldAmt.Add(followUp.SoldAmt);
                    }
                }
                return retval;
            }

            public FollowUp GetFollowUp(DTOProductCategory oCategory, DateTime fch)
            {
                FollowUp retval = new FollowUp(oCategory);
                foreach (DTOProductSkuForecast Item in this.Where(x => x.Category.Equals(oCategory)))
                {
                    var followUp = Item.GetFollowUp(fch);
                    {
                        var withBlock = retval;
                        withBlock.FcastQty += followUp.FcastQty;
                        withBlock.SoldQty += followUp.SoldQty;
                        withBlock.FcastAmt.Add(followUp.FcastAmt);
                        withBlock.SoldAmt.Add(followUp.SoldAmt);
                    }
                }
                return retval;
            }
        }

        public class FollowUp
        {
            public DTOProduct Product { get; set; }
            public int FcastQty { get; set; }
            public int SoldQty { get; set; }
            public DTOAmt FcastAmt { get; set; }
            public DTOAmt SoldAmt { get; set; }

            public FollowUp(DTOProduct oProduct)
            {
                Product = oProduct;
                FcastAmt = DTOAmt.Factory();
                SoldAmt = DTOAmt.Factory();
            }
        }

        public List<Forecast> Forecasts { get; set; }

        public DTOProductSkuForecast() // for serialization
    : base()
        {
        }

        public DTOProductSkuForecast(Guid oGuid) : base(oGuid)
        {
            Forecasts = new List<Forecast>();
        }

        public static decimal Volume(DTOProductSkuForecast.Collection oSkus, DateTime DtFch)
        {
            decimal retval = 0;
            var oSkusTrimmed = oSkus.Where(x => x.Forecasts.Count > 0).Where(y => y.Forecasts.Any(z => z.Target != 0)).ToList();
            foreach (var oSku in oSkusTrimmed)
            {
                var iForecast = Forecasted(oSku, DtFch);
                var iProposal = Proposal(oSku, iForecast);
                var iOptimized = OptimizedProposal(oSku, iProposal);
                var DcVolume = iOptimized * DTOProductSku.volumeM3OrInherited(oSku);
                retval += DcVolume;
            }
            return retval;
        }

        public static int Forecasted(DTOProductSkuForecast oSku, DateTime DtFch)
        {
            var firstYearMonth = DTOYearMonth.Current();
            var lastYearMonth = DTOYearMonth.FromFch(DtFch);
            decimal dcForecasted = 0;

            var oFcasts = oSku.Forecasts.Where(x => x.YearMonth.IsInRange(DTO.GlobalVariables.Now(), DtFch)).ToList();
            foreach (var item in oFcasts)
            {
                var monthMinutes = item.YearMonth.DaysInmonth() * 24 * 60;
                if (item.YearMonth.IsLowerThan(firstYearMonth))
                {
                }
                else if (item.YearMonth.Equals(firstYearMonth) & item.YearMonth.Equals(lastYearMonth))
                {
                    // If item.Target > 100 Then Stop ' --------------------------------------------------------------------
                    var minutesSpent = (DtFch - DTO.GlobalVariables.Now()).TotalMinutes;
                    dcForecasted += item.Target * (int)minutesSpent / monthMinutes;
                    break;
                }
                else if (item.YearMonth.Equals(firstYearMonth))
                {
                    var minutesSpent = (item.YearMonth.LastFch() - DTO.GlobalVariables.Now()).TotalMinutes;
                    dcForecasted += item.Target * (int)minutesSpent / monthMinutes;
                }
                else if (item.YearMonth.Equals(lastYearMonth))
                {
                    var minutesSpent = DtFch.Day * 24 * 60 + DtFch.Hour * 60 + DtFch.Minute;
                    dcForecasted += item.Target * minutesSpent / monthMinutes;
                }
                else if (item.YearMonth.IsGreaterThan(lastYearMonth))
                    break;
                else
                    dcForecasted += item.Target;
            }

            int retval = (int)Math.Round(dcForecasted);
            return retval;
        }


        public DTOProductSkuForecast.FollowUp GetFollowUp(DateTime DtFch)
        {
            DTOProductSkuForecast.FollowUp retval = new DTOProductSkuForecast.FollowUp(this);

            var oPastMonths = this.Forecasts.Where(x => x.YearMonth.Year == DtFch.Year & (int)x.YearMonth.Month < (int)DtFch.Month).ToList();
            retval.FcastQty = oPastMonths.Sum(x => x.Target);
            retval.SoldQty = oPastMonths.Sum(x => x.Sold);

            var oCurrentMonth = this.Forecasts.FirstOrDefault(x => x.YearMonth.Year == DtFch.Year & (int)x.YearMonth.Month == (int)DtFch.Month);
            if (oCurrentMonth != null)
            {
                var monthMinutes = DTOYearMonth.FromFch(DtFch).DaysInmonth() * 24 * 60;
                var minutesSpent = DtFch.Day * 24 * 60 + DtFch.Hour * 60 + DtFch.Minute;
                retval.FcastQty += oCurrentMonth.Target * minutesSpent / monthMinutes;
                retval.SoldQty += oCurrentMonth.Sold;
            }

            if (this.Cost != null)
            {
                retval.FcastAmt = DTOAmt.Factory(retval.FcastQty * this.Cost.Eur);
                retval.SoldAmt = DTOAmt.Factory(retval.SoldQty * this.Cost.Eur);
            }
            return retval;
        }


        public static int Proposal(DTOProductSku oSku, int iForecast)
        {
            int retval = iForecast + oSku.Clients + oSku.SecurityStock - oSku.Stock - oSku.Proveidors;
            return retval;
        }

        public static int OptimizedProposal(DTOProductSku oSku, int iProposal)
        {
            int retval = iProposal;
            if (oSku.LastProduction)
                retval = 0;
            else if (retval >= 0)
            {
                if (oSku.InnerPack > 1)
                {
                    int resto = retval % oSku.InnerPack;
                    retval += resto;
                }
            }
            else
                retval = 0;
            return retval;
        }

        public static MatHelper.Excel.Sheet Excel(DTOProductSkuForecast.Collection values, DTOLang oLang)
        {
            var dtFch = values.SelectMany(x => x.Forecasts).Max(y => y.FchCreated);
            string sSheetName = "Forecast " + TextHelper.VbFormat(dtFch, "dd/MM/yy HH:mm");
            var sFileName = string.Format("M+O Forecast {0:yyyy.MM.dd.HH.mm}.xlsx", dtFch);
            var sCurrentTag = DTOYearMonth.Current().Tag();
            var sMinTag = values.SelectMany(x => x.Forecasts).Where(y => string.Compare(y.YearMonth.Tag(), sCurrentTag) >= 0).Min(z => z.YearMonth.Tag());
            var sMaxTag = values.SelectMany(x => x.Forecasts).Max(y => y.YearMonth.Tag());

            var oRange = DTOYearMonth.Range(sMinTag, sMaxTag);
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(sSheetName, sFileName);
            {
                var withBlock = retval;
                withBlock.AddColumn("Brand");
                withBlock.AddColumn("Category");
                withBlock.AddColumn("Code");
                withBlock.AddColumn("Product");
                withBlock.AddColumn("Stock", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Customer pending", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Suplier pending", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Net Cost", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("Volume", MatHelper.Excel.Cell.NumberFormats.m3D2);
                foreach (var item in oRange)
                    withBlock.AddColumn(item.Formatted(DTOLang.ENG()), MatHelper.Excel.Cell.NumberFormats.Integer);
            }

            foreach (var value in values.Where(x => x.HasTargetsInRange(oRange)))
            {
                decimal dcCost = 0;
                if (value.Cost != null)
                    dcCost = value.Cost.Eur * (100 - value.CustomerDto) / 100;

                var oRow = retval.AddRow();
                oRow.AddCell(value.Category.Brand.Nom.Tradueix(oLang));
                oRow.AddCell(value.Category.Nom.Tradueix(oLang));
                oRow.AddCell(value.RefProveidor);
                oRow.AddCell(value.NomProveidor);
                oRow.AddCell(value.Stock);
                oRow.AddCell(value.Clients);
                oRow.AddCell(value.Proveidors);
                oRow.AddCell(dcCost);
                oRow.AddCell(value.VolumeM3OrInherited());
                foreach (var item in oRange)
                {
                    DTOProductSkuForecast.Forecast oFc = value.Forecasts.FirstOrDefault(x => x.YearMonth.Equals(item));
                    if (oFc == null)
                        oRow.AddCell(0);
                    else
                        oRow.AddCell(oFc.Target);
                }
            }

            return retval;
        }



        public class Forecast
        {
            public DTOYearMonth YearMonth { get; set; }
            public int Target { get; set; }
            public int Sold { get; set; }
            public DTOUser UserCreated { get; set; }
            public DateTime FchCreated { get; set; }
        }

        public bool HasTargetsInRange(List<DTOYearMonth> oRange)
        {
            var retval = Forecasts.Where(x => x.Target > 0).Any(y => y.YearMonth.IsInRange(oRange));
            return retval;
        }
    }
}
