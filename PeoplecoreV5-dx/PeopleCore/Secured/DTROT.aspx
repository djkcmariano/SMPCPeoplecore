<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTROT.aspx.vb" Inherits="Secured_DTROTList" %>
<%@ Register Src="~/Include/wucFilterGeneric.ascx" TagName="Filter" TagPrefix="wuc" %>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

 <script type="text/javascript">
     function cbCheckAll_CheckedChanged(s, e) {
         grdMain.PerformCallback(s.GetChecked().toString());
     }

  </script>

<br />
<div class="page-content-wrap" >     
    <div class="row">
            <uc:FilterSearch runat="server" ID="FilterSearch1" EnableContent="false" EnableFilter="true" FilterName="EmployeeFilter" >
            </uc:FilterSearch>
    </div>    
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
                        <li><asp:LinkButton runat="server" ID="lnkAddMass" OnClick="lnkAddMass_Click" Text="Mass Application" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible ="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary"/></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                                                                                
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                                                
                        <uc:ConfirmBox runat="server" ID="cfbCancel" TargetControlID="lnkCancel" ConfirmMessage="Selected items will be cancelled. Proceed?"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="GrdNoSearch" KeyFieldName="DTROTNo" OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback" >                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="xDTRDate" Caption="Date" />
                                <dx:GridViewDataTextColumn FieldName="OvtIn1" Caption="In" />
                                <dx:GridViewDataTextColumn FieldName="OvtOut1" Caption="Out" />
                                <dx:GridViewDataTextColumn FieldName="OvtIn2" Caption="In 2" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="OvtOut2" Caption="Out 2" Visible="false" />
                                <%--<dx:GridViewDataTextColumn FieldName="ChargedToDesc" Caption="Charged to" Visible="false" />--%>
                                <dx:GridViewDataTextColumn FieldName="OTBreak" Caption="OT Break" Visible="false" />
                                <%--<dx:GridViewDataCheckColumn FieldName="IsForCompensatory" Caption="For CTO" />--%>
                                <%--<dx:GridViewDataCheckColumn FieldName="IsOnCall" Caption="On Call" Visible="false" />--%>
                                <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Filed<br />Hrs" />
                                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Disapprover<br />Remarks" Width="12%"  Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status" Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveBy" Caption="Approved /<br />Disapproved<br />By" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveDate" Caption="Approved /<br />Disapproved<br />Date" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date<br />Applied &nbsp;&nbsp;" Width="5%" HeaderStyle-VerticalAlign="Top" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Applied By" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CancellationRemark" Caption="Cancellation Reason" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CancelledBy" Caption="Cancelled By" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CancelledDate" Caption="Cancelled Date" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" />
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


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style=" display:none;">
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
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTROTNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtDTROTTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_DTRApplication" CompletionSetCount="1" 
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
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">
                        <asp:TextBox ID="txtDTRDate" runat="server" CssClass="required form-control" ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                            Format="MM/dd/yyyy" TargetControlID="txtDTRDate" />
                        <asp:RangeValidator ID="RangeValidator3" runat="server" 
                            ControlToValidate="txtDTRDate" Display="None" 
                            ErrorMessage="&lt;b&gt;Please enter valid entry&lt;/b&gt;" 
                            MaximumValue="3000-12-31" MinimumValue="1900-01-01" Type="Date" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                            AcceptNegative="Left" ClearTextOnInvalid="true" DisplayMoney="Left" 
                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" 
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtDTRDate" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" 
                            runat="Server" TargetControlID="RangeValidator3" />
                   </div>
            </div>
         
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Time :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOvtIn1" runat="server" CssClass="form-control required" Placeholder="OT In" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                        TargetControlID="txtOvtIn1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                        ControlExtender="MaskedEditExtender4x"
                        ControlToValidate="txtOvtIn1"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
                </div>
                <%--<label class="col-md-1 control-label"></label>--%>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOvtOut1" runat="server" CssClass="form-control required" Placeholder="OT Out" ></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtOvtOut1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                        ControlExtender="MaskedEditExtender4"
                        ControlToValidate="txtOvtOut1"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
            </div>
       
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Time 2 :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOvtIn2" runat="server"  CssClass="form-control" Placeholder="OT In2"></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtOvtIn2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                        ControlExtender="MaskedEditExtender5"
                        ControlToValidate="txtOvtIn2"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
            
                <%--<label class="col-md-1 control-label"></label>--%>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOvtOut2" runat="server" cssclass="form-control" Placeholder="OT Out2"></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                        TargetControlID="txtOvtOut2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                        ControlExtender="MaskedEditExtender6"
                        ControlToValidate="txtOvtOut2"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">OT Break (Hrs) :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOTBreak" runat="server" CssClass="number form-control"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtOTBreak" />
                </div>
            </div>

             <%--<div class="form-group" style="display:none;">
              <label class="col-md-4 control-label"></label>
                 <div class="col-md-6">
                    <asp:Checkbox ID="txtIsForcompensatory" Text="&nbsp; Tick to tag for compensatory time off" runat="server"></asp:Checkbox>
                 </div>
            </div> 

             <div class="form-group" style="visibility:hidden; position:absolute;" >
                <label class="col-md-4 control-label"></label>
                <div class="col-md-6">
                    <asp:Checkbox ID="txtIsOncall" Text="&nbsp; For On-Call" runat="server"></asp:Checkbox>
                </div>
            </div>--%>

            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label has-space">Charged to :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboCostCenterNo" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                    <label class="col-md-4 control-label  has-required">OT Reason :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboOvertimeReasonNo" runat="server" CssClass="form-control required" AutoPostBack="True" OnSelectedIndexChanged="cboOvertimeReasonNo_OnSelectedIndexChanged">
                        </asp:DropdownList>
                    </div>
            </div>

             <div class="form-group" >
                <label class="col-md-4 control-label has-space">Reason :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtReason" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>


             <div class="form-group" >
                <label class="col-md-4 control-label has-required">Approval Status :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboApprovalStatNo" runat="server" DataMember="EApprovalStat" CssClass="form-control required">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">NOTE:</label>
                <div class="col-md-6">
                    <asp:Label runat="server" ID="lblMsgNotice" style=" display: inline-block;"></asp:Label>
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
</asp:Content>
