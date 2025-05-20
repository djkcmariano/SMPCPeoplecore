<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpAllowHistory.aspx.vb" Inherits="Secured_EmpAllowHistory" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
    <Header>
        <center>
            <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />                
        </center>            
        <asp:Label runat="server" ID="lbl" /> 
    </Header>
    <Content>            
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            <h3>&nbsp;</h3>
                        </div>
                        <div>                                                
                            &nbsp;                                                                                                                                                    
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeAllowanceHistoryNo">                                                                                   
                                    <Columns>                                        
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Visible="false"/> 
                                        <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Allowance Type" />                                        
                                        <dx:GridViewBandColumn Caption="Old Allowance" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="OldStartDate" Caption="Old StartDate" />
                                                <dx:GridViewDataTextColumn FieldName="OldEndDate" Caption="Old EndDate" />
                                                <dx:GridViewDataTextColumn FieldName="OldAmount" Caption="Old AMount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            </Columns>
                                        </dx:GridViewBandColumn>          
                                        <dx:GridViewBandColumn Caption="New Allowance" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="StartDate" Caption="Start Date" />
                                                <dx:GridViewDataTextColumn FieldName="EndDate" Caption="End Date" />
                                                <dx:GridViewDataTextColumn FieldName="CurrentAmount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            </Columns>
                                        </dx:GridViewBandColumn>
                                        <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Pay Date Effect" />     
                                        <dx:GridViewDataTextColumn FieldName="DTRCode" Caption="DTR No." />    
                                        <dx:GridViewDataTextColumn FieldName="IsRetro" Caption="Retro?"/>                                                                         
                                    </Columns>                            
                                </dx:ASPxGridView>                                
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>            
    </Content>
</uc:Tab>    
</asp:Content>

