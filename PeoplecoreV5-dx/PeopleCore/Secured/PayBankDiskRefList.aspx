<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayBankDiskRefList.aspx.vb" Inherits="Secured_PayBankDiskRefList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayBankDiskRefNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="7%" />
                                    <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayLocDesc" Caption="Company Name" />
                                    <dx:GridViewDataTextColumn FieldName="BankTypeDesc" Caption="Bank Type" />
                                    <dx:GridViewDataTextColumn FieldName="AccountNumber" Caption="Bank Account No." Width="7%" />
                                    <dx:GridViewDataTextColumn FieldName="BankCode" Caption="Bank Code" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="CompanyCode" Caption="Company Code" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="BranchCode_Company" Caption="Branch Code" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="BranchCode_PayrollAccount" Caption="Branch Account" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="BatchNo" Caption="Batch No." Width="7%" />
                                    <dx:GridViewDataTextColumn FieldName="EffectiveDate" Caption="Pay Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayDate1" Caption="Pay Date 1" Width="7%" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayDate2" Caption="Pay Date 2" Width="7%" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayDate3" Caption="Pay Date 3" Width="7%" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayDate4" Caption="Pay Date 4" Width="7%" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayOutSchedDesc" Caption="Pay Out Schedules" Width="7%" Visible="false" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>


<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayBankDiskRefNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Company :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPayLocNo" runat="server" CssClass="form-control required"  ></asp:DropdownList>
                </div>
            </div>
            <div class="form-group" style="Display: none">
                <label class="col-md-4 control-label has-space">Payroll Group :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPayClassNo" runat="server" CssClass="form-control"  ></asp:DropdownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Bank Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBankTypeNo" DataMember="EBankType" runat="server" CssClass="form-control"  ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Bank Account No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtAccountNumber" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Bank Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBankCode" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Company Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCompanyCode" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Branch Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBranchCode_Company" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Branch Account :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBranchCode_PayrollAccount" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>         

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">FV1</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">FV2</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">FV3</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">FV4</label><br />
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">MBTC only :</label>
                <div class="col-md-2">
                    <center>
                        <asp:Textbox ID="txtFV1" SkinID="txtdate" CssClass="number form-control" runat="server" ></asp:Textbox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:Textbox ID="txtFV2" SkinID="txtdate" CssClass="number form-control" runat="server" ></asp:Textbox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:Textbox ID="txtFV3" SkinID="txtdate" CssClass="number form-control" runat="server" ></asp:Textbox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:Textbox ID="txtFV4" SkinID="txtdate" CssClass="number form-control" runat="server" ></asp:Textbox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Batch No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBatchNo" SkinID="txtdate" CssClass="number form-control" runat="server" >
                    </asp:Textbox>
                </div>
            </div>

           <div class="form-group">
                <label class="col-md-4 control-label has-space">Pay Date :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEffectiveDate" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtEffectiveDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                </div>
            </div>

            <div class="form-group" style="Display: none;">
                <label class="col-md-4 control-label has-space">Payout Schedule No. :</label>
                <div class="col-md-7">
<%--                    <asp:Textbox ID="txtPayOutSchedNo" SkinID="txtdate" CssClass="number form-control" runat="server" >
                    </asp:Textbox>--%>
                    <asp:Dropdownlist ID="cboPayOutSchedNo" DataMember="EPayOutSched" runat="server" CssClass="form-control"  ></asp:Dropdownlist>
                </div>
            </div>

           <div class="form-group" style="Display: none;">
                <label class="col-md-4 control-label has-space">Pay Date 1 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayDate1" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPayDate1" Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtPayDate1"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                </div>
            </div>

           <div class="form-group" style="Display: none;">
                <label class="col-md-4 control-label has-space">Pay Date 2 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayDate2" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPayDate2" Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtPayDate2"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                </div>
            </div>

           <div class="form-group" style="Display: none;">
                <label class="col-md-4 control-label has-space">Pay Date 3 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayDate3" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPayDate3" Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtPayDate3"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                </div>
            </div>

           <div class="form-group" style="Display: none;">
                <label class="col-md-4 control-label has-space">Pay Date 4 :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayDate4" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtPayDate4" Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtPayDate4"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                </div>
            </div>

            <div class="form-group" style="Display: none;">
                <label class="col-md-4 control-label has-space">Debit Amount :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDebitAmount" SkinID="txtdate" CssClass="number form-control" runat="server" >
                    </asp:Textbox>
                </div>
            </div>
            <br />
        </div>
        
         </fieldset>
</asp:Panel>
</asp:Content> 