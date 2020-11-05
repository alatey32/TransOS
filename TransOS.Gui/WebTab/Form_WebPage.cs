using AngleSharp.Dom;
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

        readonly TabPage tabPage;

        public Form_WebPage(Engine Engine, TabPage tabPage)
        {
            InitializeComponent();

            this.Engine = Engine;
            this.tabPage = tabPage;
        }

        private async void button_Go_Click(object sender, EventArgs e)
        {
            IDocument NewDocument = null;            
            this.panel_WebContent.Controls.Clear();

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
                    this.button_Go.Enabled = false;
                    this.button_Go.Text = "Loading...";

                    // Get content async
                    var NewContent = await this.Engine.WebTab.GetContentAsync(Url);

                    // Parsing html
                    NewDocument = await this.Engine.WebTab.ParseHtml(NewContent);

                    string PageTitle = NewDocument.Title;
                    if (!string.IsNullOrWhiteSpace(PageTitle))
                        this.tabPage.Text = PageTitle;
                    else
                        this.tabPage.Text = "Tab";

                    // enable interface                    
                    this.button_Go.Enabled = true;
                    this.button_Go.Text = "Go!";
                }
            }

            if (NewDocument != null)
            {
                Control NewControl = this.Engine.WebBrowser.GetView(NewDocument, this.panel_WebContent.Width);
                if (NewControl != null)
                    this.panel_WebContent.Controls.Add(NewControl);
            }
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
