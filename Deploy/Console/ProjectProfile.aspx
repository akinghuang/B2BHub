<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ProjectProfile.aspx.cs" Inherits="DIMERCO.B2B.Portal.Console.ProjectProfile1" Title="Untitled Page" ValidateRequest="false" %>
<%@ Register Src="~/Console/wuc/wucProjectProfile.ascx" TagName="wucProjectProfile" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:wucProjectProfile ID="wucProjectProfile" runat="server" />
</asp:Content>
