<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfDTRDetlListAppr_Allocation_Appr.aspx.vb" Inherits="SecuredManager_SelfDTRDetlListAppr_Allocation" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    
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
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDisApproved" OnClick="lnkDisApproved_Click" Text="Disapprove" CssClass="control-primary" />
                                    </li>
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDisApproved" ConfirmMessage="Are you sure you want to disapprove?"  />
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkApproved" ConfirmMessage="Are you sure you want to approve?"  />
                                </ul>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExportDetl" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdDetl" runat="server" ClientInstanceName="grdDetl" SkinID="grdDX" KeyFieldName="DTRDetiLogAllocNo" Width="100%" OnCommandButtonInitialize="grdDetl_CommandButtonInitialize" OnCustomCallback="grdDetl_CustomCallback">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." HeaderStyle-Wrap="true" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" HeaderStyle-Wrap="true" />
                                    <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" HeaderStyle-Wrap="true" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="CostCenterDesc" Caption="Cost Center" HeaderStyle-Wrap="true" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DTRDate" Caption="DTR Date" HeaderStyle-Wrap="true" Visible="true" />
                                    <dx:GridViewDataTextColumn FieldName="ProjectDesc" Caption="Project" HeaderStyle-Wrap="true" Visible="true" />
                                    <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hours Worked" HeaderStyle-Wrap="true" />
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Task" HeaderStyle-Wrap="true"  PropertiesTextEdit-EncodeHtml="false"/>
                                    <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Status" HeaderStyle-Wrap="true" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
                                        <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                            </dx:ASPxCheckBox>
                                        </HeaderTemplate>
                                    </dx:GridViewCommandColumn>
                                </Columns>
                                <SettingsPager PageSize="60" />
                                <SettingsContextMenu Enabled="true">
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" />
                                </SettingsContextMenu>
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="True" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdDetl"  />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnShow" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none;">
        <fieldset class="form" id="fsMain">
            <!-- Header here -->
            <div class="cf popupheader">
                <h4>
                </h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
  
            </div>
            <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label">
                    Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtDTRDetiLogAllocNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"   Placeholder="Autonumber"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Department :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboDepartmentNo" DataMember="EDepartment" CssClass="form-control" runat="server" 
                        >
                        </asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Cost Center :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboCostCenterNo" DataMember="ECostCenter" CssClass="form-control" runat="server" 
                        >
                        </asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Project :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboProjectNo" DataMember="EProject" CssClass="form-control" runat="server" 
                        >
                        </asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Hours Work :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtHrs" SkinID="txtdate" runat="server" CssClass="form-control required"
                        ></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Task :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtRemarks" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" 
                        ></asp:Textbox>
                    </div>
                </div>
                <div class="form-group">
                    <br />
                </div>
            </div>
        </fieldset>
    </asp:Panel>
 
</asp:Content>

