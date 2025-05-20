<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfApplicantAppr.aspx.vb" Inherits="SecuredManager_SelfApplicantAppr" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>
<%@ Register Src="~/Include/AppHistory.ascx" TagName="History" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">

                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAccept" OnClick="lnkAccept_Click" Text="Accept" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkReject" OnClick="lnkReject_Click" Text="Reject" CssClass="control-primary" /></li>
                            <%--<li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>--%>
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbApprove" TargetControlID="lnkAccept" ConfirmMessage="Selected applicant(s) will be accepted, Proceed?"  />
                        <uc:ConfirmBox runat="server" ID="cfbDisapprove" TargetControlID="lnkReject" ConfirmMessage="Selected applicant(s) will be rejected, Proceed?"  />                                                    
                    </ContentTemplate>
                    <%--<Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>--%>
                    </asp:UpdatePanel> 
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ApplicantRandomAnsNo">                                                                                   
                            <Columns>                                                            
                                <dx:GridViewDataColumn Caption="Applicant Name">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnk" Text='<%# Eval("FullName") %>' CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnk_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                           
                                <dx:GridViewDataTextColumn FieldName="MRCode" Caption="MR No." />                                                                                  
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position Applied" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CourseDesc" Caption="Course" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EducLevelDesc" Caption="Educ. Level" Visible="false" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." />
                                <dx:GridViewDataDateColumn FieldName="BirthDate" Caption="Birth Date" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="BirthAge" Caption="Age" />
                                <dx:GridViewDataTextColumn FieldName="PresentAddress" Caption="Present Address" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CityDesc" Caption="City" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PresentPhoneno" Caption="Present Phone No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="HomeAddress" Caption="Provincial Address" Visible="false" />
                                <dx:GridViewDataDateColumn FieldName="EncodeDate" Caption="Applied Date" Visible="false" />
                                <dx:GridViewDataDateColumn FieldName="Serveddate" Caption="Accepted Date" Visible="false" />
                                <dx:GridViewDataDateColumn FieldName="dateREjected" Caption="Rejected Date" Visible="false" />
                                <dx:GridViewDataColumn Caption="History" Width="5%" CellStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkHistory" CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnkHistory_Click">
                                            <i class="fa fa-history"></i>
                                        </asp:LinkButton>
                                    </DataItemTemplate>                                    
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Visible="false" />
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

<uc:Info runat="server" ID="Info1" />
<uc1:History runat="server" ID="History" />
</asp:Content>

