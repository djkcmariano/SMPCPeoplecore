<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayBonusList.aspx.vb" Inherits="Secured_PayBonusList" Theme="PCoreStyle" %>

<asp:Content id="Content1" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">
    function grid_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield.Set('VisibleIndex', parseInt(e.index));
        pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
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
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" Visible="false"/></li>
                        <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>                        
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                    <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process" />                    
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
                    OnFillContextMenuItems="MyGridView_FillContextMenuItems">                                                                                   
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
                            <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" CellStyle-HorizontalAlign="Left"/>
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
                                    <asp:LinkButton runat="server" ID="lnkForward" OnClick="lnkForward_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Forward to Forwarded Income Module" /><br />
                                    <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkForward" ConfirmMessage="Do you want to proceed?" MessageType="Process" />
                                </Template>
                            </dx:MenuItem>
                        </Items>
                        <ItemStyle Width="240px"></ItemStyle>
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
                            <li><asp:LinkButton runat="server" ID="lnkExportD" OnClick="lnkExportD_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportD" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        <div class="panel-body">
            <div class="row">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayBonusDetiNo" Width="100%">
                        <Columns>
                            
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataTextColumn FieldName="HiredDate" Caption="Date Hired" />
                            <dx:GridViewDataTextColumn FieldName="RegularizedDate" Caption="Date Regularized" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="DaysServeCutOffDate" Caption="No. of Days Served" />
                            <dx:GridViewDataTextColumn FieldName="lwop" Caption="LWOP" />
                            <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="Factor" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="GrossAmount" Caption="Gross" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                            <dx:GridViewDataTextColumn FieldName="Adjustment" Caption="Adjustment" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                            <dx:GridViewDataTextColumn FieldName="NetPay" Caption="Net Pay" PropertiesTextEdit-DisplayFormatString="{0:N2}" />                                          
                            <dx:GridViewDataTextColumn FieldName="BonusTypeDesc" Caption="Bonus Type" />
                        </Columns>

                        <SettingsContextMenu Enabled="true">                                
                            <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                        </SettingsContextMenu>                                                                                            
                        <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                        <SettingsSearchPanel Visible="true" />  
                        <Settings ShowFooter="true" />       
                                             
                    </dx:ASPxGridView>     
                    <dx:ASPxGridViewExporter ID="grdExportD" runat="server" GridViewID="grdDetl" />                
                </div>
            </div>                                                           
        </div>                   
    </div>
</div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
        </div>         
        <div  class="entryPopupDetl form-horizontal">                     
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:HiddenField runat="server" ID="hifPayNo" />
                    <asp:Textbox ID="txtPayCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Pay Date :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayDate" runat="server" CssClass="form-control required" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtPayDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtPayDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPayDate" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Payroll group :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPayClassNo" runat="server" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">
                    <h5><b>Cut Off Date</b></h5>
                </label>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtStartDate" runat="server" CssClass="form-control required" placeholder="From" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator2" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate" />
                </div>                           
                <div class="col-md-3">
                    <asp:Textbox ID="txtEndDate" runat="server" CssClass="form-control required" placeholder="To" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator3" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Applicable :</label>
                <div class="col-md-3">
                    <asp:Dropdownlist ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control" placeholder="Month" />
                </div>
                <div class="col-md-3">
                    <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control" placeholder="Year" />
                </div>
            </div>   
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Payroll Period :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayPeriod" runat="server" CssClass="form-control" />                  
                </div>
            </div>                                    

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Performance Period Type :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPEPeriodNo" DataMember="EPEPeriod" runat="server" CssClass="form-control" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">No. Of Months To Assume :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtNoOfMonthsAssume" CssClass="number form-control" runat="server" />                               
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox ID="txtIsPaymentSuspended" runat="server" Text="&nbsp;Suspend for review (Exlude from YTD)" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox ID="txtIsActivateDed" runat="server" Text="&nbsp;Activate the attendance base deduction" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">
                    <h5><b>Applicable Deduction</b></h5>
                </label>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-3">                    
                    <asp:Checkbox ID="txtIsDeductTax" runat="server" Text="&nbsp;Tax" />                    
                </div>               
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">
                    <h5><b>Payroll Components</b></h5>
                </label>
            </div>                                            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-3">                    
                    <asp:Checkbox ID="txtIsIncludeMass" runat="server" Text="&nbsp;Template" />
                </div>
                <div class="col-md-3">                    
                    <asp:Checkbox ID="txtIsIncludeForw" runat="server" Text="&nbsp;Forwarded" />
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-3">                    
                    <asp:Checkbox ID="txtIsIncludeLoan" runat="server" Text="&nbsp;Loan" />
                </div>
                <div class="col-md-3">                    
                    <asp:Checkbox ID="txtIsIncludeOther" runat="server" Text="&nbsp;Other" />
                </div>
            </div>
            <br />
        </div>
        <br />          
    </fieldset>
</asp:Panel>


<%--<asp:Button ID="btnShow2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShow2" PopupControlID="Panel2" CancelControlID="lnkClose2" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel2" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="fsDetl">
        <!-- Header here -->
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave2" CssClass="fa fa-floppy-o submit fsDetl lnkSave2" OnClick="lnkSave2_Click"  />   
        </div>
         <!-- Body here -->
        <div  class="entryPopupDetl2 form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">                                        
                    <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Date :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtStartDateX" runat="server" CssClass="form-control" placeholder="From" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDateX" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDateX" />
                    <asp:CompareValidator runat="server" ID="CompareValidator4" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDateX" />
                </div>
                <div class="col-md-3">
                    <asp:Textbox ID="txtEndDateX" runat="server" CssClass="form-control" placeholder="To" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDateX" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDateX" />
                    <asp:CompareValidator runat="server" ID="CompareValidator5" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDateX" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">
                    <h5><b>Percent Factor</b></h5>
                </label>
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label">Late :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtLate" runat="server" CssClass="number form-control" />                                                          
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Undertime :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtUnder" runat="server" CssClass="number form-control" />                                                                              
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Absent :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtAbsent" runat="server" CssClass="number form-control" />                                                               
                </div>
            </div>
            <br />
        </div>
    </fieldset>
</asp:Panel>--%>

</asp:Content>