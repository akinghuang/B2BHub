<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucTaskProfile.ascx.cs" Inherits="MW.DataHub.Portal.Console.wuc.wucTaskProfile" %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-1.10.2.js"></script>

<script type="text/javascript">
var Protocol="";
var Schedule = "";
$(document).ready(function(){
    Protocol=$("#<%=ddlProtocol.ClientID %>").find("option:selected").val();
    Schedule=$("#<%=radioListTimeType.ClientID %>").find("input:checked").val();
    protocolChange();
    ScheduleTypeChange();
})

function ChangeIframeHeight()
{
    var wind=window.parent;
    var bodyHeight=$(document.body).height();
    var i=1;
    while(i++)
    {
        $(wind.document).find("iframe").attr("height",bodyHeight);
        bodyHeight=$(wind.document.body).height();
        wind=wind.parent;
        if(i>=5)
        break;
    }
}

function BtSaveOnClientClick()
{
    var Sequence=$("#<%=txtSequence.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var TaskName=$("#<%=txtTaskName.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var EveryTime= $("#<%=txtEvery.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var DailyTime=$("#<%=txtDaily.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var WeekTime=$("#<%=txtWeek.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var Monthly=$("#<%=txtMonthly.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var TimeType=$("#<%=radioListTimeType.ClientID %>").find("input:checked").val();
    var ProcotolType=$("#<%=ddlProtocol.ClientID %>").find("option:selected").val();
    var Port=$("#<%=txtPort.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var regEvery=/^\d+$/;
    var IO=$("#<%=radioListProtocol.ClientID %>").find("input:checked").val();
    //var regTime=/^([2][0-3]|[0-1][0-9]|[0-9]{1}):([0-5][0-9]|[6][0])$/;
    var regTime=/^(([1-9]{1})|([0-1][0-9])|([1-2][0-3])):([0-5][0-9])$/;
    var i=0;
    if(!regEvery.test(Sequence)){
        $("#<%=txtSequence.ClientID %>").closest("td").css("color","red");
        i++;
    }
    else{
        $("#<%=txtSequence.ClientID %>").closest("td").css("color","");
    }
    
    if(IO==undefined||IO==""){
        $("#<%=radioListProtocol.ClientID %>").closest("td").css("color","red");
        i++;
    }
    else{
        $("#<%=radioListProtocol.ClientID %>").closest("td").css("color","");
    }
    
    if(TaskName==""){
        $("#<%=txtTaskName.ClientID %>").closest("td").css("color","red");
        i++;
    }
    else{
        $("#<%=txtTaskName.ClientID %>").closest("td").css("color","");
    }
    
    if(ProcotolType=="FTP"||ProcotolType=="FTPs"||ProcotolType=="HTTP"|| ProcotolType=="HTTPs"){
         if(!regEvery.test(Port)){
             $("#<%=txtPort.ClientID %>").closest("td").css("color","red");
             i++;
         }
         else{
             $("#<%=txtPort.ClientID %>").closest("td").css("color","");
         }
    
    }
    switch(TimeType){
        case "Every":
            if(regEvery.test(EveryTime))
            {
                $("#<%=txtEvery.ClientID %>").closest("td").css("color","");
            }
            else{
                $("#<%=txtEvery.ClientID %>").closest("td").css("color","red");
                i++;
            }
            break;
        case "Daily":
            if(regTime.test(DailyTime))
            {
                $("#<%=txtDaily.ClientID %>").closest("td").css("color","");
            }
            else{
                $("#<%=txtDaily.ClientID %>").closest("td").css("color","red");
                i++;
            }
            break;
        case "Week":
            if(regTime.test(WeekTime))
            {
                $("#<%=txtWeek.ClientID %>").closest("td").css("color","");
            }
            else{
                $("#<%=txtWeek.ClientID %>").closest("td").css("color","red");
                i++;
            }
            break;
        case "Monthly":
            if(regTime.test(Monthly))
            {
                $("#<%=txtMonthly.ClientID %>").closest("td").css("color","");
            }
            else{
                $("#<%=txtMonthly.ClientID %>").closest("td").css("color","red");
                i++;
            }
            break;
    }
    if(i>0){
        return false;
    }
    else{
        return true;
    }
    
}

function ddlProtocolSelectChange(e)
{
    Protocol=$(e).find("option:selected").val();
    protocolChange();
}

function protocolChange()
{
    if(Protocol=="FTP"){
         $("input[id*='txtServer']").closest("tr").show();
         $("input[id*='txtFolder']").closest("tr").show();
         $("input[id*='txtPort']").closest("tr").show();
         $("input[id*='txtUserID']").closest("tr").show();
         $("input[id*='txtPassWord']").closest("tr").show();
         $("input[id*='txtCertificate']").closest("tr").hide();
    }
    else if(Protocol=="FTPs"){
         $("input[id*='txtServer']").closest("tr").show();
         $("input[id*='txtFolder']").closest("tr").show();
         $("input[id*='txtPort']").closest("tr").show();
         $("input[id*='txtUserID']").closest("tr").show();
         $("input[id*='txtPassWord']").closest("tr").show();
         $("input[id*='txtCertificate']").closest("tr").show();
    }
    else if(Protocol=="sFTP"){
         $("input[id*='txtServer']").closest("tr").show();
         $("input[id*='txtFolder']").closest("tr").show();
         $("input[id*='txtPort']").closest("tr").show();
         $("input[id*='txtUserID']").closest("tr").show();
         $("input[id*='txtPassWord']").closest("tr").show();
         $("input[id*='txtCertificate']").closest("tr").show();
    }
    else if(Protocol=="Local"){
         $("input[id*='txtServer']").closest("tr").hide();
         $("input[id*='txtFolder']").closest("tr").show();
         $("input[id*='txtPort']").closest("tr").hide();
         $("input[id*='txtUserID']").closest("tr").hide();
         $("input[id*='txtPassWord']").closest("tr").hide();
         $("input[id*='txtCertificate']").closest("tr").hide();
    }
    else if(Protocol=="HTTP"){
         $("input[id*='txtServer']").closest("tr").show();
         $("input[id*='txtFolder']").closest("tr").hide();
         $("input[id*='txtPort']").closest("tr").show();
         $("input[id*='txtUserID']").closest("tr").show();
         $("input[id*='txtPassWord']").closest("tr").show();
         $("input[id*='txtCertificate']").closest("tr").hide();
    }
    else if(Protocol=="HTTPs"){
         $("input[id*='txtServer']").closest("tr").show();
         $("input[id*='txtFolder']").closest("tr").hide();
         $("input[id*='txtPort']").closest("tr").show();
         $("input[id*='txtUserID']").closest("tr").show();
         $("input[id*='txtPassWord']").closest("tr").show();
         $("input[id*='txtCertificate']").closest("tr").show();
    }
}

function radioListTimeTypeChange(e)
{
    Schedule=$(e).find("input:checked").val();
    ScheduleTypeChange();
}

function ScheduleTypeChange()
{
     switch(Schedule)
    {
        case "Every":
            $("input[id*='txtEvery']").closest("tr").show();
            $("input[id*='txtDaily']").closest("tr").hide();
            $("input[id*='txtWeek']").closest("tr").hide();
            $("input[id*='Monthly']").closest("tr").hide();
        break;
        case "Daily":
            $("input[id*='txtEvery']").closest("tr").hide();
            $("input[id*='txtDaily']").closest("tr").show();
            $("input[id*='txtWeek']").closest("tr").hide();
            $("input[id*='Monthly']").closest("tr").hide();
        break;
        case "Week":
            $("input[id*='txtEvery']").closest("tr").hide();
            $("input[id*='txtDaily']").closest("tr").hide();
            $("input[id*='txtWeek']").closest("tr").show();
            $("input[id*='Monthly']").closest("tr").hide();
        break;
        case "Monthly":
            $("input[id*='txtEvery']").closest("tr").hide();
            $("input[id*='txtDaily']").closest("tr").hide();
            $("input[id*='txtWeek']").closest("tr").hide();
            $("input[id*='Monthly']").closest("tr").show();
        break;
    }
}
</script>
<table style="width:100%">
    <tr>
        <td colspan="2"> <asp:Button ID="tbSave" Text="Create Task" runat="server" OnClick="BtSave_OnClick"  OnClientClick="return BtSaveOnClientClick();"/></td>
    </tr>
    <tr>
        <td style="border-width:0 0 1pt 0; border-style:dotted; border-color:Black; font-weight:bold" colspan="2"> 
            Task Profile</td>
    </tr>
    <tr>
        <td width="20%">Sequence:</td>
        <td>
            <asp:TextBox ID="txtSequence" runat="server" Width="70px" oninput="onlyInputInteger(this)" onpropertychange="onlyInputInteger(this)"></asp:TextBox>*(Sequence number in 
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
        <td width="20%">Task&nbsp; Name:</td>
        <td>
            <asp:TextBox ID="txtTaskName" runat="server"></asp:TextBox>*(Not null)</td>
    </tr>
    <tr>
        <td width="20%">Description:</td>
        <td>
            <CKEditor:CKEditorControl 
                ID="txtDesc" runat="server">
            </CKEditor:CKEditorControl>
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
            Configuration</td>
    </tr>
    <tr>
        <td width="20%">Protocol:</td>
        <td>
            <asp:DropDownList ID="ddlProtocol" runat="server" onchange="ddlProtocolSelectChange(this)" style="display:inline-table">
                <asp:ListItem Value="FTP">FTP</asp:ListItem>
                <asp:ListItem Value="Local">Local</asp:ListItem>
                <asp:ListItem Value="HTTP">HTTP</asp:ListItem>
                <asp:ListItem Value="FTPs">FTPs</asp:ListItem>
                <asp:ListItem Value="sFTP">sFTP</asp:ListItem>
                <asp:ListItem Value="HTTPs">HTTPs</asp:ListItem>
            </asp:DropDownList>
            *<asp:RadioButtonList ID="radioListProtocol" runat="server"  style="display:inline-table"
                RepeatDirection="Horizontal">
                <asp:ListItem Value="Incoming">Incoming</asp:ListItem>
                <asp:ListItem Value="Outgoing">Outgoing</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td width="20%">File Type:</td>
        <td>
            <asp:TextBox ID="txtFileType" runat="server"></asp:TextBox>(*.* for all)</td>
    </tr>
    <tr>
        <td width="20%">Runtime Parameters:</td>
        <td>
            <asp:TextBox ID="txtRunTimeParas" runat="server" Rows="8" TextMode="MultiLine" 
                Width="800px"></asp:TextBox><br />(In xml, defined by each specific individual 
            project base on requirement)</td>
    </tr>
    <tr>
        <td width="20%">Message Handler Component:</td>
        <td>
            <asp:TextBox ID="txtMHandl" Width="800px" runat="server"></asp:TextBox><br />(the dll file which 
            developed for specific EDI project/Task to process incoming/generate outgoing 
            message)</td>
    </tr>
    <tr>
        <td width="20%">Business Handler Component:</td>
        <td>
            <asp:TextBox ID="txtBHandler" Width="800px" runat="server"></asp:TextBox><br />(the dll file which 
            developed for specific EDI project/Task to process incoming/generate outgoing 
            data into business system)</td>
    </tr>
    <tr>
        <td style="border-width:0 0 1pt 0; border-style:dotted; border-color:Black; font-weight:bold" colspan=2> 
            Remote Interface Setting</td>
    </tr>
    <tr>
        <td width="20%">Server:</td>
        <td>
            <asp:TextBox ID="txtServer" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td width="20%">Folder:</td>
        <td>
            <asp:TextBox ID="txtFolder" Width="800px" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td width="20%">Port:</td>
        <td>
            <asp:TextBox ID="txtPort" runat="server" oninput="onlyInputInteger(this)" onpropertychange="onlyInputInteger(this)"></asp:TextBox>*(Sequence number in 
            numeric)</td>
    </tr>
    <tr>
        <td width="20%">User ID:</td>
        <td>
            <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td width="20%">Password:</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>
    </tr>
    <tr style="display:none">
        <td width="20%">Certificate:</td>
        <td>
            <asp:TextBox ID="txtCertificate" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="border-width:0 0 1pt 0; border-style:dotted; border-color:Black; font-weight:bold" colspan="2"> 
            Local Interface Setting</td>
    </tr>
    <tr>
        <td>
            Folder:</td>
            <td>
            <asp:TextBox ID="txtLFolder" Width="800px" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="border-width:0 0 1pt 0; border-style:dotted; border-color:Black; font-weight:bold" colspan="2"> 
            Schedule Setting</td>
    </tr>
    <tr><td> <asp:RadioButtonList ID="radioListTimeType" runat="server" RepeatDirection="Horizontal" onchange="radioListTimeTypeChange(this)">
    <asp:ListItem Selected="True" Value="Every" >Every</asp:ListItem>
    <asp:ListItem Value="Daily" >Daily</asp:ListItem>
    <asp:ListItem Value="Week" >Week</asp:ListItem>
    <asp:ListItem Value="Monthly" >Monthly</asp:ListItem>
    </asp:RadioButtonList></td></tr>
    <tr>
        <td>
            Every:</td>
            <td>
            <asp:TextBox ID="txtEvery" runat="server" oninput="onlyInputInteger(this)" onpropertychange="onlyInputInteger(this)"></asp:TextBox>Minutes*</td>
    </tr>
    <tr style="display:none">
        <td>
            Daily:</td>
            <td>
            <asp:TextBox ID="txtDaily" runat="server"></asp:TextBox>HH:MM*</td>
    </tr>
    <tr style="display:none">
        <td>
            Week:</td>
            <td>
            <asp:DropDownList ID="ddlWeek" runat="server">
            <asp:ListItem Value="Monday">Monday</asp:ListItem>
            <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
            <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
            <asp:ListItem Value="Thusday">Thursday</asp:ListItem>
            <asp:ListItem Value="Friday">Friday</asp:ListItem>
            <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
            <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
            </asp:DropDownList> <asp:TextBox ID="txtWeek" runat="server"></asp:TextBox>HH:MM*</td>
    </tr>
    <tr style="display:none">
        <td>
            Monthly:</td>
            <td>
            <asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList> <asp:TextBox ID="txtMonthly" runat="server"></asp:TextBox>HH:MM* </td>
    </tr>
    <tr>
        <td width="20%"> <asp:Button ID="BtSave1" Text="Create Task" runat="server"  OnClick="BtSave_OnClick" OnClientClick="return BtSaveOnClientClick();"/></td>
        <td>
            &nbsp;</td>
    </tr>
</table>

