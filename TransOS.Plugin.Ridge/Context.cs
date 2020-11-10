using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Ridge
{
    public class Context
    {
        internal readonly IContext Os;

        public Context(IContext Os)
        {
            this.Os = Os;
        }

        public void OpenForm(IRidgeObject RidgeRoot)
        {
            var forma = new Form1(Os, RidgeRoot)
            {
                //MinimizeBox = false
            };
            forma.Show();
        }
    }
}
