<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpStandardHeader_EI_Detl.aspx.vb" Inherits="Secured_EmpStandardHeader_EI_Detl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Label runat="server" ID="lbl" /> 
            
        </Header>        
        <Content>
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                    
                                <%--<li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />--%>
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="EmployeeEIDetiNo" Width="100%">
                                    <Columns>                                
                                        <dx:GridViewDataTextColumn FieldName="ApplicantStandardMainNo" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                        <dx:GridViewDataTextColumn FieldName="ApplicantStandardMainDesc" Caption="Exit Interview Process" />
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Form" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <%--<asp:LinkButton runat="server" ID="lnkForm" CssClass="fa fa-external-link" Font-Size="Medium" CommandArgument='<%# Bind("ApplicantStandardMainNo") %>' OnClick="lnkTemplate_Click" />--%>
                                                <asp:LinkButton runat="server" ID="lnkForm" CssClass="fa fa-external-link" Font-Size="Medium" CommandArgument='<%# Eval("ApplicantStandardMainNo") & "|" & Eval("EmployeeEINo") & "|" &  Eval("EmployeeNo") & "|" & Eval("IsEnabled")  %>' OnClick="lnkTemplate_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Print" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>                                                
                                                <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" Font-Size="Medium" CommandArgument='<%# Eval("ApplicantStandardMainNo") & "|" & Eval("EmployeeEINo") & "|" &  Eval("EmployeeNo") & "|" & Eval("IsEnabled")  %>' OnClick="lnkPrint_Click" OnPreRender="lnk_PreRender" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <%--<dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select"/>--%>
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

