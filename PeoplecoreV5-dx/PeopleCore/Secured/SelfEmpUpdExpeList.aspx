<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfEmpUpdExpeList.aspx.vb" Inherits="Secured_EmpUpdExpeList" %>

<asp:Content id="Content1" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab">
    <Header>
        <center>
            <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
            <br />            
        </center>            
        <asp:Label runat="server" ID="lbl" />        
    </Header> 
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
                        <div class="col-md-2">
                            <asp:Dropdownlist ID="cboTabNo" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>                    
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkApprove" OnClick="lnkApprove_Click" Text="Approve" CssClass="control-primary" Visible="false" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDisapprove" OnClick="lnkDisapprove_Click" Text="Disapprove" CssClass="control-primary" Visible="false" /></li>                                                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li> 
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" Visible="false" /></li>
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDisapprove" ConfirmMessage="Are you sure you want to disapprove?"  />
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkApprove" ConfirmMessage="Are you sure you want to approve?"  />
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeExpeUpdNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click"/>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />   
                                        <dx:GridViewDataComboBoxColumn FieldName="EventTypeDesc" Caption="Action" />       
                                        <dx:GridViewDataTextColumn FieldName="EmployeeExpeCode" Caption="Record No." />  
                                        <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Requested" />  
                                        <dx:GridViewDataComboBoxColumn FieldName="ApprovalStatDesc" Caption="Status" Visible="false" />                                                                                                                                                    
                                        <dx:GridViewDataTextColumn FieldName="ApproveDisapproveDate" Caption="Approved / Disapproved Date" />                                                                       
                                        <dx:GridViewDataTextColumn FieldName="ApproveDisapproveBy" Caption="Approved / Disapproved By" Visible="false" />
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
            </div>
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
                    <label class="col-md-3 control-label has-space">Government Agency :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsGov_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIsGov" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">From :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFromDate_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFromDate" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">To :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblToDate_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblToDate" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Position Title :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblPosition_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblPosition" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Department / Agency / Office / Company :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblExpeComp_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblExpeComp" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblExpeCompAdd_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblExpeCompAdd" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Contact No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompPhone_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompPhone" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Status of Employment / Appointment :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployeeStatDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmployeeStatDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Salary/Job/Pay Grade/Step Increment :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSalaryLevel_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSalaryLevel" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Immediate Supervisor :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblImmediateSuperior_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblImmediateSuperior" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Industry Type :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIndustryDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblIndustryDesc" />
                    </div>                    
                </div>                                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Experience Type :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblExpeTypeDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblExpeTypeDesc" />
                    </div>                    
                </div>                                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">List of Accomplishments and Contributions :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblAccomplishment_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblAccomplishment" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Summary of Actual Duties :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDuties_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDuties" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Monthly Salary :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCurrentSalary_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCurrentSalary" />
                    </div>                    
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space">Reason For Leaving :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblReasonsForLeaving_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblReasonsForLeaving" />
                    </div>                    
                </div>                                             
                <br />
            </div>                                                                                              
        </fieldset>
    </asp:Panel>
</asp:Content>