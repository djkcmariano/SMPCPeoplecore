<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayLastList.aspx.vb" Inherits="Secured_PayLastList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">

    function grid_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield.Set('VisibleIndex', parseInt(e.index));
        pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }

    function gridDetl_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield1.Set('VisibleIndex', parseInt(e.index));
        pmRowMenuDetl.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }
</script>
 
<div class="row">
    <div class="panel panel-default">
        <div class="panel-heading">                                
            <div class="col-md-2">
                <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
            </div>                
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">                        
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary"  Visible="false"/></li>
                        <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>                        
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                    <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process"  />                    
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
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayNo"
                        OnFillContextMenuItems="grdMain_FillContextMenuItems">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." />                                
                            <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                            <dx:GridViewDataComboBoxColumn FieldName="PayTypeDesc" Caption="Payroll Type" />
                            <dx:GridViewDataDateColumn FieldName="PayDate" Caption="Pay Date" />
                            <dx:GridViewDataTextColumn FieldName="PayPeriod" Caption="Period" Width="5%" CellStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataComboBoxColumn FieldName="MonthDesc" Caption="Month" />
                            <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" />
                            <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />
                            
                            <dx:GridViewDataColumn Caption="Other<br />Income"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkOtherIncome" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkOtherIncome_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Other<br />Deduction"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkOtherDeduction" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkOtherDeduction_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Summary"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkSummary" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkSummary_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Process" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkProcess_Detail" CssClass='<%# Bind("Icon") %>' Enabled='<%# Bind("IsEnabled") %>' OnClick="lnkProcess_Detail_Click" />
                                    <uc:ConfirmBox runat="server" ID="cfProcess_Detail" TargetControlID="lnkProcess_Detail" ConfirmMessage='<%# Bind("ConfirmMessage") %>' Visible='<%# Bind("IsEnabled") %>'  /> 
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>--%>
                            <dx:GridViewDataColumn Caption="Details"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list control-primary" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>

                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />     
                        </Columns>       
                        <ClientSideEvents ContextMenu="grid_ContextMenu" />                
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                    <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                        <Items>
                            <dx:MenuItem Text="Report" Name="Name">
                                <Template>
                                    <asp:LinkButton runat="server" ID="lnkPrint" OnClick="lnkPrint_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Payroll Register Report" /><br />
                                </Template>
                            </dx:MenuItem>
                        </Items>
                        <ItemStyle Width="250px"></ItemStyle>
                    </dx:ASPxPopupMenu>
                    <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />

                </div>
            </div>                                                           
        </div> 
    </div>
</div>

<div class="row">
    <div class="panel panel-default">
        <div class="panel-heading">
                    <div class="col-md-6 panel-title">
                        Payroll No. :&nbsp;<asp:Label ID="lblDetl" runat="server"></asp:Label>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAddD" OnClick="lnkAddD_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                <li><asp:LinkButton runat="server" ID="lnkDeleteD" OnClick="lnkDeleteD_Click" Text="Delete" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkExportD" OnClick="lnkExportD_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteD" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExportD" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
        <div class="panel-body">
            <div class="row">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayLastDetiNo" Width="100%"
                    OnFillContextMenuItems="grdDetl_FillContextMenuItems">
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEditD" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditD_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataDateColumn FieldName="HiredDate" Caption="Date Hired" />
                            <dx:GridViewDataDateColumn FieldName="RegularizedDate" Caption="Date Regularized" />
                            <dx:GridViewDataDateColumn FieldName="SeparatedDate" Caption="Separated Date" />
                            <dx:GridViewDataTextColumn FieldName="DaysServeCutOffDate" Caption="No. of Days Served" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="YearsServe" Caption="Year(s) Served" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeRateClassDesc" Caption="Rate Class" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Salary" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />  
                            <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />  
        
                        </Columns>
                        <ClientSideEvents ContextMenu="gridDetl_ContextMenu" />   
                        
                        <SettingsContextMenu Enabled="true">                                
                            <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                        </SettingsContextMenu>                                                                                            
                        <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                        <SettingsSearchPanel Visible="true" />  
                        <Settings ShowFooter="false" />     
                                           
                    </dx:ASPxGridView>        
                    <dx:ASPxGridViewExporter ID="grdExportD" runat="server" GridViewID="grdDetl" />  
                    
                    <dx:ASPxPopupMenu ID="pmRowMenuDetl" runat="server" ClientInstanceName="pmRowMenuDetl">
                        <Items>
                            <dx:MenuItem Text="Report" Name="Name">
                                <Template>
                                    <asp:LinkButton runat="server" ID="lnkResignRpt" OnClick="lnkResignRpt_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Resigned Remuneration Report" /><br />
                                </Template>
                            </dx:MenuItem>
                        </Items>
                        <ItemStyle Width="250px"></ItemStyle>
                    </dx:ASPxPopupMenu>
                    <dx:ASPxHiddenField ID="hf1" runat="server" ClientInstanceName="hiddenfield1" />
                              
                </div>
            </div>                                                           
        </div>                   
    </div>
</div>

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="Panel1"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
           
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction no. :</label>
                <div class="col-md-7">
                    <asp:HiddenField runat="server" ID="hifPayNo" />
                    <asp:Textbox ID="txtPayCode" ReadOnly="true" runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Pay date :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayDate" runat="server" skinid="txtdate" CssClass="form-control required"
                        ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4x" runat="server"
                        TargetControlID="txtPayDate"
                        Format="MM/dd/yyyy" />
                                                                              
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                        TargetControlID="txtPayDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />

                        <asp:RangeValidator
                        ID="RangeValidator1x"
                        runat="server"
                        ControlToValidate="txtPayDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                            
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender1x"
                        TargetControlID="RangeValidator1x" />    
                                                                           
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Payroll group :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayClassNo" runat="server" CssClass="form-control required"
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">CUT-OFF DATE :</label>
                <div class="col-md-7">
           
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Start date :</label>
                <div class="col-md-2">
                    <asp:Textbox ID="txtStartDate" runat="server" skinid="txtdate" CssClass="form-control required"
                        ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4xx" runat="server"
                        TargetControlID="txtStartDate"
                        PopupButtonID="ImageButton1"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4xx" runat="server"
                        TargetControlID="txtStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                    <asp:RangeValidator
                        ID="RangeValidator1xx"
                        runat="server"
                        ControlToValidate="txtStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender6"
                        TargetControlID="RangeValidator1xx" />
                </div>
            
                <label class="col-md-2 control-label has-required">End date :</label>
                <div class="col-md-2">
                    <asp:Textbox ID="txtEndDate" runat="server" skinid="txtdate" CssClass="form-control required"
                        ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                        TargetControlID="txtEndDate"
                        PopupButtonID="ImageButton2"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtEndDate"
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
                        ControlToValidate="txtEnddate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender1"
                        TargetControlID="RangeValidator3" />
                </div>
            </div>
            <div class="form-group" runat="server" visible="false">
                <label class="col-md-4 control-label has-required">Base date :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBaseDateNo" DataMember="EDateBase" runat="server" CssClass="form-control"
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Applicable month :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Applicable year :</label>
                <div class="col-md-2">
                    <asp:Textbox ID="txtApplicableYear" runat="server" SkinID="txtdate" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            
                <label class="col-md-2 control-label">Payroll period :</label>
                <div class="col-md-2">
                    <asp:Textbox ID="txtPayperiod" runat="server" SkinID="txtdate" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group" runat="server" visible="false">
                <label class="col-md-4 control-label has-required">Bonus basis :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBonusBasisNo" DataMember="EBonusBasis" runat="server" CssClass="form-control required" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group" runat="server" visible="false">
                <label class="col-md-4 control-label has-required">Bonus type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBonusTypeNo" DataMember="EBonusType" runat="server" CssClass="form-control required" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label">Please click here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="txtIsPaymentSuspended" runat="server" />
                        <span >Tick to suspend for review (Exclude from YTD)</span>
                </div>
            </div>

            <div class="form-group" runat="server" visible="false">
                <label class="col-md-4 control-label">Please click here</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsActivateDed" runat="server" />
                    <span >Tick to activate the attendance base deduction</span>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">APPLICABLE DEDUCTIONS :</label>
                <div class="col-md-7">
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label ">Tax :</label>
                <div class="col-md-1 ">
                    <asp:Checkbox ID="txtIsDeductTax" runat="server" 
                                    ></asp:Checkbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">PAYROLL COMPONENTS :</label>
                <div class="col-md-7">
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label ">Forwarded/one time :</label>
                <div class="col-md-1 ">
                    <asp:Checkbox ID="txtIsIncludeForw" runat="server" 
                                    ></asp:Checkbox>
                </div>
           
                <label class="col-md-2 control-label ">Other Income/Deduction :</label>
                <div class="col-md-1 ">
                    <asp:Checkbox ID="txtIsIncludeOther" runat="server" 
                                    ></asp:Checkbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label ">Loan transaction :</label>
                <div class="col-md-1 ">
                    <asp:Checkbox ID="txtIsIncludeLoan" runat="server" 
                                    ></asp:Checkbox>
                </div>
            
                <label class="col-md-2 control-label ">Template :</label>
                <div class="col-md-1 ">
                    <asp:Checkbox ID="txtIsIncludeMass" runat="server" 
                                    ></asp:Checkbox>
                    <asp:Checkbox ID="txtIsposted" Visible="false" runat="server" 
                        ></asp:Checkbox>
                </div>
            </div>
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
  
<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="Panel2"
    CancelControlID="imgClosed" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="Panel2" runat="server" CssClass="entryPopup2">
        <fieldset class="form" id="fsDetl">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsDetl btnSaveDetl" OnClick="btnSaveDetl_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl2 form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayLastDetiNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" CompletionSetCount="1" 
                    CompletionInterval="250" ServiceMethod="cboEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" />
                        <script type="text/javascript">
                            function getRecord(source, eventArgs) {
                                document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                            }
                        </script>
                </div>
            </div>
            <div class="form-group" style="visibility:hidden;position:absolute;">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-7">
                    <asp:checkbox ID="txtIsIncludeLeavebalance" runat="server" Text="Please check here if you include the remaining leave balance for SL and VL."></asp:checkbox>
                </div>
            </div>
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>


</asp:Content>

