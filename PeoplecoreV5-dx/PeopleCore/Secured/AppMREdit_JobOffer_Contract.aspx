<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="AppMREdit_JobOffer_Contract.aspx.vb" Inherits="Secured_AppMREdit_JobOffer_Contract" Theme="PCoreStyle" %>

<%@ Register Src="~/Include/JobOffer.ascx" TagName="JobOffer" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">


<uc:JobOffer runat="server" ID="JobOffer1" />


</asp:Content>

