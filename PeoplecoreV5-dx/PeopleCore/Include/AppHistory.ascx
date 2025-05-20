<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AppHistory.ascx.vb" Inherits="Include_AppHistory" %>

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="pInfomation" runat="server" CssClass="entryPopup" style="display:none">
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
                                        <h4><asp:Label runat="server" ID="lbl"></asp:Label></h4>                                
                                    </div>                          
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="table-responsive">
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th>MR No</th>
                                                        <th>Position</th>
                                                        <th>Department</th>
                                                        <th>Plantilla No.</th>
                                                        <th>Status</th>
                                                        <th>Applied Date</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="Repeater1" OnItemDataBound="Repeater1_ItemDataBound">                                                        
                                                        <ItemTemplate>                                                            
                                                           <tr data-toggle="collapse" data-target='<%# "#div" & Eval("MRCode") %>' class="accordion-toggle">                                                                
                                                                <td><asp:Label runat="server" ID="Label1" Text='<%# Bind("MRCode") %>' /></td>
                                                                <td><asp:Label runat="server" ID="Label2" Text='<%# Bind("PositionDesc") %>' /></td>
                                                                <td><asp:Label runat="server" ID="Label3" Text='<%# Bind("DepartmentDesc") %>' /></td>
                                                                <td><asp:Label runat="server" ID="Label4" Text='<%# Bind("PlantillaDesc") %>' /></td>
                                                                <td><asp:Label runat="server" ID="Label5" Text='<%# Bind("ApplicantStatDesc") %>' /></td>
                                                                <td><asp:Label runat="server" ID="Label6" Text='<%# Bind("DateApplied") %>' /></td>
                                                           </tr>
                                                           <tr>
                                                                <td colspan="6" style="padding:0 !important;">
                                                                    <div class="accordian-body collapse" id='<%# "div" & Eval("MRCode") %>'>
                                                                        <asp:HiddenField runat="server" ID="hfMRNo" Value='<%# Bind("MRNo") %>' />
                                                                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="MRHiredMassNo" Width="100%">
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn FieldName="tDesc" Caption="Screening Type" />
                                                                                <dx:GridViewDataTextColumn FieldName="InterviewedBy" Caption="Screened By" />
                                                                                <dx:GridViewDataTextColumn FieldName="Venue" Caption="Venue" />
                                                                                <dx:GridViewDataTextColumn FieldName="ScreeningDateFrom" Caption="From" />
                                                                                <dx:GridViewDataTextColumn FieldName="ScreeningDateTo" Caption="To" />
                                                                                <dx:GridViewDataTextColumn FieldName="ScreeningTime" Caption="Time" />
                                                                                <dx:GridViewDataTextColumn FieldName="InterviewStatDesc" Caption="Status" />
                                                                            </Columns>
                                                                            <SettingsPager Mode="ShowAllRecords" />
                                                                        </dx:ASPxGridView>
                                                                    </div> 
                                                                </td>
                                                           </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>                                                    
                                                </tbody>
                                            </table>                                                                                     
                                            <%--<dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="MRNo">                                                                                   
                                                <Columns>                                                                                               
                                                    <dx:GridViewDataTextColumn FieldName="MRCode" Caption="MR No." />                                                                                  
                                                    <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />
                                                    <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Dept/Office/Region" />
                                                    <dx:GridViewDataTextColumn FieldName="PlantillaDesc" Caption="Plantilla No." />
                                                    <dx:GridViewDataComboBoxColumn FieldName="ApplicantStatDesc" Caption="Status" />
                                                    <dx:GridViewDataDateColumn FieldName="DateApplied" Caption="Applied Date" />
                                                </Columns>
                                                <Templates>
                                                    <DetailRow>
                                                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="MRHiredMassNo" OnBeforePerformDataSelect="lnkDetails_Click">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="tDesc" Caption="Screening Type" />
                                                                <dx:GridViewDataTextColumn FieldName="InterviewedBy" Caption="Screened By" />
                                                                <dx:GridViewDataTextColumn FieldName="Venue" Caption="Venue" />
                                                                <dx:GridViewDataTextColumn FieldName="ScreeningDateFrom" Caption="From" />
                                                                <dx:GridViewDataTextColumn FieldName="ScreeningDateTo" Caption="To" />
                                                                <dx:GridViewDataTextColumn FieldName="ScreeningTime" Caption="Time" />
                                                                <dx:GridViewDataTextColumn FieldName="InterviewStatDesc" Caption="Status" />
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </DetailRow>
                                                </Templates>
                                            <SettingsDetail ShowDetailRow="true" />                            
                                            </dx:ASPxGridView>--%>
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
