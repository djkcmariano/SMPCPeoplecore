<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfAppMREdit_EvalTemplateForm.aspx.vb" Inherits="SecuredManager_SelfAppMREdit_EvalTemplateForm" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/StandardTemplate.ascx" TagName="StandardTemplate" TagPrefix="uc" %>
<%@ Register Src="~/Include/HeaderInfo.ascx" TagName="HeaderInfo" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc1:HeaderInfo runat="server" ID="HeaderInfo1" />
<uc:StandardTemplate runat="server" ID="StandardTemplate1" />


</asp:Content>


