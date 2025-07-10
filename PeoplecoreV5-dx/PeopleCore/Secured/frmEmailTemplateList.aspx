<%@ Page Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="frmEmailTemplateList.aspx.vb" Inherits="Secured_frmEmailTemplateList" %>


<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">


    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <%--<li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                             <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>--%>
                            </ul>
                           <%-- <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" />--%>
                        </ContentTemplate>
         <%--               <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmailTempNo">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="EmailTempCode" Caption="Code" />
                                    <dx:GridViewDataTextColumn FieldName="EmailTempDesc" Caption="Decription" />
                                    <dx:GridViewDataTextColumn FieldName="EmailTempSubj" Caption="Subject" />
                                    <dx:GridViewDataTextColumn FieldName="EmailTriggerDesc" Caption="Trigger" />
                                    <dx:GridViewDataTextColumn FieldName="EmailAddress" Caption="Email Recipients" Width="25%" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />
                                    <dx:GridViewDataCheckColumn FieldName="IsEnabledMain" Caption="Enabled" ReadOnly="true" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<br /><br />
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-7">
                    <h4 class="panel-title">Reference No.: <asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                            <%--<li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>--%>
                            <%--<li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>--%>
                        </ul>
                        <%--<uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />--%>
                    </ContentTemplate>
<%--                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" Width="100%" KeyFieldName="EmailRecipientNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="EmailRecipientCode" Caption="Detail No." />
                            <dx:GridViewDataComboBoxColumn FieldName="EmailRecipientDesc" Caption="Description" />
                            <dx:GridViewDataComboBoxColumn FieldName="OrganizationLDesc" Caption="Organization" />
                            <dx:GridViewDataComboBoxColumn FieldName="FullName" Caption="Name" />
                            <dx:GridViewDataComboBoxColumn FieldName="Email" Caption="Email" />
                            <dx:GridViewDataCheckColumn FieldName="IsStatic" Caption="Static" ReadOnly="true" />
                            <dx:GridViewDataCheckColumn FieldName="IsEnabled" Caption="Enabled" ReadOnly="true" />
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" Visible="false"/> 
                        </Columns>                            
                    </dx:ASPxGridView>
                    <%--<dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />      --%>                                  
                </div>                            
            </div>
        </div>
    </div>
</div>


    <asp:Button ID="btnShow" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopupMain"
        CancelControlID="imgClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlPopupMain" runat="server" CssClass="entryPopup" Style="display: none">
        <fieldset class="form" id="fsMain">
            <!-- Header here -->
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:LinkButton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click" />
            </div>
            <!-- Body here -->
            <div class="entryPopupMain form-horizontal">
                <br />
                <div class="form-group">
                    <label class="col-md-4 control-label">Reference no. :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Code :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtEmailTempCode" runat="server" CssClass="required form-control" />

                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtEmailTempDesc" runat="server" CssClass="required form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Trigger :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboEmailTriggerNo" runat="server" CssClass="number form-control" >
                        </asp:Dropdownlist>
                    </div>
                </div>

                <div class="form-group" style="display: none;">
                    <label class="col-md-4 control-label">Email to :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtEmailAddress" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>


                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Subject :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtEmailTempSubj" runat="server" CssClass="required form-control"></asp:TextBox>

                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Body :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtEmailTempMsg" TextMode="MultiLine" Rows="10" runat="server" CssClass="required form-control"></asp:TextBox>
                        
                        <br />
                        <asp:LinkButton runat="server" ID="lnkViewParam" OnClick="lnkViewParam_Click" CausesValidation="false" Text="Click here to view parameters." />

                    </div>
                </div><div class="form-group">
                        <label class="col-md-4 control-label has-space">&nbsp;</label>
                        <div class="col-md-7">
                            <asp:CheckBox runat="server" ID="chkIsEnabledMain" Text="&nbsp;Enabled" />
                        </div>
                    </div>
                <br />
            </div>
            <!-- Footer here -->

        </fieldset>
    </asp:Panel>

    <asp:Button ID="btnShowD" runat="server" style="display:none" />
        <ajaxtoolkit:ModalPopupExtender ID="mdlShowDetl" runat="server" TargetControlID="btnShowD" PopupControlID="pnlPopupDetl"
            CancelControlID="imgClosed" BackgroundCssClass="modalBackground" >
        </ajaxtoolkit:ModalPopupExtender>
        
        <asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsDetl">
                <!-- Header here -->
                 <div class="cf popupheader">
                        <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                        <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsDetl btnSaveDetl" OnClick="btnSaveDetl_Click"  />   
                 </div>
                 <!-- Body here -->
                 <div  class="entryPopupDetl form-horizontal">
                    <div class="form-group" style="display:none;">
                        <label class="col-md-4 control-label">Reference No. :</label>
                        <div class="col-md-7">
                            <asp:Textbox ID="txtEmailRecipientNo" runat="server" CssClass="form-control"></asp:Textbox>
                        </div>
                    </div>
        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Detail No. :</label>
                        <div class="col-md-7">
                            <asp:Textbox ID="txtEmailRecipientCode" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber"></asp:Textbox>
                         </div>
                    </div>
        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Description :</label>
                        <div class="col-md-7">
                            <asp:Textbox ID="txtEmailRecipientDesc" runat="server" CssClass="required form-control"></asp:Textbox>
                         </div>
                    </div>
                     
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Receiver Org :</label>
                        <div class="col-md-7">
                            <asp:DropDownList ID="cboOrganizationLNo" runat="server" DataMember = "EOrganizationL" CssClass="required form-control" OnSelectedIndexChanged="cboEOrganizationL_SelectedIndexChanged" AutoPostBack ="true"  />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Employee Name :</label>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtEmployeeName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." AutoPostBack="true" /> 
                            <asp:HiddenField runat="server" ID="hifEmployeeNo" OnValueChanged="hifEmployeeNo_ValueChanged"/>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                            TargetControlID="txtEmployeeName" MinimumPrefixLength="2" 
                            CompletionInterval="250" ServiceMethod="PopulateEmployee_Universal" CompletionSetCount="1" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getRecord1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                             <script type="text/javascript">
                                 function getRecord1(source, eventArgs) {
                                     document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                 }
                             </script>
                            
                        </div>
                    </div>
        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Email :</label>
                        <div class="col-md-7">
                            <asp:Textbox ID="txtEmail" runat="server" CssClass="required form-control"></asp:Textbox>
                         </div>
                    </div>
                     
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">&nbsp;</label>
                        <div class="col-md-7">
                            <asp:CheckBox runat="server" ID="chkIsStatic" Text="&nbsp;Static" OnCheckedChanged="chkIsStatic_CheckedChanged" AutoPostBack="true" />
                        </div>
                    </div>
                     
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">&nbsp;</label>
                        <div class="col-md-7">
                            <asp:CheckBox runat="server" ID="chkIsEnabled" Text="&nbsp;Enabled" />
                        </div>
                    </div>
                    <br />
                    </div>
                  <!-- Footer here -->
                 
                 </fieldset>
        </asp:Panel>
</asp:Content>

