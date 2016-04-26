<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ProjectManager.aspx.cs" Inherits="DIMERCO.B2B.Portal.Console.ProjectManager1" Title="Untitled Page" %>
<%@ Register Src="~/Console/wuc/wucProjectManager.ascx" TagName="wucProjectManager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:wucProjectManager ID="wucProjectManager" runat="server" />
</asp:Content>
