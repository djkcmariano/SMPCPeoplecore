<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayMainDeductForwList_Backup2.aspx.vb" Inherits="Secured_PayMainDeductForwList" %>


<asp:Content id="Content2" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }
</script>

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" style="width:150px;" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>

                    <div >
                        <label class="col-md-1 control-label has-space" style=" text-align:right; vertical-align:middle;"></label>
                        <div class="col-md-3" style=" vertical-align:middle;">
                            <asp:Dropdownlist ID="cboPayMainDeductForwIdNo" runat="server" CssClass="form-control" CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="cboPayMainDeductForwIdNo_OnSelectedIndexChanged"></asp:Dropdownlist>
                        </div>
                    </div>

                    <div class="col-md-2" style=" padding-left:0px;">
                        <ul class="panel-controls" style=" text-align:left; float:left;">
                            <li><asp:LinkButton runat="server" ID="lnkDeleteBatckFile" OnClick="lnkDeleteBatckFile_Click" Text="Delete Batch File" CssClass="control-primary" /></li>
                            <uc:ConfirmBox runat="server" ID="cfbDeleteBatckFile" TargetControlID="lnkDeleteBatckFile" ConfirmMessage="Are you sure you want to delete selected Uploaded Batch File?"  />
                        </ul> 
                    </div>

                    <div>          
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                        <ContentTemplate>
                            <uc:Filter runat="server" ID="Filter1" EnableContent="true" Visible="true">
                            <Content>
        <%--                            <div class="form-group">
                                    <label class="col-md-4 control-label">Filter By :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control" />
                                        </div>
                                        <ajaxToolkit:CascadingDropDown ID="cdlfilterby" TargetControlID="cbofilterby" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterBy" runat="server" Category="tNo" LoadingText="Loading..." />
                                    </div>
		                            <div class="form-group">
                                    <label class="col-md-4 control-label">Filter Value :</label> 
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control" />
                                    </div>
                                    <ajaxToolkit:CascadingDropDown ID="cdlfiltervalue" TargetControlID="cbofiltervalue" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterValue" runat="server" Category="tNo" ParentControlID="cbofilterby" LoadingText="Loading..." PromptText="-- Select --" />
                                    </div>--%>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Filter By :</label>
                                    <div class="col-md-8">
                                        <asp:Dropdownlist ID="cboFilteredbyNo" AutoPostBack="true"  runat="server" CssClass="form-control" 
                                            OnSelectedIndexChanged="cboFilteredbyNo_SelectedIndexChanged" ></asp:Dropdownlist>
                                    </div>
                                </div>

                                <div class="form-group" >
                                    <label class="col-md-4 control-label has-space">Filter Value :</label>
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                                        <asp:HiddenField runat="server" ID="hiffilterbyid"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="drpAC" runat="server"  
                                        TargetControlID="txtName" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="populateDataDropdown" CompletionSetCount="0"
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="getRecordx" FirstRowSelected="true" UseContextKey="true" />
                                         <script type="text/javascript">
                                             function getRecordx(source, eventArgs) {
                                                 document.getElementById('<%= hiffilterbyid.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                    
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Applicable Month :</label>
                                    <div class="col-md-8">
                                        <asp:Dropdownlist ID="cboApplicableMonth3" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                                </div>

                                <div class="form-group" >
                                    <label class="col-md-4 control-label has-space">Applicable Year :</label>
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtApplicableYear3" CssClass="form-control number" style="display:inline-block;" Placeholder="Applicable Year..." /> 
                                    </div>
                                </div>

                                <div class="form-group" >
                                    <label class="col-md-4 control-label has-space">Date From :</label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" CausesValidation="false" Placeholder="Date From"
                                            ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                            TargetControlID="txtDateFrom"
                                            Format="MM/dd/yyyy" />  
                                                                          
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                            TargetControlID="txtDateFrom"
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
                                            ControlToValidate="txtDateFrom"
                                            ErrorMessage="<b>Please enter valid entry</b>"
                                            MinimumValue="1900-01-01"
                                            MaximumValue="3000-12-31"
                                            Type="Date" Display="None"  />
                                                                        
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                                            ID="ValidatorCalloutExtender3"
                                            TargetControlID="RangeValidator4"
                                            /> 
                                    </div>
                                </div>

                                <div class="form-group" >
                                    <label class="col-md-4 control-label has-space">Date To :</label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" CausesValidation="false" Placeholder="Date To"
                                            ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                            TargetControlID="txtDateTo"
                                            Format="MM/dd/yyyy" />  
                                                                          
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                            TargetControlID="txtDateTo"
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
                                            ID="RangeValidator5"
                                            runat="server"
                                            ControlToValidate="txtDateTo"
                                            ErrorMessage="<b>Please enter valid entry</b>"
                                            MinimumValue="1900-01-01"
                                            MaximumValue="3000-12-31"
                                            Type="Date" Display="None"  />
                                                                        
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                                            ID="ValidatorCalloutExtender4"
                                            TargetControlID="RangeValidator5"
                                            /> 
                                    </div>
                                </div>

                                <div class="form-group" >
                                    <label class="col-md-4 control-label has-space">Deduction Type :</label>
                                    <div class="col-md-8">
                                        <asp:Dropdownlist ID="cboPayDeductTypeNo3" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                                </div>
<%--                                <div class="form-group" >
                                    <label class="col-md-4 control-label has-space">File Batch No. :</label>
                                    <div class="col-md-8">
                                        <asp:Dropdownlist ID="cboPayMainDeductForwIdNo" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                    </div>
                                </div>--%>

                            </Content>
                            </uc:Filter>
                                                                                                                                                                    
                        </ContentTemplate>
                        </asp:UpdatePanel> 


                    </div>        


                    
                </div>

<%--                <div class="panel-heading">

                </div>--%>

                <div class="panel-heading">

                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" Visible="true" /></li> 
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

                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayMainDeductForwNo"
                           OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                    <dx:GridViewDataTextColumn FieldName="FileName" Caption="File Name" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataTextColumn FieldName="PayDeductTypeDesc" Caption="Deduction Type" />
                                    <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="Balance" Caption="Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" />

                                    <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" />
                                    <dx:GridViewDataComboBoxColumn FieldName="MonthDesc" Caption="Month" />
                                    <dx:GridViewDataTextColumn FieldName="StartDate" Caption="Start Date" />
                                    <dx:GridViewDataTextColumn FieldName="EndDate" Caption="End Date" />

                                    <dx:GridViewDataCheckColumn FieldName="IsSpecialPay" Caption="Special<br />Pay?"  HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="true"/>
                                    <dx:GridViewDataCheckColumn FieldName="IsBonus" Caption="Bonus?"  HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="true"/>
                                    <dx:GridViewDataCheckColumn FieldName="IsForwarded" Caption="Forwarded?"  HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="true"/>
                                    <dx:GridViewDataCheckColumn FieldName="IsSuspended" Caption="Suspended?"  HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="true"/>

                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Payment" Visible="false">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkPayment" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkPayment_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                    <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init">
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

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlMain" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlPopupMain" TargetControlID="btnShowMain">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
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
                    <asp:TextBox ID="txtPayMainDeductForwNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
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
                <label class="col-md-4 control-label  has-required">Deduction Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayDeductTypeNo" runat="server" CssClass="required form-control" >
                    </asp:DropDownList>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDescription" runat="server" Rows="2" TextMode="MultiLine" CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Amount :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="required form-control">
                    </asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount" FilterType="Numbers, Custom" ValidChars="-." /> 
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Applicable Month :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control" Enabled="true" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Applicable Year :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control number" Enabled="true" />
                </div>                
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Effective Date :</label>
                <div class="col-md-3">                    
                    <asp:Textbox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="Start Date" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator3" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate" />                    
                </div>
                <div class="col-md-3">                    
                    <asp:Textbox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="End Date" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator4" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate" />                    
                </div>
            </div>

            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label has-space">Pay Schedule :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPayScheduleNo" runat="server" CssClass="form-control" />
                </div>                
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">                    
                    <asp:Checkbox ID="chkIsSpecialPay" runat="server" Text="&nbsp;Tick here if deduction is for Special Payroll." />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">                    
                    <asp:Checkbox ID="chkIsBonus" runat="server" Text="&nbsp;Tick here if deduction is deducted in Bonus." />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">                    
                    <asp:Checkbox ID="chkIsForwarded" runat="server" Text="&nbsp;Tick here if deduction is forwarded (continuous deduction)." />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">                    
                    <asp:Checkbox ID="chkIsSuspended" runat="server" Text="&nbsp;Tick here to suspend deduction." />
                </div>
            </div>

            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>


<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose2" PopupControlID="Panel3" TargetControlID="Button1" />
<asp:Panel id="Panel3" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsUpload">      
         <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>
                    <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    <asp:LinkButton runat="server" ID="lnkSave2" CssClass="fa fa-floppy-o submit fsUpload lnkSave2" OnClick="lnkSave2_Click"  />   
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkSave2" />
                </Triggers>
            </asp:UpdatePanel>            
         </div>         
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-9 control-label has-space"><code>File must be .csv with following column : Employee No., Description, Amount</code></label>                
                <br />
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">File Name :</label>
                <div class="col-md-7">
                    <asp:FileUpload runat="server" ID="fuFilename" Width="100%" CssClass="required" />                   
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Deduct Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayDeductTypeNo2" runat="server" CssClass="required form-control" />                    
                </div>
            </div> 
           <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDescription2" runat="server" Rows="4" textmode="MultiLine" CssClass="form-control" />
                </div>
            </div>   
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Applicable Month :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboApplicableMonth2" DataMember="EMonth" runat="server" CssClass="form-control" Enabled="true" />
                </div>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Applicable Year :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtApplicableYear2" runat="server" CssClass="form-control number" Enabled="true" />
                </div>                
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Effective Date :</label>
                <div class="col-md-3">                    
                    <asp:Textbox ID="txtStartDate2" runat="server" CssClass="form-control" placeholder="Start Date" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate2" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDate2" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate2" />                    
                </div>
                <div class="col-md-3">                    
                    <asp:Textbox ID="txtEndDate2" runat="server" CssClass="form-control" placeholder="End Date" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate2" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDate2" />
                    <asp:CompareValidator runat="server" ID="CompareValidator2" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate2" />                    
                </div>
            </div>

            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label has-space">Pay Schedule :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPaySchedule2No" runat="server" CssClass="form-control" />
                </div>                
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">                    
                    <asp:Checkbox ID="chkIsSpecialPay2" runat="server" Text="&nbsp;Tick here if deduction is for Special Payroll." />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">                    
                    <asp:Checkbox ID="chkIsBonus2" runat="server" Text="&nbsp;Tick here if deduction is deducted in Bonus." />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">                    
                    <asp:Checkbox ID="chkIsForwarded2" runat="server" Text="&nbsp;Tick here if deduction is forwarded (continuous deduction)." />
                </div>
            </div>
                     
            <br /> 
        </div>         
        <div class="cf popupfooter">
        </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowRate" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShowRate" runat="server" TargetControlID="btnShowRate" PopupControlID="pnlPopupShowRate" CancelControlID="imgCloseRate" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupShowRate" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="Fieldset2">
            <!-- Header here -->
             <div class="cf popupheader">
                    <h4>Payment Details</h4>
                    <asp:Linkbutton runat="server" ID="imgCloseRate" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;     
             </div>
             <!-- Body here -->
             <div  class="container-fluid entryPopupDetl">                  
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdRate" ClientInstanceName="grdRate" runat="server" KeyFieldName="PayMainDeductForwPaymentNo" Width="100%">
                        <Columns>                                
                            <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." />
                            <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Pay Date" />
                            <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />                                                                                                      
                        </Columns> 
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
                        </TotalSummary>                     
                    </dx:ASPxGridView>                           
                </div>
            </div>
        </fieldset>
</asp:Panel>


</asp:Content>

