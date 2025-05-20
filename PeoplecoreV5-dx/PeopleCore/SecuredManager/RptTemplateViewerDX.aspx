<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptTemplateViewerDX.aspx.vb" Inherits="Secured_RptTemplateViewerDX" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">    
        function btShowReport_Click(s, e) {
            callbackPanel.PerformCallback();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" ID="ScriptManager2"></asp:ScriptManager>    
        <center><h4><asp:Label runat="server" ID="lbl" Font-Names="Arial" /></h4></center>
        <dx:ASPxButton ID="lnkPreview" runat="server" Text="Preview" AutoPostBack="false" CssClass="btn btn-primary">
            <ClientSideEvents Click="btShowReport_Click" />
        </dx:ASPxButton>
        <dx:ASPxCallbackPanel ID="callbackPanel" runat="server" ClientInstanceName="callbackPanel" Width="100%" oncallback="callbackPanel_Callback">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">                    
                    <asp:Checkbox ID="txtIsView" runat="server" style="visibility:hidden; position:absolute;"></asp:Checkbox> 
                    <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" runat="server" ToolbarMode="Ribbon"></dx:ASPxDocumentViewer>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>
    </form>
</body>
</html>
