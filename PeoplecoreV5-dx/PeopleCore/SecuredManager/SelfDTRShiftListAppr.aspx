<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfDTRShiftListAppr.aspx.vb" Inherits="SecuredManager_SelfDTRShiftListAppr" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">   
<script type="text/javascript">

    function validateShift(spk, index) {
        if (spk.checked) {
            if (index == 1) {
                document.getElementById("ctl00_cphBody_cboShiftNoMon").disabled = false;
            } else if (index == 2) {
                document.getElementById("ctl00_cphBody_cboShiftNoTue").disabled = false;
            } else if (index == 3) {
                document.getElementById("ctl00_cphBody_cboShiftNoWed").disabled = false;
            } else if (index == 4) {
                document.getElementById("ctl00_cphBody_cboShiftNoThu").disabled = false;
            } else if (index == 5) {
                document.getElementById("ctl00_cphBody_cboShiftNoFri").disabled = false;
            } else if (index == 6) {
                document.getElementById("ctl00_cphBody_cboShiftNoSat").disabled = false;
            } else if (index == 7) {
                document.getElementById("ctl00_cphBody_cboShiftNoSun").disabled = false;
            }
        } else {
            if (index == 1) {
                document.getElementById("ctl00_cphBody_cboShiftNoMon").disabled = true;
            } else if (index == 2) {
                document.getElementById("ctl00_cphBody_cboShiftNoTue").disabled = true;
            } else if (index == 3) {
                document.getElementById("ctl00_cphBody_cboShiftNoWed").disabled = true;
            } else if (index == 4) {
                document.getElementById("ctl00_cphBody_cboShiftNoThu").disabled = true;
            } else if (index == 5) {
                document.getElementById("ctl00_cphBody_cboShiftNoFri").disabled = true;
            } else if (index == 6) {
                document.getElementById("ctl00_cphBody_cboShiftNoSat").disabled = true;
            } else if (index == 7) {
                document.getElementById("ctl00_cphBody_cboShiftNoSun").disabled = true;
            }
        }
    }

    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
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
                        <li><asp:LinkButton runat="server" ID="lnkAddMass" OnClick="lnkAddMass_Click" Text="Mass Application" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDisApproved" OnClick="lnkDis_Click" Text="Disapprove" CssClass="control-primary" /></li>
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDisApproved" ConfirmMessage="Are you sure you want to disapprove?"  />
                        <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkApproved" ConfirmMessage="Are you sure you want to approve?"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRShiftNo" OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." /> 
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />                                             
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="xDateFrom" Caption="From" />
                                <dx:GridViewDataTextColumn FieldName="xDateTo" Caption="To" />
                                <dx:GridViewDataTextColumn FieldName="ShiftDesc" Caption="Shift" />
                                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Disapprover<br />Remarks" Width="12%" Visible="false" />
                                <dx:GridViewDataColumn Caption="Approver Remarks" Width="12%" Visible="false" >
                                    <DataItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRemarks" Text='<%# Bind("Remarks") %>' Enabled='<%# Bind("IsEnabled") %>' Width="100%" TextMode="MultiLine" Rows="2" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status"  Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveBy" Caption="Approved /<br />Disapproved<br />By" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveDate" Caption="Approved /<br />Disapproved<br />Date"  Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date <br /> Applied &nbsp;&nbsp;" HeaderStyle-VerticalAlign="Top" />
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
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
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
                    Transaction No. : &nbsp;<asp:Label runat="server" ID="lbl" />
                </div>
                <div>                
                    <ul class="panel-controls"> 
                        &nbsp;                                                       
                        <li><asp:LinkButton runat="server" ID="lnkAddD" OnClick="lnkAddD_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDeleteD" OnClick="lnkDeleteD_Click" Text="Delete" CssClass="control-primary" /></li>      
                                     
                    </ul>  
                    <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="lnkDeleteD" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                                                                                                                                        
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="DTRShiftAllocNo;Code" Width="100%">
                            <Columns>    
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditD" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditD_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="ProjectDesc" Caption="Project" />
                                <dx:GridViewDataTextColumn FieldName="Task" Caption="Task"  PropertiesTextEdit-EncodeHtml="false" />    
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
                                    
                                </dx:GridViewCommandColumn>                   
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="imgClose" BackgroundCssClass="modalBackground" />

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none" >
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
                    <asp:Textbox ID="txtDTRShiftNo" CssClass="form-control" runat="server" ></asp:Textbox>
                   
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-4 control-label has-space ">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtDTRShiftTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass=" form-control" Placeholder="Autonumber"></asp:TextBox>
                     <asp:Checkbox ID="txtIsCrew" CssClass="form-control" runat="server" Visible="false" ></asp:Checkbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_SubordinateAppr_Crew" CompletionSetCount="0"
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                             var crew = Split(eventArgs.get_value(), 1);
                             //ViewCrew(crew);
                         }

                         function Split(obj, index) {
                             var items = obj.split("|");
                             for (i = 0; i < items.length; i++) {
                                 if (i == index) {
                                     return items[i];
                                 }
                             }
                         }

                         function ViewCrew(fval) {
                             var isCrew = fval;
                             if (isCrew == "True") {
                                 $('#divTime').removeAttr("style");
                                 $('#divShift').css({ 'display': 'none' });
                                 $('#divShiftmon').css({ 'display': 'none' });
                                 $('#divShifttue').css({ 'display': 'none' });
                                 $('#divShiftwed').css({ 'display': 'none' });
                                 $('#divShiftthu').css({ 'display': 'none' });
                                 $('#divShiftfri').css({ 'display': 'none' });
                                 $('#divShiftsat').css({ 'display': 'none' });
                                 $('#divShiftsun').css({ 'display': 'none' });
                                 jQuery('#' + '<%=(cboShiftNo.ClientID)%>').rules("add", { required: false });
                                 jQuery('#' + '<%=(txtIn1.ClientID)%>').rules("add", { required: true });
                                 jQuery('#' + '<%=(txtOut1.ClientID)%>').rules("add", { required: true });
                             } else {
                                 $('#divTime').css({ 'display': 'none' });
                                 $('#divShift').removeAttr("style");
                                 $('#divShiftmon').removeAttr("style");
                                 $('#divShifttue').removeAttr("style");
                                 $('#divShiftwed').removeAttr("style");
                                 $('#divShiftthu').removeAttr("style");
                                 $('#divShiftfri').removeAttr("style");
                                 $('#divShiftsat').removeAttr("style");
                                 $('#divShiftsun').removeAttr("style");
                                 jQuery('#' + '<%=(cboShiftNo.ClientID)%>').rules("add", { required: true });
                                 jQuery('#' + '<%=(txtIn1.ClientID)%>').rules("add", { required: false });
                                 jQuery('#' + '<%=(txtOut1.ClientID)%>').rules("add", { required: false });

                             }

                         }
                     </script>
                    
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">
                      <asp:TextBox ID="txtDateFrom" runat="server" CssClass="required form-control" style="display:inline-block;" Placeholder="From"></asp:TextBox> 
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtDateFrom"
                        Format="MM/dd/yyyy" />  
                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtDateFrom"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                                    
                        <asp:RangeValidator
                        ID="RangeValidator3"
                        runat="server"
                        ControlToValidate="txtDateFrom"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                    
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender4"
                        TargetControlID="RangeValidator3" />                                                                           
               </div>
            
          
                <%--<label class="col-md-1 control-label">To :</label>--%>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="required form-control" style="display:inline-block;"  Placeholder="To"></asp:TextBox> 
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtDateTo"
                        Format="MM/dd/yyyy" />  
                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtDateTo"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                                    
                        <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtDateTo"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                    
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender2"
                        TargetControlID="RangeValidator3" />                                                                           
                </div>
            </div>
            
            <div class="form-group" id="divShift"  style="display:block;">
                <label class="col-md-4 control-label has-required">Shift :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNo" DataMember="EShiftL" runat="server"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboShiftNo_SelectedIndexChanged" ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group" id="divShiftmon" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Shift Mon :</label>
                    <div class="col-md-5">
                        <asp:Dropdownlist ID="cboShiftNoMon" DataMember="EShiftL" runat="server" Enabled="false"  CssClass="form-control">
                        </asp:Dropdownlist>
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox runat="server" ID="txtisshiftmon" onclick="validateShift(this,1)" />&nbsp;<span>click to enable.</span>
                    </div>
                </div>
                <div class="form-group" id="divShifttue" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Shift Tue :</label>
                    <div class="col-md-5">
                        <asp:Dropdownlist ID="cboShiftNoTue" DataMember="EShiftL" runat="server" Enabled="false"  CssClass="form-control">
                        </asp:Dropdownlist>
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox runat="server" ID="txtisshifttue" onclick="validateShift(this,2)" />&nbsp;<span>click to enable.</span>
                    </div>
                </div>
                <div class="form-group" id="divShiftwed" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Shift Wed :</label>
                    <div class="col-md-5">
                        <asp:Dropdownlist ID="cboShiftNoWed" DataMember="EShiftL" runat="server" Enabled="false"  CssClass="form-control">
                        </asp:Dropdownlist>
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox runat="server" ID="txtisshiftwed" onclick="validateShift(this,3)" />&nbsp;<span>click to enable.</span>
                    </div>
                </div>
                <div class="form-group" id="divShiftthu" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Shift Thu :</label>
                    <div class="col-md-5">
                        <asp:Dropdownlist ID="cboShiftNoThu" DataMember="EShiftL" runat="server" Enabled="false"  CssClass="form-control">
                        </asp:Dropdownlist>
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox runat="server" ID="txtisshiftthu" onclick="validateShift(this,4)" />&nbsp;<span>click to enable.</span>
                    </div>
                </div>
                <div class="form-group" id="divShiftfri" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Shift Fri :</label>
                    <div class="col-md-5">
                        <asp:Dropdownlist ID="cboShiftNoFri" DataMember="EShiftL" runat="server" Enabled="false"  CssClass="form-control">
                        </asp:Dropdownlist>
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox runat="server" ID="txtisshiftfri" onclick="validateShift(this,5)" />&nbsp;<span>click to enable.</span>
                    </div>
                </div>
                <div class="form-group" id="divShiftsat" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Shift Sat :</label>
                    <div class="col-md-5">
                        <asp:Dropdownlist ID="cboShiftNoSat" DataMember="EShiftL" runat="server" Enabled="false"  CssClass="form-control">
                        </asp:Dropdownlist>
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox runat="server" ID="txtisshiftsat" onclick="validateShift(this,6)" />&nbsp;<span>click to enable.</span>
                    </div>
                </div>
                <div class="form-group" id="divShiftsun" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Shift Sun :</label>
                    <div class="col-md-5">
                        <asp:Dropdownlist ID="cboShiftNoSun" DataMember="EShiftL" runat="server" Enabled="false"  CssClass="form-control">
                        </asp:Dropdownlist>
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox runat="server" ID="txtisshiftsun" onclick="validateShift(this,7)" />&nbsp;<span>click to enable.</span>
                    </div>
                </div>
            <div class="form-group" id="divTime"  style="display:none;">
                <label class="col-md-4 control-label has-required">Shift Time In :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtIn1" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
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
             
                <label class="col-md-1 control-label">Out:</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOut1" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
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
            <div class="form-group" style=" display:none;">
                    <label class="col-md-4 control-label has-space">
                    Charge To :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboCostCenterNo" runat="server"  CssClass="form-control">
                        </asp:Dropdownlist>
                    </div>
                </div>
            

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Reason :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtReason" TextMode="MultiLine" Rows="3"  runat="server"  CssClass="form-control required"></asp:Textbox>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Approval Status :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboApprovalStatNo" DataMember="EApprovalStat" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                </div>
            </div>
         </div>
          <!-- Footer here -->
         <br /><br />
        
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

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="Fieldset1">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4></h4>
                <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRShiftAllocNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"   Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Project :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboProjectNo" DataMember="EProject" CssClass="form-control" runat="server" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Task :</label>
                <div class="col-md-6">
                    <%--<asp:Textbox ID="txtRemarks" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" 
                        ></asp:Textbox>--%>
                    <dx:ASPxHtmlEditor ID="txtTask" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                </div>
            </div>
            <div class="form-group">
                <br />
            </div>

        </div>
        
         </fieldset>
</asp:Panel>

</asp:Content>
