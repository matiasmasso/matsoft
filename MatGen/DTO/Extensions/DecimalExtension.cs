
public static class DecimalExtension
{
    /// <summary>
    /// GetDecimalPlaces returns the number of decimal digits, useful ie: to unify right align percentages
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int GetDecimalPlaces(this decimal n)
    {
        n = Math.Abs(n); //make sure it is positive.
        n -= (int)n;     //remove the integer part of the number.
        var decimalPlaces = 0;
        while (n > 0)
        {
            decimalPlaces++;
            n *= 10;
            n -= (int)n;
        }
        return decimalPlaces;
    }
}

