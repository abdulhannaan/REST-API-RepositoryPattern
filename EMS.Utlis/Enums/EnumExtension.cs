using System.ComponentModel;
using System.Reflection;

namespace EMS.Utlis.Enums
{
    public static class EnumExtension
    {
        public static string ToDescriptionString<T>(this T val) where T : Enum
        {
            FieldInfo fieldInfo = val.GetType().GetField(val.ToString());

            if (fieldInfo == null)
                return val.ToString();

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : val.ToString();
        }
    }
}
