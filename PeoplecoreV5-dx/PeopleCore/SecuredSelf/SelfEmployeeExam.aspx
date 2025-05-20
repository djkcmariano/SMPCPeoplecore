<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelfEmployeeExam.aspx.vb" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Secured_EmpExamList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:TabSelf runat="server" ID="TabSelf">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
            </center>
            <asp:Label runat="server" ID="lbl" />
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
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeExamNo">
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeExamNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="ExamTypeDesc" Caption="Examination Title" />
                                            <dx:GridViewDataTextColumn FieldName="ScoreRating" Caption="Rating" />
                                            <dx:GridViewDataTextColumn FieldName="xDateTaken" Caption="Date of Examination / Conferment" />
                                            <dx:GridViewDataTextColumn FieldName="xDateExpired" Caption="Expiry Date" />
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        </Columns>
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </Content>
    </uc:TabSelf>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
        <fieldset class="form" id="fsMain">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
                &nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>
            <div class="entryPopupDetl form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder= "Autonumber"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required" runat="server" id="lblOtherExam">Examination Title :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboExamTypeNo" DataMember="EExamType" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboExamTypeNo_SelectedIndexChanged" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"><asp:CheckBox runat="server" ID="txtIsOtherExam" AutoPostBack="true" OnCheckedChanged="txtIsOtherExam_CheckedChanged" />  If others (please specify) :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtOtherExam" CssClass="form-control" TextMode="MultiLine" Rows="2" Placeholder="Examination Title" />                    
                    </div>
                </div>  
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Rating (if applicable) :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtScoreRating" CssClass="form-control number" Visible="false" />
                        <asp:TextBox runat="server" ID="txtScoreRatingDesc" CssClass="form-control"  />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Date of Examination / Conferment :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtDateTaken" CssClass="form-control required" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateTaken" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateTaken" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateTaken" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Place of Examination / Conferment :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtVenue" TextMode="MultiLine" CssClass="form-control" Rows="2" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space" runat="server" id="lblLicense">Certificate / License No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtLicenseNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Date Released :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtDateReleased" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtDateReleased" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtDateReleased" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateReleased" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space" runat="server" id="lblExpiry">Expiry Date (if any) :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtDateExpired" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtDateExpired" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="txtDateExpired" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateExpired" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" CssClass="form-control" Rows="2" />
                    </div>
                </div>
            </div>
        </fieldset>
    </asp:Panel>

</asp:Content>