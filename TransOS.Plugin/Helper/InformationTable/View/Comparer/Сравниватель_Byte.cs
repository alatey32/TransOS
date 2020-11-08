using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TransOS.Plugin.Helper.InformationTable.View.Comparer
{
    internal class Сравниватель_Byte : IComparer
    {
        public int Compare(object x, object y)
        {
            return (byte)x - (byte)y;
        }
    }
}
