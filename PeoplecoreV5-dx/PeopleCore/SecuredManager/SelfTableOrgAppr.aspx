<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfTableOrgAppr.aspx.vb" Inherits="SecuredManager_SelfTableOrgAppr" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">                    
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="cboTabNo_SelectedIndexChanged" CssClass="form-control" runat="server" />                
                </div>
                <div>                                     
                    <ul class="panel-controls">                            
                        <li><asp:LinkButton runat="server" ID="lnkApprove" OnClick="lnkApprove_Click" Text="Approve" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDisapprove" OnClick="lnkDisapprove_Click" Text="Disapprove" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbApprove" TargetControlID="lnkApprove" ConfirmMessage="Selected items will be approved. Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="cfbDisapprove" TargetControlID="lnkDisapprove" ConfirmMessage="Selected items will be disapproved. Proceed?"  />                                         
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TableOrgNo" >                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="TableOrgDesc" Caption="Title" />
                                <dx:GridViewDataTextColumn FieldName="UserName" Caption="Encoded By" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" />
                                <dx:GridViewDataTextColumn FieldName="PostedDate" Caption="Posted Date" />
                                <dx:GridViewDataTextColumn FieldName="Added" Caption="Add't No. of Box" />
                                <dx:GridViewDataTextColumn FieldName="RevisionDate" Caption="Revision Date" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ArchivedDate" Caption="Archive Date" Visible="false" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Chart" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-external-link" Font-Size="Medium" CommandArgument='<%# Bind("TableOrgNo") %>' CommandName='<%# Bind("ApprovalStatNo") %>' ToolTip='<%# Bind("TableOrgDesc") %>' OnClick="lnkChart_Click" OnPreRender="lnkChart_PreRender" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" />                                                                
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>             
 </div>
        
</asp:Content>

