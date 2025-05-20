<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSBillingEdit_Pay.aspx.vb" Inherits="Secured_BSBillingEdit_Pay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {

    });

    function getselectedvalue_none(dval) {
        $('#' + dval).css({ 'display': 'none' });
    }

    function getselectedvalue_display(dval) {
        $('#' + dval).removeAttr("style");
    }
  </script>

    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" />
            <div style="display:none;">
               
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
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BSPayNo">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BSPayNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="InvoiceNo" Caption="Invoice No." />
                                            <dx:GridViewDataTextColumn FieldName="DTRCode" Caption="DTR No." />
                                            <dx:GridViewDataTextColumn FieldName="PayrollNo" Caption="Payroll No." Visible="false" />
                                            <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                            <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Payroll Date" Visible="false" />
                                            <dx:GridViewDataTextColumn FieldName="PayPeriod" Caption="Payroll Period" Visible="false" />
                                            <dx:GridViewDataTextColumn FieldName="MonthDesc" Caption="Month" />
                                            <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" />
                                            <dx:GridViewDataTextColumn FieldName="TotalBilling" Caption="Total Billing" />
                                            <dx:GridViewDataTextColumn FieldName="PaidAmount" Caption="Paid Amount" />
                                            <dx:GridViewDataTextColumn FieldName="Balance" Caption="Balance" />

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
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
                &nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>
            <div class="entryPopupDetl form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtBSPayNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>
                
                <div class="form-group" runat="server" visible="false">
                    <label class="col-md-4 control-label has-required">
                    Payroll No. :</label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cboPayNo" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group" id="dtrno">
                    <label class="col-md-4 control-label has-space">
                    DTR No. :</label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cboDTRNo" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group" id="pyno">
                    <label class="col-md-4 control-label has-space">
                    Pakyawan Trans. No. :</label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cboPYNo" runat="server" CssClass="form-control" />
                    </div>
                </div>

                
                <br />
                <br />
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>

