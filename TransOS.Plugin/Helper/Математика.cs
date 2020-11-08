using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper
{
    public static class Математика
    {
        public static int ВозвестиВСтепень(int Число, uint Степень)
        {
            ВозвестиВСтепень(ref Число, Степень);
            return Число;
        }

        public static void ВозвестиВСтепень(ref int Число, uint Степень)
        {
            if (Степень == 0)
                Число = 0;
            else
            {
                var ЧислоОрегинал = Число;

                for (uint i = 1; i < Степень; i++)
                {
                    Число *= ЧислоОрегинал;
                }
            }
        }

        public static uint ОкруглитьКБольшему(uint ОкругляемоеЧисло, uint КЧемуОкруглять)
        {
            uint ЦелаяЧасть = ОкругляемоеЧисло / КЧемуОкруглять;
            uint ДробнаяЧасть = ОкругляемоеЧисло % КЧемуОкруглять;
            if (ДробнаяЧасть > 0)
                ЦелаяЧасть++;
            return ЦелаяЧасть * КЧемуОкруглять;
        }
    }
}
