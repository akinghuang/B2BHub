<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucChangeManager.ascx.cs"
    Inherits="DIMERCO.B2B.Portal.Console.wuc.wucChangeManager" %>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />

<script type="text/javascript">
var ProjectID=<%=ProjectID %>;
function btnNewChangingClick()
{
   $(parent.document).find("iframe[id*='iframe1']").attr("src","ChangeProfile.aspx?ProjectID="+ProjectID);
   
}
function trOndblclik(e)
{
   $(parent.document).find("iframe[id*='iframe1']").attr("src","ChangeProfile.aspx?ProjectChangeID="+$(e).attr("name")+"&ProjectID="+ProjectID);
   
}

function editOnclick(e)
{
   $(parent.document).find("iframe[id*='iframe1']").attr("src","ChangeProfile.aspx?ProjectChangeID="+$(e).attr("name")+"&ProjectID="+ProjectID);
}

function trOnclik(e)
{
    var objTr=$(".tr02").get(0);
    $(objTr).attr("class","");
    $(e).attr("class","tr02");
    bindProjectChangeDetail($(e).attr("name"));
    $("#dialog").dialog({width:900,height:600,close: function( event, ui ){var objTr=$(".tr02").get(0);$(objTr).attr("class","");}});
    changeDialogBackgroundColor();
    var tempwidth	= $("#tdContent").width()+50;
	$("#dialog").dialog("option","width",tempwidth);
}

function bindProjectChangeDetail(id)
{
    var dataPara = {"ID":id};
    var url="AjaxRequest.aspx?funtion=GetProjectChangeDetail";
    $.post( url, dataPara, function(data) {
        $("#tdSubject").text(data.Subject);
        $("#tdOwner").text(data.Owner);
        $("#tdSource").text(strJsonDeReplace(data.Suorce,"")); 
        $("#tdContent").html(strJsonDeReplace(data.Content,"Html"));// function defined at Default.Master
           
        $("#tdStartDT").text(data.StartDT);  
        $("#tdTargetDT").text(data.TargetDT);  
        $("#tdCompleteDT").text(data.CompleteDT);  
        $("#tdOnlineDT").text(data.OnlineDT);  
        var SuorceCode="<a href='"+strJsonDeReplace(data.SourceCode,"")+"' title='Click to download' style='background-image: url(../Image/Icon/download.gif);width: 24px; height: 18px; display: block;'></a>";
        $("#tdSourceCode").html(SuorceCode);
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

<table border="0">
    <tr>
        <td>
            <input type="button" id="btNewChanging" value="New Changing" onclick=" btnNewChangingClick()" />
        </td>
    </tr>
    <tr>
        <td>
            <table class="table01" border="0" cellpadding="0" cellspacing="0" align="center"
                width="100%">
                <tr class="trHead">
                    <td>
                        Create date
                    </td>
                    <td>
                        Subject
                    </td>
                    <td>
                        Owner
                    </td>
                    <td>
                        Source
                    </td>
                    <td>
                        Start date
                    </td>
                    <td>
                        Target date
                    </td>
                    <td>
                        Complete date
                    </td>
                    <td>
                        Online date
                    </td>
                    <td>
                        Source Code
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <asp:Repeater ID="RptProjectChange" runat="server" OnItemCommand="RptProjectChange_OnItemCommand"
                    OnItemDataBound="RptProjectChange_OnItemDataBound">
                    <ItemTemplate>
                        <tr title=" click to show detail" onmousemove="trMouseMove(this);" name='<%#Eval("ID") %>'
                            onmouseout="trMouseOut(this)" onclick="trOnclik(this);">
                            <td>
                                <%#Eval("CreatedDT") is DBNull ? "" : Convert.ToDateTime(Eval("CreatedDT")).ToString("yyyy/MM/dd hh:mm:ss") %> 
                            </td>
                            <td>
                                <%#Eval("ChangeTitle")%>
                            </td>
                            <td>
                                <%#Eval("Owner") %>
                            </td>
                            <td>
                                <%#Eval("ChangeSuorce")%>
                            </td>
                            <td>
                                <%#Eval("UpdatedDT") is DBNull ? "" : Convert.ToDateTime(Eval("StartDT")).ToString("yyyy/MM/dd")%>
                            </td>
                            <td>
                                <%#Eval("TargetDT") is DBNull ? "" : Convert.ToDateTime(Eval("TargetDT")).ToString("yyyy/MM/dd")%>
                            </td>
                            <td>
                                <%#Eval("CompleteDT") is DBNull ? "" : Convert.ToDateTime(Eval("CompleteDT")).ToString("yyyy/MM/dd")%>
                            </td>
                            <td>
                                <%#Eval("OnlineDT") is DBNull ? "" : Convert.ToDateTime(Eval("OnlineDT")).ToString("yyyy/MM/dd")%>
                            </td>
                            <td onclick="cancleEvent(event)">
                                <asp:ImageButton ID="ibtDownload" runat="server" ImageUrl="~/Image/Icon/download.gif"
                                    title="Click to download" CommandArgument='<%#Eval("SourceCode") %>' CommandName="Download" />
                            </td>
                            <td align="center" onclick="cancleEvent(event)">
                                <a title="Edit" name='<%#Eval("ID") %>' style="background-image: url(../Image/Icon/edit.gif);
                                    width: 16px; height: 16px; display: block;" onclick="editOnclick(this)" />
                            </td>
                            <td onclick="cancleEvent(event)">
                                <asp:ImageButton ID="ibtDelete" title="Click to delete" CommandArgument='<%#Eval("ID") %>'
                                    runat="server" ImageUrl="~/Image/Icon/Remove.gif" CommandName="Delete" OnClientClick="return ConfirmDelete()" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </td>
    </tr>
</table>

<div id="dialog" title="Project Change Detail" style="display: none">
    <table border="0" class="table02" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="180px">
                Subject :
            </td>
            <td id="tdSubject">
            </td>
            <td width="35px">
                <input type="button"  id="btUp" value="↑" onclick="upClick()" />
            </td>
        </tr>
        <tr>
            <td width="80px">
                Owner :
            </td>
            <td id="tdOwner">
            </td>
            <td width="35px">
                <input type="button"  id="btDown" value="↓" onclick="downClick()" />
            </td>
        </tr>
        <tr>
            <td width="80px">
                Source :
            </td>
            <td colspan="2" id="tdSource">
            </td>
        </tr>
       <tr>
            <td width="80px">
                Content :
            </td>
            <td colspan="2" >
            </td>
        </tr>
        <tr>
            <td id="tdContent" colspan="3" height="150px" valign="top" >
                
            </td>
        </tr>
        <tr>
            <td width="80px">
                Start Date :
            </td>
            <td colspan="2" id="tdStartDT">
            </td>
        </tr>
        <tr>
            <td width="80px">
               Target Date :
            </td>
            <td colspan="2" id="tdTargetDT">
            </td>
        </tr>
        <tr>
            <td width="80px">
               Complete Date :
            </td>
            <td colspan="2" id="tdCompleteDT">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Online Date :
            </td>
            <td colspan="2" id="tdOnlineDT">
            </td>
        </tr>
        
        <tr>
            <td width="80px">
                Source Code :
            </td>
            <td colspan="2" id="tdSourceCode" >
            </td>
        </tr>
        
    </table>
    
</div>
