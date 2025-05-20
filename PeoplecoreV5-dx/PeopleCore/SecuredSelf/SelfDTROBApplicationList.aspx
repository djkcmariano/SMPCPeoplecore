<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SelfDTROBApplicationList.aspx.vb" Inherits="SecuredSelf_SelfDTROBApplicationList" %>



<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
    <script type="text/javascript">
        function OnTextChanged(keyValue, reason) {
            var Key = keyValue.toString();
            hfChanges.Set(Key, reason);
        }

        function cbCheckAll_CheckedChanged(s, e) {
            grdDetl.PerformCallback(s.GetChecked().toString());
        }
	</script>

 <br />
<div class="page-content-wrap" >         
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
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                                                                                
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRLogMainNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." /> 
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />                                            
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="StartDate" Caption="Start Date" />
                                <dx:GridViewDataTextColumn FieldName="EndDate" Caption="End Date" />
                                <dx:GridViewDataTextColumn FieldName="In1" Caption="In" />
                                <dx:GridViewDataTextColumn FieldName="Out1" Caption="Out" />
                                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Disapprover<br />Remarks" Width="12%" Visible="false" />                                                               
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status"  Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveBy" Caption="Approved /<br />Disapproved<br />By" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveDate" Caption="Approved /<br />Disapproved<br />Date"  Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date <br /> Applied &nbsp;&nbsp;" Width="5%" HeaderStyle-VerticalAlign="Top" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Applied By" Visible="false" />
                                <dx:GridViewDataColumn Caption="OB Form" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" OnClick="lnkPrint_Click" Font-Size="Medium" OnPreRender="lnkPrint_PreRender" />
                                    </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="2%">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Facility" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" Visible="false" />      
                                <dx:GridViewDataComboBoxColumn FieldName="LocationDesc" Caption="Location" Visible="false" />                                                                                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="ProjectDesc" Caption="Project" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />                                 
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
<br />
<div class="page-content-wrap" >
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                Transaction No. :&nbsp;<asp:Label runat="server" ID="lbl" />
                </div>
                <div>
                    <ul class="panel-controls">
                    &nbsp;                                                       
                        <li>
                            <asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary"  Visible="false" />
                        </li>
                    </ul>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="DTRLogNo" Width="100%" 
                            OnCustomCallback="gridDetl_CustomCallback">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." /> 
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />                                            
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="xDTRDate" Caption="Date" />
                                <dx:GridViewDataTextColumn FieldName="In1" Caption="In" />
                                <dx:GridViewDataTextColumn FieldName="Out1" Caption="Out" />
                                <dx:GridViewDataTextColumn FieldName="In2" Caption="In 2"/>
                                <dx:GridViewDataTextColumn FieldName="Out2" Caption="Out 2" /> 
                                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="CancelStatus" Caption="Status" />
                                <dx:GridViewDataTextColumn FieldName="CancelRemarks" Caption="Cancellation Reason" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Approver<br />Remarks" Width="12%" Visible="false" />                                                               
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status"  Width="12%" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveBy" Caption="Approved /<br />Disapproved<br />By" Width="5%" Visible="false"/>
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveDate" Caption="Approved /<br />Disapproved<br />Date"  Width="5%" Visible="false"/>
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date <br /> Applied &nbsp;&nbsp;" Width="5%" HeaderStyle-VerticalAlign="Top" Visible="false"/>
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Applied By" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Facility" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" Visible="false" />      
                                <dx:GridViewDataComboBoxColumn FieldName="LocationDesc" Caption="Location" Visible="false" />                                                                                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="ProjectDesc" Caption="Project" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" /> 
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
                                    <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hfChanges" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />     
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">DTR OB No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRLogMainNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtDTRLogTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" ></asp:TextBox>
                </div>
            </div> 
            
            <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Date :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control required" Placeholder="From" 
                        ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtStartDate"
                        Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                        <asp:RangeValidator
                        ID="RangeValidator3"
                        runat="server"
                        ControlToValidate="txtStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender6"
                        TargetControlID="RangeValidator3"
                        />
                    </div>
                    <%--<label class="col-md-1 control-label">To :</label>--%>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtEndDate" runat="server"  CssClass="form-control required" Placeholder="To"
                        ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtEndDate"
                        Format="MM/dd/yyyy" />
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
                        ClearTextOnInvalid="true"  
                        />
                        <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender2"
                        TargetControlID="RangeValidator4"
                        />
                    </div>
                </div>
         
            <div class="form-group">
                <label class="col-md-4 control-label has-required">In 1 :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIn1" runat="server" CssClass="form-control required" Placeholder="In" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                        TargetControlID="txtIn1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                        ControlExtender="MaskedEditExtender4x"
                        ControlToValidate="txtIn1"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
                </div>
<%--                <label class="col-md-1 control-label">Out 1 :</label>--%>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOut1" runat="server" CssClass="form-control required" Placeholder="Out" ></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtOut1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                        ControlExtender="MaskedEditExtender4"
                        ControlToValidate="txtOut1"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
            </div>
       
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">In 2 :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIn2" runat="server" CssClass="form-control" Placeholder="In 2"></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtIn2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                        ControlExtender="MaskedEditExtender5"
                        ControlToValidate="txtIn2"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                         />
                </div>
            
                <label class="col-md-1 control-label">Out 2 :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOut2" runat="server" cssclass="form-control" Placeholder="Out 2"></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                        TargetControlID="txtOut2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                        ControlExtender="MaskedEditExtender6"
                        ControlToValidate="txtOut2"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-4 control-label has-required">Reason :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtReason" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control required"></asp:TextBox>
                </div>
            </div>

             <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Approval Status :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboApprovalStatNo" runat="server" DataMember="EApprovalStat" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group" >
                <div class="col-md-6" Style="Text-align: center; left: 160px;">
                    <label id="Label1"  style="Color: Red; Text-align: center; font-weight: Bold;" runat="server" CssClass="form-control">Note: Time in Military Format</label>
                </div>
            </div>
         </div>
          <!-- Footer here -->
         <br />
            
        
</fieldset>
</asp:Panel>

<asp:Button ID="btnShowCancel" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlCancel" runat="server" TargetControlID="btnShowCancel" PopupControlID="pnlPopupCancel" CancelControlID="lnkCloseCancel" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopupCancel" runat="server" CssClass="entryPopup2">
        <fieldset class="form" id="Fieldset1">
            <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="lnkCloseCancel" CssClass="fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:Linkbutton runat="server" ID="lnkSaveCancel" OnClick="lnkSaveCancel_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
            </div>
            <div  class="entryPopupDetl2 form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Reason for Cancellation :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtCancellationRemark" TextMode="MultiLine" Rows="4" CssClass="form-control required" />
                    </div>
                </div>
            </div>
            <div class="cf popupfooter">
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>
