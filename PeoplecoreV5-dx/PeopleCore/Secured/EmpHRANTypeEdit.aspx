<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpHRANTypeEdit.aspx.vb" Inherits="Secured_EmpHRANTypeEdit" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<script type="text/javascript">
    function disableenable(chk,index) {
        var fval = chk.checked;
        if (chk.checked && index == '1') {
            document.getElementById("ctl00_cphBody_Tab_txtIsTenureContinue").disabled = false;

            document.getElementById("ctl00_cphBody_Tab_txtIsRegularized").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtIsOrientee").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtIsProbationary").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtIsNA").disabled = false;

        } else {
            document.getElementById("ctl00_cphBody_Tab_txtIsTenureContinue").disabled = true;

            document.getElementById("ctl00_cphBody_Tab_txtIsRegularized").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtIsOrientee").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtIsProbationary").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtIsNA").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtIsTenureContinue").checked = false;
        };
            
    };
    function disableenable_behind(fval,index) {
        if (fval == 'True' && index=='1') {
            document.getElementById("ctl00_cphBody_Tab_txtIsTenureContinue").disabled = false;

            document.getElementById("ctl00_cphBody_Tab_txtIsRegularized").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtIsOrientee").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtIsProbationary").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtIsNA").disabled = false;

        } else {
            document.getElementById("ctl00_cphBody_Tab_txtIsTenureContinue").disabled = true;

            document.getElementById("ctl00_cphBody_Tab_txtIsRegularized").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtIsOrientee").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtIsProbationary").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtIsNA").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtIsTenureContinue").checked = false;
        };
    };
  

</script>
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" />
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1">
                <br />
                <br />
                <fieldset class="form" id="fsMain">
                    <div  class="form-horizontal">

                        <div class="form-group" style="display:none;">
                            <label class="col-md-3 control-label has-space">Reference No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtHRANTypeNo" runat="server" CssClass="form-control" ></asp:Textbox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Reference No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" ></asp:Textbox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Code :</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtHRANTypeCode" runat="server" CssClass="required form-control"/>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Description :</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtHRANTypeDesc" runat="server" cssclass="required form-control" />
                            </div>
                        </div>                        
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Confirmation :</label>
                            <div class="col-md-8">
                                <dx:ASPxHtmlEditor ID="txtConfirmation" runat="server"  Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Remarks :</label>
                            <div class="col-md-8">
                                <dx:ASPxHtmlEditor ID="txtRemark" runat="server"  Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Employee Movement :</label>
                            <div class="col-md-7">
                                
                                <asp:RadioButton ID="txtIsNewHired" Text="&nbsp; New Hire " GroupName="movement" onclick="disableenable(this,1);" runat="server" /><code>This will update the Hired Date.</code><br />
                                <asp:RadioButton ID="txtIsRehired" Text="&nbsp; Re-hire " GroupName="movement" onclick="disableenable(this,1);" runat="server" /><code>This will update the Re-hired Date.</code><br />
                                <asp:RadioButton ID="txtIsSeparated" Text="&nbsp; Separation " GroupName="movement" onclick="disableenable(this,0);" runat="server" /><code>This will update the Employment Status and Separated Date.</code><br />
                                <asp:RadioButton ID="txtIsBlacklisted" Text="&nbsp; Blacklist " GroupName="movement" onclick="disableenable(this,0);" runat="server" /><code>This will update the Employment Status and Blacklisted Date.</code><br />
                                <asp:RadioButton ID="txtIsUntag" Text="&nbsp; Untag " GroupName="movement" onclick="disableenable(this,1);" runat="server" /><code>This will use to untag the group.</code><br />

                                <br />
                                <asp:CheckBox ID="txtIsTenureContinue" Text="&nbsp; Continued service " GroupName="movement" runat="server" /><code></code><br />

                                <asp:CheckBox ID="txtIsUnique" Text="&nbsp; Unique Date." runat="server" Visible="false" style="display:none;" />
                                <asp:CheckBox ID="txtIsPromotion" Text="&nbsp; Promotion." runat="server" Visible="false" style="display:none;" />
                                <asp:CheckBox ID="txtIsConferment" Text="&nbsp; Conferment of Rank." runat="server" Visible="false" style="display:none;" />

                                <br />
                                <asp:RadioButton ID="txtIsRegularized" Text="&nbsp; Regularization " GroupName="prob"  runat="server" /><code>This will update the Regularization Date.</code><br />
                                <asp:RadioButton ID="txtIsOrientee" Text="&nbsp; Orientee " GroupName="prob" runat="server" /><code>This will update the Orientee Date.</code><br />
                                <asp:RadioButton ID="txtIsProbationary" Text="&nbsp; Probationary " GroupName="prob" runat="server" /><code>This will update the Probationary Date.</code><br />
                                <asp:RadioButton ID="txtIsNA" Text="&nbsp; N/A " GroupName="prob"  runat="server" /><code></code><br />
                                <br />
                                <asp:CheckBox ID="txtIsYTDForwardToPreviousPayroll" Text="&nbsp; Include YTD " GroupName="movement" runat="server" /><code>This will auto transfer of YTD to previous payroll module.</code><br />

                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">BIR Separation Reason :</label>
                            <div class="col-md-6">
                                <asp:DropdownList ID="cboBIRCategorySepaNo" runat="server" CssClass="form-control" DataMember="EBIRCategorySepa" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Employee Pay :</label>
                            <div class="col-md-6">
                                <label class="radio-inline">
                                    <asp:RadioButton ID="txtIsActivePay" GroupName="EmploymentStat" Text="Activate" runat="server" />
                                </label>
                                <label class="radio-inline">
                                    <asp:RadioButton ID="txtIsSuspendPay" GroupName="EmploymentStat" Text="Suspend" runat="server" />
                                </label>
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsSalaryAdjustment" runat="server" Text="&nbsp;Tick here if can edit or adjust the salary."></asp:Checkbox>
                            </div>
                        </div>
                        
                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsViewSalary" runat="server" Text="&nbsp;Tick here if can view the salary."></asp:Checkbox>
                            </div>
                        </div> 

                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsServiceRecord" runat="server" Text="&nbsp;Tick here if accredited in service record."></asp:Checkbox>
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsEmploymentRecord" runat="server" Text="&nbsp;Tick here if accredited in Employment Record."></asp:Checkbox>
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsConcurrent" runat="server" Text="&nbsp;Tick here if concurrent/secondment."></asp:Checkbox>                                
                            </div>
                        </div>
                        
                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsWithExitInterview" runat="server" Text="&nbsp;Tick here if with exit interview."></asp:Checkbox>
                            </div>
                        </div> 

                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsAppointmentReport" runat="server" Text="&nbsp;Tick here if has appointment report."></asp:Checkbox>
                            </div>
                        </div> 

                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsAutoPost" runat="server" Text="&nbsp;Tick here to auto post based on effectivity date."></asp:Checkbox>
                            </div>
                        </div> 

                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsOnline" runat="server" Text="&nbsp;Tick here to view online."></asp:Checkbox>
                            </div>
                        </div>
                        
                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsContractPrep" runat="server" Text="&nbsp;Tick here to list in contract preparation."></asp:Checkbox>
                            </div>
                        </div> 

                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsNot201" runat="server" Text="&nbsp;Tick here if not to effect in 201."></asp:Checkbox>                                
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Checkbox ID="txtIsAutoAbolish" runat="server" Text="&nbsp;Tick here to abolish Temporary PK upon posting of HRAN."></asp:Checkbox>                                
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-3 control-label has-space">Total no. of escalation :</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtNoOfScal" runat="server" SkinID="txtdate" CssClass="form-control"/>
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
                            <label class="col-md-3 control-label has-space">&nbsp;</label>
                            <div class="col-md-6">
                                <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                            </div>
                        </div>   

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />
                            </div>
                        </div>

                        <br />
                        <br />
                    </div>
                </fieldset>
            </asp:Panel>
        </Content>
    </uc:Tab>
</asp:Content>

