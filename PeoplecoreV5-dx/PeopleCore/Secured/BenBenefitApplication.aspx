<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BenBenefitApplication.aspx.vb" Inherits="Secured_BenBenefitApplication" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

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
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                    
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbApprove" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                        <uc:ConfirmBox runat="server" ID="cfCancel" TargetControlID="lnkCancel" ConfirmMessage="Selected items will be cancelled. Proceed?"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitApplicationNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BenefitApplicationNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataDateColumn FieldName="DateFiled" Caption="Date Filed" />
                                <dx:GridViewDataComboBoxColumn FieldName="BenefitTypeDesc" Caption="Benefit Type" />
                                <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />                                                                  
                                <dx:GridViewDataColumn Caption="Attachment" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkAttachment" OnClick="lnkAttachment_Click" CssClass="fa fa-paperclip " CommandArgument='<%# Eval("BenefitApplicationNo") & "|" & Eval("Fullname")  %>' Font-Size="Medium" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
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

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Employee Name :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem"  CompletionSetCount="1"
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
                <label class="col-md-4 control-label has-required">Date Filed :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtDateFiled" runat="server" CssClass="form-control required" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateFiled" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateFiled" Mask="99/99/9999" MaskType="Date" />
                </div>
            </div>
                        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Benefit Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboBenefitTypeNo" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtAmount" runat="server" CssClass="form-control number" />
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                </div>
            </div>

            <br />                                    
        </div>                    
    </fieldset>
</asp:Panel>

</asp:Content>

