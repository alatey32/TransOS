using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TransOS.Plugin.Helper.InformationTable.View.Comparer
{
    internal class Сравниватель_Decimal : IComparer
    {
        public int Compare(object x, object y)
        {
            decimal r = (decimal)x;
            r -= (decimal)y;

            if (r == 0)
                return 0;
            if (r > 0)
                return 1;
            return -1;
        }
    }
}
