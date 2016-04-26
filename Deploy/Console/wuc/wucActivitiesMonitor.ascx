<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucActivitiesMonitor.ascx.cs"
    Inherits="DIMERCO.B2B.Portal.Console.wuc.wucActivitiesMonitor" %>

<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>

<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>

<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />

<script type="text/javascript">
var refreshId=0;
$(document).ready(function(){
    $("#tableActivities").rowspan(0);
    $("#tableActivities").rowspan(1);
    $("#tableActivities").rowspan(2);
    $(".tdRunFailTimes").each(function(index,element){
        var RunFailTimes =$(element).text()==""?"0":$(element).text();
        if(parseInt(RunFailTimes)>0)
        {
            $(element).closest("tr").find("td:gt(2)").css("color","red");
        }
    })
    refresh();
    var myDate=new Date();
    var refreshAt=myDate.getFullYear()+"/"+getFullDate(myDate.getMonth().toString())+"/"+getFullDate(myDate.getDay().toString())+" "+getFullDate(myDate.getHours().toString())+":"+getFullDate(myDate.getMinutes().toString())+":"+getFullDate(myDate.getSeconds().toString());
    //refreshAt=refreshAt.replace("下午","PM").replace("上午","AM");
    $("#spanTime").text("Refresh at "+refreshAt);
})

function getFullDate(str)
{
    return str.length==1?"0"+str:str;
}

function doPostBackSearch()
{
    $("#<%=txtIsRefresh.ClientID %>").val("IsRefresh");
    $("form").get(0).submit();
}
function refresh()
{
    var regIntegers=/^\d+$/;
    try{
        var strRefreshTime=$("#<%=txtMinutes.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
        var refreshTime= regIntegers.test(strRefreshTime)?parseInt(strRefreshTime):0;
        if($("#<% =cbRefresh.ClientID%>").is( ":checked" ))
        {
            if(refreshTime>0){
                refreshId=window.setInterval(doPostBackSearch,refreshTime*60*1000);
            }
            else{
                window.clearInterval(refreshId);
                alert("Input content is not positive integers");
            }    
        }
        else{
            window.clearInterval(refreshId);
        }
    }
    catch(ex)
    {
        window.clearInterval(refreshId);
        $("#spanTime").text("");
    }
}
function aLinkOnclick(e,str)
{
    $("iframe").attr("src",$(e).attr("alt"));
    var titlePage="";
    switch(str){
        case "Project":
            titlePage=str+" History : "+$(e).text();
            break;
        case "Task":
            titlePage=str+" History : "+$(e).closest("td").prev().text()+" - "+$(e).text();
            break;
    }
    $("#dialog").dialog({width:800,title:titlePage});
    changeDialogBackgroundColor();
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

<asp:TextBox ID="txtIsRefresh" runat="server" Style="display: none"></asp:TextBox>
<table border="0" cellpadding="0" cellspacing="0" class="table03">
    <tr>
        <td>
            Project :
            <asp:TextBox ID="txtProjectName" runat="server"> </asp:TextBox>
        </td>
        <td>
            Status :
            <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:CheckBox ID="cbShow" runat="server" Text="show fail only" />
        </td>
        <td>
            <asp:Button ID="btSearch" runat="server" Text="Search" OnClick="btSearch_OnClick" />
        </td>
        <td>
            <asp:CheckBox ID="cbRefresh" Checked="true" onchange="refresh()" runat="server" />Auto
            refresh every<asp:TextBox ID="txtMinutes" Width="20px" runat="server" Text="5"  oninput="onlyInputInteger(this)" onpropertychange="onlyInputInteger(this)"
                
                onchange="refresh()"></asp:TextBox>minutes
        </td>
        <td>
            <span id="spanTime"></span>
        </td>
    </tr>
</table>
<div class="divPage" width="100%" align="center">
    <div style="float: left">
        Display<asp:DropDownList ID="ddlPageSize" runat="server" onchange="ddlPageSizeOnChange(this)"
            CssClass="divPage" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_OnSelectedIndexChanged">
        </asp:DropDownList>
        Records per page
    </div>
    <table style="position: relative; left: -100px;">
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
                <asp:DropDownList ID="ddlPageIndex" runat="server" onchange="ddlPageIndexOnChange(this)"
                    CssClass="divPage" AutoPostBack="true" OnSelectedIndexChanged="ddlPageIndex_OnSelectedIndexChanged">
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
<table border="0" id="tableActivities" cellpadding="0" class="table01" cellspacing="0"
    width="100%">
    <tr class="trHead">
        <td>
            Machine
        </td>
        <td>
            Project
        </td>
        <td>
            Task
        </td>
        <td>
            Status
        </td>
        <td>
            Next Run
        </td>
        <td>
            Last Run Start
        </td>
        <td>
            Last Run End
        </td>
        <td>
            Last Run Result
        </td>
        <td>
            Last Success Time
        </td>
        <td>
            Fail Time
        </td>
    </tr>
    <asp:Repeater ID="rptactivities" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("HostMachineID")%>
                </td>
                <td>
                    <a alt='ProjectHistory.aspx?ProjectID=<%#Eval("ProjectID") %>' onclick="aLinkOnclick(this,'Project')"
                        class="aLink">
                        <%#Eval("ProjectName") %></a>
                </td>
                <td>
                    <a alt='projectTaskHistory.aspx?TaskID=<%#Eval("TaskID") %>' onclick="aLinkOnclick(this,'Task')"
                        class="aLink">
                        <%#Eval("TaskName") %></a>
                </td>
                <td>
                    <%#Eval("Running") is DBNull?"Waiting": Convert.ToBoolean(Eval("Running")) ? "Running" : "Waiting"%>
                </td>
                <td>
                    <%#Eval("NextRunTime") is DBNull ? "" : Convert.ToDateTime(Eval("NextRunTime")) < DateTime.Now ? "" : Convert.ToDateTime(Eval("NextRunTime")).ToString("yyyy/MM/dd hh:mm:ss")%>
                </td>
                <td>
                    <%#Eval("LastRunStartDT") is DBNull ? "": Convert.ToDateTime(Eval("LastRunStartDT")).ToString("yyyy/MM/dd hh:mm:ss")%>
                </td>
                <td>
                    <%#Eval("LastRunEndDT") is DBNull ? "" : Convert.ToDateTime(Eval("LastRunEndDT")).ToString("yyyy/MM/dd hh:mm:ss")%>
                </td>
                <td>
                    <%#Eval("LastRunStatus") is DBNull?"":Convert.ToBoolean( Eval("LastRunStatus"))?"Success":"Failed" %>
                </td>
                <td>
                    <%#Eval("LastSuccessDT") is DBNull ? "" : Convert.ToDateTime(Eval("LastSuccessDT")).ToString("yyyy/MM/dd hh:mm:ss")%>
                </td>
                <td class="tdRunFailTimes">
                    <%#Eval("RunFailTimes") %>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<div class="divPage" width="100%" align="center">
    <div style="float: left">
        Display<asp:DropDownList ID="ddlPageSize1" runat="server" onchange="ddlPageSizeOnChange(this)"
            CssClass="divPage" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_OnSelectedIndexChanged">
        </asp:DropDownList>
        Records per page
    </div>
    <table style="position: relative; left: -100px">
        <tr>
            <td width="30px">
                <asp:ImageButton ID="ibtGoFirst1" runat="server" ImageUrl="~/Image/Icon/GoFirst.gif"
                    OnClientClick="return goFirstOnClientClick()" OnClick="btSearch_OnClick" title="Go first" />
            </td>
            <td>
                <asp:ImageButton ID="ibtGoPrevious1" runat="server" ImageUrl="~/Image/Icon/GoPrevious.gif"
                    OnClientClick="return goPreviousOnClientClick()" OnClick="btSearch_OnClick" title="Go previous" />
            </td>
            <td>
                <asp:DropDownList ID="ddlPageIndex1" runat="server" onchange="ddlPageIndexOnChange(this)"
                    CssClass="divPage" AutoPostBack="true" OnSelectedIndexChanged="ddlPageIndex_OnSelectedIndexChanged">
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
<div id="dialog" class="divDialog">
    <iframe id="iframe" name="iframe" frameborder="0" height="500px" width="100%" src="">
    </iframe>
</div>
