<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="frmContractTempList.aspx.vb" Inherits="Secured_PETemplateList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }

</script>
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">                    
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control"  runat="server" />            
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                                                
                        <ul class="panel-controls">                                                                                
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                                                                                
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Are you sure you want to archive selected item(s)?"  />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ContractTempNo"
                            OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>     
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />                           
                                    <dx:GridViewDataTextColumn FieldName="ContractTempCode" Caption="Code" />
                                    <dx:GridViewDataTextColumn FieldName="ContractTempDesc" Caption="Decription" /> 
                                    <dx:GridViewDataTextColumn FieldName="ReportTypeDesc" Caption="Report Type" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Modified" Visible="false" />
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


</asp:content>
