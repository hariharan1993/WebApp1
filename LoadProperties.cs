using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace WebApplication1
{
    public static class LoadPropertities
    {
        public static String idpIssuerUrl;
        public static String certificate;
        public static String issuer;
        public static String assertionConsumerServiceUrl;
        public static void initProperties()
        {
            var data = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(@"D:\Programs\WebApplication1\WebApplication1\config.properties"))
            {
                String key = row.Split('=')[0];
                String value = row.Split('=')[1];
                data.Add(key, value);
            }
            idpIssuerUrl = data["idpIssuerUrl"];
            certificate = data["certificate"];
            issuer = data["issuer"];
            assertionConsumerServiceUrl = data["assertionConsumerServiceUrl"];
        }
    }
}
