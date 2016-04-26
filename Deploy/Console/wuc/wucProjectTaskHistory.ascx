<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucProjectTaskHistory.ascx.cs" Inherits="DIMERCO.B2B.Portal.Console.wuc.wucProjectTaskHistory" %>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />
<script type="text/javascript">
$(document).ready(function(){
    $("#<%=txtDateTStart.ClientID %>").datepicker({dateFormat: "yy/mm/dd"});
    $("#<%=txtDateTEnd.ClientID %>").datepicker({dateFormat: "yy/mm/dd"});
})

function checkInput(){
    var DateTStart=$("#<%=txtDateTStart.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var DateTEnd=$("#<%=txtDateTEnd.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var regDate=new RegExp("^(?:(?!0000)[0-9]{4}/(?:(?:0[1-9]|1[0-2])/(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])/(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$");
    if((DateTStart==""||regDate.test(DateTStart))&&(DateTEnd==""||regDate.test(DateTEnd)))
    {
        $("#<%=txtDateTStart.ClientID %>").closest("td").css("color","");
        return true;
    }
    else{
        $("#<%=txtDateTStart.ClientID %>").closest("td").css("color","red");
        return false;
    }
}


function ddlPageIndexOnChange(e){
    var PageIndex=$(e).find("option:selected").val();
    $("select[id*='ddlPageIndex']").val(PageIndex);
}

function ddlPageSizeOnChange(e){
    var PageSize=$(e).find("option:selected").val();
    $("select[id*='ddlPageSize']").val(PageSize);
}

function goFirstOnClientClick(){
    if($("#<%=ddlPageIndex.ClientID %>").find("option:selected").prev().length==0)
    {
        return false;
    }
    else{
        $("#<%=ddlPageIndex.ClientID %>").find("option:first").attr("selected",true);
        $("#<%=ddlPageIndex1.ClientID %>").find("option:first").attr("selected",true);
        return true;
    }
}

function goLastOnClientClick(){
    if($("#<%=ddlPageIndex.ClientID %>").find("option:selected").next().length==0)
    {
        return false;
    }
    else{
        $("#<%=ddlPageIndex.ClientID %>").find("option:last").attr("selected",true);
        $("#<%=ddlPageIndex1.ClientID %>").find("option:last").attr("selected",true);
        return true;
    }
}

function goPreviousOnClientClick(){
    if($("#<%=ddlPageIndex.ClientID %>").find("option:selected").prev().length==0)
    {
        return false;
    }
    else{
        $("#<%=ddlPageIndex.ClientID %>").find("option:selected").prev().attr("selected",true);
        $("#<%=ddlPageIndex1.ClientID %>").find("option:selected").prev().attr("selected",true);
        return true;
    }
}

 function goNextOnClientClick(){
        if($("#<%=ddlPageIndex.ClientID %>").find("option:selected").next().length==0)
        {
            return false;
        }
        else{
            $("#<%=ddlPageIndex.ClientID %>").find("option:selected").next().attr("selected",true);
            $("#<%=ddlPageIndex1.ClientID %>").find("option:selected").next().attr("selected",true);
            return true;
        }
    }
</script>
<table class="table03">
<tr><td>
Date Time :
</td><td><asp:TextBox ID="txtDateTStart" class="txtTime" runat="server"></asp:TextBox>--<asp:TextBox ID="txtDateTEnd" class="txtTime" runat="server"></asp:TextBox></td>
<td><asp:Button ID="btSearch" runat="server" Text="Search" OnClientClick="return checkInput()" OnClick="btSearch_OnClick" /></td>
</tr>
</table>

<div class="divPage">
                    <div style="float: left">
                Display<asp:DropDownList ID="ddlPageSize" runat="server" onchange="ddlPageSizeOnChange(this)" CssClass="divPage"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_OnSelectedIndexChanged">
                </asp:DropDownList>
                Records per page
            </div>
            <table style="position: relative; left: 180px">
                <tr>
                    <td width="30px">
                        <asp:ImageButton ID="ibtGoFirst" runat="server" ImageUrl="~/Image/Icon/GoFirst.gif"
                            OnClientClick="return goFirstOnClientClick()" OnClick="btSearch_OnClick" title="Go first" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtGoPrevious" runat="server" ImageUrl="~/Image/Icon/GoPrevious.gif"
                            OnClientClick="return goPreviousOnClientClick()" OnClick="btSearch_OnClick" title="Go previous" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPageIndex" runat="server" onchange="ddlPageIndexOnChange(this)" CssClass="divPage"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlPageIndex_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtGoNext" runat="server" ImageUrl="~/Image/Icon/GoNext.gif"
                            OnClientClick="return goNextOnClientClick()" OnClick="btSearch_OnClick" title="Go next" />
                    </td>
                    <td width="30px" align="right">
                        <asp:ImageButton ID="ibtGoLast" runat="server" ImageUrl="~/Image/Icon/GoLast.gif"
                            OnClientClick="return goLastOnClientClick()" OnClick="btSearch_OnClick" title="Go last" />
                    </td>
                </tr>
            </table>
            </div>
<table border="0" cellpadding="0" cellspacing="0" class="table01" id="tableProjectTaskHistory" width="100%">
<tr class="trHead">

<td>Run Time</td>
<td>Run End</td>
<td>Result</td>
<td>Massage</td>
</tr>
<asp:Repeater runat="server" ID="rptProjectTaskHistory">
<ItemTemplate>
<tr>

        <td><%#Eval("RunStartDT") is DBNull ? "" : Convert.ToDateTime(Eval("RunStartDT")).ToString("yyyy/MM/dd hh:mm:ss")%></td>
        <td><%#Eval("RunEndDT") is DBNull ? "" : Convert.ToDateTime(Eval("RunEndDT")).ToString("yyyy/MM/dd hh:mm:ss")%></td>
        <td style='color:<%#Eval("RunStatus") is DBNull?"": Convert.ToBoolean(Eval("RunStatus"))?"":"red"%>'><%#Eval("RunStatus") is DBNull?"": Convert.ToBoolean(Eval("RunStatus"))?"Success":"Failed"%></td>
        <td><%#Eval("RunResult")%></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
<div class="divPage">
            <div style="float: left">
                Display<asp:DropDownList ID="ddlPageSize1" runat="server" onchange="ddlPageSizeOnChange(this)" CssClass="divPage"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_OnSelectedIndexChanged">
                </asp:DropDownList>
                Records per page
            </div>
            <table style="position: relative; left: 180px">
                <tr>
                    <td width="30px">
                        <asp:ImageButton ID="ibtGoFirst1" runat="server" ImageUrl="~/Image/Icon/GoFirst.gif"
                          OnClientClick="return goFirstOnClientClick()" OnClick="btSearch_OnClick"  title="Go first" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtGoPrevious1" runat="server" ImageUrl="~/Image/Icon/GoPrevious.gif"
                          OnClientClick="return goPreviousOnClientClick()" OnClick="btSearch_OnClick"   title="Go previous" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPageIndex1" runat="server" onchange="ddlPageIndexOnChange(this)" CssClass="divPage"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlPageIndex_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtGoNext1" runat="server" ImageUrl="~/Image/Icon/GoNext.gif"
                            OnClientClick="return goNextOnClientClick()" OnClick="btSearch_OnClick" title="Go next" />
                    </td>
                    <td width="30px" align="right">
                        <asp:ImageButton ID="ibtGoLast1" runat="server" ImageUrl="~/Image/Icon/GoLast.gif"
                            OnClientClick="return goLastOnClientClick()" OnClick="btSearch_OnClick" title="Go last" />
                    </td>
                </tr>
            </table>
    </div>

