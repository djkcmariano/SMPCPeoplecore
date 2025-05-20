<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpHRANList.aspx.vb" Inherits="Secured_EmpHRANList" EnableEventValidation="false" %>

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
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" /></li>                            
                                    <li><asp:LinkButton runat="server" ID="lnkDisapproved" OnClick="lnkDisapproved_Click" Text="Disapprove" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkSync" OnClick="lnkSync_Click" Text="Sync" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?"  MessageType="Post" />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HRANNo"
                            OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback"
                            OnFillContextMenuItems="MyGridView_FillContextMenuItems">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("HRANNo") %>' OnClick="lnkEdit_Click" />
                                            <%--<asp:Label runat="server" ID="lnkEdit" Text='<%# Bind("xtransno") %>'></asp:Label>--%>
                                        </DataItemTemplate>

                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="HRANCode" Caption="HRAN No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataComboBoxColumn FieldName="HRANTypeDesc" Caption="HRAN Type" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />
                                    <dx:GridViewDataDateColumn FieldName="PreparationDate" Caption="Prepared Date" />
                                    <dx:GridViewDataDateColumn FieldName="EffectiveDate" Caption="Effective Date" />
                                    <dx:GridViewDataTextColumn FieldName="Status" Caption="Status" />
                                    <dx:GridViewDataTextColumn FieldName="ApproveDisApproveby" Caption="Approved /<br />Disapproved<br />By" />
                                    <dx:GridViewDataTextColumn FieldName="appdate" Caption="Approved /<br />Disapproved<br />Date" />
                                    <dx:GridViewDataCheckColumn FieldName="IsReady" Caption="For Posting?" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="chkStatus" Caption="Checklist Status" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ServedDate" Caption="Posted Date" />
                                    <dx:GridViewDataTextColumn FieldName="ServedBy" Caption="Posted By" />
                                    <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Sector" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="LocationDesc" Caption="Location" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="ProjectDesc" Caption="Project" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PreparedBy" Caption="Prepared By" Visible="false" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                    <HeaderTemplate>
                                            Select<br />
                                            <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                            </dx:ASPxCheckBox>
                                        </HeaderTemplate>
				                    </dx:GridViewCommandColumn>

                                </Columns>
                                <ClientSideEvents ContextMenu="grid_ContextMenu" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                            <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                                <Items>
                                    <dx:MenuItem Text="Report" Name="Name">
                                        <Template>
                                                <asp:LinkButton runat="server" ID="lnkPrint" OnClick="lnkPrint_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Notice of Personnel Action" /><br />
                                                <%--<asp:LinkButton runat="server" ID="lnkAppointment" OnClick="lnkContract_Click" CssClass="control-primary" Font-Size="small"  OnPreRender="lnkPrint_PreRender" Text="Notice / Contract / Appointment" />--%>
                                        </Template>
                                    </dx:MenuItem>
                                </Items>
                                <ItemStyle Width="270px"></ItemStyle>
                            </dx:ASPxPopupMenu>
                            <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>