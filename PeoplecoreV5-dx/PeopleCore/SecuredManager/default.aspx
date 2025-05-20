<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="SecuredManger_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <asp:Literal runat="server" ID="lContent" />
    <br />
    <asp:Panel runat="server" ID="pDashboard">
        
    </asp:Panel>  
</asp:Content>

