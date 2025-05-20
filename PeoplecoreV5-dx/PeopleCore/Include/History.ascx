<%@ Control Language="VB" AutoEventWireup="false" CodeFile="History.ascx.vb" Inherits="Include_History" %>
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
                                    </div>                                                                                                      
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>                            
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="DTRNo" Width="100%">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="ProcessedBy" Caption="Processed By"/> 
                                                <dx:GridViewDataTextColumn FieldName="TimeStart" Caption="Start Time"/> 
                                                <dx:GridViewDataTextColumn FieldName="TimeEnd" Caption="End Time"/>   
                                                <dx:GridViewDataTextColumn FieldName="ProcessingTime" Caption="Processing Time"/>  
                                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status"/>                                                        
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