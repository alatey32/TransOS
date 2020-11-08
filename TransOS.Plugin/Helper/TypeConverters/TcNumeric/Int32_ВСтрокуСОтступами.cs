using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.TypeConverters.TcNumeric
{
    public class Int32_ВСтрокуСОтступами
    {
        /// <summary>
        /// превращает число в строку, остаёться N знака после комы, остальные отсекаются.
        /// </summary>
        /// <param name="Число"></param>
        /// <param name="ЧислоЗнаковПослеКоммы"></param>
        /// <returns></returns>
        public static string Конвертировать(int? Число, bool Отступы = true)
        {
            if (Число == null)
                return null;

            var s = Число.ToString();

            // вставляем пробелы через каждые 3 позиции
            if (Отступы)
            {
                for (int j = 3; j < s.Length; j += 4)
                {
                    s = s.Insert(s.Length - j, " ");
                }
            }

            return s;
        }
    }
}
