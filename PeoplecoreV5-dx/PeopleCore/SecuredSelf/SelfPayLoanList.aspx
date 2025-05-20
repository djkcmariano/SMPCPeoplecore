<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfPayLoanList.aspx.vb" Inherits="SecuredSelf_SelfPayLoanList" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">    
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">                    
                    <asp:Dropdownlist ID="cboTabNo" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="lnkSearch_Click" />
                </div>
                <div>
                    &nbsp;
                </div>                           
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="LoanNo">                                                                                   
                            <Columns>                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="RefNo" Caption="Loan No." />                                
                                <dx:GridViewDataTextColumn FieldName="PayDeductTypeDesc" Caption="Loan Type" />
                                <dx:GridViewDataTextColumn FieldName="GrantedDate" Caption="Granted Date" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PrincipalAmount" Caption="Principal<br />Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                                <dx:GridViewDataTextColumn FieldName="BegBalance" Caption="Beginning<br />Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                                <dx:GridViewDataTextColumn FieldName="InterestRate" Caption="Interest" Visible="false" />                            
                                <dx:GridViewDataTextColumn FieldName="NoOfPayment" Caption="No. Of Payments" Visible="false"  />
                                <dx:GridViewDataTextColumn FieldName="Amort" Caption="Amortization" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="TotalPayment" Caption="Total Payment" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Balance" Caption="Balance" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                                                                                        
                            </Columns>                            
                        </dx:ASPxGridView>                                                   
                    </div>
                </div>                                    
            </div>                                                   
        </div>
    </div>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4><asp:Label runat="server" ID="lbl" /></h4>                                      
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdMain" runat="server" KeyFieldName="LoanPayNo" Width="100%">                                                                                   
                            <Columns>                                
                                <dx:GridViewDataTextColumn FieldName="CodeDetl" Caption="Detail No." />                            
                                <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." />                                                        
                                <dx:GridViewDataTextColumn FieldName="PayPeriod" Caption="Payroll Period" />
                                <dx:GridViewDataTextColumn FieldName="MonthDesc" Caption="Month" />
                                <dx:GridViewDataTextColumn FieldName="applicableYear" Caption="Year" />
                                <dx:GridViewDataTextColumn FieldName="Amort" Caption="Payment" PropertiesTextEdit-DisplayFormatString="{0:N2}"  />
                                <dx:GridViewDataTextColumn FieldName="RemarksPrepaid" Caption="Remarks" />                                                                                                                                
                            </Columns>                            
                        </dx:ASPxGridView>                                                   
                    </div>
                </div>                                                           
            </div>                                                   
        </div>
    </div>
</div>
</asp:Content>

