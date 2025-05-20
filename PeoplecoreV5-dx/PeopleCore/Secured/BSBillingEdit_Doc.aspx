<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSBillingEdit_Doc.aspx.vb" Inherits="Secured_BSBillingEdit_Doc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab" HeaderVisible="true">
<Content>
<br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                    
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSDocNo">                                                                                   
                                    <Columns>                                        
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Record No." />
                                        <dx:GridViewDataTextColumn FieldName="DocDesc" Caption="Description" />
                                        <dx:GridViewDataTextColumn FieldName="Details" Caption="Remarks" />
                                        <dx:GridViewDataTextColumn FieldName="DocFile" Caption="File name" />                                        
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Download" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDownload" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkDownload_Click" OnPreRender="lnkDownload_PreRender" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>                                        
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>                            
                                </dx:ASPxGridView>                                
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>        
    </Content>
</uc:Tab>
<asp:UpdatePanel runat="server" ID="upUpload">
<ContentTemplate>
<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Record No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDocDesc" runat="server" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDetails" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">File Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboFileTypeNo" runat="server" DataMember="EFileType" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">File name :</label>
                <div class="col-md-7">
                    <asp:FileUpload runat="server" ID="fuDoc" Width="100%" CssClass="required" />
                </div>
            </div>                        
        </div>
        <br /><br />
    </fieldset>
</asp:Panel>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="lnkSave" />
</Triggers>
</asp:UpdatePanel>   
</asp:Content>

