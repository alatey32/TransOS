using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Helper;

namespace TransOS.Plugin.Ridge
{
    public class RidgeList : CustomList<IRidgeObject>
    {
        /// <summary>
        /// Added element
        /// </summary>
        public event Action<IRidgeObject> Added;

        /// <summary>
        /// Removed element
        /// </summary>
        public event Action<IRidgeObject> Removed;

        private readonly IRidgeObject ObjectAsParent;

        public RidgeList(IRidgeObject ObjectAsParent)
        {
            this.ObjectAsParent = ObjectAsParent;
        }

        public RidgeList(IRidgeObject ObjectAsParent, Func<IEnumerable<IRidgeObject>> GetElementMethod) : base(GetElementMethod)
        {
            this.ObjectAsParent = ObjectAsParent;
        }

        public override void Add(IRidgeObject item)
        {
            item.Parent = this.ObjectAsParent;
            base.Add(item);

            this.Added?.Invoke(item);
        }

        public override bool Remove(IRidgeObject item)
        {
            item.Parent = null;
            bool result = base.Remove(item);

            if (result)
                this.Removed?.Invoke(item);

            return result;
        }
    }
}
