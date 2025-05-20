<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SelfDTRLeaveApplicationListAppr.aspx.vb" Inherits="SecuredManager_SelfDTRLeaveApplicationListAppr" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

	<script type="text/javascript">
//	    function OnTextChanged(keyValue, reason) {
//	        var Key = keyValue.toString();
//	        hfChanges.Set(Key, reason);
//	    }

	    function cbCheckAll_CheckedChanged(s, e) {
	        grdMain.PerformCallback(s.GetChecked().toString());
	    }


//	    function cbCheckAllMain_CheckedChanged(s, e) {
//	        grdDetl.PerformCallback(s.GetChecked().toString());

//	    }
	</script>


<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div class="col-md-2" style="vertical-align:middle; text-align:right; display: none;"> 
                    <label class="control-label">HSO Approval Status :</label>
                </div>
                <div class="col-md-2" style="Display: none"> 
                    <asp:Dropdownlist ID="cboTab2No" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" style=" vertical-align:middle; text-align:left;" runat="server" />
                </div>
                <div>                
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                                                
                    <ul class="panel-controls">                                                        
                        <li><asp:LinkButton runat="server" ID="lnkAddMass" OnClick="lnkAddMass_Click" Text="Mass Application" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDisApproved" OnClick="lnkDis_Click" Text="Disapprove" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkHSOApproved" OnClick="lnkHSOApproved_Click" Text="HSO Approve" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkHSODisapproved" OnClick="lnkHSODisapproved_Click" Text="HSO Disapprove" CssClass="control-primary" /></li>
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDisApproved" ConfirmMessage="Are you sure you want to disapprove?"  />
                        <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkApproved" ConfirmMessage="Are you sure you want to approve?"  />
                        <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="lnkHSOApproved" ConfirmMessage="Are you sure you want to approve?"  />
                        <uc:ConfirmBox runat="server" ID="ConfirmBox4" TargetControlID="lnkHSODisapproved" ConfirmMessage="Are you sure you want to disapprove?"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="LeaveApplicationNo" OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                           
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                <dx:GridViewDataTextColumn FieldName="xStartDate" Caption="From" />
                                <dx:GridViewDataTextColumn FieldName="xEndDate" Caption="To" />                             
                                <dx:GridViewDataTextColumn FieldName="AppliedHrs" Caption="Filed<br />Hr/s" />
                                <dx:GridViewDataTextColumn FieldName="PaidHrs" Caption="Paid<br />Hr/s" />
                                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Disapproval<br />Remarks" Width="12%" Visible="false"/>
                                <dx:GridViewDataColumn Caption="Approver Remarks" Width="12%" Visible="false" >
                                    <DataItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRemarks" Text='<%# Bind("Remarks") %>' Enabled='<%# Bind("IsEnabled") %>' Width="100%" TextMode="MultiLine" Rows="2" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status"  Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveBy" Caption="Approved /<br />Disapproved<br />By" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveDate" Caption="Approved /<br />Disapproved<br />Date"  Width="5%" />

                                <dx:GridViewDataTextColumn FieldName="OrganizationGroupRemarks" Caption="HSO<br />Approval Status"  Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="OrgGroupApproveDisApproveDate" Caption="HSO<br />Approved /<br />Disapproved<br />Date"  Width="5%" />

                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date <br /> Applied &nbsp;&nbsp;" HeaderStyle-VerticalAlign="Top" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Applied By" Visible="false" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
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
                                 <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
				                </dx:GridViewCommandColumn>
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
                        <li><asp:LinkButton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" Text="Save" CssClass="control-primary" Visible="false" /></li>   
                        <li>
                            <asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary" />
                        </li>                
                    </ul>                                                                                                                                                                         
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="LeaveApplicationDetiNo" Width="100%" OnCustomCallback="gridDetl_CustomCallback">
                            <Columns>                                
                                <dx:GridViewDataTextColumn FieldName="LeaveApplicationDetiTransNo" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="xDTRDate" Caption="DTR Date" />
                                <dx:GridViewDataTextColumn FieldName="DayDesc" Caption="Day" />
                                <dx:GridViewDataTextColumn FieldName="PaidHrs" Caption="Paid Hr/s"  /> 
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status"  />
                                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason"  />                                                                                            
                                <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Paid Hours" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:TextBox runat="server" ID="txtPaidHrs" Width="100%" Text='<%# Bind("PaidHrs") %>' />
                                        <asp:HiddenField runat="server" ID="hifLeaveApplicationDetiNo" Value='<%# Bind("LeaveApplicationDetiNo") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn> --%>        
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
                                        <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                            </dx:ASPxCheckBox>
                                        </HeaderTemplate>
                                    </dx:GridViewCommandColumn>                
                            </Columns>                            
                        </dx:ASPxGridView>                        
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
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtLeaveApplicationNo" CssClass="form-control" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtLeaveApplicationTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-6">
                     <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_SubordinateAppr" CompletionSetCount="0" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                         }
                     </script>
                    
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Leave Type :</label>
                <div class="col-md-6">
                        <asp:Dropdownlist ID="cboLeavetypeNo" runat="server" CssClass="form-control required"> </asp:Dropdownlist>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date :</label>
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


            <%--<div class="form-group" style="Text-align: Left; Position: relative; Left: 250px; width: 350px" >
            <label class="col-md-5" style="Color: Red; Text-align: Left; width: 400px; font-weight: normal">NOTE: Filed HOURS/s for OB and Filed DAY/s for Leave.</label>
            </div>--%>

            <div class="form-group">
               <label class="col-md-4 control-label has-required">Filed Hr/s :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtAppliedHrs" runat="server" CssClass="form-control required" SkinID="txtDate" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtAppliedHrs" />
                </div>
            </div>
        
            <%--<div class="form-group" style="display:none;">
                <label class="col-md-4 control-label"></label>
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
                <label class="col-md-4 control-label has-space">Reason :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtReason" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control required" 
                        ></asp:Textbox>
                </div>
            </div>
        
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Approval Status :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboApprovalStatNo" DataMember="EApprovalStat" runat="server"  CssClass="form-control"></asp:Dropdownlist>
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

    <asp:Button ID="btnShowDis" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlDisApproval" runat="server" TargetControlID="btnShowDis" PopupControlID="pnlPopupDis" CancelControlID="lnkCloseDis" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopupDis" runat="server" CssClass="entryPopup2">
        <fieldset class="form" id="Fieldset2">
            <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="lnkCloseDis" CssClass="fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:Linkbutton runat="server" ID="lnkSaveDis" OnClick="lnkDisApproved_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
            </div>
            <div  class="entryPopupDetl2 form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Reason for Disapproval :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="TxtDisApprovalRemarks" TextMode="MultiLine" Rows="4" CssClass="form-control required" />
                    </div>
                </div>
            </div>
            <div class="cf popupfooter">
            </div>
        </fieldset>
    </asp:Panel>

</asp:Content>