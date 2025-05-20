<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayMainList.aspx.vb" Inherits="Secured_PayMainList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">


<script type="text/javascript">

    function grid_ContextMenu(s, e) {
        if (e.objectType == "row")
            hiddenfield.Set('VisibleIndex', parseInt(e.index));
        pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }

</script>


<div class="page-content-wrap">   
    
    <uc:PayHeader runat="server" ID="PayHeader" />
    <uc:FormTab runat="server" ID="FormTab" />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">
                     <div class="panel-title">
                        
                    </div>               
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
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXTotal" KeyFieldName="PayMainNo"
                    OnFillContextMenuItems="MyGridView_FillContextMenuItems">                                                                                   
                        <Columns>
                            <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Info" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkInfo" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkInfo_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>  --%>  
                            <dx:GridViewDataTextColumn FieldName="code" Caption="Trans. No." />
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false"/>
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />   
                            <dx:GridViewDataTextColumn FieldName="TaxExemptCode" Caption="Tax Code" Visible="false" />                                                  
                            <dx:GridViewDataTextColumn FieldName="TotalTaxableIncome" Caption="Taxable<br />Income" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="TotalNonTaxableIncome" Caption="Non<br />Taxable" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="Gross" Caption="Gross<br />Income" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="TotalNetBasicIncome" Caption="Net Basic<br />Income" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="TaxWithheld" Caption="Tax" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="EmployeeSSS" Caption="SSS" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="GSISEEFromSuspension" Caption="SSS<br/>From Suspension" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeHDMF" Caption="HDMF" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="EmployeePH" Caption="PH" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="EmployeePF" Caption="PF" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="EmployeeIHP" Caption="HF" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="EmployerSSS" Caption="SSS BS" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="GSISERFromSuspension" Caption="SSS BS<br/>From Suspension" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="EmployerHDMF" Caption="HDMF BS" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false"  />
                            <dx:GridViewDataTextColumn FieldName="EmployerPH" Caption="PH BS" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false"  />
                            <dx:GridViewDataTextColumn FieldName="EmployerPF" Caption="PF BS" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false"  />
                            <dx:GridViewDataTextColumn FieldName="EmployerIHP" Caption="HF BS" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false"  />
                            <dx:GridViewDataTextColumn FieldName="TaxExemption" Caption="Tax<br />Exempt" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="TotalDeduction" Caption="Total<br />Deduction" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                            <dx:GridViewDataTextColumn FieldName="NetPay" Caption="Net Pay" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />                                                        
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                                    
                            <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="EmployeeRateClassDesc" Caption="Rate Class" Visible="false" />
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
                        <ClientSideEvents ContextMenu="grid_ContextMenu" /> 
                        <Settings ShowGroupFooter="VisibleIfExpanded" ShowFooter="true" />  
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="FullName" SummaryType="Count" />
                            <dx:ASPxSummaryItem FieldName="NetPay" SummaryType="Sum" />
                        </TotalSummary>                      
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />  
                    
                    <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                        <Items>
                            <dx:MenuItem Text="Report" Name="Name">
                                <Template>
                                    <asp:LinkButton runat="server" ID="lnkRptTax" OnClick="lnkRptTax_Click" CssClass="control-primary" Font-Size="small" OnPreRender="lnkPrint_PreRender" Text="Tax Annualized Computation Report" /><br />
                                </Template>
                            </dx:MenuItem>
                        </Items>
                        <ItemStyle Width="280px"></ItemStyle>
                    </dx:ASPxPopupMenu>
                    <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" /> 
                                                          
                </div>                            
            </div>
        </div>
    </div>
</div>  
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title" >
                    Name:&nbsp; <asp:Label runat="server" ID="lblDetl" />
                </div>                   
                <div>
                    <ul class="panel-controls">     
                        <li><asp:LinkButton runat="server" ID="lnkPayslip" OnClick="lnkPayslip_Click" Text="Pay Slip" CssClass="control-primary" OnPreRender="lnkPayslip_PreRender" /></li>                           
                    </ul>                                                         
                </div> 
            </div>
            <div class="panel-body">
                <div class="panel-body">
                    <div class="panel-body" style="margin-top:0px;padding-top:0px;">
                        <div class="row">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h3>Income Details</h3>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                            <ContentTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkExportIncome" OnClick="lnkExportIncome_Click" Text="Export" CssClass="control-primary pull-right" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkExportIncome" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="table-responsive">                    
                                    <dx:ASPxGridView ID="grdIncome" ClientInstanceName="grdIncome" runat="server" KeyFieldName="PayMainIncomeNo" Width="100%" >                                                                                   
                                        <Columns>                           
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />                            
                                            <dx:GridViewDataTextColumn FieldName="PayIncomeTypeCode" Caption="Code" />                                                        
                                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                            <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />                            
                                            <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hours" />
                                            <dx:GridViewDataCheckColumn FieldName="IsAccum" Caption="Accumulated" ReadOnly="true" Width="5%" />
                                            <dx:GridViewDataCheckColumn FieldName="IsTaxable" Caption="Taxable" ReadOnly="true" Width="5%" />                                                         
                                        </Columns>
                                        <SettingsPager Mode="ShowAllRecords" />                            
                                        <SettingsBehavior AllowSort="true" />    
                                        <Settings ShowFooter="true" />  
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />   
                                        </TotalSummary>                        
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExportIncome" runat="server" GridViewID="grdIncome" />

                                </div>                            
                                <br />
                         </div>

                         <div class="row">
                            <div class="row">
                                <div class="col-md-6">
                                    <h3>Deduction Details</h3>
                                </div>
                                <div class="col-md-6">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                        <ContentTemplate>
                                                <asp:LinkButton runat="server" ID="lnkExportDeduct" OnClick="lnkExportDeduct_Click" Text="Export" CssClass="control-primary pull-right" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkExportDeduct" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdDeduction" ClientInstanceName="grdDeduction" runat="server" KeyFieldName="PayMainDeductNo" Width="100%">
                                    <Columns>                           
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />                            
                                        <dx:GridViewDataTextColumn FieldName="PayDeductTypeCode" Caption="Code" />
                                        <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                        <dx:GridViewDataTextColumn FieldName="LoanCode" Caption="Loan No." />
                                        <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hours" />
                                        <dx:GridViewDataCheckColumn FieldName="IsTaxExempted" Caption="Tax Exempt" ReadOnly="true" Width="5%" />
                                    </Columns>
                                    <SettingsPager Mode="ShowAllRecords" />                            
                                    <SettingsBehavior AllowSort="true" /> 
                                    <Settings ShowFooter="true" />  
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />   
                                    </TotalSummary>                            
                                </dx:ASPxGridView> 
                                <dx:ASPxGridViewExporter ID="grdExportDeduct" runat="server" GridViewID="grdDeduction" />                                  
                            </div>                            
                    </div>
                </div>
            </div>
            </div>
        </div>
    </div>
    
        
    
</div>


</asp:Content>
