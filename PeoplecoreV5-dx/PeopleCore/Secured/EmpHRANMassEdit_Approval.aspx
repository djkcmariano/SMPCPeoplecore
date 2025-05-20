<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpHRANMassEdit_Approval.aspx.vb" Inherits="Secured_EmpHRANEdit_Approval" Theme="PCoreStyle"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

     <script type="text/javascript">
         function cbCheckAll_CheckedChanged(s, e) {
             grdMain.PerformCallback(s.GetChecked().toString());
         }
    </script>


<uc:Tab runat="server" ID="Tab">
    <Header>        
        <asp:Label runat="server" ID="lbl" /> 
        <div style="display:none;">
            <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
        </div>       
    </Header>
    <Content>
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                    
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HRANMassRoutingNo"
                                OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("HRANMassRoutingNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="HRANApprovalTypeDesc" Caption="Approval Type" />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Signatory Name" />
                                        <dx:GridViewDataTextColumn FieldName="xApproveDate" Caption="Date Approved" />
                                        <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remarks" />         
                                        <dx:GridViewDataTextColumn FieldName="OrderNo" Caption="Order No." />                                    
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
    </Content>
</uc:Tab>
<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none" >
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal"> 

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtHRANMassRoutingNo" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
                                   
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkIsApproved" runat="server" Text="&nbsp;Please tick if you want to approve this transaction" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Approval Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboHRANApprovalTypeNo" runat="server" CssClass="required form-control" DataMember="EHRANApprovalType" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Signatory Name :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>                    
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateManager" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
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
                <label class="col-md-4 control-label has-space">Date Approved :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtApproveDate" runat="server" CssClass="form-control" />
                    <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server" Format="MM/dd/yyyy" TargetControlID="txtApproveDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedSeparatedDate" runat="server" 
                        AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" 
                        MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                        OnInvalidCssClass="MaskedEditError" TargetControlID="txtApproveDate" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Order No. :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="number required form-control" />
                </div>
            </div>
            <br /><br />
        </div>                    
    </fieldset>
</asp:Panel>
</asp:Content>

