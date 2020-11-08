using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Plugin.Helper.InformationTable.Attr
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class ФункцияAttribute : Attribute
    {
        public readonly bool ВерхнееМеню;
        public readonly bool КонтекстноеМеню;

        /// <summary>
        /// Вызывается при двойном счелчке или Enter'е
        /// </summary>
        public readonly bool ПоУмолчанию;
        public readonly Keys КлавишиВызова;

        public readonly string Подпись;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ВерхнееМеню"></param>
        /// <param name="КонтекстноеМеню"></param>
        /// <param name="ПоУмолчанию">Вызывается при двойном счелчке или Enter'е</param>
        /// <param name="КлавишиВызова">Клавиша вызова будет работать только тогда, когда функция разрешена в контекстном меню</param>
        public ФункцияAttribute(
            bool ВерхнееМеню = true,
            bool КонтекстноеМеню = true,
            bool ПоУмолчанию = false,
            Keys КлавишиВызова = Keys.None,
            string Подпись = "")
        {
            this.ВерхнееМеню = ВерхнееМеню;
            this.КонтекстноеМеню = КонтекстноеМеню;
            this.ПоУмолчанию = ПоУмолчанию;
            this.КлавишиВызова = КлавишиВызова;
            this.Подпись = Подпись;
        }
    }
}
