<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSBillingPaid.aspx.vb" Inherits="Secured_BSBillingPaid" EnableEventValidation="false" %>

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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSNo"
                            OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback"
                            OnFillContextMenuItems="MyGridView_FillContextMenuItems">
                                <Columns>
                                    
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans No." />
                                    <dx:GridViewDataTextColumn FieldName="BSClientDesc" Visible="false" Caption="Customer Name" />
                                    <dx:GridViewDataTextColumn FieldName="ProjectDesc" Caption="Project Name" />
                                    <dx:GridViewDataTextColumn FieldName="ProjectCode" Caption="Project Code" />
                                    <dx:GridViewDataTextColumn FieldName="Details" Caption="Details" />
                                    <dx:GridViewDataTextColumn FieldName="Notes" Visible="false" Caption="Notes" />
                                    <dx:GridViewDataTextColumn FieldName="TaxRate" Caption="VAT Rate" />
                                    <dx:GridViewDataTextColumn FieldName="ASFRate" Caption="ASF Rate" />
                                    <dx:GridViewDataTextColumn FieldName="VATAmount" Caption="VAT Amount" />
                                    <dx:GridViewDataTextColumn FieldName="ASFAmount" Caption="ASF Amount" />
                                    <dx:GridViewDataTextColumn FieldName="TotalBilling" Caption="Total Billing" />
                                    <dx:GridViewDataDateColumn FieldName="Submitted Date" Visible="false" Caption="S-Date"/>
                                    <dx:GridViewDataDateColumn FieldName="DueDate" Caption="DueDate"/>

                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                    <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                            </dx:ASPxCheckBox>
                                        </HeaderTemplate>
				                    </dx:GridViewCommandColumn>

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