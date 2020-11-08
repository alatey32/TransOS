using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.TypeConverters.TcString
{
    public class КонвертрНеПустойСтроки : КонвертрТримированнойСтроки
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var Строка = value as string;

            if (string.IsNullOrWhiteSpace(Строка))
                value = context.PropertyDescriptor.GetValue(context.Instance);

            return base.ConvertFrom(context, culture, value);
        }
    }
}
