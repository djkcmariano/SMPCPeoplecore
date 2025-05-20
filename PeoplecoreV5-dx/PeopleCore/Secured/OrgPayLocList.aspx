<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="OrgPayLocList.aspx.vb" Inherits="Secured_OrgPayLocList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<br />
<div class="page-content-wrap">            
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    &nbsp;
                </div>
                <div>                                  
                    <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>                            
                    </ul>                                                                        
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayLocNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("PayLocNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                <dx:GridViewDataTextColumn FieldName="PayLocCode" Caption="Company Code" />
                                <dx:GridViewDataTextColumn FieldName="PayLocDesc" Caption="Company Name" />
                                <dx:GridViewDataTextColumn FieldName="Address" Caption="Address" />
                                <dx:GridViewDataTextColumn FieldName="PhoneNo" Caption="Phone No." />     
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder" />   
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Modified" Visible="false" />         
                                <dx:GridViewDataTextColumn FieldName="TINNo" Caption="TIN" Visible="false" />  
                                <dx:GridViewDataTextColumn FieldName="SSSNo" Caption="SSS No." Visible="false" />  
                                <dx:GridViewDataTextColumn FieldName="HDMFNo" Caption="HDMF No." Visible="false" />  
                                <dx:GridViewDataTextColumn FieldName="PHNo" Caption="PH No." Visible="false" />                 
                                <%--<dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />                            --%>
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
