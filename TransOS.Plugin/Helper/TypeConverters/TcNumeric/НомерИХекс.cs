using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.TypeConverters.TcNumeric
{
    /// <summary>
    /// Конвертирует в вид: 15 (0x0F)
    /// <para>Обратный порядок байт</para>
    /// </summary>
    public class НомерИХекс : TypeConverter
    {
        /*public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                if (context.PropertyDescriptor.PropertyType == typeof(byte) ||
                    context.PropertyDescriptor.PropertyType == typeof(sbyte) ||
                    context.PropertyDescriptor.PropertyType == typeof(short) ||
                    context.PropertyDescriptor.PropertyType == typeof(ushort) ||
                    context.PropertyDescriptor.PropertyType == typeof(int) ||
                    context.PropertyDescriptor.PropertyType == typeof(uint) ||
                    context.PropertyDescriptor.PropertyType == typeof(long) ||
                    context.PropertyDescriptor.PropertyType == typeof(ulong))
                    return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }*/

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value == null)
                    return null;

                string ФорматСтроки = "{0:N0} (0x{1})";
                var ТипЗначения = value.GetType();
                if (ТипЗначения == typeof(byte))
                {
                    var ЗнаяениеКак_byte = (byte)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_byte, this.МассивБайтовВФорматированнуюСтроку(new byte[] { ЗнаяениеКак_byte }));
                }

                if (ТипЗначения == typeof(byte?))
                {
                    var ЗнаяениеКак_byte = (byte?)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_byte, this.МассивБайтовВФорматированнуюСтроку(new byte[] { ЗнаяениеКак_byte.Value }));
                }

                if (ТипЗначения == typeof(sbyte))
                {
                    var ЗнаяениеКак_sbyte = (sbyte)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_sbyte, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_sbyte)));
                }

                if (ТипЗначения == typeof(sbyte?))
                {
                    var ЗнаяениеКак_sbyte = (sbyte?)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_sbyte, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_sbyte.Value)));
                }

                if (ТипЗначения == typeof(short))
                {
                    var ЗнаяениеКак_short = (short)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_short, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_short).Reverse()));
                }

                if (ТипЗначения == typeof(short?))
                {
                    var ЗнаяениеКак_short = (short?)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_short, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_short.Value).Reverse()));
                }

                if (ТипЗначения == typeof(ushort))
                {
                    var ЗнаяениеКак_ushort = (ushort)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_ushort, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_ushort).Reverse()));
                }

                if (ТипЗначения == typeof(ushort?))
                {
                    var ЗнаяениеКак_ushort = (ushort?)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_ushort, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_ushort.Value).Reverse()));
                }

                if (ТипЗначения == typeof(int))
                {
                    var ЗнаяениеКак_int = (int)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_int, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_int).Reverse()));
                }

                if (ТипЗначения == typeof(int?))
                {
                    var ЗнаяениеКак_int = (int?)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_int, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_int.Value).Reverse()));
                }

                if (ТипЗначения == typeof(uint))
                {
                    var ЗнаяениеКак_uint = (uint)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_uint, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_uint).Reverse()));
                }

                if (ТипЗначения == typeof(uint?))
                {
                    var ЗнаяениеКак_uint = (uint?)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_uint, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_uint.Value).Reverse()));
                }

                if (ТипЗначения == typeof(long))
                {
                    var ЗнаяениеКак_long = (long)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_long, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_long).Reverse()));
                }

                if (ТипЗначения == typeof(long?))
                {
                    var ЗнаяениеКак_long = (long?)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_long, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_long.Value).Reverse()));
                }

                if (ТипЗначения == typeof(ulong))
                {
                    var ЗнаяениеКак_ulong = (ulong)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_ulong, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_ulong).Reverse()));
                }

                if (ТипЗначения == typeof(ulong?))
                {
                    var ЗнаяениеКак_ulong = (ulong?)value;
                    return string.Format(ФорматСтроки, ЗнаяениеКак_ulong, this.МассивБайтовВФорматированнуюСтроку(BitConverter.GetBytes(ЗнаяениеКак_ulong.Value).Reverse()));
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        private string МассивБайтовВФорматированнуюСтроку(byte[] Массив)
        {
            return BitConverter.ToString(МассивБайтов.УбратьПервыеНули(Массив));
        }

        private string МассивБайтовВФорматированнуюСтроку(IEnumerable<byte> Массив)
        {
            return this.МассивБайтовВФорматированнуюСтроку(Массив.ToArray());
        }
    }
}
