<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptTemplateViewer.aspx.vb" Inherits="SecuredSelf_RptTemplateViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        <asp:ScriptManager runat="server" ID="ScriptManager2"></asp:ScriptManager>
        <center><h4><asp:Label runat="server" ID="lbl" Font-Names="Arial" /></h4></center>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" Height="543px" ShowPrintButton="true" />        
    </div>
    </form>
</body>
</html>
