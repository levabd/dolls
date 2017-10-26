// ReSharper disable once CheckNamespace
public static class FloatExtensions
{
    public static bool CheckRange(this float num, float min, float max)
    {
        return num >= min && num <= max;
    }
}

