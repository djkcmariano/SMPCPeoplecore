<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucDTRHeader.ascx.vb" Inherits="Include_wucDTRHeader" %>


<style type="text/css">

.table th, .table td { 
     border-top: none !important; 
     border: 1px dotted gray;
     border-left:none;
 }
 
</style>


<div class="row">
    <%--<h3>DTR Details</h3>--%>
    <div class="panel panel-default" style="margin-bottom:10px;">
        <div class="table-responsive">
            <table class="table table-condensed"> 
                <tbody> 
                <tr> 
                    <td style="width:15%;text-align:left;"><strong>DTR No.</strong></td> 
                    <td style="width:35%;"><asp:label ID="lblDTRCode" runat="server" class="col-md-12 control-label" /></td>
                    <td style="width:15%;text-align:left;"><strong>Payroll Group</strong></td> 
                    <td style="width:35%;"><asp:label ID="lblPayClassDesc" runat="server" class="col-md-12 control-label" /></td>
                </tr> 
                <tr> 
                    <td style="text-align:left;"><strong>Cut Off Date</strong></td> 
                    <td ><asp:label ID="lblDTRCutoff" runat="server" class="col-md-12 control-label" /></td>
                    <td style="text-align:left;"><strong>Payroll Type</strong></td> 
                    <td ><asp:label ID="lblPayTypeDesc" runat="server" class="col-md-12 control-label" /></td>
                </tr> 
                </tbody> 
            </table> 
        </div>
    </div>
</div>