using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.InformationTable
{
    public enum ЦветностьЭлемента
    {
        /// <summary>
        /// Цвет записи никогда не изменяется
        /// </summary>
        Нет,

        /// <summary>
        /// Изменяется сплошной цвет во всех колонках
        /// <para>Ожидается реализация интерфейса "СплошнаяЦветность"</para>
        /// </summary>
        Сплошная,
        /*
        /// <summary>
        /// Изменяется цвет отдельно в каждой колонке
        /// </summary>
        Раздельная*/
    }
}
