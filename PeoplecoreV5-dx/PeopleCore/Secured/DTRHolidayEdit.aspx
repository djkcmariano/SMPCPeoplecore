<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DTRHolidayEdit.aspx.vb" Inherits="Secured_DTRHolidayEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab" HeaderVisible="true">
    <Header>
        <asp:Label runat="server" ID="lbl" />        
    </Header>    
    <Content>
        <br />                         
        <asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup">
           <fieldset class="form" id="fsMain">            
             <div  class="entryPopupDetl form-horizontal">
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Reference No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Holiday/Suspension :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtHolidayDesc" runat="server"  CssClass="required form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Date :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtHolidayDate" runat="server" CssClass="required form-control" />
	                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtHolidayDate"  Format="MM/dd/yyyy" />
		                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtHolidayDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Day Type :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboDayTypeNo" DataMember="EDayTypeL" runat="server" CssClass="required form-control" />                                                                   
                   </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">No. of Hours :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtNoOfHour"  runat="server" CssClass="required number form-control" />                                                                                          
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    Start of Suspension :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtHolIn" runat="server" CssClass="form-control" Placeholder="Start of Suspension" ></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                        TargetControlID="txtHolIn" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                        ControlExtender="MaskedEditExtender4x"
                        ControlToValidate="txtHolIn"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:Checkbox ID="chkIsApplyToAll"  runat="server" Text="&nbsp; Tick to apply policy to all employees" />
                         &nbsp;<span ></span>                                              
                    </div>
                </div>
                <div class="form-group" runat="server" visible="false">
                    <label class="col-md-4 control-label">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:Checkbox ID="chkIsAM"  runat="server" Text="&nbsp; If applicable to AM only" />                                        
                    </div>
                </div>
                <div class="form-group" style="display:none";>
                <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
                </div> 
                <div class="form-group" style="visibility:hidden;position:absolute;">
                    <label class="col-md-4 control-label">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:Checkbox ID="txtIsEnabled"  runat="server" Text="&nbsp; Enabled" />                                        
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />
                    </div>
                </div>
            </div>
            <br />
            </fieldset>
        </asp:Panel>        
    </Content>
</uc:Tab>
</asp:Content>

