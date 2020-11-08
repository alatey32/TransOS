using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TransOS.Plugin.Helper
{
    public static class CollectionHelper
    {
        public static void ВыполнитьДляКаждого<ТипЭлемента>(this IEnumerable<ТипЭлемента> Массив, Action<ТипЭлемента> Действие)
        {
            foreach (var Элемент in Массив)
            {
                Действие.Invoke(Элемент);
            }
        }

        public static void ВыполнитьДляКаждого<ТипЭлемента>(this IEnumerable Массив, Action<ТипЭлемента> Действие)
        {
            foreach (var Элемент in Массив)
            {
                Действие.Invoke((ТипЭлемента)Элемент);
            }
        }
    }
}
