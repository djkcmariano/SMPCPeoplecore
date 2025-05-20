<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BENBenefitBalanceList.aspx.vb" Inherits="Secured_BENBenefitBalanceList" %>



<asp:content ID="Cont1" contentplaceholderid="cphBody" runat="server">


<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="col-md-4">
                                <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkGo_Click" CssClass="form-control" runat="server" />
                            </div>
                            <ul class="panel-controls pull-left">
                                <li>
                                    <a style="text-decoration:none;color:Black;">Year &nbsp;&nbsp;</a>
                                </li>
                                <li>
                                    <asp:TextBox ID="txtYear"  SkinID="txtdate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="lnkGo_Click" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtYear" FilterType="Numbers, Custom" ValidChars="-." /> 
                                    <%--<ajaxToolkit:NumericUpDownExtender ID="NUD1" runat="server" TargetControlID="txtYear" Width="100" Minimum="2010" />--%>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
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
                                    <dx:GridViewDataComboBoxColumn FieldName="BenefitTypeDesc" Caption="Benefit Type" />
                                    <dx:GridViewDataTextColumn FieldName="Credit" Caption="Earned" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="Used" Caption="Used" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="Balance" Caption="Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
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
                                    <h3>Benefit Earned, Credits & Adjustments</h3> 
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
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="BenefitCreditNo" Width="100%">                                                                                   
                                <Columns>                            
                                    <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>--%>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false"/>
                                    <dx:GridViewDataDateColumn FieldName="AcquireDate" Caption="Acquired Date" />
                                    <dx:GridViewDataComboBoxColumn FieldName="BenefitTypeDesc" Caption="Benefit Type" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remarks" />
                                    <%--<dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" Visible="false"/>
                                    <dx:GridViewDataTextColumn FieldName="EncodedByName" Caption="Encoded By" Visible="false"/>--%>
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
                                    <h3>Benefit Used</h3>
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
                           <dx:ASPxGridView ID="grdDetl1" ClientInstanceName="grdDetl1" runat="server" KeyFieldName="BenefitApplicationNo" Width="100%">                                                                                   
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                    <dx:GridViewDataDateColumn FieldName="DateFiled" Caption="Date Filed" />
                                    <dx:GridViewDataComboBoxColumn FieldName="BenefitTypeDesc" Caption="Benefit Type" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />
                                    <dx:GridViewDataTextColumn FieldName="BenefitStatDesc" Caption="Status" Visible="false" />
                                </Columns> 
                                <SettingsContextMenu Enabled="true">                                
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                                <SettingsSearchPanel Visible="false" />  
                                
                                <Settings ShowFooter="true" />  
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="Amount" SummaryType="Custom" />
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
                    <asp:TextBox ID="txtBenefitCreditNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                     <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
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
                <label class="col-md-4 control-label has-required">Benefit Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboBenefitTypeNo" CausesValidation="false" runat="server" CssClass="required form-control">
                    </asp:DropdownList>
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-3">
                   <asp:TextBox ID="txtAmount" runat="server" CssClass="required form-control"></asp:TextBox>
                   <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount" FilterType="Numbers, Custom" ValidChars="-." /> 
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
                <label class="col-md-4 control-label has-required">Remarks :</label>
                <div class="col-md-7">
                   <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="3" runat="server" CssClass="required form-control"></asp:TextBox>
                 </div>
            </div>

            <br />
        </div>
        
         </fieldset>
</asp:Panel>

</asp:content>