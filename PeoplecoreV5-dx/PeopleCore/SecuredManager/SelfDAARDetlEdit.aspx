<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfDAARDetlEdit.aspx.vb" Inherits="SecuredManager_SelfDAARDetlEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
<Content>
<br /><br />
<div id="Div1" class="row" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="col-md-6 panel-title">
                <asp:Label ID="lblDetl" runat="server"></asp:Label>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkAddD" OnClick="lnkAddD_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                        <li><asp:LinkButton runat="server" ID="lnkDeleteD" OnClick="lnkDeleteD_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExportD" OnClick="lnkExportD_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteD" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkExportD" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="table-responsive">
                    <dx:ASPxGridView SkinID="grdDXWOSearch" ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="DAARDetlNo" Width="100%">
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEditD" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditD_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="10%" />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Width="30%" />
                            <dx:GridViewDataTextColumn FieldName="ForNTEDate" Caption="Date NTE Send" Width="20%" />  
                            <dx:GridViewDataTextColumn FieldName="NTESubmitDate" Caption="Date NTE Reply" Width="20%" />                             
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />          
                        </Columns>
                        <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7" />                                           
                    </dx:ASPxGridView>        
                    <dx:ASPxGridViewExporter ID="grdExportD" runat="server" GridViewID="grdDetl" />            
                </div>
            </div>                                                           
        </div>                   
    </div>
</div>
</Content>
</uc:Tab>

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlDetl" runat="server" 
    BackgroundCssClass="modalBackground" CancelControlID="imgClose" 
    PopupControlID="Panel2" TargetControlID="btnShowDetl">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="Panel2" runat="server" CssClass="entryPopup" >
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsMain btnSaveDetl" OnClick="btnSaveDetl_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
           <div class="form-group">
                <label class="col-md-4 control-label">Transaction no. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCodeDeti" CssClass="form-control" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_SubordinateAppr" CompletionSetCount="0" 
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
                             document.getElementById('<%= cboDepartmentNo.ClientID %>').value = Split(eventArgs.get_value(), 1);
                             document.getElementById('<%= txtPresentAddress.ClientID %>').value = Split(eventArgs.get_value(), 15);
                             document.getElementById('<%= txtPermanentAddress.ClientID %>').value = Split(eventArgs.get_value(), 16);
                         }
                            </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Department :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboDepartmentNo" runat="server" DataMember="EDepartment" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label">Present address :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPresentAddress" runat="server" Rows="3" CssClass="form-control" 
                        TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label">Permanent address :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPermanentAddress" runat="server" Rows="3" CssClass="form-control" 
                        TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsForNTE" Text="&nbsp;For NTE" Enabled="false" />
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtRemarks" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
         </div>
         <br />
          <!-- Footer here -->
         
    </fieldset>
</asp:Panel>    
</asp:Content>

