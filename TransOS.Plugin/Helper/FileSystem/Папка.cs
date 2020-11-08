using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.FileSystem
{
    public static class Папка
    {
        /// <summary>
        /// Удаляет все файлы и папки в заданной папке (которая не удаляется)
        /// </summary>
        public static void УдалитьСодержимое(string Папка)
        {
            // проверка на существование
            if (!Directory.Exists(Папка))
                throw new DirectoryNotFoundException($"Папка '{Папка}' не существует");

            // удаляем подпапки
            foreach (var Подпапка in Directory.EnumerateDirectories(Папка))
            {
                Directory.Delete(Подпапка, true);
            }

            // удаляем файлы
            foreach (var Файл in Directory.EnumerateFiles(Папка))
            {
                File.Delete(Файл);
            }
        }

        /// <summary>
        /// Используется Windows Shell
        /// </summary>
        /// <param name="ЦелеваяПапка">Существующая</param>
        public static void Копировать(string ЦелеваяПапка, string ПапкаИсточник, bool ПодтвердитьВсё = false)
        {
            /* В разных версиях Windows (XPx32 и 7x64) разные интерфейсы (GUID),
             * потому такой сложный код
             */

            var Тип = Type.GetTypeFromProgID("shell.application", true);
            var shell = Activator.CreateInstance(Тип);

            var ЦелеваяПапкаКакОболочка = shell.GetType().InvokeMember("NameSpace", BindingFlags.InvokeMethod, null, shell, new object[] { ЦелеваяПапка });
            var ПапкаИсточникКакОболочка = shell.GetType().InvokeMember("NameSpace", BindingFlags.InvokeMethod, null, shell, new object[] { ПапкаИсточник });

            var Агргументы = new List<object>
            {
                ПапкаИсточникКакОболочка
            };
            if (ПодтвердитьВсё)
                Агргументы.Add(FILEOP_FLAGS.FOF_NOCONFIRMATION);
            ЦелеваяПапкаКакОболочка.GetType().InvokeMember("CopyHere", BindingFlags.InvokeMethod, null, ЦелеваяПапкаКакОболочка, Агргументы.ToArray());

            /* var shell = new Shell32.Shell();
            var ЦелеваяПапкаКакОболочка = shell.NameSpace(ЦелеваяПапка);
            var ПапкаИсточникКакОболочка = shell.NameSpace(ПапкаИсточник);*/

            // проверка
            /*if (ЦелеваяПапкаКакОболочка == null)
                throw new DirectoryNotFoundException(string.Format("Целевая папка '{0}' не найдена", ЦелеваяПапка));
            if (ПапкаИсточникКакОболочка == null)
                throw new DirectoryNotFoundException(string.Format("Папка источник '{0}' не найдена", ПапкаИсточник));

            // FILEOP_FLAGS - флаги

            ЦелеваяПапкаКакОболочка.CopyHere(ПапкаИсточникКакОболочка);*/
        }



        /// <summary>
        /// Используется Windows Shell
        /// </summary>
        /// <param name="ЦелеваяПапка">Существующая</param>
        /// <param name="ПапкаИсточник">Существующая</param>
        public static void КопироватьСодержимое(string ЦелеваяПапка, string ПапкаИсточник, bool ОчиститьЦелевуюПапку = true, bool ЗаменитьСуществующие = true, bool ВсеПодпапки = true)
        {
            SearchOption Опция = ВсеПодпапки ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            if (ОчиститьЦелевуюПапку)
                УдалитьСодержимое(ЦелеваяПапка);

            //Создать идентичное дерево каталогов
            foreach (string dirPath in Directory.GetDirectories(ПапкаИсточник, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(ПапкаИсточник, ЦелеваяПапка));

            //Скопировать все файлы. И перезаписать(если такие существуют)
            foreach (string newPath in Directory.GetFiles(ПапкаИсточник, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(ПапкаИсточник, ЦелеваяПапка), true);
        }

        /// <summary>
        /// Создаёт путь к файлу если он не существует (папка)
        /// </summary>
        /// <param name="ПутьКФайлу"></param>
        public static void СоздатьПутьКФайлу(string ПутьКФайлу)
        {
            var ПутьКПапке = Path.GetDirectoryName(ПутьКФайлу);
            Создать(ПутьКПапке);
        }

        /// <summary>
        /// Создаёт путь папки если он не существует
        /// </summary>
        /// <returns>False - папка существовала</returns>
        public static bool Создать(string ПутьПапки)
        {
            if (!Directory.Exists(ПутьПапки))
            {
                var ЧастиПути = ПутьПапки.Split('\\');
                for (int i = 2; i < (ЧастиПути.Count() + 1); i++)
                {
                    string Путь = string.Join("\\", ЧастиПути.Take(i));
                    if (!Directory.Exists(Путь))
                    {
                        Directory.CreateDirectory(Путь);
                    }
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Открыть папку в проводнике
        /// </summary>
        /// <param name="Путь"></param>
        public static void Открыть(string Путь)
        {
            // статически
            /*var shell = new Shell32.Shell();
            shell.Open(Путь);*/

            // динамически
            var Тип = Type.GetTypeFromProgID("shell.application", true);
            var shell = Activator.CreateInstance(Тип);
            shell.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null, shell, new object[] { Путь });
        }

        /// <summary>
        /// Получить размер папки в байтах
        /// </summary>
        /// <param name="Путь"></param>
        /// <returns></returns>
        public static long Размер(string Путь)
        {
            long РазмерПапки = 0;
            foreach (string ПутьФайла in Directory.EnumerateFiles(Путь, "*", SearchOption.AllDirectories)
                .Where(x => !Directory.Exists(x)))
            {
                var fi = new FileInfo(ПутьФайла);
                РазмерПапки += fi.Length;
            }
            return РазмерПапки;
        }

        public static string РазмерКакСтрока(string Путь)
        {
            return Файл.РазмерКакСтрока(Размер(Путь));
        }

    }
}
