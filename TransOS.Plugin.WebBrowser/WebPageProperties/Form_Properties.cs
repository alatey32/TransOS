using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.WebBrowser.WebPageSystem;

namespace TransOS.Plugin.WebBrowser.WebPageProperties
{
    public partial class Form_Properties : Form
    {
        public Form_Properties(WebPage webPage)
        {
            InitializeComponent();

            // HttpResponse
            var sb = new StringBuilder();
            if (webPage.WebRequest != null)
            {
                var asHttp = webPage.WebRequest as HttpWebRequest;
                if (asHttp != null)
                    sb.AppendLine($"{asHttp.Method} {asHttp.RequestUri.LocalPath} HTTP/{asHttp.ProtocolVersion}");

                foreach (var StKey in webPage.WebRequest.Headers.AllKeys)
                    foreach (var StValue in webPage.WebRequest.Headers.GetValues(StKey))
                        sb.AppendLine($"{StKey}: {StValue}");
            }
            this.textBox_Request.Text = sb.ToString();

            // HttpResponse
            sb.Clear();

            if (webPage.WebResponse != null)
            {
                var asHttp = webPage.WebResponse as HttpWebResponse;
                if (asHttp != null)
                    sb.AppendLine($"HTTP/{asHttp.ProtocolVersion} {(int)asHttp.StatusCode} {asHttp.StatusDescription}");

                foreach (var StKey in webPage.WebResponse.Headers.AllKeys)
                    foreach (var StValue in webPage.WebResponse.Headers.GetValues(StKey))
                        sb.AppendLine($"{StKey}: {StValue}");
            }
            this.textBox_HttpResponse.Text = sb.ToString();

            // SSL/TLS certs
            foreach (var certItem in webPage.RemoteCerts)
            {
                X509Certificate2 cert2 = new X509Certificate2(certItem.certificate);
                var VieItem = listView_Certs.Items.Add(cert2.Subject);
                VieItem.Tag = cert2;
            }
            this.listView_Certs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void listView_Certs_DoubleClick(object sender, EventArgs e)
        {
            var lvi = this.listView_Certs.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if(lvi != null)
                X509Certificate2UI.DisplayCertificate(lvi.Tag as X509Certificate2);
        }
    }
}
