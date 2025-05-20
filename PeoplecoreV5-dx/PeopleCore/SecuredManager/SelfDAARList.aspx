<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfDAARList.aspx.vb" Inherits="Secured_ERDAARList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Case Close" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary" /></li>                        
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be recovered, Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkReceived" ConfirmMessage="Posted record cannot be recovered, Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="lnkCancel" ConfirmMessage="Posted record cannot be recovered, Proceed?"  />                  
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
                            <%--<dx:GridViewDataTextColumn FieldName="FullName" Caption="Issued To" />--%>          
                            <dx:GridViewDataTextColumn FieldName="DAARDate" Caption="Date Issued" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="IssuedBy" Caption="Issued By" Visible="false" />
                            <dx:GridViewBandColumn Caption="Offense" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="DAPolicyTypeDesc" Caption="DA Policy Type" />
                                    <dx:GridViewDataTextColumn FieldName="DAPolicyDesc" Caption="DA Policy" />
                                    <dx:GridViewDataTextColumn FieldName="DACaseTypeDesc" Caption="Case Type" />
                                </Columns>
                            </dx:GridViewBandColumn>
                            <%--<dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Status"/>
                            <dx:GridViewDataTextColumn FieldName="ApproveDisApproveby" Caption="Approvedy/<br/> DisApproved by" Visible="false"/>
                            <dx:GridViewDataTextColumn FieldName="Appdate" Caption="Approvedy /<br/> DisApproved Date" Visible="false"/>--%>
                            <%--<dx:GridViewDataColumn Caption="Details"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="false">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-external-link control-primary" Font-Size="Medium"/>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>--%>
                            <%--<dx:GridViewDataColumn Caption="Print"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print control-primary" Font-Size="Medium" OnClick="lnkPrint_Click" OnPreRender="lnkPrint_PreRender" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>--%>                            
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />     
                        </Columns>                     
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
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
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsMain btnSaveDetl" OnClick="btnSaveDetl_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
           <div class="form-group">
                <label class="col-md-4 control-label">Transaction no. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCodeDeti" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
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
                             document.getElementById('<%= txtPresentAddress.ClientID %>').value = Split(eventArgs.get_value(), 2);
                             document.getElementById('<%= txtPermanentAddress.ClientID %>').value = Split(eventArgs.get_value(), 3);
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
                    <asp:TextBox ID="txtRemarks" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
         </div>
         <br />
          <!-- Footer here -->
         
    </fieldset>
</asp:Panel>
 
</asp:Content>

