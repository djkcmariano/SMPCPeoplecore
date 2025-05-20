<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="BENLeaveMoneApplication.aspx.vb" Inherits="Secured_BENLeaveMoneApplication" MaintainScrollPositionOnPostback="true" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function disableenable_behind(fval) {
        if (fval == '0') {
            $("#trSLBegBal").hide();
            $("#trSLApplied").hide();
            $("#trSLEndBal").hide();
            //$("#trSLVLReason").hide();
        } else {
            $("#trSLBegBal").show();
            $("#trSLApplied").show();
            $("#trSLEndBal").show();
            //$("#trSLVLReason").show();
        }
    }

    function disableenable_behind_Reason(fval) {
        if (fval == '0') {
            $("#trSLVLReason").hide();
        } else {
            ("#trSLVLReason").show();
        }
    }

    function disableenable_behind_Rate(fval, Id) {
        if (fval == '0') {
            if (Id == '1') { $("#trMonthlyRate").hide(); $("#trMonetaryValue").hide(); };
            if (Id == '2') { $("#trFactorRate").hide(); };
        } else {
            if (Id == '1') { $("#trMonthlyRate").show(); $("#trMonetaryValue").show(); };
            if (Id == '2') { $("#trFactorRate").show(); };
        }
    }

    function VLBalance() {

        var VLBegBal = document.getElementById("ctl00_cphBody_txtVLBegBal").value;
        var VLApplied = document.getElementById("ctl00_cphBody_txtVLApplied").value;
        document.getElementById("ctl00_cphBody_txtVLEndBal").value = Number(VLBegBal) - Number(VLApplied);

        var VLEndBal = Number(VLBegBal) - Number(VLApplied);

        if (VLEndBal < 0) {
            document.getElementById("ctl00_cphBody_txtVLEndBal").value = 0;
        };
    }

    function SLBalance() {

        var SLBegBal = document.getElementById("ctl00_cphBody_txtSLBegBal").value;
        var SLApplied = document.getElementById("ctl00_cphBody_txtSLApplied").value;
        document.getElementById("ctl00_cphBody_txtSLEndBal").value = Number(SLBegBal) - Number(SLApplied);

        var SLEndBal = Number(SLBegBal) - Number(SLApplied);

        if (SLEndBal < 0) {
            document.getElementById("ctl00_cphBody_txtSLEndBal").value = 0;
        };
    }

    function TotalApplied() {

        var VLBegBal = document.getElementById("ctl00_cphBody_txtVLBegBal").value;
        var SLBegBal = document.getElementById("ctl00_cphBody_txtSLBegBal").value;

        var Allow_VL = document.getElementById("ctl00_cphBody_txtTotalAllow_VL").value;
        var Allow_SLVL = document.getElementById("ctl00_cphBody_txtTotalAllow_SLVL").value;

        var TypeNo = document.getElementById("ctl00_cphBody_cboLeaveMonitizedTypeNo").value;
        var AppliedDays = document.getElementById("ctl00_cphBody_txtTotalApplied").value;
        var VLApplied = 0, SLApplied = 0;

        var txtApp = document.getElementById("ctl00_cphBody_txtTotalApplied");

        if (TypeNo == '1') { //50% or more

            if (Number(AppliedDays) > Number(Allow_SLVL) && Number(Allow_SLVL) != 0) {
                VLApplied = 0;
                SLApplied = 0;
                document.getElementById("lblMsgApplied").innerHTML = "Applied days is greater than the allowable for monetization.\nPlease validate your entry.";
            }
            else if (Number(AppliedDays) <= Number(Allow_SLVL) && Number(Allow_SLVL) != 0) {
                if (Number(VLBegBal) - 5 >= 0) {

                };

                VLApplied = Number(VLBegBal) - 5;
                SLApplied = Number(AppliedDays) - Number(VLApplied);
                document.getElementById("lblMsgApplied").innerHTML = "Please validate your entry...";
            }
            else {
                VLApplied = Number(Allow_VL);
                SLApplied = Number(AppliedDays) - Number(Allow_VL);
                document.getElementById("lblMsgApplied").innerHTML = "Please validate your entries...";
            };

            document.getElementById("ctl00_cphBody_txtVLApplied").value = Number(VLApplied);
            document.getElementById("ctl00_cphBody_txtSLApplied").value = Number(SLApplied);

        };

        if (TypeNo == '2') {
            document.getElementById("ctl00_cphBody_txtVLApplied").value = Number(AppliedDays);
        };

    }

    function VL_SL_Balance() {
        TotalApplied();
        VLBalance();
        SLBalance();
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
                        <li><asp:LinkButton runat="server" ID="lnkResubmit" OnClick="lnkResubmit_Click" Text="Resubmit" CssClass="control-primary" CausesValidation="false" Visible="false"/></li>                                               
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" CausesValidation="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" CausesValidation="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDisApproved" OnClick="lnkDisApproved_Click" Text="Disapprove" CssClass="control-primary" CausesValidation="false" /></li>  
                        <li><asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary" CausesValidation="false" /></li> 
                        <li><asp:LinkButton runat="server" ID="lnkForProcessing" OnClick="ForProcessing_Click" Text="Approve For Processing" CssClass="control-primary" CausesValidation="false" /></li> 
                        <li><asp:LinkButton runat="server" ID="lnkForwardPayroll" OnClick="lnkForwardPayroll_Click" Text="Forward To Payroll" CssClass="control-primary" CausesValidation="false" /></li> 
                        <li><asp:LinkButton runat="server" ID="lnkPaid" OnClick="lnkPaid_Click" Text="Paid" CssClass="control-primary" CausesValidation="false" /></li> 
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" CausesValidation="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" CausesValidation="false" /></li>

                        <uc:ConfirmBox runat="server" ID="cfbResubmit" TargetControlID="lnkResubmit" ConfirmMessage="Are you sure you want to resubmit selected application(s)?"  />
                        <uc:ConfirmBox runat="server" ID="cfbApproved" TargetControlID="lnkApproved" ConfirmMessage="Are you sure you want to approve the selected transaction(s)?" />
                        <uc:ConfirmBox runat="server" ID="cfbDisApproved" TargetControlID="lnkDisApproved" ConfirmMessage="Are you sure you want to disapprove the selected transaction(s)? Transaction(s) can no longer be modifed." />
                        <uc:ConfirmBox runat="server" ID="cfbCancel" TargetControlID="lnkCancel" ConfirmMessage="Are you sure you want to cancel the selected transaction(s)? Transaction(s) can no longer be modifed." />
                        <uc:ConfirmBox runat="server" ID="cfbForProcessing" TargetControlID="lnkForProcessing" ConfirmMessage="Are you sure you want to approve selected transaction(s) for processing?" />
                        <uc:ConfirmBox runat="server" ID="cfbForwardPayroll" TargetControlID="lnkForwardPayroll" ConfirmMessage="Are you sure you want to forward selected transaction(s) to Payroll for payment? Transaction(s) can no longer be modifed." />
                        <uc:ConfirmBox runat="server" ID="cfbPaid" TargetControlID="lnkPaid" ConfirmMessage="Are you sure you want to tag as paid the selected transaction(s)? Transaction(s) can no longer be modifed." />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="LeaveMonitizedNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" CausesValidation="false" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="LeaveMonitizedTypeDesc" Caption="Monetization Type" />

                                <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Leave Type" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="LeaveHrs" Caption="Leave<br />Hrs" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ActualLeavehrs" Caption="Acutal Leave<br />Hrs" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="xLeaveHrs" Caption="Leave<br />Days" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ActualNoofLeaves" Caption="Paid<br />Days" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="HourlyRate" Caption="Hourly Rate" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="CurrentRate" Caption="MBS" Visible="true" PropertiesTextEdit-DisplayFormatString="{0:N2}" />

                                <dx:GridViewDataTextColumn FieldName="VLAppliedDays" Caption="<center>VL Applied<br />(Days)</center>" Visible="true" />
                                <dx:GridViewDataTextColumn FieldName="SLAppliedDays" Caption="<center>SL Applied<br />(Days)</center>" Visible="true" />
                                <dx:GridViewDataTextColumn FieldName="AppliedDays" Caption="<center>Total Applied<br />(Days)</center>" Visible="true" />

                                <dx:GridViewDataTextColumn FieldName="VLAmount" Caption="VL Amount" Visible="false" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="SLAmount" Caption="SL Amount" Visible="false" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="TotalAmount" Caption="Total Amount" Visible="true" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="LeaveMoneReasonTypeDesc" Caption="Justification" />
                                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Other Reason" Visible="false" />

                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="<center>With<br />Required<br />Document?</center>" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:Label ID="lblDocs" runat="server" Visible="true" Text='<%# Bind("DocsRequirement") %>' Width="100%"></asp:Label>                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataCheckColumn FieldName="IsRetiree" Caption="<center>Compulsary<br />Retiree</center>" Visible="true" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />

                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status" Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveby" Caption="Approved /<br />Disapproved<br />By" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="xAppDate" Caption="Approved /<br />Disapproved<br />Date" Width="5%"  />

                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Approver(s)" Width="12%" Visible="false" />

                                <dx:GridViewDataTextColumn FieldName="yyRemarks" Caption="Remarks" Width="12%" />

                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" FieldName="yyxRemarks" Caption=" " HeaderStyle-HorizontalAlign="Center" Width="10%">
                                    <DataItemTemplate>
                                        <asp:TextBox ID="txtxRemarks" runat="server" Text='<%# Bind("yyxRemarks") %>' Placeholder="Leave remarks here..."
                                            TextMode="MultiLine" Rows="2" style=" font-family: verdana; font-size:11px; border: solid 1px #757575; width: 150px; height:auto" CssClass="form-control"></asp:TextBox>                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Width="5%" />

                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date<br />Applied &nbsp;&nbsp;" Width="5%" HeaderStyle-VerticalAlign="Top" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Applied By" Visible="false" />
                                
                                
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

<%--                            <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
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
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />  --%>                             
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
                    <asp:Label runat="server" ID="lbl" />
                </div>
                <div>                
                    <ul class="panel-controls"> 
                        &nbsp;                                                       
                        <li></li>                        
                    </ul>                                                                                                                                                                         
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" Width="100%">
                            <Columns>                                
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name" />
                                <dx:GridViewDataTextColumn FieldName="LeaveCode" Caption="Leave Type" />
                                <dx:GridViewDataTextColumn FieldName="xxBalanceInDays" Caption="Beginning Balance (Days)"  /> 
                                <dx:GridViewDataTextColumn FieldName="xxAppliedInDays" Caption="Day(s) Applied"  />   
                                <dx:GridViewDataTextColumn FieldName="xxEndBalanceInDays" Caption="Ending Balance (Days)"  />   
                                                                                                                              
                                <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Paid Hours" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:TextBox runat="server" ID="txtPaidHrs" Width="100%" Text='<%# Bind("PaidHrs") %>' />
                                        <asp:HiddenField runat="server" ID="hifLeaveApplicationDetiNo" Value='<%# Bind("LeaveApplicationDetiNo") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn> --%>                           

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
            <div class="form-group" style="display:block;">
                <label class="col-md-4 control-label">NOTE:</label>
                <div class="col-md-6">
                    <asp:Label runat="server" ID="lblMsgNotice" style=" display: inline-block;"></asp:Label>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtLeaveMonitizedNo" CssClass="form-control" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtLeaveMonitizedTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-6">
                     <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." AutoPostBack="true" CausesValidation="false" OnTextChanged="ELeaveMonetized_WebValidate_Employee" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" CompletionSetCount="1" 
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
                <label class="col-md-4 control-label has-space">Transaction Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTransDate" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false"
                        ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtTransDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtTransDate"
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
                        ControlToValidate="txtTransDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender6"
                        TargetControlID="RangeValidator3"
                        />   
                </div>

            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Type of Leave Monetization :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboLeaveMonitizedTypeNo" runat="server" AutoPostBack="true" CausesValidation="false" DataMember="ELeaveMonitizedType" CssClass="form-control required"
                        ></asp:Dropdownlist>
<%--                    <br />
                    <table width="100%">
                    <tr>
                    <td style=" padding-left:10px; border:1px solid gray;">
                        <asp:Label runat="server" ID="lblMsgNotice2" style=" display: inline-block;"></asp:Label>
                    </td>
                    </tr>
                    </table>--%>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">ENTITLEMENT :</label>
                <div class="col-md-6">
                    <asp:Label runat="server" ID="lblMsgNotice2" style=" display: inline-block;"></asp:Label>
               </div>
            </div>

            <div class="form-group">
                <%--<label class="col-md-4 control-label has-space"></label>--%>
                <div class="col-md-12">
                <hr />
                <label ID="Label3" Style=" text-align:left; font-weight:bold; width:400px;">No. of Days Monetized for the Year (<span style=" font-style:italic; font-weight:normal;">System generated</span>)</label>
               </div>
            </div>

<%--            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-6">
                    <label ID="Label7" Style=" text-align:left; font-weight:bold; width:400px;">No. of Days Monetized for the Year (<span style=" font-style:italic; font-weight:normal;">System generated</span>)</label>
               </div>
            </div>--%>

            <div class="form-group">
                <label class="col-md-1 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalYear_VLOnly" SkinID="txtdate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
               </div>
               <div class="col-md-3">
                    <label ID="Label5" Style=" text-align:left; font-weight:normal;"> - Regular Monetization</label>
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalYear_SLVL_VL" SkinID="txtdate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
               </div>
               <div class="col-md-4">
                    <label ID="Label6" Style=" text-align:left; font-weight:normal;">- 50% or more VL/SL (VL only)</label>
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalYear_SLVL_SL" SkinID="txtdate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
               </div>
               <div class="col-md-4">
                    <label ID="Label8" Style=" text-align:left; font-weight:normal;">- 50% or more VL/SL (SL only)</label>
               </div>
            </div>

            <div class="form-group" style=" display:none;">
                <%--<label class="col-md-4 control-label has-space"></label>--%>
                <div class="col-md-12">
                <label ID="Label4" Style=" text-align:left; font-weight:bold; width:400px;">Allowable for Monetization (<span style=" font-style:italic; font-weight:normal;">System generated</span>)</label>
               </div>
            </div>

            <div class="form-group" style=" display:none;">
                <label class="col-md-1 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalAllow_VL" SkinID="txtdate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
               </div>
               <div class="col-md-3">
                    <label ID="Label7" Style=" text-align:left; font-weight:normal;"> - Regular Monetization</label>
               </div>
            </div>
            <div class="form-group" style=" display:none;">
                <label class="col-md-1 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalAllow_SLVL" SkinID="txtdate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
               </div>
               <div class="col-md-4">
                    <label ID="Label9" Style=" text-align:left; font-weight:normal;">- 50% or more VL/SL</label>
               </div>
            </div>

            <div class="form-group">
                <%--<label class="col-md-4 control-label has-space"></label>--%>
                <div class="col-md-12">
                <hr />
                <label ID="Label10" Style=" text-align:left; font-weight:bold; width:400px;">Beginning Balance (Days) (<span style=" font-style:italic; font-weight:normal;">System generated</span>)</label>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-1 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtVLBegBal" SkinID="txtdate" runat="server" Enabled="false" CssClass="form-control"
                        ></asp:TextBox>
               </div>
               <label class="col-md-2 control-label has-space" style=" text-align:left;"> - VL</label>
               <div class="col-md-4">
                    <label ID="Label2" style=" font-style:italic; font-weight:normal; vertical-align:middle; text-align:left;" >(as of this application)</label>
               </div>
            </div>

            <div class="form-group" id="trSLBegBals">
                <label class="col-md-1 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSLBegBal" SkinID="txtdate" runat="server" Enabled="false" CssClass="form-control"
                        ></asp:TextBox>
               </div>
               <label class="col-md-2 control-label has-space" style=" text-align:left;"> - SL</label>
               <div class="col-md-4">
                    <label ID="Label1" style=" font-style:italic; font-weight:normal; vertical-align:middle; text-align:left;">(as of this application)</label>
               </div>
            </div>

            <div class="form-group">
                <%--<label class="col-md-4 control-label has-space"></label>--%>
                <div class="col-md-12">
                <label ID="Label16" Style=" text-align:left; font-weight:bold; width:600px; font-style:normal; text-decoration:none;">Remaining Allowable for Monetization based on Beginning Balance (<span style=" font-style:italic; font-weight:normal;">System generated</span>)</label>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-1 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalAllow_ToApply_VL" SkinID="txtdate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
               </div>
               <div class="col-md-3">
                    <label ID="Label17" Style=" text-align:left; font-weight:normal;"> - Regular Monetization <br />*Net of 5 days mandatory leave</label>
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-1 control-label has-space"></label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalAllow_ToApply_SLVL" SkinID="txtdate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
               </div>
               <div class="col-md-4">
                    <label ID="Label18" Style=" text-align:left; font-weight:normal;">- 50% or more VL/SL</label>
               </div>
            </div>


            <div class="form-group">
                <%--<label class="col-md-4 control-label has-space"></label>--%>
                <div class="col-md-12">
                <label ID="Label11" Style=" text-align:left; font-weight:bold; width:400px; font-style:normal; text-decoration:none;">Application for Monetization</label>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">No. of Day(s) Applied :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalApplied" runat="server" CssClass="form-control required number" Width="150" AutoPostBack="true" OnTextChanged="txtTotalApplied_TextChanged"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" Operator="DataTypeCheck" Type="Integer" SetFocusOnError="true" Text="Please enter a valid whole number." ControlToValidate="txtTotalApplied" />
                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftb3" FilterMode="ValidChars" ValidChars="1234567890." TargetControlID="txtTotalApplied" />
                </div>
                <code class="col-md-4 control-label has-space" runat="server" id="lblMsgApplied" style=" font-weight:normal; vertical-align:middle; text-align:left;"></code>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">VL :</label>
                <div class="col-md-3">
                    <%--<asp:TextBox ID="txtVLApplied" SkinID="txtdate" runat="server"  CssClass="form-control required number" AutoPostBack="true" OnTextChanged="txtVLApplied_TextChanged"
                        ></asp:TextBox>--%>
                    <asp:TextBox ID="txtVLApplied" runat="server" CssClass="form-control number" Width="150" onkeyup="VLBalance();"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck" Type="Integer" SetFocusOnError="true" Text="Please enter a valid whole number." ControlToValidate="txtVLApplied" />
                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftb1" FilterMode="ValidChars" ValidChars="1234567890." TargetControlID="txtVLApplied" />
               </div>
               <div class="col-md-3">
                    <label ID="Label13" style=" font-style:italic; font-weight:normal; vertical-align:middle; text-align:left;" >(Auto-populated)</label>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">SL :</label>
                <div class="col-md-3">
                    <%--<asp:TextBox ID="txtSLApplied" SkinID="txtdate" runat="server"  CssClass="form-control required number" AutoPostBack="true" OnTextChanged="txtSLApplied_TextChanged"
                        ></asp:TextBox>--%>
                    <asp:TextBox ID="txtSLApplied" runat="server" CssClass="form-control number" Width="150" onkeyup="SLBalance();"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" Type="Integer" SetFocusOnError="true" Text="Please enter a valid whole number." ControlToValidate="txtSLApplied" />
                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ttb2" FilterMode="ValidChars" ValidChars="1234567890." TargetControlID="txtSLApplied" />
               </div>
               <div class="col-md-3">
                    <label ID="Label14" style=" font-style:italic; font-weight:normal; vertical-align:middle; text-align:left;" >(Auto-populated)</label>
               </div>
            </div>

            <div class="form-group" id="trMonthlyRate">
                <label class="col-md-4 control-label has-space">MBS :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCurrentRate" runat="server" CssClass="form-control number" Width="150" ReadOnly="true"></asp:TextBox>
                </div>
               <div class="col-md-3">
                    <label ID="Label19" style=" font-style:italic; font-weight:normal; vertical-align:middle; text-align:left;" >(Auto-populated)</label>
               </div>
            </div>

            <div class="form-group" id="trFactorRate">
                <label class="col-md-4 control-label has-space">Factor Rate :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtFactorRate" runat="server" CssClass="form-control number" Width="150" ReadOnly="true"></asp:TextBox>
                </div>
               <div class="col-md-3">
                    <label ID="Label20" style=" font-style:italic; font-weight:normal; vertical-align:middle; text-align:left;" >(Auto-populated)</label>
               </div>
            </div>

            <div class="form-group" id="trMonetaryValue">
                <label class="col-md-4 control-label has-space">Monetary Value :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control number" Width="150" Enabled="false"></asp:TextBox>
                </div>
               <div class="col-md-3">
                    <label ID="Label15" style=" font-style:italic; font-weight:normal; vertical-align:middle; text-align:left;" >(Auto-populated)</label>
               </div>
            </div>

            <div class="form-group">
                <%--<label class="col-md-4 control-label has-space"></label>--%>
                <div class="col-md-12">
                <label ID="Label12" Style=" text-align:left; font-weight:bold; width:400px;">Ending Balance (Days) (<span style=" font-style:italic; font-weight:normal;">System generated</span>)</label>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">VL :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtVLEndBal" SkinID="txtdate" runat="server" Enabled="false" CssClass="form-control"
                        ></asp:TextBox>
               </div>
            </div>

            <div class="form-group" id="trSLEndBals">
                <label class="col-md-4 control-label has-space">SL :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtSLEndBal" SkinID="txtdate" runat="server" Enabled="false" CssClass="form-control"
                        ></asp:TextBox>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-6">
               </div>
            </div>


            <div class="form-group" id="trSLVLReason">
                <label class="col-md-4 control-label has-required">Justification :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboLeaveMoneReasonTypeLNo" runat="server" CausesValidation="false" DataMember="ELeaveMoneReasonTypeL" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboLeaveMoneReasonTypeLNo_SelectedIndexChanged"></asp:Dropdownlist>
                    <table width="100%">
                    <tr>
                    <td style=" padding-left:0px; border:0px solid gray;">
                        <asp:Label runat="server" ID="lblMsgNotice3" style=" display: inline-block; position:absolute; "></asp:Label>
                    </td>
                    </tr>
                    </table>
               </div>
            </div>

            <div class="form-group" runat="server" id="trSLVLReasonOthers">
                <label class="col-md-4 control-label has-required">Please Specify :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtxRemarks" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control required"
                        ></asp:Textbox>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-6">
               </div>
            </div>


        </div>
        <br />
        
         </fieldset>
</asp:Panel>



</asp:Content>