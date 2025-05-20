<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CarJDList.aspx.vb" Inherits="Secured_CarJDList" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/JobDescription.ascx" TagName="Info" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">                
    <br />
    <div class="page-content-wrap" >         
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="lnkSearch_Click" />
                    </div>
                    <div> 
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                    
                            <ul class="panel-controls">                                    
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="JDNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("JDNo") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                                    <dx:GridViewDataTextColumn FieldName="TaskDesc" Caption="Functional Title" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Job Description" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkPreview" CssClass="fa fa-file-o" Font-Size="Medium" CommandArgument='<%# Bind("JDNo") %>' OnClick="lnkPreview_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                                    
                                    <%--<dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" HeaderStyle-HorizontalAlign="Center" />--%>
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                        </div>
                    </div>                                                           
                </div>                   
            </div>
        </div>
    </div>
    
    <uc:Info runat="server" ID="Info1" />

</asp:Content>

