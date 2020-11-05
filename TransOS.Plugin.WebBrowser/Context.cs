using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Plugin.WebBrowser
{
    public class Context
    {
        public Context()
        {

        }

        public Control GetView(IDocument WebDocument, int ControlWidth)
        {
            var Forma = new Form_Browser(WebDocument, ControlWidth);
            return Forma.panel1;
        }
    }
}
