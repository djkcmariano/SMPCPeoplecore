<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="OrgPayClassList.aspx.vb" Inherits="Secured_OrgPayClassList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<br />
<div class="page-content-wrap">            
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>                                  
                    <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible ="false"/></li> 
                        <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>                           
                    </ul>

                    <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="lnkDelete" />
                    <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Selected items will be archived. Proceed?"  />
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayClassNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                <dx:GridViewDataTextColumn FieldName="PayClassCode" Caption="Code" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />                                 
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />                            
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>  
<br /><br />

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPayClassCode" runat="server" CssClass="form-control required" />
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPayClassDesc" runat="server" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-required">Company :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboPayLocNo" CssClass="form-control required"  />
                </div>
            </div>
            <div class="form-group">
                                                           
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="txtIsCrew" 
                        runat="server" /> 
                        <span> for crew employee.</span>
                 </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Night Premium :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboNPNo" DataMember="ENightPremium" runat="server" CssClass="form-control"
                        ></asp:Dropdownlist>
               </div>
            </div>
                
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Noted By :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtnotedby" CssClass="form-control" /> 
                    <asp:HiddenField runat="server" ID="hifnotedbyno"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtnotedby" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    <script type="text/javascript">
                        function getRecord(source, eventArgs) {
                            document.getElementById('<%= hifnotedbyno.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Noted By 2 :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtnotedby2" CssClass="form-control" /> 
                    <asp:HiddenField runat="server" ID="hifnotedbyno2"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                    TargetControlID="txtnotedby2" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getRecord1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    <script type="text/javascript">
                        function getRecord1(source, eventArgs) {
                            document.getElementById('<%= hifnotedbyno2.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Prepared By :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtpreparedby" CssClass="form-control" /> 
                    <asp:HiddenField runat="server" ID="hifpreparedbyno"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                    TargetControlID="txtpreparedby" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getRecord2" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    <script type="text/javascript">
                        function getRecord2(source, eventArgs) {
                            document.getElementById('<%= hifpreparedbyno.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Prepared By 2 :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtpreparedby2" CssClass="form-control" /> 
                    <asp:HiddenField runat="server" ID="hifpreparedbyno2"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"  
                    TargetControlID="txtpreparedby2" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getRecord3" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                        <script type="text/javascript">
                            function getRecord3(source, eventArgs) {
                                document.getElementById('<%= hifpreparedbyno2.ClientID %>').value = eventArgs.get_value();
                            }
                        </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reviewed By :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtreviewedby" CssClass="form-control " style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifreviewedbyno"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"  
                    TargetControlID="txtreviewedby" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getRecord4" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    <script type="text/javascript">
                        function getRecord4(source, eventArgs) {
                            document.getElementById('<%= hifreviewedbyno.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reviewed By 2 :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtreviewedby2" CssClass="form-control " style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifreviewedbyno2"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"  
                    TargetControlID="txtreviewedby2" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getRecord5" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    <script type="text/javascript">
                        function getRecord5(source, eventArgs) {
                            document.getElementById('<%= hifreviewedbyno2.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Approved By :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtapprovedby" CssClass="form-control " style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifapprovedbyno"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server"  
                    TargetControlID="txtapprovedby" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getRecord6" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    <script type="text/javascript">
                        function getRecord6(source, eventArgs) {
                            document.getElementById('<%= hifapprovedbyno.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Approved By 2 :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtapprovedby2" CssClass="form-control " style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifapprovedbyno2"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server"  
                    TargetControlID="txtapprovedby2" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getRecord7" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    <script type="text/javascript">
                        function getRecord7(source, eventArgs) {
                            document.getElementById('<%= hifapprovedbyno2.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">SSS No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtSSSNo" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">PH No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPHNo" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">HDMF No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtHDMFNo" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">TIN :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtTINNo" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">BIR Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtTINBranchCode" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                </div>
            </div>                     
            <br /><br />
        </div>                    
    </fieldset>
</asp:Panel>



</asp:Content>
