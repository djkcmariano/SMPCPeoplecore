<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucDTRDetailHeader.ascx.vb" Inherits="Include_wucDTRDetailHeader" %>


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
                    <td style="width:10%;text-align:left;"><strong>Employee Name :</strong></td> 
                    <td style="width:20%;"><asp:label ID="lblFullName" runat="server" class="col-md-12 control-label" /></td>
                    <td style="width:10%;text-align:left;"><strong>Shift :</strong></td> 
                    <td style="width:20%;"><asp:label ID="lblShiftCode" runat="server" class="col-md-12 control-label" /></td>
                    <td style="width:10%;text-align:left;"><strong>Working Hrs :</strong></td> 
                    <td style="width:20%;"><asp:label ID="lblWorkingHrs" runat="server" class="col-md-12 control-label" /></td>
                </tr>
                <tr> 
                    <td style="width:10%;text-align:left;"><strong>Employee No. :</strong></td> 
                    <td style="width:20%;"><asp:label ID="lblEmployeeCode" runat="server" class="col-md-12 control-label" /></td>
                    <td style="width:10%;text-align:left;"><strong>Day Type :</strong></td> 
                    <td style="width:20%;"><asp:label ID="lblDayTypeCode" runat="server" class="col-md-12 control-label" /></td>
                    <td style="width:10%;text-align:left;"><strong>Overtime :</strong></td> 
                    <td style="width:20%;"><asp:label ID="lblOvt" runat="server" class="col-md-12 control-label" /></td>
                </tr> 
                <tr> 
                    <td style="text-align:left;"><strong>DTR Date :</strong></td> 
                    <td ><asp:label ID="lblDTRDate" runat="server" class="col-md-12 control-label" /></td>
                    <td style="text-align:left;"><strong>Req. Hrs. :</strong></td> 
                    <td ><asp:label ID="lblHrs" runat="server" class="col-md-12 control-label" /></td>
                    <td style="text-align:left;"><strong>Leave Hrs :</strong></td> 
                    <td ><asp:label ID="lblLeaveHrs" runat="server" class="col-md-12 control-label" /></td>
                </tr> 
                </tbody> 
            </table> 
        </div>
    </div>
</div>