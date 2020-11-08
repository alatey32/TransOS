using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.InformationTable.Interface
{
    /// <summary>
    /// Предназначен для тех объектов, которые можно скопировать и установить из копии
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ЯКопирабельный<T>
    {
        /// <summary>
        /// Если элемент копируется, то здесь можно сохранить орегинал
        /// </summary>
        public virtual T ОрегиналКопии { get; protected set; }

        public virtual T Копия()
        {
            throw new NotImplementedException("Метод 'Копия' не реализован");
        }

        public virtual void УстановитьИз(T Значение)
        {
            throw new NotImplementedException("Метод 'УстановитьИз' не реализован");
        }
    }
}
