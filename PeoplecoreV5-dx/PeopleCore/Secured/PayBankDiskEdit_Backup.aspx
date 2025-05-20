<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayBankDiskEdit_Backup.aspx.vb" Inherits="Secured_PayBankDiskEdit_Backup" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<asp:UpdatePanel runat="server" ID="UpdatePanel2">
<Triggers>
    <asp:PostBackTrigger ControlID="lnkCreate" />
</Triggers>
<ContentTemplate>    
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:Panel runat="server" ID="Panel1">
                    <br />
                    <fieldset class="form" id="fsMain">
                        <div  class="form-horizontal">
                            <div class="form-group" style="display:none;">
                                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                                <div class="col-md-5">
                                    <asp:Textbox ID="txtPayBankDiskNo" runat="server" CssClass="form-control" ></asp:Textbox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                                <div class="col-md-5">
                                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" ></asp:Textbox>
                                </div>
                            </div>

                            <div class="form-group" style="visibility:hidden; position:absolute;" >
                                <label class="col-md-4 control-label has-space"></label>
                                <div class="col-md-5">
                                    <asp:Checkbox ID="txtIsOnHold" runat="server" Text="&nbsp;Please click here if came from on hold payroll."></asp:Checkbox>
                                </div>
                            </div> 

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Bank Type :</label>
                                <div class="col-md-5">
                                    <asp:DropdownList ID="cboBankTypeNo" DataMember="EBankType" runat="server" CssClass="form-control required"></asp:DropdownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Payroll/Voucher No. :</label>
                                <div class="col-md-5">
                                    <asp:DropdownList ID="cboPayNo" AutoPostBack="true" runat="server" CssClass="form-control required" OnSelectedIndexChanged="cboPayNo_SelectedIndexChanged"></asp:DropdownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Start Date :</label>
                                <div class="col-md-2">
                                
                                    <asp:Textbox ID="txtStartDate" runat="server" CssClass="form-control"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy" />  
                                                                          
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                        TargetControlID="txtStartDate"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"
                                        DisplayMoney="Left"
                                        AcceptNegative="Left" />
                                
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">End Date :</label>
                                <div class="col-md-2">
                                        <asp:Textbox ID="txtEndDate" runat="server" CssClass="form-control"></asp:Textbox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="MM/dd/yyyy" />  
                                                                          
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                            TargetControlID="txtEndDate"
                                            Mask="99/99/9999"
                                            MessageValidatorTip="true"
                                            OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError"
                                            MaskType="Date"
                                            DisplayMoney="Left"
                                            AcceptNegative="Left" />

                                    </div>
                            </div>


                            <div class="form-group" >
                                <label class="col-md-4 control-label has-space">Pay Date :</label>
                                <div class="col-md-2">
                                    <asp:Textbox ID="txtPayDate" runat="server" CssClass="form-control"></asp:Textbox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtpayDate" Format="MM/dd/yyyy" />  
                                                                          
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                            TargetControlID="txtpayDate"
                                            Mask="99/99/9999"
                                            MessageValidatorTip="true"
                                            OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError"
                                            MaskType="Date"
                                            DisplayMoney="Left"
                                            AcceptNegative="Left" />
                                </div>
                            </div>
                        
                        

                            <div class="form-group" >
                                <label class="col-md-4 control-label has-space">Payroll Group :</label>
                                <div class="col-md-5">
                                    <asp:DropdownList ID="cboPayClassNo"  runat="server" CssClass="form-control"></asp:DropdownList>
                                </div>
                            </div>

                            <div class="form-group" >
                                <label class="col-md-4 control-label has-space">Total Amount :</label>
                                <div class="col-md-2">
                                    <asp:Textbox ID="txtTotalAmount" runat="server" CssClass="form-control"></asp:Textbox>
                                </div>
                            </div> 

                            <div class="form-group" style="visibility:hidden; position:absolute;" >
                                <label class="col-md-4 control-label has-space"></label>
                                <div class="col-md-5">
                                    <asp:Checkbox ID="txtIsPosted" runat="server" ></asp:Checkbox>
                                </div>
                            </div> 

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space"></label>
                                <div class="col-md-5">
                                    <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                    <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />
                                    <asp:Button runat="server"  ID="lnkCreate" CssClass="btn btn-default" CausesValidation="false" OnClick="lnkCreate_Click" Text="Create Disk"></asp:Button>
                                    <asp:Button runat="server"  ID="lnkBPI" CssClass="btn btn-default" CausesValidation="false" OnClick="lnkDiskSummary_Click" Text="BPI Summary"></asp:Button>
                                </div>
                            </div>

                            <br />
                        </div>
                    </fieldset>
                </asp:Panel>
            </div>
        </div>
    </div>
</div>       
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

