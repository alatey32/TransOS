using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.InformationTable.View
{
    public enum ВидОтображениеФормы
    {
        /// <summary>
        /// Форма не отображается
        /// </summary>
        Нет,

        /// <summary>
        /// Форма отображеня для просмотра и редактирования
        /// </summary>
        Показ,

        /// <summary>
        /// В форме пользователь выбирает товар (для отчёта или другой формы)
        /// </summary>
        Выбор,

        /// <summary>
        /// Пользователь отмечает галочками выбранные пункты
        /// </summary>
        ВыборНескольких
    }
}
