<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppInfoEdit.aspx.vb" Inherits="Secured_AppInfoEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
    <Header>
        <center>
            <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />                
        </center>            
        <asp:Label runat="server" ID="lbl" /> 
    </Header>
    <Content>
        <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div class="form-horizontal">            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Special Skills and Hobbies :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtHobbies" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Non-Academic Distinctions / Recognition :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtRecognition" CssClass="form-control" TextMode="MultiLine" Rows="2" />                            
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Membership in Association / Organization :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtOrganization" CssClass="form-control" TextMode="MultiLine" Rows="2" />                            
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-6 control-label has-space"><li><p style="text-align:left;">Are you related by consanguinity or affinity to the appointing or recommending authority, or to the chief of bureau or office or to the person who has immediate supervision over you in the Office, Bureau or Department where you will be apppointed,</p></li></label>                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left; text-indent:50px">a. within the third degree</p></label>               
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblNGovApplicant" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblNGovApplicant_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:Textbox runat="server" ID="txtNGovApplicantDeti" CssClass="form-control" TextMode="MultiLine" Rows="2" Enabled="false" />                    
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;text-indent:50px">b. within the fourth degree (For Local Government Unit - Career Employees) ?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsLGovApplicant" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsLGovApplicant_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <%--<div class="col-md-5" style=" visibility:hidden">
                    <label>(If Yes, Give Details)</label><br />
                    <%--<asp:TextBox runat="server" ID="txtGovApplicantDeti" Enabled="false" CssClass="form-control" /><br />
                    Relationship to Applicant :<br />
                    <asp:TextBox runat="server" ID="txtGovApplicantDeti2" Enabled="false" CssClass="form-control" /><br />
                     Dept. / Branch :<br />
                    <asp:TextBox runat="server" ID="txtGovApplicantDeti3" Enabled="false" CssClass="form-control" />
                </div>--%>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:Textbox runat="server" ID="txtGovApplicantDeti" CssClass="form-control" TextMode="MultiLine" Rows="2" Enabled="false" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">a. Have you ever been found guilty of any administrative offense?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsOffensed" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsOffensed_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:TextBox runat="server" ID="txtOffensedDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" /><br />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">b. Have you been criminally charged before any court?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsCharged" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsCharged_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:TextBox runat="server" ID="txtChargedDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" /><br />
                    <asp:TextBox runat="server" ID="txtChargedDetiDate" Enabled="false" CssClass="form-control" placeholder="Date Filed" /><br />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtChargedDetiDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtChargedDetiDate" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtChargedDetiDate" Display="Dynamic" />
                    <asp:TextBox runat="server" ID="txtChargedDetiStatus" Enabled="false" CssClass="form-control" placeholder="Status of Case/s" />
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><li><p style="text-align:left;">Have you ever been convicted of any crime or violation of any law, decree, ordinance or regulation by any court or tribunal?</p></li></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsCourt" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsCourt_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:TextBox runat="server" ID="txtCourtDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" /><br />                    
                </div>
            </div>

            <%--<div class="form-group" style="visibility:hidden">
                <label class="col-md-4 control-label has-space"><li><p style="text-align:left;">Have you ever been found guilty or been penalized for any offense or violation involving moral turpitude or carrying the penalty of disqualification to hold public office?</p></li></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsGuilty" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsGuilty_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>(If Yes, Give Details)</label><br />
                    <asp:TextBox runat="server" ID="txtGuiltyDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="4" /><br />                    
                </div>
            </div>
            <div class="form-group" style="visibility:hidden">
                <label class="col-md-4 control-label has-space"><li><p style="text-align:left;">Have you ever been suspended, discharged or forced to resign from any of your previous positions?</p></li></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsSuspended" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsSuspended_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>(If Yes, Give Details)</label><br />
                    <asp:TextBox runat="server" ID="txtSuspendedDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="4" /><br />                    
                </div>
            </div>--%>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><li><p style="text-align:left;">Have you ever been separated from the service in any of the following modes: resignation, retirement, dropped from the rolls, dismissal, termination, end of term, finished contract or phased out (abolition) in the public or private sector?</p></li></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsSector" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsSector_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:TextBox runat="server" ID="txtSectorDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" /><br />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">a. Have you ever been a candidate in a national or local election held within the last year (except Barangay election)?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsCandidate" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsCandidate_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:TextBox runat="server" ID="txtCandidateDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" /><br />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">b. Have you resigned from the government service during the three (3)-month period before the last election to promote/actively campaign for a national or local candidate?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsResigned" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsResigned_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:TextBox runat="server" ID="txtResignedDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" /><br />                    
                </div>
            </div>

            <div class="form-group" style="visibility:hidden;position:absolute;">
                <label class="col-md-4 control-label has-space"><li><p style="text-align:left;">ABCDE?</p></li></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="RadioButtonList1" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsResigned_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:TextBox runat="server" ID="TextBox1" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" /><br />                    
                </div>
            </div>

            <div class="form-group" style="visibility:hidden; position:absolute;">
                <label class="col-md-4 control-label has-space"><li><p style="text-align:left;">Have you acquired the status of an immigrant or permanent resident of another country?</p></li></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsGuilty" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsGuilty_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>If Yes, Give Details</label><br />
                    <asp:TextBox runat="server" ID="txtGuiltyDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="4" /><br />                    
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-6 control-label has-space"><li><p style="text-align:left;">Pursuant to: (a) Indigenous People's Act (RA 8371); (b) Magna Carta for Disabled Persons (RA 7277); and (c) Solo Parents Welfare Act of 2000 (RA 8972), please answer the following items:</p></li></label>
                <div class="col-md-1">
                    
                </div>
                <div class="col-md-4">
                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;text-indent:30px">a. Are you a member of any indigenous group?</p></li></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsIndigenGrp" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsIndigenGrp_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, please specify:</label><br />
                    <asp:TextBox runat="server" ID="txtIndigenGrpDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" /><br />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left; text-indent:30px">b. Are you a person with disability?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsAbled" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsAbled_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>if YES, give details:</label><br />
                    <asp:TextBox runat="server" ID="txtAbledDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" /><br />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left; text-indent:30px">c. Are you a solo parent?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsSoloParent" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsSoloParent_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>If YES, please specify ID No.:</label><br />
                    <asp:TextBox runat="server" ID="txtSoloParentDeti" Enabled="false" CssClass="form-control" placeholder="Solo Parent ID" /><br />
                    <asp:TextBox runat="server" ID="txtSoloParentDetiDate" Enabled="false" CssClass="form-control" placeholder="Solo Parent ID Expiry Date" /><br />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtSoloParentDetiDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender6" TargetControlID="txtSoloParentDetiDate" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtSoloParentDetiDate" Display="Dynamic" />
                </div>
            </div>
            <br />
           <%-- <h4  ><b>OTHER DETAILS</b></h4>--%>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">SSS No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtSSSNo" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">GSIS No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtGSISNo" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Philhealth No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtPHNo" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">HDMF No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtHDMFNo" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group" style=" display:none">
                <label class="col-md-4 control-label has-space">Tax Code :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboTaxExemptNo" DataMember="ETaxExempt" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">TIN :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtTINNo" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Community Tax Certificate No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCTCN" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Issued at :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCTCNIssuedAt" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Issued on :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCTCNIssuedOn" CssClass="form-control" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtCTCNIssuedOn" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtCTCNIssuedOn" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtCTCNIssuedOn" Display="Dynamic" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Height (meters) :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtHeight" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Weight (kilograms) :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtWeight" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Shoe Size :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboShoeNo" DataMember="EShoe" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">T-shirt Size :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboTShirtNo" DataMember="ETShirt" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">PRC / License No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtGovtIssuedID" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Passport No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtGovtIssuedIDNo" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Date / Place of Issuance :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtGovtIssueDatePlace" CssClass="form-control" />
                </div>
            </div>
            <br />
            <h5><b>Are you related to any official or employee in the BSP</b></h5>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">a. within the third degree of consanguinity (i.e. mother/father, son/daughter, brother/sister, nephew/niece, uncle/aunt, grandparent, grandchild)?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsConsanguinity" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsConsanguinity_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>If Yes, give name/s of relative/s and relationship/s</label><br />
                    <asp:TextBox runat="server" ID="txtConsanguinityDeti" Enabled="false" CssClass="form-control" placeholder="Name/s of relative/s and relationship/s" TextMode="MultiLine" Rows="2" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">b. within the third degree of affinity (i.e. mother-in-law/father-in-law, son-in-law/daughter-in-law, brother-in-law/sister-in-law, uncle-in-law, aunt-in-law, grandparent-in-law, grandchild-in-law)?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsAffinity" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsAffinity_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>If Yes, give name/s of relative/s and relationship/s</label><br />
                    <asp:TextBox runat="server" ID="txtAffinityDeti" Enabled="false" CssClass="form-control" placeholder="Name/s of relative/s and relationship/s" TextMode="MultiLine" Rows="2" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">c. Other relative/s not specified in items a and b?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsOtherRelative" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsOtherRelative_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>If Yes, give name/s of relative/s and relationship/s</label><br />
                    <asp:TextBox runat="server" ID="txtOtherRelativeDeti" Enabled="false" CssClass="form-control" placeholder="Name/s of relative/s and relationship/s" TextMode="MultiLine" Rows="2" />                    
                </div>
            </div>      
            <h5><b></b></h5>      
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">Do you have relative/s who was/were former employee/s of the Bangko Sentral ng Pilipinas?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsFormer" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsFormer_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-5">
                    <label>If Yes, give name/s, relationship, department and date of retirement</label><br />
                    <asp:TextBox runat="server" ID="txtFormerDeti" Enabled="false" CssClass="form-control" placeholder="Name/s of relative/s and relationship/s" TextMode="MultiLine" Rows="2" />                    
                </div>
            </div>

            <div class="form-group">
                <%--<label class="col-md-4 control-label has-space"><p style="text-align:left;">Have you ever been a respondent for a preliminary investigation before the Prosecutor's / Fiscal's Office or any law enforcement agency such as the police, PDEA, NBI, etc.?</p></label>--%>
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">Have you ever been a respondent in any preliminary investigation before any quasi-judicial agency, Office of the Prosecutor or any law enforcement agency?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsRespondent" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsRespondent_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>If Yes, state nature of the offense complained of</label><br />
                    <asp:TextBox runat="server" ID="txtRespondentDeti" Enabled="false" CssClass="form-control" TextMode="MultiLine" Rows="2" />                    
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-4">
                </div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-2">STATUS</div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">On-going</div>
                        <div class="col-md-2">
                            <%--<asp:CheckBox ID="chkIsOngoingA" runat="server" />--%>
                            <asp:RadioButton runat="server" ID="chkIsOngoingA" GroupName="statGroup" />
                        </div>
                        <div class="col-md-2" style="visibility:hidden;position:absolute"><asp:CheckBox ID="chkIsOngoingC" runat="server" /></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">Dismissed</div>
                        <div class="col-md-2">
                            <%--<asp:CheckBox ID="chkIsDismissedA" runat="server" />--%>
                            <asp:RadioButton runat="server" ID="chkIsDismissedA" GroupName="statGroup" />
                        </div>
                        <div class="col-md-2" style="visibility:hidden;position:absolute"><asp:CheckBox ID="chkIsDismissedC" runat="server" /></div>
                    </div>
                </div>                
            </div>
            <br />
            <%--<div class="form-group" style="visibility:hidden">
                <label class="col-md-4 control-label has-space">Acquitted&nbsp;</label>
                <div class="col-md-3">
                    <asp:CheckBox ID="chkIsAcquittedA" runat="server" />&nbsp;<span>Admin</span>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkIsAcquittedC" runat="server" />&nbsp;<span>Criminal</span>
                </div>                
            </div>--%>                                    
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">Please check any ailment/s for which you have been treated or are presently undergoing treatment</p></label>
                <div class="col-md-7">
                    <div class="col-md-6"><asp:CheckBox ID="chkIsHypertension" runat="server" />&nbsp;<span>Hypertension</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsDiabetes" runat="server" />&nbsp;<span>Diabetes</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsAcquiredHeartDisease" runat="server" />&nbsp;<span>Acquired heart disease</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsKidneyDisease" runat="server" />&nbsp;<span>Kidney disease</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsTuberculosis" runat="server" />&nbsp;<span>Pulmonary tuberculosis</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsChronicPumonary" runat="server" />&nbsp;<span>Chronic pulmonary disease</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsMalignancies" runat="server" />&nbsp;<span>Malignancies/cancer</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsAutoimmune" runat="server" />&nbsp;<span>Autoimmune disease</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsCardiovascularAccident" runat="server" />&nbsp;<span>Cardiovascular accident (CVA)</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsNeuroPsychiatric" runat="server" />&nbsp;<span>Neuro-psychiatric condition</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsHematologic" runat="server" />&nbsp;<span>Hematologic condition</span></div>
                    <div class="col-md-6"><asp:CheckBox ID="chkIsChronicLiver" runat="server" />&nbsp;<span>Chronic liver disease</span></div>
                    <div class="col-md-12"><asp:CheckBox ID="chkIsMajorcongenital" runat="server" />&nbsp;<span>Majorcongenital anomaly/deformation</span></div>
                    <div class="col-md-4"><asp:CheckBox ID="chkIsOthers" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsOthers_CheckedChange" />&nbsp;<span>Others</span></div>
                    <div class="col-md-8"><asp:TextBox runat="server" ID="txtOtherDeti" Enabled="false" CssClass="form-control" /></div>
                </div>                
            </div>
            FOR APPLICANTS TO A SECURITY GUARD POSITION ONLY
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">If considered, are you willing to be assigned / re-assigned to the Main / SPC / regional offices / branches of the Bangko Sentral ng Pilipinas in the performance of your job?</p></label>
                <div class="col-md-2">
                    <asp:RadioButtonList runat="server" ID="rblIsAssigned" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>                
            </div>
            <br />
            
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-7">
                    <asp:LinkButton runat="server" ID="lnkSave" OnClick="lnkSave_Click" Text="Save" CssClass="btn btn-md btn-primary submit lnkSave" />
                    <asp:LinkButton runat="server" ID="lnkModify" OnClick="lnkModify_Click" Text="Modify" CssClass="btn btn-md btn-primary" CausesValidation="false"  />
                </div>
            </div>
        </div>
            </fieldset>
        </asp:Panel>
    </Content>
</uc:Tab>
</asp:Content>

