<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EvalTemplate_Form.aspx.vb" Inherits="Secured_EvalTemplate_Form" %>
<%@ Register Src="~/Include/EvalTemplate.ascx" TagName="EvalTemplate" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">


<uc:EvalTemplate runat="server" ID="EvalTemplate1" />


</asp:Content>

