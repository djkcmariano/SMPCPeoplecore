<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpBenSALNDetails.aspx.vb" Inherits="Secured_EmpBenSALNDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc:Tab runat="server" ID="Tab">   
        <Header>            
        <asp:Label runat="server" ID="lbl"/>
    </Header>
     
    <Content>
    <asp:Panel runat="server" ID="Panel1">        
        <br /><br />            
        <fieldset class="form" id="fsMain">
            <div  class="form-horizontal">
                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">DECLARANT'S &nbsp;&nbsp;INFORMATION</label>
                    </h5>
                </div>
                <div class="form-group" style=" display:none;">
                    <label class="col-md-3 control-label has-space">Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtSALNNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Type of Filing :</label>
                    <div class="col-md-6">
                        <asp:RadioButton runat="server" ID="rdoIsJoint1" GroupName="xrdIsJoint" CausesValidation="false" Text="Jointly Filed" /> &nbsp;&nbsp;
                        <asp:RadioButton runat="server" ID="rdoIsJoint2" GroupName="xrdIsJoint" CausesValidation="false" Text="Separately Filed" /> &nbsp;&nbsp;
                        <asp:RadioButton runat="server" ID="rdoIsNA" GroupName="xrdIsJoint" CausesValidation="false" Text="Not Applicable" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Applicable Year :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtApplicableYear" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group" style=" display:none;">
                    <label class="col-md-3 control-label has-space">Employee No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtEmployeeCode" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group" style=" display:none;">
                    <label class="col-md-3 control-label has-space">Employee Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Family Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtLName" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-required">First Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtFName" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Middle Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMName" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Address :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control required" TextMode="MultiLine" Rows="3" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Position :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboPositionNo" runat="server" CssClass="form-control" DataMember="EPosition" />
                    </div>
                </div>

                <div class="form-group" style=" display:none;">
                    <label class="col-md-3 control-label has-space">Income :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtIncome" CssClass="form-control Number" runat="server" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Agency / Office :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtOffice" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Office Address :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtOfficeAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Date Accomplished :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtDateAccomplished" CssClass="form-control required"/>
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateAccomplished" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateAccomplished" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtDateAccomplished" Display="Dynamic" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Tax Identification No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtTIN1" runat="server" CssClass="form-control required"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Comm. Tax Cert. No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtCTCN1" runat="server" CssClass="form-control required"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Issued At :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtIssuedAt1" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Issued on :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtIssuedOn1" CssClass="form-control required"/>
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtIssuedOn1" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtIssuedOn1" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtIssuedOn1" Display="Dynamic" />
                    </div>
                </div>


                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">SPOUSE'S &nbsp;&nbsp;INFORMATION</label>
                    </h5>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-6">
                        <asp:RadioButton runat="server" ID="rdoIsMarried1" GroupName="xrdIsMarried" CausesValidation="false" AutoPostBack="true" OnCheckedChanged="xrdIsMarriedChangeLoad" Text="I am married." />
                        <asp:RadioButton runat="server" ID="rdoIsMarried2" GroupName="xrdIsMarried" CausesValidation="false" AutoPostBack="true" OnCheckedChanged="xrdIsMarriedChangeLoad" Text="I am not married." />
                    </div>
                </div>
 
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Family Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtSpouseLName" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">First Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtSpouseFName" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Middle Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtSpouseMName" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Position :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtSpousePosition" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Agency / Office :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtSpouseOffice" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Office Address :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtSpouseOfficeAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Tax Identification No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtTIN" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Comm. Tax Cert. No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtCTCN2" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Issued At :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtIssuedAt2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Issued on :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtIssuedOn2" CssClass="form-control"/>
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtIssuedOn2" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="txtIssuedOn2" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtIssuedOn2" Display="Dynamic" />
                    </div>
                </div>

               <div class="form-group">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-6">            
                        <asp:LinkButton runat="server" ID="lnkSave" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-default submit lnkSave" />       
                        <asp:LinkButton runat="server" ID="lnkModify" OnClick="btnModify_Click" Text="Modify" CssClass="btn btn-default " CausesValidation="false"  />                      
                    </div>
                </div>


                <br /><br />
            </div>                                                
        </fieldset>
    </asp:Panel>
    </Content>

</uc:Tab>    
</asp:Content>