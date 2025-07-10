<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BenHousingApplication.aspx.vb" Inherits="Secured_BenHousingApplication" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

     <script type="text/javascript">
         function cbCheckAll_CheckedChanged(s, e) {
             grdMain.PerformCallback(s.GetChecked().toString());
         }

         function grid_ContextMenu(s, e) {
             if (e.objectType == "row")
                 hiddenfield.Set('VisibleIndex', parseInt(e.index));
             pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
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
                            <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" Visible="false" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDisApproved" OnClick="lnkDisApproved_Click" Text="DisApprove" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary" Visible="false" /></li>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HousingApplicationNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback" OnFillContextMenuItems="MyGridView_FillContextMenuItems">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("HousingApplicationNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataComboBoxColumn FieldName="HousingTypeDesc" Caption="Applliation Type" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" />
                                <dx:GridViewDataComboBoxColumn FieldName="HousingClassDesc" Caption="Housing Class" />
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status" Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveBy" Caption="Approved /<br />Disapproved<br />By" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveDate" Caption="Approved /<br />Disapproved<br />Date" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date<br />Applied &nbsp;&nbsp;" Width="5%" HeaderStyle-VerticalAlign="Top" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Applied By" Visible="false" />                                                           
                                <dx:GridViewDataColumn Caption="Attachment" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkAttachment" OnClick="lnkAttachment_Click" CssClass="fa fa-paperclip " CommandArgument='<%# Eval("HousingApplicationNo") & "|" & Eval("Fullname")  %>' Font-Size="Medium" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
				                </dx:GridViewCommandColumn>
                            </Columns>   
                            <ClientSideEvents ContextMenu="grid_ContextMenu" />                          
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                        <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                            <Items>
                                <dx:MenuItem Text="Report" Name="Name">
                                    <Template>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Company Housing Availment Form" /><br />
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
                <label class="col-md-4 control-label has-space">Application Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboHousingTypeNo" DataMember="EHousingType" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Subsidy :</label>
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

