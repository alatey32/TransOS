using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.Helper.InformationTable.Attr;
using TransOS.Plugin.Helper.InformationTable.Interface;

namespace TransOS.Plugin.Helper.InformationTable
{
    /// <summary>
    /// Все типы элементов информационной таблицы должны наследовать этот класс
    /// </summary>
    public abstract class БазовыйЭлемент<ТипЗаписи> : ЯКопирабельный<ТипЗаписи>
        where ТипЗаписи : БазовыйЭлемент<ТипЗаписи>
    {
        public readonly ЦветностьЭлемента Цветность;
        public readonly string Группа;

        [Колонка(Видна: false), Browsable(false)]
        public Таблица<ТипЗаписи> ИнфТаблица
        {
            get { return this.ИнфТаблица_; }
            set
            {
                this.ИнфТаблица_ = value;
                this.ТаблицаУстановлена();
            }
        }

        public ListViewItem ВизуальныйЭлемент;

        [Колонка(Видна: false), Browsable(false)]
        public override ТипЗаписи ОрегиналКопии
        {
            get
            {
                return base.ОрегиналКопии;
            }
            protected set
            {
                base.ОрегиналКопии = value;
            }
        }

        private Таблица<ТипЗаписи> ИнфТаблица_;

        public БазовыйЭлемент(
            ЦветностьЭлемента Цветность = ЦветностьЭлемента.Нет,
            string Группа = null)
        {
            this.Цветность = Цветность;
            this.Группа = Группа;
        }

        /// <summary>
        /// Вызывается установке таблицы (при добавлении этой записи в таблицу)
        /// </summary>
        protected virtual void ТаблицаУстановлена()
        {
        }
    }
}
