<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpUpdDepeList.aspx.vb" Inherits="Secured_EmpUpdDepeList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeDepeUpdNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click"/>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />   
                                        <dx:GridViewDataComboBoxColumn FieldName="EventTypeDesc" Caption="Action" />       
                                        <dx:GridViewDataTextColumn FieldName="EmployeeDepeCode" Caption="Record No." />
                                        <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Requested" />    
                                        <dx:GridViewDataComboBoxColumn FieldName="ApprovalStatDesc" Caption="Status" Visible="false" />                                                                                                                                                  
                                        <dx:GridViewDataTextColumn FieldName="ApproveDisapproveDate" Caption="Approved /<br />Disapproved<br />Date" />                                                                    
                                        <dx:GridViewDataTextColumn FieldName="ApproveDisapproveBy" Caption="Approved /<br />Disapproved<br />By" Visible="false" />
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
            <asp:HiddenField runat="server" ID="hiftransNo" />
            <div class="entryPopupDetl form-horizontal">                                                               
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">&nbsp;</label>
                    <div class="col-md-4">
                        <b>Old</b>
                    </div>
                    <div class="col-md-4">
                        <b>New</b>
                    </div>                    
                </div>                                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Relationship  :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRelationshipDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRelationshipDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Surname  :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLastName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLastName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">First Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFirstName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFirstName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Middle Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMiddleName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMiddleName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Maiden Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMaidenName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMaidenName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Name Extension :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployeeExtDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployeeExtDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Date of Birth :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBirthDate_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBirthDate" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Civil Status :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCivilStatDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCivilStatDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Contact No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblPhoneNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblPhoneNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Home Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHomeAddress_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHomeAddress" />
                    </div>                    
                </div>
                
                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Employer / Business Name :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployerName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployerName" />
                    </div>                    
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Business Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployerAdd_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployerAdd" />
                    </div>                    
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Occupation / Position :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblOccupation_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblOccupation" />
                    </div>                    
                </div>                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Assign as dependent :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsDependent_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsDependent" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Assign as beneficiary :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsBeneficiary_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsBeneficiary" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Family member has own insurance :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsInsurance_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsInsurance" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Assign as HMO/MIS dependent :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsWithHMO_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsWithHMO" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Remarks :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRemarks_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRemarks" />
                    </div>                    
                </div>                                                                                              
        </fieldset>
    </asp:Panel>
</asp:Content>

