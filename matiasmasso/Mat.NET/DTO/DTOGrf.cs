using DTO;
using MatHelperStd;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
public class DTOGrf
{
    public DateTime FchFrom { get; set; }
    public DateTime FchTo { get; set; }
    public TimeHelper.DateIntervals DateInterval { get; set; }
    public List<DTOGrfItem> Items { get; set; }
}

public class DTOGrfItem
{
    public DateTime Fch { get; set; }
    public decimal Value { get; set; }
}

public class DTOGrfMesValue
{
    public DTOProduct Product { get; set; }
    public Color Color { get; set; }
    public List<DTOYearMonth> Mesos { get; set; }
    public decimal Resto { get; set; }

    public enum Codis
    {
        Comandes,
        Albarans
    }

    public decimal Sum()
    {
        decimal retval = Mesos.Sum(x => x.Eur);
        return retval;
    }


    public DTOGrfMesValue(DTOProduct oProduct, List<DTOYearMonth> oYearMonths)
    {
        Product = oProduct;
        Mesos = oYearMonths;
    }
}
