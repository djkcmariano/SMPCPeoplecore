<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="TrnTakenCostList.aspx.vb" Inherits="Secured_TrnTakenCostList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" />
        </Header>
        <Content>
            <br />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TrnTakenCostNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataComboBoxColumn FieldName="TrnCostTypeDesc" Caption="Cost Type" />  
                                <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}"/>
                                <dx:GridViewDataTextColumn FieldName="Particulars" Caption="Particulars" />      
                                <dx:GridViewDataTextColumn FieldName="VoucherDate" Caption="Voucher Date" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="Payee" Caption="Payee" />                                                                                                                                                                  
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
            <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
                <fieldset class="form" id="fsMain">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">  
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:Textbox ID="txtTrnTakenCostNo" ReadOnly="true" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                                              
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Cost Type :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboTrnCostTypeNo" DataMember="ETrnCostType" runat="server" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Amount :</label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtAmount" CssClass="number form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Particulars :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtParticulars" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Voucher Date :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtVoucherDate" runat="server" CssClass="form-control"></asp:TextBox> 
                                                                    
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                TargetControlID="txtVoucherDate"
                                Format="MM/dd/yyyy" />  
                                      
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                    TargetControlID="txtVoucherDate"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Date"
                                    DisplayMoney="Left"
                                    AcceptNegative="Left" />
                                    
                                    <asp:RangeValidator
                                    ID="RangeValidator1"
                                    runat="server"
                                    ControlToValidate="txtVoucherDate"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                                    
                                    <ajaxToolkit:ValidatorCalloutExtender 
                                    runat="Server" 
                                    ID="ValidatorCalloutExtender1"
                                    TargetControlID="RangeValidator1" />                                                                           
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Payee :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtPayee" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                            </div>
                        </div>

                    </div>                    
                </fieldset>
            </asp:Panel>

        </Content>
    </uc:Tab>
</asp:Content>

