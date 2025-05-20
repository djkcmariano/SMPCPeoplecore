<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayBankDiskDetiList.aspx.vb" Inherits="Secured_PayBankDiskDetiList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

     <script type="text/javascript">
         function grid_ContextMenu(s, e) {
             if (e.objectType == "row")
                 hiddenfield.Set('VisibleIndex', parseInt(e.index));
             pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
         }
    </script>
     
<div class="page-content-wrap">   
    <uc:PayHeader runat="server" ID="PayHeader" />      
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" Visible="false" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" Visible="false" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" Visible="false" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary"  Visible="false"/></li>                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary"  Visible="false" /></li>
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXTotal" KeyFieldName="PayBankDiskNo"
                                OnFillContextMenuItems="MyGridView_FillContextMenuItems">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                    <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DateFrom" Caption="Start Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DateTo" Caption="End Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Pay Date" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false"  />
                                    <dx:GridViewDataComboBoxColumn FieldName="PaymentTypeDesc" Caption="Payment Type"  />
                                    <dx:GridViewDataComboBoxColumn FieldName="BankTypeDesc" Caption="Bank Type &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"  />
<%--                                    <dx:GridViewDataTextColumn FieldName="PayDate1" Caption="Pay Date 1" Visible="true" />
                                    <dx:GridViewDataTextColumn FieldName="PayDate2" Caption="Pay Date 2" Visible="true" />
                                    <dx:GridViewDataTextColumn FieldName="PayDate3" Caption="Pay Date 3" Visible="true" />
                                    <dx:GridViewDataTextColumn FieldName="PayDate4" Caption="Pay Date 4" Visible="true" />--%>

                                    <dx:GridViewBandColumn Caption="Pay Date" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" FieldName="PayDate1" Caption="1" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkCreateDisk1" CssClass="fa label" CommandArgument='<%# Bind("PayOutSchedNo1") %>' ToolTip='<%# Bind("PayOutSchedNo1Desc") %>' Text='<%# Bind("PayDate1") %>' Font-Bold="false" ForeColor="#2222CC" Font-Underline="true" Font-Size="Small" OnClick="lnkCreateDisk_Click" OnPreRender="lnkPrint_PreRender" CausesValidation="false" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" FieldName="PayDate2" Caption="2" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkCreateDisk2" CssClass="fa label" CommandArgument='<%# Bind("PayOutSchedNo2") %>' ToolTip='<%# Bind("PayOutSchedNo2Desc") %>' Text='<%# Bind("PayDate2") %>' Font-Bold="false" ForeColor="#2222CC" Font-Underline="true" Font-Size="Small" OnClick="lnkCreateDisk_Click" OnPreRender="lnkPrint_PreRender" CausesValidation="false" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" FieldName="PayDate3" Caption="3" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkCreateDisk3" CssClass="fa label" CommandArgument='<%# Bind("PayOutSchedNo3") %>' ToolTip='<%# Bind("PayOutSchedNo3Desc") %>' Text='<%# Bind("PayDate3") %>' Font-Bold="false" ForeColor="#2222CC" Font-Underline="true" Font-Size="Small" OnClick="lnkCreateDisk_Click" OnPreRender="lnkPrint_PreRender" CausesValidation="false" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" FieldName="PayDate4" Caption="4" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkCreateDisk4" CssClass="fa label" CommandArgument='<%# Bind("PayOutSchedNo4") %>' ToolTip='<%# Bind("PayOutSchedNo4Desc") %>' Text='<%# Bind("PayDate4") %>' Font-Bold="false" ForeColor="#2222CC" Font-Underline="true" Font-Size="Small" OnClick="lnkCreateDisk_Click" OnPreRender="lnkPrint_PreRender" CausesValidation="false" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:GridViewBandColumn>

                                    <dx:GridViewBandColumn Caption="Total Amount" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="TotalAmount1" Caption="1" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            <dx:GridViewDataTextColumn FieldName="TotalAmount2" Caption="2" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            <dx:GridViewDataTextColumn FieldName="TotalAmount3" Caption="3" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            <dx:GridViewDataTextColumn FieldName="TotalAmount4" Caption="4" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        </Columns>
                                    </dx:GridViewBandColumn>   


                                    <dx:GridViewDataTextColumn FieldName="TotalAmount" Caption="<center>Total Amount</center>" PropertiesTextEdit-DisplayFormatString="{0:N2}" />

                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Date Posted" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DateProcessed" Caption="Date Processed" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ApplicableMonth" Caption="Applicable Month" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayPeriod" Caption="Payroll Period" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayCateDesc" Caption="Payroll Category" Visible="false" />
                                    
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Detail" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Visible="false" /> 
                                </Columns>      
                                <ClientSideEvents ContextMenu="grid_ContextMenu" />  
                                <Settings ShowGroupFooter="VisibleIfExpanded" ShowFooter="true" /> 
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="TotalAmount" SummaryType="Sum" />
                                </TotalSummary>
                                                    
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />  
                            
                            <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                                <Items>
                                    <dx:MenuItem Text="Report" Name="Name">
                                        <Template>
                                                <asp:LinkButton runat="server" ID="lnkPrint" OnClick="lnkPrint_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Bank Attachment Report" /><br />
                                                <%--<asp:LinkButton runat="server" ID="lnkUnionBank" OnClick="lnkUnionBank_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="UnionBank Attachment Report" />--%><br />
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
 </div>


  <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <div class="col-md-4 panel-title">
                            Transaction No. :&nbsp;<asp:Label ID="lblDetl" runat="server"></asp:Label>
                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkAddD" OnClick="lnkAddD_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                        <li><asp:LinkButton runat="server" ID="lnkDeleteD" OnClick="lnkDeleteD_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul>
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteD" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExportDetl" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayBankDiskDetiNo" Width="100%">                                                                                   
                                <Columns>   
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditD_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn> 
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />                        
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataTextColumn FieldName="BankAccountNo" Caption="BankAccount No." />
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="AmountManual" Caption="Adjustment Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Pay Date" />
                                    <dx:GridViewDataTextColumn FieldName="Amount1" Caption="Pay out 1"  PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="Amount2" Caption="Pay out 2"  PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="Amount3" Caption="Pay out 3"  PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="Amount4" Caption="Pay out 4"  PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Total Net Pay"  PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" /> 
                                </Columns>   
                                <SettingsContextMenu Enabled="true">                                
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                                <SettingsSearchPanel Visible="true" />  
                                
                                <Settings ShowFooter="true" />  
                                <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7">
                                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" />        
                                </SettingsPager>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="EmployeeCode" SummaryType="Count" />
                                    <dx:ASPxSummaryItem FieldName="AmountManual" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
                                </TotalSummary>                         
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>    

  <asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="Panel2"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="Panel2" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayBankDiskDetiNo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" ></asp:Textbox>
                </div>
            </div> 
            
            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" CompletionSetCount="0" 
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
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control required" TextMode="MultiLine" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Adjustment :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtAmountManual" runat="server" CssClass="form-control required" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmountManual" FilterType="Numbers, Custom" ValidChars="-." /> 
                </div>
            </div> 
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
 
</asp:Content>

