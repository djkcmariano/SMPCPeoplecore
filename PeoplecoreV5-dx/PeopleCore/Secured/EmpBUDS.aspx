<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="EmpBUDS.aspx.vb" Inherits="Secured_EmpBUDS" %>

<%--<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>--%>
<%@ Register Src="~/Include/wucFilterGeneric.ascx" TagName="Filter" TagPrefix="wuc" %>

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
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" Visible="true">
                           <ContentTemplate>
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Update Records" CssClass="control-primary"  Visible="false"/>
                                    </li>
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
                    <%--<div class="row">
                        <asp:GridView runat="server" ID="grdM" DataSourceID="SqlDataSource1" AutoGenerateColumns="true" EmptyDataText="no record found" DataKeyNames="EmployeeNo" />
                    </div>--%>
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXWOSearch" KeyFieldName="EmployeeNo">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                           <%--<asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeNo") %>' OnClick="lnkEdit_Click" />--%>
                                            <asp:Label runat="server" ID="lnkEdit" Text='<%# Bind("xtransno") %>'></asp:Label>
                                            
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="true" />
                                    <dx:GridViewDataComboBoxColumn FieldName="CivilStatDesc" Caption="Civil Status" Visible="true" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group"/>
                                    <dx:GridViewDataTextColumn FieldName="DateHired" Caption="Date Hired" />
                                    <dx:GridViewDataTextColumn FieldName="RegularizedDate" Caption="Date of Regularization" />
                                    <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" Visible="true" />
                                    <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." Visible="true" />
                                    
                                </Columns>
                                <SettingsSearchPanel Visible="false" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                                        
                            <asp:SqlDataSource runat="server" ID="SqlDataSource1">
                                <%--<SelectParameters>
                                    <asp:Parameter Name="OnlineUserNo" Type="Int32" DefaultValue="-99" />
                                    <asp:Parameter Name="PayLocNo" Type="Int32" DefaultValue="0" />
                                    <asp:Parameter Name="TabIndex" Type="Int32" DefaultValue="1" />
                                </SelectParameters>--%>
                            </asp:SqlDataSource>                                                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

