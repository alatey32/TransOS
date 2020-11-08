using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper
{
    public class МассивБайтов
    {
        #region ststic
        public static bool Идентичны(byte[] Массив1, byte[] Массив2)
        {
            if (Массив1 == null)
            {
                if (Массив2 == null)
                    return true;
                return false;
            }

            if (Массив2 == null)
            {
                return false;
            }

            if (Массив1.Count() == Массив2.Count())
            {
                var l = Массив1.Count();
                for (int i = 0; i < l; i++)
                {
                    if (Массив1[i] != Массив2[i])
                        return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Конвертировать строку 16-ричных значений с дефисом вида "44-AC-..." в массив байтов
        /// </summary>
        /// <param name="hex">Строка 16-ричных значений с дефисом вида "44-AC-..."</param>
        /// <returns>Массив байтов</returns>
        public static byte[] КонвертироватьХексСтрокуВМассивБайтов(string hex)
        {
            return hex.Split('-')
                .Select(x => Convert.ToByte(x, 16))
                .ToArray();
        }

        public static int ИндексПодмножества(byte[] Массив, byte[] Подмножество)
        {
            if (Массив.Length >= Подмножество.Length)
            {
                int МаксимальныйНачальныйИндексПоиска = Массив.Length - Подмножество.Length + 1;
                for (int i = 0; i < МаксимальныйНачальныйИндексПоиска; i++)
                {
                    int j = 0;
                    for (; j < Подмножество.Length; j++)
                    {
                        if (Массив[i + j] != Подмножество[j])
                            break;
                    }

                    if (j == Подмножество.Length)
                        return i;
                }
            }
            return -1;
        }

        public static int ИндексПодмножества(List<byte> Массив, byte[] Подмножество)
        {
            if (Массив.Count >= Подмножество.Length)
            {
                int МаксимальныйНачальныйИндексПоиска = Массив.Count - Подмножество.Length + 1;
                for (int i = 0; i < МаксимальныйНачальныйИндексПоиска; i++)
                {
                    int j = 0;
                    for (; j < Подмножество.Length; j++)
                    {
                        if (Массив[i + j] != Подмножество[j])
                            break;
                    }

                    if (j == Подмножество.Length)
                        return i;
                }
            }
            return -1;
        }

        public static int ИндексБайта(byte[] Массив, byte Байт)
        {
            for (int i = 0; i < Массив.Length; i++)
            {
                if (Массив[i] == Байт)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Конвертировать массив байтов в строку 16-ричных значений с дефисом вида "44-AC-..."
        /// </summary>
        /// <param name="Массив">Массив байтов</param>
        /// <returns>Строка 16-ричных значений с дефисом вида "44-AC-..."</returns>
        public static string ВСтроку(byte[] Массив)
        {
            if (Массив == null)
                return null;
            return BitConverter.ToString(Массив);
        }

        /// <summary>
        /// Убирает первые нулевые байты массива. Но если есть только один нулевой элемент, он оставляется
        /// </summary>
        /// <param name="Массив"></param>
        /// <returns></returns>
        public static byte[] УбратьПервыеНули(byte[] Массив)
        {
            if (Массив == null)
                return null;

            var Результат = Массив.SkipWhile(x => x == 0).ToArray();
            if (Результат.Length == 0)
                return new byte[] { 0x00 };
            return Результат;
        }

        public static void Копировать(byte[] В, int ИндексВ, byte[] Из, int ИндексИз, int Количество)
        {
            for (int i = 0; i < Количество; i++)
            {
                В[ИндексВ + i] = Из[ИндексИз + i];
            }
        }

        #endregion

        /// <summary>
        /// Список всех байтов
        /// </summary>
        public readonly List<byte> Байты = new List<byte>();

        public void Добавить(byte ОдинБайт)
        {
            this.Байты.Add(ОдинБайт);
        }

        public void Добавить(IEnumerable<byte> МногоБайт, int Смещение, int Количество)
        {
            this.Байты.AddRange(МногоБайт.Skip(Смещение).Take(Количество));
        }

        public int ИндексБайта(byte Байт)
        {
            return this.Байты.IndexOf(Байт);
        }

        public void Очистить()
        {
            this.Байты.Clear();
        }

        public byte[] ВМассив(int Количество)
        {
            return this.Байты.Take(Количество).ToArray();
        }
    }
}
