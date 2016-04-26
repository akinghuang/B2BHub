<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="MW.DataHub.Portal.Default1" Title="MilkyWay DataHub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    var UserID=<%=UserID %>;
    var UserName="<%=UserName %>";
    $(document).ready(function(){
        $(".menu_ul li").click(function(){
            $(".menu_li").attr("class","");
            $(this).attr("class","menu_li");
            var menuSelect=$(this).find("a").text();
            switch(menuSelect){
                case "Monitor":
                    $("#iframe").attr("src","Console/ActivitiesMonitor.aspx");
                    break;
                case "Projects":
                    $("#iframe").attr("src","Console/Projectmanager.aspx");
                    break;
                case "Setting":
                    $("#iframe").attr("src","Console/UserProjectManager.aspx");
                    break;
            } 
        })
        if(UserID>0)
        {
            $(".menu_li:gt(0)").attr("class","");
            $(".menu_li:eq(0)").attr("class","menu_li");
            $("#lbtLog").show();
            $("#iframe").attr("src","Console/ActivitiesMonitor.aspx");
            $("#divMenu").show();
            $("#divUserName").text(UserName);
            $("#divUserName").show();
        }
        else{
            logClick();
        }
    })
    
    function logClick(win){
        if(win==null||win==undefined)
        {
            window.location.href="Console/Log.aspx";
        }
        else{
            window.location.href="Log.aspx";
        }   
    }
    
    function urlChange(url)
    {
         $("#iframe").attr("src",url);
    }
    
    </script>

    <style type="text/css">
        .body
        {
            background-color: #b24926;
        }
        body
        {
            background-color: #0769AD;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 0 20px 0 20px;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20%; padding-left: 40px;">
                    <img src="Image/Images/MWlogo.png" alt="MilkyWay DataHub" />
                </td>
                <td style="padding-left: 20px; position: relative; top: 17px;" valign="middle">
                    <span class="title01"></span>
                </td>
                <td width="400px"></td>
            </tr>
            <tr><td style="height: 10px; text-align: right; padding-right: 200px;" colspan="3">
            <span id="divUserName" style="display: none;color:White;" ></span>&nbsp;&nbsp;<a class="a02" 
                onmousemove="this.className='a01'" onmouseout="this.className='a02'" style="display: none;"
                id="lbtLog" onclick="logClick()">[Logoff]</a>
        </td></tr>
        </table>
        <div id="divMenu" style="height: 60px">
            <ul class="menu_ul" style="list-style-type: none; border: solid 1 #bf775f; background-color: #05568D;
                box-shadow: 0px 0px 5px rgba(1,1,1,0.7); height: 60px;">
                <li class="menu_li"><a >Monitor</a></li><li><a >Projects</a></li><li>
                    <a >Setting</a></li></ul>
        </div>
        <div id="divLink">
        </div>
        <div style="display: block; height: 700px; background-color: #fff; border-top: solid 1 black;"
            valign="top">
            <iframe id="iframe" frameborder="0" enableviewstate="false" src="Console/Log.aspx" width="100%" height="100%"
                name="Top"></iframe>
        </div>
    </div>
</asp:Content>
