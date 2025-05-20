<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfEmpUpdDial.aspx.vb" Inherits="Secured_SelfEmpUpdDial" %>

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
                                    <%--<li><asp:LinkButton runat="server" ID="lnkApprove" OnClick="lnkApprove_Click" Text="Approve" CssClass="control-primary" Visible="false" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDisapprove" OnClick="lnkDisapprove_Click" Text="Disapprove" CssClass="control-primary" Visible="false" /></li>                                                                                    --%>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li> 
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" Visible="false" /></li>
                                    <%--<uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDisapprove" ConfirmMessage="Are you sure you want to disapprove?"  />
                                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkApprove" ConfirmMessage="Are you sure you want to approve?"  />--%>
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeDialectUpdNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click"/>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                        
                                        <dx:GridViewDataComboBoxColumn FieldName="EventTypeDesc" Caption="Action" />       
                                        <%--<dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />  --%>
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
                    <label class="col-md-3 control-label has-space">Language / Dialect :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDialectDesc_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblDialectDesc" />
                    </div>                    
                </div>                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Writing Proficiency Level :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblWritingProfLevel_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblWritingProfLevel" />
                    </div>                    
                </div>               
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Reading Proficiency Level :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblReadingProfLevel_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblReadingProfLevel" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Speaking Proficiency Level :</label>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpeakingProfLevel_Old" />
                    </div>
                    <div class="col-md-4 has-padding">
                        <asp:Label runat="server" ID="lblSpeakingProfLevel" />
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