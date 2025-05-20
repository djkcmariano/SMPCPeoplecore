<%@ Control Language="VB" AutoEventWireup="false" CodeFile="applicationhistory.ascx.vb" Inherits="Include_applicationhistory" %>

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pInfomation" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
        </div>
        <div class="container-fluid entryPopupDetl">        
        <div class="page-content-wrap">
                
                <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div>
                                    <h4>Application History</h4>                                
                                </div>
                                <div>                                
                                </div>                           
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <mcn:DataPagerGridView ID="grd" runat="server" DataKeyNames="MRNo,ApplicantNo,EmployeeNo">
                                            <Columns>                        
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label0" Text='<%# Bind("PositionDesc") %>' ForeColor="Blue" Font-Bold="true" /><br />
                                                        <label>Date Applied :</label><asp:Label runat="server" ID="lblDateApplied" Text='<%# Bind("DateApplied") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="60%" />
                                                </asp:TemplateField>                
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkStatus" Text="View Status"  Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcn:DataPagerGridView>
                                    </div>
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
