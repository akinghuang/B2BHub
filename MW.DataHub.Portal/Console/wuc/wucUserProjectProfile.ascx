<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucUserProjectProfile.ascx.cs" Inherits="MW.DataHub.Portal.Console.wuc.wucUserProjectProfile" %>

<table width="100%">
<tr>
<td width="80px">UserID</td><td><asp:TextBox ID="txtUserID" runat="server" ></asp:TextBox>*</td>
</tr>
<tr><td width="80px">Name</td><td><asp:TextBox ID="txtName" runat="server"></asp:TextBox>*</td>
</tr>
<tr><td width="80px">PassWord</td><td><asp:TextBox ID="txtPassWord" runat="server"></asp:TextBox></td></tr>
<tr><td width="80px">Status</td><td><asp:CheckBoxList ID="cbStatus" runat="server" RepeatDirection="Horizontal" >
<asp:ListItem Selected="True" Text="Active" Value="Active"></asp:ListItem>
<asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
</asp:CheckBoxList></td></tr>
<tr>
<td width="80px">Description</td><td><asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Rows="4" ></asp:TextBox></td>
</tr>
<tr><td colspan="2"> <asp:Button ID="btSave" runat="server"  Text="Save"/></td></tr>
</table>