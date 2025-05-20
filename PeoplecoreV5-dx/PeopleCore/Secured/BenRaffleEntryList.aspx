<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="BenRaffleEntryList.aspx.vb" Inherits="Secured_BenRaffleEntryList" EnableEventValidation="false" %>



<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

    <%-- <script type="text/javascript">
         function cbCheckAll_CheckedChanged(s, e) {
             grdDetl.PerformCallback(s.GetChecked().toString());
         }

    </script>--%>

<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>                                                
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Are you sure you want to post transaction?" MessageType="Post"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXWOSearch" KeyFieldName="RaffleEntryNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="RaffleEntryCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="RaffleEntryDesc" Caption="Description" />
                                <dx:GridViewDataComboBoxColumn FieldName="RaffleEntryTypeDesc" Caption="Type of Entry" />
                                <dx:GridViewDataDateColumn FieldName="RaffleDate" Caption="Active<br/>Employees<br />as of" Width="5%" />
                                <dx:GridViewDataDateColumn FieldName="EncodeDate" Caption="Date<br />Encoded" Width="5%" />
                                <%--<dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />--%>
                                <%--<dx:GridViewDataCheckColumn FieldName="IsSuspended" Caption="Exclude from Draw" />--%>
   
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Bank<br/>Reference" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <%--<asp:LinkButton runat="server" ID="lnkBankAdvice" CssClass="fa fa-pencil" Font-Size="Medium" Visible='<%# Bind("IsVisible") %>' OnClick="lnkBankAdviceEdit_Click" />--%>
                                        <asp:LinkButton runat="server" ID="lnkBankAdvice" Visible='<%# Bind("IsVisible") %>' Font-Bold="false" ForeColor="#2222CC" Font-Underline="true" Font-Size="14px" Text="Edit" OnClick="lnkBankAdviceEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataDateColumn FieldName="EffectiveDate" Caption="Credit Date" Width="5%" />

                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" FieldName="PayDate1" Caption="Bank Advice" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkCreateDisk" CssClass="fa label" CommandArgument='<%# Bind("RaffleEntryNo") %>' Text='Generate' Visible='<%# Bind("IsVisible") %>' Font-Bold="false" ForeColor="#2222CC" Font-Underline="true" Font-Size="14px" OnClick="lnkCreateDisk_Click" OnPreRender="lnkPrint_PreRender" CausesValidation="false" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>  
                                                                                   
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
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
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAppend" OnClick="lnkAppend_Click" Text="Add" CssClass="control-primary" /></li>                                                   
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExportDetl" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>                                                                                                                                                     
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" SkinID="grdDX" runat="server" KeyFieldName="RaffleEntryDetiNo" Width="100%">
                            <%--OnCommandButtonInitialize="grdDetl_CommandButtonInitialize" OnCustomCallback="gridDetl_CustomCallback" >--%>
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" Enabled='<%# Bind("IsEnabled") %>' />
                                </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />                                                              
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                                <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />
                                <dx:GridViewDataTextColumn FieldName="BankAccountNo" Caption="Bank Account No." />
                                <dx:GridViewDataTextColumn FieldName="AmountTax" Caption="Gross Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Tax" Caption="Tax" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Amount" Caption="Net Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataDateColumn FieldName="HiredDate" Caption="Hired Date" Visible="false" />
                                <dx:GridViewDataDateColumn FieldName="SeparatedDate" Caption="Separated Date" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="TINNo" Caption="TIN" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%" SelectAllCheckboxMode="AllPages">
					                <%--<HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server">
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>--%>
				                </dx:GridViewCommandColumn>  

                            </Columns>                     
                        </dx:ASPxGridView>     
                        <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />                            
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRaffleEntryNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRaffleEntryCode" runat="server" CssClass="form-control required"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRaffleEntryDesc" runat="server" CssClass="form-control required"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Type of Entry :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboRaffleEntryTypeNo" DataMember="ERaffleEntryType" runat="server" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label  has-required">Income Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboPayIncomeTypeNo" runat="server" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Active Employees as of :</label>
                <div class="col-md-3">
                        <asp:TextBox ID="txtRaffleDate" runat="server" CssClass="required form-control" ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                            Format="MM/dd/yyyy" TargetControlID="txtRaffleDate" />
                        <asp:RangeValidator ID="RangeValidator3" runat="server" 
                            ControlToValidate="txtRaffleDate" Display="None" 
                            ErrorMessage="&lt;b&gt;Please enter valid entry&lt;/b&gt;" 
                            MaximumValue="3000-12-31" MinimumValue="1900-01-01" Type="Date" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                            AcceptNegative="Left" ClearTextOnInvalid="true" DisplayMoney="Left" 
                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" 
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtRaffleDate" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" 
                            runat="Server" TargetControlID="RangeValidator3" />
                   </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-6"> 
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:Textbox>
                </div>
            </div>

            <%--<div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-6">
                    <asp:Checkbox ID="txtIsSuspended" Text="&nbsp; Forward to Forwarded Income upon posting." runat="server"></asp:Checkbox>
                 </div>
            </div>--%>
       
        </div>

        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


<asp:Button ID="btnShowAppend" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlAppend" runat="server" TargetControlID="btnShowAppend" PopupControlID="pnlPopupAppend" CancelControlID="lnkCloseAppend" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupAppend" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="Fieldset1">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseAppend" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveAppend" OnClick="lnkSaveAppend_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveAppend" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <%--<div class="form-group">
                <label class="col-md-4 control-label has-space">Criteria :</label>
                <div class="col-md-6">
                    <dx:ASPxFilterControl ID="ASPxFilterControl1" runat="server" Styles-GroupType-CssClass="ontop" />                
                </div>
            </div>--%>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Filter By :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboFilteredbyNo" DataMember="EFilteredByAll" AutoPostBack="true"  runat="server" CssClass="form-control" 
                        OnSelectedIndexChanged="cboFilteredbyNo_SelectedIndexChanged" ></asp:Dropdownlist>
                        <%--<asp:Dropdownlist ID="cboFilteredbyNo" DataMember="EFilteredByAll" AutoPostBack="true"  runat="server" CssClass="form-control" ></asp:Dropdownlist>--%>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Filter Value :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hiffilterbyid"/>
                    <ajaxToolkit:AutoCompleteExtender ID="drpAC" runat="server"  
                    TargetControlID="txtName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="populateDataDropdown" CompletionSetCount="0"
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" />
                     <script type="text/javascript">
                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hiffilterbyid.ClientID %>').value = eventArgs.get_value();
                         }
                     </script>
                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-6">                    
                    <asp:Textbox ID="txtAmount" runat="server" CssClass="form-control required"></asp:Textbox>
                </div>
            </div>
            <br /><br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkCloseAppend" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="Fieldset2">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave2" OnClick="lnkSave2_Click" CssClass="fa fa-floppy-o submit Fieldset2 lnkSave2" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">                             
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Employee Name :</label>
                <div class="col-md-6">
                    <asp:HiddenField runat="server" ID="hifRaffleEntryDetiNo" />
                    <asp:TextBox runat="server" ID="txtFullname" CssClass="form-control" Enabled="false" ReadOnly="true"  />
                </div>
            </div>           
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-6">                    
                    <asp:Textbox ID="txtAmount2" runat="server" CssClass="form-control required"></asp:Textbox>
                </div>
            </div>
            <br /><br />
        </div>
        <div class="cf popupfooter">
        </div> 
    </fieldset>
</asp:Panel>


<%-- BANK ADVICE REFERENCE --%>

<asp:Button ID="btnBankAdvice" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlBankAdvice" runat="server" TargetControlID="btnBankAdvice" PopupControlID="pnlPopupBankAdvice"
    CancelControlID="imgCloseBA" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupBankAdvice" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="Fieldset3">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgCloseBA" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveBankAdvice" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSaveBankAdvice_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRaffleEntryNo2" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode2" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Name of Company :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPayLocNo" runat="server" CssClass="form-control"  ></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Bank Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBankTypeNo" DataMember="EBankType" runat="server" CssClass="form-control" Enabled="false"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" style=' display:none;'>
                <label class="col-md-4 control-label has-space">Bank Account No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtAccountNumber" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Bank Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBankCode" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Company Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCompanyCode" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Branch Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBranchCode_Company" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Branch Account :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBranchCode_PayrollAccount" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>         

            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">FV1</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">FV2</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">FV3</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">FV4</label><br />
                    </center>
                </div>
            </div>

            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label has-space">MBTC only :</label>
                <div class="col-md-2">
                    <center>
                        <asp:Textbox ID="txtFV1" SkinID="txtdate" CssClass="number form-control" runat="server" ></asp:Textbox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:Textbox ID="txtFV2" SkinID="txtdate" CssClass="number form-control" runat="server" ></asp:Textbox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:Textbox ID="txtFV3" SkinID="txtdate" CssClass="number form-control" runat="server" ></asp:Textbox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:Textbox ID="txtFV4" SkinID="txtdate" CssClass="number form-control" runat="server" ></asp:Textbox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Batch No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBatchNo" SkinID="txtdate" CssClass="number form-control required" runat="server" >
                    </asp:Textbox>
                </div>
            </div>

           <div class="form-group">
                <label class="col-md-4 control-label has-required">Pay Date :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEffectiveDate" runat="server" SkinID="txtdate" CssClass="form-control required"  ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtEffectiveDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                </div>
            </div>

            <br />
        </div>
        
         </fieldset>
</asp:Panel>



</asp:content>
