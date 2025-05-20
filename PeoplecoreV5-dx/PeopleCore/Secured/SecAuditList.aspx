<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SecAuditList.aspx.vb" Inherits="Secured_SecAuditList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-4">
                    <label class="col-md-4 control-label has-space">Name of Table :</label><asp:Dropdownlist ID="cboTableName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="fltxtfilter_SelectedIndexChanged" />
                    <label class="col-md-4 control-label has-space">Transaction No. :</label><asp:Dropdownlist ID="cboTransNo" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkRefresh" OnClick="lnkRefresh_Click" Text="Load Data" CssClass="control-primary" /></li>                            
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" AutoGenerateColumns="true" >                                                                                   
                                                   
                        </dx:ASPxGridView>  
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                  
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>        


</asp:Content>
