using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Gui.WebTab
{
    public partial class Form_WebPage : Form
    {
        readonly Engine Engine;

        public Form_WebPage(Engine Engine)
        {
            InitializeComponent();

            this.Engine = Engine;
        }

        private async void button_Go_Click(object sender, EventArgs e)
        {
            string NewContent = null;

            if (!string.IsNullOrWhiteSpace(this.textBox_Address.Text))
            {
                // try parse URL
                Uri Url = null;
                try
                {
                    Url = new Uri(this.textBox_Address.Text);
                }
                catch(UriFormatException)
                {
                    MessageBox.Show("Incorrect page address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Url = null;
                }

                // Getting content
                if (Url != null)
                {
                    // disable interface
                    this.textBox_PageContent.Enabled = false;
                    this.button_Go.Enabled = false;
                    this.button_Go.Text = "Loading...";

                    // Get content async
                    NewContent = await this.Engine.WebTab.GetContentAsync(Url);

                    // enable interface
                    this.textBox_PageContent.Enabled = true;
                    this.button_Go.Enabled = true;
                    this.button_Go.Text = "Go!";
                }
            }

            this.textBox_PageContent.Text = NewContent;
        }

        private void textBox_Address_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.button_Go_Click(sender, e);
            }
        }
    }
}
