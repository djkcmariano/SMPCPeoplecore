<%@ Control Language="VB" AutoEventWireup="false" CodeFile="JobOffer.ascx.vb" Inherits="Include_EvalTemplate" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Src="~/Include/ConfirmBox.ascx" TagName="ConfirmBox" TagPrefix="uc" %>  
<%@ Register Src="~/Include/ChatBox.ascx" TagName="ChatBox" TagPrefix="uc" %>

<style type="text/css">

.table th, .table td { 
     border-top: none !important; 
     border: 1px dotted gray;
     border-left:none;
 }
 
 
.chat_badge {
  position: relative;
  top: -12px;
  right: 13px;
  padding: 3px 7px;
  border-radius: 50%;
  background: red;
  color: white;
  font-size:xx-small;
  display: inline-block;
}


</style>


<script type="text/javascript">
    function avoidDoubleClick(elm) {
        elm.disabled = 'true';
        elm.value = 'Please wait...';
    }

    function getImmediateSuperior(source, eventArgs) {
        document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }
    function ResetImmediateSuperior() {
        if (document.getElementById('<%= txtSuperiorName.ClientID %>').value == "") {
            document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = "0";
        }
    }
</script>
    
<!-- Chat Box -->
<uc:ChatBox runat="server" ID="ChatBox2">
</uc:ChatBox>

<div class="row">
    <div class="panel panel-default" style="margin-bottom:10px;">
        <div class="table-responsive">
            <table class="table table-condensed"> 
                <tbody> 
                <tr> 
                    <td style="width:15%;text-align:left;"><strong><asp:label ID="lbltr1" runat="server" /></strong></td> 
                    <td style="width:35%;"><asp:label ID="lbltd1" runat="server" class="col-md-12 control-label" /></td>
                    <td style="width:15%;text-align:left;"><strong><asp:label ID="lbltr5" runat="server" /></strong></td> 
                    <td style="width:35%;"><asp:label ID="lbltd5" runat="server" class="col-md-12 control-label" /></td>
                </tr> 
                <tr> 
                    <td style="text-align:left;"><strong><asp:label ID="lbltr2" runat="server" /></strong></td> 
                    <td ><asp:label ID="lbltd2" runat="server" class="col-md-12 control-label" /></td>
                    <td style="text-align:left;"><strong><asp:label ID="lbltr6" runat="server" /></strong></td> 
                    <td ><asp:label ID="lbltd6" runat="server" class="col-md-12 control-label" /></td>
                </tr>
                <tr> 
                    <td style="text-align:left;"><strong><asp:label ID="lbltr3" runat="server" /></strong></td> 
                    <td ><asp:label ID="lbltd3" runat="server" class="col-md-12 control-label" /></td>
                    <td style="text-align:left;"><strong><asp:label ID="lbltr7" runat="server" /></strong></td> 
                    <td ><asp:label ID="lbltd7" runat="server" class="col-md-12 control-label" /></td>
                </tr>
                <tr>                    
                    <td style="text-align:left;"><strong><asp:label ID="lbltr4" runat="server" /></strong></td> 
                    <td ><asp:label ID="lbltd4" runat="server" class="col-md-12 control-label" /></td>
                    <td style="text-align:left;"><strong><asp:label ID="lbltr8" runat="server" /></strong></td> 
                    <td ><asp:label ID="lbltd8" runat="server" class="col-md-12 control-label" /></td>
                </tr>
                </tbody> 
            </table> 
        </div>
    </div>
</div>

<div class="row">
  <div class="panel panel-default" style="margin-bottom:0px;">
    <div class="panel-heading" style="padding:5px;">
            <div class="panel-title">
                <asp:Image runat="server" ID="imgPhoto" width="50" height="50" CssClass="img-circle" style="border: 2px solid white; padding:0px;margin:0px" />&nbsp;&nbsp;
                <a role="button" data-toggle="collapse" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                    <asp:label ID="lblName" runat="server" style="color:#069; cursor:pointer;" />
                </a>
            </div>            
                                              
             <div class="pull-right">  
                <div style="padding:10px;"> 
                     <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                        <ContentTemplate>
                                <asp:Button runat="server" ID="lnkAccept" CssClass="btn btn-primary" Text="Accept" OnClick="lnkAccept_Click" />
                                <asp:Button runat="server" ID="lnkDecline" CssClass="btn btn-primary" Text="Decline" OnClick="lnkDecline_Click" />
                                <asp:Button runat="server" ID="lnkReOffer" CssClass="btn btn-primary" Text="Revise Offer" OnClick="lnkReOffer_Click" />
                                <asp:Button runat="server" ID="lnkRevise" CssClass="btn btn-primary" Text="Revise Offer" OnClick="lnkRevise_Click" />
                                <asp:Button runat="server" ID="lnkApproved" CssClass="btn btn-primary" Text="Approve" OnClick="lnkApproved_Click" OnPreRender="btnOpenFile_PreRender" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="avoidDoubleClick(this);" />
                                <asp:Button runat="server" ID="lnkDisapproved" CssClass="btn btn-primary" Text="Disapprove" OnClick="lnkDisapproved_Click" />
                                <asp:Button runat="server" ID="lnkSubmitForApproval" CssClass="btn btn-primary" Text="Submit for approval" OnClick="lnkSubmitForApproval_Click" UseSubmitBehavior="false" OnClientClick="avoidDoubleClick(this);" />
                                <asp:Button runat="server" ID="lnkSendOffer" OnClick="lnkSendOffer_Click" Text="Send Offer" CssClass="btn btn-primary" OnPreRender="btnOpenFile_PreRender" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="avoidDoubleClick(this);" />
                                <asp:Button runat="server" ID="lnkJobOffer" OnClick="lnkJobOffer_Click" Text="Generate Job Offer" CssClass="btn btn-default" OnPreRender="lnkJobOffer_PreRender" />
                                <asp:Button runat="server" ID="lnkPreview" OnClick="lnkPreview_Click" Text="Preview" CssClass="btn btn-default" />
                                <%--<asp:Button runat="server" ID="lnkMessage" CssClass="btn btn-default" OnClick="lnkMessage_Click" Text="Message" ></asp:Button>--%>
                                <asp:label ID="lblMessageCount" runat="server" />
                                <uc:ConfirmBox runat="server" ID="cfbSendMail" TargetControlID="lnkSendOffer" ConfirmMessage="Are you sure you want to proceed?"  />
                                <uc:ConfirmBox runat="server" ID="cfbSubmitForApproval" TargetControlID="lnkSubmitForApproval" ConfirmMessage="Are you sure you want to submit for approval?"  />
                                <uc:ConfirmBox runat="server" ID="cfbApproved" TargetControlID="lnkApproved" ConfirmMessage="Are you sure you want to approve?"  />
                                <uc:ConfirmBox runat="server" ID="cfbAccept" TargetControlID="lnkAccept" ConfirmMessage="Are you sure you want to accept the job offer?"  />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkSendOffer" />
                            <asp:PostBackTrigger ControlID="lnkApproved" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>    
            
            
    </div>
    <div id="collapseOne" class="panel-collapse collapse">
      <div id="container1" class="container">
          <asp:Repeater runat="server" ID="rRef">        
                <ItemTemplate>   
                   <div class="col-md-12 container-border">
                        <div class="col-md-6">
                            <asp:label ID="lblDisplay1" runat="server" Text='<%# Bind("Display1") %>' />
                        </div>
                        <div class="col-md-6">
                            <asp:label ID="lblDisplay2" runat="server" Text='<%# Bind("Display2") %>' />
                        </div>
                    </div> 
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    
  </div>
  
</div>

<div class="panel panel-default">
<div class="page-content-wrap">
    <div class="col-md-12 bhoechie-tab-container">
        <div class="panel panel-default" style="margin-bottom:0px;">
            <div class="col-md-2 bhoechie-tab-menu">
                <div class="list-group">
                    <%--Tab Pay Offer--%>
                    <asp:LinkButton ID="lnkSalary" OnClick="lnkSalary_Click" runat="server" CssClass="list-group-item active text-left" Text="Pay Offer"></asp:LinkButton>
                    <%--Tab Allowances--%>
                    <asp:LinkButton ID="lnkAllowance" OnClick="lnkAllowance_Click" runat="server" CssClass="list-group-item text-left" Text="Allowances"></asp:LinkButton>
                    <%--Tab Benefits Package--%>
                    <asp:LinkButton ID="lnkBenefits" OnClick="lnkBenefits_Click" runat="server" CssClass="list-group-item text-left" Text="Benefits Package"></asp:LinkButton>
                    <%--Tab Signatory--%>
                    <asp:LinkButton ID="lnkApprover" OnClick="lnkApprover_Click" runat="server" CssClass="list-group-item text-left" Text="Document Signatory"></asp:LinkButton>
                    <%--Salary Analysis--%>
                    <asp:LinkButton ID="lnkAnalysis" OnClick="lnkAnalysis_Click" runat="server" CssClass="list-group-item text-left" Text="Salary Analysis" Visible="false"></asp:LinkButton>
                </div>
            </div>
            <div class="col-md-10 bhoechie-tab" style=" border-left:1px solid #e5e5e5;">
                <br />
                <div class="page-content-wrap" id="divSalary" runat="server">
                    <asp:Panel runat="server" ID="Panel1">
                        <fieldset class="form" id="fsMain">
                            <div  class="form-horizontal">
                                
                                <div class="form-group" style="visibility:hidden;position:absolute;">
                                    <label class="col-md-4 control-label has-space"></label>
                                    <div class="col-md-2">
                                        <asp:Checkbox runat="server" ID="chkIsWithOffer" Text="With pay offer?" />
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-required">
                                    Pay Offer :</label>
                                    <div class="col-md-2">
                                        <asp:TextBox runat="server" ID="txtCurrentSalary" CssClass="form-control required" />
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-required">
                                    Rate Class :</label>
                                    <div class="col-md-6">
                                        <asp:DropdownList ID="cboEmployeeRateClassNo"  runat="server" DataMember="EEmployeeRateClass" CssClass="required form-control">
                                        </asp:DropdownList>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-required">
                                    Contract Template :</label>
                                    <div class="col-md-6">
                                        <asp:DropdownList ID="cboContractTempNo"  runat="server" CssClass="form-control required">
                                        </asp:DropdownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-required">Onboard Date :</label>
                                    <div class="col-md-2">
                                        <asp:TextBox runat="server" ID="txtOnboardDate" CssClass="form-control required" />
                                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtOnboardDate" Format="MM/dd/yyyy" />
                                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtOnboardDate" Mask="99/99/9999" MaskType="Date" />
                                        <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtOnboardDate" Display="Dynamic" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-required">Immediate Head :</label>
                                    <div class="col-md-7">
                                        <asp:TextBox runat="server" ID="txtSuperiorName" CssClass="form-control required" onblur="ResetImmediateSuperior()" style="display:inline-block;" Placeholder="Type here..." />
                                        <asp:HiddenField runat="server" ID="hifImmediateSuperiorNo"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="aceSuperiorName" runat="server"  
                                        TargetControlID="txtSuperiorName" MinimumPrefixLength="2" CompletionSetCount="1"
                                        CompletionInterval="250" ServiceMethod="PopulateManager"
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        OnClientItemSelected="getImmediateSuperior" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                        <script type="text/javascript">

                                            function getImmediateSuperior(source, eventArgs) {
                                                document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = eventArgs.get_value();
                                            }
                                        </script>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space"></label>
                                    <div class="col-md-6">
                                        <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                        <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />
                                    </div>
                                </div>
                            <br />
                            <br />
                        </div>
                    </fieldset>
                </asp:Panel>

                </div>
                <div class="page-content-wrap" id="divAllowance" runat="server" visible="false">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="col-md-6 panel-title">
                                    <asp:label ID="lblAllowanceName" runat="server" />&nbsp;
                                </div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                    <ContentTemplate>
                                        <ul class="panel-controls">
                                            <li>
                                                <asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" />
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" />
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" Visible="false" />
                                            </li>
                                        </ul>
                                        <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkExportDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="MROfferNo">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" Enabled='<%# Bind("IsEnabled") %>' />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="MROfferNo" Caption="" Visible="false" />
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Visible="false" />
                                            <dx:GridViewDataComboBoxColumn FieldName="PayIncomeTypeDesc" Caption="Allowance Type" />
                                            <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            <dx:GridViewDataCheckColumn FieldName="IsPerDay" Caption="Per Day" ReadOnly="true" />
                                            <dx:GridViewDataTextColumn FieldName="PayScheduleDesc" Caption="Payroll Schedule" />
                                            <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                                            <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" Visible="false" />
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                        </Columns>
                                        <SettingsContextMenu Enabled="true">
                                            <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" />
                                        </SettingsContextMenu>
                                        <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />
                                        <SettingsSearchPanel Visible="false" />
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="page-content-wrap" id="divBenefits" runat="server" visible="false">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="col-md-6 panel-title">
                                     <asp:label ID="lblBenefitsName" runat="server" />&nbsp;
                                </div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                    <ContentTemplate>
                                        <ul class="panel-controls">
                                            <li><asp:LinkButton runat="server" ID="lnkAddPac" OnClick="lnkAddPac_Click" Text="Benefits Package" CssClass="control-primary" /></li>
                                            <li><asp:LinkButton runat="server" ID="lnkAddBen" OnClick="lnkAddBen_Click" Text="Add" CssClass="control-primary" /></li>
                                            <li><asp:LinkButton runat="server" ID="lnkDeleteBen" OnClick="lnkDeleteBen_Click" Text="Delete" CssClass="control-primary" /></li>
                                            <li><asp:LinkButton runat="server" ID="lnkExportBen" OnClick="lnkExportBen_Click" Text="Export" CssClass="control-primary" Visible="false" /></li>
                                        </ul>
                                        <uc:ConfirmBox runat="server" ID="cfbDeleteBen" TargetControlID="lnkDeleteBen" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lnkExportBen" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdBen" ClientInstanceName="grdBen" runat="server" SkinID="grdDX" KeyFieldName="MRBenefitPackageNo"
                                    OnCustomCallback="grdBen_CustomCallback" OnCustomColumnSort="grdBen_CustomColumnSort" OnCustomColumnDisplayText="grdBen_CustomColumnDisplayText">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEditBen" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditBen_Click" Enabled='<%# Bind("IsEnabled") %>'/>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="CodeDeti" Caption="Transaction No." Visible="false" />
                                            <dx:GridViewDataTextColumn FieldName="BenefitPackageTypeDesc" Caption="&nbsp;" GroupIndex="0" >
                                                <Settings SortMode="Custom" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="MRBenefitPackageNo" Caption="" Visible="false"/>
                                            <dx:GridViewDataTextColumn FieldName="MRBenefitPackageDesc" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Amount/Coverage" PropertiesTextEdit-EncodeHtml="false" />
                                            <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order" />
                                            <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                                            <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded"  Visible="false" />
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                        </Columns>
                                        <Styles>
                                            <GroupRow Font-Bold="true">
                                            </GroupRow>
                                        </Styles>
                                        <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7">
                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                                        </SettingsPager>
                                        <SettingsContextMenu Enabled="true">
                                            <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" />
                                        </SettingsContextMenu>
                                        <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="True" />
                                        <SettingsSearchPanel Visible="false" />
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExportBen" runat="server" GridViewID="grdBen" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="page-content-wrap" id="divApprover" runat="server" visible="false">
                    <div class="row">

                            <div class="panel-body">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdApprover" ClientInstanceName="grdApprover" SkinID="grdDX" runat="server" KeyFieldName="RecordNo">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Display" Caption="Signatory Name" PropertiesTextEdit-EncodeHtml="false" HeaderStyle-HorizontalAlign="Center" CellStyle-Paddings-Padding="10px" HeaderStyle-Font-Bold="true" Width="30%" CellStyle-HorizontalAlign="Center" />
                                            <dx:GridViewDataTextColumn FieldName="ApproveDate" Caption="Date Approved" PropertiesTextEdit-EncodeHtml="false" HeaderStyle-HorizontalAlign="Center" CellStyle-Paddings-Padding="10px" HeaderStyle-Font-Bold="true" Width="10%" CellStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <Styles>
                                            <Header BackColor="#F1F5F9" Font-Size="Small" />
                                            <FocusedRow BackColor="Transparent" ForeColor="Black"></FocusedRow>
                                        </Styles> 
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                    </div>
                </div>

                
                <div class="page-content-wrap" id="divAnalysis" runat="server" visible="false">
                    <asp:Panel runat="server" ID="Panel2">
                         <fieldset class="form" id="Fieldset5">
                              <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">
                                        Previous Salary :</label>
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtSalaryPrevious" CssClass="form-control" ReadOnly="true" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">
                                        Expected Salary :</label>
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtSalaryExpected" CssClass="form-control" ReadOnly="true" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">
                                        Reason for hiring :</label>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtReasonforhiring" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space"></label>
                                        <div class="col-md-6">
                                            <%--<asp:Button runat="server"  ID="lnkSaveAnalysis" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSaveAnalysis_Click" />
                                            <asp:Button runat="server"  ID="lnkModifyAnalysis" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModifyAnalysis_Click" />--%>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                </div>
                         </fieldset>
                    </asp:Panel>
                </div>

                <br />
                <br />
            </div>
        </div>
    </div>
</div>
</div>


<!-- Add Income -->
<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="fsDetl">
        <div class="cf popupheader">
            <h4>
                &nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />
            &nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="btnSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveDetl" ToolTip="Save" />
        </div>
        <div  class="entryPopupDetl2 form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">
                Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtMROfferNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">
                Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">
                Income Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPayIncomeTypeNo"  runat="server" DataMember='EPayIncomeType' CssClass="required form-control">
                    </asp:DropdownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">
                Amount :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="required form-control"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtAmount" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">
                </label>
                <div class="col-md-8">
                    <asp:CheckBox ID="txtIsPerDay" runat="server" Text="&nbsp; Amount is per day" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">
                Payroll Schedule :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayScheduleNo" runat="server" CssClass="required form-control" DataMember="EPaySchedule">
                    </asp:DropDownList>
                </div>
            </div>
            <br />
        </div>
        <div class="cf popupfooter">
        </div>
    </fieldset>
</asp:Panel>

<asp:Button ID="btnShowBen" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShowBen" runat="server" TargetControlID="btnShowBen" PopupControlID="pnlPopupBen" CancelControlID="imgClosed" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupBen" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsBen">
        <!-- Header here -->
        <div class="cf popupheader">
            <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />
            &nbsp;
            <asp:LinkButton runat="server" ID="btnSaveBen" CssClass="fa fa-floppy-o submit fsBen btnSaveBen" OnClick="btnSaveBen_Click"  />
        </div>
        <!-- Body here -->
        <div  class="entryPopupDetl form-horizontal">
            <br />
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">
                Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMRBenefitPackageNo" runat="server" CssClass="form-control" 
                    ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">
                Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDeti" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">
                Benefit Package Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboBenefitPackageTypeNo" runat="server" DataMember="EBenefitPackageType" CssClass="form-control"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">
                Benefit Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtMRBenefitPackageDesc" runat="server" CssClass="required form-control" TextMode="MultiLine" Rows="3" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">
                Amount/Coverage :</label>
                <div class="col-md-7">
                    <dx:ASPxHtmlEditor ID="txtRemarks" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">
                Order :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOrderLevel" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <br />
        </div>
        <!-- Footer here -->
    </fieldset>
</asp:Panel>


<asp:Button ID="btnShowPac" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShowPac" runat="server" TargetControlID="btnShowPac" PopupControlID="pnlPopupPac" CancelControlID="imgClosedPac" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupPac" runat="server" CssClass="entryPopup3" style="display:none">
    <fieldset class="form" id="fsPac">
        <!-- Header here -->
        <div class="cf popupheader">
            <asp:Linkbutton runat="server" ID="imgClosedPac" CssClass="cancel fa fa-times" ToolTip="Close" />
            &nbsp;
            <asp:LinkButton runat="server" ID="btnSavePac" CssClass="fa fa-floppy-o submit fsPac btnSavePac" OnClick="btnSavePac_Click"  />
        </div>
        <!-- Body here -->
        <div  class="entryPopupDetl3 form-horizontal">
            <br />
            <div class="form-group">
                <label class="col-md-4 control-label has-required">
                Benefits Package :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboBenefitPackageNo" runat="server" DataMember="EBenefitPackage" CssClass="form-control required"  />
                </div>
            </div>
            
            <br />
        </div>
        <!-- Footer here -->
    </fieldset>
</asp:Panel>


 <asp:Button ID="btnRemarkDisAppr" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlRemarkDisAppr" runat="server" TargetControlID="btnRemarkDisAppr" PopupControlID="pnlPopupRemarkDisAppr"
    CancelControlID="lnkCloseDisappr" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupRemarkDisAppr" runat="server" CssClass="entryPopup3" style=" display:none;">
    <fieldset class="form" id="Fieldset2">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="lnkCloseDisappr" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveDisappr" CssClass="fa fa-floppy-o submit fsMain lnkSaveDisappr" OnClick="lnkSave_ClickDisAppr"  /> 
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl3 form-horizontal">
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Remarks :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRemarkDisAppr" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" ></asp:Textbox>
                </div>
            </div>
  
         </div>
          <!-- Footer here -->
         <br />           
    </fieldset>
</asp:Panel>  

<asp:Button ID="btnRemarkDecline" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlRemarkDecline" runat="server" TargetControlID="btnRemarkDecline" PopupControlID="pnlPopupRemarkDecline"
    CancelControlID="lnkCloseDecline" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupRemarkDecline" runat="server" CssClass="entryPopup3" style=" display:none;">
    <fieldset class="form" id="Fieldset3">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="lnkCloseDecline" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveDecline" CssClass="fa fa-floppy-o submit fsMain lnkSaveDecline" OnClick="lnkSave_ClickDecline"  /> 
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl3 form-horizontal">
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Reason :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRemarkDecline" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" ></asp:Textbox>
                </div>
            </div>
  
         </div>
          <!-- Footer here -->
         <br />           
    </fieldset>
</asp:Panel>  

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="pnpPopUpAttachment" CancelControlID="lnkCloseAttachment" BackgroundCssClass="modalBackground" />
<asp:Panel id="pnpPopUpAttachment" runat="server" CssClass="entryPopup3" style="display:none">
    <fieldset class="form" id="Fieldset1">
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseAttachment" CssClass="fa fa-times" ToolTip="Close" />&nbsp;
                
        </div>
        <div class="entryPopupDetl3 form-horizontal">
            <div class="row">
                <dx:ASPxGridView ID="grdReport" ClientInstanceName="grdReport" runat="server" width="100%" KeyFieldName="ReportNo">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="xReportTitle" Caption="Title" />
                        <dx:GridViewDataColumn Caption="Preview" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                            <DataItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" OnClick="lnkPrint_Click" Font-Size="Medium" OnPreRender="lnkPrint_PreRender"/>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
    </fieldset>
</asp:Panel>

<asp:Button ID="btnRemarkReOffer" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlRemarkReOffer" runat="server" TargetControlID="btnRemarkReOffer" PopupControlID="pnlPopupRemarkReOffer"
    CancelControlID="lnkCloseReOffer" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupRemarkReOffer" runat="server" CssClass="entryPopup3" style=" display:none;">
    <fieldset class="form" id="Fieldset4">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="lnkCloseReOffer" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveReOffer" CssClass="fa fa-floppy-o submit fsMain lnkSaveReOffer" OnClick="lnkSave_ClickReOffer"  /> 
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl3 form-horizontal">
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Remarks :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRemarkReOffer" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" ></asp:Textbox>
                </div>
            </div>
  
         </div>
          <!-- Footer here -->
         <br />           
    </fieldset>
</asp:Panel>  


<%--<div class="row">
    <dx:ASPxRichEdit ID="DemoRichEdit" runat="server" ReadOnly="true" RibbonMode="None" ShowStatusBar="false" Visible="false"
	Settings-HorizontalRuler-Visibility="Hidden" ShowConfirmOnLosingChanges="false" Width="100%" />
    <br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowPrintButton="true" Visible="false" />
</div>--%>

        