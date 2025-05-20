<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppMREdit_EvalTemplateForm.aspx.vb" Inherits="Secured_AppMREdit_EvalTemplateForm" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/StandardTemplate.ascx" TagName="StandardTemplate" TagPrefix="uc" %>
<%@ Register Src="~/Include/EvalTemplate.ascx" TagName="EvalTemplate" TagPrefix="uc2" %>
<%@ Register Src="~/Include/HeaderInfo.ascx" TagName="HeaderInfo" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc1:HeaderInfo runat="server" ID="HeaderInfo1" />
<uc2:EvalTemplate runat="server" ID="EvalTemplate1" />



</asp:Content>

