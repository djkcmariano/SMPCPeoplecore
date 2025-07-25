﻿<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayMainList_Deduct.aspx.vb" Inherits="Secured_PayMainList_OD" %>

<asp:Content id="Content2" contentplaceholderid="cphBody" runat="server">


<div class="page-content-wrap">     
    <uc:PayHeader runat="server" ID="PayHeader" />    
    <uc:FormTab runat="server" ID="FormTab" />
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                          <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />     
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkExportCL" OnClick="lnkExportCL_Click" Text="Export Coop Loan" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                        
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExport" />
                                <asp:PostBackTrigger ControlID="lnkExportCL" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayMainDeductNo">                                                                                   
                                <Columns>                            
                                    
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayDeductTypeDesc" Caption="Deduction Type" />
                                    <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hrs" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataComboBoxColumn FieldName="SourceDeductTypeDesc" Caption="Payroll Component" />
                                    <dx:GridViewDataTextColumn FieldName="tCode" Caption="Reference No." Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="GCash" Caption="GCash No." Visible="false" />  
                                    <dx:GridViewDataTextColumn FieldName="FamBankAccountNo" Caption="Family Allotment Bank No" Visible="false" />    
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


