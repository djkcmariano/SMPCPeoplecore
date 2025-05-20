<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSCompTypeList.aspx.vb" Inherits="Secured_BSCompTypeList" %>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                
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
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSCompTypeNo">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                            <dx:GridViewDataTextColumn FieldName="BSCompTypeCode" Caption="Component Type Code"/>
                            <dx:GridViewDataTextColumn FieldName="BSCompTypeDesc" Caption="Description" />                                                        
                            <dx:GridViewDataTextColumn FieldName="EntryCode" Caption="Entry Code" />
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                                       
                                                      
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>
<br />
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">
                    <div class="panel-title">
                        <asp:Label runat="server" ID="lbl" />
                    </div>                   
                </div>
                <div>                    
                    <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>                        
                    </ul>
                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                </div> 
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdMain" runat="server" KeyFieldName="BSCompTypeDetiNo" Width="100%">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click"/>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="CodeDetl" Caption="Trans No." />                            
                            <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" />                                                        
                            <dx:GridViewDataTextColumn FieldName="PayDeductTypeDesc" Caption="Deduction Type" />
                            
                                                                             
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlPopupmain" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopupMain" CancelControlID="imgClose" BackgroundCssClass="modalBackground" />

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
<fieldset class="form" id="fsMain">
    <div class="cf popupheader">
        <h4>&nbsp;</h4>
        <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
        <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
    </div>
    <!-- Body here -->
    <div  class="entryPopupDetl form-horizontal">    
        <div class="form-group">
            <label class="col-md-4 control-label">Transaction No. :</label>
            <div class="col-md-7">                
                <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" />            
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-4 control-label has-required">Code :</label>
            <div class="col-md-7">
                <asp:Textbox ID="txtBSCompTypeCode" runat="server" CssClass="required form-control" />            
            </div>
        </div> 

        <div class="form-group">
            <label class="col-md-4 control-label has-required">Description :</label>
            <div class="col-md-7">
                <asp:Textbox ID="txtBSCompTypeDesc" runat="server" CssClass="required form-control" />            
            </div>
        </div> 
        <div class="form-group">
            <label class="col-md-4 control-label has-required">Entry Code :</label>
            <div class="col-md-7">
                <asp:Textbox ID="txtEntryCode" runat="server" CssClass="required form-control" />            
            </div>
        </div> 
        
        <br />
    </div>
    <!-- Footer here -->         
    </fieldset>
</asp:Panel>

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlPopupDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" />
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2" style="display:none;">
    <fieldset class="form" id="fsDetl2">
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSaveDetl" CssClass="fa fa-floppy-o submit fsDetl2 lnkSaveDetl" OnClick="lnkSaveDetl_Click"  />   
        </div>         
        <div  class="entryPopupDetl2 form-horizontal">
        <div class="form-group">
            <label class="col-md-4 control-label">Detail No. :</label>
            <div class="col-md-7">
                <asp:TextBox ID="txtCodeDetl" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            </div>
        </div> 
        <div class="form-group">
            <label class="col-md-4 control-label has-required">Income Type :</label>
            <div class="col-md-7">
                <asp:Dropdownlist ID="cboPayIncomeTypeNo"  runat="server" DataMember="EPayIncomeType" CssClass="form-control" />            
            </div>
        </div> 
        <div class="form-group">
            <label class="col-md-4 control-label has-required">Deduction Type :</label>
            <div class="col-md-7">
                <asp:Dropdownlist ID="cboPayDeductTypeNo"  runat="server" DataMember="EPayDeductType" CssClass="form-control" />            
            </div>
        </div> 
       
        <br />
        </div>                 
    </fieldset>
</asp:Panel>

      
</asp:Content>
