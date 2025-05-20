<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DTRDetlList_Disc.aspx.vb" Inherits="Secured_DTRDetlList_Disc" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">                                                 
                <div class="col-md-12">
                    <div class="col-md-6">
                        <label class="col-md-4 control-label">DTR No. :</label>
                        <asp:label ID="lblDTRCode" runat="server" class="col-md-6 control-label"></asp:label>                                                                                            
                    </div>
                    <div class="col-md-6">
                        <label class="col-md-4 control-label">Payroll Group :</label>
                        <asp:label ID="lblPayClassDesc" runat="server" class="col-md-6 control-label"></asp:label>
                    </div>                                                  
                </div>
                <div class="col-md-12">
                    <div class="col-md-6">
                        <label class="col-md-4 control-label">DTR Cut-off :</label>
                        <asp:label ID="lblDTRCutoff" runat="server" class="col-md-6 control-label"></asp:label>                                                                                            
                    </div>
                    <div class="col-md-6">
                        <label class="col-md-4 control-label">Payroll Type :</label>
                        <asp:label ID="lblPayTypeDesc" runat="server" class="col-md-6 control-label"></asp:label>
                    </div>                                                  
                </div>                                            
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                    
                        </ul>                                                    
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <%--<div class="panel-heading">                                
                    <div class="panel-body" style="border:2px solid #f5f5f5;">
                        <div class="form-group">
                            <div class="col-md-11">
                                <label class="col-md-2 control-label">DTR No. :</label>
                                <asp:label ID="lblDTRCode" runat="server" class="col-md-3 control-label"></asp:label>
                                <div class="col-md-6">
                                    <label class="col-md-3 control-label">Payroll Group :</label>
                                    <asp:label ID="lblPayClassDesc" runat="server" class="col-md-3 control-label"></asp:label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-11">
                                <label class="col-md-2 control-label">DTR Cut-off :</label>
                                <asp:label ID="lblDTRCutoff" runat="server" class="col-md-3 control-label"></asp:label>
                                <div class="col-md-6">
                                    <label class="col-md-3 control-label">Pay Type :</label>
                                    <asp:label ID="lblPayTypeDesc" runat="server" class="col-md-3 control-label"></asp:label>
                                </div>
                            </div>
                        </div>
                    </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                    
                    </ul>                                                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkExport" />
                </Triggers>
                </asp:UpdatePanel>
            </div>--%>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRDetiDiscNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="WorkingHrs" Caption="Hrs Work" />                  
                                <dx:GridViewDataTextColumn FieldName="HolidayHrs" Caption="Paid Holidays" />
                                <dx:GridViewDataTextColumn FieldName="PaidLeave" Caption="Leave Hrs" />
                                <dx:GridViewDataTextColumn FieldName="DOvt" Caption="OT" />
                                <dx:GridViewDataTextColumn FieldName="DOvt8" Caption="OT8" />
                                <dx:GridViewDataTextColumn FieldName="DNP" Caption="NP" />
                                <dx:GridViewDataTextColumn FieldName="DNP8" Caption="NP8" />
                                <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Absent" />
                                <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" />
                                <dx:GridViewDataTextColumn FieldName="Under" Caption="Undertime" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Detail" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                          
                            </Columns>                     
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>

<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">                                
                <h3 class="panel-title">
                    <asp:Label ID="lblFullName" runat="server"></asp:Label>
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" runat="server" KeyFieldName="DTRDetiLogNo" Width="100%" >                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn FieldName="DTRDate" Caption="Date" />
                                <dx:GridViewDataColumn FieldName="ShiftCode" Caption="Shift" />                                                                           
                                <dx:GridViewDataColumn FieldName="DayTypeCode" Caption="D-Type" />                  
                                <dx:GridViewDataColumn FieldName="Hrs" Caption="Req. Hrs" />
                                <dx:GridViewDataColumn FieldName="In1" Caption="In1" />
                                <dx:GridViewDataColumn FieldName="Out1" Caption="Out1" />
                                <dx:GridViewDataColumn FieldName="In2" Caption="In2" />
                                <dx:GridViewDataColumn FieldName="Out2" Caption="Out2" />
                                <dx:GridViewDataColumn FieldName="OvtIn1" Caption="OT In1" />
                                <dx:GridViewDataColumn FieldName="OvtOut1" Caption="OT Out1" />
                                <dx:GridViewDataColumn FieldName="OvtIn2" Caption="OT In2" />
                                <dx:GridViewDataColumn FieldName="OvtOut2" Caption="OT Out2" />
                                <dx:GridViewDataColumn FieldName="WorkingHrs" Caption="Working Hrs" />
                                <dx:GridViewDataColumn FieldName="Ovt" Caption="OT" />
                                <dx:GridViewDataColumn FieldName="Ovt8" Caption="OT8" />
                                <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Absent" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="Under" Caption="Undertime" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataColumn FieldName="NP" Caption="NP" />
                                <dx:GridViewDataColumn FieldName="NP8" Caption="NP8" />
                                <dx:GridViewDataColumn FieldName="LeaveTypeCode" Caption="L-Type" />
                                <dx:GridViewDataColumn FieldName="LeaveHrs" Caption="L-Hrs" />                                                  
                            </Columns>
                            <SettingsPager PageSize="31" />         
                        </dx:ASPxGridView>
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>

</asp:Content>

