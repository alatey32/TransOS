using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.TypeConverters.TcNumeric
{
    public class Decimal_ВСтрокуСОтступами
    {
        /// <summary>
        /// превращает число в строку, остаёться N знака после комы, остальные отсекаются.
        /// </summary>
        /// <param name="Число"></param>
        /// <param name="ЧислоЗнаковПослеКоммы"></param>
        /// <returns></returns>
        public static string Конвертировать(decimal? Число, uint ЧислоЗнаковПослеКоммы = 2, bool Отступы = true)
        {
            if (Число == null)
                return null;

            // формирование коэфициента
            int Коэфициент = 10;
            Математика.ВозвестиВСтепень(ref Коэфициент, ЧислоЗнаковПослеКоммы);


            // преобразование в строку
            //return string.Format("{0:N2}", Число);            
            Число *= Коэфициент;
            long i = decimal.ToInt64(Число.Value);
            Число = i;
            Число /= Коэфициент;
            var s = Число.ToString();

            int ИндексКомы = s.IndexOf(',');
            if (ИндексКомы == -1)
            {
                s += ",";
                for (uint j = 0; j < ЧислоЗнаковПослеКоммы; j++)
                {
                    s += "0";
                }
            }
            else
            {
                ИндексКомы = s.Length - ИндексКомы - 1;
                ИндексКомы = Convert.ToInt32(ЧислоЗнаковПослеКоммы) - ИндексКомы;
                for (int j = 0; j < ИндексКомы; j++)
                {
                    s += '0';
                }
            }

            // вставляем пробелы через каждые 3 позиции
            if (Отступы)
            {
                int k = 3;
                if (ЧислоЗнаковПослеКоммы > 0)
                {
                    k += 1 + Convert.ToInt32(ЧислоЗнаковПослеКоммы);
                }

                for (int j = k; j < s.Length; j += 4)
                {
                    s = s.Insert(s.Length - j, " ");
                }
            }

            return s;
        }
    }
}
