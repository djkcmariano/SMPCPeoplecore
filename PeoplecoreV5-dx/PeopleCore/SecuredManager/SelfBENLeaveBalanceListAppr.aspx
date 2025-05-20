<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfBENLeaveBalanceListAppr.aspx.vb" Inherits="Secured_BENLeaveBalanceList" %>



<asp:content ID="Cont1" contentplaceholderid="cphBody" runat="server">



<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6">
                        <div class="form-group">
                            <ul class="panel-controls pull-left">
                                <li>
                                    <a style="text-decoration:none;color:Black;">As of &nbsp;&nbsp;</a>
                                </li>
                                <li >
                                    <asp:TextBox ID="txtDate"  SkinID="txtdate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkGo_Click" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true" />
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                             
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender6" TargetControlID="RangeValidator3" />
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataComboBoxColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                    <dx:GridViewDataTextColumn FieldName="Earned" Caption="Earned" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center" Visible="False" />
                                    <dx:GridViewBandColumn Caption="A" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Credit" Caption="Credit" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        </Columns>
                                    </dx:GridViewBandColumn>   
                                    <dx:GridViewBandColumn Caption="B" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Used" Caption="Used" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        </Columns>
                                    </dx:GridViewBandColumn>   
                                    <dx:GridViewBandColumn Caption="C" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Forefeit" Caption="Forfeited" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        </Columns>
                                    </dx:GridViewBandColumn>  
                                    <dx:GridViewBandColumn Caption="D = A - B - C" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Balance" Caption="Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        </Columns>
                                    </dx:GridViewBandColumn> 
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>                          
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6 panel-title">
                        Name: <asp:Label ID="lblDetl" runat="server"></asp:Label>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" Visible="false" /></li>                                                    
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="panel-body">
                    <div class="row">
                         <p><asp:Label ID="lblDate" runat="server"></asp:Label></p>    
                    </div>
                    <div class="row">
                        <div class="panel-body">
                             <div class="row">
                                <div class="col-md-6">
                                    <h3>Leave Earned, Credits, & Adjustments</h3> 
                                </div>
                                <div class="col-md-6">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                        <ContentTemplate>
                                                <asp:LinkButton runat="server" ID="lnkExportD" OnClick="lnkExportD_Click" Text="Export" CssClass="control-primary pull-right" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkExportD" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="LeaveCreditNo" Width="100%">                                                                                   
                                <Columns>                            
                                    <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>--%>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false"/>
                                    <dx:GridViewDataComboBoxColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                    <dx:GridViewDataDateColumn FieldName="AcquireDate" Caption="Acquired Date" />
                                    <dx:GridViewDataDateColumn FieldName="DateForefeited" Caption="Forfeited Date" />
                                    <dx:GridViewDataTextColumn FieldName="LeaveHrs" Caption="Leave Hr/s" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remarks" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" Visible="false"/>
                                    <dx:GridViewDataTextColumn FieldName="EncodedByName" Caption="Encoded By" Visible="false"/>
                                    <%--<dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" Visible="false" /> --%>
                                </Columns>     
                                
                                <SettingsContextMenu Enabled="true">                                
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                                <SettingsSearchPanel Visible="false" />  
                                
                                <Settings ShowFooter="true" />  
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="LeaveHrs" SummaryType="Sum" />   
                                </TotalSummary>
                                                       
                            </dx:ASPxGridView>
                            
                            <dx:ASPxGridViewExporter ID="grdExportD" runat="server" GridViewID="grdDetl" />   
                        </div>
                        </div>
                    </div>

                     <div class="row">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <h3>Leave Used</h3>
                                </div>
                                <div class="col-md-6">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                        <ContentTemplate>
                                                <asp:LinkButton runat="server" ID="lnkExportUsed" OnClick="lnkExportUsed_Click" Text="Export" CssClass="control-primary pull-right" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkExportUsed" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl1" ClientInstanceName="grdDetl1" runat="server" KeyFieldName="LeaveApplicationDetiNo" Width="100%">                                                                                   
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Leave Application No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                    <dx:GridViewDataDateColumn FieldName="DTRDate" Caption="DTR Date" />
                                    <dx:GridViewDataTextColumn FieldName="DayDesc" Caption="Day" />
                                    <dx:GridViewDataTextColumn FieldName="DayTypeCode" Caption="Day Type" />
                                    <dx:GridViewDataComboBoxColumn FieldName="ShiftDesc" Caption="Shift" />
                                    <dx:GridViewDataComboBoxColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                    <dx:GridViewDataTextColumn FieldName="PaidHrs" Caption="Paid Hr/s" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" /> 
                                </Columns> 
                                <SettingsContextMenu Enabled="true">                                
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                                <SettingsSearchPanel Visible="false" />  
                                
                                <Settings ShowFooter="true" />  
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="PaidHrs" SummaryType="Custom" />
                                    <dx:ASPxSummaryItem FieldName="tStatus" SummaryType="Custom" />
                                </TotalSummary>                           
                            </dx:ASPxGridView> 

                            <dx:ASPxGridViewExporter ID="grdExportUsed" runat="server" GridViewID="grdDetl1" />
                        </div>
                        </div>
                    </div>

                </div>
                   
            </div>
       </div>
      

                   

 </div>


<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtLeaveCreditNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                     <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..."/> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
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
                <label class="col-md-4 control-label has-required">Leave Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboLeaveTypeno" CausesValidation="false" DataMember="ELeaveType" runat="server" CssClass="required form-control">
                    </asp:DropdownList>
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Leave Hr/s. :</label>
                <div class="col-md-3">
                   <asp:TextBox ID="txtLeaveHRs" runat="server" CssClass="required form-control"></asp:TextBox>
                   <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtLeaveHRs" FilterType="Numbers, Custom" ValidChars="-." /> 
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Acquired Date :</label>
                <div class="col-md-3">
                     <asp:TextBox ID="txtAcquireDate" runat="server" CssClass="required form-control"> </asp:TextBox>


                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAcquireDate"
                    Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtAcquireDate"
                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    
                     <asp:RangeValidator
                    ID="RangeValidator3"
                    runat="server"
                    ControlToValidate="txtAcquireDate"
                    ErrorMessage="<b>Please enter valid entry</b>"
                    MinimumValue="01-01-1900"
                    MaximumValue="12-31-3000"
                    Type="Date" Display="None"  />
                    
                    <ajaxToolkit:ValidatorCalloutExtender 
                    runat="Server" 
                    ID="ValidatorCalloutExtender2"
                    TargetControlID="RangeValidator3" /> 

                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Date Forfeited :</label>
                <div class="col-md-3">
                     <asp:TextBox ID="txtDateForefeited" runat="server" CssClass="form-control"> </asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateForefeited"
                    Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDateForefeited"
                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    
                     <asp:RangeValidator
                    ID="RangeValidator2"
                    runat="server"
                    ControlToValidate="txtDateForefeited"
                    ErrorMessage="<b>Please enter valid entry</b>"
                    MinimumValue="01-01-1900"
                    MaximumValue="12-31-3000"
                    Type="Date" Display="None"  />
                    
                    <ajaxToolkit:ValidatorCalloutExtender 
                    runat="Server" 
                    ID="ValidatorCalloutExtender1"
                    TargetControlID="RangeValidator2" /> 

                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Remark :</label>
                <div class="col-md-7">
                   <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="3" runat="server" CssClass="required form-control"></asp:TextBox>
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                   <asp:CheckBox ID="txtIsConsume" runat="server" Text="&nbsp; Please check here to add or deduct bank balance."></asp:CheckBox>
                 </div>
            </div>

            <br />
        </div>
        
         </fieldset>
</asp:Panel>

</asp:content>