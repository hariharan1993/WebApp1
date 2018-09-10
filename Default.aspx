<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Microsoft Identity Demo</title>
</head>
<body>
   <form id="form1" runat="server">
		<div>
		<p>
			<asp:Label ID="Label1" runat="server" 
				Text="Sample SP Application to demo SAML based SSO with Microsoft Identity" 
				style="font-weight: 700; text-align: center;"></asp:Label>
		
		&nbsp;
		</p></div>
		<p>
			<asp:Label ID="Label2" runat="server" Text="Username"></asp:Label>
		&nbsp;
			<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
		</p>
		<p>
			<asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>  &nbsp;
			<asp:TextBox ID="TextBox2" runat="server" type="password" TextMode="Password"></asp:TextBox>
		</p>
		<p>
			<asp:Button ID="Button1" runat="server" Text="Login" onclick="Button1_Click" />&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Login with Microsoft" />
           
		</p>
       
		</form>

    <asp:Label ID ="lblMsg" runat="server"></asp:Label>


</body>
</html>
