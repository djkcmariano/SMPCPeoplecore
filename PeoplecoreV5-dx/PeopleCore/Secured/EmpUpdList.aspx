<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpUpdList.aspx.vb" Inherits="Secured_EmpUpdList" %>

<asp:Content ContentPlaceHolderID="cphBody" ID="Content1" runat="server">

<uc:Tab runat="server" ID="Tab">
    <Content>
        <style type="text/css">
            .has-padding{ 
                padding-top:7px;
            }
        </style>
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-3">
                            <asp:Dropdownlist ID="cboTabNo" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>                    
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkApprove" OnClick="lnkApprove_Click" Text="Approve" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDisapprove" OnClick="lnkDisapprove_Click" Text="Disapprove" CssClass="control-primary" /></li>                                                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDisapprove" ConfirmMessage="Are you sure you want to disapprove?"  />
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkApprove" ConfirmMessage="Are you sure you want to approve?"  />
                                </ul>                                                    
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeUpdNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeUpdNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false"/>
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />   
                                        <dx:GridViewDataComboBoxColumn FieldName="EventTypeDesc" Caption="Action" />
                                        <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Requested" />   
                                        <dx:GridViewDataComboBoxColumn FieldName="ApprovalStatDesc" Caption="Status" Visible="false" />                                                                                                                                                
                                        <dx:GridViewDataTextColumn FieldName="ApproveDisapproveDate" Caption="Approved /<br/>Disapproved<br/>Date" />
                                        <dx:GridViewDataTextColumn FieldName="ApproveDisapproveBy" Caption="Approved /<br/>Disapproved<br/>By" Visible="false" />                                                                       
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                           
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>               
    </Content>
</uc:Tab>
<asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
                <asp:Linkbutton runat="server" ID="lnkDisapprove2" CssClass="fa fa-thumbs-down" ToolTip="Disapprove" OnClick="lnkDisapprove2_Click" />
                <asp:Linkbutton runat="server" ID="lnkApprove2" CssClass="fa fa-thumbs-up" ToolTip="Approve" OnClick="lnkApprove2_Click" />
                <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="lnkDisapprove2" ConfirmMessage="Are you sure you want to disapprove?"  />
                <uc:ConfirmBox runat="server" ID="ConfirmBox4" TargetControlID="lnkApprove2" ConfirmMessage="Are you sure you want to approve?"  />

            </div>
            <div class="entryPopupDetl form-horizontal">                                                               
                <asp:HiddenField runat="server" ID="hifEmployeeUpdNo" />
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-4">
                        <b>Old</b>
                    </div>
                    <div class="col-md-4">
                        <b>New</b>
                    </div>                    
                </div>
                <asp:PlaceHolder runat="server" ID="ph" />
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Title  :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCourtesyDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCourtesyDesc" />
                    </div>                    
                </div>
                <%--<div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Last Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLastName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLastName" />
                    </div>                    
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">First Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFirstName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFirstName" />
                    </div>                    
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Middle Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMiddleName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMiddleName" />
                    </div>                    
                </div>--%>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Maiden Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMaidenName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMaidenName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Name Extension :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployeeExtDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployeeExtDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Nick Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblNickName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblNickName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Date of Birth :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBirthDate_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBirthDate" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Place of Birth :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBirthPlace_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBirthPlace" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Sex :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblGenderDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblGenderDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Citizenship :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCitizenDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCitizenDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsDualCitizenshipDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsDualCitizenshipDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDualCitizenshipTypeDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDualCitizenshipTypeDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDualCitizenshipCountry_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDualCitizenshipCountry" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Civil Status :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCivilStatDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCivilStatDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Height (meters) :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHeight_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHeight" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Weight (kilograms) :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblWeight_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblWeight" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Blood Type :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBloodTypeDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBloodTypeDesc" />
                    </div>                    
                </div>                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Religion :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblReligionDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblReligionDesc" />
                    </div>                    
                </div>                                                         
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Date of Marriage :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMarriageDate_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMarriageDate" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Place of Marriage :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMarriagePlace_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMarriagePlace" />
                    </div>                    
                </div>
                <h4><b>SPOUSE DETAILS</b></h4>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Name Extension (JR., SR) :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseEmployeeExtDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseEmployeeExtDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Maiden Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseMaidenName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseMaidenName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Occupation :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseOccupation_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseOccupation" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Employer/Business Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseEmployerName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseEmployerName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Employer/Business Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseEmployerAddress_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseEmployerAddress" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Employer/Business Telephone No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseEmployerContactNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpouseEmployerContactNo" />
                    </div>                    
                </div>
                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">CONTACT&nbsp;&nbsp;INFORMATION</label>
                    </h5>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Permanent Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHomeAddress_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHomeAddress" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCityHomeDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCityHomeDesc" />
                    </div>                    
                </div>                
                 <div class="form-group">
                    <label class="col-md-4 control-label has-space">Residential Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblPresentAddress_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblPresentAddress" />
                    </div>                    
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCityDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCityDesc" />
                    </div>                    
                </div>
                
                <%--<div class="form-group">
                    <label class="col-md-4 control-label has-space">Present Contact No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblPresentPhoneNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblPresentPhoneNo" />
                    </div>                    
                </div>--%>                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Telephone No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHomePhoneNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHomePhoneNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Mobile No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMobileNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMobileNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Email Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmail_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmail" />
                    </div>                    
                </div>              
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Contact Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblContactName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblContactName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Relationship :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblContactRelationshipDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblContactRelationshipDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Contact No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblContactNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblContactNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblContactAddress_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblContactAddress" />
                    </div>                    
                </div>
                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">COMPANY&nbsp;&nbsp;CONTACT&nbsp;&nbsp;INFORMATION</label>
                    </h5>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Mobile No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompanyMobileNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompanyMobileNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Direct No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompanyTelNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompanyTelNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Fax No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFaxNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFaxNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Local No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLocalNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLocalNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Email Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompanyEmail_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompanyEmail" />
                    </div>                    
                </div>
                <br />
                <br />
            </div>                                                                                              
        </fieldset>
    </asp:Panel>
</asp:Content>


