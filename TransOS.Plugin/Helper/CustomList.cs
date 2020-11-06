using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper
{
    public class CustomList<ElementType> : IList<ElementType>
    {
        protected readonly List<ElementType> Элементы = new List<ElementType>();

        private readonly Func<IEnumerable<ElementType>> GetElementMethod = null;

        public CustomList()
        {
            this.IsReadOnly = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GetElements">Method that dynamically gets all elements</param>
        public CustomList(Func<IEnumerable<ElementType>> GetElementMethod)
        {
            this.GetElementMethod = GetElementMethod;
            this.IsReadOnly = true;
        }
                
        public void ReloadElements()
        {
            if (this.GetElementMethod != null)
            {
                this.Элементы.Clear();
                this.Элементы.AddRange(this.GetElementMethod());
            }
        }

        #region IEnumerable<ElementType>

        IEnumerator IEnumerable.GetEnumerator()
        {
            this.ReloadElements();
            return this.Элементы.GetEnumerator();
        }

        public IEnumerator<ElementType> GetEnumerator()
        {
            this.ReloadElements();
            return this.Элементы.GetEnumerator();
        }

        #endregion

        #region ICollection<ElementType>

        public int Count { get { return this.Элементы.Count; } }
        public bool IsReadOnly { get; protected set; }

        public virtual void Add(ElementType item)
        {
            this.Элементы.Add(item);
        }

        public virtual void Clear()
        {
            this.Элементы.Clear();
        }

        public bool Contains(ElementType item)
        {
            return this.Элементы.Contains(item);
        }

        public void CopyTo(ElementType[] array, int arrayIndex)
        {
            this.ReloadElements();
            this.Элементы.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(ElementType item)
        {
            return this.Элементы.Remove(item);
        }

        #endregion

        #region IList<ElementType>

        public ElementType this[int index]
        {
            get
            {
                this.ReloadElements();
                return this.Элементы[index];
            }
            set { this.Элементы[index] = value; }
        }

        public int IndexOf(ElementType item)
        {
            this.ReloadElements();
            return this.Элементы.IndexOf(item);
        }

        public virtual void Insert(int index, ElementType item)
        {
            this.Элементы.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            this.Remove(this.Элементы[index]);
        }

        #endregion
    }
}
