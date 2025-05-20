<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CarCompAuditEdit_Analysis.aspx.vb" Inherits="Secured_CarJDEditComp" Theme="PCoreStyle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />            
            </center>            
            <asp:Label runat="server" ID="lbl" />        
        </Header>
        <Content>            
            <br />
            <div class="panel panel-default">
                <div class="panel-body">
                    <fieldset class="form" id="Fieldset1">
                        <div  class="form-horizontal">
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Filter By :</label>
                                <div class="col-md-6">
                                    <asp:DropdownList ID="cboCompFilterByNo" runat="server" CssClass="form-control required" OnSelectedIndexChanged="cboCompFilterByNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropdownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Position :</label>
                                <div class="col-md-6">
                                    <asp:DropdownList ID="cboCompPositionNo" runat="server" CssClass="form-control required" OnSelectedIndexChanged="cboCompPositionNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropdownList>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div> 


                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-4">

                            </div>
                            <div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>                    
                                        <ul class="panel-controls">                      
                                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                            
                                        </ul>                                                    
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
                                    <dx:ASPxPivotGrid ID="pvtGrid" runat="server" Width="100%" EnableCallBacks="False" OnDataBound="pvtGrid_DataBound">
                                        <OptionsFilter NativeCheckBoxes="true" />
                                        <OptionsView HorizontalScrollBarMode="Auto" />
                                    </dx:ASPxPivotGrid> 
                                    <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvtGrid" />                           
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>                        
        </Content>
    </uc:Tab>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Competency Type :</label>
                    <div class="col-md-7">                        
                        <asp:DropdownList ID="cboCompTypeNo" DataMember="ECompType" runat="server" OnSelectedIndexChanged="cboCompTypeNo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Competency :</label>
                    <div class="col-md-7">                                       
                        <asp:DropdownList ID="cboCompNo" runat="server" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Proficiency :</label>
                    <div class="col-md-7">                                       
                        <asp:DropdownList ID="cboAcquiredScaleNo" runat="server" CssClass="form-control required" DataMember="ECompScale" OnSelectedIndexChanged="cboCompScaleNo_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Indicator :</label>
                    <div class="col-md-7">                                       
                        <asp:Textbox ID="txtAcquiredAnchor" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" Enabled="false" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-7">                                       
                        <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

