<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucPEReviewMainHeader.ascx.vb" Inherits="Include_wucPEReviewMainHeader" %>


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
                    <td style="width:15%;text-align:left;"><strong>Transaction No.</strong></td> 
                    <td style="width:35%;"><asp:label ID="lblCode" runat="server" class="col-md-12 control-label" /></td>
                    <td style="width:15%;text-align:left;"><strong>Performance Period Type</strong></td> 
                    <td style="width:35%;"><asp:label ID="lblPEPeriodDesc" runat="server" class="col-md-12 control-label" /></td>
                </tr> 
                <tr> 
                    <td style="text-align:left;"><strong>Appraisal Name</strong></td> 
                    <td ><asp:label ID="lblPEReviewMainDesc" runat="server" class="col-md-12 control-label" /></td>
                    <td style="text-align:left;"><strong>Performance Cycle</strong></td> 
                    <td ><asp:label ID="lblPECycleDesc" runat="server" class="col-md-12 control-label" /></td>
                </tr>
                </tbody> 
            </table> 
        </div>
    </div>
</div>