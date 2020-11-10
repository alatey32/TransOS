using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.Gi.Gui.Windows.Tabs;
using TransOS.Plugin.WebBrowser.SslTlsCertificate;

namespace TransOS.Plugin.WebBrowser
{
    public class Context
    {
        internal readonly IContext Os;

        internal Form_Browser LastForm_Browser;

        public Context(IContext Os)
        {
            this.Os = Os;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = this.RemoteCertificateValidationCallback;

            this.Os.Application.Register(new AppReg(this));
        }

        public ITab OpenUrl(Uri Url, string Text = null, string TabId = null)
        {
            if (string.IsNullOrWhiteSpace(Text))
                Text = "New tab";

            var NewTab = this.Os.Gi.Gui.Windows.Tabs.Add(Text);
            if (TabId != null)
                NewTab.Id = TabId;

            this.LastForm_Browser = new Form_Browser(NewTab, this)
            {
                Url = Url
            };

            NewTab.RestoreCommand = Url.ToString();
            NewTab.Body.Add(this.LastForm_Browser.panel1);
            NewTab.Select();
            return NewTab;
        }

        internal List<CertItem> InputCerts = new List<CertItem>();

        private bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            this.InputCerts.Add(new CertItem(new X509Certificate(certificate), chain, sslPolicyErrors));

            return true;
        }
    }
}
