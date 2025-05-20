<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayJackupMatrixList.aspx.vb" Inherits="Secured_PayJackupMatrixList"%>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                       <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control"  runat="server" />   
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>               
                                    <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>                                      
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                
                                <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Selected items will be archived. Proceed?"  />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExport" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="Pk">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="Section" Caption="Section" />
                                    <dx:GridViewDataTextColumn FieldName="Position" Caption="Position" />
                                    <dx:GridViewDataTextColumn FieldName="Basic" Caption="Basic" />
                                    <dx:GridViewDataTextColumn FieldName="RA" Caption="RA" />
                                    <dx:GridViewDataTextColumn FieldName="Deminimis" Caption="Deminimis" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>


<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Pk:</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPk" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Vessel :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtVessel" runat="server" CssClass="required form-control" style ="display:inline-block;" Placeholder="Type here..." />
                     <asp:HiddenField runat="server" ID="hifVesselNo"/>
                     <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                        TargetControlID="txtVessel" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateReferential" CompletionSetCount="1" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecordVessel" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecordVessel(source, eventArgs) {
                             document.getElementById('<%= hifVesselNo.ClientID %>').value = eventArgs.get_value();
                         }
                     </script>         
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Position :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPosition" runat="server" CssClass="required form-control" style ="display:inline-block;" Placeholder="Type here..." />
                     <asp:HiddenField runat="server" ID="hifPositionNo"/>
                     <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                        TargetControlID="txtPosition" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateReferential" CompletionSetCount="2" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecordPosition" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecordPosition(source, eventArgs) {
                             document.getElementById('<%= hifPositionNo.ClientID %>').value = eventArgs.get_value();
                         }
                     </script>     
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Basic:</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtBasic" CssClass="form-control required" Placeholder="0.00" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBasic" FilterType="Numbers, Custom" ValidChars="." /> 
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">RA:</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtRA" cssclass="form-control" Placeholder="0.00" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtRA" FilterType="Numbers, Custom" ValidChars="." /> 
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Deminimis:</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtDeminimis" cssclass="form-control" Placeholder="0.00" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtDeminimis" FilterType="Numbers, Custom" ValidChars="." /> 
                </div>
            </div>
        </div>
        
    </fieldset>
</asp:Panel>
</asp:content>
