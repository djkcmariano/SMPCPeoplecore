<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DTR.aspx.vb" Inherits="Secured_DTR" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/History.ascx" TagName="History" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
    function grid_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield.Set('VisibleIndex', parseInt(e.index));
        pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }
</script>

    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkPostB" OnClick="lnkPostB_Click" Text="Post For Billing" CssClass="control-primary" Visible="false" />
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" />
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" />
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkProcessDisc" OnClick="lnkProcessDisc_Click" Text="Process Discrepancy" CssClass="control-primary" Visible="false" />
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                </li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkPostB" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                            <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />
                            <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process" />
                            <uc:ConfirmBox runat="server" ID="cfblnkProcessDisc" TargetControlID="lnkProcessDisc" ConfirmMessage="Do you want to proceed?"  />
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRNo"
                        OnFillContextMenuItems="MyGridView_FillContextMenuItems">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" CommandArgument='<%# Bind("DTRNo") %>'  OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="History" CellStyle-HorizontalAlign="Center" Width="10">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkHistory" OnClick="lnkHistory_Click" CssClass="fa fa-history" CommandArgument='<%# Eval("DTRNo") & "|" & Eval("DTRCode")  %>' Font-Size="Medium" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="DTRCode" Caption="DTR No." />
                                    <dx:GridViewDataDateColumn FieldName="xStartDate" Caption="Start Date" />
                                    <dx:GridViewDataDateColumn FieldName="xEndDate" Caption="End Date" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayTypeDesc" Caption="Payroll Type" />
                                    <dx:GridViewDataCheckColumn FieldName="IsAutomated" Caption="Automated" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" PropertiesTextEdit-EncodeHtml="false" CellStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="StartTime" Caption="Start Time"/>
                                    <dx:GridViewDataTextColumn FieldName="EndTime" Caption="End Time"/>
                                    <dx:GridViewDataTextColumn FieldName="ProcessingTime" Caption="Processing <br> Time"  HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" Visible="False" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" Visible="False" />
                                    <dx:GridViewDataCheckColumn FieldName="IsPosted" Caption="Posted" Visible="False" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                    <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" Visible="False" />
                                    <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Posted Date" Visible="False" />
                                    <dx:GridViewDataDateColumn FieldName="xPayStartDate" Caption="Pay Start Date" Visible="False" />
                                    <dx:GridViewDataDateColumn FieldName="xPayEndDate" Caption="Pay End Date" Visible="False" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="DTR<br/>Summary" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkView" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkView_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Manual<br/>DTR" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkManualDTR" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkManualDTR_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="List of <br/>Employee" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEmployee" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkEmployee_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" />
                                </Columns>
                                <ClientSideEvents ContextMenu="grid_ContextMenu" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                            <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                                <Items>
                                    <dx:MenuItem Text="Report" Name="Name">
                                        <Template>
                                            <asp:LinkButton runat="server" ID="lnkPrint" OnClick="lnkPrint_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Employee No DTR and Payroll" />
                                            <br />
                                        </Template>
                                    </dx:MenuItem>
                                </Items>
                                <ItemStyle Width="250px"></ItemStyle>
                            </dx:ASPxPopupMenu>
                            <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />
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
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />
            </div>
            <!-- Body here -->
            <div class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label">
                    Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtDTRNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    DTR No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtDTRCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Payroll Type :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPayTypeNo" DataMember="EPayType" runat="server" CssClass="form-control required"
                        >
                        </asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Payroll Group :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPayClassNo" runat="server" CssClass="form-control required"
                        >
                        </asp:Dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Date From :</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtStartDate" runat="server" SkinID="txtdate" CssClass="required form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                        TargetControlID="txtStartDate"
                        Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                        <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender1"
                        TargetControlID="RangeValidator4" />
                    </div>
                    <label class="col-md-1 control-label has-space">
                    To :</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtEndDate" runat="server" SkinID="txtdate" CssClass="required form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                        TargetControlID="txtEndDate"
                        Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                        <asp:RangeValidator
                        ID="RangeValidator1"
                        runat="server"
                        ControlToValidate="txtEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender2"
                        TargetControlID="RangeValidator1" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    PAYROLL CUT-OFF DATE</label>
                    <div class="col-md-6">
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Date From :</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtPayStartDate" runat="server" SkinID="txtdate" CssClass="required form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtPayStartDate"
                        Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtPayStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                        <asp:RangeValidator
                        ID="RangeValidator2"
                        runat="server"
                        ControlToValidate="txtPayStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender3"
                        TargetControlID="RangeValidator4" />
                    </div>
                    <label class="col-md-1 control-label has-space">
                    To :</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtPayEndDate" runat="server" SkinID="txtdate" CssClass="required form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtPayEndDate"
                        Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtPayEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                        <asp:RangeValidator
                        ID="RangeValidator5"
                        runat="server"
                        ControlToValidate="txtPayEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender4"
                        TargetControlID="RangeValidator5" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                    </label>
                    <div class="col-md-6">
                        <label class="check">
                        <asp:Checkbox ID="txtIsautomated" Visible="True"  runat="server" CssClass="icheckbox">
                        </asp:Checkbox>
                        Please click here if you want automate the processing.
                        </label>
                    </div>
                </div>
            </div>
        </fieldset>
    </asp:Panel>
    <uc:History runat="server" ID="History" />
</asp:Content>

