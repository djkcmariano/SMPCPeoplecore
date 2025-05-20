<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpStandardHeader_EI_Clearance.aspx.vb" Inherits="Secured_EmpStandardHeader_EI_Clearance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                           <div style="display:none;">
                            <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
                        </div>
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeEIClearanceNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeEIClearanceNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name of Employee" />
                                        <dx:GridViewDataTextColumn FieldName="IfullName" Caption="Superior" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeClearanceTypeDesc" Caption="Clearance Type" />
                                        <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />                                                                            
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>                                
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
             
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtEmployeeEIClearanceNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Name of Employee :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="1" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                            <script type="text/javascript">
                                function getRecord(source, eventArgs) {
                                    document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                }
                            </script>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Name of Superior :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtIfullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifImmediateSuperiorNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                        TargetControlID="txtIfullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateManager" CompletionSetCount="1" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                            <script type="text/javascript">
                                function getRecord1(source, eventArgs) {
                                    document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = eventArgs.get_value();
                                }
                            </script>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Clearance Type :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboEmployeeEIClearanceTypeNo" CssClass="form-control required" DataMember="EEmployeeEIClearanceType" runat="server"></asp:DropdownList>
                    </div>
                </div>
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="required form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </div>
                </div>  

                
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

