<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CarJDEdit.aspx.vb" Inherits="Secured_CarJDEdit" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>                       
            <asp:Label runat="server" ID="lbl" />            
        </Header>
        <Content>
        <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div  class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtJDNo" CssClass="form-control" Enabled="false" Placeholder="Autonumber" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Legal Basis :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtJDCode" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Date Reviewed :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtDateReviewed" CssClass="form-control" placeholder="mm/dd/yyyy" />
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateReviewed" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateReviewed" Mask="99/99/9999" MaskType="Date" />                            
                            <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateReviewed" Display="Dynamic" ForeColor="Red" />
                            <asp:CompareValidator runat="server" ID="Comparevalidator2" ErrorMessage="Reviewed date must not beyond the present/current date." Operator="LessThanEqual" ControlToValidate="txtDateReviewed" Type="Date" Display="Dynamic" ForeColor="Red" />
                        </div>                        
                    </div>                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Position :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboPositionNo" DataMember="EPosition" CssClass="form-control required" />
                        </div>
                    </div> 

                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Facility :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboFacilityNo" DataMember="EFacility" CssClass="form-control"  />
                        </div>
                    </div>

                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Division :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboDivisionNo" DataMember="EDivision" CssClass="form-control"  />
                        </div>
                    </div>

                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Department :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboDepartmentNo" DataMember="EDepartment" CssClass="form-control"  />
                        </div>
                    </div> 

                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Section :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboSectionNo" DataMember="ESection" CssClass="form-control"  />
                        </div>
                    </div> 

                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Unit :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboUnitNo" DataMember="EUnit" CssClass="form-control"  />
                        </div>
                    </div> 

                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Group :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboGroupNo" DataMember="EGroup" CssClass="form-control"  />
                        </div>
                    </div> 

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Functional Title :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboTaskNo" DataMember="ETask" CssClass="form-control"  />
                        </div>
                    </div> 

                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Job Grade :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboJobGradeNo" DataMember="EJobGrade" CssClass="form-control"  />
                        </div>
                    </div> 
                                       
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Reporting To :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtReportingTo" CssClass="form-control" TextMode="Multiline" Rows="4" />
                        </div>                        
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsReportingTo" Text="&nbsp; Tick to display online" ToolTip="Tick to display online"/>
                        </div>
                    </div>                    
                    <br />
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Supervises :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSupervises" CssClass="form-control" TextMode="Multiline" Rows="4" />
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsSupervises" Text="&nbsp; Tick to display online" ToolTip="Tick to display online"  />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Coordinates With :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCoordinate" CssClass="form-control" TextMode="Multiline" Rows="4" />
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsCoordinatesWith" Text="&nbsp; Tick to display online" ToolTip="Tick to display online" />
                        </div>
                    </div>                   
                    <div class="form-group">
                        <%--<label class="col-md-3 control-label has-space">Job Mandate :</label>--%>
                        <label class="col-md-3 control-label has-space">Job Objective :</label>
                        <div class="col-md-6">
                            <dx:ASPxHtmlEditor ID="txtJobMandate" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsJobMandate" Text="&nbsp; Tick to display online" ToolTip="Tick to display online"/>
                        </div>                        
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Job Summary :</label>
                        <div class="col-md-6">                            
                            <dx:ASPxHtmlEditor ID="txtSummary" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic"  />
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsJobSummary" Text="&nbsp; Tick to display online" ToolTip="Tick to display online" />
                        </div>
                    </div>
                    <%--<div class="form-group" style="visibility:hidden">
                        <label class="col-md-3 control-label has-space">Key Relationships :</label>
                        <div class="col-md-6">
                            <dx:ASPxHtmlEditor ID="txtInterRelationship" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" Visible="false" />
                            <div class="row">
                                <div class="col-md-3">Departments/<br />Offices</div>
                                <div class="col-md-3">                                    
                                    <asp:RadioButton runat="server" ID="chkKR1O" Text="&nbsp;Occasional" GroupName="1" Checked="false" /><br />
                                    <asp:RadioButton runat="server" ID="chkKR1F" Text="&nbsp;Frequent" GroupName="1" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtKR1P" CssClass="form-control" TextMode="MultiLine" Rows="3" Placeholder="Purpose" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">Officers/Staff</div>
                                <div class="col-md-3">
                                    <asp:RadioButton runat="server" ID="chkKR2O" Text="&nbsp;Occasional" GroupName="2" Checked="false" /><br />
                                    <asp:RadioButton runat="server" ID="chkKR2F" Text="&nbsp;Frequent" GroupName="2" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtKR2P" CssClass="form-control" TextMode="MultiLine" Rows="3" Placeholder="Purpose" />
                                </div>
                            </div> 
                            <br />
                            <div class="row">
                                <div class="col-md-3">External</div>
                                <div class="col-md-3">
                                    <asp:RadioButton runat="server" ID="chkKR3O" Text="&nbsp;Occasional" GroupName="3" Checked="false" /><br />
                                    <asp:RadioButton runat="server" ID="chkKR3F" Text="&nbsp;Frequent" GroupName="3" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtKR3P" CssClass="form-control" TextMode="MultiLine" Rows="3" Placeholder="Purpose" />
                                </div>
                            </div>                                                        
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsInterrelationship" Text="&nbsp; Tick to display online" ToolTip="Tick to display online" />
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Working Condition :</label>
                        <div class="col-md-6">
                            <dx:ASPxHtmlEditor ID="txtWcondition" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsWorkingCondition" Text="&nbsp; Tick to display online" ToolTip="Tick to display online" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Key Responsibilities :</label>
                        <div class="col-md-6">
                            <dx:ASPxHtmlEditor ID="txtDutiesAndResponsibilities" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsKeyResponsibilities" Text="&nbsp; Tick to display online" ToolTip="Tick to display online" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Attributes :</label>
                        <div class="col-md-6">
                            <dx:ASPxHtmlEditor ID="txtOtherAttributes" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsAttributes" Text="&nbsp; Tick to display online" ToolTip="Display online" />
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Remarks :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="Multiline" Rows="4" />
                        </div>                                                
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Effective Date :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtEffectiveDate" runat="server" CssClass="form-control" />
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtEffectiveDate" Mask="99/99/9999" MaskType="Date" />
                            <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtEffectiveDate" Display="Dynamic" />
                        </div>
                    </div>                                      
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="chkIsEduc" Text="&nbsp;Tick to display education in JD" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="chkIsExpe" Text="&nbsp;Tick to display experience in JD" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="chkIsTrn" Text="&nbsp;Tick to display training in JD" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="chkIsElig" Text="&nbsp;Tick to display eligibility in JD" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-6">
                            <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                        </div>
                    </div>                                
                    <div class="form-group">
                        <div class="col-md-8 col-md-offset-3">
                            <div class="pull-left">
                                <asp:Button runat="server"  ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button runat="server"  ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                                
                            </div>
                        </div>
                    </div>                    
                    <br /><br />                     
                </div>                                                
            </fieldset>
        </asp:Panel>
        </Content>
    </uc:Tab>
</asp:Content>

