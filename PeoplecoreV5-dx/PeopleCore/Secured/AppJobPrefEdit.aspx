<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppJobPrefEdit.aspx.vb" Inherits="Secured_AppJobPrefEdit" %>
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
            <%--<h4><b>Preferred Location</b></h4>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-required">First Choice :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboDepNo1" DataMember="EDepartment" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Second Choice :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboDepNo2" DataMember="EDepartment" CssClass="form-control" />
                </div>
            </div>                       
            <br />--%>
            <h4><b>Position of Interest</b></h4>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">First Choice :</label>
                <div class="col-md-7">
                    <%--<asp:DropDownList runat="server" ID="cboPositionNo" CssClass="form-control required" />--%>
                    <asp:TextBox runat="server" ID="txtPosition1" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Second Choice :</label>
                <div class="col-md-7">
                    <%--<asp:DropDownList runat="server" ID="cboPositionNo1" CssClass="form-control" />--%>
                    <asp:TextBox runat="server" ID="txtPosition2" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Major individual Accomplishment relevant to the job being applied for :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtIndiAccomplishJob" TextMode="MultiLine" Rows="4" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Can start work on (date of availability) :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtAboutToStart" CssClass="form-control" SkinID="txtdate" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtAboutToStart" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtAboutToStart" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtAboutToStart" Display="Dynamic" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">What is the minimum salary you are willing to accept? :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtSalaryDesired" CssClass="form-control number" SkinID="txtnumber" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Willing to relocate :</label>
                <div class="col-md-7">
                    <asp:RadioButtonList runat="server" ID="rblIsWR" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Willing to travel :</label>
                <div class="col-md-7">
                    <asp:RadioButtonList runat="server" ID="rblIsWT" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="Frequently&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="2" Text="Occasionally&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Willing to accept contractual employment :</label>
                <div class="col-md-7">
                    <asp:RadioButtonList runat="server" ID="rblIsACE" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>
                </div>
            </div>
            <%--<div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space"><p style="text-align:left;">Have you taken the  pre-employment exam or qualifying test for the Management Associates Program in the past ?</p></label>
                <div class="col-md-2" style="display:none;">
                    <asp:RadioButtonList runat="server" ID="rblIs1" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIs1_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>                    
                </div>
                <div class="col-md-5">
                    <label>(If Yes, Give Details)</label><br />
                    <asp:Textbox runat="server" ID="txtExamDate" CssClass="form-control" placeholder="Date" /><br />
                    <asp:Textbox runat="server" ID="txtVenue" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Venue" /><br />
                    <asp:DropDownList runat="server" ID="cboExamStatNo" DataMember="EExamStat" CssClass="form-control" placeholder="Status" />
                </div>                
            </div>--%>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">How vacancy was known :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboVacancySourceNo" DataMember="EVacancySource" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Name of referror (if any) :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtReferrorName" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Have you taken BSP pre-employment test before?</label>
                <div class="col-md-7">
                    <asp:RadioButtonList runat="server" ID="rblIsPreEmpTest" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIsPreEmpTest_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Yes&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem Value="0" Text="No&nbsp;&nbsp;&nbsp;" Selected="True" />
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    If yes, indicate the date?<br />
                    <asp:TextBox runat="server" ID="txtPreEmpTestDate" CssClass="form-control" SkinID="txtdate" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtPreEmpTestDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtPreEmpTestDate" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtPreEmpTestDate" Display="Dynamic" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    For what position and department?<br />
                    <asp:TextBox runat="server" ID="txtPreEmpTestDeptPos" CssClass="form-control" />
                </div>
            </div>
                        
            <div class="form-group"> 
                <label class="col-md-2"></label>                               
                <div class="col-md-8">                    
                    <asp:Label runat="server" ID="lblDisc" CssClass="form-control" Width="100%" Height="100%" style=" text-align:justify" />
                </div>
            </div>
            <%--<div class="form-group">                
                <div class="col-md-11">
                    <p style="text-align:justify;">
                    I declare that the information given are true and correct to the best of my knowledge and belief. 
                    It is understood that any false representation that I have made shall be a ground for disqualifying 
                    me for employment and will subject me to an administrative case and / or revocation of my appointment 
                    as employee of the Bangko Sentral ng Pilipinas. I further waive my  right of confidentiality  on information 
                    that  pertains to request for BSP Watchlist  clearance in compliance to M.B. 
                    Resolution No. 1487A prior to employment</p>.<br /><br />
                    <%--I declare under oath that this form has been accomplished by me, and is a true, correct, accurate and complete statement pursuant to the provisions of pertinent laws, rules and regulations of the Republic of the Philippines.<br /><br />
                    I also authorize the agency head or his/her authorized representative to verify / validate the contents stated herein and to conduct investigation on my personal background. Any false information contained herein may serve as grounds for cancellation of my application or dismissal in case I am employed.<br /><br />
                    I trust that all information shall remain confidential.
                    <asp:Label runat="server" ID="lblAppliedDate" />
                </div>
            </div>--%>
            <%--<div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:RadioButtonList runat="server" ID="rdoA" RepeatDirection="Horizontal" Visible="false">
                        <asp:ListItem Text="Accept&nbsp;&nbsp;&nbsp;" Value="1" Selected="True"/>
                        <%--<asp:ListItem Text="Reject&nbsp;&nbsp;&nbsp;" Value="0" Enabled="false" />
                    </asp:RadioButtonList>
                </div>
            </div>--%>           
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-7">
                    <asp:LinkButton runat="server" ID="lnkSave" OnClick="btnSave_Click" Text="Submit" CssClass="btn btn-md btn-primary submit lnkSave" />
                    <asp:LinkButton runat="server" ID="lnkModify" OnClick="btnModify_Click" Text="Modify" CssClass="btn btn-md btn-primary" />   
                    <%--<wuc:ConfirmBox runat="server" ID="cfbSave" ConfirmMessage="Do you really want to submit?" TargetControlID="lnkSave" />                         --%>
                </div>
            </div>            
        </div>                            
                    
            </fieldset >
        </asp:Panel>
        </Content>
    </uc:Tab>    
</asp:Content>

