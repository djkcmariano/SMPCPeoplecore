<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FileUpload.ascx.vb" Inherits="Include_FileUpload" %>
<%@ Register src="~/include/ConfirmBox.ascx" tagName="ConfirmBox" tagPrefix="uc" %>
<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pInfomation" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
        </div>
        <div class="container-fluid entryPopupDetl">        
            <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>                                                                            
                                    <div class="col-md-6">
                                        <asp:HiddenField runat="server" ID="hifNo" />
                                        <asp:FileUpload runat="server" ID="fuDoc" Width="100%" CssClass="required" />&nbsp;<asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Upload" CssClass="control-primary submit lnkAdd" />
                                    </div>
                                    <div>
                                        <ul class="panel-controls">                                        
                                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                
                                        </ul>
                                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </div>                           
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkAdd" />
                                </Triggers>
                                </asp:UpdatePanel>
                            </div>                            
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="DocNo" Width="100%">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="DocDesc" Caption="Description" Visible="false" />                                                                
                                                <dx:GridViewDataTextColumn FieldName="DocFile" Caption="File Name" />
                                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Download" HeaderStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkDownload" CssClass="fa fa-download" OnPreRender="lnkOpenFile_PreRender" Font-Size="Medium" CommandArgument='<%# Bind("DocNo") %>' OnClick="lnkDownload_Click" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                            </Columns>
                                            <SettingsBehavior AllowSort="false" AllowGroup="false" />
                                                                        
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
                </div>        
        </div>
    </fieldset>
</asp:Panel>