<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucLog.ascx.cs" Inherits="DIMERCO.B2B.Portal.Console.wuc.wucLog" %>
<style type="text/css">
    .div01
    {
        border:solid 2px #FFF;
    }
   
    .div03{width: 750px;
        height: 180px;
        position: absolute;
        top: 40%;
        left: 10%;}
    body
    {
        background-color: #FFFFcc;
        background: url(../Image/Images/Login03.jpg) ;
        background-size: 100% 1000px}
   </style>
<script type="text/javascript">
    $(document).ready(function(){
        $("#btnLogIn").focus();
    })
    function checkUserLogin(){
           var stateLog="";
           var userName=$("input[id*='txtUserName']").val().replace(/(^\s*)|(\s*$)/g,"");
           var passWord=$("input[id*='txtPassWord']").val().replace(/(^\s*)|(\s*$)/g,"");
           var data = {"UserName":userName,"PassWord":passWord};
           
           var url="AjaxRequest.aspx?funtion=CheckLog";
           $.post( url, data, function( data ) {
               if(data.state=="1")
               {
                   $("#spanUserName").hide();
                   $("#spanPassWord").hide();
                   window.location.href="../Default.aspx";
               }
               else if(data.state=="2")
               {
                   $("#spanUserName").show();
                   $("#spanPassWord").hide();
               }
               else if(data.state=="3")
               {
                   $("#spanUserName").hide();
                   $("#spanPassWord").show();
               }
           }, "json");
        
    }
    
    function textOnKeyDown(event)
    {
        event=window.event?window.event:event;
        if(event.keyCode==13)
        {
            checkUserLogin();
        }
    }
</script>


<div class="div03">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 280px; padding-left: 40px;">
                <img src="../Image/Images/logo.png" width="250px" height="70px" alt="" />
            </td>
            <td>
            <div class="div01">
                <div style="height: 20px">
                </div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr height="50px">
                            <td style="padding-left: 50px; color:#FFF">
                                User Name :
                            </td>
                            <td width="155px">
                                <asp:TextBox ID="txtUserName" Width="155" runat="server"  onkeydown="textOnKeyDown(event)"/>
                            </td>
                            <td  width="130px">
                            <span id="spanUserName" style="color: Red; display: none; font-size: 12px">*Incorrect username</span>
                            </td>
                        </tr>
                        <tr height="50px">
                            <td style="padding-left: 50px; color:#FFF">
                                Password :
                            </td>
                            <td width="155px">
                                <asp:TextBox ID="txtPassWord" runat="server" Width="155" TextMode="Password" onkeydown="textOnKeyDown(event)" />
                            </td>
                            <td width="130px">
                            <span id="spanPassWord" style="color: Red; display: none; font-size: 12px">*Incorrect password</span>
                            </td>
                        </tr>
                        <tr height="50px">
                            <td>
                            </td>
                            <td align="right">
                                <input type="button" value="Login" id="btnLogIn" onclick="checkUserLogin()" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</div>
