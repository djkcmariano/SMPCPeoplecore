<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayLoanList.aspx.vb" Inherits="Secured_PayLoanList" EnableEventValidation="false" %>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

    <div class="page-content-wrap">
<script type="text/javascript">

//    $(document).ready(function () {
//        $('#txtBegBalance').keyup(calculate);
//        $('#txtInterestRate').keyup(calculate);
//    });

//    function calculate(e) {
//        $('#txtTotalAmount').val($('#txtInterestRate').val() + $('#txtBegBalance').val());

//        alert($('#txtTotalAmount').val($('#txtInterestRate').val() + $('#txtBegBalance').val()));
//    }

    function disableenable(chk) {
        var fval = chk.value;

        if (fval == '2') {
            document.getElementById("ctl00_cphBody_txtInterest").disabled = false;
            document.getElementById("ctl00_cphBody_txtInterestRate").disabled = true;
            document.getElementById("ctl00_cphBody_txtMaturityDate").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_txtInterest").disabled = true;
            document.getElementById("ctl00_cphBody_txtInterestRate").disabled = false;
            document.getElementById("ctl00_cphBody_txtMaturityDate").disabled = true;
        }
    }
    function disableenable_behind(fval) {


        if (fval == '2') {
            document.getElementById("ctl00_cphBody_txtInterest").disabled = false;
            document.getElementById("ctl00_cphBody_txtInterestRate").disabled = true;
            document.getElementById("ctl00_cphBody_txtMaturityDate").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_txtInterest").disabled = true;
            document.getElementById("ctl00_cphBody_txtInterestRate").disabled = false;
            document.getElementById("ctl00_cphBody_txtMaturityDate").disabled = true;
        }
    }

    function totalamount_principal() {

        var principal = document.getElementById("ctl00_cphBody_txtPrincipalAmount").value;
        document.getElementById("ctl00_cphBody_txtBegBalance").value = Number(principal);
        totalamount();
    }

    function totalamount() {

        var interest = document.getElementById("ctl00_cphBody_txtInterestRate").value;
        var begbalance = document.getElementById("ctl00_cphBody_txtBegBalance").value;
        document.getElementById("ctl00_cphBody_txtTotalAmount").value = Number(begbalance) + Number(interest);
        document.getElementById("ctl00_cphBody_txtBalance").value = Number(begbalance) + Number(interest);

        var total = Number(begbalance) + Number(interest);

        var noofpayment = document.getElementById("ctl00_cphBody_txtNoOfPayment").value;
        var amort = document.getElementById("ctl00_cphBody_txtAmort").value

        if (Number(noofpayment) > 0 && Number(total) > 0) {
            document.getElementById("ctl00_cphBody_txtAmort").value = total / noofpayment;
        } else if (Number(amort) > 0 && Number(total) > 0) {
            document.getElementById("ctl00_cphBody_txtNoOfPayment").value = total / amort;
        }

    }

    function noofpayment() {
        var total = document.getElementById("ctl00_cphBody_txtTotalAmount").value;
        var noofpayment = document.getElementById("ctl00_cphBody_txtNoOfPayment").value;
        if (Number(noofpayment) > 0 && Number(total) > 0) {
            document.getElementById("ctl00_cphBody_txtAmort").value = total / noofpayment;
        }
    }
    function amort() {
        var total = document.getElementById("ctl00_cphBody_txtTotalAmount").value;
        var amort = document.getElementById("ctl00_cphBody_txtAmort").value;
        if (Number(amort) > 0 && Number(total) > 0) {
            document.getElementById("ctl00_cphBody_txtNoOfPayment").value = total / amort;
        }
    }
</script>
     
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
                                        <asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                    </li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExport" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                            <Content>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">
                                    Filter By :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control" />
                                    </div>
                                    <ajaxToolkit:CascadingDropDown ID="cdlfilterby" TargetControlID="cbofilterby" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterBy" runat="server" Category="tNo" LoadingText="Loading..." />
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">
                                    Filter Value :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control" />
                                    </div>
                                    <ajaxToolkit:CascadingDropDown ID="cdlfiltervalue" TargetControlID="cbofiltervalue" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterValue" runat="server" Category="tNo" ParentControlID="cbofilterby" LoadingText="Loading..." PromptText="-- Select --" />
                                </div>
                            </Content>
                        </uc:Filter>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXWOSearch" KeyFieldName="LoanNo">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="TransNo" />
                                <dx:GridViewDataTextColumn FieldName="RefNo" Caption="Loan No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="Fullname" Caption="Employee <br/> Name" />
                                <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll <br/> Group"/>
                                <dx:GridViewDataTextColumn FieldName="PayDeductTypeDesc" Caption="Loan Type" />
                                <dx:GridViewDataTextColumn FieldName="GrantedDate" Caption="Granted <br/> Date"/>
                                <dx:GridViewDataTextColumn FieldName="DeductStartDate" Caption="Deduct <br/> Start"/>
                                <dx:GridViewDataTextColumn FieldName="PrincipalAmount" Caption="Principal" PropertiesTextEdit-DisplayFormatString="{0:N2}"/>
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="TotalAmount" Caption="Total <br/> Amount" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="BegBalance" Caption="Beg. Balance" Visible="false" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                                <dx:GridViewDataTextColumn FieldName="InterestRate" Caption="Interest" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="NoOfPayment" Caption="No. Of Payments" Visible="false"  />
                                <dx:GridViewDataTextColumn FieldName="Amort" Caption="Amort" PropertiesTextEdit-DisplayFormatString="{0:N2}"/>
                                <dx:GridViewDataTextColumn FieldName="TotalPayment" Caption="Total <br/> Payment" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Balance" Caption="Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="LoanFormulaTypeDesc" Caption="Formula" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                        <asp:SqlDataSource runat="server" ID="SqlDataSource1"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTab2No" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch2_Click" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <div class="panel-title">
                            <asp:Label runat="server" ID="lbl" />
                        </div>
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAddDetl2" OnClick="lnkAddDetl2_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDeleteDetl2" OnClick="lnkDeleteDetl2_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDeleteDetl3" OnClick="lnkDeleteDetl3_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExportgrdDetl2" OnClick="lnkExportgrdDetl2_Click" Text="Export" CssClass="control-primary" Visible="false" />
                                    </li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkDeleteDetl2" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="lnkDeleteDetl3" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExportgrdDetl2" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="LoanPayNo" Width="100%">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" Visible='<%# Bind("IsEnabled") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="CodeDetl" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." />
                                <dx:GridViewDataTextColumn FieldName="PayPeriod" Caption="Payroll Period" />
                                <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Payout Date" />
                                <dx:GridViewDataTextColumn FieldName="MonthDesc" Caption="Month" />
                                <dx:GridViewDataTextColumn FieldName="applicableYear" Caption="Year" />
                                <dx:GridViewDataTextColumn FieldName="Amort" Caption="Payment" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                                <dx:GridViewDataTextColumn FieldName="RemarksPrepaid" Caption="Remarks" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />
                        <dx:ASPxGridView ID="grdDetl2" ClientInstanceName="grdDetl2" runat="server" KeyFieldName="LoanPaySchedNo" Width="100%" Visible="false">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditDetl2" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl2_Click" Visible='<%# Bind("IsEnabled") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="CodeDetl" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="OrderNo" Caption="Order No." />
                                <dx:GridViewDataTextColumn FieldName="ScheduleDate" Caption="Schedule" />
                                <dx:GridViewDataTextColumn FieldName="BalancePrev" Caption="Loan Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="PrincipalAmort" Caption="Principal" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="InterestAmort" Caption="Interest" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="TotalAmort" Caption="Loan Amortization" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Payment" Caption="Payment" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                                <dx:GridViewDataTextColumn FieldName="BalancePres" Caption="Amortization Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Visible="false" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Visible="True" />
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsBehavior AllowSort="true" />
                            <Settings ShowFooter="true" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="PrincipalAmort" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="InterestAmort" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="TotalAmort" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Payment" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="BalancePres" SummaryType="Sum" />
                            </TotalSummary>
                            <SettingsContextMenu Enabled="true">
                                <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" />
                            </SettingsContextMenu>
                            <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="True" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportgrdDetl2" runat="server" GridViewID="grdDetl2" />
                        <dx:ASPxGridView ID="grdDetl3" ClientInstanceName="grdDetl3" runat="server" KeyFieldName="LoanPayForwNo" Width="100%">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditDetl3" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl3_Click" Enabled='<%# Bind("IsEnabled") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." />
                                <dx:GridViewDataTextColumn FieldName="Amort" Caption="Amortization" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Payment" Caption="Payment" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Balance" Caption="Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Pay Date" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>
                            <Settings ShowFooter="true" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Amort" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Payment" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="Balance" SummaryType="Sum" />
                            </TotalSummary>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="grdDetl3" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnShow" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow" PopupControlID="Panel1" CancelControlID="imgClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />
            </div>
            <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Reference No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtRefNo" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Name of Employee :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" />
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                TargetControlID="txtFullName" MinimumPrefixLength="2" 
                CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                CompletionListCssClass="autocomplete_completionListElement" 
                CompletionListItemCssClass="autocomplete_listItem" 
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"                
                OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx"  />
                        <script type="text/javascript">


                        function getRecord(source, eventArgs) {
                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Loan Type :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayDeductTypeNo"  runat="server" CssClass="required form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Loan Formula Type :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboLoanFormulaTypeNo" DataMember="ELoanFormulaType"  runat="server" onchange="disableenable(this);" CssClass="required form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Date Granted :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtDateGranted" runat="server" SkinID="txtdate" CssClass="required form-control" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDateGranted" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtDateGranted" />
                        <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtDateGranted" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Start of Deduction :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtDeductionStart" runat="server" SkinID="txtdate" CssClass="required form-control" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDeductionStart" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtDeductionStart" />
                        <asp:CompareValidator runat="server" ID="CompareValidator2" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtDeductionStart" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Maturity Date :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtMaturityDate" runat="server" SkinID="txtdate" CssClass="form-control"/>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="MM/dd/yyyy" TargetControlID="txtMaturityDate" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtMaturityDate" />
                        <asp:CompareValidator runat="server" ID="CompareValidator5" ErrorMessage="<b>Please enter valid entry</b>"  Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtMaturityDate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Interest (%) :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtInterest" runat="server" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Principal Amount :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPrincipalAmount" onkeyup="totalamount_principal()" runat="server" CssClass="required form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Beginning Balance :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtBegBalance" onkeyup="totalamount();" runat="server" CssClass="required form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Interest (amount):</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtInterestRate" onkeyup="totalamount();" runat="server" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Total Amount :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    No. of Payment :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNoOfPayment" onkeyup="noofpayment();" runat="server" CssClass="required form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Amortization :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtAmort" onkeyup="amort();" runat="server" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Total Payment :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTotalPayment" ReadOnly="true" runat="server" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Balance :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtBalance" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Payroll Schedule :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayScheduleNo" DataMember="EPaySchedule" runat="server" CssClass="required form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    LOAN PRE-PAYMENT :</label>
                    <div class="col-md-7">
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:Checkbox ID="chkIsPrepaid" runat="server" Text="&nbsp;Prepaid" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Prepaid Date :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPrepaidDate" runat="server" SkinID="txtdate" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtPrepaidDate" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtPrepaidDate" />
                        <asp:CompareValidator runat="server" ID="CompareValidator3" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPrepaidDate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Prepaid By :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPrepaidBy" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:Checkbox ID="chkIsDeductBonus" runat="server" Text="&nbsp;Include in bonus" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:Checkbox ID="chkIsSuspend" runat="server" Text="&nbsp;Suspend deduction" />
                    </div>
                </div>
                <br />
            </div>
            <!-- Footer here -->
        </fieldset>
    </asp:Panel>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button1" PopupControlID="Panel2" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel2" runat="server" CssClass="entryPopup2" style="display:none;">
        <fieldset class="form" id="fsDetl2">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveDetl" CssClass="fa fa-floppy-o submit fsDetl2 lnkSaveDetl" OnClick="lnkSaveDetl_Click"  />
            </div>
            <div  class="entryPopupDetl2 form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Detail No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCodeDetl" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Prepaid date :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtDatePrepaid" runat="server" CssClass="required form-control" SkinID="txtdate" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDatePrepaid" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtDatePrepaid" />
                        <asp:CompareValidator runat="server" ID="CompareValidator4" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtDatePrepaid" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Amount :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="required form-control number" SkinID="txtdate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRemarksPrepaid" runat="server" Rows="4" TextMode="multiLine"  CssClass="form-control" />
                    </div>
                </div>
                <br />
            </div>
        </fieldset>
    </asp:Panel>
    <asp:Button ID="btnShowLoanSched" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowLoanSched" PopupControlID="Panel3" CancelControlID="lnkCloseDetlLoanSched" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel3" runat="server" CssClass="entryPopup2" style="display:none;">
        <fieldset class="form" id="fsDetl3">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkCloseDetlLoanSched" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveDetlLoanSched" CssClass="fa fa-floppy-o submit fsDetl2 lnkSaveDetl" OnClick="lnkSaveDetl2_Click"  />
            </div>
            <div  class="entryPopupDetl2 form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Detail No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCodeDetl2" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Schedule Date :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtScheduleDate" runat="server" CssClass="required form-control" SkinID="txtdate" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="MM/dd/yyyy" TargetControlID="txtScheduleDate" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtScheduleDate" />
                        <asp:CompareValidator runat="server" ID="CompareValidator6" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtScheduleDate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Loan Balance :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtBalancePrev" runat="server" CssClass="required form-control number" SkinID="txtdate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Principal Amort :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPrincipalAmort" runat="server" CssClass="required form-control number" SkinID="txtdate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Interest Amort :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtInterestAmort" runat="server" CssClass="form-control number" SkinID="txtdate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Total Amortization :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTotalAmort" runat="server" CssClass="form-control number" SkinID="txtdate" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Payment Amount:</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPayment" runat="server" CssClass="form-control number" SkinID="txtdate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Amortization Balance:</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtBalancePres" runat="server" CssClass="form-control number" SkinID="txtdate" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRemarksLoanSched" runat="server" Rows="4" TextMode="multiLine"  CssClass="form-control" />
                    </div>
                </div>
                <br />
            </div>
        </fieldset>
    </asp:Panel>
    <asp:Button ID="Button2" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="Button2" PopupControlID="Panel5" CancelControlID="Linkbutton1" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel5" runat="server" CssClass="entryPopup2" style="display:none;">
        <fieldset class="form" id="Fieldset1">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveDetl3" CssClass="fa fa-floppy-o submit Fieldset1 lnkSaveDetl3" OnClick="lnkSaveDetl3_Click"  />
            </div>
            <div  class="entryPopupDetl2 form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Detail No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtLoanPayForwNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Schedule Date :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPayDate" runat="server" CssClass="required form-control" SkinID="txtdate" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="MM/dd/yyyy" TargetControlID="txtPayDate" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtPayDate" />
                        <asp:CompareValidator runat="server" ID="CompareValidator7" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPayDate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Amortization :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtAmortForw" runat="server" CssClass="form-control number" SkinID="txtdate" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Payment :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPaymentForw" runat="server" CssClass="form-control number" SkinID="txtdate" />
                    </div>
                </div>
                <br />
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>
