﻿<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="RptTemplateViewerDynamic.aspx.vb" Inherits="Secured_rptTemplateViewerDynamic" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">
    function GetRecord(source, eventArgs) {
        document.getElementById('<%= hifNo.ClientID %>').value = eventArgs.get_value();
    }
</script>
        
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <fieldset class="form" id="fsMain">
                        <div  class="form-horizontal">
                            <asp:Panel runat="server" ID="pForm" />
                            <asp:HiddenField runat="server" ID="hifNo" />
                            <br />
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space"></label>
                                <div class="col-md-6">            
                                    <asp:Button runat="server" ID="lnkPreview" CssClass="btn btn-primary submit fsMain" Text="Preview" OnClick="lnkPreview_Click" />                   
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
            <rsweb:ReportViewer ID="rviewer" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" Height="543px" >
            <%--<rsweb:ReportViewer ID="rviewer" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" Height="543px" ShowPrintButton="true" >--%>
            </rsweb:ReportViewer>
    </div>

</div>

</asp:Content> 

