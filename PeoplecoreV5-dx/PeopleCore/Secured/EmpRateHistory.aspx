<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpRateHistory.aspx.vb" Inherits="Secured_EmpRateHistory" Theme="PCoreStyle" %>

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
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeRateHistoryNo">                                                                                   
                                    <Columns>                                        
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Visible="false"/>        
                                        <dx:GridViewDataTextColumn FieldName="HRANCode" Caption="HRAN No." />                                  
                                        <dx:GridViewDataTextColumn FieldName="HRANTypeDesc" Caption="HRAN Type" />
                                        <dx:GridViewDataDateColumn FieldName="Effectivity" Caption="Effective Date" />
                                        <dx:GridViewBandColumn Caption="Old Rate" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="OldEmployeeRateClassDesc" Caption="Rate Class" />
                                                <dx:GridViewDataTextColumn FieldName="OldRate" Caption="Salary" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            </Columns>
                                        </dx:GridViewBandColumn>          
                                        <dx:GridViewBandColumn Caption="New Rate" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="NewEmployeeRateClassDesc" Caption="Rate Class" />
                                                <dx:GridViewDataTextColumn FieldName="NewRate" Caption="Salary" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            </Columns>
                                        </dx:GridViewBandColumn>   
                                        <dx:GridViewDataTextColumn FieldName="PercentIncrease" Caption="% Increase" />
                                        <dx:GridViewDataDateColumn FieldName="PayDate" Caption="Pay Date Effect" Visible="false" />     
                                        <dx:GridViewDataTextColumn FieldName="DTRCode" Caption="DTR No." Visible="false"/> 
                                        <dx:GridViewDataTextColumn FieldName="CutOff" Caption="Cut Off Date" Visible="false"/>                                                                         
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

