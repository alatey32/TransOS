using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.WebBrowser.SslTlsCertificate
{
    internal class CertItem
    {
        internal readonly X509Certificate certificate;
        internal readonly X509Chain chain;
        internal readonly SslPolicyErrors sslPolicyErrors;

        internal CertItem(X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            this.certificate = certificate;
            this.chain = chain;
            this.sslPolicyErrors = sslPolicyErrors;
        }
    }
}
