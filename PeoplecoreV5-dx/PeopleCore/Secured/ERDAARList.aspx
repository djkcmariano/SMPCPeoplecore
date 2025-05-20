<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="ERDAARList.aspx.vb" Inherits="Secured_ERDAARList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

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

<div class="row">
    <div class="panel panel-default">
        <div class="panel-heading">                                
            <div class="col-md-2">
                <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
            </div>                
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">                        
                        <li><asp:LinkButton runat="server" ID="lnkReceived" OnClick="lnkReceived_Click" Text="Receive" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary" /></li>                        
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be recovered, Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkReceived" ConfirmMessage="Are you sure you want to tag as Receive?"  />
                    <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="lnkCancel" ConfirmMessage="Are you sure you want to tag as Cancel?"  />                  
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                           
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkExport" />
                </Triggers>
                </asp:UpdatePanel>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="table-responsive">
                    <%--<dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DAARNo" OnFillContextMenuItems="MyGridView_FillContextMenuItems">--%>
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DAARNo">
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                
                            <dx:GridViewDataComboBoxColumn FieldName="DocketNo" Caption="Report No."/>
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employeee No." Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Issued To" Visible="false" />          
                            <dx:GridViewDataTextColumn FieldName="DAARDate" Caption="Date Issued"/>
                            <dx:GridViewDataTextColumn FieldName="IssuedBy" Caption="Issued By"/>
                            <dx:GridViewBandColumn Caption="Offense" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="DAPolicyTypeDesc" Caption="DA Policy Type" />
                                    <dx:GridViewDataTextColumn FieldName="DAPolicyDesc" Caption="DA Policy" />
                                    <dx:GridViewDataTextColumn FieldName="DACaseTypeDesc" Caption="Case Type" />
                                </Columns>
                            </dx:GridViewBandColumn>

                            <dx:GridViewDataTextColumn FieldName="ApproveDisApproveby" Caption="Approvedy/<br/> Disapproved by" Visible="false"/>
                            <dx:GridViewDataTextColumn FieldName="Appdate" Caption="Approvedy /<br/> DisApproved Date" Visible="false"/>

                            <%--<dx:GridViewDataColumn Caption="Details"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list control-primary" Font-Size="Medium" OnClick="lnkDetails_Click"/>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>--%>

                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />     
                        </Columns>   
                        <ClientSideEvents ContextMenuItemClick="function(s,e) { OnContextMenuItemClick(s, e); }" />
                        <ClientSideEvents ContextMenu="grid_ContextMenu" />                                                                   
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    
                    <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                        <Items>
                            <dx:MenuItem Text="Report" Name="Name">
                                <Template>
                                    <asp:LinkButton runat="server" Font-Size="Small" ID="lnkAttachment" OnClick="lnkAttachment_Click"><i class="fa fa-paperclip"></i>&nbsp;Attachment</asp:LinkButton><br />
                                    <asp:LinkButton runat="server" Font-Size="Small" ID="lnkPrint" OnClick="lnkPrint_Click" OnPreRender="lnkPrint_PreRender"><i class="fa fa-print"></i>&nbsp;Print</asp:LinkButton><br />
                                    <asp:LinkButton runat="server" Font-Size="Small" ID="lnkCopy" OnClick="lnkCopy_Click"><i class="fa fa-copy"></i>&nbsp;Copy</asp:LinkButton>
                                </Template>
                            </dx:MenuItem>
                        </Items>
                        <ItemStyle Width="200px"></ItemStyle>
                    </dx:ASPxPopupMenu>
                    <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />
                                       
                </div>
            </div>                                                                       
        </div> 
    </div>  
</div>

<div class="row" runat="server" visible="false">
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
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="DAARDetlNo" Width="100%">
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEditD" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditD_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />  
                              
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />          
                        </Columns>
                        <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7" />
                        <SettingsSearchPanel Visible="true" />                     
                    </dx:ASPxGridView>        
                    <dx:ASPxGridViewExporter ID="grdExportD" runat="server" GridViewID="grdDetl" />            
                </div>
            </div>                                                           
        </div>                   
    </div>
</div>

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
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="0" 
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
            <div class="form-group">
                <label class="col-md-4 control-label">Present address :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPresentAddress" runat="server" Rows="3" CssClass="form-control" 
                        TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Permanent address :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPermanentAddress" runat="server" Rows="3" CssClass="form-control" 
                        TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Remark :</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtRemarks" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
         </div>
         <br />
          <!-- Footer here -->
         
    </fieldset>
</asp:Panel>


<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
    BackgroundCssClass="modalBackground" CancelControlID="imgClose" 
    PopupControlID="Panel1" TargetControlID="Button1">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="entryPopup" >
        <fieldset class="form" id="Fieldset1">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4><asp:Label runat="server" ID="lblTitle" /></h4>
                <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave2" CssClass="fa fa-floppy-o submit fsMain lnkSave2" OnClick="lnkSave2_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">           

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:HiddenField runat="server" ID="hifTransNo"  />
                    <asp:TextBox runat="server" ID="txtFullname2" CssClass="form-control required" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo2"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                    TargetControlID="txtFullname2" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="0" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecordH" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">                         
                         function getRecordH(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo2.ClientID %>').value = Split(eventArgs.get_value(), 0);                             
                         }
                    </script>
                </div>
            </div>            
         <br />
          <!-- Footer here -->
         </div>
    </fieldset>
</asp:Panel>

 
</asp:Content>

