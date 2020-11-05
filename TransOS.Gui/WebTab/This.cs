using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Gui.WebTab
{
    /// <summary>
    /// Web tabs controller
    /// </summary>
    public class This
    {
        private Engine Engine;

        internal This(Engine Engine)
        {
            this.Engine = Engine;
        }

        /// <summary>
        /// Add new tab
        /// </summary>
        public void Add()
        {
            var NewTab = new TabPage("New tab");
            var NewPageForm = new Form_WebPage(this.Engine, NewTab);
            NewTab.Controls.Add(NewPageForm.panel1);

            this.Engine.Mainform.tabControl_WebTabs.TabPages.Add(NewTab);
            this.Engine.Mainform.tabControl_WebTabs.SelectedTab = NewTab;
        }

        /// <summary>
        /// Remove new tab
        /// </summary>
        public void Remove()
        {
            var SelectedTab = this.Engine.Mainform.tabControl_WebTabs.SelectedTab;
            if (SelectedTab != null)
                this.Engine.Mainform.tabControl_WebTabs.TabPages.Remove(SelectedTab);
        }

        /// <summary>
        /// Get page content
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public string GetContent(Uri Url)
        {
            string NewContent = null;

            WebRequest request = null;
            try
            {
                request = WebRequest.Create(Url);
            }
            catch(NotSupportedException Ex)
            {
                return Ex.ToString();
            }

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch(WebException Ex)
            {
                return Ex.ToString();
            }
            
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    NewContent = reader.ReadToEnd();
                }
            }
            response.Close();

            return NewContent;
        }

        /// <summary>
        /// Get page content async
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public async Task<string> GetContentAsync(Uri Url)
        {
            return await Task.Run(() => GetContent(Url));
        }

        public async Task<IDocument> ParseHtml(string HtmlText)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            return await context.OpenAsync(req => req.Content(HtmlText));
        }
    }
}
