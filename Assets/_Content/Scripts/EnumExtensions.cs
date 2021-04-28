using System;
using System.ComponentModel;

public static class EnumExtensions
{
    public static string ToDescription(this Enum value)
    {
        DescriptionAttribute[] da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return da.Length > 0 ? da[0].Description : value.ToString();
    }
}