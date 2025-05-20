<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfBenefitApplication.aspx.vb" Inherits="SecuredSelf_SelfBenefitApplication" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap">         
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitApplicationNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BenefitApplicationNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataComboBoxColumn FieldName="BenefitTypeDesc" Caption="Benefit Type" />                                                                  
                                <dx:GridViewDataTextColumn FieldName="xDateFiled" Caption="Date Filed" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Attachment" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkAttachment" CssClass="fa fa-paperclip" Font-Size="Medium" CommandArgument='<%# Bind("BenefitApplicationNo") %>' Visible='<%# Bind("IsUploadDoc") %>' OnClick="lnkAttachment_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Dependent/s" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDependents" CssClass="fa fa-child" Font-Size="Medium" CommandArgument='<%# Bind("BenefitApplicationNo") %>' Visible='<%# Bind("IsDepe") %>' OnClick="lnkBenefitHMO_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
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

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Employee Name :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee_SubordinateAppr" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem"  CompletionSetCount="1"
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
                <label class="col-md-4 control-label has-space">Benefit Status :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboBenefitStatNo" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Date Filed :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDateFiled" runat="server" CssClass="form-control" SkinID="txtdate" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateFiled" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateFiled" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateFiled" Display="Dynamic" />
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Benefit Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboBenefitTypeNo" DataMember="EBenefitType" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboBenefitTypeNo_SelectedIndexChanged" />
                </div>
            </div>
            <asp:PlaceHolder runat="server" ID="phamount" Visible="false">
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Amount :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" />
                </div>
            </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="phloan" Visible="false">
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">No. of Payments :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" SkinID="txtdate" ID="txtNoOfpayments" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">Deduction start :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtDeductionStart" runat="server" SkinID="txtdate" cssclass="form-control"  
                        ></asp:TextBox> 

                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                            TargetControlID="txtDeductionStart"
                            Format="MM/dd/yyyy" />  
                                                                          
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                            TargetControlID="txtDeductionStart"
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
                            ControlToValidate="txtDeductionStart"
                            ErrorMessage="<b>Please enter valid entry</b>"
                            MinimumValue="1900-01-01"
                            MaximumValue="3000-12-31"
                            Type="Date" Display="Dynamic"  />  
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Payroll Schedule :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayScheduleNo" DataMember="EPaySchedule" runat="server" CssClass="required form-control" 
                    ></asp:Dropdownlist>
                </div>
            </div> 
            </asp:PlaceHolder>
            
            <asp:PlaceHolder runat="server" ID="PlaceHolder2" Visible="false">
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Deduction Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboPayDeductTypeNo" CssClass="form-control" />
                </div>
            </div>
            </asp:PlaceHolder>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                </div>
            </div>
            <%-- RETIREMENT PLAN ENROLLMENT FORM  --%>                       
            <asp:PlaceHolder runat="server" ID="ph1" Visible="false">
            <asp:HiddenField runat="server" ID="hifBenefitRetirePlanNo" />
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Type of Advice :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboAdviceTypeNo" CssClass="form-control">
                        <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                        <asp:ListItem Value="1" Text="New Authorization" />
                        <asp:ListItem Value="2" Text="Change / Update Authorization" />
                        <asp:ListItem Value="3" Text="Terminate Authorization" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Amount of Contribution :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboContriAmount" CssClass="form-control">
                        <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                        <asp:ListItem Value="0.01" Text="1%" />
                        <asp:ListItem Value="0.02" Text="2%" />
                        <asp:ListItem Value="0.03" Text="3%" />
                        <asp:ListItem Value="0.04" Text="4%" />
                        <asp:ListItem Value="0.05" Text="5%" />
                        <asp:ListItem Value="0.06" Text="6%" />
                        <asp:ListItem Value="0.07" Text="7%" />
                        <asp:ListItem Value="0.08" Text="8%" />
                        <asp:ListItem Value="0.09" Text="9%" />
                        <asp:ListItem Value="0.10" Text="10%" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">      
                <label class="col-md-4 control-label has-space">&nbsp;</label>          
                <div class="col-md-7">
                    <p style="text-align:justify">                    
                        I hereby authorize Golden Arches Development Corp. to deduct the amount specified above from my salary which will serve as my voluntary contribution under the terms and condition of the Retirement Plan.<br /><br />
                        I understand that I have the right to change the amount of deduction or to terminate this authorization within the limits specified in the Plan. I also understand that I cannot withdraw my contributions and or/ investment earnings on these contributions before separation from the Company.
                    </p>
                </div>
            </div>            
            </asp:PlaceHolder>
            <%-- RETIREMENT PLAN ENROLLMENT FORM  --%>

            <%-- HMO ENROLLMENT FORM  --%>                       
            <asp:PlaceHolder runat="server" ID="ph3" Visible="false">
            <asp:HiddenField runat="server" ID="hifBenefitApplicationHMONo" />
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Benefit PLAN :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboBenefitHMOPlanTypeNo" CssClass="form-control"
                     AutoPostBack="true" OnSelectedIndexChanged="cboBenefitHMOPlanTypeNo_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Additional Premium Cost :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" SkinID="txtdate" ID="txtPrincipalCost" CssClass="form-control" />
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label has-space">Percent Cost :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" SkinID="txtdate" ID="txtPercentCost" CssClass="form-control" />
                </div>
            </div> 
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Dependent/s Cost :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" SkinID="txtdate" ID="txtDependentsCost" CssClass="form-control" Enabled="false" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Total Cost :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" SkinID="txtdate" ID="txtTotalCost" CssClass="form-control" Enabled="false" />
                </div>
            </div>     
                  
            </asp:PlaceHolder>
            <%-- END OF HMO PLAN ENROLLMENT FORM  --%>
            <%-- CAR ENROLLMENT FORM  --%>                       
            <asp:PlaceHolder runat="server" ID="ph4" Visible="false">
            <asp:HiddenField runat="server" ID="hifBenefitApplicationFleetNo" />
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Name of Vehicle :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboFleetNo" DataMember="Efleet" CssClass="form-control"
                    >
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Amount :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" SkinID="txtdate" ID="txtAmountF" CssClass="form-control" />
                </div>
            </div>
          
            
               
                  
            </asp:PlaceHolder>
            <%-- END OF CAR PLAN ENROLLMENT FORM  --%>

            <%-- Allowance ENROLLMENT FORM  --%>                       
            <asp:PlaceHolder runat="server" ID="phallowance" Visible="false">
            <asp:HiddenField runat="server" ID="hifbenefitapplicationallowanceno" />
            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Amount :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtAmountD"   runat="server" SkinID="txtdate" CssClass="required form-control"  
                    ></asp:TextBox>   
                    <ajaxToolkit:FilteredTextBoxExtender
                    ID="FilteredTextBoxExtender2"
                    runat="server"
                    TargetControlID="txtAmountD"
                    FilterType="Numbers, Custom" ValidChars="." /> 
                </div>
            </div> 
           <div class="form-group">
                <label class="col-md-4 control-label">Start Date :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtstartdate" runat="server" SkinID="txtdate" cssclass="form-control"  
                    ></asp:TextBox> 

                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2x" runat="server"
                        TargetControlID="txtStartDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2x" runat="server"
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
                <label class="col-md-4 control-label">End Date :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtEndDate" runat="server" SkinID="txtdate" cssclass="form-control" 
                    ></asp:TextBox> 

                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                        TargetControlID="txtEndDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
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
                        ID="RangeValidator1"
                        runat="server"
                        ControlToValidate="txtEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="Dynamic"  />  
                </div>
            </div> 
               
                  
            </asp:PlaceHolder>
            <%-- END OF Allowance ENROLLMENT FORM  --%>

            <br />                                    
        </div>                    
    </fieldset>
</asp:Panel>
</asp:Content>