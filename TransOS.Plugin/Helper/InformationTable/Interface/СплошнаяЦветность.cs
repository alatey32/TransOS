using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.InformationTable.Interface
{
    public interface СплошнаяЦветность
    {
        /// <summary>
        /// Получить фоновый цвет всей записи
        /// </summary>
        /// <returns>Фоновый цвет всей записи</returns>
        Color ПолучитьСплошнойЦвет();
    }
}
