<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSBillingCollection.aspx.vb" Inherits="Secured_BSBillingCollection" EnableEventValidation="false" %>

<asp:Content runat="server" id="Content1" ContentPlaceHolderID="head">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

     <script type="text/javascript">
         function cbCheckAll_CheckedChanged(s, e) {
             grdMain.PerformCallback(s.GetChecked().toString());
         }

         function grid_ContextMenu(s, e) {
             if (e.objectType == "row")
                 hiddenfield.Set('VisibleIndex', parseInt(e.index));
             pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
         }
    </script>

    <br />
    <div class="page-content-wrap" >
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                <%--    <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>--%>
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSRegisterNo">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BSRegisterNo") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans No." />
                                    <dx:GridViewDataTextColumn FieldName="BSClientDesc"  Caption="Client Name" />
                                    <dx:GridViewDataTextColumn FieldName="BSRegisterDesc" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="ORNo" Caption="JV / O.R. Number" />
                                    <dx:GridViewDataTextColumn FieldName="ReceivedDate" Caption="Date Received" />
                                    <dx:GridViewDataTextColumn FieldName="CheckNo" Caption="Check No./Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />
                                    <dx:GridViewDataTextColumn FieldName="Details" Caption="Notes" />
                                </Columns>
                                <ClientSideEvents ContextMenu="grid_ContextMenu" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                            <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>