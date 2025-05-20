<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SelfEmployeeAlteApprover.aspx.vb" Inherits="Secured_SelfEmployeeAlteApprover" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">

    function Split(obj, index) {
        var items = obj.split("|");
        for (i = 0; i < items.length; i++) {
            if (i == index) {
                return items[i];
            }
        }
    }

    function getapp(source, eventArgs) {
        document.getElementById('<%= hifappno.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }

    function getalte(source, eventArgs) {
        document.getElementById('<%= hifalteno.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }

</script>

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeAlteNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="ApprFullname" Caption="Name of Approver" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="AlteFullname" Caption="Name of Alternate Approver" />
                                    <dx:GridViewDataTextColumn FieldName="FromDate" Caption="Date From" />
                                    <dx:GridViewDataTextColumn FieldName="ToDate" Caption="Date To" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" />
                                    <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" /> 
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
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label  has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeAlteNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-required">Name of Approver :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtApprFullname" CssClass="form-control" style="display:inline-block;" placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifappno"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtApprFullname" MinimumPrefixLength="2"
                    CompletionInterval="250" ServiceMethod="populateManager" ServicePath="~/asmx/WebService.asmx"
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getapp" FirstRowSelected="true" UseContextKey="true" CompletionSetCount="1" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Alternate Approver :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtAlteFullname" CssClass="form-control required" style="display:inline-block;" placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifalteno"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                    TargetControlID="txtAlteFullname" MinimumPrefixLength="2"
                    CompletionInterval="250" ServiceMethod="populateManagerAll" ServicePath="~/asmx/WebService.asmx"
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getalte" FirstRowSelected="true" UseContextKey="true" CompletionSetCount="1" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Date From :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtfromdate" runat="server" SkinID="txtdate" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdate" Format="MM/dd/yyyy" />                   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtfromdate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtfromdate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4" TargetControlID="RangeValidator1" /> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Date To :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txttodate" runat="server" SkinID="txtdate" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttodate" Format="MM/dd/yyyy" />                   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txttodate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txttodate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator2" /> 
                </div>
            </div>
                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


</asp:content>
