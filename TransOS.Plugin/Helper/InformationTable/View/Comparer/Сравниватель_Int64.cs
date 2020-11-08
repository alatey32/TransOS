using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TransOS.Plugin.Helper.InformationTable.View.Comparer
{
    internal class Сравниватель_Int64 : IComparer
    {
        public int Compare(object x, object y)
        {
            long lx = (long)x;
            long ly = (long)y;

            if (lx > ly)
                return 1;
            if (lx < ly)
                return -1;
            return 0;
        }
    }
}
