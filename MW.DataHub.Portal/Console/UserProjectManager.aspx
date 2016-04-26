<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="UserProjectManager.aspx.cs" Inherits="MW.DataHub.Portal.Console.UserProjectManager" Title="Untitled Page" %>
<%@ Register src="wuc/wucUserProjectManager.ascx" tagname="wucUserProjectManager" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucUserProjectManager ID="wucUserProjectManager1" runat="server" />
</asp:Content>
