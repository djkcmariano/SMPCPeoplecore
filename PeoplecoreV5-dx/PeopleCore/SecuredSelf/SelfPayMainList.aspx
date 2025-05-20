<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfPayMainList.aspx.vb" Inherits="SecuredSelf_SelfPayMainList" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap">         
    <br />
    <div class="row">        
        <div class="col-md-4">
            <div class="row">            
            <b>Payroll Information</b>
            <table border="1" width="100%">
                <tr>
                    <td style="width:40%">Employee No.</td>
                    <td><asp:Label runat="server" ID="lblEmployeeCode" /></td>
                </tr>
                <tr>
                    <td>Employee Name</td>
                    <td><asp:Label runat="server" ID="lblFullname" /></td>
                </tr>
                <tr>
                    <td>SSS</td>
                    <td><asp:Label runat="server" ID="lblSSSNo" /></td>
                </tr>
                <tr>
                    <td>TIN</td>
                    <td><asp:Label runat="server" ID="lblTINNo" /></td>
                </tr>
                <tr>
                    <td>PhilHealth</td>
                    <td><asp:Label runat="server" ID="lblPHNo" /></td>
                </tr>
                <tr>
                    <td>HDMF</td>
                    <td><asp:Label runat="server" ID="lblHDMFNo" /></td>
                </tr>                                
            </table>
            </div> 
            <asp:Panel runat="server" ID="pPayroll">           
            <br />
            <div class="row">            
            <b>Summary</b>            
            <table border="1" width="100%">
                <tr>
                    <td style="width:40%">Transaction No.</td>
                    <td><asp:Label runat="server" ID="lblCode" /></td>
                </tr>
                <tr>
                    <td>Tax Code</td>
                    <td><asp:Label runat="server" ID="lblTaxExemptDesc" /></td>
                </tr>
                <tr>
                    <td>Payroll Period</td>
                    <td><asp:Label runat="server" ID="lblPayPeriod" /></td>
                </tr>
                <tr>
                    <td>Basic Salary</td>
                    <td><asp:Label runat="server" ID="lblCurrentSalary" /></td>
                </tr>
                <tr>
                    <td>Gross Income</td>
                    <td><asp:Label runat="server" ID="lblGross" /></td>
                </tr>
                <tr>
                    <td>Withholding Tax</td>
                    <td><asp:Label runat="server" ID="lblTaxWithheld" /></td>
                </tr>
                <tr>
                    <td>SSS Cont.</td>
                    <td><asp:Label runat="server" ID="lblEmployeeSSS" /></td>
                </tr>
                <tr>
                    <td>Pag-ibig Cont.</td>
                    <td><asp:Label runat="server" ID="lblEmployeeHDMF" /></td>
                </tr>
                <tr>
                    <td>Philhealth Cont.</td>
                    <td><asp:Label runat="server" ID="lblEmployeePH" /></td>
                </tr>
               <tr>
                    <td>PF Cont.</td>
                    <td><asp:Label runat="server" ID="lblEmployeePF" /></td>
                </tr>
                 <%--<tr>
                    <td>HF Cont.</td>
                    <td><asp:Label runat="server" ID="lblEmployeeIHP" /></td>
                </tr>--%>
                <tr>
                    <td>Total Deduction</td>
                    <td><asp:Label runat="server" ID="lblTotalDeduction" /></td>
                </tr>
                <tr>
                    <td>Net Pay</td>
                    <td><asp:Label runat="server" ID="lblNetPay" /></td>
                </tr>
            </table>            
            </div>
            <br />
            <div class="row">            
            <b>Year To Date Total</b>
            <table border="1" width="100%">
                <tr>
                    <td style="width:40%">Basic</td>
                    <td><asp:Label runat="server" ID="lblYTDTaxableIncome" /></td>
                </tr>
                <tr>
                    <td>Non-Basic</td>
                    <td><asp:Label runat="server" ID="lblYTDTaxableNonBasic" /></td>
                </tr>
                <tr>
                    <td>Taxable Bonus</td>
                    <td><asp:Label runat="server" ID="lblTotalAccumulatedIncome" /></td>
                </tr>
                <tr>
                    <td>Taxable Income</td>
                    <td><asp:Label runat="server" ID="lblYTDGross" /></td>
                </tr>
                <tr>
                    <td>Tax Exemption</td>
                    <td><asp:Label runat="server" ID="lblYTDExemption" /></td>
                </tr>
                <tr>
                    <td>Tax Withheld</td>
                    <td><asp:Label runat="server" ID="lblYTDTaxWithHeld" /></td>
                </tr>
                </table>
            </div>
            <br />
            <%--<div class="row">            
            <b>Leave</b>
            <table border="1" width="100%">
                <tr>                    
                    <td style="width:40%"></td>
                    <td style="width:30%">Vacation Leave</td>
                    <td style="width:30%">Sick Leave</td>
                </tr>
                <tr>
                    <td>Last Availed</td>
                    <td><asp:Label runat="server" ID="lblVLUsed" /></td>
                    <td><asp:Label runat="server" ID="lblSLUsed" /></td>
                </tr>
                <tr>
                    <td>Balance</td>
                    <td><asp:Label runat="server" ID="lblVL" /></td>
                    <td><asp:Label runat="server" ID="lblSL" /></td>
                </tr>                
                </table>
            </div>--%>
            </asp:Panel>
        </div>
        <div class="col-md-8">
            <div class="row">
                <div class="form-horizontal">                        
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space"><b>Pay Date :</b></label>
                        <div class="col-md-6">                            
                            <asp:DropDownList runat="server" ID="cboPayMainNo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPayMainNo_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-4">            
                            <asp:LinkButton runat="server" ID="lnkPreview" CssClass="btn btn-primary submit fsMain" Text="Preview" OnClick="lnkPayslip_Click" OnPreRender="lnkPayslip_PreRender"/>                   
                        </div>
                    </div>
                </div>
            </div>
            <br />              
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div>                                                
                            <h5>Income</h5>
                        </div>
                        <div>                            
                        </div>                           
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">                                
                                <dx:ASPxGridView ID="grdIncome" ClientInstanceName="grdIncome" runat="server" KeyFieldName="PayMainIncomeNo" Width="100%">
                                    <Columns>                            
                                        <dx:GridViewDataTextColumn FieldName="PayIncomeTypeCode" Caption="Code" />
                                        <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                        <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />                                                        
                                        <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hrs" />                                        
                                    </Columns>                            
                                </dx:ASPxGridView>
                            </div>
                        </div>
                    </div>                                                   
                </div>
            </div>
            <br />
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div>                                                
                            <h5>Deduction</h5>
                        </div>
                        <div>                            
                        </div>                           
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">                                
                                <dx:ASPxGridView ID="grdDeduction" ClientInstanceName="grdIncome" runat="server" KeyFieldName="PayMainDeductNo" Width="100%">
                                    <Columns>                            
                                        <dx:GridViewDataTextColumn FieldName="PayDeductTypeCode" Caption="Code" />
                                        <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                        <dx:GridViewDataTextColumn FieldName="LoanCode" Caption="Loan No." />
                                        <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" />                                                        
                                        <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hrs" />                                        
                                    </Columns>                            
                                </dx:ASPxGridView>                                                                                                                                 
                            </div>
                        </div>                                                                  
                    </div>                                                   
                </div>
            </div>                       
        </div>
    </div>              
</div>    
</asp:Content>

