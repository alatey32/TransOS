using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Gui.DevTools
{
    public partial class Form_QueryToCodeQuery : Form
    {
        public Form_QueryToCodeQuery()
        {
            InitializeComponent();
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Paste
            if (string.IsNullOrWhiteSpace(this.textBox_In.Text))
            {
                if (Clipboard.ContainsText())
                    this.textBox_In.Text = Clipboard.GetText();
            }

            // convert
                string OutText = null;
            if(!string.IsNullOrWhiteSpace(this.textBox_In.Text))
            {
                OutText = this.textBox_In.Text.Trim();

                OutText = OutText.Replace("\r", "");
                OutText = OutText.Replace("\n", "");
                OutText = OutText.Replace("\t", " ");
                OutText = OutText.Replace('"', '\'');
            }
            this.textBox_Out.Text = OutText;

            // Copy
            if (!string.IsNullOrWhiteSpace(this.textBox_Out.Text))
            {
                Clipboard.SetText(this.textBox_Out.Text);
            }
        }
    }
}
