using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TransOS.Plugin.Helper.InformationTable.View.Comparer
{
    internal class Сравниватель_UInt64 : IComparer
    {
        public int Compare(object x, object y)
        {
            ulong ulx = (ulong)x;
            ulong uly = (ulong)y;

            if (ulx > uly)
                return 1;
            if (ulx < uly)
                return -1;
            return 0;
        }
    }
}
