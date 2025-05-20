<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecMenuRef.aspx.vb" Inherits="Secured_SecMenuRef" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">

        <Content>
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
                                    <li><asp:LinkButton runat="server" ID="lnkEnable" OnClick="lnkEnable_Click" Text="Enable" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDisable" OnClick="lnkDisable_Click" Text="Disable" CssClass="control-primary" /></li>
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
                        <div class="table-responsive-vertical" style="max-height:420px;">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="MenuRefNo">                                                                                   
                                    <Columns>
                                                      
                                        <dx:GridViewDataTextColumn FieldName="MenuDesc" Caption="Reference Name" />
                                        <dx:GridViewDataTextColumn FieldName="IsEnabled" Caption="Enabled?" />                                                                                                                                                                                                                 
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
        </div>    
        </Content>
    </uc:Tab>

</asp:Content>


