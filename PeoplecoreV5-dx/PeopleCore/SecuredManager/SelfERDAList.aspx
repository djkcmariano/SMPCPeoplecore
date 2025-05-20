<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfERDAList.aspx.vb" Inherits="SecuredManager_SelfERDAList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                         <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" Visible="false" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul> 
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DANo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                <dx:GridViewDataTextColumn FieldName="DACode" Caption="Case No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ReceivedDate" Caption="Date Received" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />         
                                <dx:GridViewBandColumn Caption="Offense" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="ViolationDate" Caption="Date" />
                                        <dx:GridViewDataTextColumn FieldName="DAPolicyTypeDesc" Caption="DA Policy Type" />
                                        <dx:GridViewDataTextColumn FieldName="DAPolicyDesc" Caption="DA Policy" />
                                        <dx:GridViewDataTextColumn FieldName="DACaseTypeDesc" Caption="Case Type" Visible="false"/>
                                        <dx:GridViewDataTextColumn FieldName="OffenseCount" Caption="Count" />
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <%--<dx:GridViewBandColumn Caption="Penalty Imposed" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataComboBoxColumn FieldName="DATypeDesc" Caption="Penalty Type" /> 
                                        <dx:GridViewDataTextColumn FieldName="StartDate" Caption="Start Date" />                                                                
                                        <dx:GridViewDataTextColumn FieldName="EndDate" Caption="End Date" /> 
                                    </Columns>
                                </dx:GridViewBandColumn>  --%>    
                                <dx:GridViewBandColumn Caption="Penalty" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataComboBoxColumn FieldName="DATypeDesc" Caption="Imposed" /> 
                                        <dx:GridViewDataTextColumn FieldName="IsServed" Caption="Served" />                                                                
                                    </Columns>
                                </dx:GridViewBandColumn>        
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />                                                                                                                             
                                <dx:GridViewDataColumn Caption="Print"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print control-primary" Font-Size="Medium" OnClick="lnkPrint_Click" OnPreRender="lnkPrint_PreRender" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
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