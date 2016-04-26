<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="TaskProfile.aspx.cs" Inherits="MW.DataHub.Portal.Console.TaskProfile1" Title="Untitled Page"  ValidateRequest="false"%>
<%@ Register Src="~/Console/wuc/wucTaskProfile.ascx" TagName="wucTaskProfile" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucTaskProfile ID="wucTaskProfile" runat="server" />

</asp:Content>
