<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="OrgSection.aspx.vb" Inherits="Secured_OrgSection" %>

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
                    <uc:Filter runat="server" ID="Filter1" />                                
                </div>                           
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging">
                        <Columns>                                                                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEdit_Click" CommandArgument='<%# Bind("SectionNo") %>' />                                           
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                        </asp:TemplateField>                             
                        <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Reference No.">
                            <HeaderStyle Width="7%" HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>                                                                      
                        <asp:BoundField DataField="SectionCode" HeaderText="Code" SortExpression="SectionCode">                            
                            <HeaderStyle Width="7%" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SectionDesc" HeaderText="Description" SortExpression="SectionDesc">                             
                            <HeaderStyle Width="15%"  HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SectionACode" HeaderText="Account Code" SortExpression="SectionACode">                             
                            <HeaderStyle Width="7%"  HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>                       
                        <asp:BoundField DataField="FullName" HeaderText="Head / R.M." SortExpression="FullName">
                            <HeaderStyle Width="15%"  HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FullName1" HeaderText="Payroll Manager" SortExpression="FullName1">                            
                            <HeaderStyle Width="15%"  HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FullName2" HeaderText="Scheduler" SortExpression="FullName2">
                            <HeaderStyle Width="15%"  HorizontalAlign  ="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FullName3" HeaderText="Personnel Manager" SortExpression="FullName3">
                            <HeaderStyle Width="15%"  HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                        </asp:TemplateField>
                    </Columns>                                
                    </mcn:DataPagerGridView>
                    </div>
                </div>                    
                <div class="row">
                    <div class="col-md-4">                                
                        <asp:DataPager ID="dpMain" runat="server" PagedControlID="grdMain" PageSize="10">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/images/arrow_first.png" PreviousPageImageUrl="~/images/arrow_previous.png" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                    <asp:TemplatePagerField>
                                        <PagerTemplate>Page
                                            <asp:Label ID="CurrentPageLabel" runat="server" Text="<%# IIf(Container.TotalRowCount>0,  (Container.StartRowIndex / Container.PageSize) + 1 , 0) %>" /> of
                                            <asp:Label ID="TotalPagesLabel" runat="server" Text="<%# Math.Ceiling (System.Convert.ToDouble(Container.TotalRowCount) / Container.PageSize) %>" /> (
                                            <asp:Label ID="TotalItemsLabel" runat="server" Text="<%# Container.TotalRowCount%>" /> records )
                                        </PagerTemplate>
                                    </asp:TemplatePagerField>
                                <asp:NextPreviousPagerField ButtonType="Image" LastPageImageUrl="~/images/arrow_last.png" NextPageImageUrl="~/images/arrow_next.png" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />                              
                            </Fields>
                        </asp:DataPager>
                    </div>
                    <div class="col-md-6 col-md-offset-2">                                
                        <div class="pull-right">
                            <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDelete_Click" />
                        </div>
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="btnDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" />
                    </div>
                </div>                       
            </div>                   
        </div>
    </div>
</div>
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
                    <asp:Textbox ID="txtSectionCode" runat="server" CssClass="required form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtSectionDesc" runat="server" CssClass="required form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Account Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtSectionACode" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Head / Restaurant Manager :</label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateManager" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                        function getRecord(source, eventArgs) {
                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>
                </div>
                <div class="col-md-2">
                    <asp:CheckBox runat="server" ID="chkIsBranch" Text="&nbsp;For Branch" />
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Payroll Manager :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName1" CssClass="form-control" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifemployeeno1"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                    TargetControlID="txtFullName1" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="0"
                    OnClientItemSelected="getRecord1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecord1(source, eventArgs) {
                             document.getElementById('<%= hifemployeeno1.ClientID %>').value = eventArgs.get_value();
                         }
                            </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Scheduler :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName2" CssClass="form-control" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifemployeeno2"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                    TargetControlID="txtFullName2" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="0"
                    OnClientItemSelected="getRecord2" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                        function getRecord2(source, eventArgs) {
                            document.getElementById('<%= hifemployeeno2.ClientID %>').value = eventArgs.get_value();
                        }
                     </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Personnel Manager :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName3" CssClass="form-control" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifemployeeno3"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"  
                    TargetControlID="txtFullname3" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="0"
                    OnClientItemSelected="getRecord3" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecord3(source, eventArgs) {
                             document.getElementById('<%= hifemployeeno3.ClientID %>').value = eventArgs.get_value();
                         }
                            </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Training Manager :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName4" CssClass="form-control" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifemployeeno4"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"  
                    TargetControlID="txtFullname4" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="0"
                    OnClientItemSelected="getRecord3" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecord3(source, eventArgs) {
                             document.getElementById('<%= hifemployeeno3.ClientID %>').value = eventArgs.get_value();
                         }
                    </script>
                </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
                </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archived" />
                </div>
            </div>
        </div>                    
    </fieldset>
</asp:Panel>

<%--            
   
       
<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlpopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">

           
            
            
            <div class="form-group">
                    <label class="col-md-4 control-label">Personnel Manager :</label>
                    <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName3" CssClass="form-control" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifemployeeno3"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"  
                    TargetControlID="txtFullname3" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="0"
                    OnClientItemSelected="getRecord3" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecord3(source, eventArgs) {
                             document.getElementById('<%= hifemployeeno3.ClientID %>').value = eventArgs.get_value();
                         }
                            </script>
                </div>
            </div>

            <div class="form-group">
                    <label class="col-md-4 control-label">Training Manager :</label>
                    <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName4" CssClass="form-control" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifemployeeno4"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"  
                    TargetControlID="txtFullname4" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="0"
                    OnClientItemSelected="getRecord3" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecord3(source, eventArgs) {
                             document.getElementById('<%= hifemployeeno3.ClientID %>').value = eventArgs.get_value();
                         }
                            </script>
                </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label">Please check here</label>
                    <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsArchived" />
                    <span> to archive data.</span>
                </div>
            </div>
            <div class="form-group" style="visibility:hidden;">
                    <label class="col-md-4 control-label" style="visibility:hidden;">Please check here</label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIsApplyToAll" runat="server" />  &nbsp;
                        <span >if visible to all company. </span>
                    </div>
                </div>
                <br />
                </div>
              <!-- Footer here -->
                       
         </fieldset>
</asp:Panel>--%>
  
</asp:Content>
