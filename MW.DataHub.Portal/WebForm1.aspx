<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MW.DataHub.Portal.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    
        <asp:Button ID="Button2" runat="server"  Text="Error Process" 
            onclick="Button2_Click" />
    
    <asp:Button ID="TT" runat="server" Text="New " onclick="TT_Click" />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Button" />
        <asp:Button ID="Button4" runat="server"  Text="FTP
        
        " onclick="Button4_Click" />
    </div>
    </form>
</body>
</html>
