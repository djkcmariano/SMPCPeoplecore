<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DTRShiftRefEdit.aspx.vb" Inherits="Secured_PayLoanEdit" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
    function disableenable(chk) {

        if (chk.checked) {
            document.getElementById("ctl00_cphBody_Tab_txtAddLate").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_Tab_txtAddLate").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtAddLate").value = "";
        }

    };
    function disableenable_behind(fval) {
        if (fval == 'True') {
            document.getElementById("ctl00_cphBody_Tab_txtAddLate").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_Tab_txtAddLate").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtAddLate").value = "";
        }
    };

    function disableenableOT(chk) {

        if (chk.checked) {
            document.getElementById("ctl00_cphBody_Tab_txtOTEnd").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_Tab_txtOTEnd").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtOTEnd").value = "";
        }

    };

</script>

    
    <uc:Tab runat="server" ID="Tab">
        <Header>        
            <asp:Label runat="server" ID="lbl" />     
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div class="form-horizontal">
                    
                   <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Reference No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtShiftNo" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Reference No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Shift Code :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtShiftCode" runat="server" CssClass="required form-control" />                        
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Shift Description :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtShiftDesc" runat="server" CssClass="required form-control" />                        
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Required Hours :</label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtHrs" runat="server" CssClass="required form-control" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtHrs" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">No. of Swipes :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboNoOfSwipe" CssClass="required form-control" AutoPostBack="true" OnSelectedIndexChanged="cboNoOfSwipe_OnSelectedIndexChanged" >
                                <asp:ListItem Text="-- Select --" Value="" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="4" Value="4" />
                            </asp:DropDownList>
                        </div>
                    </div> 

                    <%--<div class="form-group">
                        <label class="col-md-3 control-label">Report Hours :</label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtrephrs" runat="server" CssClass="required form-control" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtrephrs" />
                        </div>
                    </div>--%>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">In1 :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtIn1" runat="server"  SkinID="txtdate" CssClass="required form-control"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                TargetControlID="txtIn1" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                ControlExtender="MaskedEditExtender1"
                                ControlToValidate="txtIn1"
                                IsValidEmpty="true"
                                EmptyValueMessage="Time is required"
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"

                                Display="Dynamic"
                                TooltipMessage=""/>
                           
                        </div>
                        <label class="col-md-2 control-label has-required">Out1 :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtOut1" runat="server"  SkinID="txtdate" CssClass="required form-control"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txtOut1" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                ControlExtender="MaskedEditExtender2"
                                ControlToValidate="txtOut1"
                                IsValidEmpty="true"
                                EmptyValueMessage="Time is required"
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"
                                Display="Dynamic"
                                TooltipMessage=""/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">In2 :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtIn2" runat="server"  SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                TargetControlID="txtIn2" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                                ControlExtender="MaskedEditExtender3"
                                ControlToValidate="txtIn2"
                                IsValidEmpty="true"
                                EmptyValueMessage="Time is required"
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"
                                Display="Dynamic"
                                TooltipMessage=""/>
                        </div>

                        <label class="col-md-2 control-label has-space">Out2 :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtOut2" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                TargetControlID="txtOut2" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                                ControlExtender="MaskedEditExtender4"
                                ControlToValidate="txtOut2"
                                IsValidEmpty="true"
                                EmptyValueMessage="Time is required"
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"
                                Display="Dynamic"
                                TooltipMessage="" />
                        </div>

                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Work Hours Limit (Required Hrs + OT Hrs) :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtWorkHrsLimit" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtWorkHrsLimit" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Break Hours :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtBreakHrs1" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtBreakHrs1" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Break Start :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtBreakIn" runat="server"  SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                TargetControlID="txtBreakIn" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server"
                                ControlExtender="MaskedEditExtender3"
                                ControlToValidate="txtBreakIn"
                                IsValidEmpty="true"
                                EmptyValueMessage="Time is required"
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"
                                Display="Dynamic"
                                TooltipMessage=""/>
                        </div>

                        <label class="col-md-2 control-label has-space">Break End :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtBreakOut" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server"
                                TargetControlID="txtBreakOut" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server"
                                ControlExtender="MaskedEditExtender4"
                                ControlToValidate="txtBreakOut"
                                IsValidEmpty="true"
                                EmptyValueMessage="Time is required"
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"
                                Display="Dynamic"
                                TooltipMessage="" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">ADJUSTED FLEX :</label>
                        <div class="col-md-6">
                           
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-2" style="padding-top:7px;">            
                            <asp:CheckBox runat="server" ID="txtIsAdjustedFlex" AutoPostBack="true" OnCheckedChanged="txtIsAdjustedFlex_OnSelectedIndexChanged" />
                            <span>Adjusted flex for late</span>
                        </div>
                        <label class="col-md-2 control-label has-space">Late Adjusted Hrs :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtAdjustedHrs"  CssClass="form-control" SkinID="txtdate" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtAdjustedHrs" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-2" style="padding-top:7px;">            
                            <asp:CheckBox runat="server" ID="txtIsAdjustedFlexUnder" AutoPostBack="true" />
                            <span>Adjusted flex for undertime</span>
                        </div>
                        <label class="col-md-2 control-label has-space">Undertime Adjusted Hrs :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtAdjustedHrsUnder"  CssClass="form-control" SkinID="txtdate" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtAdjustedHrsUnder" />
                        </div>
                    </div>
                    <br />
                    <br />

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="txtIsFlexiBreak" />
                            <span>Flexible break (4 swipes only).</span>
                        </div>
                    </div>
                    <div class="form-group" style="visibility:hidden;position:absolute;">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="txtIsFlex" />
                            <span>Full flexible works in a cut-off</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="txtIsDailyFlex" />
                            <span>Daily flexible works</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="txtIsNonPunching" />
                            <span>Non punching employees</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="txtIsCompress" />
                            <span>Compress employee</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="txtIsGraveyard" />
                            <span>Graveyard shift</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="txtIsOTApply" onclick="disableenableOT(this);" />
                            <span>Overtime auto apply</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">
                        &nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="chkIsWFH"  />
                            <span>Work from home</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Overtime Start :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtOTStart" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                TargetControlID="txtOTStart" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                ControlExtender="MaskedEditExtender4"
                                ControlToValidate="txtOTStart"
                                IsValidEmpty="true"
                                EmptyValueMessage="Time is required"
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"
                                Display="Dynamic"
                                TooltipMessage="" />
                        </div>

                        <label class="col-md-2 control-label has-space">Overtime End :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtOTEnd" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server"
                                TargetControlID="txtOTEnd" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                            
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator8" runat="server"
                                ControlExtender="MaskedEditExtender4"
                                ControlToValidate="txtOTEnd"
                                IsValidEmpty="true"
                                EmptyValueMessage="Time is required"
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"
                                Display="Dynamic"
                                TooltipMessage=""
                            
                                    />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Mandatory OT Hr/s :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtOTAdj" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtOTAdj" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:CheckBox runat="server" ID="txtIsAddLate" onclick="disableenable(this);" Text="&nbsp; Tick to enable paid shorten hours" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">No. of paid shorten hours :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtAddLate"  CssClass="form-control" SkinID="txtdate" Enabled="false" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtAddLate" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="txtIsApplyToAll" />
                            <span>Tick to apply to all employees</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="txtIsSatUnder" />
                            <span>Tick to enable the policy of DACON Saturday Work</span>
                        </div>
                    </div>
                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Company Name :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                            </asp:Dropdownlist>
                        </div>
                    </div> 
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">
                    &nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button runat="server"  ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                        </div>
                    </div>
                </div>                               
                <br /><br /> 
            </fieldset>
            </asp:Panel>                                                  
        </Content>
    </uc:Tab>
</asp:Content>

