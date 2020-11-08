using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.TypeConverters.TcBoolean
{
    public abstract class БазовыйКонвертр : BooleanConverter
    {
        private readonly string ПодписьИстины;
        private readonly string ПодписьЛжи;

        public БазовыйКонвертр(string ПодписьИстины, string ПодписьЛжи)
        {
            this.ПодписьИстины = ПодписьИстины;
            this.ПодписьЛжи = ПодписьЛжи;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return (bool)value ? this.ПодписьИстины : this.ПодписьЛжи;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value != null && value.GetType() == typeof(string))
            {
                return (string)value == this.ПодписьИстины;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
