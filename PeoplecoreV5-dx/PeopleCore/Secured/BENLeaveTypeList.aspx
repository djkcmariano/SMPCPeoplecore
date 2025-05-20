<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BENLeaveTypeList.aspx.vb" Inherits="Secured_BENLeaveTypeList" %> 

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
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
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" />
                                    </li>
                                    <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                    </li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Are you sure you want to archive selected item(s)?"  />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="LeaveTypeNo">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="LeaveTypeTransNo" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="LeaveTypeCode" Caption="Code" />
                                    <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="ChargeToLeaveType" Caption="Charge to leave type" />
                                    <dx:GridViewDataComboBoxColumn FieldName="GenderDesc" Caption="Gender" />
                                    <dx:GridViewDataCheckColumn FieldName="IsWithPay" Caption="Pay" />
                                    <dx:GridViewDataCheckColumn FieldName="IsMaintainBalance" Caption="Balance" />
                                    <dx:GridViewDataCheckColumn FieldName="IsOnline" Caption="Used Online" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MaxFiledHrs" Caption="Max. Filed Hrs" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ForfeitedTypeDesc" Caption="Forfeited Type" Visible="false" />   
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                    <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />   
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                </Columns>
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnShowDetl" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none" >
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
            <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label">
                    Reference No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtLeavetypeNo"  runat="server" Enabled="false" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Reference No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtLeavetypeTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Code :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtLeaveTypeCode" runat="server" CssClass="required form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Description :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtLeaveTypeDesc" runat="server" CssClass="required form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label  has-space">
                    Charge to leave type :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboChargeToLeaveTypeNo" DataMember="ELeavetype" runat="server" CssClass="form-control">
                        </asp:DropdownList>
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label  has-space">
                    Charge to leave type2 :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboChargeToLeaveTypeNo2"  DataMember="ELeavetype" runat="server" CssClass="form-control">
                        </asp:DropdownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Type of Leave :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboLeaveCateNo"  DataMember="ELeaveCate" runat="server" CssClass="form-control">
                        </asp:DropdownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label  has-space">
                    Gender :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboGenderNo"  DataMember="EGender" runat="server" CssClass="form-control">
                        </asp:DropdownList>
                    </div>
                </div>
                <div class="form-group" style="visibility:hidden;position:absolute;">
                    <label class="col-md-4 control-label">
                    Please check here 
                    </label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIsApplyToAll" runat="server"  />
                        <span >&nbsp;if viewable to all company</span>&nbsp;
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label  has-space">
                    </label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="chkIsonline" runat="server" Text="&nbsp; Tick if Leave Type is used online" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label  has-space">
                    </label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIswithPay" runat="server" Text="&nbsp; Tick if Leave Type is with pay"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label  has-space">
                    </label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIsMaintainBalance"  runat="server" Text="&nbsp; Tick if with maintaining balance" AutoPostBack="true" OnCheckedChanged="txtIsMaintainBalance_OnCheckedChanged"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Leave Balance :</label>
                    <div class="col-md-7">
                        <asp:RadioButton ID="txtIsRefresh"  GroupName="MaintainingBalance" Text="&nbsp; Reset balance and refresh every cut-off period" runat="server" AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" />
                        <br />
                        <asp:RadioButton ID="txtIsForefeited" GroupName="MaintainingBalance" Text="&nbsp; With forfeiture" runat="server" AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" />
                        <br />
                        <asp:RadioButton ID="txtIsAccumulated" GroupName="MaintainingBalance" Text="&nbsp; Accumulated balance" runat="server" AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" />
                        <br />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label  has-space">
                    Cut-off Period :</label>
                    <div class="col-md-4">
                        <asp:DropdownList ID="cboRefreshCutOffNo" runat="server" CssClass="form-control">
                        </asp:DropdownList>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="cboRefreshCutOffDays" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label  has-space">
                    Forfeiture Period :</label>
                    <div class="col-md-4">
                        <asp:Textbox ID="txtForfeitedCount" runat="server" CssClass="form-control" ></asp:Textbox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtForfeitedCount" />
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="cboForfeitedPeriodNo" CssClass="form-control">
                            <asp:ListItem Text="-- Select --" Value="" />
                            <asp:ListItem Text="Days" Value="1" />
                            <%--<asp:ListItem Text="Months" Value="2" />--%>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label  has-space">Forfeiture Type :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboForfeitureTypeNo"  DataMember="EForfeitureType" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cboForfeitureTypeNo_OnSelectedIndexChanged">
                        </asp:DropdownList>
                    </div>
                </div>

                <div class="form-group">
                <label class="col-md-4 control-label has-space">Specific Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSpecificDate" runat="server" SkinID="txtdate" CssClass="form-control"></asp:TextBox> 
                                                                    
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                        TargetControlID="txtSpecificDate"
                        Format="MM/dd/yyyy" />  
                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtSpecificDate"
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
                        ControlToValidate="txtSpecificDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                    
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender1"
                        TargetControlID="RangeValidator4" />                                                                           
                </div>
            </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Leave Hr/s :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtLeaveDays" runat="server" CssClass="form-control number" SkinID="txtdate" />
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Maximum Filed Hrs (Annual) :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtMaxFiledHrs" runat="server" CssClass="form-control number" SkinID="txtdate"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>
                <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">
                    Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
                </div>
                <br />
            </div>
        </fieldset>
    </asp:Panel>

</asp:Content>