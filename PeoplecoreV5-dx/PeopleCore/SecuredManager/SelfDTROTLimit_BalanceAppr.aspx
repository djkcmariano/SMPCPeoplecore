<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="SelfDTROTLimit_BalanceAppr.aspx.vb" Inherits="SecuredManager_SelfDTROTLimit_BalanceAppr" %>

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
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
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
                                    <label class="col-md-4 control-label">Applicable Month :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="cboApplicableMonth" DataMember="EMonth"  CssClass="form-control" />
                                    
                                    </div>
                                </div>
		                        <div class="form-group">
                                    <label class="col-md-4 control-label">Applicable Year :</label> 
                                    <div class="col-md-8">
                                        <asp:Textbox runat="server" ID="txtApplicableYear" CssClass="form-control" />
                                    </div>
                                
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
                                    
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" />
                                    <dx:GridViewDataTextColumn FieldName="MonthCode" Caption="Month" />

                                    <dx:GridViewBandColumn Caption="OT (Claims)" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="MaxHrs" Caption="(A)<br/>Limit" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                            <dx:GridViewDataTextColumn FieldName="Used" Caption="(B)<br/>Used" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                            <dx:GridViewDataTextColumn FieldName="Balance" Caption="(C = A - B)<br/>Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        </Columns>
                                    </dx:GridViewBandColumn>  

                                    <dx:GridViewBandColumn Caption="Compensatory Time-Off (CTO)" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="MaxHrsCTO" Caption="(A)<br/>Limit" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                            <dx:GridViewDataTextColumn FieldName="UsedCTO" Caption="(B)<br/>Used" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                            <dx:GridViewDataTextColumn FieldName="BalanceCTO" Caption="(C = A - B)<br/>Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        </Columns>
                                    </dx:GridViewBandColumn>  


<%--                                    <dx:GridViewBandColumn Caption="A" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewBandColumn Caption="Limit" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="MaxHrs" Caption="OT" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                                    <dx:GridViewDataTextColumn FieldName="MaxHrsCTO" Caption="CTO" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                        </Columns>
                                    </dx:GridViewBandColumn>   
                                    <dx:GridViewBandColumn Caption="B" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewBandColumn Caption="Used" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Used" Caption="OT" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                                    <dx:GridViewDataTextColumn FieldName="UsedCTO" Caption="CTO" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                        </Columns>
                                    </dx:GridViewBandColumn>   
                                    <dx:GridViewBandColumn Caption="C = A - B" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <dx:GridViewBandColumn Caption="Balance" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Balance" Caption="OT" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                                    <dx:GridViewDataTextColumn FieldName="BalanceCTO" Caption="CTO" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                        </Columns>
                                    </dx:GridViewBandColumn> --%>

                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="OT Claims" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>

                                </Columns>
                                <SettingsSearchPanel Visible="false" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                                        
                                                                                  
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6 panel-title">
                        Name: <asp:Label ID="lblDetl" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                         <p><asp:Label ID="lblDate" runat="server"></asp:Label></p>    
                    </div>
              
                     <div class="row">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <h3>OT Claims</h3>
                                </div>
                                <div class="col-md-6">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                        <ContentTemplate>
                                                <asp:LinkButton runat="server" ID="lnkExportClaims" OnClick="lnkExportClaims_Click" Text="Export" CssClass="control-primary pull-right" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkExportClaims" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl1" ClientInstanceName="grdDetl1" runat="server" SettingsSearchPanel-Visible="false" KeyFieldName="EmployeeNo" Width="100%">                                                                                   
                                <Columns>
                                    <dx:GridViewDataDateColumn FieldName="DTRCode" Caption="DTR No." />
                                    <dx:GridViewDataDateColumn FieldName="DTRDate" Caption="DTR Date" />
                                    <dx:GridViewDataTextColumn FieldName="DayDesc" Caption="Day" />
                                    <dx:GridViewDataTextColumn FieldName="DayTypeCode" Caption="Day Type" />
                                    <dx:GridViewDataComboBoxColumn FieldName="ShiftCode" Caption="Shift" />
                                    <dx:GridViewDataTextColumn FieldName="OTClaim" Caption="OT Claim (Hrs)" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="OTCTO" Caption="OT CTO (Hrs)" PropertiesTextEdit-DisplayFormatString="{0:N3}" />
                                </Columns> 
                                <SettingsContextMenu Enabled="true">                                
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                                <SettingsSearchPanel Visible="false" />  
                                <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7" Mode="ShowAllRecords">
                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />        
                                </SettingsPager>
                                <Settings ShowFooter="true" />  
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="OTClaim" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="OTCTO" SummaryType="Sum" />
                                </TotalSummary>                           
                            </dx:ASPxGridView> 

                            <dx:ASPxGridViewExporter ID="grdExportClaims" runat="server" GridViewID="grdDetl1" />
                        </div>
                        </div>
                    </div>

                </div>
                   
            </div>
       </div>
      

                   

 </div>

</asp:Content>