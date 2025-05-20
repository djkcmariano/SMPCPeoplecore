<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfEmployeeListAppr.aspx.vb" Inherits="Secured_EmpListing" Theme="PCoreStyle" %>

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
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" Visible="false" /></li>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeNo" OnCustomButtonCallback="lnkEdit_Click">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataBinaryImageColumn FieldName="Photo" Caption="Photo">
                                    <PropertiesBinaryImage ImageHeight="50px" ImageWidth="50px" >
                                    </PropertiesBinaryImage>
                                </dx:GridViewDataBinaryImageColumn>
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />     
                                <dx:GridViewDataTextColumn FieldName="HomeAddress" Caption="Residential Address" Visible="false" />                                                                      
                                <dx:GridViewDataTextColumn FieldName="BirthDate" Caption="Birth Date" Visible="false" />                  
                                <dx:GridViewDataTextColumn FieldName="BirthAge" Caption="Birth Age" Visible="false" />                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />                                                                           
                                <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Business Unit" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" Visible="false" />                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Team" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GenderDesc" Caption="Gender" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CivilStatDesc" Caption="Civil Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Sub Team" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="DateHired" Caption="Date Hired" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="RegularizedDate" Caption="Date of Regularization" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." />
                                <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" />
                                <dx:GridViewDataTextColumn FieldName="PresentAddress" Caption="Present Address" />
                                <dx:GridViewDataTextColumn FieldName="TINNo" Caption="TIN" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="SSSNo" Caption="SSS" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PHNo" Caption="PhilHealth" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="HDMFNo" Caption="HDMF" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="BankTypeDesc" Caption="Bank Type" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="BankAccountNo" Caption="Bank Account No." Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="TaxExemptDesc" Caption="Tax Code" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="SuperiorName" Caption="Superior" Visible="false" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>
</asp:Content>

