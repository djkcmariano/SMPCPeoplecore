<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRPolicy_OT.aspx.vb" Inherits="Secured_DTRRefPolicyOT" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
    <script type="text/javascript">
 
    function getselectedvalue_server(dval) {

        var fval = dval;
       
        $('#1').removeAttr("style");
        $('#2').removeAttr("style");
        if (fval == '0') {
            $('#1').css({ 'display': 'none' });
            $('#2').css({ 'display': 'none' });
        }

    }

</script>

    <br />
    <uc:Tab runat="server" ID="Tab">
        <Content>
            <div class="page-content-wrap">
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                            &nbsp;
                            </div>
                            <div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayClassNo">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                            <dx:GridViewDataTextColumn FieldName="PayClassCode" Caption="Code" />
                                            <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Description" />
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" Visible="false"/>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="page-content-wrap">
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                            &nbsp;
                            </div>
                            <div>
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                </ul>
                                <uc:ConfirmBox ID="ConfirmBox2" runat="server" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="lnkDeleteDetl" />
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="PayClassDTRRefOTNo">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                            <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Employee Class" />
                                            <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Employee Status" />
                                            <dx:GridViewDataTextColumn FieldName="MinAdvOTHrs" Caption="Min. Adv. OT" />
                                            <dx:GridViewDataTextColumn FieldName="MinOTHrs" Caption="Min. OT </br> After work" />
                                            <dx:GridViewDataTextColumn FieldName="MaxOT" Caption="Max. OT." />
                                            <dx:GridViewDataCheckColumn FieldName="IsDeductLateFrOT" Caption="Deduct late from overtime" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                            <dx:GridViewDataCheckColumn FieldName="IsDeductUnderFrOT" Caption="Deduct undertime from overtime" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                        </Columns>
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </Content>
    </uc:Tab>
    <asp:Button ID="Button2" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel2" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsDetail">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />
                &nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetail" ToolTip="Save" />
            </div>
            <div class="entryPopupDetl form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCodeDetl" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Employee Classification :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Employee Status :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <br />
                <h5>
                    <b>FRACTION</b></h5>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Overtime (in minutes) :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtFractionOT" runat="server" CssClass="form-control"  />
                    </div>
                </div>
                <br />
                <h5>
                    <b>OVERTIME ADJUSTMENT</b></h5>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Minimum Advance Overtime :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtMinAdvOTHrs" runat="server" CssClass="form-control"  />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Minimum Overtime (after work) :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtMinOTHrs" runat="server" CssClass="form-control"  />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Maximum Overtime :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtMaxOT" runat="server" CssClass="form-control"  />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Enable deduction of overtime per 
                    </label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboOTDeductTypeNo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbo_SelectedIndexChange" >
                            <asp:ListItem Value="" Text="" Selected="True" />
                            <asp:ListItem Value="1" Text="week" />
                            <asp:ListItem Value="2" Text="cut-off" />
                        </asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group" id="1" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Deduction in hrs. : 
                    </label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtOTDeductHrs" runat="server" SkinID="txtdate" CssClass="form-control"  />
                    </div>
                </div>
                <div class="form-group" id="2" style="display:none;">
                    <label class="col-md-4 control-label has-space">
                    Day type : 
                    </label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboOTDeductDTypeNo" runat="server" CssClass="form-control" >
                            <asp:ListItem Value="" Text="" Selected="True" />
                            <asp:ListItem Value="1" Text="regular" />
                            <asp:ListItem Value="2" Text="holiday" />
                        </asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsAddWorkHrs" Text="&nbsp;Check to consider rendered hrs in Maximum Overtime" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsDeductLateFrOT" Text="&nbsp;Check to deduct late from overtime" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsDeductUnderFrOT" Text="&nbsp;Check to deduct undertime from overtime" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsApplyToAll" Text="&nbsp;Check to apply to all employees" Visible="false"  />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsNoOTIfLateCurrent" Text="&nbsp;Tick to disallow overtime if tardiness and undertime is incurred" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsNoOTOnRDIfLateInRWD" Text="&nbsp;Tick to disallow overtime on a Rest Day if absent or on leave on any of the five (5) days preceding the rest day/day off." />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsNoOTOnRHIfLateInRWD" Text="&nbsp;Tick to disallow overtime on a Regular Holiday if absent or on leave on any of the five (5) days preceding the holiday." />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsNoOTOnSHIfLateInRWD" Text="&nbsp;Tick to disallow overtime on a Special Holiday if absent or on leave on any of the five (5) days preceding the holiday." />
                    </div>
                </div>
                <br />
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>
