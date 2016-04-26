<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucTaskManager.ascx.cs"
    Inherits="DIMERCO.B2B.Portal.Console.wuc.wucTaskManager" %>

<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>

<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>

<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />

<script type="text/javascript">
var ProjectID=<%=ProjectID %>;
function btnNewTaskClick()
{
   $(parent.document).find("iframe[id*='iframe1']").attr("src","TaskProfile.aspx?ProjectID="+ProjectID);
}
function trOndblclik(e)
{
   $(parent.document).find("iframe[id*='iframe1']").attr("src","TaskProfile.aspx?ID="+$(e).attr("name")+"&ProjectID="+ProjectID);
}

function editOnclick(e)
{
   $(parent.document).find("iframe[id*='iframe1']").attr("src","TaskProfile.aspx?ID="+$(e).attr("name")+"&ProjectID="+ProjectID);
}

function trOnclik(e)
{
    var objTr=$(".tr02").get(0);
    $(objTr).attr("class","");
    $(e).attr("class","tr02");
    bindProjectTaskDetail($(e).attr("name"));
    $("#dialog").dialog({width:950,height:600,close: function( event, ui ){var objTr=$(".tr02").get(0);$(objTr).attr("class","");}});
    changeDialogBackgroundColor();
    var tempwidth	= $("#tdDescription").width()>$("#tdParameters").width()?$("#tdDescription").width():$("#tdParameters").width()+50;
	$("#dialog").dialog("option","width",tempwidth);
}

function bindProjectTaskDetail(id)
{
    var dataPara = {"ID":id};
    var url="AjaxRequest.aspx?funtion=GetProjectTaskDetail";
    $.post( url, dataPara, function(data) {
        $("#tdSequence").text(data.Seq);
        $("#tdStatus").text(data.Status); 
        $("#tdTaskName").text(data.TaskName);
        $("#tdDescription").html(strJsonDeReplace(data.Desc,"Html"));// function defined at Default.Master
           
        $("#tdProtocol").text(data.Protocol);  
        $("#tdIO").text(data.IO);  
        $("#tdFileType").text(data.FileType);  
        $("#tdParameters").text(strJsonDeReplace(data.Parameters,""));
        $("#tdMsgHandler").text(data.MsgHandler);  
        $("#tdBizHandler").text(data.BizHandler);  
         
        $("#tdRServer").text(data.RServer);  
        $("#tdRFolder").text(data.RFolder);  
        $("#tdRPort").text(data.RPort); 
        $("#tdRUID").text(data.RUID);  
        $("#tdRPWD").text(data.RPWD);  
        $("#tdRCert").text(data.RCert);  
        
        $("#tdLFolder").text(data.LFolder); 
        
        $("#tdScheduleType").text(data.ScheduleType);
        $("#tdScheduleInterval").text(data.ScheduleInterval); 
        $("#tdMonthly").text(data.ScheduleMonth); 
        $("#tdScheduleTime").text(data.ScheduleTime); 
        $("#tdWeek").text(data.ScheduleWeekDay); 
        protocolChange(data.Protocol);
        ScheduleTypeChange(data.ScheduleType);
    }, "json");
}

function protocolChange(Protocol)
{
    if(Protocol=="FTP"){
         $("#tdRServer").closest("tr").show();
         $("#tdRFolder").closest("tr").show();
         $("#tdRPort").closest("tr").show();
         $("#tdRUID").closest("tr").show();
         $("#tdRPWD").closest("tr").show();
         $("#tdRCert").closest("tr").hide();
    }
    else if(Protocol=="FTPs"){
         $("#tdRServer").closest("tr").show();
         $("#tdRFolder").closest("tr").show();
         $("#tdRPort").closest("tr").show();
         $("#tdRUID").closest("tr").show();
         $("#tdRPWD").closest("tr").show();
         $("#tdRCert").closest("tr").show();
    }
    else if(Protocol=="Local"){
         $("#tdRServer").closest("tr").hide();
         $("#tdRFolder").closest("tr").show();
         $("#tdRPort").closest("tr").hide();
         $("#tdRUID").closest("tr").hide();
         $("#tdRPWD").closest("tr").hide();
         $("#tdRCert").closest("tr").hide();
    }
    else if(Protocol=="HTTP"){
         
         $("#tdRServer").closest("tr").show();
         $("#tdRFolder").closest("tr").hide();
         $("#tdRPort").closest("tr").show();
         $("#tdRUID").closest("tr").show();
         $("#tdRPWD").closest("tr").show();
         $("#tdRCert").closest("tr").hide();
    }
    else if(Protocol=="HTTPs"){
        
         $("#tdRServer").closest("tr").show();
         $("#tdRFolder").closest("tr").hide();
         $("#tdRPort").closest("tr").show();
         $("#tdRUID").closest("tr").show();
         $("#tdRPWD").closest("tr").show();
         $("#tdRCert").closest("tr").show();
    }
}

function ScheduleTypeChange(Schedule)
{
     switch(Schedule)
    {
        case "Every":
            $("#tdScheduleInterval").closest("tr").show();
            $("#tdMonthly").closest("tr").hide();
            $("#tdScheduleTime").closest("tr").hide();
            $("#tdWeek").closest("tr").hide();
        break;
        case "Daily":
            $("#tdScheduleInterval").closest("tr").hide();
            $("#tdMonthly").closest("tr").hide();
            $("#tdScheduleTime").closest("tr").show();
            $("#tdWeek").closest("tr").hide();
        break;
        case "Week":
            $("#tdScheduleInterval").closest("tr").hide();
            $("#tdMonthly").closest("tr").hide();
            $("#tdScheduleTime").closest("tr").show();
            $("#tdWeek").closest("tr").show();
        break;
        case "Monthly":
            $("#tdScheduleInterval").closest("tr").hide();
            $("#tdMonthly").closest("tr").show();
            $("#tdScheduleTime").closest("tr").show();
            $("#tdWeek").closest("tr").hide();
        break;
    }
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

<table border="0" cellpadding="0" cellspacing="0" width="80%">
    <tr>
        <td height="30px">
            <input id="btnAdd" value="NewTask" type="button" onclick="btnNewTaskClick()" />
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
                        Task
                    </td>
                    <td>
                        Protocol
                    </td>
                    <td>
                        Schedule
                    </td>
                    <td>
                        Last Run Time
                    </td>
                    <td>
                        Next Run Time
                    </td>
                    <td>
                        Last Result
                    </td>
                    <td>
                        Running Status
                    </td>
                    <td>
                        Status
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <asp:Repeater ID="rptTaskList" runat="server" OnItemCommand="rptTaskList_OnItemCommand">
                    <ItemTemplate>
                        <tr title=" click to show detail" onmousemove="trMouseMove(this);" name='<%#Eval("ID") %>'
                            onmouseout="trMouseOut(this)" onclick="trOnclik(this);">
                            <td>
                                <%#Eval("Sequence")%>
                            </td>
                            <td>
                                <%#Eval("TaskName") %>
                            </td>
                            <td>
                                <%#Eval("Protocol")%>
                            </td>
                            <td>
                                <%#Eval("ScheduleType")%>
                            </td>
                            <td>
                                <%#Eval("LastRunTime") is DBNull ? "" : Convert.ToDateTime(Eval("LastRunTime")).ToString("yyyy/MM/dd hh:mm:ss")%>
                            </td>
                            <td>
                                <%#Eval("NextRunTime") is DBNull ? "" : Convert.ToDateTime(Eval("NextRunTime"))<DateTime.Now ? "" : Convert.ToDateTime(Eval("NextRunTime")).ToString("yyyy/MM/dd hh:mm:ss")%>
                            </td>
                            <td style='color: <%#Eval("LastRunStatus") is DBNull?"": Convert.ToBoolean(Eval("LastRunStatus"))?"":"red"%>'>
                                <%#Eval("LastRunStatus") is DBNull?"":Convert.ToBoolean( Eval("LastRunStatus"))?"Success":"Failed" %>
                            </td>
                            <td>
                                <%# Eval("Running") is DBNull ? "Waiting" : Convert.ToBoolean(Eval("Running")) ? "Running" : "Waiting"%>
                            </td>
                            <td style='<%#Eval("Status").ToString()=="Active"?"background-color: #99cc33": "background-color: Yellow" %>'>
                                <%#Eval("Status") %>
                            </td>
                            <td align="center" onclick="cancleEvent(event)">
                                <a title="Edit" name='<%#Eval("ID") %>' style="background-image:url(../Image/Icon/edit.gif);width:16px;height:16px;display:block;" onclick="editOnclick(this)" />
                                </td>
                            <td onclick="cancleEvent(event)">
                                <asp:ImageButton ID="ibtDelete" title="Click to delete" CommandName="Delete" CommandArgument='<%#Eval("ID") %>'
                                    ImageUrl="~/Image/Icon/Remove.gif" runat="server" OnClientClick="return ConfirmDelete()" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </td>
    </tr>
</table>
<div id="dialog" title="Project Task Detail" style="display: none">
    <table border="0" class="table02" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="220px">
                Sequence :
            </td>
            <td id="tdSequence">
            </td>
            <td width="35px">
                <input type="button" id="btUp" value="↑" onclick="upClick()" />
            </td>
        </tr>
        <tr>
            <td width="80px">
                Status :
            </td>
            <td id="tdStatus">
            </td>
            <td width="35px">
                <input type="button" id="btDown" value="↓" onclick="downClick()" />
            </td>
        </tr>
        <tr>
            <td width="80px">
                Task Name :
            </td>
            <td colspan="2" id="tdTaskName">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Description :
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td id="tdDescription" colspan="3" height="150px" valign="top">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Protocol :
            </td>
            <td colspan="2" id="tdProtocol">
            </td>
        </tr>
        <tr>
            <td width="80px">
                IO :
            </td>
            <td colspan="2" id="tdIO">
            </td>
        </tr>
        <tr>
            <td width="80px">
                File Type :
            </td>
            <td colspan="2" id="tdFileType">
            </td>
        </tr>
        <tr>
            <td >
                Runtime Parameters :
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td id="tdParameters" colspan="3" height="150px" valign="top">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Message Handler Component :
            </td>
            <td colspan="2" id="tdMsgHandler">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Business Handler Component :
            </td>
            <td colspan="2" id="tdBizHandler">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Server :
            </td>
            <td colspan="2" id="tdRServer">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Folder :
            </td>
            <td colspan="2" id="tdRFolder">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Port :
            </td>
            <td colspan="2" id="tdRPort">
            </td>
        </tr>
        <tr>
            <td width="80px">
                User ID :
            </td>
            <td colspan="2" id="tdRUID">
            </td>
        </tr>
        <tr>
            <td >
                Password :
            </td>
            <td colspan="2" id="tdRPWD">
            </td>
        </tr>
        <tr>
            <td >
                Certificate :
            </td>
            <td colspan="2" id="tdRCert">
            </td>
        </tr>
        <tr>
            <td >
                Local Folder :
            </td>
            <td colspan="2" id="tdLFolder">
            </td>
        </tr>
        <tr>
            <td >
                Schedule Type :
            </td>
            <td colspan="2" id="tdScheduleType">
            </td>
        </tr>
        <tr>
            <td >
                Minutes :
            </td>
            <td colspan="2" id="tdScheduleInterval">
            </td>
        </tr>
        <tr>
            <td >
                Daily :
            </td>
            <td colspan="2" id="tdScheduleTime">
            </td>
        </tr>
        <tr>
            <td >
                Week :
            </td>
            <td colspan="2" id="tdWeek">
            </td>
        </tr>
        <tr>
            <td >
                Monthly :
            </td>
            <td colspan="2" id="tdMonthly">
            </td>
        </tr>
        
    </table>
</div>
