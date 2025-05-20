<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppList.aspx.vb" Inherits="Secured_AppList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap" >         
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
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                        
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                         
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ApplicantNo">                        
                            <Columns>
                                <dx:GridViewDataColumn Caption="Edit" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("ApplicantNo") %>' OnClick="lnkEdit_Click" />
                                        <%--<asp:Label runat="server" ID="lnkEdit" Text='<%# Bind("xtransno") %>'></asp:Label>--%>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                               
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Applicant Name" />
                                <dx:GridViewDataTextColumn FieldName="BirthAge" Caption="Age" Width="50" />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Preferred Position" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CourseDesc" Caption="Course" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EducLevelDesc" Caption="Educ. Level" Visible="false" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." />
                                <dx:GridViewDataDateColumn FieldName="BirthDate" Caption="Birth Date" Visible="false" />                                
                                <dx:GridViewDataTextColumn FieldName="PresentAddress" Caption="Present Address" />                                
                                <dx:GridViewDataTextColumn FieldName="PresentPhoneno" Caption="Present Phone No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="HomeAddress" Caption="Provincial Address" Visible="false" />
                                <dx:GridViewDataDateColumn FieldName="EncodeDate" Caption="Applied Date" Visible="false" />
                                <dx:GridViewDataDateColumn FieldName="Serveddate" Caption="Accepted Date" Visible="false" />                                
                                <dx:GridViewDataTextColumn FieldName="ApplicantStatDesc" Caption="Status" />
                                <dx:GridViewDataColumn Caption="Attachment" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkAttachment" OnClick="lnkAttachment_Click" CssClass="fa fa-paperclip " CommandArgument='<%# Eval("ApplicantNo") & "|" & Eval("Fullname")  %>' Font-Size="Medium" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="PDS Form" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" OnClick="lnkPrint_Click" Font-Size="Medium" OnPreRender="lnkPrint_PreRender" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="BSP Form" CellStyle-HorizontalAlign="Center" Width="10" Visible="false">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPrint2" CssClass="fa fa-print" OnClick="lnkPrint2_Click" Font-Size="Medium" OnPreRender="lnkPrint2_PreRender" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                                                                                              
                            </Columns>                                                        
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                        <asp:SqlDataSource runat="server" ID="SqlDataSource1">
                          
                        </asp:SqlDataSource>
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

</asp:Content>

