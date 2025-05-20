<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfAppRandomAnsApprList.aspx.vb" Inherits="SecuredManager_SelfAppRandomAnsApprList" Theme="PCoreStyle" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAccept" OnClick="lnkAccept_Click" Text="Accept" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkReject" OnClick="lnkReject_Click" Text="Reject" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                    </li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbApprove" TargetControlID="lnkAccept" ConfirmMessage="Selected applicant(s) will be accepted, Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="cfbDisapprove" TargetControlID="lnkReject" ConfirmMessage="Selected applicant(s) will be rejected, Proceed?"  />
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ApplicantRandomAnsNo">
                                <Columns>
                                    <dx:GridViewDataColumn Caption="Profile" CellStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnk" Text="click here" CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnk_Click">
                                            <i class="fa fa-user"></i>
                                        </asp:LinkButton>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="History" CellStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkHistory" Text="click here" CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnkHistory_Click">
                                            <i class="fa fa-history"></i>
                                        </asp:LinkButton>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>


                                    <dx:GridViewDataColumn Caption="Assessment" CellStyle-HorizontalAlign="Center" Visible="false">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkQSx" Text="click here" CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("MRNo") %>' OnClick="lnkQS_Click">
                                            <i class="fa fa-check-square-o"></i>
                                        </asp:LinkButton>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                     <dx:GridViewDataColumn Caption="Assessment" CellStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkQS" Text="click here" CommandArgument='<%# Eval("ApplicantRandomAnsNo") & "|" & Eval("IsApplicant") & "|" & Eval("MRNo") & "|" & Eval("ApplicantNo") %>' OnClick="lnkQSx_Click">
                                            <i class="fa fa-check-square-o"></i>
                                        </asp:LinkButton>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Applicant Name" />
                                    <dx:GridViewDataTextColumn FieldName="MRCode" Caption="MR No." />
                                    <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" />
                                    <dx:GridViewDataComboBoxColumn FieldName="GenderDesc" Caption="Gender" />
                                    <dx:GridViewDataTextColumn FieldName="BirthAge" Caption="Age" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." />
                                    <dx:GridViewDataDateColumn FieldName="BirthDate" Caption="Birth Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PresentAddress" Caption="Present Address" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="CityDesc" Caption="City" />
                                    <dx:GridViewDataTextColumn FieldName="PresentPhoneno" Caption="Present Phone No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="HomeAddress" Caption="Permanent Address" Visible="false" />
                                    <dx:GridViewDataDateColumn FieldName="EncodeDate" Caption="Applied Date" Visible="false" />
                                    <dx:GridViewDataDateColumn FieldName="Serveddate" Caption="Accepted Date" Visible="false" />
                                    <dx:GridViewDataDateColumn FieldName="dateREjected" Caption="Rejected Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status" Width="12%" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                </Columns>
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                            <asp:SqlDataSource runat="server" ID="SqlDataSource1"></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <uc:Info runat="server" ID="Info1" />
    <uc:History runat="server" ID="History" />

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pInfomation" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
        </div>
        <div class="container-fluid entryPopupDetl">        
        <div class="page-content-wrap">
            <asp:GridView runat="server" ID="grdQS" AutoGenerateColumns="false" BorderWidth="0" Width="100%" ShowHeader="false">
                <Columns>
                    <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                        <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="30%" />
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Title") %>' ID="lblType" />                                       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                        <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="65%" />
                        <ItemTemplate>
                            <asp:HiddenField runat="server" Value='<%# Bind("QSTypeNo") %>' ID="hifQSTypeNo" />
                            <asp:HiddenField runat="server" Value='<%# Bind("ID") %>' ID="hifQSNo" />
                            <asp:Label runat="server" Text='<%# Bind("Value") %>' ID="lblValue" />                                       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                        <HeaderStyle HorizontalAlign="Center" BorderWidth="0" Width="5%" />
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chk" Enabled="false" Checked='<%# Bind("IsPass") %>'  />
                        </ItemTemplate>
                    </asp:TemplateField>                                
                </Columns>
            </asp:GridView>
        </div>
        </div>
    </fieldset>
</asp:Panel>

</asp:Content>

