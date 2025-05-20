<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="CliRandomTestList.aspx.vb" Inherits="Secured_CliRandomTestList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>                        
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
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ClinicRandomTestNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="ClinicRandomTestDesc" Caption="Description" />    
                                <dx:GridViewDataTextColumn FieldName="MonthDesc" Caption="Applicable Month" />                                                                         
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year" CellStyle-HorizontalAlign="Left" />                                                   
                                <dx:GridViewDataTextColumn FieldName="HeadCount" Caption="Required Head count" CellStyle-HorizontalAlign="Left" />                                                                
                                <dx:GridViewDataTextColumn FieldName="LastDateProcess" Caption="Date Processed" />     
                                <dx:GridViewDataTextColumn FieldName="ProcessedBy" Caption="Processed By" Visible="false" />                                                           
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Detail" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
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

 <div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">
                    <h4 class="panel-title"><asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul> 
                        <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                    </Triggers>
                    </asp:UpdatePanel> 
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="ClinicRandomTestDetiNo" Width="100%">                                                                                   
                            <Columns>                          
                                <dx:GridViewDataTextColumn FieldName="ClinicRandomTestDetiCode" Caption="Transaction No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />  
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                                                           
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" /> 
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />                                                                                                        
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>  
                            <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7">
                                <PageSizeItemSettings Visible="true" Items="10, 20, 50" />        
                            </SettingsPager>
                            <SettingsContextMenu Enabled="true">                                
                                <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                            </SettingsContextMenu>                                                                                            
                            <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                            <SettingsSearchPanel Visible="true" />     
                            <Settings ShowFooter="true" />                         
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>


</asp:content>
