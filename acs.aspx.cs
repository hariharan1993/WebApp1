using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SalesforceIdentity.SAMLSpBroker;

namespace WebApplication1
{
    public partial class acs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConsumeResponse samlResponse = new ConsumeResponse();
            samlResponse.loadXmlFromBase64SAMLRes(Request.Form["SAMLResponse"]);

            if (samlResponse.isAuthenticated())
            {
                //ConsumeResponse.Redirect("home.aspx?UserID=" + samlResponse.getSubject());
                Response.Redirect(LoadPropertities.assertionConsumerServiceUrl + "?UserID=" + samlResponse.getSubject());
            }
            else
            {
                //ConsumeResponse.Write("SSO is failed!");
            }
        }
    }
}