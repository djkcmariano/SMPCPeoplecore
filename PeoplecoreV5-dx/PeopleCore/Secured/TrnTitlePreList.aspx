<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="TrnTitlePreList.aspx.vb" Inherits="Secured_TrnTitlePreList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" /> 
        </Header>
        <Content>
        <br />
        <div class="page-content-wrap">         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                       <div class="col-md-2">
                    
                        </div>
                        <div>
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
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TrnTitlePreNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                            
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />                                                                           
                                        <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" /> 
                                        <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order Level" />                                                                                                                                                                         
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>


            <asp:Button ID="Button1" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
                <fieldset class="form" id="fsMain">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">         
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:Textbox ID="txtTrnTitlePreNo" ReadOnly="true" runat="server" CssClass="form-control" />
                            </div>
                        </div>               
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:Textbox ID="txtTrnTitlePreCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Training Title :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtTrnTitleDesc" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                                <asp:HiddenField runat="server" ID="hifTrnTitleNo" OnValueChanged="hifTrnTitleNo_ValueChanged"/>
                                <ajaxToolkit:AutoCompleteExtender ID="aceTrnTitle" runat="server"
                                TargetControlID="txtTrnTitleDesc" MinimumPrefixLength="2" EnableCaching="true"                    
                                CompletionSetCount="1" CompletionInterval="500" ServiceMethod="PopulateTrnTitle" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="GetRecord" FirstRowSelected="true" UseContextKey="true" />
                                    <script type="text/javascript">
                                        function GetRecord(source, eventArgs) {
                                            var trnTitleId = "<%= hifTrnTitleNo.ClientID %>";
                                            document.getElementById('<%= hifTrnTitleNo.ClientID %>').value = eventArgs.get_value();
                                            __doPostBack(trnTitleId, "");

                                        }
                                    </script>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Description :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label">Order Level :</label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtOrderLevel" CssClass="form-control number" />                                
                            </div>
                        </div>
                    </div>                    
                </fieldset>
            </asp:Panel>

        </Content>
    </uc:Tab>
</asp:Content>

