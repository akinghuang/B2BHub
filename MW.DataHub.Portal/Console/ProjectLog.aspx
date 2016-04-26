<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ProjectLog.aspx.cs" Inherits="MW.DataHub.Portal.Console.ProjectLog" Title="Untitled Page" %>
<%@ Register src="wuc/wucProjectLog.ascx" tagname="wucProjectLog" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucProjectLog ID="wucProjectLog1" runat="server" />
</asp:Content>
