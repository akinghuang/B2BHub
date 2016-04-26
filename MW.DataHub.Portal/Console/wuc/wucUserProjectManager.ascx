<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucUserProjectManager.ascx.cs"
    Inherits="MW.DataHub.Portal.Console.wuc.wucUserProjectManager" %>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
<script type="text/javascript" src="../../Src/JS/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.css" />
<link type="text/css" rel="Stylesheet" href="../../Src/JS/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />

<script type="text/javascript">

function trOndblclik(e){
    var objTr=$(".tr02").get(0);
    $(objTr).attr("class","");
    $(e).attr("class","tr02");
    $("#dialog").dialog({width:400,title:"Modification",close: function( event, ui ){var objTr=$(".tr02").get(0);$(objTr).attr("class","");}}).parent().appendTo("form");
    changeDialogBackgroundColor();
    var id=$(e).attr("name");
    BindUser(id);
}
function btAddClick(){
    $("#dialog").dialog({width:400,title:"Add New"}).parent().appendTo("form");
    $("#<%=hfID.ClientID %>").val("");
    $("#<%=textuserID.ClientID %>").val("");
    $("#<%=textuserName.ClientID %>").val("");   
    $("#<%=txtDesc.ClientID %>").val("");    
    $("#<%=txtPassword.ClientID %>").val("");
}

function BindUser(id){
    var data = {"ID":id};
        var url="AjaxRequest.aspx?funtion=GetUserListByID";
        $.post( url, data, function( data ) {
            $("#<%=hfID.ClientID %>").val(id);
            $("#<%=textuserID.ClientID %>").val(data.UserID);
            $("#<%=textuserName.ClientID %>").val(data.UserName);   
            $("#<%=txtDesc.ClientID %>").val(data.Desc);    
            $("#<%=txtPassword.ClientID %>").val(data.Password);
            $("#<%=rbStatusList.ClientID %>").val(data.Status);
        }, "json");
}

function checkInput(){
    var userID=$("#<%=textuserID.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var password=$("#<%=txtPassword.ClientID %>").val().replace(/(^\s*)|(\s*$)/g,"");
    var i=0;
    if(userID==""){
        $("#<%=textuserID.ClientID %>").closest("td").css("color","red");
        i++;
    }
    else{
        $("#<%=textuserID.ClientID %>").closest("td").css("color","");
    }
    if(password==""){
        $("#<%=txtPassword.ClientID %>").closest("td").css("color","red");
        i++;
    }
    else{
        $("#<%=txtPassword.ClientID %>").closest("td").css("color","");
    }
    if(i>0){
        return false;
    }
    else{
        return true;
    }
}
</script>

<table class="table03">
    <tr>
        <td>
            User ID :<asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
        </td>
        <td>
            Name :<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </td>
        <td>
            Status :<asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btSearch" runat="server" Text="Search" OnClick="btSearch_OnClick" />
        </td>
        <td>
            <input type="button" id="btAdd" value="New Add" onclick="btAddClick()" />
        </td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" width="80%" class="table01">
    <tr class="trHead">
        <td>
            User ID
        </td>
        <td>
            Name
        </td>
        <td>
            Status
        </td>
        <td>
            Last Login IP
        </td>
        <td>
            Last Login Date
        </td>
        <td>
        </td>
    </tr>
    <asp:Repeater ID="rptUserManager" runat="server" OnItemCommand="rptUserManager_OnItemCommand">
        <ItemTemplate>
            <tr title="Double click to edit" onmousemove="trMouseMove(this);" name='<%#Eval("ID") %>'
                onmouseout="trMouseOut(this)" ondblclick="trOndblclik(this);">
                <td>
                    <%#Eval("UserID") %>
                </td>
                <td>
                    <%#Eval("FullName")%>
                </td>
                <td style='<%#Eval("Status").ToString()=="Active"?"background-color: #99cc33":"background-color: Yellow" %>'>
                    <%#Eval("Status") %>
                </td>
                <td>
                    <%#Eval("LastLoginIP") %>
                </td>
                <td>
                   <%#Eval("LastLoginDT") is DBNull ? "" : Convert.ToDateTime(Eval("LastLoginDT")).ToString("yyyy/MM/dd hh:mm:ss")%>
                </td>
                <td>
                    <asp:ImageButton ID="ibtDelete" ImageUrl="~/Image/Icon/Remove.gif" CommandName="Delete"
                        CommandArgument='<%#Eval("ID") %>' runat="server" OnClientClick="return ConfirmDelete()"
                        title="Click to delete" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<div id="dialog" style="display: none">
    <asp:HiddenField ID="hfID" runat="server" />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td height="30px">
                User ID :
            </td>
            <td>
                <asp:TextBox ID="textuserID" runat="server"></asp:TextBox>*(Not null)
            </td>
        </tr>
        <tr>
            <td height="30px">
                Name :
            </td>
            <td>
                <asp:TextBox ID="textuserName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="30px">
                Password :
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>*(Not null)
            </td>
        </tr>
        <tr>
            <td>
                Status :
            </td>
            <td>
                <asp:RadioButtonList ID="rbStatusList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Active" Selected="True" Value="Active"></asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                Description :
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDesc" Rows="4" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_OnClick" OnClientClick="return checkInput()" />
            </td>
        </tr>
    </table>
</div>
