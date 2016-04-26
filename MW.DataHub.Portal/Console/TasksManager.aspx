<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="TasksManager.aspx.cs" Inherits="MW.DataHub.Portal.Console.TaskManager" Title="Untitled Page" %>
<%@ Register Src="~/Console/wuc/wucTaskManager.ascx" TagName="wucTaskManager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucTaskManager ID="wucTaskManager" runat="server" ></uc1:wucTaskManager>
</asp:Content>
