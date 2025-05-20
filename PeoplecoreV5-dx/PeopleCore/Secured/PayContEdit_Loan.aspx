<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayContEdit_Loan.aspx.vb" Inherits="Secured_PayBankDiskRefList" %>
<%@ Register Src="~/Include/wucPayContHeader.ascx" TagName="PayContHeader" TagPrefix="uc" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<uc:PayContHeader runat="server" ID="PayContHeader1" />

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6">
                        <asp:Label ID="lbl" runat="server" CssClass="panel-title"></asp:Label>
                        <div style="visibility:hidden;position:absolute;">
                            <asp:Checkbox ID="txtIsPosted" runat="server" ></asp:Checkbox>
                        </div>
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXTotal" KeyFieldName="PayContDetiLoanNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataTextColumn FieldName="HiredDate" Caption="Date Hired"  Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="SSSNo" Caption="SSS No."  Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="HDMFNo" Caption="HDMF No."  Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PayDeductTypeDesc" Caption="Loan Type" />
                                    <dx:GridViewDataTextColumn FieldName="RefNo" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="PrincipalAmount" Caption="Principal Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="GrantedDate" Caption="Date Granted" />
                                    <dx:GridViewDataTextColumn FieldName="Amort" Caption="Amortization" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Modified" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Modified By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ApplicableMonth" Caption="Applicable Month" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsManual" Caption="Manual Add?" ReadOnly="true" Visible="false" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" /> 
                                </Columns>      
                                <Settings ShowGroupFooter="VisibleIfExpanded" ShowFooter="true" />  
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="FullName" SummaryType="Count" />
                                    <dx:ASPxSummaryItem FieldName="Amort" SummaryType="Sum" />
                                </TotalSummary>                       
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
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

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-required">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayContDetiLoanNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                </div>
            </div>
             <div  class="form-group" >
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" placeholder="Type here..." /> 
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
                <label class="col-md-4 control-label has-required">Loan Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayDeductTypeNo" DataMember="EPayContLoanType" runat="server" CssClass="form-control required"  ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtRefNo" runat="server" CssClass="form-control"  ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Principal Amount :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtPrincipalAmount" runat="server" CssClass="form-control"  ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPrincipalAmount" FilterType="Numbers, Custom" ValidChars="-." /> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date Granted :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtGrantedDate" runat="server" CssClass="form-control required"  ></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtGrantedDate" Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtGrantedDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amortization :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtAmort" runat="server" CssClass="form-control required"  ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmort" FilterType="Numbers, Custom" ValidChars="-." /> 
                </div>
            </div>

            

            <br />
        </div>
        
         </fieldset>
</asp:Panel>
</asp:Content> 