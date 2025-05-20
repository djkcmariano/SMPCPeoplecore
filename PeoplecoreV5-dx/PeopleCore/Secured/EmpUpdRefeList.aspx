<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpUpdRefeList.aspx.vb" Inherits="Secured_EmpUpdRefeList" %>


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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeRefeUpdNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click"/>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="Fullname" Caption="Employee Name" />   
                                        <dx:GridViewDataComboBoxColumn FieldName="EventTypeDesc" Caption="Action" />       
                                        <dx:GridViewDataTextColumn FieldName="EmployeeRefeCode" Caption="Record No." />
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
                <%--<div class="form-group">
                    <label class="col-md-3 control-label has-space">Relationship :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRelationRefeType_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRelationRefeType" />
                    </div>                    
                </div>--%>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Surname :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLastName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLastName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">First Name:</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFirstName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblFirstName" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Middle Name:</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMiddleName_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblMiddleName" />
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
                    <label class="col-md-3 control-label has-space">Email :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmail_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblEmail" />
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
                    <label class="col-md-3 control-label has-space">Employer / Company :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompany_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCompany" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblAddress_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblAddress" />
                    </div>                    
                </div>
                <%--<div class="form-group">
                    <label class="col-md-3 control-label has-space">Business Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBusinessAddress_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBusinessAddress" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Business Phone No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBusinessPhoneNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblBusinessPhoneNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Years Known : :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblAcquaintanceYear_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblAcquaintanceYear" />
                    </div>                    
                </div>--%>                
                <br />
            </div>                                                                                              
        </fieldset>
    </asp:Panel>
</asp:Content>

