using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.Helper.InformationTable.Attr;
using TransOS.Plugin.Helper.TypeConverters.TcNumeric;

namespace TransOS.Plugin.Helper.InformationTable
{
    public static class Помощник
    {
        public static ListViewItem СвойствоКакЭлемнтТаблицы(ColumnHeader КолонкаСвойства, object ОбъектСвойства)
        {
            var ЭлементТаблицы = new ListViewItem
            {
                Tag = ОбъектСвойства,
                UseItemStyleForSubItems = false
            };

            // NULL красным цветом
            var Свойство = (PropertyInfo)КолонкаСвойства.Tag;
            var КакСтрока = Помощник.СвойствоКакСтрока(Свойство, ОбъектСвойства);
            if (КакСтрока == null)
            {
                string Подпись_NULL = "<null>";
                var АтрибутКолонки = Helper.Attr.BaseMethods.GetOne<КолонкаAttribute>(Свойство);
                if (АтрибутКолонки != null)
                    Подпись_NULL = АтрибутКолонки.Подпись_NULL;

                ЭлементТаблицы.Text = Подпись_NULL;
                if (Подпись_NULL == "<null>")
                    ЭлементТаблицы.ForeColor = Color.Red;
            }
            else
            {
                ЭлементТаблицы.Text = КакСтрока;
                ЭлементТаблицы.ForeColor = SystemColors.WindowText;
            }

            return ЭлементТаблицы;
        }

        public static string СвойствоКакСтрока(PropertyInfo Свойство, object ОбъектСвойства)
        {
            var ЗначениеЭлементаСписка = Свойство.GetValue(ОбъектСвойства, null);
            string СтрокаЗначения = null;
            if (ЗначениеЭлементаСписка != null)
            {
                // если есть конвертр, то конвертируем им
                var АтрибутКонвертации = Helper.Attr.BaseMethods.GetOne<TypeConverterAttribute>(Свойство);
                if (АтрибутКонвертации != null)
                {
                    var ТипКонвертера = Type.GetType(АтрибутКонвертации.ConverterTypeName);
                    var Конвертр = (TypeConverter)Activator.CreateInstance(ТипКонвертера);
                    return Конвертр.ConvertToString(ЗначениеЭлементаСписка);
                }

                var ТипЗначенияСвойства = ЗначениеЭлементаСписка.GetType();
                if (ТипЗначенияСвойства == typeof(decimal))
                {
                    var АтрибутСтатуса = Helper.Attr.BaseMethods.GetOne<СтатусAttribute>(Свойство);
                    if (АтрибутСтатуса == null)
                        СтрокаЗначения = Decimal_ВСтрокуСОтступами.Конвертировать((decimal)ЗначениеЭлементаСписка);
                    else
                        СтрокаЗначения = Decimal_ВСтрокуСОтступами.Конвертировать((decimal)ЗначениеЭлементаСписка, АтрибутСтатуса.ЧислоЗнаковПослеКомы);
                }
                else if (ТипЗначенияСвойства == typeof(int))
                {
                    СтрокаЗначения = Int32_ВСтрокуСОтступами.Конвертировать((int)ЗначениеЭлементаСписка);
                }
                else if (ТипЗначенияСвойства.IsEnum)
                {
                    var АтрибутОписания = Helper.Attr.BaseMethods.GetOneFromEnumMember<DescriptionAttribute>(ЗначениеЭлементаСписка);
                    if (АтрибутОписания != null)
                    {
                        СтрокаЗначения = АтрибутОписания.Description;
                    }
                    else
                        СтрокаЗначения = ЗначениеЭлементаСписка.ToString();
                }
                else if (ТипЗначенияСвойства == typeof(bool))
                {
                    СтрокаЗначения = (bool)ЗначениеЭлементаСписка ? "да" : "нет";
                }
                else
                    СтрокаЗначения = ЗначениеЭлементаСписка.ToString();
            }
            return СтрокаЗначения;
        }

        public static void ОбновитьПодЭлемнтТаблицы(ListViewItem.ListViewSubItem ПодЭлемнтТаблицы, ColumnHeader КолонкаСвойства, object ОбъектСвойства)
        {
            var Свойство = (PropertyInfo)КолонкаСвойства.Tag;
            var КакСтрока = Помощник.СвойствоКакСтрока(Свойство, ОбъектСвойства);
            if (КакСтрока == null)
            {
                string Подпись_NULL = "<null>";
                var АтрибутКолонки = Helper.Attr.BaseMethods.GetOne<КолонкаAttribute>(Свойство);
                if (АтрибутКолонки != null)
                    Подпись_NULL = АтрибутКолонки.Подпись_NULL;

                ПодЭлемнтТаблицы.Text = Подпись_NULL;
                if (Подпись_NULL == "<null>")
                    ПодЭлемнтТаблицы.ForeColor = Color.Red;
            }
            else
            {
                ПодЭлемнтТаблицы.Text = КакСтрока;
                ПодЭлемнтТаблицы.ForeColor = SystemColors.WindowText;
            }
        }

        public static ListViewItem.ListViewSubItem СвойствоКакПодЭлемнтТаблицы(ColumnHeader КолонкаСвойства, object ОбъектСвойства)
        {
            var ЭлементТаблицы = new ListViewItem.ListViewSubItem();

            // NULL красным цветом
            var Свойство = (PropertyInfo)КолонкаСвойства.Tag;
            var КакСтрока = Помощник.СвойствоКакСтрока(Свойство, ОбъектСвойства);
            if (КакСтрока == null)
            {
                string Подпись_NULL = "<null>";
                var АтрибутКолонки = Helper.Attr.BaseMethods.GetOne<КолонкаAttribute>(Свойство);
                if (АтрибутКолонки != null)
                    Подпись_NULL = АтрибутКолонки.Подпись_NULL;

                ЭлементТаблицы.Text = Подпись_NULL;
                if (Подпись_NULL == "<null>")
                    ЭлементТаблицы.ForeColor = Color.Red;
            }
            else
            {
                ЭлементТаблицы.Text = КакСтрока;
                ЭлементТаблицы.ForeColor = SystemColors.WindowText;
            }

            return ЭлементТаблицы;
        }

        public static PropertyInfo[] ПолучитьОтображаемыеСвойства(Type ТипЭлемента)
        {
            var ПрисокСвойств = new List<PropertyInfo>();
            foreach (var Свойство in ТипЭлемента.GetProperties())
            {
                var АтрибутВидимости = Helper.Attr.BaseMethods.GetOne<КолонкаAttribute>(Свойство);
                if (АтрибутВидимости != null && !АтрибутВидимости.Видна)
                {
                }
                else
                    ПрисокСвойств.Add(Свойство);
            }
            return ПрисокСвойств.ToArray();
        }

        public static PropertyInfo[] ПолучитьОтображаемыеСвойства<ТипЭлемента>()
            where ТипЭлемента : БазовыйЭлемент<ТипЭлемента>
        {
            return ПолучитьОтображаемыеСвойства(typeof(ТипЭлемента));
        }
    }
}
