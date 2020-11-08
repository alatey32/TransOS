using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.TypeConverters
{
    /// <summary>
    /// string[] => s1, s2, ...
    /// </summary>
    public class КонвертрМассиваСтрок : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value != null && value.GetType() == typeof(string[]))
                return string.Join(", ", (string[])value);

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
