<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucPayContHeader.ascx.vb" Inherits="Include_wucPayHeader" %>


<style type="text/css">

.table th, .table td { 
     border-top: none !important; 
     border: 1px dotted gray;
     border-left:none;
 }
 
</style>


<div class="row">
    <%--<h3>Payroll Details</h3>--%>
    <div class="panel panel-default" style="margin-bottom:10px;">
        <div class="table-responsive">
            <table class="table table-condensed"> 
                <tbody> 
                <tr> 
                    <td style="width:15%;text-align:left;"><strong>Transaction No.</strong></td> 
                    <td style="width:35%;"><asp:label ID="lblCode" runat="server" class="col-md-12 control-label" /></td>
                    <td style="width:15%;text-align:left;"><strong>Applicable Month</strong></td> 
                    <td style="width:35%;"><asp:label ID="lblMonthDesc" runat="server" class="col-md-12 control-label" /></td>
                </tr> 
                <tr> 
                    <td style="text-align:left;"><strong>Company Name</strong></td> 
                    <td ><asp:label ID="lblCompanyName" runat="server" class="col-md-12 control-label" /></td>
                    <td style="text-align:left;"><strong>Applicable Year</strong></td> 
                    <td ><asp:label ID="lblApplicableYear" runat="server" class="col-md-12 control-label" /></td>
                </tr> 
                </tbody> 
            </table> 
        </div>
    </div>
</div>
      

