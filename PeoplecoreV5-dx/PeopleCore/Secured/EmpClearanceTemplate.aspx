<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpClearanceTemplate.aspx.vb" Inherits="Secured_EmpClearanceTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
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
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="cboTabNo_SelectedIndexChanged" CssClass="form-control" runat="server" />
                </div>
                <div>                                     
                    <ul class="panel-controls">                                                                            
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>         
                                    <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false"/></li>
                    </ul>           
                    <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Selected items will be archived. Proceed?"  />
                    <%--<uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                            --%>
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ClearanceTemplateNo" Width="100%"
                            OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ClearanceTemplateNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                <dx:GridViewDataTextColumn FieldName="ClearanceTemplateCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="ClearanceTemplateDesc" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="ClearanceTypeDesc" Caption="Clearance Type" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />  
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

</asp:Content>

