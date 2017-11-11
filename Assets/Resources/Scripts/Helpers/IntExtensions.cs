// ReSharper disable once CheckNamespace
public static class IntExtensions
{
    public static bool CheckRange(this int num, int min, int max)
    {
        return num >= min && num <= max;
    }
}

