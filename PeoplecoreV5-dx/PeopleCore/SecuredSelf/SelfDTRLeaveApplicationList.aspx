<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SelfDTRLeaveApplicationList.aspx.vb" Inherits="SecuredSelf_SelfDTRLeaveApplicationList" %>


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
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                    </li>
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="LeaveApplicationNo">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                    <dx:GridViewDataTextColumn FieldName="xStartDate" Caption="From" />
                                    <dx:GridViewDataTextColumn FieldName="xEndDate" Caption="To" />
                                    <dx:GridViewDataTextColumn FieldName="AppliedHrs" Caption="Filed<br />Hr/s" />
                                    <dx:GridViewDataTextColumn FieldName="PaidHrs" Caption="Paid<br />Hr/s" />
                                    <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" Width="12%" />
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Disapprover<br />Remarks" Width="12%" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status"  Width="12%" />
                                    <dx:GridViewDataTextColumn FieldName="ApproveDisApproveBy" Caption="Approved /<br />Disapproved<br />By" Width="5%" />
                                    <dx:GridViewDataTextColumn FieldName="ApproveDisApproveDate" Caption="Approved /<br />Disapproved<br />Date"  Width="5%" />

                                    <dx:GridViewDataTextColumn FieldName="OrganizationGroupRemarks" Caption="HSO<br />Approval Status"  Width="12%" />
                                    <dx:GridViewDataTextColumn FieldName="OrgGroupApproveDisApproveDate" Caption="HSO<br />Approved /<br />Disapproved<br />Date"  Width="5%" />

                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date <br /> Applied &nbsp;&nbsp;" Width="5%" HeaderStyle-VerticalAlign="Top" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Applied By" Visible="false" />
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
                                    <dx:GridViewDataColumn Caption="Leave Form" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" OnClick="lnkPrint_Click" Font-Size="Medium" OnPreRender="lnkPrint_PreRender" />
                                    </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%"/>
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
                                <asp:LinkButton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" Text="Save" CssClass="control-primary" Visible="false" />
                            </li>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary" />
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="LeaveApplicationDetiNo" Width="100%" 
                        OnCustomCallback="gridDetl_CustomCallback">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="LeaveApplicationDetiTransNo" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="xDTRDate" Caption="DTR Date" />
                                    <dx:GridViewDataTextColumn FieldName="DayDesc" Caption="Day" />
                                    <dx:GridViewDataTextColumn FieldName="PaidHrs" Caption="Paid Hours"  />
                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status"  />
                                    <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason"  />
                                    <%--<dx:GridViewDataTextColumn FieldName="Reason" >
					                <DataItemTemplate>
						                <dx:ASPxTextBox ID="txtLB" runat="server" Width="100%" Text='<%#Bind("Reason")%>' Enabled='<%#Bind("IsEnabledCancel")%>' OnInit="txtLB_Init">
						                </dx:ASPxTextBox>
					                </DataItemTemplate>
				                </dx:GridViewDataTextColumn>--%>
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
    <asp:Button ID="btnShow" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style=" display:none;">
        <fieldset class="form" id="fsMain">
            <!-- Header here -->
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />
            </div>
            <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label">
                    Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtLeaveApplicationNo" CssClass="form-control" runat="server" />
                    </div>
                </div>

                 <div class="form-group">
                    <label class="col-md-4 control-label has-space">Leave Balance as of: </label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtAsOf" runat="server" CssClass="form-control" Placeholder="As Of" ReadOnly="true" Enabled="false"
                        ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                        TargetControlID="txtAsOf"
                        Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtAsOf"
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
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-2">
                        <label>SL</label><asp:Textbox ID="txtSLBal" CssClass="form-control" runat="server" ReadOnly="true" /> 
                    </div>
                    <div class="col-md-2">.
                        <label>VL</label><asp:Textbox ID="txtVLBal" CssClass="form-control" runat="server" ReadOnly="true" /> 
                    </div>
                    <div class="col-md-2" style=" display:none;">
                        <label>SL/VL</label><asp:Textbox ID="txtSILBal" CssClass="form-control" runat="server" ReadOnly="true" /> 
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtLeaveApplicationTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Leave Type :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboLeavetypeNo" runat="server" CssClass="form-control required"> </asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Date :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control required" Placeholder="From" AutoPostBack="true" OnTextChanged="PopulateHrs_Inclusive"
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
                        <asp:TextBox ID="txtEndDate" runat="server"  CssClass="form-control required" Placeholder="To" AutoPostBack="true" OnTextChanged="PopulateHrs_Inclusive"
                        ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtEndDate"
                        Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
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
                <label class="col-md-4 control-label has-required">
                    Filed Hr/s :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtAppliedHrs" runat="server" CssClass="form-control required" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtAppliedHrs" />
                    </div>
                </div>
                <%--<div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    </label>
                    <div class="col-md-6">
                        <asp:Checkbox ID="txtISForAM" Text="&nbsp; Check for half day leave (AM only)" runat="server" >
                        </asp:Checkbox>
                    </div>
                </div>--%>

                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">Leave Reason :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboLeaveReasonNo" runat="server" CssClass="form-control required" AutoPostBack="True" OnSelectedIndexChanged="cboLeaveReasonNo_OnSelectedIndexChanged">
                        </asp:DropdownList>
                    </div>
            </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Reason :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtReason" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control required" 
                        ></asp:Textbox>
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space ">
                    Approval Status :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboApprovalStatNo" DataMember="EApprovalStat" runat="server"  CssClass="form-control"
                        >
                        </asp:Dropdownlist>
                    </div>
                </div>
            </div>
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