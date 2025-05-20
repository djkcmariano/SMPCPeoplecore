<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecEnableMenu.aspx.vb" Inherits="Secured_SecEnableMenu" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">

        <Content>
            <br />
            <div class="page-content-wrap">         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>                    
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkEnable" OnClick="lnkEnable_Click" Text="Enable" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDisable" OnClick="lnkDisable_Click" Text="Disable" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    <uc:ConfirmBox runat="server" ID="cfEnable" TargetControlID="lnkEnable" ConfirmMessage="Are you sure you want to enable the selected module(s)? Proceed?"  />
                                    <uc:ConfirmBox runat="server" ID="cfDisable" TargetControlID="lnkDisable" ConfirmMessage="Are you sure you want to disable the selected module(s)? Proceed?"  />
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
                        <div class="table-responsive-vertical" style="max-height:420px;">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="MenuMassNo">                                                                                   
                                    <Columns>
                                                      
                                        <dx:GridViewDataTextColumn FieldName="MenuMassDesc" Caption="Module Name" />
                                        <dx:GridViewDataTextColumn FieldName="IsEnabled" Caption="Enabled?" />                                                                                                                                                                                                                 
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />                                        
                                        </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                    </Columns>                            
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                            </div>
                            </div>
                        </div>

                    </div>
                   
                </div>
            </div>
        <div class="page-content-wrap">         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lbl" />
                        </div>
                        <div>    
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                            <ContentTemplate>               
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkEnableDeti" OnClick="lnkEnableDeti_Click" Text="Enable" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDisableDeti" OnClick="lnkDisableDeti_Click" Text="Disable" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExportDeti" OnClick="lnkExportDeti_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul> 
                                <uc:ConfirmBox runat="server" ID="cfEnableDeti" TargetControlID="lnkEnableDeti" ConfirmMessage="Are you sure you want to enable the selected menu? Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="cfDisableDeti" TargetControlID="lnkDisableDeti" ConfirmMessage="Are you sure you want to disable the selected menu? Proceed?"  />
                                </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExportDeti"/>
                            </Triggers>
                        </asp:UpdatePanel>
                        </div>                                                                                                   
                    </div>
                    <div class="panel-body">
                        <div class="row">
                        <div class="table-responsive-vertical" style="max-height:420px;">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="MenuNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" Enabled='<%# Bind("xIsEnabled") %>' OnClick="lnkEditDetl_Click" />                                        
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>           
                                        <dx:GridViewDataTextColumn FieldName="MenuTitle" Caption="Menu Title" />
                                        <dx:GridViewDataTextColumn FieldName="MenuStyle" Caption="Menu Style" />
                                        <dx:GridViewDataTextColumn FieldName="IsEnabled" Caption="Enabled?" />                                                                                                                                                                                                                 
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />
                            </div>
                            </div>
                        </div>

                    </div>
                   
                </div>
            </div>
        </div>    
        </Content>
    </uc:Tab>

    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel2" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel2" runat="server" CssClass="entryPopup2" style="display:none;">
        <fieldset class="form" id="fsDetl2">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveDetl" CssClass="fa fa-floppy-o submit fsDetl2 lnkSaveDetl" OnClick="lnkSaveDetl_Click"  />
            </div>
            <div  class="entryPopupDetl2 form-horizontal">
                <br />
                <br />
                <asp:HiddenField runat="server" ID="hifmenuno"/>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Menu Title :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtMenuTitle" runat="server" enabled="false" CssClass=" form-control"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Menu Style :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtMenuStyle" runat="server" enabled="false"  CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Module :</label>
                    <div class="col-md-7">
                        <asp:dropdownlist ID="cboMenuMassNo" runat="server"  CssClass="form-control" />
                    </div>
                </div>

                <br />
            </div>
        </fieldset>
    </asp:Panel>

</asp:Content>


