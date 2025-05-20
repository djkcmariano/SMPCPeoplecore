<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfEmployeeRateAppr.aspx.vb" Inherits="Secured_EmpRateEdit" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <%--<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    &nbsp;
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
                </div> 
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeNo">                                                                                   
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeRateClassDesc" Caption="Rate Class" />                                                        
                            <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Salary" />                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Allowances" HeaderStyle-HorizontalAlign="Center" Width="3%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkAllowance" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkAllowance_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Facility" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" Visible="false" />      
                            <dx:GridViewDataComboBoxColumn FieldName="LocationDesc" Caption="Location" Visible="false" />                                                                                                                                
                            <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="ProjectDesc" Caption="Project" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />                                
                            <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />                            
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>--%>
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />            
            </center>            
            <asp:Label runat="server" ID="lbl" />        
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-3 control-label">Employee No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtEmployeeCode" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Employee Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtFullname" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Rate Class :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboEmployeeRateClassNo" runat="server" CssClass="form-control" DataMember="EEmployeeRateClass" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Current Salary :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCurrentSalary" CssClass="form-control" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCurrentSalary" FilterType="Numbers, Custom" ValidChars="." /> 
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button runat="server"  ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                        </div>
                    </div>
                </div>                               
                <br /><br /> 
            </fieldset>
            </asp:Panel>                                                  
        </Content>
    </uc:Tab>
</asp:Content>

