<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayLeaveList.aspx.vb" Inherits="Secured_PayLeaveList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                            <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" CellStyle-HorizontalAlign="Left" />
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
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayLeaveDetiNo" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                            <dx:GridViewDataTextColumn FieldName="NoOfLeaves" Caption="No. of Leaves"  Width="5%" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="GrossAmount" Caption="Gross" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="Adjustment" Caption="Adjustment"  PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeRateClassDesc" Caption="Rate Class" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="CalendarYear" Caption="Calendar Year" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="HourlyRate" Caption="Hourly Rate"  PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewBandColumn Caption="Leave Hours" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="Leavehrs" Caption="Non-Tax" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="LeaveHrsTax" Caption="Taxable" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="TotalLeaveHrs" Caption="Total" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewBandColumn Caption="Net Pay" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="NetPay" Caption="Non-Tax" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="NetPayTax" Caption="Taxable" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="TotalNetPay" Caption="Total" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                </Columns>
                            </dx:GridViewBandColumn>
                                                    
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
<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />

<asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
        </div>         
        <div class="entryPopupDetl form-horizontal">           
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
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
                <label class="col-md-4 control-label has-required">Payroll Group :</label>
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
                    <asp:Dropdownlist ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control" />                        
                </div>
                <div class="col-md-3">
                    <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control" />                        
                </div>
            </div>
            <div class="form-group">                                            
                <label class="col-md-4 control-label has-space">Payroll Period :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayPeriod" runat="server" CssClass="form-control" />
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-labe has-spacel">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsPaymentSuspended" runat="server" Text="&nbsp;Suspend for review (Exlude from YTD)" />                        
                </div>
            </div>
            <%--<div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsAdvancedCredits" runat="server" Text="&nbsp;Process leave balance up to end date." />                    
                </div>
            </div>--%>            
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
    </fieldset>
</asp:Panel>

<%--<script type="text/javascript">
    
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdDetl" runat="server" DataKeyNames="PayLeaveDetiNo" SkinID="grdDetl" >
                           <Columns>
                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server"   Text='<%# Bind("PayLeaveDetiNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                                                                            
                                                                            
                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee No." >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                </asp:BoundField>

                            <asp:BoundField DataField="Fullname" HeaderText="Employee Name" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>

                        
                            <asp:BoundField DataField="LeaveTypeDesc" HeaderText="Leave Type" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="GrossAmount" HeaderText="Gross" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                                                                            
                            <asp:BoundField DataField="Adjustment" HeaderText="Adjustment" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                                                                            
                                                                              
                                                                                                                                                      
                                <asp:BoundField DataField="NetPay" HeaderText="NetPay(Non-Tax)" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                                                                             
                                <asp:BoundField Visible="false"  DataField="PayIncomeTypeDescNT" HeaderText="Income Type(Non-Tax)" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                                                                            
                            <asp:BoundField DataField="NetPayTax" HeaderText="NetPay(Tax)" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                                                                             
                                <asp:BoundField Visible="false"  DataField="PayIncomeTypeDescT" HeaderText="Income Type(Tax)" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                                                                           
                                <asp:BoundField DataField="NoOfLeaves" HeaderText="No. of Leaves" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                                                                            
                        </Columns>
                        <PagerSettings Mode="NextPreviousFirstLast" />
                    </mcn:DataPagerGridView> 
                </div> 
                    <div class="row">
                        <div class="col-md-4">
                            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="grdDetl" PageSize="10">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/images/arrow_first.png" PreviousPageImageUrl="~/images/arrow_previous.png" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>Page
                                                <asp:Label ID="CurrentPageLabel" runat="server" Text="<%# IIf(Container.TotalRowCount>0,  (Container.StartRowIndex / Container.PageSize) + 1 , 0) %>" /> of
                                                <asp:Label ID="TotalPagesLabel" runat="server" Text="<%# Math.Ceiling (System.Convert.ToDouble(Container.TotalRowCount) / Container.PageSize) %>" /> (
                                                <asp:Label ID="TotalItemsLabel" runat="server" Text="<%# Container.TotalRowCount%>" /> records )
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    <asp:NextPreviousPagerField ButtonType="Image" LastPageImageUrl="~/images/arrow_last.png" NextPageImageUrl="~/images/arrow_next.png" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />                              
                                </Fields>
                            </asp:DataPager>
                        </div>
                        <div class="col-md-6 col-md-offset-2">
                            <div class="btn-group pull-right">
                               
                            </div>
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

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">

            <div style="visibility:hidden;">
                    <asp:Textbox ID="txtPayNo" runat="server" 
                        ></asp:Textbox>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction no. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayCode" ReadOnly="true" runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Pay date :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayDate" runat="server" skinid="txtdate" CssClass="form-control"
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
                    <asp:Dropdownlist ID="cboPayClassNo" runat="server" CssClass="form-control"
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
                    <asp:Textbox ID="txtStartDate" runat="server" skinid="txtdate" CssClass="form-control"
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
                    <asp:Textbox ID="txtEndDate" runat="server" skinid="txtdate" CssClass="form-control"
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
            
            
            <div class="form-group">
                <label class="col-md-4 control-label">Please click here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="txtIsPaymentSuspended" runat="server" />
                        <span >to suspend for review(Exlude from YTD).</span>
                </div>
            </div>

            <div id="Div1" class="form-group" runat="server" visible="false">
                <label class="col-md-4 control-label">Please click here</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsAdvancedCredits" runat="server" />
                    <span > to process leave balance up to end date. </span>
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
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>--%>


</asp:Content>

