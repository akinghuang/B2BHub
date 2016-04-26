<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="SelectFrame.aspx.cs" Inherits="DIMERCO.B2B.Portal.Console.SelectFrame1" Title="Untitled Page" %>
<%@ Register Src="~/Console/wuc/wucSelectFrame.ascx" TagName="wucSelectFrame" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucSelectFrame ID="wucSelectFrame" runat="server" />

</asp:Content>
