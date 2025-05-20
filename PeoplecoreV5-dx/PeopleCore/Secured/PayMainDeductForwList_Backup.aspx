<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayMainDeductForwList_Backup.aspx.vb" Inherits="Secured_PayMainDeductForwList" %>


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
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" style="width:200px;" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>
                    <div>
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

