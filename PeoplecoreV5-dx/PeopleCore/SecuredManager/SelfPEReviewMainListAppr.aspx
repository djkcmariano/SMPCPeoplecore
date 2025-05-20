<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SelfPEReviewMainListAppr.aspx.vb" Inherits="Secured_SelfPEReviewMainListAppr" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
     <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                                                
                    <ul class="panel-controls">                                                        
                        
                        <li><asp:LinkButton runat="server" ID="lnkRevise" Text="Revise" OnClick="lnkRevise_Click" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkApproved" Text="Approve" OnClick="lnkApproved_Click" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" Text="Add" CssClass="control-primary" Visible="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" Text="Export" CssClass="control-primary" /></li>                                                                                
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEReviewNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="CodeDeti" Caption="Review No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="PETemplateDesc" Caption="Title" />
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year"  CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataTextColumn FieldName="PEPeriodDesc" Caption="Period" />
                                <dx:GridViewDataTextColumn FieldName="PECycleDesc" Caption="Performance Cycle" /> 
                                <dx:GridViewDataTextColumn FieldName="PEEvalPeriodDesc" Caption="Evaluation Type" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />
                                <dx:GridViewDataColumn Caption="Evaluation Form"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkForm" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkForm_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                         
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%" />
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
