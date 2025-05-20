<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucTrnHeader.ascx.vb" Inherits="Include_wucPayHeader" %>


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
                    <td style="width:15%;text-align:left;vertical-align:middle;">
                        <asp:label ID="Label1" runat="server" class="col-md-12" Font-Bold="true" Text="Training No." />
                    </td> 
                    <td style="width:35%;vertical-align:middle;">
                        <asp:label ID="lblCode" runat="server" class="col-md-12" />
                   </td>
                    <td style="width:15%;text-align:left;vertical-align:middle;">
                        <asp:label ID="Label2" runat="server" class="col-md-12" Font-Bold="true" Text="Training Status" />
                   </td> 
                    <td style="width:35%;vertical-align:middle;">
                        <asp:label ID="lblTrnStatDesc" runat="server" class="col-md-12" />
                    </td>
                </tr> 
                <tr> 
                    <td style="text-align:left;vertical-align:middle;">
                        <asp:label ID="Label3" runat="server" class="col-md-12" Font-Bold="true" Text="Training Title" />
                    </td> 
                    <td style="vertical-align:middle;">
                        <asp:label ID="lblTrnTitleDesc" runat="server" class="col-md-12" />
                    </td>
                    <td style="text-align:left;vertical-align:middle;">
                        <asp:label ID="Label4" runat="server" class="col-md-12" Font-Bold="true" Text="Enrollment Type" />
                    </td> 
                    <td style="vertical-align:middle;">
                        <asp:label ID="lblEnrolTypeDesc" runat="server" class="col-md-12" />
                    </td>
                </tr> 
                <tr> 
                    <td style="text-align:left;vertical-align:middle;">
                        <asp:label ID="Label5" runat="server" class="col-md-12" Font-Bold="true" Text="Date" />
                    </td> 
                    <td style="vertical-align:middle;">
                        <asp:label ID="lblTrainingDate" runat="server" class="col-md-12" />
                    </td>
                    <td style="text-align:left;vertical-align:middle;">
                        <asp:label ID="Label6" runat="server" class="col-md-12" Font-Bold="true" Text="Minimum Seats" />
                    </td> 
                    <td style="vertical-align:middle;">
                        <asp:label ID="lblMinimumSeats" runat="server" class="col-md-12" />
                    </td>
                </tr> 
                <tr> 
                    <td style="text-align:left;vertical-align:middle;">
                        <asp:label ID="Label7" runat="server" class="col-md-12" Font-Bold="true" Text="Time" />
                    </td> 
                    <td style="vertical-align:middle;">
                        <asp:label ID="lblTrainingTime" runat="server" class="col-md-12" />
                    </td>
                    <td style="text-align:left;vertical-align:middle;">
                        <asp:label ID="Label8" runat="server" class="col-md-12" Font-Bold="true" Text="Maximum Seats" />
                    </td> 
                    <td style="vertical-align:middle;">
                        <asp:label ID="lblMaximumSeats" runat="server" class="col-md-12" />
                    </td>
                </tr> 
                </tbody> 
            </table> 
        </div>
    </div>
</div>
      

