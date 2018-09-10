using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using System.IO;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.IO.Compression;
using WebApplication1;

namespace SalesforceIdentity
{
    namespace SAMLSpBroker
    {
        public class ConsumeResponse
        {
            private XmlDocument xmlDoc;            
            private Certificate certificate;

            public ConsumeResponse()
            {
				LoadPropertities.initProperties();
                certificate = new Certificate();
                certificate.loadCert(LoadPropertities.certificate);
            }

            public void loadSAMLResponse(string xml)
            {
                xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                //xmlDoc.loadSAMLResponse(xml);
                xmlDoc.LoadXml(xml);
            }

            public void loadXmlFromBase64SAMLRes(string response)
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                loadSAMLResponse(enc.GetString(Convert.FromBase64String(response)));
            }

            public bool isAuthenticated()
            {
                bool status = false;
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                XmlNodeList nodeList = xmlDoc.SelectNodes("//ds:Signature", manager);

                SignedXml signedXml = new SignedXml(xmlDoc);
                foreach (XmlNode node in nodeList)
                {
                    //signedXml.loadSAMLResponse((XmlElement)node);
                    signedXml.LoadXml((XmlElement)node);
                    status = signedXml.CheckSignature(certificate.cert, true);
                    if (!status)
                        return false;
                }
                return status;
            }

            public string getSubject()
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:NameID", manager);
                return node.InnerText;
            }
        }

        public class AuthnRequest
        {
            public string id;
            private string issueInstant;
            
            

            public enum AuthRequestFormat
            {
                Base64 = 1
            }

            public AuthnRequest()
            {
                LoadPropertities.initProperties();
				id = "_" + System.Guid.NewGuid().ToString();
                issueInstant = DateTime.Now.ToUniversalTime().ToString("yyyy-mm-ddTH:mm:ssZ");
            }

            public string GetRequest(AuthRequestFormat format)
            {
                string samlReq = "<samlp:AuthnRequest xmlns:samlp=\"urn:oasis:names:tc:SAML:2.0:protocol\""
                                + " ProtocolBinding=\"urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST\""
                                + " Version=\"2.0\""
                                + " IssueInstant=\"" + issueInstant + "\""
                                + " ID=\"" + id + "\""
                                + " AssertionConsumerServiceURL=\"" + LoadPropertities.assertionConsumerServiceUrl + "\">\n";
                samlReq += "<saml:Issuer xmlns:saml=\"urn:oasis:names:tc:SAML:2.0:assertion\">" + LoadPropertities.issuer
                            + "</saml:Issuer>\n";
                samlReq += "<samlp:NameIDPolicy Format=\"urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified\" />\n";
                samlReq += "</samlp:AuthnRequest>";
                
                if (format == AuthRequestFormat.Base64)
                {
                    byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(samlReq);
                    using (MemoryStream output = new MemoryStream())
                    {
                        using (DeflateStream zip = new DeflateStream(output , CompressionMode.Compress, true))
                        {
                            zip.Write(toEncodeAsBytes, 0, toEncodeAsBytes.Length);
                        }
                        string base64 = Convert.ToBase64String(output.ToArray());
                        return HttpUtility.UrlEncode(base64);
                    }
                }
                return null;               
            }
        }
		
		public class Certificate
        {
            public X509Certificate2 cert;

            public void loadCert(string certificate)
            {
                cert = new X509Certificate2();
                cert.Import(StringToByteArray(certificate));
            }

            private byte[] StringToByteArray(string st)
            {
                byte[] bytes = new byte[st.Length];
                for (int i = 0; i < st.Length; i++)
                {
                    bytes[i] = (byte)st[i];
                }
                return bytes;
            }
        }
    }
}
