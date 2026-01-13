using System;

namespace DTO
{
    public static class EnumExtension
    {
        //usage: StatusEnum MyStatus = "Active".ToEnum<StatusEnum>();
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

    }
}
