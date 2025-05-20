<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="RptUserList.aspx.vb" Inherits="Secured_PEMeritList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">                                
                <div class="col-md-2">
                    
                </div>                
                 <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">                        
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                           
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkExport" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="UserReportNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" CommandArgument='<%# Bind("UserReportNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Analytic No." />                                                              
                                <dx:GridViewDataTextColumn FieldName="ReporTtitle" Caption="Title" />
                                <dx:GridViewDataTextColumn FieldName="CreatedByName" Caption="Created By" Visible="false" />   
                                <dx:GridViewDataTextColumn FieldName="CreatedDate" Caption="Date Created" Visible="false" />  
                                <dx:GridViewDataTextColumn FieldName="EncodeByName" Caption="Modified By" Visible="false" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodedDate" Caption="Date Encoded" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="DataSourceName" Caption="Datasource" Visible="false" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" />     
                            </Columns>                         
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                        

                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>




</asp:content>
