<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfEmpEI_Clearance.aspx.vb" Inherits="SecuredSelf_SelfEmpEI_" %>

<%@ Register Src="~/Include/wucFilterGeneric.ascx" TagName="Filter" TagPrefix="wuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" runat="server">
<div class="page-content-wrap" >         
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-7">
                 
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" Visible="true">
                           <ContentTemplate>
                                <ul class="panel-controls">
                                    
                                    <li>
                                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    </li>
                                </ul>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExport" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeEIClearanceNo">                                                                                   
                                <Columns>
                                    
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="IfullName" Caption="In-charge" />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeClearanceTypeDesc" Caption="Item Accountability" />
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Item Amount" /> 
                                    <dx:GridViewDataTextColumn FieldName="DateReturned" Caption="Date Returned" /> 
                                    <dx:GridViewDataCheckColumn FieldName="IsCleared" Caption="Returned" />                                                                              
                                   
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