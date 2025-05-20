<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpListing.aspx.vb" Inherits="Secured_EmpListing" Theme="PCoreStyle" EnableEventValidation="false" %>

<%--<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>--%>
<%@ Register Src="~/Include/wucFilterGeneric.ascx" TagName="Filter" TagPrefix="wuc" %>
<%@ Register Src="~/Include/FileUpload.ascx" TagName="FileUpload" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="page-content-wrap">
        <div class="row">
                <uc:FilterSearch runat="server" ID="FilterSearch1" EnableContent="false" EnableFilter="true" FilterName="EmployeeFilter" >
                </uc:FilterSearch>
        </div>   
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
                                        <asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Migrate 201" CssClass="control-primary"  Visible="true"/>
                                    </li>
                                </ul>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExport" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <%--<uc:Filter runat="server" ID="Filter1" EnableContent="true" >
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
                        </uc:Filter>--%>
                        
                    </div>
                </div>
                <div class="panel-body">
                    <%--<div class="row">
                        <asp:GridView runat="server" ID="grdM" DataSourceID="SqlDataSource1" AutoGenerateColumns="true" EmptyDataText="no record found" DataKeyNames="EmployeeNo" />
                    </div>--%>
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="GrdNoSearch" KeyFieldName="EmployeeNo">
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
                                    <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group/District" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" />
                                    <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section/Store" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="ProjectDesc" Caption="Project" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Staus" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="GenderDesc" Caption="Gender" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="CivilStatDesc" Caption="Civil Status" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group"/>
                                    <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="DateHired" Caption="Date Hired" />
                                    <dx:GridViewDataTextColumn FieldName="DateHired" Caption="FDS" ToolTip="First Day in Service" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="RegularizedDate" Caption="Date of<br />Regularization" />
                                    <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="TINNo" Caption="TIN" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="SSSNo" Caption="SSS" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="PHNo" Caption="PhilHealth" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="HDMFNo" Caption="HDMF" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="SeparatedDate" Caption="Separated Date " Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="BankTypeDesc" Caption="Bank Type" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="BankAccountNo" Caption="Bank Account No." Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="TaxExemptDesc" Caption="Tax Code" Visible="false" />                                    
                                    <dx:GridViewDataTextColumn FieldName="FPId" Caption="FPId" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="LastLoyaltyDate" Caption="Last Loyalty<br/>Anniversary Date" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="OTParameterDesc" Caption="OT Parameter" Visible="false" /> 
                                    <dx:GridViewDataColumn Caption="Attachment" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkAttachment" OnClick="lnkAttachment_Click" CssClass="fa fa-paperclip " CommandArgument='<%# Eval("EmployeeNo") & "|" & Eval("Fullname")  %>' Font-Size="Medium" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
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
    <uc:FileUpload runat="server" ID="FileUpload" />
<asp:UpdatePanel runat="server" ID="upUpload">
<ContentTemplate>
<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Record No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>
           
            <div class="form-group">
                <label class="col-md-4 control-label has-required">File name :</label>
                <div class="col-md-7">
                    <asp:FileUpload runat="server" ID="fuDoc" Width="100%" CssClass="required" />
                </div>
            </div>                        
        </div>
        <br /><br />
    </fieldset>
</asp:Panel>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="lnkSave" />
</Triggers>
</asp:UpdatePanel>   
</asp:Content>


