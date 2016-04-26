<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ActivitiesMonitor.aspx.cs" Inherits="MW.DataHub.Portal.Console.ActivitiesMonitor" Title="Untitled Page" %>

<%@ Register src="wuc/wucActivitiesMonitor.ascx" tagname="wucActivitiesMonitor" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucActivitiesMonitor ID="wucActivitiesMonitor1" runat="server" />
    </asp:Content>
