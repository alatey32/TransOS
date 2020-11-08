using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.InformationTable.Attr
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class СтатусAttribute : Attribute
    {
        public readonly string Подпись;
        public readonly bool Изменяемое;
        public readonly uint ЧислоЗнаковПослеКомы;

        public СтатусAttribute(string Подпись = "", bool Изменяемое = false, uint ЧислоЗнаковПослеКомы = 2)
        {
            this.Подпись = Подпись;
            this.ЧислоЗнаковПослеКомы = ЧислоЗнаковПослеКомы;
        }
    }
}
