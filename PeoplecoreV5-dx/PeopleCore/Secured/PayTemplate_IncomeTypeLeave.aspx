<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayTemplate_IncomeTypeLeave.aspx.vb" Inherits="Secured_PayTemplate_IncomeTypeLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
    <uc:Tab runat="server" menustyle="TabRef" ID="Tab">
        <Content>
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
                                </ul>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="LeaveTypeNo">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                            <dx:GridViewDataTextColumn FieldName="LeaveTypeCode" Caption="Code" />
                                            <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" />
                                            
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
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
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
                        <asp:Textbox ID="txtLeaveTypeNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Code :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtLeaveTypeCode" runat="server" Enabled="false" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtLeaveTypeDesc" runat="server" Enabled="false" CssClass="form-control required" />
                    </div>
                </div>
             
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Income Type :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayIncomeTypeNo" DataMember="EPayIncomeType" runat="server" CssClass="form-control" />
                    </div>
                </div>
                
                <br />
                <br />
                <br />
                <br />
            </div>
        </fieldset>
    </asp:Panel>

</asp:Content>

