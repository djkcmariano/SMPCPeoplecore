<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpUpdExamList.aspx.vb" Inherits="Secured_EmpUpdExamList" %>

<asp:Content id="Content1" contentplaceholderid="cphBody" runat="server">
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeExamUpdNo">                                                                                   
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
                                        <dx:GridViewDataTextColumn FieldName="EmployeeExamCode" Caption="Record No." />
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
            <asp:HiddenField runat="server" ID="hifTransNo" />
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
                    <label class="col-md-3 control-label has-space">Examination Title  :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblExamTypeDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblExamTypeDesc" />
                    </div>                    
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space">City Address :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCityDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblCityDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Rating :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblScoreRatingDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblScoreRatingDesc" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Date of Examination / Conferment :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDateTaken_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDateTaken" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Place of Examination / Conferment :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblVenue_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblVenue" />
                    </div>                    
                </div>   
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Certificate / License No. :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLicenseNo_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblLicenseNo" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Date Released :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDateReleased_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDateReleased" />
                    </div>                    
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Expiry Date (if any) :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDateExpired_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDateExpired" />
                    </div>                    
                </div>
                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Remarks :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRemark_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblRemark" />
                    </div>                    
                </div>                                          
                <br />
            </div>                                                                                              
        </fieldset>
    </asp:Panel>
</asp:Content>
