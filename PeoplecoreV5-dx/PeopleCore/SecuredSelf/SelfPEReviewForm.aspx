<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfPEReviewForm.aspx.vb" Inherits="Secured_SelfPEReviewForm" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/PEForm.ascx" TagName="PEForm" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc:PEForm runat="server" ID="PEForm1" />

</asp:Content>

