<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ChangeManager.aspx.cs" Inherits="MW.DataHub.Portal.Console.ChangeManager" Title="Untitled Page" %>
<%@ Register Src="~/Console/wuc/wucChangeManager.ascx" TagName="wucChangeManager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:wucChangeManager ID="wucChangeManager" runat="server" />
</asp:Content>
