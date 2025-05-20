<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PEReviewTab.ascx.vb" Inherits="Include_Tab" %>

<style type="text/css">
    .table th, .table td { 
         border-top: none !important; 
         border: 1px dotted gray;
         border-left:none;
     }
</style>


<div class="page-content-wrap">         
<div class="col-md-12 bhoechie-tab-container">   
<%--<div class="row" >
        <div class="col-md-2" style="padding-top:22px;padding-bottom:0px;">
                <center>
                    <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                    <br />            
                </center>  
        </div>
        <div class="col-md-10" style="padding:20px 20px 0px 20px;" >

            <div class="panel panel-default">
                <table class="table table-condensed"> 
                    <tbody> 
                    <tr> 
                        <td style="width:15%;text-align:left;"><strong>Employee No.</strong></td> 
                        <td style="width:35%;"><asp:label ID="lblPayCode" runat="server" class="col-md-8 control-label" /></td>
                        <td style="width:15%;text-align:left;"><strong>Hired Date</strong></td> 
                        <td style="width:35%;"><asp:label ID="lblPayDate" runat="server" class="col-md-8 control-label" /></td>
                    </tr> 
                    <tr> 
                        <td style="text-align:left;"><strong>Employee Name</strong></td> 
                        <td ><asp:label ID="lblPayClassDesc" runat="server" class="col-md-8 control-label" /></td>
                        <td style="text-align:left;"><strong>Regularization Date</strong></td> 
                        <td ><asp:label ID="lblPayPeriod" runat="server" class="col-md-8 control-label" /></td>
                    </tr> 
                    <tr> 
                        <td style="text-align:left;"><strong>Position</strong></td> 
                        <td ><asp:label ID="lblPayCutoff" runat="server" class="col-md-8 control-label" /></td>
                        <td style="text-align:left;"><strong>Employee Status</strong></td> 
                        <td ><asp:label ID="lblPayScheduleDesc" runat="server" class="col-md-8 control-label" /></td>
                    </tr> 
                    <tr> 
                        <td style="text-align:left;"><strong>Department</strong></td> 
                        <td ><asp:label ID="Label1" runat="server" class="col-md-8 control-label" /></td>
                        <td style="text-align:left;"><strong>Job Grade</strong></td> 
                        <td ><asp:label ID="Label2" runat="server" class="col-md-8 control-label" /></td>
                    </tr> 
                    </tbody> 
                </table> 

            </div>
        </div>
</div>--%>
     
<%--<div class="panel panel-default">   </div>  --%>        
    <div class="col-md-2 bhoechie-tab-menu">
        <div style="padding:0px 0px;">
             <asp:PlaceHolder runat="server" ID="PlaceHolder3" />                                 
        </div>
        <div class="list-group">
            <asp:PlaceHolder runat="server" ID="PlaceHolder1" />
        </div>
    </div>
    <div class="col-md-10 bhoechie-tab" style=" border-left:1px solid #e5e5e5;">                        
        <asp:PlaceHolder runat="server" ID="PlaceHolder2" />
    </div>

</div>


</div>