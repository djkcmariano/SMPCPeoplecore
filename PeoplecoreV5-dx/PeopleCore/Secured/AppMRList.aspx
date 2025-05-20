<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppMRList.aspx.vb" Inherits="Secured_AppMRList" Theme="PCoreStyle" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
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
                                                                                
                        <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDisapproved" OnClick="lnkDisapproved_Click" Text="Disapprove" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkSync" OnClick="lnkSnyc_Click" Text="Sync" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li> 
                        
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be recovered, Proceed?"  />                                                                               
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkApproved" ConfirmMessage="Approved transaction can not be modified. Proceed?"  />
                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkDisapproved" ConfirmMessage="Disapprove transaction can not be modified. Proceed?"  />

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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="MRNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("MRNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <%--<dx:GridViewDataColumn Caption="Comparative<br />Assessment" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" OnClick="lnkPrint_Click" Font-Size="Medium" OnPreRender="lnkPrint_PreRender" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn--%>
                                <dx:GridViewDataTextColumn FieldName="MRCode" Caption="MR No." />
                                <dx:GridViewDataDateColumn FieldName="NeededDate" Caption="Date Needed" />
                                <dx:GridViewDataTextColumn FieldName="PlantillaDesc" Caption="Plantilla No.(s)" />
                                <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                                <dx:GridViewDataTextColumn FieldName="DivisionDesc" Caption="Division" />
                                <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />
                                <dx:GridViewDataTextColumn FieldName="MRStatDesc" Caption="MR Status" />
                                <dx:GridViewDataTextColumn FieldName="QuarterDesc" Caption="Quarter" Visible = "false" />
                                <dx:GridViewDataTextColumn FieldName="DatePushed" Caption="Sync Date" />
                                <dx:GridViewDataTextColumn FieldName="publishedby" Caption="Published By" />
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveby" Caption="Approved /<br />Disapproved<br />By" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="appdate" Caption="Approved /<br />Disapproved<br />Date" Width="5%" />
                                <%--<dx:GridViewDataTextColumn FieldName="appdate" Caption="Approved /<br />Disapproved Date" Width="5%" />--%>                                
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

<%--<br />
<div class="page-content-wrap" >         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="lnkSearch_Click" />
                    </div>
                    <div>
                        <uc:Filter runat="server" ID="Filter1" />                                                                             
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="MRNo">
                                <Columns>                                                                        
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEdit_Click" CommandArgument='<%# Bind("MRNo") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MRCode" HeaderText="Transaction No." SortExpression="MRNo">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PositionDesc" HeaderText="Position" SortExpression="PositionDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DepartmentDesc" HeaderText="Department" SortExpression="DepartmentDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:BoundField>                                    
                                    <asp:BoundField DataField="MRStatDesc" HeaderText="Status" SortExpression="MRStatDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>                                    
                                    <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>                                            
                                            <asp:ImageButton ID="btnDetails" runat="server" CausesValidation="false" SkinID="grdDetail" CssClass="cancel" OnClick="btnDetails_Click" CommandArgument='<%# Bind("MRNo") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>
                                </Columns>
                            </mcn:DataPagerGridView>                           
                        </div>
                    </div>                    
                    <div class="row">
                        <div class="col-md-4">
                            <!-- Paging here -->
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="grdMain" PageSize="10">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/images/arrow_first.png" PreviousPageImageUrl="~/images/arrow_previous.png" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>Page
                                                <asp:Label ID="CurrentPageLabel" runat="server" Text="<%# IIf(Container.TotalRowCount>0,  (Container.StartRowIndex / Container.PageSize) + 1 , 0) %>" /> of
                                                <asp:Label ID="TotalPagesLabel" runat="server" Text="<%# Math.Ceiling (System.Convert.ToDouble(Container.TotalRowCount) / Container.PageSize) %>" /> (
                                                <asp:Label ID="TotalItemsLabel" runat="server" Text="<%# Container.TotalRowCount%>" /> records )
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    <asp:NextPreviousPagerField ButtonType="Image" LastPageImageUrl="~/images/arrow_last.png" NextPageImageUrl="~/images/arrow_next.png" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />                              
                                </Fields>
                            </asp:DataPager>
                        </div>
                        <div class="col-md-6 col-md-offset-2">
                            <!-- Button here -->
                            <div class="pull-right">                                
                                <asp:Button ID="btnApprove" Text="Approve" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnDisapprove" Text="Disapprove" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false"  UseSubmitBehavior="false" CssClass="btn btn-default" OnClick="btnDelete_Click" />                                
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDelete" />
                        </div>                        
                    </div>                       
                </div>                   
            </div>
       </div>
 </div>  --%>  
</asp:Content>

