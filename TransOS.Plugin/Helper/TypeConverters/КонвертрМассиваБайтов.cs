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
    /// Конвертирует массив байтов в HEX-строку
    /// </summary>
    public class КонвертрМассиваБайтов : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return null;

            if (value.GetType() == typeof(string))
                return МассивБайтов.КонвертироватьХексСтрокуВМассивБайтов((string)value);

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return МассивБайтов.ВСтроку((byte[])value);
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
