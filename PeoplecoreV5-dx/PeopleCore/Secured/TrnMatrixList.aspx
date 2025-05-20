<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="TrnMatrixList.aspx.vb" Inherits="Secured_TrnMatrixList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch1_Click" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>                    
                </div>
                 <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeNo">                                                                                   
                            <Columns>                         
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataDateColumn FieldName="HiredDate" Caption="Date Hired" />                                                                           
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" /> 
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" /> 
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
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
 
 
<div class="page-content-wrap" > 
    <div class="col-md-12 bhoechie-tab-container" > 
        <div class="panel panel-default" style="margin-bottom:0px;">
             <div class="panel-heading" style="padding:5px;">
                <div class="panel-title">
                    <asp:Image runat="server" ID="imgPhoto" width="50" height="50" CssClass="img-circle" style="border: 2px solid white; padding:0px;margin:0px" />&nbsp;&nbsp;
                    <asp:Label ID="lblDetl" runat="server" />
                </div>
                <div style="padding:10px;">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">                            
                            <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                    </Triggers>
                    </asp:UpdatePanel>                                       
                </div> 
             </div>    
             
         
             <div class="col-md-2 bhoechie-tab-menu">
                <div class="list-group">
                    <%--To be Taken--%>
                    <asp:LinkButton ID="lnkTaken" OnClick="lnkTaken_Click" runat="server" CssClass="list-group-item active text-left" Text="To Be Taken"></asp:LinkButton>
                    <%--Required--%>
                    <asp:LinkButton ID="lnkRequired" OnClick="lnkRequired_Click" runat="server" CssClass="list-group-item text-left" Text="Required"></asp:LinkButton>
                    <%--Completed--%>
                    <asp:LinkButton ID="lnkCompleted" OnClick="lnkCompleted_Click" runat="server" CssClass="list-group-item text-left" Text="Completed"></asp:LinkButton>
                    <%--Incomplete--%>
                    <asp:LinkButton ID="lnkIncomplete" OnClick="lnkIncomplete_Click" runat="server" CssClass="list-group-item text-left" Text="Incomplete"></asp:LinkButton>
                    <%--No Show--%>
                    <asp:LinkButton ID="lnkNoShow" OnClick="lnkNoShow_Click" runat="server" CssClass="list-group-item text-left" Text="No Show"></asp:LinkButton>
                    <%--Service Contract--%>
                    <asp:LinkButton ID="lnkService" OnClick="lnkService_Click" runat="server" CssClass="list-group-item text-left" Text="Training Bond"></asp:LinkButton>
                </div>
            </div>

             <div class="col-md-10 bhoechie-tab" style=" border-left:1px solid #e5e5e5;">                  
             <br />

               <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" Width="100%" KeyFieldName="RecordNo">                                                                                   
                                <Columns>                          
                                    <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />
                                    <dx:GridViewDataTextColumn FieldName="RequiredStatDesc" Caption="Required" />
                                    <dx:GridViewDataTextColumn FieldName="TrainingDate" Caption="Date of Training" />
                                    <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hour(s)" />
                                    <dx:GridViewBandColumn Caption="Validity of Certificate" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataDateColumn FieldName="xValidityFrom" Caption="From" /> 
                                            <dx:GridViewDataDateColumn FieldName="xValidityTo" Caption="To" />
                                        </Columns>
                                    </dx:GridViewBandColumn>    
                                    <dx:GridViewBandColumn Caption="Validity of Bond" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataDateColumn FieldName="xServStartDate" Caption="From" /> 
                                            <dx:GridViewDataDateColumn FieldName="xServEndDate" Caption="To" />
                                        </Columns>
                                    </dx:GridViewBandColumn> 
                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />        
                                    <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" />                                                   
                                </Columns>          
                                <SettingsSearchPanel Visible="true" />                       
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />
                        </div>
                    </div> 
                    <br />                                                       
                </div>   
                
             </div>
        </div>   
    </div>
</div>

</asp:content>
