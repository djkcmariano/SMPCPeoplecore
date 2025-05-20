<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" EnableEventValidation="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="CliClinicList.aspx.vb" Inherits="Secured_CliClinicList" %>

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
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>                                                    
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                    </asp:UpdatePanel> 
                    <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                        <Content>
                                <div class="form-group">
                                <label class="col-md-4 control-label">Filter By :</label>
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control" />
                                    </div>
                                    <ajaxToolkit:CascadingDropDown ID="cdlfilterby" TargetControlID="cbofilterby" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterBy" runat="server" Category="tNo" LoadingText="Loading..." />
                                </div>
		                        <div class="form-group">
                                <label class="col-md-4 control-label">Filter Value :</label> 
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control" />
                                </div>
                                <ajaxToolkit:CascadingDropDown ID="cdlfiltervalue" TargetControlID="cbofiltervalue" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterValue" runat="server" Category="tNo" ParentControlID="cbofilterby" LoadingText="Loading..." PromptText="-- Select --" />
                                </div>
                        </Content>
                        </uc:Filter>
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
                                            <%--<asp:Label runat="server" ID="lnkEdit" Text='<%# Bind("xtransno") %>'></asp:Label>--%>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                                                           
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
                                <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="TINNo" Caption="TIN" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="SSSNo" Caption="SSS" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PHNo" Caption="PhilHealth" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="HDMFNo" Caption="HDMF" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="BankTypeDesc" Caption="Bank Type" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="BankAccountNo" Caption="Bank Account No." Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="TaxExemptDesc" Caption="Tax Code" Visible="false" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="List of Dependents" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />                                        
                                </DataItemTemplate>
                            </dx:GridViewDataColumn> 
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>

<div class="page-content-wrap" id="divDetl" runat="server">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title" >
                    Transaction No.:&nbsp; <asp:Label runat="server" ID="lblDetl" />
                </div>                   
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">

                <div class="row">
                        <div class="table-responsive">                    
                            <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="employeedepeno" Width="100%" >                                                                                   
                                <Columns>      
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("employeedepeno") %>' OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                     
                                    <dx:GridViewDataTextColumn FieldName="code" Caption="Record No." />
                                    <dx:GridViewDataTextColumn FieldName="pFullname" Caption="Name" />
                                    <dx:GridViewDataTextColumn FieldName="RelationshipDesc" Caption="Relationship to Applicant" />
                                    <dx:GridViewDataTextColumn FieldName="BirthDate" Caption="Birth Date" />
                                    <dx:GridViewDataTextColumn FieldName="CivilStatDesc" Caption="Civil Status" />
                                    <dx:GridViewDataTextColumn FieldName="PhoneNo" Caption="Contact No." />                                                                     
                                </Columns>
                                                             
                            </dx:ASPxGridView>
                                            
                            <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />  
                        </div>                            
                    </div>
            </div>
        </div>
    </div>
</div> 

</asp:Content>

