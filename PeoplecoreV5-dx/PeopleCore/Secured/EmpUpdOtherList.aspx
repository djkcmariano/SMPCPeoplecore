<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpUpdOtherList.aspx.vb" Inherits="Secured_EmpUpdOtherList" %>


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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeOtherUpdNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click"/>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false"/>
                                        <dx:GridViewDataTextColumn FieldName="Fullname" Caption="Employee Name" />   
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
            <asp:HiddenField runat="server" ID="hiftransNo" />
            <div class="entryPopupDetl form-horizontal">                                                               
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">&nbsp;</label>
                    <div class="col-md-4 has-padding">
                        <b>Old</b>
                    </div>
                    <div class="col-md-4 has-padding">
                        <b>New</b>
                    </div>                    
                </div> 
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Special Skills and Hobbies :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHobbies_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblHobbies" />
                    </div>                    
                </div>                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Non-Academic Distinctions / Recognition :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRecognition_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRecognition" />
                    </div>                    
                </div>               
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Membership in Association / Organization :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblOrganization_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblOrganization" />
                    </div>                    
                </div>                
                <div class="form-group">
                    <div class="col-md-12">
                        <label>Are you related by consanguinity or affinity to the appointing or recommending authority, or to the chief of bureau or office or to the person who has immediate supervision over you in the Office, Bureau or Department where you will be apppointed,</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">a. within the third degree</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsNGovEmployee_Old" /><br /><asp:Label runat="server" ID="lblNGovEmployeeDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsNGovEmployee" /><br /><asp:Label runat="server" ID="lblNGovEmployeeDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">b. within the fourth degree (For Local Government Unit - Career Employees) ?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsLGovEmployee_Old" /><br /><asp:Label runat="server" ID="lblGovEmployeeDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsLGovEmployee" /><br /><asp:Label runat="server" ID="lblGovEmployeeDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">a. Have you ever been found guilty of any administrative offense?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsOffensed_Old" /><br /><asp:Label runat="server" ID="lblOffensedDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsOffensed" /><br /><asp:Label runat="server" ID="lblOffensedDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">b. Have you been criminally charged before any court?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsCharged_Old" /><br />
                        <asp:Label runat="server" ID="lblChargedDeti_Old" /><br />
                        <asp:Label runat="server" ID="lblChargedDetiDate_Old" /><br />
                        <asp:Label runat="server" ID="lblChargedDetiStatus_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsCharged" /><br />
                        <asp:Label runat="server" ID="lblChargedDeti" /><br />
                        <asp:Label runat="server" ID="lblChargedDetiDate" /><br />
                        <asp:Label runat="server" ID="lblChargedDetiStatus" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Have you ever been convicted of any crime or violation of any law, decree, ordinance or regulation by any court or tribunal?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsCourt_Old" /><br /><asp:Label runat="server" ID="lblCourtDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsCourt" /><br /><asp:Label runat="server" ID="lblCourtDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Have you ever been separated from the service in any of the following modes: resignation, retirement, dropped from the rolls, dismissal, termination, end of term, finished contract or phased out (abolition) in the public or private sector?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsSector_Old" /><br /><asp:Label runat="server" ID="lblSectorDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsSector" /><br /><asp:Label runat="server" ID="lblSectorDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">a. Have you ever been a candidate in a national or local election held within the last year (except Barangay election)?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsCandidate_Old" /><br /><asp:Label runat="server" ID="lblCandidateDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsCandidate" /><br /><asp:Label runat="server" ID="lblCandidateDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">b. Have you resigned from the government service during the three (3)-month period before the last election to promote/actively campaign for a national or local candidate?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsResigned_Old" /><br /><asp:Label runat="server" ID="lblResignedDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsResigned" /><br /><asp:Label runat="server" ID="lblResignedDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <div class="col-md-12">
                        <label>Pursuant to: (a) Indigenous People's Act (RA 8371); (b) Magna Carta for Disabled Persons (RA 7277); and (c) Solo Parents Welfare Act of 2000 (RA 8972), please answer the following items:</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">a. Are you a member of any indigenous group?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsIndigenGrp_Old" /><br /><asp:Label runat="server" ID="lblIndigenGrpDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsIndigenGrp" /><br /><asp:Label runat="server" ID="lblIndigenGrpDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">b. Are you a person with disability?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAbled_Old" /><br /><asp:Label runat="server" ID="lblAbledDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAbled" /><br /><asp:Label runat="server" ID="lblAbledDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">c. Are you a solo parent?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsSoloParent_Old" /><br />
                        <asp:Label runat="server" ID="lblSoloParentDeti_Old" /><br />
                        <asp:Label runat="server" ID="lblSoloParentExpiryDate_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsSoloParent" /><br />
                        <asp:Label runat="server" ID="lblSoloParentDeti" /><br />
                        <asp:Label runat="server" ID="lblSoloParentExpiryDate" />
                    </div>                    
                </div>
                <div class="form-group">
                    <div class="col-md-12">
                        <label>Are you related to any official or employee in the BSP</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">a. within the third degree of consanguinity (i.e. mother/father, son/daughter, brother/sister, nephew/niece, uncle/aunt, grandparent, grandchild)?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsConsanguinity_Old" /><br /><asp:Label runat="server" ID="lblConsanguinityDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsConsanguinity" /><br /><asp:Label runat="server" ID="lblConsanguinityDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">b. within the third degree of affinity (i.e. mother-in-law/father-in-law, son-in-law/daughter-in-law, brother-in-law/sister-in-law, uncle-in-law, aunt-in-law, grandparent-in-law, grandchild-in-law)?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAffinity_Old" /><br /><asp:Label runat="server" ID="lblAffinityDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAffinity" /><br /><asp:Label runat="server" ID="lblAffinityDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">c. Other relative/s not specified in items a and b?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsOtherRelative_Old" /><br /><asp:Label runat="server" ID="lblOtherRelativeDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsOtherRelative" /><br /><asp:Label runat="server" ID="lblOtherRelativeDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Do you have relative/s who was/were former employee/s of the Bangko Sentral ng Pilipinas?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsFormer_Old" /><br /><asp:Label runat="server" ID="lblFormerDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsFormer" /><br /><asp:Label runat="server" ID="lblFormerDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Have you ever been a respondent for a preliminary investigation before the Prosecutor's / Fiscal's Office or any law enforcement agency such as the police, PDEA, NBI, etc.?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsRespondent_Old" /><br /><asp:Label runat="server" ID="lblRespondentDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsRespondent" /><br /><asp:Label runat="server" ID="lblRespondentDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <div class="col-md-12">
                        <label>STATUS</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">On-going</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsOngoingA_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsOngoingA" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Dismissed</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsDismissedA_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsDismissedA" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Hypertension</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsHypertension_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsHypertension" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Diabetes</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsDiabetes_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsDiabetes" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Acquired heart disease</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAcquiredHeartDisease_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAcquiredHeartDisease" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Kidney disease</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsKidneyDisease_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsKidneyDisease" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Pulmonary tuberculosis</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsTuberculosis_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsTuberculosis" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Chronic pulmonary disease</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsChronicPumonary_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsChronicPumonary" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Malignancies/cancer</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsMalignancies_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsMalignancies" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Autoimmune disease</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAutoimmune_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAutoimmune" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Cardiovascular accident (CVA)</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsCardiovascularAccident_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsCardiovascularAccident" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Neuro-psychiatric condition</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsNeuroPsychiatric_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsNeuroPsychiatric" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Hematologic condition</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsHematologic_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsHematologic" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Chronic liver disease</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsChronicLiver_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsChronicLiver" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Majorcongenital anomaly/deformation</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsMajorcongenital_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsMajorcongenital" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Others</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsOthers_Old" /><br />
                        <asp:Label runat="server" ID="lblOtherDeti_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsOthers" /><br />
                        <asp:Label runat="server" ID="lblOtherDeti" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">If considered, are you willing to be assigned / re-assigned to the Main / SPC / regional offices / branches of the Bangko Sentral ng Pilipinas in the performance of your job?</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAssigned_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsAssigned" />
                    </div>                    
                </div>
                <br />
            </div>                                                                                            
        </fieldset>
    </asp:Panel>
</asp:Content>

