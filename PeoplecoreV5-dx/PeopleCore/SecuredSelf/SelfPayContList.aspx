<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfPayContList.aspx.vb" Inherits="SecuredSelf_SelfPayContList" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    &nbsp;
                </div>
                <div>
                    &nbsp;
                </div>                           
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayContDetiNo">                                                                                   
                            <Columns>                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Width="10%" />
                                <dx:GridViewDataTextColumn FieldName="MonthDesc" Caption="Month" />                                
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeSSS" Caption="SSS EE" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="10%" />
                                <dx:GridViewDataTextColumn FieldName="EmployerSSS" Caption="SSS ER" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="10%"  />
                                <dx:GridViewDataTextColumn FieldName="EmployeePH" Caption="PH EE" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="10%" />
                                <dx:GridViewDataTextColumn FieldName="EmployerPH" Caption="PH ER" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="10%" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeHDMF" Caption="HDMF EE" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="10%" />
                                <dx:GridViewDataTextColumn FieldName="EmployerHDMF" Caption="HDMF ER" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="10%"  /> 
                                <dx:GridViewDataTextColumn FieldName="EmployeePF" Caption="PF EE" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="10%" />
                                <dx:GridViewDataTextColumn FieldName="EmployerPF" Caption="PF ER" PropertiesTextEdit-DisplayFormatString="{0:N2}" Width="10%"  />                                                               
                            </Columns>                            
                        </dx:ASPxGridView>                                                   
                    </div>
                </div>                                                         
            </div>                                                   
        </div>
    </div>
</div>
    
</asp:Content>

