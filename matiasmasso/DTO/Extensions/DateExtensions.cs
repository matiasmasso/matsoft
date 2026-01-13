using System;

public static class DateOnlyExtension
{
    public static DateOnly Today()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }

    public static DateOnly? ToDateOnly(this DateTime? d)
    {
        return d == null ? null : new DateOnly(((DateTime)d).Year, ((DateTime)d).Month, ((DateTime)d).Day);
    }

    public static DateOnly FromYearMontDay(int year, int month, int day)
    {
        return new DateTime(year, month, day).ToDateOnly();
    }

    public static DateOnly ToDateOnly(this DateTime d)
    {
        return  new DateOnly(d.Year, d.Month, d.Day);
    }
    public static DateTime? ToDateTime(this DateOnly? d)
    {
        return d == null ? null : new DateTime((int)d.Year()!, (int)d.Month()!, (int)d.Day()!);
    }

    public static DateTime CurrentYearFirstDateTime() => (new DateTime(DateTime.Today.Year, 1, 1));
    public static DateOnly CurrentYearFirstDateOnly() => CurrentYearFirstDateTime().ToDateOnly();

    public static int? Year(this DateOnly? d)
    {
        return d == null ? null : ((DateOnly)d).ToDateTime(new TimeOnly()).Year;
    }
    public static int? Month(this DateOnly? d)
    {
        return d == null ? null : ((DateOnly)d).ToDateTime(new TimeOnly()).Month;
    }
    public static int? Day(this DateOnly? d)
    {
        return d == null ? null : ((DateOnly)d).ToDateTime(new TimeOnly()).Day;
    }


}

