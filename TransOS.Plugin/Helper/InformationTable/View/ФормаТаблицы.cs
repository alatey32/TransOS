using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.Helper.InformationTable.Attr;
using TransOS.Plugin.Helper.InformationTable.Interface;

namespace TransOS.Plugin.Helper.InformationTable.View
{
    public partial class ФормаТаблицы<ТипЗаписи> : Form
        where ТипЗаписи : BaseElement<ТипЗаписи>
    {
        static string ПолучитьНазваниеКолонки(PropertyInfo Свойство)
        {
            var АтрибутКолонкиВТаблице = Helper.Attr.BaseMethods.GetOne<КолонкаAttribute>(Свойство);
            if (АтрибутКолонкиВТаблице != null && !string.IsNullOrWhiteSpace(АтрибутКолонкиВТаблице.Название))
                return АтрибутКолонкиВТаблице.Название;

            var АтрибутНазвания = Helper.Attr.BaseMethods.GetOne<DisplayNameAttribute>(Свойство);
            return АтрибутНазвания != null ?
                АтрибутНазвания.DisplayName :
                Свойство.Name;
        }

        public readonly Таблица<ТипЗаписи> Таблица;

        public event Action<string> ПриВводеЦифр;

        int ИндексВставки = 0;

        private readonly Dictionary<Keys, MethodInfo> СкрытыеФункции = new Dictionary<Keys, MethodInfo>();

        public ТипЗаписи ВыбраныйЭлемент
        {
            get
            {
                if (this.listView1.SelectedItems.Count > 0)
                    return (ТипЗаписи)this.listView1.SelectedItems[0].Tag;
                return null;
            }
        }

        /// <summary>
        /// Выбранные галочками
        /// </summary>
        public IEnumerable<ТипЗаписи> ВыбраныеЭлементы
        {
            get
            {
                return this.listView1.Items.Cast<ListViewItem>()
                    .Where(x => x.Checked)
                    .Select(x => (ТипЗаписи)x.Tag);
            }
        }

        /// <summary>
        /// Выделенные мышкой (не галочками)
        /// </summary>
        public IEnumerable<ТипЗаписи> ВыделенныеЭлементы
        {
            get
            {
                return this.listView1.Items.Cast<ListViewItem>()
                    .Where(x => x.Selected)
                    .Select(x => (ТипЗаписи)x.Tag);
            }
        }

        /// <summary>
        /// Включена ли отправка (перетаскивание)
        /// </summary>
        public bool DragNDrop_Отправка = false;
        readonly string ЗаголовокФормы;

        /// <summary>
        /// Сортировщик колонок
        /// </summary>
        private readonly СортировщикКолонок<ТипЗаписи> lvwColumnSorter;

        readonly ВидОтображениеФормы ОтображениеФормы;
        public event Action<Таблица<ТипЗаписи>, ToolStripItemCollection> СтатусОбновлён;

        public ФормаТаблицы(Таблица<ТипЗаписи> Таблица, string ЗаголовокФормы = null, ВидОтображениеФормы ОтображениеФормы = ВидОтображениеФормы.Показ)
        {
            InitializeComponent();

            this.Таблица = Таблица;
            this.ЗаголовокФормы = ЗаголовокФормы;
            this.ОтображениеФормы = ОтображениеФормы;

            // установка для выбора нескольких вариантов
            if (this.ОтображениеФормы == ВидОтображениеФормы.ВыборНескольких)
            {
                this.listView1.CheckBoxes = true;
            }

            var ОтображаемыеСвойства = Помощник.ПолучитьОтображаемыеСвойства<ТипЗаписи>();

            // установка сортировщика
            this.lvwColumnSorter = new СортировщикКолонок<ТипЗаписи>(ОтображаемыеСвойства);
            this.listView1.ListViewItemSorter = this.lvwColumnSorter;

            // привязка событий к таблице
            this.Таблица.ДобавленаЗапись += new Action<ТипЗаписи, bool>(Таблица_ДобавленаЗапись);
            this.Таблица.УдаленаЗапись += new Action<ТипЗаписи>(Таблица_УдаленаЗапись);
            this.Таблица.ОбновлёнСтатус += new Action(Таблица_ОбновлёнСтатус);
            this.Таблица.Обновлено += new Action(Таблица_Обновлено);
            this.Таблица.ОбновленаЗапись += new Action<ТипЗаписи>(Таблица_ОбновленаЗапись);
            this.Таблица.РазрешеноВизуальноеОбновление += new Action<bool>(Таблица_РазрешеноВизуальноеОбновление);
            this.Таблица.ПоказанаЗапись += new Action<ТипЗаписи, bool, bool>(Таблица_ПоказанаЗапись);

            // адаптируем колонки
            foreach (var Свойство in ОтображаемыеСвойства)
            {
                string НазваниеКолонки = ПолучитьНазваниеКолонки(Свойство);
                this.listView1.Columns.Add(НазваниеКолонки).Tag = Свойство;

                if (this.Таблица.Поиск)
                {
                    var ПунктМеню = this.поискToolStripMenuItem.DropDownItems.Add(НазваниеКолонки);
                    ПунктМеню.Tag = Свойство;
                    ПунктМеню.Click += new EventHandler(this.ПунктМенюПоиска_Click);
                }
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // адаптируем статус
            foreach (var СвойствоСтатус in this.Таблица.GetType().GetProperties()
                .ToDictionary(x => x, y => Helper.Attr.BaseMethods.GetOne<СтатусAttribute>(y))
                .Where(x => x.Value != null))
            {
                var Надпись = new ToolStripStatusLabel();
                Надпись.Tag = new KeyValuePair<PropertyInfo, string>(СвойствоСтатус.Key, string.IsNullOrWhiteSpace(СвойствоСтатус.Value.Подпись) ? СвойствоСтатус.Key.Name : СвойствоСтатус.Value.Подпись);

                // ссылка на изменение
                //if (СвойствоСтатус.Key.CanRead && СвойствоСтатус.Key.CanWrite)
                if (СвойствоСтатус.Value.Изменяемое)
                {
                    Надпись.IsLink = true;
                    Надпись.Click += new EventHandler(this.Надпись_Click);
                }

                this.statusStrip1.Items.Add(Надпись);
            }
            if (this.statusStrip1.Items.Count > 0)
                this.ОбновитьСтатусы();
            else
            {
                // прячем статус
                this.statusStrip1.Visible = false;
                this.listView1.Height += this.statusStrip1.Height;
            }

            // адаптируем меню
            bool ПунктКонтекстногоМенюПоУмолчаниюУстановлен = false;
            bool ПунктКонтекстногоМенюПоУмолчаниюУстановлен2 = false;

            // -меняем меню, если таблица показана для выбора
            ToolStripMenuItem ЭлементВерхнегоМеню;
            switch (this.ОтображениеФормы)
            {
                case ВидОтображениеФормы.Выбор:
                    // Выбрать
                    ЭлементВерхнегоМеню = new ToolStripMenuItem("Выбрать");
                    ЭлементВерхнегоМеню.Font = new System.Drawing.Font(ЭлементВерхнегоМеню.Font, FontStyle.Bold);
                    ЭлементВерхнегоМеню.Click += new EventHandler(ЭлементВерхнегоМеню_Click);
                    this.menuStrip1.Items.Insert(this.ИндексВставки++, ЭлементВерхнегоМеню);
                    break;

                case ВидОтображениеФормы.ВыборНескольких:
                    // Выбрать
                    ЭлементВерхнегоМеню = new ToolStripMenuItem("Выбрать");
                    ЭлементВерхнегоМеню.Click += new EventHandler(ЭлементВерхнегоМеню_Click);
                    this.menuStrip1.Items.Insert(this.ИндексВставки++, ЭлементВерхнегоМеню);

                    var ЭлементКонтекстногогоМеню = new ToolStripMenuItem("Выбрать");
                    ЭлементКонтекстногогоМеню.Click += new EventHandler(ЭлементВерхнегоМеню_Click);
                    this.contextMenuStrip1.Items.Add(ЭлементКонтекстногогоМеню);

                    // отметить все
                    ЭлементВерхнегоМеню = new ToolStripMenuItem("Отметить все");
                    ЭлементВерхнегоМеню.Click += new EventHandler(ОтметитьВсе_Click);
                    this.menuStrip1.Items.Insert(this.ИндексВставки++, ЭлементВерхнегоМеню);

                    ЭлементКонтекстногогоМеню = new ToolStripMenuItem(ЭлементВерхнегоМеню.Text);
                    ЭлементКонтекстногогоМеню.Click += new EventHandler(ОтметитьВсе_Click);
                    this.contextMenuStrip1.Items.Add(ЭлементКонтекстногогоМеню);

                    // снять все отметки
                    ЭлементВерхнегоМеню = new ToolStripMenuItem("Снять все отметки");
                    ЭлементВерхнегоМеню.Click += new EventHandler(СнятьВсеОтметки_Click);
                    this.menuStrip1.Items.Insert(this.ИндексВставки++, ЭлементВерхнегоМеню);

                    ЭлементКонтекстногогоМеню = new ToolStripMenuItem(ЭлементВерхнегоМеню.Text);
                    ЭлементКонтекстногогоМеню.Click += new EventHandler(СнятьВсеОтметки_Click);
                    this.contextMenuStrip1.Items.Add(ЭлементКонтекстногогоМеню);

                    // Инвертировать отметки
                    ЭлементВерхнегоМеню = new ToolStripMenuItem("Инвертировать отметки");
                    ЭлементВерхнегоМеню.Click += new EventHandler(ИнвертироватьОтметки_Click);
                    this.menuStrip1.Items.Insert(this.ИндексВставки++, ЭлементВерхнегоМеню);

                    ЭлементКонтекстногогоМеню = new ToolStripMenuItem(ЭлементВерхнегоМеню.Text);
                    ЭлементКонтекстногогоМеню.Click += new EventHandler(ИнвертироватьОтметки_Click);
                    this.contextMenuStrip1.Items.Add(ЭлементКонтекстногогоМеню);
                    break;

                default:
                    foreach (var МетодФункция in this.Таблица.GetType().GetMethods().ToDictionary(x => x, y => Helper.Attr.BaseMethods.GetOne<ФункцияAttribute>(y)).Where(x => x.Value != null))
                    {
                        string НазваниеСтатуса = !string.IsNullOrWhiteSpace(МетодФункция.Value.Подпись) ?
                                МетодФункция.Value.Подпись : МетодФункция.Key.Name;

                        //разбиваем на подменю
                        var Подменю = НазваниеСтатуса.Split('/');

                        if (МетодФункция.Value.ВерхнееМеню)
                        {
                            // ищем/создаём подменю
                            var КоллекцияПоискаПодменю = this.menuStrip1.Items;
                            for (int i = 0; i < Подменю.Count() - 1; i++)
                            {
                                var НайденоеПодменю = КоллекцияПоискаПодменю
                                    .Cast<ToolStripItem>()
                                    .Where(x => x.GetType() == typeof(ToolStripMenuItem))
                                    .Cast<ToolStripMenuItem>()
                                    .FirstOrDefault(x => x.Text == Подменю[i]);
                                if (НайденоеПодменю == null)
                                {
                                    НайденоеПодменю = new ToolStripMenuItem(Подменю[i]);

                                    if (i == 0)
                                        КоллекцияПоискаПодменю.Insert(this.ИндексВставки++, НайденоеПодменю);
                                    else
                                        КоллекцияПоискаПодменю.Add(НайденоеПодменю);
                                }

                                КоллекцияПоискаПодменю = НайденоеПодменю.DropDownItems;
                            }
                            НазваниеСтатуса = Подменю.Last();

                            // создаём пункт меню
                            var ЭлементГлавногоМеню = new ToolStripMenuItem(НазваниеСтатуса);
                            ЭлементГлавногоМеню.Tag = МетодФункция.Key;

                            if (!this.Modal && !ПунктКонтекстногоМенюПоУмолчаниюУстановлен2 && МетодФункция.Value.ПоУмолчанию)
                            {
                                ЭлементГлавногоМеню.Font = new Font(ЭлементГлавногоМеню.Font, FontStyle.Bold);
                                ПунктКонтекстногоМенюПоУмолчаниюУстановлен2 = true;
                            }

                            ЭлементГлавногоМеню.Click += new EventHandler(ЭлементГлавногоМеню_Click);

                            if (КоллекцияПоискаПодменю == this.menuStrip1.Items)
                                КоллекцияПоискаПодменю.Insert(this.ИндексВставки++, ЭлементГлавногоМеню);
                            else
                                КоллекцияПоискаПодменю.Add(ЭлементГлавногоМеню);
                        }

                        if (МетодФункция.Value.КонтекстноеМеню)
                        {
                            // ищем/создаём подменю
                            var КоллекцияПоискаПодменю = this.contextMenuStrip1.Items;
                            for (int i = 0; i < Подменю.Count() - 1; i++)
                            {
                                var НайденоеПодменю = КоллекцияПоискаПодменю
                                    .Cast<ToolStripItem>()
                                    .Where(x => x.GetType() == typeof(ToolStripMenuItem))
                                    .Cast<ToolStripMenuItem>()
                                    .FirstOrDefault(x => x.Text == Подменю[i]);
                                if (НайденоеПодменю == null)
                                {
                                    НайденоеПодменю = new ToolStripMenuItem(Подменю[i]);
                                    КоллекцияПоискаПодменю.Add(НайденоеПодменю);
                                }

                                КоллекцияПоискаПодменю = НайденоеПодменю.DropDownItems;
                            }
                            НазваниеСтатуса = Подменю.Last();

                            var ЭлементКонтекстногоМеню = new ToolStripMenuItem(НазваниеСтатуса);
                            ЭлементКонтекстногоМеню.Tag = МетодФункция.Key;

                            if (!this.Modal && !ПунктКонтекстногоМенюПоУмолчаниюУстановлен && МетодФункция.Value.ПоУмолчанию)
                            {
                                ЭлементКонтекстногоМеню.Font = new Font(ЭлементКонтекстногоМеню.Font, FontStyle.Bold);
                                ПунктКонтекстногоМенюПоУмолчаниюУстановлен = true;
                            }
                            else if (МетодФункция.Value.КлавишиВызова != Keys.None)
                            {
                                ЭлементКонтекстногоМеню.ShortcutKeys = МетодФункция.Value.КлавишиВызова;
                                ЭлементКонтекстногоМеню.ShowShortcutKeys = true;
                            }
                            ЭлементКонтекстногоМеню.Click += new EventHandler(ЭлементГлавногоМеню_Click);
                            КоллекцияПоискаПодменю.Add(ЭлементКонтекстногоМеню);
                        }

                        // скрытая функция
                        if (!МетодФункция.Value.ВерхнееМеню && !МетодФункция.Value.КонтекстноеМеню)
                        {
                            this.СкрытыеФункции.Add(МетодФункция.Value.КлавишиВызова, МетодФункция.Key);
                        }
                    }
                    break;
            }

            // даём возможность информационной таблице изменить меню
            this.Таблица.ФормированиеМенюЗавершено(this.menuStrip1, this.contextMenuStrip1);

            // если верхнее меню пустое (), то скрываем его
            if (this.menuStrip1.Items.Count == 2 && !this.Таблица.Поиск)
            {
                this.menuStrip1.Visible = false;
                var y = this.listView1.Location.Y;
                this.listView1.Location = new Point(this.listView1.Location.X, 0);
                this.listView1.Height += y;
            }

            // добавляем в меню "поиск", если нужно
            if (this.Таблица.Поиск)
            {
                ToolStripItem КнопкаПоиска = null;

                // ищем колонку поиска по умолчанию
                // (потом зделать поиск колонки поиска при адаптации колонок)
                foreach (var Свойство in typeof(ТипЗаписи).GetProperties())
                {
                    var АтрибутКолонкиВТаблице = Helper.Attr.BaseMethods.GetOne<КолонкаAttribute>(Свойство);
                    if (АтрибутКолонкиВТаблице != null && АтрибутКолонкиВТаблице.ПоискПоУмолчанию)
                    {
                        var КнопкиМенюПоисков = this.поискToolStripMenuItem.DropDownItems.Cast<ToolStripItem>().Where(x => Свойство.Equals(x.Tag)).Take(1);
                        if (КнопкиМенюПоисков.Count() > 0)
                        {
                            КнопкаПоиска = КнопкиМенюПоисков.First();
                            break;
                        }
                    }
                }

                if (КнопкаПоиска == null)
                    КнопкаПоиска = this.поискToolStripMenuItem.DropDownItems[0];

                // вызываем клик по первому полю поиска
                КнопкаПоиска.PerformClick();
            }
            else
            {
                this.поискToolStripMenuItem.Visible = false;
                this.toolStripTextBox_Поиск.Visible = false;
            }
        }

        /// <summary>
        /// Клик на статусе-ссылке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Надпись_Click(object sender, EventArgs e)
        {
            var Статус = (ToolStripLabel)sender;
            var Тэг = (KeyValuePair<PropertyInfo, string>)Статус.Tag;

            var Форма = new Form_ЗадатьЗначениеСвойства(Тэг.Key, this.Таблица)
            {
                Text = Тэг.Value
            };
            if (Форма.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.ОбновитьСтатусы();
        }

        void Таблица_ПоказанаЗапись(ТипЗаписи obj, bool Показать, bool Выделять)
        {
            if (Показать || Выделять)
            {
                var lvi = this.listView1.Items.Cast<ListViewItem>().Single(x => x.Tag == obj);

                if (Показать)
                    lvi.EnsureVisible();

                if (Выделять)
                    lvi.Selected = true;
            }
        }

        void Таблица_РазрешеноВизуальноеОбновление(bool obj)
        {
            if (!obj)
                this.listView1.BeginUpdate();
            else
                this.listView1.EndUpdate();
        }

        void ПунктМенюПоиска_Click(object sender, EventArgs e)
        {
            var ПунктМеню_Источник = (ToolStripItem)sender;
            this.поискToolStripMenuItem.Text = string.Format("Поиск по '{0}':", ПунктМеню_Источник.Text);
            this.поискToolStripMenuItem.Tag = ПунктМеню_Источник.Tag;
        }

        void Перекрасить(ListViewItem НужнаяЗапись, ТипЗаписи obj)
        {
            switch (obj.Цветность)
            {
                case ЦветностьЭлемента.Нет:
                    break;

                case ЦветностьЭлемента.Сплошная:
                    // получаем интерфейс
                    var ИнтерфейсПолученияЦвета = (СплошнаяЦветность)obj;

                    // задаём цвет
                    //НужнаяЗапись.UseItemStyleForSubItems = true;

                    // НужнаяЗапись.BackColor = ИнтерфейсПолученияЦвета.ПолучитьСплошнойЦвет();

                    var Цвет = ИнтерфейсПолученияЦвета.ПолучитьСплошнойЦвет();
                    НужнаяЗапись.SubItems.ВыполнитьДляКаждого<ListViewItem.ListViewSubItem>(x => x.BackColor = Цвет);

                    break;
            }
        }

        void Таблица_ОбновленаЗапись(ТипЗаписи obj)
        {
            var НужнаяЗапись = this.listView1.Items.Cast<ListViewItem>().Single(x => (ТипЗаписи)x.Tag == obj);

            for (int i = 0; i < this.listView1.Columns.Count; i++)
            {
                Помощник.ОбновитьПодЭлемнтТаблицы(НужнаяЗапись.SubItems[i], this.listView1.Columns[i], obj);
            }
            this.Перекрасить(НужнаяЗапись, obj);
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.ОбновитьСтатусы();
        }

        void Таблица_Обновлено()
        {
            // очистка
            this.listView1.Items.Clear();
            this.listView1.Groups.Clear();

            // применяем фильтр поиска
            IEnumerable<ТипЗаписи> ОтфильтрованныеТовары = null;
            if (this.Таблица.Поиск && !string.IsNullOrWhiteSpace(this.toolStripTextBox_Поиск.Text))
            {
                // Получаем название фильтрованного свойства
                var Свойство = (PropertyInfo)this.поискToolStripMenuItem.Tag;

                // фильтр - поиск подстроки
                ОтфильтрованныеТовары = this.Таблица.Все.Where(x => Помощник.СвойствоКакСтрока(Свойство, x) != null && Помощник.СвойствоКакСтрока(Свойство, x).ToLower().IndexOf(this.toolStripTextBox_Поиск.Text.ToLower()) >= 0);
            }
            else
            {
                ОтфильтрованныеТовары = this.Таблица.Все;
            }

            // сортировка


            // Вывод списка
            this.listView1.BeginUpdate();
            foreach (ТипЗаписи Элемент in ОтфильтрованныеТовары)
            {
                this.ТолькоДобавитьЗапись(Элемент);
            }
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.listView1.EndUpdate();
            this.ОбновитьСтатусы();
        }

        void Таблица_ОбновлёнСтатус()
        {
            this.ОбновитьСтатусы();
        }

        void ЭлементГлавногоМеню_Click(object sender, EventArgs e)
        {
            var МетодФункция = (MethodInfo)((ToolStripItem /*ToolStripMenuItem*/)sender).Tag;
            var Параметры = МетодФункция.GetParameters();
            if (Параметры.Count() == 1)
            {
                // Ищем метод: Метод(ТипЗаписи Запись);
                if (Параметры[0].ParameterType == typeof(ТипЗаписи))
                {
                    if (this.listView1.SelectedItems.Count > 0)
                    {
                        МетодФункция.Invoke(this.Таблица, new object[] { this.listView1.SelectedItems[0].Tag });
                    }
                    return;
                }
                // Ищем метод: Метод(IEnumerable<ТипЗаписи> Записи);
                else if (Параметры[0].ParameterType == typeof(IEnumerable<ТипЗаписи>))
                {
                    if (this.listView1.SelectedItems.Count > 0)
                    {
                        МетодФункция.Invoke(this.Таблица, new object[] { this.listView1.SelectedItems.Cast<ListViewItem>().Select(x => (ТипЗаписи)x.Tag) });
                    }
                    return;
                }
            }

            var r = МетодФункция.Invoke(this.Таблица, null);
            if (r != null && r.GetType() == typeof(ТипЗаписи))
            {
                this.Таблица.ДобавитьЗапись((ТипЗаписи)r);
            }
        }

        void Таблица_ДобавленаЗапись(ТипЗаписи Элемент, bool Показать)
        {
            var item = this.ТолькоДобавитьЗапись(Элемент);

            if (Показать)
            {
                item.EnsureVisible();
            }

            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.ОбновитьСтатусы();
        }

        private ListViewItem ТолькоДобавитьЗапись(ТипЗаписи Элемент)
        {
            var item = Помощник.СвойствоКакЭлемнтТаблицы(this.listView1.Columns[0], Элемент);
            if (Элемент.ВизуальныйЭлемент != null)
                item.Checked = Элемент.ВизуальныйЭлемент.Checked;
            Элемент.ВизуальныйЭлемент = item;

            for (int i = 1; i < this.listView1.Columns.Count; i++)
            {
                item.SubItems.Add(Помощник.СвойствоКакПодЭлемнтТаблицы(this.listView1.Columns[i], Элемент));
            }
            this.Перекрасить(item, Элемент);
            this.listView1.Items.Add(item);

            // выбор группы
            if (Элемент.Группа != null)
            {
                var Группа = this.listView1.Groups.Cast<ListViewGroup>().FirstOrDefault(x => x.Header == Элемент.Группа);
                if (Группа == null)
                {
                    Группа = new ListViewGroup(Элемент.Группа);
                    this.listView1.Groups.Add(Группа);
                }
                item.Group = Группа;
            }

            return item;
        }

        void Таблица_УдаленаЗапись(ТипЗаписи obj)
        {
            this.listView1.Items.Remove(this.listView1.Items.Cast<ListViewItem>().Single(x => x.Tag == obj));
            obj.ВизуальныйЭлемент = null;
            this.ОбновитьСтатусы();
        }

        /// <summary>
        /// Обновляются тексты статуса (название, и значение)
        /// </summary>
        private void ОбновитьСтатусы()
        {
            foreach (ToolStripStatusLabel item in this.statusStrip1.Items)
            {
                var СвойствоСтатус = (KeyValuePair<PropertyInfo, string>)item.Tag;
                item.Text = string.Format("{0}: {1}", СвойствоСтатус.Value, Помощник.СвойствоКакСтрока(СвойствоСтатус.Key, this.Таблица));
            }

            if (this.СтатусОбновлён != null)
                this.СтатусОбновлён(this.Таблица, this.statusStrip1.Items);
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                if (this.ПриВводеЦифр != null)
                {
                    var Конвертр = new KeysConverter();
                    var s = Конвертр.ConvertToString(e.KeyCode);
                    this.ПриВводеЦифр(s);
                }
            }
            else if (e.KeyCode == Keys.Return)
            {
                this.listView1_DoubleClick(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (this.Modal)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                }
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                this.listView1.BeginUpdate();
                this.listView1.Items.Cast<ListViewItem>().ToList()
                    .ForEach(x => x.Selected = true);
                this.listView1.EndUpdate();
            }
            else if (this.СкрытыеФункции.ContainsKey(e.KeyCode))
            {
                this.ЭлементГлавногоМеню_Click(new ToolStripMenuItem
                {
                    Tag = this.СкрытыеФункции[e.KeyCode]
                }, null);
            }
        }

        private void ФормаТаблицы_Load(object sender, EventArgs e)
        {
            if (this.ЗаголовокФормы == null)
                this.Text = this.Таблица.Подпись;
            else
                this.Text = this.ЗаголовокФормы;
        }

        private void toolStripTextBox_Поиск_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.listView1.Select();
                if (this.listView1.SelectedItems.Count == 0)
                    this.listView1.Items[0].Selected = true;
            }
        }

        private void toolStripTextBox_Поиск_TextChanged(object sender, EventArgs e)
        {
            this.Таблица.Обновить();
            if (this.listView1.Items.Count > 0)
                this.listView1.Items[0].Selected = true;
        }

        public void ФормаТаблицы_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ОчиститьВизуальныеСобытия();
        }

        /// <summary>
        /// Отвязывает все События инфТаблицы от формы
        /// </summary>
        public void ОчиститьВизуальныеСобытия()
        {
            this.Таблица.ДобавленаЗапись -= new Action<ТипЗаписи, bool>(Таблица_ДобавленаЗапись);
            this.Таблица.УдаленаЗапись -= new Action<ТипЗаписи>(Таблица_УдаленаЗапись);
            this.Таблица.ОбновлёнСтатус -= new Action(Таблица_ОбновлёнСтатус);
            this.Таблица.Обновлено -= new Action(Таблица_Обновлено);
            this.Таблица.ОбновленаЗапись -= new Action<ТипЗаписи>(Таблица_ОбновленаЗапись);
            this.Таблица.РазрешеноВизуальноеОбновление -= new Action<bool>(Таблица_РазрешеноВизуальноеОбновление);
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (this.DragNDrop_Отправка && e.Button == System.Windows.Forms.MouseButtons.Left)
                this.DoDragDrop(this.listView1.SelectedItems.Cast<ListViewItem>().ToArray(), DragDropEffects.Move);
        }

        private void ФормаТаблицы_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Control)
            {
                this.toolStripTextBox_Поиск.Focus();
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ColumnHeader Колонка;
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;

                    // установка надписи сортировки
                    Колонка = this.listView1.Columns[e.Column];
                    if (Колонка.Tag != null && Колонка.Tag.GetType().BaseType == typeof(PropertyInfo))
                    {
                        string НазваниеКолонки = ПолучитьНазваниеКолонки((PropertyInfo)Колонка.Tag);
                        Колонка.Text = НазваниеКолонки + " ^";
                    }
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;

                    // установка надписи сортировки
                    Колонка = this.listView1.Columns[e.Column];
                    if (Колонка.Tag != null && Колонка.Tag.GetType().BaseType == typeof(PropertyInfo))
                    {
                        string НазваниеКолонки = ПолучитьНазваниеКолонки((PropertyInfo)Колонка.Tag);
                        Колонка.Text = НазваниеКолонки + " v";
                    }
                }
            }
            else
            {
                // возврат надписи сортировки к обычному виду                
                if (lvwColumnSorter.SortColumn >= 0)
                {
                    Колонка = this.listView1.Columns[lvwColumnSorter.SortColumn];
                    if (Колонка.Tag != null && Колонка.Tag.GetType().BaseType == typeof(PropertyInfo))
                    {
                        Колонка.Text = ПолучитьНазваниеКолонки((PropertyInfo)Колонка.Tag);
                    }
                }

                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Descending;

                // установка надписи сортировки
                Колонка = this.listView1.Columns[e.Column];
                if (Колонка.Tag != null && Колонка.Tag.GetType().BaseType == typeof(PropertyInfo))
                {
                    string НазваниеКолонки = ПолучитьНазваниеКолонки((PropertyInfo)Колонка.Tag);
                    Колонка.Text = НазваниеКолонки + " ^";
                }
            }
            if (this.listView1.ShowGroups/* && Метаинструмент.ОперационнаяСистема.Текущая.Тип == ТипОперационнойСистемы.WindowsXP*/)
            //if(true)
            {
                this.listView1.Sorting = SortOrder.None;
                this.listView1.ListViewItemSorter = null;

                // получаем колонку
                var КолонкаСвойства = this.listView1.Columns[this.lvwColumnSorter.SortColumn];
                var Свойство = (PropertyInfo)КолонкаСвойства.Tag;

                // получаем сортировщик
                var Сортировщик = this.lvwColumnSorter.Сравниватели[this.lvwColumnSorter.SortColumn];

                // сортируем                
                object Значение1, Значение2;

                this.listView1.BeginUpdate();
                if (lvwColumnSorter.Order == SortOrder.Descending)
                {
                    foreach (ListViewGroup Группа in this.listView1.Groups)
                    {
                        int КоличествоСтрок = Группа.Items.Count;
                        for (int i = 0; i < КоличествоСтрок; i++)
                        {
                            ListViewItem СамыйБольшойЭлемент = Группа.Items[0];
                            for (int j = 1; j < КоличествоСтрок - i; j++)
                            {
                                ListViewItem ЭлементОбъекта2 = Группа.Items[j];

                                Значение1 = Свойство.GetValue(СамыйБольшойЭлемент.Tag, null);
                                Значение2 = Свойство.GetValue(ЭлементОбъекта2.Tag, null);

                                if (Сортировщик.Compare(Значение2, Значение1) > 0)
                                {
                                    СамыйБольшойЭлемент = ЭлементОбъекта2;
                                }
                            }

                            // перемещаем в конец
                            СамыйБольшойЭлемент.Remove();

                            var r = new ListViewItem(СамыйБольшойЭлемент.Text)
                            {
                                Group = Группа,
                                Tag = СамыйБольшойЭлемент.Tag
                            };
                            r.SubItems.AddRange(СамыйБольшойЭлемент.SubItems.Cast<ListViewItem.ListViewSubItem>().Skip(1).Select(x => x.Text).ToArray());
                            this.listView1.Items.Add(r);
                        }
                    }
                }
                else //if SortOrder.Ascending
                {
                    foreach (ListViewGroup Группа in this.listView1.Groups)
                    {
                        int КоличествоСтрок = Группа.Items.Count;
                        for (int i = 0; i < КоличествоСтрок; i++)
                        {
                            ListViewItem СамыйБольшойЭлемент = Группа.Items[0];
                            for (int j = 1; j < КоличествоСтрок - i; j++)
                            {
                                ListViewItem ЭлементОбъекта2 = Группа.Items[j];

                                Значение1 = Свойство.GetValue(СамыйБольшойЭлемент.Tag, null);
                                Значение2 = Свойство.GetValue(ЭлементОбъекта2.Tag, null);

                                if (Сортировщик.Compare(Значение2, Значение1) < 0)
                                {
                                    СамыйБольшойЭлемент = ЭлементОбъекта2;
                                }
                            }

                            // перемещаем в конец
                            СамыйБольшойЭлемент.Remove();

                            var r = new ListViewItem(СамыйБольшойЭлемент.Text)
                            {
                                Group = Группа,
                                Tag = СамыйБольшойЭлемент.Tag
                            };
                            r.SubItems.AddRange(СамыйБольшойЭлемент.SubItems.Cast<ListViewItem.ListViewSubItem>().Skip(1).Select(x => x.Text).ToArray());
                            this.listView1.Items.Add(r);
                        }
                    }
                }
                this.listView1.EndUpdate();
            }
            else
                this.listView1.Sort();
            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            switch (this.ОтображениеФормы)
            {
                case ВидОтображениеФормы.ВыборНескольких:
                    // ничего не делаем
                    break;

                case ВидОтображениеФормы.Выбор:
                    if (this.ВыбраныйЭлемент != null)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    break;

                default:
                    var ДействияПоУмолчанию = this.contextMenuStrip1.Items.Cast<ToolStripMenuItem>().Where(x => x.Font.Bold).Take(1);
                    var Количество = ДействияПоУмолчанию.Count();
                    if (Количество == 1)
                        ДействияПоУмолчанию.Single().PerformClick();
                    else if (Количество > 1)
                        throw new Exception(string.Format("Обнаружено {0} действий по умолчанию", Количество));
                    break;
            }
        }

        /// <summary>
        /// Выбрать несколько
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ЭлементВерхнегоМеню_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); // ?
        }

        void ОтметитьВсе_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Cast<ListViewItem>().ToList()
                .ForEach(x => x.Checked = true);
        }

        void СнятьВсеОтметки_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Cast<ListViewItem>().ToList()
                .ForEach(x => x.Checked = false);
        }

        void ИнвертироватьОтметки_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Cast<ListViewItem>().ToList()
                .ForEach(x => x.Checked = !x.Checked);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var Запись = (ТипЗаписи)e.Item.Tag;
            this.Таблица.ОтметкаИзменена(Запись);
        }
    }
}
