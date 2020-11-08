using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.WebBrowser.SslTlsCertificate;

namespace TransOS.Plugin.WebBrowser
{
    public class Context
    {
        internal readonly IContext Os;

        public Context(IContext Os)
        {
            this.Os = Os;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = this.RemoteCertificateValidationCallback;
        }

        public void OpenUrl(Uri Url, string Text = null)
        {
            if (string.IsNullOrWhiteSpace(Text))
                Text = "New tab";

            var NewTab = this.Os.Gi.Gui.Windows.Tabs.Add(Text);

            var Forma = new Form_Browser(NewTab, this)
            {
                Url = Url
            };
            NewTab.Body.Add(Forma.panel1);
            NewTab.Select();
        }

        internal List<CertItem> InputCerts = new List<CertItem>();

        private bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            this.InputCerts.Add(new CertItem(new X509Certificate(certificate), chain, sslPolicyErrors));

            return true;
        }
    }
}
