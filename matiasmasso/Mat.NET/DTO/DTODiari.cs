using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTODiari
    {
        public DTOEmp Emp { get; set; }
        public DTOLang Lang { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public DTODistributionChannel Channel { get; set; }
        public DTORep Rep { get; set; }
        public List<int> Years { get; set; }
        public List<DTOProductBrand> Brands { get; set; }
        public int MaxDisplayableBrands { get; set; } = 100;
        public List<DtoDiariItem> Items { get; set; }
        public DTOContact Owner { get; set; }
        public DTOUser User { get; set; }

        public Modes Mode { get; set; } = Modes.Pdcs;

        public enum Modes
        {
            Pdcs,
            Albs,
            Fras
        }

        public static DTODiari Factory(DTODiari.Modes oMode, DTOUser oUser, int iYear = 0, int iMonth = 0, int iDay = 0, DTOContact oOwner = null/* TODO Change to default(_) if this is not a reference type */, DTODistributionChannel oChannel = null/* TODO Change to default(_) if this is not a reference type */, DTORep oRep = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (iYear == 0)
                iYear = DTO.GlobalVariables.Today().Year;
            DTODiari retval = new DTODiari();
            {
                var withBlock = retval;
                withBlock.Year = iYear;
                withBlock.Month = iMonth;
                withBlock.Day = iDay;
                withBlock.Channel = oChannel;
                withBlock.Rep = oRep;
                withBlock.Emp = oUser.Emp;
                withBlock.Lang = oUser.Lang;
                withBlock.User = oUser;
                withBlock.Owner = oOwner;
                withBlock.Mode = oMode;
            }
            return retval;
        }

        public DateTime Fch()
        {
            DateTime retval = default(DateTime);
            if (Year == 0)
                retval = DTO.GlobalVariables.Today();
            else if (Month == 0)
            {
                if (Year == DTO.GlobalVariables.Today().Year)
                    retval = DTO.GlobalVariables.Today();
                else
                    retval = new DateTime(Year, 12, 31);
            }
            else if (Day == 0)
            {
                if (Year == DTO.GlobalVariables.Today().Year & Month == DTO.GlobalVariables.Today().Month)
                    retval = DTO.GlobalVariables.Today();
                else
                    retval = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
            }
            else
                retval = new DateTime(Year, Month, Day);
            return retval;
        }

        public int DisplayableBrandsCount()
        {
            int retval = Math.Min(Brands.Count, MaxDisplayableBrands);
            return retval;
        }

        public List<DTOProductBrand> DisplayableBrands()
        {
            List<DTOProductBrand> retval = Brands.GetRange(0, DisplayableBrandsCount());
            return retval;
        }
    }

    public class DtoDiariItem
    {
        public DTODiari Parent { get; set; }
        public object Source { get; set; }
        public DTOPurchaseOrder PurchaseOrder { get; set; }
        public DateTime Fch { get; set; }
        public Levels Level { get; set; }
        public int Index { get; set; }
        public int ParentIndex { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public List<decimal> Values { get; set; }
        public decimal Total { get; set; }

        public enum Levels
        {
            Yea,
            Mes,
            Dia,
            Pdc
        }

        public decimal Resto()
        {
            decimal SumaDisplayedBrands = 0;

            foreach (decimal value in DisplayableValues())
                SumaDisplayedBrands += value;
            decimal retval = Total - SumaDisplayedBrands;
            return retval;
        }

        public List<decimal> DisplayableValues()
        {
            List<decimal> retval = Values.GetRange(0, Parent.DisplayableBrandsCount());
            return retval;
        }
    }
}
