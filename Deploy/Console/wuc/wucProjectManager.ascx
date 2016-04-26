<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucProjectManager.ascx.cs"
    Inherits="DIMERCO.B2B.Portal.Console.wuc.wucProjectManager" %>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />

<script type="text/javascript">

function lbtNewProjectClick()
{
    $(parent.document).find("iframe").attr("src","Console/SelectFrame.aspx?");
}
function trOndblclik(e)
{
    $(parent.document).find("iframe").attr("src","Console/SelectFrame.aspx?ProjectID="+$(e).attr("name"));
}

function editOnclick(e)
{
    $(parent.document).find("iframe").attr("src","Console/SelectFrame.aspx?ProjectID="+$(e).attr("name"));
}

function trOnclik(e)
{
    var objTr=$(".tr02").get(0);
    $(objTr).attr("class","");
    $(e).attr("class","tr02");
    bindProjectDetail($(e).attr("name"));
    $("#dialog").dialog({width:800,height:600,close: function( event, ui ){var objTr=$(".tr02").get(0);$(objTr).attr("class","");}});
    changeDialogBackgroundColor();
    var tempwidth	= $("#tdDescription").width()>$("#tdParameters").width()?$("#tdDescription").width():$("#tdParameters").width()+50;
	$("#dialog").dialog("option","width",tempwidth);
}

function bindProjectDetail(id)
{
    var dataPara = {"ID":id};
    var url="AjaxRequest.aspx?funtion=GetProjectDetail";
    $.post( url, dataPara, function(data) {
        $("#tdSequence").text(data.Seq);
        $("#tdProjectName").text(data.ProjectName);  
        $("#tdHostMachine").text(data.HostMachine);  
        $("#tdProcessID").text(data.ProcessID);  
        $("#tdOwner").text(data.Owner);  
        $("#tdUpdateDT").text(data.UpdateDT);  
        $("#tdStatus").text(data.Status);  
        $("#tdAdminEmail").text(data.AdminMail);  
        $("#tdUserEmail").text(data.UserMail);  
        $("#tdDescription").html(strJsonDeReplace(data.Desc,"Html"));
        $("#tdParameters").text(strJsonDeReplace(data.Parameters,""));// function defined at Default.Master
    }, "json");
}
    
function downClick()
{
     var objTr=$(".tr02").get(0);
     var objTrNext=$(objTr).next().get(0);
     if(objTrNext)
     {
         trOnclik(objTrNext);
     }
     else
     {
         objTrNext=$(".trHead").next().get(0);
         trOnclik(objTrNext);
     }
}

function upClick()
{
     var objTr=$(".tr02").get(0);
     var objTrNext=$(objTr).prev().get(0);
     if(objTr&&objTrNext!=$(".trHead").get(0))
     {
         trOnclik(objTrNext);
     }
     else
     {
         objTrNext=$(".trHead").nextAll().last().get(0);
         trOnclik(objTrNext);
     }
}

</script>

<table  border="0" cellpadding="0" cellspacing="0" width="80%">
    <tr>
        <td>
            Project Name:<asp:TextBox runat="server" ID="tetProjectCode"></asp:TextBox>
            Status:<asp:DropDownList runat="server" ID="ddlProjectStatus">
                <asp:ListItem Text="All" Value=""></asp:ListItem>
                <asp:ListItem Text="Active" Value="Active" ></asp:ListItem>
                <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button Text="Query" runat="server" ID="btnSearch"  OnClick="btnSearch_OnClick"/>
            <a class="aLink"
                onclick="lbtNewProjectClick()" id="lbtNewProject">New Project</a>
        </td>
    </tr>
    <tr>
        <td>
            <table class="table01" border="0" cellpadding="0" cellspacing="0" align="center"
                width="100%">
                <tr class="trHead">
                    <td>
                        Seq
                    </td>
                    <td>
                        Project
                    </td>
                    <td>
                        Host Machine
                    </td>
                    <td>
                        Process ID
                    </td>
                    <td>
                        Owner
                    </td>
                    <td>
                        Update Date
                    </td>
                    <td>
                        Status
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <asp:Repeater ID="rptProjectList" runat="server"  OnItemCommand="rptProjectList_OnItemCommand">
                <ItemTemplate >
                <tr title=" click to show detail" onmousemove="trMouseMove(this);" name='<%#Eval("ID") %>' onmouseout="trMouseOut(this)" onclick="trOnclik(this)" >
                            <td>
                                <%#Eval("Sequence")%>
                            </td>
                            <td>
                                <%#Eval("ProjectName") %>
                            </td>
                            <td>
                                <%#Eval("HostMachineID")%>
                            </td>
                            <td>
                                <%#Eval("ProcessID")%>
                            </td>
                            <td>
                                <%#Eval("Owner")%>
                            </td>
                            <td>
                                <%#Eval("UpdatedDT") is DBNull ? "" : Convert.ToDateTime(Eval("UpdatedDT")).ToString("yyyy/MM/dd hh:mm:ss")%>
                            </td>
                            <td style='<%#Eval("Status").ToString()=="Active"?"background-color: #99cc33":"background-color: Yellow" %>'>
                                <%#Eval("Status") %>
                            </td>
                            <td align="center" onclick="cancleEvent(event)">
                                <a title="Edit" name='<%#Eval("ID") %>' style="background-image:url(../Image/Icon/edit.gif);width:16px;height:16px;display:block;" onclick="editOnclick(this)" />
                                </td>
                            <td onclick="cancleEvent(event)">
                                <asp:ImageButton ID="ibtDelete" ImageUrl="~/Image/Icon/Remove.gif" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' runat="server" OnClientClick="return ConfirmDelete()" title="Click to delete" />
                            </td>
                        </tr>
                        </ItemTemplate>
                </asp:Repeater>
                </table>
        </td>
    </tr>

</table>


<div id="dialog" title="Project Detail" style="display: none">
    <table border="0" class="table02" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="180px">
                Sequence :
            </td>
            <td id="tdSequence">
            </td>
            <td width="35px">
                <input type="button"  id="btUp" value="↑" onclick="upClick()" />
            </td>
        </tr>
        <tr>
            <td width="80px">
                Project Name :
            </td>
            <td id="tdProjectName">
            </td>
            <td width="35px">
                <input type="button"  id="btDown" value="↓" onclick="downClick()" />
            </td>
        </tr>
        <tr>
            <td width="80px">
                Host Machine :
            </td>
            <td colspan="2" id="tdHostMachine">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Process ID :
            </td>
            <td colspan="2" id="tdProcessID">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Owner :
            </td>
            <td colspan="2" id="tdOwner">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Update Date :
            </td>
            <td colspan="2" id="tdUpdateDT">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Status :
            </td>
            <td colspan="2" id="tdStatus">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Administrator's email addresses :
            </td>
            <td colspan="2" id="tdAdminEmail">
            </td>
        </tr>
        <tr>
            <td width="80px">
                User's email addresses :
            </td>
            <td colspan="2" id="tdUserEmail">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Description :
            </td>
            <td colspan="2" >
            </td>
        </tr>
        <tr>
            <td id="tdDescription" colspan="3" height="150px" valign="top" >
                
            </td>
        </tr>
        <tr>
            <td width="80px">
                Runtime Parameters :
            </td>
            <td colspan="2" >
            </td>
        </tr>
        <tr>
            <td id="tdParameters" colspan="3" height="150px" valign="top" >
                
            </td>
        </tr>
    </table>
    
</div>