using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using TransOS.Plugin.Helper.InformationTable.View.Comparer;

namespace TransOS.Plugin.Helper.InformationTable.View
{
    /// <summary>
    /// Сортировка по колонкам в ListView
    /// </summary>
    internal class СортировщикКолонок<ТипЭлемента> : IComparer
        where ТипЭлемента : BaseElement<ТипЭлемента>
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;

        #region Сравниватели

        private Сравниватель_Byte Сравниватель_Byte;
        private Сравниватель_SByte Сравниватель_SByte;
        private Сравниватель_Int16 Сравниватель_Int16;
        private Сравниватель_UInt16 Сравниватель_UInt16;
        private Сравниватель_Int32 Сравниватель_Int32;
        private Сравниватель_UInt32 Сравниватель_UInt32;
        private Сравниватель_Int64 Сравниватель_Int64;
        private Сравниватель_UInt64 Сравниватель_UInt64;
        private Сравниватель_Decimal Сравниватель_Decimal;

        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();

        #endregion

        readonly PropertyInfo[] ОтображаемыеСвойства;
        public readonly IComparer[] Сравниватели;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public СортировщикКолонок(PropertyInfo[] ОтображаемыеСвойства)
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            this.ОтображаемыеСвойства = ОтображаемыеСвойства;

            // создаём сравниватели для типа
            this.Сравниватели = new IComparer[ОтображаемыеСвойства.Count()];
            int i = 0;
            foreach (var Свойство in ОтображаемыеСвойства)
            {
                var rt = Свойство.GetGetMethod().ReturnType;
                if (rt == typeof(byte))
                {
                    if (this.Сравниватель_Byte == null)
                        this.Сравниватель_Byte = new Сравниватель_Byte();
                    Сравниватели[i] = this.Сравниватель_Byte;
                }
                else if (rt == typeof(sbyte))
                {
                    if (this.Сравниватель_SByte == null)
                        this.Сравниватель_SByte = new Сравниватель_SByte();
                    Сравниватели[i] = this.Сравниватель_SByte;
                }
                else if (rt == typeof(short))
                {
                    if (this.Сравниватель_Int16 == null)
                        this.Сравниватель_Int16 = new Сравниватель_Int16();
                    Сравниватели[i] = this.Сравниватель_Int16;
                }
                else if (rt == typeof(ushort))
                {
                    if (this.Сравниватель_UInt16 == null)
                        this.Сравниватель_UInt16 = new Сравниватель_UInt16();
                    Сравниватели[i] = this.Сравниватель_UInt16;
                }
                else if (rt == typeof(int))
                {
                    if (this.Сравниватель_Int32 == null)
                        this.Сравниватель_Int32 = new Сравниватель_Int32();
                    Сравниватели[i] = this.Сравниватель_Int32;
                }
                else if (rt == typeof(uint))
                {
                    if (this.Сравниватель_UInt32 == null)
                        this.Сравниватель_UInt32 = new Сравниватель_UInt32();
                    Сравниватели[i] = this.Сравниватель_UInt32;
                }
                else if (rt == typeof(long))
                {
                    if (this.Сравниватель_Int64 == null)
                        this.Сравниватель_Int64 = new Сравниватель_Int64();
                    Сравниватели[i] = this.Сравниватель_Int64;
                }
                else if (rt == typeof(ulong))
                {
                    if (this.Сравниватель_UInt64 == null)
                        this.Сравниватель_UInt64 = new Сравниватель_UInt64();
                    Сравниватели[i] = this.Сравниватель_UInt64;
                }
                else if (rt == typeof(decimal))
                {
                    if (this.Сравниватель_Decimal == null)
                        this.Сравниватель_Decimal = new Сравниватель_Decimal();
                    Сравниватели[i] = this.Сравниватель_Decimal;
                }
                else if (rt == typeof(string))
                {
                    Сравниватели[i] = this.ObjectCompare;
                }
                i++;
            }
        }

        public СортировщикКолонок(Type ТипЭлемента)
            : this(Помощник.ПолучитьОтображаемыеСвойства(ТипЭлемента))
        {

        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult = 0;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // получаем сравниватель
            var ТекущийСравниватель = this.Сравниватели[ColumnToSort];

            if (ТекущийСравниватель != null)
            {
                // получаем значения свойств из элемента инф таблицы
                var Значение_X = this.ОтображаемыеСвойства[ColumnToSort].GetValue(listviewX.Tag, null);
                var Значение_Y = this.ОтображаемыеСвойства[ColumnToSort].GetValue(listviewY.Tag, null);

                // сравниваем
                compareResult = ТекущийСравниватель.Compare(Значение_X, Значение_Y);
            }
            else
            {
                // сранвиваем текстовые выражения
                compareResult = this.ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
            }

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }

    }
}
