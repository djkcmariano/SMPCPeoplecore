<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSClient.aspx.vb" Inherits="Secured_BSClient" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">     
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
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSClientNo">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                                                         
                            <dx:GridViewDataTextColumn FieldName="BSClientCode" Caption="Client Code"/>
                            <dx:GridViewDataTextColumn FieldName="BSClientDesc" Caption="Client Name/Company"/>
                            <dx:GridViewDataTextColumn FieldName="BSClusterDesc" Caption="Cluster" />
                            <dx:GridViewDataTextColumn FieldName="ContactName" Caption="Name of Contact"/>
                            <dx:GridViewDataTextColumn FieldName="Address" Caption="Client Address" />
                            <dx:GridViewDataTextColumn FieldName="Phone" Caption="Phone No." />
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
<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>        
        <div class="entryPopupDetl form-horizontal">                                    
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtBSClientCode" runat="server" CssClass="form-control required" />
                </div>
            </div>   
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Client/Company :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBSClientDesc" runat="server" CssClass="form-control required " TextMode="MultiLine" Rows="2" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Cluster :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboBSClusterNo" runat="server" DataMember="BBSCluster" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Contact :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtContactName" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="2" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Title :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtContactTitle" runat="server" CssClass="form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Address :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Phone No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPhone" runat="server" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">FAX No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFax" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Mobile No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtMobile" runat="server" CssClass="form-control" />
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label has-space">Email Address :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                </div>
             </div>
             <div class="form-group">
                <label class="col-md-4 control-label has-space">Website :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtWebsite" CssClass="form-control" />
                </div>
             </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Notes :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>
            </div>
        </div>                    
    </fieldset>
</asp:Panel>

</asp:Content>
