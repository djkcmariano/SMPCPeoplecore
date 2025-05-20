<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CliAccidentList.aspx.vb" Inherits="Secured_CliAccidentList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul> 
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                    </asp:UpdatePanel> 
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ClinicAcciNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                <dx:GridViewDataTextColumn FieldName="DateReported" Caption="Date Reported" /> 
                                <dx:GridViewDataTextColumn FieldName="ReportByName" Caption="Reported By" Visible="false" /> 
                                <dx:GridViewDataComboBoxColumn FieldName="ClinicAcciCategoryDesc" Caption="Accident Category" /> 
                                <dx:GridViewDataComboBoxColumn FieldName="ClinicAcciTypeDesc" Caption="Accident Type" /> 
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" /> 
                                <dx:GridViewDataTextColumn FieldName="Fullname" Caption="Employee Name" />  
                                <dx:GridViewDataTextColumn FieldName="MembershipClassDesc" Caption="Member Class" />  
                                <dx:GridViewBandColumn Caption="Accident/Incident Occured" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="AccidentRemarks" Caption="How"  />       
                                        <dx:GridViewDataTextColumn FieldName="DateOccured" Caption="Date" /> 
                                        <dx:GridViewDataTextColumn FieldName="TimeOccured" Caption="Time" />      
                                        <dx:GridViewDataTextColumn FieldName="PlaceOccured" Caption="Place" />  
                                    </Columns>
                                </dx:GridViewBandColumn>                                                          
                                <dx:GridViewDataTextColumn FieldName="InvistigatedBy" Caption="Investigated By" Visible="false" />    
                                <dx:GridViewDataTextColumn FieldName="InvestigationResult" Caption="Investigation Result" Visible="false" />                                                                
                                <dx:GridViewDataTextColumn FieldName="Recommendation" Caption="Recommendation" Visible="false" />                                                                
                                <dx:GridViewDataTextColumn FieldName="Resolution" Caption="Resolution" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="Assessment" Caption="Assessment" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="Treatment" Caption="Treatment" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ClinicAcciStatDesc" Caption="Status" />                                                                                       
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>

</asp:content>