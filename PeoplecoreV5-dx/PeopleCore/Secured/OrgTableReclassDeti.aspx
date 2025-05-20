<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="OrgTableReclassDeti.aspx.vb" Inherits="Secured_OrgTableReclassDeti" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">


     <script type="text/javascript">
         function cbCheckAll_CheckedChanged(s, e) {
             grdMain.PerformCallback(s.GetChecked().toString());
         }
    </script>


    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>     
        </Header>        
        <Content>
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TableOrgReclassDetiNo"
                                OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                                    <Columns>                                        
                                        <dx:GridViewDataTextColumn FieldName="PlantillaCode" Caption="Plantilla No." />
                                        <dx:GridViewDataTextColumn FieldName="FacilityDesc" Caption="Sector" />
                                        <dx:GridViewDataTextColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />
                                        <dx:GridViewDataTextColumn FieldName="GroupDesc" Caption="Group" />
                                        <dx:GridViewDataTextColumn FieldName="Fullname" Caption="Incumbent" />
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                        <HeaderTemplate>
                                                <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                                </dx:ASPxCheckBox>
                                            </HeaderTemplate>
				                        </dx:GridViewCommandColumn>
                                    </Columns>                            
                                </dx:ASPxGridView> 
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                 
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>                                      
    </Content>        
    </uc:Tab>    
</asp:Content>

