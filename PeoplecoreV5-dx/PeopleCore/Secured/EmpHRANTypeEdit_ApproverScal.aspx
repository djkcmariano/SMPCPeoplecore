<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="EmpHRANTypeEdit_ApproverScal.aspx.vb" Inherits="Secured_EmpHRANTypeEdit_ApproverScal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<script type="text/javascript">

    function Split(obj, index) {
        var items = obj.split("|");
        for (i = 0; i < items.length; i++) {
            if (i == index) {
                return items[i];
            }
        }
    }

    function getMain(source, eventArgs) {
        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }

    function disableEmp(value) {
           // if ($("#ctl00_cphBody_cboOrganizationNo").val() == 4 || $("#ctl00_cphBody_cboOrganizationNo").val() == 9 || $("#ctl00_cphBody_cboOrganizationNo").val() == 10 || $("#ctl00_cphBody_cboOrganizationNo").val() == 11) {
        if (value == 4 || value == 9 || value == 10 || value == 11) {
            document.getElementById('<%=txtFullName.ClientID%>').disabled = false;
        } else {
            $("#ctl00_cphBody_txtFullName").val('');
            $("#ctl00_cphBody_hifEmployeeNo").val('');
            document.getElementById('<%=txtFullName.ClientID%>').disabled = true;
        }
    }


</script>

    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Label runat="server" ID="lbl" /> 
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HRANTypeApproverScalNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                        <dx:GridViewDataTextColumn FieldName="OrganizationDesc" Caption="Organization Level" />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name of Approver" />                                        
                                        <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order Level" />
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" SelectAllCheckboxMode="Page" />
                                    </Columns>                            
                                </dx:ASPxGridView>                                
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
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">    
             
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="HRANTypeApproverScalNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"/>
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Organization Level :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboOrganizationNo" CssClass="form-control required" DataMember="EOrganization" onchange="disableEmp(this.value);" runat="server"></asp:DropdownList>
                    </div>
                </div>  
                <div class="form-group">
                    <label class="col-md-4 control-label">Name of Approver :</label>
                    <div class="col-md-7">
                       <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" style="display:inline-block;" /> 
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getMain" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    </div>

                </div>  
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Remarks :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control required"></asp:Textbox>
                    </div>
                </div>              
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Order Level :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtOrderLevel" CssClass="form-control required" SkinID="txtdate"  runat="server"></asp:Textbox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" TargetControlID="txtOrderLevel" ValidChars="."  />                       
                    </div>
                </div>

                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

