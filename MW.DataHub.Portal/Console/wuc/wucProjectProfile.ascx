<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucProjectProfile.ascx.cs" Inherits="MW.DataHub.Portal.Console.wuc.wucProjectProfile"  %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<script type="text/javascript">

function btSaveOnClientClick()
{
    var Sequence=$("input[id*='txtSequence']").val().replace(/(^\s*)|(\s*$)/g,"");
    var ProjectName=$("input[id*='txtProjectName']").val().replace(/(^\s*)|(\s*$)/g,"");
    var Owner =$("input[id*='txtOwner']").val().replace(/(^\s*)|(\s*$)/g,"");
    var HostMachineID=$("input[id*='txtHostMachineID']").val().replace(/(^\s*)|(\s*$)/g,"");
    var Adminstrator=$("input[id*='txtAdminstrator']").val().replace(/(^\s*)|(\s*$)/g,"");
    var User=$("input[id*='txtUser']").val().replace(/(^\s*)|(\s*$)/g,"");
    var regInt = /^\d+$/;
    var regEmail=/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
    var i=0;
    if(!regInt.test(Sequence))
    {
        $("input[id*='txtSequence']").closest("td").css("color","red");
        i++;
    }
    else{
        $("input[id*='txtSequence']").closest("td").css("color","");
    }
    if(ProjectName=="")
    {
        $("input[id*='txtProjectName']").closest("td").css("color","red");
        i++;
    }
    else{
        $("input[id*='txtProjectName']").closest("td").css("color","");
    }
    if(Owner=="")
    {
        $("input[id*='txtOwner']").closest("td").css("color","red");
        i++;
    }
    else
    {
        $("input[id*='txtOwner']").closest("td").css("color","");
    }
    if(HostMachineID=="")
    {
        $("input[id*='txtHostMachineID']").closest("td").css("color","red");
        i++;
    }
    else
    {
        $("input[id*='txtHostMachineID']").closest("td").css("color","");
    }
    if(Adminstrator!=""&&Adminstrator!=",")
    {
        AdminstratorArray=Adminstrator.split(',');
        var flat=0;
        for(var j=0;j<AdminstratorArray.length;j++)
        {
            
            if(!regEmail.test(AdminstratorArray[j]))
            {
                flat++;
            }
        }
        if(flat>0)
        {
            $("input[id*='txtAdminstrator']").closest("td").css("color","red");
            i++;
        }
        else
        {
            $("input[id*='txtAdminstrator']").closest("td").css("color","");
        }
    }
    else if(Adminstrator ==",")
    {
        $("input[id*='txtAdminstrator']").closest("td").css("color","red");
        i++;
    }
    if(User!=""&&User!=",")
    {
        UserArray=User.split(',');
        var flat2=0;
        for(var j=0;j<UserArray.length;j++)
        {
            if(!regEmail.test(UserArray[j]))
            {
                flat2++;
            }
        }
        if(flat2>0)
        {
            $("input[id*='txtUser']").closest("td").css("color","red");
            i++;
        }
        else
        {
            $("input[id*='txtUser']").closest("td").css("color","");
        }
    }
    else if(User==",")
    {
        $("input[id*='txtUser']").closest("td").css("color","red");
        i++;
    }
    if(i>0)
    {
        return false;
    }
    else{
        return true;
    }
}
</script>
<table style="width:100%">
<tr>
        <td width="20%">
            <asp:Button ID="btSave" runat="server" Text="Create Project" OnClick="btSave_OnClick" OnClientClick="return btSaveOnClientClick()" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="border-width:0 0 1pt 0; border-style:dotted; border-color:Black; font-weight:bold" colspan="2"> Project Profile</td>
    </tr>
    
    <tr>
        <td width="20%">Sequence:</td>
        <td >
            <asp:TextBox ID="txtSequence" runat="server"  oninput="onlyInputInteger(this)" onpropertychange="onlyInputInteger(this)"></asp:TextBox>*(Sequence number in 
            numeric)</td>
    </tr>
    <tr>
        <td width="20%">Status:</td>
        <td>
            <asp:RadioButtonList ID="radioList" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
            <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
            </asp:RadioButtonList>
            </td>
    </tr>
    <tr>
        <td width="20%">Project Name:</td>
        <td>
            <asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td width="20%">Owner:</td>
        <td>
            <asp:TextBox ID="txtOwner" runat="server"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td width="20%">Description:</td>
        <td>
            <CKEditor:CKEditorControl 
                ID="txtDescription" runat="server">
            </CKEditor:CKEditorControl>
            <asp:Button ID="btReloadTemp" runat="server" Text="Reload Template"  OnClick="btReloadTemp_OnClick" />
            </td>
    </tr>
    
    <tr>
        <td style="border-width:0 0 1pt 0; border-style:dotted; border-color:Black;" colspan="2"></td>
    </tr>
    <tr>
        <td colspan=2></td>
    </tr>
    <tr>
        <td style="border-width:0 0 1pt 0; border-style:dotted; border-color:Black; font-weight:bold" colspan="2"> 
            Runtime Configuration</td>
    </tr>
    <tr>
        <td width="20%">Host Machine ID:</td>
        <td>
            <asp:TextBox ID="txtHostMachineID" runat="server"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td width="20%">Process ID:</td>
        <td>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td width="20%">Parameters:</td>
        <td>
            <asp:TextBox ID="txtParameters" runat="server" TextMode="MultiLine" Rows="8" Width="800px"></asp:TextBox>(In 
            xml)</td>
    </tr>
    <tr>
        <td style="border-width:0 0 1pt 0; border-style:dotted; border-color:Black; font-weight:bold" colspan="2"> 
            Notification Setting</td>
    </tr>
    <tr>
        <td width="20%">Administrator:</td>
        <td>
            <asp:TextBox ID="txtAdminstrator" Width="800px" runat="server"></asp:TextBox><br />(email addresses list 
            for receive system exception mail alert,separate by comma &quot;,&quot;)</td>
    </tr>
    <tr>
        <td width="20%">User:</td>
        <td>
            <asp:TextBox ID="txtUser" Width="800px" runat="server"></asp:TextBox><br />(email addresses list 
            for receive transaction exception mail alert,separate by comma &quot;,&quot;)</td>
    </tr>
    <tr>
        <td width="20%">
            <asp:Button ID="btSave1" runat="server" Text="Create Project" OnClick="btSave_OnClick" OnClientClick="return btSaveOnClientClick()" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>