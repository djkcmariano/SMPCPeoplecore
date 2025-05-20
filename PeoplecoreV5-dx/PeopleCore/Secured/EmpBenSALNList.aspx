<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="EmpBenSALNList.aspx.vb" Inherits="Secured_EmpBenSALNList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>                
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                                                
                    <ul class="panel-controls">                                                        
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" Visible="false"  /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Are you sure you want to post selected record(s)?"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="SALNNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        <asp:Label runat="server" ID="lbl" Text='<%# BIND("Code") %>' Visible="false"></asp:Label>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." Width="7%" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year" Width="5%" />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Width="20%"/> 
                                <%--<dx:GridViewDataTextColumn FieldName="Income" Caption="Income" Width="10%" PropertiesTextEdit-DisplayFormatString="{0:N2}" />--%>
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date<br />Filed &nbsp;&nbsp;" Width="15%" HeaderStyle-VerticalAlign="Top" />
                                <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted<br />By" Width="15%" />
                                <dx:GridViewDataTextColumn FieldName="PostedDate" Caption="Date<br />Posted" Width="5%"  />

                                <dx:GridViewDataColumn Caption="Print" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" OnClick="lnkPrint_Click" Font-Size="Medium" OnPreRender="lnkPrint_PreRender" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn> 

                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
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