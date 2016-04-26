<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ProjectHistory.aspx.cs" Inherits="MW.DataHub.Portal.Console.ProjectHistory" Title="Untitled Page" %>

<%@ Register src="~/Console/wuc/wucProjectHistory .ascx" tagname="wucProjectHistory" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:wucProjectHistory ID="wucProjectHistory1" runat="server" />
    
</asp:Content>
