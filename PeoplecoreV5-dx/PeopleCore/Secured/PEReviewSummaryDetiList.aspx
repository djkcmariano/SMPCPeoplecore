<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PEReviewSummaryDetiList.aspx.vb" Inherits="Secured_PEReviewSummaryDetiList" %>
<%@ Register Src="~/Include/wucPEReviewSummaryMainHeader.ascx" TagName="PEReviewSummaryMainHeader" TagPrefix="uc" %>

<asp:Content id="Content2" contentplaceholderid="cphBody" runat="server">
    <uc:PEReviewSummaryMainHeader runat="server" ID="PEReviewSummaryMainHeader1" />
    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-4">
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                    </li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEReviewSummaryNo">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataTextColumn FieldName="HiredDate" Caption="Date Hired" />
                                    <dx:GridViewDataTextColumn FieldName="AveRating" Caption="Rating" />
                                    <dx:GridViewDataTextColumn FieldName="AdjectivalRating" Caption="Adjectival Rating" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Modified By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Date Modified" Visible="false" />
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
    <ajaxToolkit:ModalPopupExtender ID="mdlMain" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlPopupMain" TargetControlID="btnShowMain">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
            <!-- Header here -->
            <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />
            </div>
            <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Transaction No :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPEReviewSummaryNo" CssClass="form-control" runat="server" ReadOnly="true" ></asp:Textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Transaction No :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" CssClass="form-control" runat="server" ReadOnly="true" Placeholder="Autonumber" ></asp:Textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Name of Employee :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" onblur="ResetEmployeeNo()" Placeholder="Type here..." />
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" CompletionSetCount="0" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                        <script type="text/javascript">

                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                         }

                         function ResetEmployeeNo() {
                             if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                 document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                             }
                         } 
                     </script>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Rating :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtAveRating" runat="server" CssClass="form-control required" ></asp:Textbox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtAveRating" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Adjectival Rating :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtAdjectivalRating" runat="server" CssClass="form-control required" ></asp:Textbox>
                    </div>
                </div>
                <br />
            </div>
            <!-- Footer here -->
        </fieldset>
    </asp:Panel>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose2" PopupControlID="Panel3" TargetControlID="Button1" />
    <asp:Panel id="Panel3" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsUpload">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="cancel fa fa-times" ToolTip="Close" />
                        &nbsp;
                        <asp:LinkButton runat="server" ID="lnkSave2" CssClass="fa fa-floppy-o submit fsUpload lnkSave2" OnClick="lnkSave2_Click"  />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkSave2" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div  class="entryPopupDetl form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    File Format :</label>
                    <div class="col-md-7">
                        <code>File must be .csv(comma delimited) with following column : 
                        <br />
                        Employee No., Employee Name, Rating, Adjectival Rating</code>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Filename :</label>
                    <div class="col-md-7">
                        <asp:FileUpload runat="server" ID="fuFilename" Width="100%" CssClass="required" />
                    </div>
                </div>
                <br />
            </div>
            <div class="cf popupfooter">
            </div>
        </fieldset>
    </asp:Panel>

</asp:Content>