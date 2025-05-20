<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PEStandardRating.aspx.vb" Inherits="Secured_PEStandardRating" Theme="PCoreStyle" %>

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
                            <h4>&nbsp;</h4>                                
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEStandardRatingNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="PERatingDesc" Caption="Rating" />    
                                <dx:GridViewDataTextColumn FieldName="Anchor" Caption="Indicator" />                                                                         
                                <dx:GridViewDataTextColumn FieldName="Proficiency" Caption="Proficiency" CellStyle-HorizontalAlign="Left" /> 
                                <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order No." CellStyle-HorizontalAlign="Left" />                                                                                                                                                                                                                                                                  
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
    </Content>        
    </uc:Tab>
    <asp:Button ID="btnShowMain" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">      
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPEStandardRatingNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                  
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Rating :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboPERatingNo" DataMember="EPERating" runat="server" CssClass="form-control"></asp:DropdownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Indicator :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtAnchor" CssClass="form-control required" TextMode="Multiline" Rows="3" />
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Proficiency :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtProficiency" CssClass="form-control required" />     
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" TargetControlID="txtProficiency" ValidChars=".-" />                   
                    </div>
                </div>               
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Order No. :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtOrderLevel" CssClass="form-control" />  
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" TargetControlID="txtOrderLevel" ValidChars="."  />                       
                    </div>
                </div>
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

