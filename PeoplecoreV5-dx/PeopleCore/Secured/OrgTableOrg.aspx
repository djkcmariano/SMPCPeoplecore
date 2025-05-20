<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="OrgTableOrg.aspx.vb" Inherits="Secured_OrgTableOrg" Theme="PCoreStyle" %>

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
                        <li><asp:LinkButton runat="server" ID="lnkRevise" OnClick="lnkRevise_Click" Text="Reuse" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkApprove" OnClick="lnkApprove_Click" Text="Approve" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDisapprove" OnClick="lnkDisapprove_Click" Text="Disapprove" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbRevise" TargetControlID="lnkRevise" ConfirmMessage="Selected items will be reused. Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="cfbApprove" TargetControlID="lnkApprove" ConfirmMessage="Selected items will be approved. Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="cfbDisapprove" TargetControlID="lnkDisapprove" ConfirmMessage="Selected items will be disapproved. Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                            
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TableOrgNo">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("TableOrgNo") %>' CommandName='<%# Bind("ApprovalStatNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="TableOrgDesc" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="UserName" Caption="Encoded By" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" />
                                <dx:GridViewDataTextColumn FieldName="PostedDate" Caption="Posted Date" Visible="false" />
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
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Department :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtTableOrgDesc" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Revision Count :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtRevisionNo" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Legal Basis :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" Rows="4" TextMode="MultiLine" />
                    </div>
                </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Effective Date :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEffectiveDate" runat="server" CssClass="form-control" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtEffectiveDate" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtEffectiveDate" Display="Dynamic" />
                </div>
            </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Top Plantilla No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtHeadPlantillaCode" CssClass="form-control required number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsFromPlantilla" Text="&nbsp;Tick to retrieve structure from the Staffing Pattern." />
                    </div>
                </div>               
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>            
</asp:Content>

