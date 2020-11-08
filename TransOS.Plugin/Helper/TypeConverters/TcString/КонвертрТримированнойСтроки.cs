using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.TypeConverters.TcString
{
    /// <summary>
    /// [TypeConverter(typeof(Метаинструмент.КонвертрТипа.КонвертрТримированнойСтроки))]
    /// </summary>
    public class КонвертрТримированнойСтроки : StringConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var Строка = value as string;
            return Строка == null ? null : Строка.Trim();
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var Строка = value as string;
            return base.ConvertFrom(context, culture, Строка == null ? null : Строка.Trim());
        }
    }
}
