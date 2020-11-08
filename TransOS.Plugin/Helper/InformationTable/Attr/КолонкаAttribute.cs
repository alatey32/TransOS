using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.InformationTable.Attr
{
    /// <summary>
    /// Указывает параметры колонки в таблице
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class КолонкаAttribute : Attribute
    {
        public readonly bool Видна;
        public readonly string Название;
        public readonly bool ПоискПоУмолчанию;

        /// <summary>
        /// Редактор(!) элементов не пропустит пустую строку и(или) элемент который равен null
        /// </summary>
        public readonly bool НеПустая;

        /// <summary>
        /// Где значение равно Null, будет выводиться эта подпись
        /// </summary>
        public readonly string Подпись_NULL;

        /// <summary>
        /// Название колонки БД
        /// </summary>
        public readonly string КолонкаБд;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Название">имеет больший приоритет чем DisplayName</param>
        /// <param name="ПоискПоУмолчанию">Если True - значит по этой колонке будет изначально инициализирован поиск</param>
        /// <param name="НеПустая">Редактор элементов не пропустит пустую строку и(или) элемент который равен null</param>
        public КолонкаAttribute(
            bool Видна = true,
            string Название = "",
            bool ПоискПоУмолчанию = false,
            bool НеПустая = false,
            string Подпись_NULL = "<null>",
            string КолонкаБд = "")
        {
            this.Видна = Видна;
            this.Название = Название;
            this.ПоискПоУмолчанию = ПоискПоУмолчанию;
            this.НеПустая = НеПустая;
            this.Подпись_NULL = Подпись_NULL;
            this.КолонкаБд = КолонкаБд;
        }
    }
}
