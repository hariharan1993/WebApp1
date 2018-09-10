using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SalesforceIdentity.SAMLSpBroker;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Client c = new Client();
            //lblMsg.Text = c.ClientID;
            
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            String user = TextBox1.Text;
            String pass = TextBox2.Text;

            if (user.Equals("admin", StringComparison.OrdinalIgnoreCase) && pass.Equals("admin"))
            {
                Response.Redirect("home.aspx?UserID=" + user);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            AuthnRequest req = new AuthnRequest();

            Response.Redirect(string.Format(LoadPropertities.idpIssuerUrl + "?client_Id={0}&redirect_uri={1}&response_mode={2}&SAMLRequest={3}", "4345a7b9-9a63-4910-a426-35363201d503", Server.UrlEncode("https://www.office.com/"), "form_post", Server.UrlEncode(req.GetRequest(AuthnRequest.AuthRequestFormat.Base64))));
        }

        
    }


    //public class Client
    //{
    //    [System.ComponentModel.Browsable(false)]
    //    public virtual string ClientID { get; }
    //}

}