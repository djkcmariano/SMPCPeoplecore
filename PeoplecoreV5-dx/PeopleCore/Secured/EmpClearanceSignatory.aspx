<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpClearanceSignatory.aspx.vb" Inherits="Secured_EmpClearanceSignatory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
    <Content>
        <br /><br />
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
                    </ul>                                        
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeClearanceDetiNo" Width="100%">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeClearanceDetiNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." Width="10%" />                                
                                <dx:GridViewDataTextColumn FieldName="Title" Caption="Department / Office" />
                                <dx:GridViewDataTextColumn FieldName="OrderBy" Caption="Order" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="CreatedDate" Caption="Date Encoded" Width="10%" />
                                <dx:GridViewDataTextColumn FieldName="Aging" Caption="Aging<br/>(Day/s)" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="IsClearedDesc" Caption="Cleared" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="ClearedDate" Caption="Date Cleared" Width="10%" />
                                <dx:GridViewDataTextColumn FieldName="ClearedBy" Caption="Cleared By" />
                                <dx:GridViewDataTextColumn FieldName="UpdateBy" Caption="Modified By" />
                                <dx:GridViewDataTextColumn FieldName="UpdateDate" Caption="Date Last<br />Modified" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" CommandArgument='<%# Eval("EmployeeClearanceDetiNo") & "|" & Eval("Code") %>' OnClick="lnkDetail_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />                                
                            </Columns>
                            <SettingsBehavior AllowSort="false" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
        <br />
        <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">                    
                    <h4>Reference No. :&nbsp;<asp:Label runat="server" ID="lbl" /></h4>
                </div>
                <div>                                     
                    <ul class="panel-controls">                                                                            
                        <li><asp:LinkButton runat="server" ID="lnkAddDeti" OnClick="lnkAddDeti_Click" Text="Add" CssClass="control-primary" /></li>                        
                        <li><asp:LinkButton runat="server" ID="lnkDeleteDeti" OnClick="lnkDeleteDeti_Click" Text="Delete" CssClass="control-primary" /></li>
                    </ul>                                        
                    <uc:ConfirmBox runat="server" ID="cfbDelete1" TargetControlID="lnkDeleteDeti" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                </div>                                                                                                   
            </div>

            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDeti" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeClearanceSignatoryNo" Width="100%">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeClearanceSignatoryNo") %>' OnClick="lnkEditDeti_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="CodeDeti" Caption="Reference No." Width="20%" />                                
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name" />
                                <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />                                
                            </Columns>
                            <SettingsBehavior AllowSort="false" />                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdMain" />                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>    
    </Content>
</uc:Tab>

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtClearanceTemplateDetiDesc" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Department / Office :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboDepartmentNo" CssClass="form-control" DataMember="EDepartment" AutoPostBack="true" OnTextChanged="cboDepartmentNo_TextChanged" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Title :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTitle" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Order Level :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOrderBy" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsCleared" Text="&nbsp;Cleared" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <div style="overflow:auto">
                        <asp:Label ID="lblRemarks" runat="server" CssClass="form-control" Height="150" />
                    </div>                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtRemarks1" CssClass="form-control"  />
                </div>
            </div>
        </div>                         
    </fieldset>
</asp:Panel>

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="lnkCloseDeti" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="Fieldset1">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseDeti" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDeti" OnClick="lnkSaveDeti_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDeti" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Name :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" Placeholder="Type here..." />
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
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
            </div>            
        </div>                         
    </fieldset>
</asp:Panel>


</asp:Content>

