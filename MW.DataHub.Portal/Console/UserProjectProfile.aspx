﻿<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="UserProjectProfile.aspx.cs" Inherits="MW.DataHub.Portal.Console.UserProjectProfile" Title="Untitled Page" %>
<%@ Register src="wuc/wucUserProjectProfile.ascx" tagname="wucUserProjectProfile" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucUserProjectProfile ID="wucUserProjectProfile1" runat="server" />
</asp:Content>
