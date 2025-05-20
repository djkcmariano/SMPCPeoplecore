<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="OrgTableReclassEdit.aspx.vb" Inherits="Secured_OrgTableReclassEdit" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" />
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1">
                <fieldset class="form" id="fsMain">
                <br />
                <div class="row">
                     <div class="col-md-6">
                <!-- START DIVISION PERMISSION -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">Criteria</h4>  
                        </div>
                        <div class="panel-body">
                            <div  class="form-horizontal">

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Position :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboPositionNo" DataMember="EPosition" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Functional Title :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboTaskNo" DataMember="ETask" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Job Level :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboSalaryGradeNo" DataMember="ESalaryGrade" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Job Family :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboOccupationalGroupNo" DataMember="EOccupationalGroup" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                                                
                            </div>
                        </div>
                    </div> 
                </div>

                     <div class="col-md-6">
                <!-- START DIVISION PERMISSION -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">Action Template</h4>  
                        </div>
                        <div class="panel-body">
                             <div  class="form-horizontal">

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Position :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboNewPositionNo" DataMember="EPosition" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Functional Title :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboNewTaskNo" DataMember="ETask" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Job Level :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboNewSalayGradeNo" DataMember="ESalaryGrade" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Unit :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboNewOccupationalGroupNo" DataMember="EOccupationalGroup" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div> 
                </div>
                </div>
            
                <div class="row"> 
                    <div class="col-md-12">  
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div  class="form-horizontal">
                                    <div class="form-group" style="display:none;">
                                        <label class="col-md-4 control-label has-space">Transaction No. :</label>
                                        <div class="col-md-6">
                                            <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" ></asp:Textbox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Description :</label>
                                        <div class="col-md-6">
                                            <asp:Textbox ID="txtTableOrgReclassDesc" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-required">TO Action Type :</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="cboTableOrgActionTypeNo" runat="server" CssClass="form-control required" DataMember="ETableOrgActionType" />
                                        </div>
                                    </div>                                   

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-required">Effective Date :</label>
                                        <div class="col-md-2">
                                            <asp:Textbox ID="txtEffectiveDate" runat="server" CssClass="form-control required"></asp:Textbox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />                   
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtEffectiveDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtEffectiveDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator2" /> 
                                        </div>
                                    </div>
                                                           
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Remarks :</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                                                       
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space"></label>
                                        <div class="col-md-6">
                                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />
                                        </div>
                                    </div>
                               </div>
                            </div>
                        </div>
                    </div>
                </div>

                </fieldset>
           </asp:Panel>
        </Content>
    </uc:Tab>
</asp:Content>

