<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucProjectLog.ascx.cs"
    Inherits="MW.DataHub.Portal.Console.wuc.wucProjectLog" %>

<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>

<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>

<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />

<script type="text/javascript">
    $(document).ready(function(){
        $("#<%=txtDateTStart.ClientID %>").datepicker({dateFormat: "yy-mm-dd"});
        $("#<%=txtDateEnd.ClientID %>").datepicker({dateFormat: "yy-mm-dd"});
    })
    function ddlPageIndexOnChange(e){
        var PageIndex=$(e).find("option:selected").val();
        $("select[id*='ddlPageIndex']").val(PageIndex);
    }
    
    function ddlPageSizeOnChange(e){
        var PageSize=$(e).find("option:selected").val();
        $("select[id*='ddlPageSize']").val(PageSize);
    }
    
    function CheckInput(){
        var DTStart=$("#<%=txtDateTStart.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
        var DTEnd=$("#<%=txtDateEnd.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
        var regDate=new RegExp("^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$");
        if(DTStart!=""&&!regDate.test(DTStart)&&DTEnd!=""&&!regDate.test(DTEnd)){
            $("#<%=txtDateTStart.ClientID %>").closest("td").css("color","red");
            return false;
        }
        else{
            return true;
        }
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
    function trOnclik(e)
    {
        var objTr=$(".tr02").get(0);
        $(objTr).attr("class","");
        $(e).attr("class","tr02");
        bindLogDetail($(e).attr("name"));
        $("#dialog").dialog({width:700,heigth:550,close: function( event, ui ){var objTr=$(".tr02").get(0);$(objTr).attr("class","");}});
        changeDialogBackgroundColor();
        var tempwidth	= $("#tdLogContent").width()+50;
		$("#dialog").dialog("option","width",tempwidth);
    }
    
    function bindLogDetail(id)
    {
        var dataPara = {"ID":id,"ProjectID":"<%=ProjectID %>"};
        var url="AjaxRequest.aspx?funtion=GetLogDetail";
        $.post( url, dataPara, function(data) {
            $("#tdDateTime").text(data.DateTime);
            $("#tdType").text(data.Type);   
            $("#tdKey").text(data.Key);    
            $("#tdLogContent").html(strJsonDeReplace(data.Content,"Html"));// function defined at Default.Master
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
<table class="table03">
    <tr>
        <td >
            Log Type :
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlLogType">
                <asp:ListItem Value="" Text="All"></asp:ListItem>
                <asp:ListItem Value="Log" Text="Log"></asp:ListItem>
                <asp:ListItem Value="Error" Text="Error"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            Date Time :
        </td>
        <td>
            <asp:TextBox runat="server" CssClass="txtTime" ID="txtDateTStart"></asp:TextBox>--<asp:TextBox ID="txtDateEnd" CssClass="txtTime"
                runat="server"></asp:TextBox>
        </td>
        <td align="left" >
            <asp:CheckBox ID="cbTodayLog" runat="server"  Checked="true"/>Today Log
        </td>
    </tr>
    <tr>
        <td >
            Key :
        </td>
        <td>
            <asp:TextBox ID="txtKey" Width="100px" runat="server"></asp:TextBox>
        </td>
        <td >
            Content :
        </td>
        <td>
            <asp:TextBox runat="server" Width="172px" ID="txtContent"></asp:TextBox>
        </td>
        <td >
            <asp:Button runat="server" ID="btSearch" Text="Search" OnClientClick="return CheckInput()"
                OnClick="btSearch_OnClick" />
        </td>
    </tr>
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
            <table border="0" cellpadding="0" cellspacing="0" width="80%" class="table01">
                <tr class="trHead">
                    <td>
                        Type
                    </td>
                    <td>
                        Date Time
                    </td>
                    <td>
                        Key
                    </td>
                    <td>
                        Content
                    </td>
                </tr>
                <asp:Repeater ID="rptProjectLog" runat="server">
                    <ItemTemplate>
                        <tr title="click to show detail" onmousemove="trMouseMove(this);" name='<%#Eval("ID") %>'
                            onmouseout="trMouseOut(this)" onclick="trOnclik(this);">
                            <td style='<%#Eval("LogType").ToString()=="Error"?"background-color: red":""%>'>
                                <%#Eval("LogType") %>
                            </td>
                            <td>
                               <%#Eval("LogTime") is DBNull ? "" : Convert.ToDateTime(Eval("LogTime")).ToString("yyyy/MM/dd hh:mm:ss")%>
                            </td>
                            <td>
                                <%#Eval("KeyValue").ToString().Length > 30 ? Eval("KeyValue").ToString().Substring(0, 30) + "..." : Eval("KeyValue")%>
                            </td>
                            <td>
                                <%#Eval("Log").ToString().Length > 20 ? Eval("Log").ToString().Substring(0, 20).Replace("<", "&lt;").Replace(">", "&gt;") + "..." : Eval("Log").ToString().Replace("<", "&lt;").Replace(">", "&gt;")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
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
<div id="dialog" title="Log Detail" style="display: none">
    <table border="0" class="table02" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="80px">
                Date Time :
            </td>
            <td id="tdDateTime">
            </td>
            <td width="35px">
                <input type="button"  id="btUp" value="↑" onclick="upClick()" />
            </td>
        </tr>
        <tr>
            <td width="80px">
                Type :
            </td>
            <td id="tdType">
            </td>
            <td width="35px">
                <input type="button"  id="btDown" value="↓" onclick="downClick()" />
            </td>
        </tr>
        <tr>
            <td width="80px">
                Key :
            </td>
            <td colspan="2" id="tdKey">
            </td>
        </tr>
        <tr>
            <td width="80px">
                Content :
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td id="tdLogContent" colspan="3" height="150px" valign="top" >
                
            </td>
        </tr>
        
    </table>
    
</div>
