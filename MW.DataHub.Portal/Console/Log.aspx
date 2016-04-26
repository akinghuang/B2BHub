<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="MW.DataHub.Portal.Console.Log1" Title="MilkyWay DataHub" %>
<%@ Register Src="~/Console/wuc/wucLog.ascx" TagName="wucLog" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucLog ID="wucLog" runat="server" />
</asp:Content>
