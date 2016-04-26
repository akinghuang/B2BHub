<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ChangeProfile.aspx.cs" Inherits="MW.DataHub.Portal.Console.ChangeProfile" Title="Untitled Page" ValidateRequest="false" %>
<%@ Register Src="~/Console/wuc/wucChangeProfile.ascx" TagName="wucChangeProfile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:wucChangeProfile ID="wucChangeProfile" runat="server" />
</asp:Content>
