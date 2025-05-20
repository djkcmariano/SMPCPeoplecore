<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SelfTrnTakenDetlListAppr.aspx.vb" Inherits="SecuredSelf_SelfDTRLeaveApplicationList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

   <script type="text/javascript">

            function cbCheckAll_CheckedChanged(s, e) {
                grdMain.PerformCallback(s.GetChecked().toString());
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
                                    <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDisApproved" OnClick="lnkDisApproved_Click" Text="Disapprove" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDisApproved" ConfirmMessage="Are you sure you want to disapprove?"  />
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkApproved" ConfirmMessage="Are you sure you want to approve?"  />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="RecordNo"
                            OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Training No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />
                                    <dx:GridViewDataDateColumn FieldName="StartDate" Caption="Start Date" />
                                    <dx:GridViewDataDateColumn FieldName="EndDate" Caption="End Date" />
                                    <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hours(s)" />
                                    <dx:GridViewDataTextColumn FieldName="RemainingSeats" Caption="Seat(s) Left" />
                                    <dx:GridViewDataTextColumn FieldName="TrnPreStatusDesc" Caption="Enrollment Status" />
                                    <dx:GridViewDataTextColumn FieldName="TrnPostStatusDesc" Caption="Post Status" />
                                    <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" Visible="false" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Reject?" HeaderStyle-HorizontalAlign="Center" Width="2%" Visible="false" >
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkReject" CssClass="fa fa-times" Font-Size="Medium" OnClick="lnkReject_Click"  Enabled='<%# Bind("IsEnabledReject") %>' />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
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