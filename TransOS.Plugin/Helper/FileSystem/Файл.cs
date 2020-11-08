using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.FileSystem
{
    public static class Файл
    {
        public static long Размер(string ПутьКФайлу)
        {
            return new FileInfo(ПутьКФайлу).Length;
        }

        public static string РазмерКакСтрока(ulong Размер)
        {
            string Шаблон = "{0} байт";
            if (Размер < 1024)
            {
            }
            else if (Размер < (1024UL * 1024UL))
            {
                Размер /= 1024UL;
                Шаблон = "{0} Кбайт";
            }
            else if (Размер < (1024UL * 1024UL * 1024UL))
            {
                Размер /= (1024L * 1024L);
                Шаблон = "{0} Мбайт";
            }
            else// if (Размер < (1024L * 1024L * 1024L * 1024L))
            {
                Размер /= (1024L * 1024L * 1024L);
                Шаблон = "{0} Гбайт";
            }
            return string.Format(Шаблон, Размер);
        }

        public static string РазмерКакСтрока(long Размер)
        {
            if (Размер >= 0)
                return РазмерКакСтрока(Convert.ToUInt64(Размер));
            return null;
        }

        public static string РазмерКакСтрока(FileInfo ИнформацияОФайле)
        {
            return РазмерКакСтрока(ИнформацияОФайле.Length);
        }

        public static string РазмерКакСтрока_ИзФайла(string ПутьКФайлу)
        {
            return РазмерКакСтрока(new FileInfo(ПутьКФайлу));
        }
    }
}
