using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Helper.Attr;

namespace TransOS.Plugin.Helper.TypeConverters
{
    public class КонвертрПеречисления<T> : КонвертрПеречисления
    {
        public КонвертрПеречисления()
            : base(typeof(T))
        {
        }
    }

    /// <summary>
    /// Отображает член перечисления как строку из атрибута Description
    /// </summary>
    public class КонвертрПеречисления : EnumConverter
    {
        private readonly Type Тип;

        public КонвертрПеречисления(Type Тип)
            : base(Тип)
        {
            this.Тип = Тип;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var Атрибут = BaseMethods.GetOneFromEnumMember<DescriptionAttribute>(value);
            return Атрибут == null ? value.ToString() : Атрибут.Description;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            foreach (var fi in this.Тип.GetFields())
            {
                var Атрибут = BaseMethods.GetOneFromEnumMember<DescriptionAttribute>(fi);
                if ((Атрибут != null) && ((string)value == Атрибут.Description))
                    return Enum.Parse(this.Тип, fi.Name);
            }

            return Enum.Parse(this.Тип, (string)value);
        }
    }
}
