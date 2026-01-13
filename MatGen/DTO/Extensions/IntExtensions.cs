
public static class IntExtensions
{

    public static bool IsEven(this int value) => (value & 1) == 0;
    public static bool IsOdd(this int value) => (value & 1) == 1;

}
