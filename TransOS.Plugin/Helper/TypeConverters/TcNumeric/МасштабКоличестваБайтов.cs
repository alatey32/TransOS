using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Helper.FileSystem;

namespace TransOS.Plugin.Helper.TypeConverters.TcNumeric
{
    public class МасштабКоличестваБайтов : TypeConverter
    {
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

                var ТипЗначения = value.GetType();
                if (ТипЗначения == typeof(byte))
                {
                    var ЗнаяениеКак_byte = (byte)value;
                    return Файл.РазмерКакСтрока(ЗнаяениеКак_byte);
                }

                if (ТипЗначения == typeof(byte?))
                {
                    var ЗнаяениеКак_byte = (byte?)value;
                    return Файл.РазмерКакСтрока(ЗнаяениеКак_byte.Value);
                }

                if (ТипЗначения == typeof(sbyte))
                {
                    var ЗнаяениеКак_sbyte = (sbyte)value;

                    if (ЗнаяениеКак_sbyte >= 0)
                        return Файл.РазмерКакСтрока(ЗнаяениеКак_sbyte);
                    return null;
                }

                if (ТипЗначения == typeof(sbyte?))
                {
                    var ЗнаяениеКак_sbyte = (sbyte?)value;

                    if (ЗнаяениеКак_sbyte >= 0)
                        return Файл.РазмерКакСтрока(ЗнаяениеКак_sbyte.Value);
                    return null;
                }

                if (ТипЗначения == typeof(short))
                {
                    var ЗнаяениеКак_short = (short)value;
                    if (ЗнаяениеКак_short >= 0)
                        return Файл.РазмерКакСтрока(ЗнаяениеКак_short);
                    return null;
                }

                if (ТипЗначения == typeof(short?))
                {
                    var ЗнаяениеКак_short = (short?)value;
                    if (ЗнаяениеКак_short >= 0)
                        return Файл.РазмерКакСтрока(ЗнаяениеКак_short.Value);
                    return null;
                }

                if (ТипЗначения == typeof(ushort))
                {
                    var ЗнаяениеКак_ushort = (ushort)value;
                    return Файл.РазмерКакСтрока(ЗнаяениеКак_ushort);
                }

                if (ТипЗначения == typeof(ushort?))
                {
                    var ЗнаяениеКак_ushort = (ushort?)value;
                    return Файл.РазмерКакСтрока(ЗнаяениеКак_ushort.Value);
                }

                if (ТипЗначения == typeof(int))
                {
                    var ЗнаяениеКак_int = (int)value;
                    if (ЗнаяениеКак_int >= 0)
                        return Файл.РазмерКакСтрока(ЗнаяениеКак_int);
                    return null;
                }

                if (ТипЗначения == typeof(int?))
                {
                    var ЗнаяениеКак_int = (int?)value;
                    if (ЗнаяениеКак_int >= 0)
                        return Файл.РазмерКакСтрока(ЗнаяениеКак_int.Value);
                    return null;
                }

                if (ТипЗначения == typeof(uint))
                {
                    var ЗнаяениеКак_uint = (uint)value;
                    return Файл.РазмерКакСтрока(ЗнаяениеКак_uint);
                }

                if (ТипЗначения == typeof(uint?))
                {
                    var ЗнаяениеКак_uint = (uint?)value;
                    return Файл.РазмерКакСтрока(ЗнаяениеКак_uint.Value);
                }

                if (ТипЗначения == typeof(long))
                {
                    var ЗнаяениеКак_long = (long)value;
                    if (ЗнаяениеКак_long >= 0)
                        return Файл.РазмерКакСтрока(ЗнаяениеКак_long);
                    return null;
                }

                if (ТипЗначения == typeof(long?))
                {
                    var ЗнаяениеКак_long = (long?)value;
                    if (ЗнаяениеКак_long >= 0)
                        return Файл.РазмерКакСтрока(ЗнаяениеКак_long.Value);
                    return null;
                }

                if (ТипЗначения == typeof(ulong))
                {
                    var ЗнаяениеКак_ulong = (ulong)value;
                    return Файл.РазмерКакСтрока(ЗнаяениеКак_ulong);
                }

                if (ТипЗначения == typeof(ulong?))
                {
                    var ЗнаяениеКак_ulong = (ulong?)value;
                    return Файл.РазмерКакСтрока(ЗнаяениеКак_ulong.Value);
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
