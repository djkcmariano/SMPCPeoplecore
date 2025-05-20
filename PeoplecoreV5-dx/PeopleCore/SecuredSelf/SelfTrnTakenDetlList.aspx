<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SelfTrnTakenDetlList.aspx.vb" Inherits="SecuredSelf_SelfDTRLeaveApplicationList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

   <script type="text/javascript">

            function cbCheckAll_CheckedChanged(s, e) {
                grdMain.PerformCallback(s.GetChecked().toString());
            }

            function grid_ContextMenu(s, e) {
                if (e.objectType == "row")
                    hiddenfield.Set('VisibleIndex', parseInt(e.index));
                pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
            }

            function OnContextMenuItemClick(sender, args) {
                if (args.item.name == "Refresh") {
                    args.processOnServer = true;
                    args.usePostBack = true;
                }
            }

    </script>

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
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkConfirm" OnClick="lnkConfirm_Click" Text="Confirm" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                    </li>
                                    <uc:ConfirmBox runat="server" ID="cfbConfirm" TargetControlID="lnkConfirm" ConfirmMessage="Are you sure you want to enroll?"  />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="RecordNo;EmployeeNo;TrnTakenNo;TrnTitleNo;IsAllowEdit"
                            OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback"
                            OnFillContextMenuItems="MyGridView_FillContextMenuItems" OnContextMenuItemVisibility="Grid_ContextMenuItemVisibility"
                            OnContextMenuItemClick="Grid_ContextMenuItemClick">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Training No." />
                                    <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />
                                    <dx:GridViewDataDateColumn FieldName="StartDate" Caption="Start Date" />
                                    <dx:GridViewDataDateColumn FieldName="EndDate" Caption="End Date" />
                                    <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hours(s)" />
                                    <dx:GridViewDataTextColumn FieldName="RemainingSeats" Caption="Seat(s) Left" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="TrnPreStatusDesc" Caption="Enrollment Status" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="TrnPostStatusDesc" Caption="Post Status" Visible="false" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Reject?" HeaderStyle-HorizontalAlign="Center" Width="2%">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkReject" CssClass="fa fa-times" Font-Size="Medium" OnClick="lnkReject_Click" Enabled='<%# Bind("IsEnabledReject") %>' />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                    <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                            </dx:ASPxCheckBox>
                                        </HeaderTemplate>
				                    </dx:GridViewCommandColumn>
                                </Columns>
                                <ClientSideEvents ContextMenuItemClick="function(s,e) { OnContextMenuItemClick(s, e); }" />
                                <ClientSideEvents ContextMenu="grid_ContextMenu" /> 
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                            <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                                <Items>
                                        <dx:MenuItem Text="Report" Name="Name">
                                            <Template>
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


    <asp:Button ID="btnShow" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup" CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style=" display:none;">
        <fieldset class="form" id="fsMain">
            <!-- Header here -->
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />
            </div>
            <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">
                
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Reason :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtReasonNotAttend" TextMode="MultiLine" Rows="4"  runat="server" CssClass="form-control required" ></asp:Textbox>
                    </div>
                </div>

            </div>
            <br />
        </fieldset>
    </asp:Panel>

    

</asp:Content>