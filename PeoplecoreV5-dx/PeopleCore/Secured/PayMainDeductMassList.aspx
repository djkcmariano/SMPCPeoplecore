<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayMainDeductMassList.aspx.vb" Inherits="Secured_PayMainDeductMassList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-4">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" style="width:200px;" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkActivate" CommandName="0" OnClick="lnkSuspend_Click" Text="Activate Deduction" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkSuspend" CommandName="1" OnClick="lnkSuspend_Click" Text="Suspend Deduction" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="cfbSuspend" TargetControlID="lnkSuspend" ConfirmMessage="Are you sure you want to suspend selected record(s)?"  />
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayMainDeductMassNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayDeductTypeDesc" Caption="Deduction Type" />
                                    <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayScheduleDesc" Caption="Payroll Schedule" />
                                    <dx:GridViewDataTextColumn FieldName="PayTypeDesc" Caption="Payroll Type" />
                                    <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsApplyToAll" Caption="Taxable" ReadOnly="true" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="SuspendedDate" Caption="Suspended Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Detail" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
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


  <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <div class="col-md-4 panel-title">
                            <asp:Dropdownlist ID="cboTabDetlNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearchDetl_Click" style="width:200px;" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkActivate_Indi" CommandName="0" OnClick="lnkSuspend_Indi_Click" Text="Activate Deduction" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkSuspend_Indi" CommandName="1" OnClick="lnkSuspend_Indi_Click" Text="Suspend Deduction" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkAppend" OnClick="lnkAppend_Click" Text="Append" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                        <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul>
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    <uc:ConfirmBox runat="server" ID="cfbSuspendDetl" TargetControlID="lnkSuspend_Indi" ConfirmMessage="Are you sure you want to suspend selected record(s)?"  />
                                    <uc:ConfirmBox runat="server" ID="cfbActivateDetl" TargetControlID="lnkActivate_Indi" ConfirmMessage="Are you sure you want to activate selected record(s)?"  />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExportDetl" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <asp:Label ID="lblDetl" runat="server"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="PayMainDeductMassDetiNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />
                                    <dx:GridViewDataTextColumn FieldName="StartDate" Caption="Start Date" />
                                    <dx:GridViewDataTextColumn FieldName="EndDate" Caption="End Date" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="xSuspendedDate" Caption="Last Date Suspended" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="xSuspendedBy" Caption="Suspended By" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="xIsSuspended" Caption="Suspended" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                    <dx:GridViewDataTextColumn FieldName="ActivatedDate" Caption="Last Date Activated" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ActivatedBy" Caption="Activated By" Visible="false" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />    
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

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayMainDeductMassNo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Deduction Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayDeductTypeNo"  runat="server" CssClass="required form-control" ></asp:Dropdownlist>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDescription" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Amount :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtAmount" runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount" FilterType="Numbers, Custom" ValidChars="-." /> 
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Payroll Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayTypeNo" runat="server" CssClass="required form-control" DataMember="EPayType" ></asp:Dropdownlist>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Payroll Schedule :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayScheduleNo" runat="server" CssClass="required form-control" DataMember="EPaySchedule" ></asp:Dropdownlist>
                </div>
            </div> 
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label  has-space">Payroll Group :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayclassNo" runat="server" CssClass="form-control"></asp:Dropdownlist>
                </div>
            </div> 

            <div class="form-group" style="visibility:hidden; position:absolute;">
                <label class="col-md-4 control-label">Apply to all employees? :</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsApplytoall" />
                </div>
            </div> 
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="fsDetl">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveDetl" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayMainDeductMassDetiNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDeti" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" EnableCaching="false" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function Split(obj, index) {
                             var items = obj.split("|");
                             for (i = 0; i < items.length; i++) {
                                 if (i == index) {
                                     return items[i];
                                 }
                             }
                         }

                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                         }
                            </script>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAmountD" runat="server" CssClass="required form-control" ></asp:TextBox>   
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtAmountD" FilterType="Numbers, Custom" ValidChars=".-" /> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Start Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtStartDate" runat="server" cssclass="form-control"></asp:TextBox> 
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate" Format="MM/dd/yyyy" />                                                       
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left"
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  />
                                                                        
                        <asp:RangeValidator
                        ID="RangeValidator2"
                        runat="server"
                        ControlToValidate="txtStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="Dynamic"  />
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">End Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEndDate" runat="server" cssclass="form-control"></asp:TextBox> 
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate" Format="MM/dd/yyyy" />                                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left"
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  />

                        <asp:RangeValidator
                        ID="RangeValidator1"
                        runat="server"
                        ControlToValidate="txtEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="Dynamic"  />  
                </div>
            </div>
                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowAppend" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlAppend" runat="server" TargetControlID="btnShowAppend" PopupControlID="pnlPopupAppend" CancelControlID="lnkCloseAppend" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupAppend" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="Fieldset1">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseAppend" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveAppend" OnClick="lnkSaveAppend_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveAppend" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Filter By :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboFilteredbyNo" DataMember="EFilteredByPay" AutoPostBack="true"  runat="server" CssClass="form-control required" OnSelectedIndexChanged="cboFilteredbyNo_SelectedIndexChanged" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Filter Value :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboFilterValue" runat="server" CssClass="form-control required"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAmountAppend" runat="server" CssClass="required form-control" ></asp:TextBox>   
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtAmountAppend" FilterType="Numbers, Custom" ValidChars=".-" /> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Start Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtStartDateAppend" runat="server" cssclass="form-control"></asp:TextBox> 
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtStartDateAppend" Format="MM/dd/yyyy" />                                                       
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtStartDateAppend"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left"
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  />
                                                                        
                        <asp:RangeValidator
                        ID="RangeValidator3"
                        runat="server"
                        ControlToValidate="txtStartDateAppend"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="Dynamic"  />
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">End Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEndDateAppend" runat="server" cssclass="form-control"></asp:TextBox> 
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndDateAppend" Format="MM/dd/yyyy" />                                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtEndDateAppend"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left"
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  />

                        <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtEndDateAppend"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="Dynamic"  />  
                </div>
            </div>
                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


</asp:Content>
