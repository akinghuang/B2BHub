<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ProjectNotification.aspx.cs" Inherits="MW.DataHub.Portal.Console.ProjectNotification" ValidateRequest="false" Title="Untitled Page" %>
<%@ Register src="wuc/wucNotification.ascx" tagname="wucNotification" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucNotification ID="wucNotification1" runat="server" />
</asp:Content>
