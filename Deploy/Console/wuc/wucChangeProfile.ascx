<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucChangeProfile.ascx.cs" Inherits="DIMERCO.B2B.Portal.Console.wuc.wucChangeProfile" %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<%@ Register assembly="CKFinder" namespace="CKFinder" tagprefix="CKFinder" %>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />
<script type="text/javascript" src="../../ckfinder/ckfinder.js"></script> 
<script type="text/javascript">
$(document).ready(function(){
    $("#<%=txtStartDate.ClientID %>").datepicker({dateFormat: "yy/mm/dd"});
    $("#<%=txtTargetDate.ClientID %>").datepicker({dateFormat: "yy/mm/dd"});
    $("#<%=txtCompleteDate.ClientID %>").datepicker({dateFormat: "yy/mm/dd"});
    $("#<%=txtOnlineDate.ClientID %>").datepicker({dateFormat: "yy/mm/dd"});
})

function CheckInput()
{
    var Subject=$("#<%=txtSubject.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var Owner=$("#<%=txtOwner.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var Source=$("#<%=txtSource.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var StartDate=$("#<%=txtStartDate.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var TargetDate=$("#<%=txtTargetDate.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var CompleteDate=$("#<%=txtCompleteDate.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var OnlineDate=$("#<%=txtOnlineDate.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var regDate=new RegExp("^(?:(?!0000)[0-9]{4}/(?:(?:0[1-9]|1[0-2])/(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])/(?:29|30)|(?:0[13578]|1[02])/31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)/02/29)$");
    var regFex=/^.*?\.(zip|7z)$/;
    var SourceCode=$("#<%=txtSourceCodePath.ClientID %>").val();
    var i=0;
    if(Subject=="")
    {
        i++;
        $("#<%=txtSubject.ClientID %>").closest("td").css("color","red");
    }
    else{
        $("#<%=txtSubject.ClientID %>").closest("td").css("color","");
    }
    
    if(Owner=="")
    {
        i++;
        $("#<%=txtOwner.ClientID %>").closest("td").css("color","red");
    }
    else{
        $("#<%=txtOwner.ClientID %>").closest("td").css("color","");
    }
    
    if(Source=="")
    {
        i++;
        $("#<%=txtSource.ClientID %>").closest("td").css("color","red");
    }
    else{
        $("#<%=txtSource.ClientID %>").closest("td").css("color","");
    }
    
    if(regDate.test(StartDate))
    {
        $("#<%=txtStartDate.ClientID %>").closest("td").css("color","");
    }
    else{
        i++;
        $("#<%=txtStartDate.ClientID %>").closest("td").css("color","red");
    }
    
    if(regDate.test(TargetDate))
    {
        $("#<%=txtTargetDate.ClientID %>").closest("td").css("color","");
    }
    else{
        i++;
        $("#<%=txtTargetDate.ClientID %>").closest("td").css("color","red");
    }
    
    if(regDate.test(CompleteDate))
    {
        $("#<%=txtCompleteDate.ClientID %>").closest("td").css("color","");
    }
    else{
        i++;
        $("#<%=txtCompleteDate.ClientID %>").closest("td").css("color","red");
    }
    
    if(regDate.test(OnlineDate))
    {
        $("#<%=txtOnlineDate.ClientID %>").closest("td").css("color","");
    }
    else{
        i++;
        $("#<%=txtOnlineDate.ClientID %>").closest("td").css("color","red");
    }
    
    if(regFex.test(SourceCode)||SourceCode=="")
    {
        $("#<%=txtSourceCodePath.ClientID %>").closest("td").css("color","");
    }
    else{
        i++;
        $("#<%=txtSourceCodePath.ClientID %>").closest("td").css("color","red");
    }
    
    if(i>0)
    {
        return false; 
    }
    else{
        return true;
    }
}


function BrowseServer(inputId) 
{ 
    var finder = new CKFinder() ; 
    finder.basePath = '../ckfinder/'; //导入CKFinder的路径 
    finder.selectActionFunction = SetFileField; //设置文件被选中时的函数 
    finder.selectActionData = inputId; //接收地址的input ID 
    finder.popup() ; 
} 
//文件选中时执行 
function SetFileField(fileUrl,data) 
{   
    $("#"+data["selectActionData"]).val(fileUrl); 
} 
</script>
<table style="width:100%">
    <tr>
        <td colspan="2">
            <asp:Button ID="btSave" runat="server" Text="Create Project Changing" OnClientClick="return CheckInput()" OnClick="btSave_OnClick"  />
        </td>
    </tr>
    <tr>
        <td style="border-width:0 0 1pt 0; border-style:dotted; border-color:Black; font-weight:bold" colspan="2"> 
            Changing Profile</td>
    </tr>
    <tr>
        <td width="20%">Subject:</td>
        <td>
            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>*(Not null)</td>
    </tr>
    <tr>
        <td width="20%">Owner:</td>
        <td>
            <asp:TextBox ID="txtOwner" runat="server"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td width="20%">Source:</td>
        <td>
            <asp:TextBox ID="txtSource" Width="800px" runat="server"></asp:TextBox>*(Not null)</td>
    </tr>
    <tr>
        <td width="20%">Content:</td>
        <td>
            
            <CKEditor:CKEditorControl ID="txtContent" runat="server">
            </CKEditor:CKEditorControl>
            <asp:Button ID="btReloadTemp" runat="server" Text="Reload Template"  OnClick="btReloadTemp_OnClick" />
            </td>
    </tr>
    
    <tr>
        <td width="20%">Start Date:</td>
        <td>
            <asp:TextBox ID="txtStartDate" CssClass="txtTime" runat="server"></asp:TextBox>*(Not null)</td>
    </tr>
    
    <tr>
        <td width="20%">Target Date:</td>
        <td>
            <asp:TextBox ID="txtTargetDate" CssClass="txtTime" runat="server"></asp:TextBox>*(Not null)</td>
    </tr>
    
    <tr>
        <td width="20%">Complete Date:</td>
        <td>
            <asp:TextBox ID="txtCompleteDate" CssClass="txtTime" runat="server"></asp:TextBox>*(Not null)</td>
    </tr>
    
    <tr>
        <td width="20%">Online Date:</td>
        <td>
            <asp:TextBox ID="txtOnlineDate" CssClass="txtTime" runat="server"></asp:TextBox>*(Not null)</td>
    </tr>
    
    <tr>
        <td width="20%">Source Code:</td>
        <td>
            <asp:TextBox ID="txtSourceCodePath" runat="server"></asp:TextBox> 
            <input type="button" value="Browse" onclick="BrowseServer('<%=txtSourceCodePath.ClientID %>');" /> (.zip.7z)
        </td>
    </tr>
    
    <tr>
        <td colspan="2">
            <asp:Button ID="btSave1" runat="server" Text="Create Project Change" OnClientClick="return CheckInput()" OnClick="btSave_OnClick"/>
        </td>
    </tr>
    </table>