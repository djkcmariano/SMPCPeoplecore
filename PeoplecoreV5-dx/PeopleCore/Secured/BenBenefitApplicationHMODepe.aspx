<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BenBenefitApplicationHMODepe.aspx.vb" Inherits="Secured_BenBenefitApplicationHMODepe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <h4>&nbsp;</h4>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitApplicationHMODepNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BenefitApplicationHMODepNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Dependent Name" />
                                <dx:GridViewDataComboBoxColumn FieldName="BenefitHMOPlanTypeDesc" Caption="Benefit Plan" />                                                                  
                                <dx:GridViewDataTextColumn FieldName="MBL" Caption="MBL" />
                                <dx:GridViewDataTextColumn FieldName="Cost" Caption="Premium Cost" />
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
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Dependents :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboEmployeedepeno"  CssClass="form-control required">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Benefit Plan :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboBenefitHMOPlanTypeNo" CssClass="form-control required"
                    AutoPostBack="true" OnSelectedIndexChanged="cboBenefitHMOPlanTypeNo_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">MBL :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtMBL" runat="server" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMBL" FilterType="Numbers, Custom" ValidChars="-." />
                </div>
            </div>
                
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Premium Cost :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtCost" runat="server" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCost" FilterType="Numbers, Custom" ValidChars="-." />
                </div>
            </div>
                    
                               
        </div>                    
    </fieldset>
</asp:Panel>   
</asp:Content>

