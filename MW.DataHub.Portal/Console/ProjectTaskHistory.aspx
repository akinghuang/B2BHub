<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ProjectTaskHistory.aspx.cs" Inherits="MW.DataHub.Portal.Console.ProjectTaskHistory" Title="Untitled Page" %>
<%@ Register src="wuc/wucProjectTaskHistory.ascx" tagname="wucProjectTaskHistory" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucProjectTaskHistory ID="wucProjectTaskHistory1" runat="server" />
</asp:Content>
