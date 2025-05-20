<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="ERProgramCostEdit.aspx.vb" Inherits="Secured_ERProgramCostEdit" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<uc:Tab runat="server" ID="Tab">
    <Header>        
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>      
    </Header>
     <Content>
        <br />
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">                                
                        <div class="col-md-10">                                           
                            <div class="form-group">

                            </div>                
                        </div>               
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul>
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdDetl" runat="server" KeyFieldName="ERProgramCostNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="Particulars" Caption="Particulars" />  
                                        <dx:GridViewDataComboBoxColumn FieldName="ERProgramCostTypeDesc" Caption="Program Cost Type" />                                                                           
                                        <dx:GridViewDataTextColumn FieldName="Cost" Caption="Cost" />           
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                                    </Columns>
                                    <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7">
                                        <PageSizeItemSettings Visible="true" Items="10, 20, 50" />        
                                    </SettingsPager>
                                    <SettingsContextMenu Enabled="true">                                
                                        <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                    </SettingsContextMenu>                                                                                            
                                    <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                                    <SettingsSearchPanel Visible="true" />     
                                    <Settings ShowFooter="true" />  
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="Cost" SummaryType="Sum" />
                                    </TotalSummary>    
                                </dx:ASPxGridView>  
                                   
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />               
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>

            <asp:Button ID="btnShow" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="Panel2" CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
            </ajaxtoolkit:ModalPopupExtender>

            <asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
                    <fieldset class="form" id="fsMain">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtERProgramCostNo" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                                </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Particulars :</label>
                            <div class="col-md-7">
                                    <asp:Textbox ID="txtParticulars" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control required" ></asp:Textbox>
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Program Cost Type :</label>
                            <div class="col-md-7">
                                    <asp:Dropdownlist ID="cboERProgramCostTypeNo" DataMember="EERProgramCostType" runat="server" CssClass="form-control required" ></asp:Dropdownlist>
                                </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Cost :</label>
                            <div class="col-md-3">
                                    <asp:Textbox ID="txtCost" runat="server" CssClass="form-control required" ></asp:Textbox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtCost" />
                             </div>
                        </div>
           
                        <br />
                        </div>
                      <!-- Footer here -->
         
                     </fieldset>
            </asp:Panel>

    </Content>
</uc:Tab>

</asp:Content> 