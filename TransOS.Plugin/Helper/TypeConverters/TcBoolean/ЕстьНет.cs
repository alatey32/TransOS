using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.TypeConverters.TcBoolean
{
    public class ЕстьНет : БазовыйКонвертр
    {
        public ЕстьНет()
            : base("Есть", "Нет")
        {
        }
    }
}
