using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.Helper.InformationTable.Attr;
using TransOS.Plugin.Helper.InformationTable.View;

namespace TransOS.Plugin.Helper.InformationTable
{
    public interface Таблица
    {
        /// <summary>
        /// Сгенерировать панель
        /// </summary>
        Panel Панель { get; }
        string Подпись { get; }

        void ОбновитьИзБазы();

        /// <summary>
        /// Добавить таблицу в контрол как новую панель
        /// </summary>
        /// <param name="Контрол">Контрол в который добавляется таблица</param>
        void ДобавитьПанельВКонтрол(Control Контрол);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ТипЗаписи">Тип записей таблицы</typeparam>
    public abstract class Таблица<ТипЗаписи> : Таблица
        where ТипЗаписи : БазовыйЭлемент<ТипЗаписи>

    {
        #region Записи

        protected readonly List<ТипЗаписи> Записи = new List<ТипЗаписи>();

        public IEnumerable<ТипЗаписи> Все { get { return this.Записи; } }

        /// <summary>
        /// Количество записей
        /// </summary>
        [Статус]
        public virtual int Количество { get { return this.Записи.Count; } }

        /// <summary>
        /// Отмеченые галочкой
        /// </summary>
        public IEnumerable<ТипЗаписи> Отмеченые { get { return this.Записи.Where(x => x.ВизуальныйЭлемент.Checked); } }

        /// <summary>
        /// Выделенные курсором
        /// </summary>
        public IEnumerable<ТипЗаписи> Выделенные { get { return this.Записи.Where(x => x.ВизуальныйЭлемент.Selected); } }

        public event Action<ТипЗаписи, bool> ДобавленаЗапись;
        public event Action<ТипЗаписи> УдаленаЗапись;
        public event Action ОбновлёнСтатус;
        public event Action Обновлено;
        public event Action<ТипЗаписи> ОбновленаЗапись;
        public event Action<ТипЗаписи, bool, bool> ПоказанаЗапись;

        /// <summary>
        /// В таблице изменилась запись, их количество, обновлено
        /// </summary>
        public event Action Изменена;
        //public event Action<ТипЗаписи, int> ПорядковыйНомерЗаписиИзменён;        

        /// <summary>
        /// Очистка списка пройдёт быстрее, если отключить вызуальное обновление
        /// </summary>
        public event Action<bool> РазрешеноВизуальноеОбновление;

        /// <summary>
        /// Вызывает событие "Обновлено"
        /// </summary>
        public virtual void Обновить()
        {
            if (this.Обновлено != null)
                this.Обновлено();

            if (this.Изменена != null)
                this.Изменена();
        }

        /// <summary>
        /// Вызывает событие "Обновлено".
        /// А вообще, этот метод должен обновлять локальный список из БД
        /// </summary>
        public virtual void ОбновитьИзБазы()
        {
            this.Обновить();
        }

        public void ПереместитьПоСписку_Вверх(ТипЗаписи Запись)
        {
            int Номер = this.Записи.IndexOf(Запись);
            if (Номер > 0)
                this.ИзменитьПорядковыйНомерЗаписи(Запись, Номер - 1);
        }

        public void ПереместитьПоСписку_Вниз(ТипЗаписи Запись)
        {
            int Номер = this.Записи.IndexOf(Запись);
            if (Номер < (this.Записи.Count - 1))
                this.ИзменитьПорядковыйНомерЗаписи(Запись, Номер + 1);
        }

        /// <summary>
        /// Визуальный компонент обновляет представление записи
        /// </summary>
        /// <param name="СуществующаяЗапись"></param>
        public void ОбновитьЗапись(ТипЗаписи СуществующаяЗапись)
        {
            if (this.ОбновленаЗапись != null)
                this.ОбновленаЗапись(СуществующаяЗапись);

            if (this.Изменена != null)
                this.Изменена();
        }

        /// <summary>
        /// Визуальный компонент обновляет представление записи
        /// </summary>
        /// <param name="СуществующаяЗапись"></param>
        public void ОбновитьЗаписи(IEnumerable<ТипЗаписи> СуществующиеЗаписи)
        {
            if (this.РазрешеноВизуальноеОбновление != null)
                this.РазрешеноВизуальноеОбновление(false);

            СуществующиеЗаписи.ToList().ForEach(x => this.ОбновитьЗапись(x));

            if (this.РазрешеноВизуальноеОбновление != null)
                this.РазрешеноВизуальноеОбновление(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="НоваяЗапись"></param>
        /// <param name="Показать">Выделить запись, что-бы её было видно</param>
        public virtual void ДобавитьЗапись(ТипЗаписи НоваяЗапись, bool Показать = true)
        {
            НоваяЗапись.ИнфТаблица = this;
            this.Записи.Add(НоваяЗапись);
            if (this.ДобавленаЗапись != null)
                this.ДобавленаЗапись(НоваяЗапись, Показать);

            if (this.Изменена != null)
                this.Изменена();
        }

        public void ДобавитьЗаписи(IEnumerable<ТипЗаписи> НовыеЗаписи)
        {
            if (this.РазрешеноВизуальноеОбновление != null)
                this.РазрешеноВизуальноеОбновление(false);

            НовыеЗаписи.ToList().ForEach(x => this.ДобавитьЗапись(x));

            if (this.РазрешеноВизуальноеОбновление != null)
                this.РазрешеноВизуальноеОбновление(true);
        }

        public virtual void УдалитьЗапись(ТипЗаписи Запись)
        {
            this.Записи.Remove(Запись);
            if (this.УдаленаЗапись != null)
                this.УдаленаЗапись(Запись);

            if (this.Изменена != null)
                this.Изменена();
        }

        public void УдалитьЗаписи(IEnumerable<ТипЗаписи> НовыеЗаписи)
        {
            if (this.РазрешеноВизуальноеОбновление != null)
                this.РазрешеноВизуальноеОбновление(false);

            НовыеЗаписи.ToList().ForEach(x => this.УдалитьЗапись(x));

            if (this.РазрешеноВизуальноеОбновление != null)
                this.РазрешеноВизуальноеОбновление(true);
        }

        /// <summary>
        /// Делает запись видимой в списке (прокручивает список если надо)
        /// </summary>
        /// <param name="Запись"></param>
        public void ПоказатьЗапись(ТипЗаписи Запись, bool Показать = true, bool Выделять = true)
        {
            if (this.ПоказанаЗапись != null)
                this.ПоказанаЗапись(Запись, Показать, Выделять);
        }

        public void ОбновитьСтатус()
        {
            if (this.ОбновлёнСтатус != null)
                this.ОбновлёнСтатус();
        }

        /// <summary>
        /// Удалить все записи (с событиями)
        /// </summary>
        public virtual void Очистить()
        {
            this.Записи.Clear();
            this.Обновить();
            /*
            if (this.РазрешеноВизуальноеОбновление != null)
                this.РазрешеноВизуальноеОбновление(false);

            var ЗаписиДляУдаления = this.Записи.ToArray();
            ЗаписиДляУдаления.ToList().ForEach(x => this.УдалитьЗапись(x));

            if (this.РазрешеноВизуальноеОбновление != null)
                this.РазрешеноВизуальноеОбновление(true);
             */
        }

        #endregion

        /// <summary>
        /// Значение DisplayName или GetType().Name
        /// </summary>
        public string Подпись
        {
            get
            {
                var АтрибутЗаголовка = Helper.Attr.BaseMethods.GetOne<DisplayNameAttribute>(this);
                if (АтрибутЗаголовка == null)
                    return this.GetType().Name;
                return АтрибутЗаголовка.DisplayName;
            }
        }

        public readonly bool Поиск;

        /// <summary>
        /// Сгенерировать панель
        /// </summary>
        public Panel Панель
        {
            get
            {
                return this.ИнфФорма.panel1;
            }
        }

        /// <summary>
        /// Сгенерировать форму
        /// </summary>
        public ФормаТаблицы<ТипЗаписи> ИнфФорма
        {
            get
            {
                var Форма = new ФормаТаблицы<ТипЗаписи>(this);
                Форма.Shown += new EventHandler(Форма_Shown);
                return Форма;
            }
        }

        public ТипЗаписи Выбрать()
        {
            var Форма = new ФормаТаблицы<ТипЗаписи>(this, null, ВидОтображениеФормы.Выбор);
            Форма.StartPosition = FormStartPosition.CenterScreen;
            Форма.Width = 600;
            Форма.Height = 500;
            Форма.Shown += new EventHandler(Форма_Shown);
            Форма.Shown += new EventHandler(Форма_Shown2);
            if (Форма.ShowDialog() == DialogResult.OK)
            {
                return Форма.ВыбраныйЭлемент;
            }
            return null;
        }

        void Форма_Shown2(object sender, EventArgs e)
        {
            this.ОбновитьИзБазы();
        }

        /// <summary>
        /// Форма, в которой содержиться информационная таблица
        /// <para>Если 'ИнфФорма' показана, то она автоматически устанавливается в это свойство</para>
        /// </summary>
        public Form ПоказаннаяФорма
        {
            get;
            private set;
        }

        public Таблица(Form ПоказаннаяФорма = null, bool Поиск = false)
        {
            this.ПоказаннаяФорма = ПоказаннаяФорма;
            this.Поиск = Поиск;
        }

        void Форма_Shown(object sender, EventArgs e)
        {
            this.ПоказаннаяФорма = (Form)sender;
        }

        /// <summary>
        /// Вызывается после формирования меню формой инф.таблицы.
        /// Не переопределённый метод, то ничего не делает.
        /// </summary>
        /// <param name="Меню"></param>
        public virtual void ФормированиеМенюЗавершено(MenuStrip ГлавноеМеню, ContextMenuStrip КонтекстноеМеню)
        {
        }

        /// <summary>
        /// Отметка галочкой изменена.
        /// Не переопределённый метод, то ничего не делает.
        /// </summary>
        public virtual void ОтметкаИзменена(ТипЗаписи запись)
        {
        }

        public void ИзменитьПорядковыйНомерЗаписи(ТипЗаписи запись, int НовыйПорядковыйНомер)
        {
            // изменяем в списке
            this.Записи.Remove(запись);
            this.Записи.Insert(НовыйПорядковыйНомер, запись);

            // изменяем в отображении
            this.Обновить();
            //this.
            /*if (this.ПорядковыйНомерЗаписиИзменён != null)
                this.ПорядковыйНомерЗаписиИзменён(запись, НовыйПорядковыйНомер);*/
        }

        /// <summary>
        /// Добавить таблицу в контрол как новую панель
        /// </summary>
        /// <param name="Контрол">Контрол в который добавляется таблица</param>
        public void ДобавитьПанельВКонтрол(Control Контрол)
        {
            Контрол.Controls.Clear();
            Контрол.Controls.Add(this.Панель);
        }

        /*
        #region Статусы        

        #endregion

        #region Функции

        #endregion
        */
    }
}
