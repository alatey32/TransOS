using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TransOS.Plugin.Helper.InformationTable.View.Comparer
{
    internal class Сравниватель_UInt32 : IComparer
    {
        public int Compare(object x, object y)
        {
            return Convert.ToInt32((uint)x - (uint)y);
        }
    }
}
