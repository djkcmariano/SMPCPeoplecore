<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpHRANListTransfer.aspx.vb" Inherits="Secured_EmpHRANListTransfer" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <div class="col-md-2">
                            <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkGo_Click" style="width:200px;" CssClass="form-control" runat="server" 
                                    ></asp:Dropdownlist>
                        </div>
                        <div >
                            <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                                <Content>
                                     <div class="form-group">
                                        <label class="col-md-4 control-label">Filter By :</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control"  AutoPostBack="true"  OnSelectedIndexChanged="cbofilterby_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            </div>
                                        </div>
                                       
		                                <div class="form-group">
                                            <label class="col-md-4 control-label">Filter Value :</label> 
                                            <div class="col-md-8">
                                                    <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control">
                                                    </asp:DropDownList>
                                            </div>
                                        </div>   
                                       
	                            </Content>
                            </uc:Filter>
                        </div>
                        
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <!-- Gridview here -->
                            <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="HRANNo,HRANCode">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("HRANNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" 
                                                OnClick="lnkEdit_Click" SkinID="grdEditbtn" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="HRANCode" HeaderText="HRAN No." 
                                        SortExpression="HRANCode">
                                    <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fullname" HeaderText="Employee Name" 
                                        SortExpression="Fullname">
                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="hrantypedesc" HeaderText="HRAN Type" 
                                        SortExpression="hrantypedesc">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmployeeStatDesc" HeaderText="Employee Status" 
                                        SortExpression="EmployeeStatDesc" Visible="false">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PositionDesc" HeaderText="Position" 
                                        SortExpression="PositionDesc">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Effectivity" HeaderText="Effectivity" 
                                        SortExpression="Effectivity">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" 
                                        SortExpression="DueDate">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tStatus" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkCaptionx" runat="server" CausesValidation="false" 
                                                OnClick="lnkView_Click" SkinID="grdPreviewbtn" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Preview" Visible="false" >
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkPreview" runat="server" CausesValidation="false" 
                                                OnClick="btnApply_Click" SkinID="grdPreviewbtn" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsSelect" runat="server" 
                                                Enabled='<%# Bind("IsEnabled") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Mode="NextPreviousFirstLast" />
                            </mcn:DataPagerGridView>
                        </div> 
                    <div class="row">
                        <div class="col-md-4">
                            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="grdMain" PageSize="10">
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
                            <div class="btn-group pull-right">  
                                <asp:Button ID="lnkPost" runat="server" CausesValidation="false"  
                                    cssClass="btn btn-default" OnClick="lnkPost_Click"  UseSubmitBehavior="false" Text="Post to Employee" > 
                                </asp:Button>
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="false" 
                                    cssClass="btn btn-default" OnClick="lnkAdd_Click"  UseSubmitBehavior="false"  Text="Add" > 
                                </asp:Button>
                                <asp:Button ID="lnkDelete" runat="server" CausesValidation="false"  UseSubmitBehavior="false" 
                                    cssClass="btn btn-default" OnClick="lnkDelete_Click" Text="Delete">
                                </asp:Button>
                            </div>
                            <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified. Proceed?"  />
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </div>
                    </div>       
                </div>
            </div>
       </div>
 </div>

 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading hover-link" data-toggle="collapse" data-parent="false" data-target="#collapseTable">
                    <h4 class="panel-title"><asp:Label ID="lblDetl" CssClass="lbltextsmall11-color"  runat="server"></asp:Label></h4>
                </div>
                <div id="collapseTable" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdAppr" runat="server"  SkinID="grdDetl"
                            DataKeyNames="HRANApprovalRoutingNo,HRANNo">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdd" runat="server" 
                                            Text='<%# Bind("HRANApprovalRoutingNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Code" HeaderText="Trans. No.">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fullname" HeaderText="Approved By">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HRANApprovalTypeDesc" HeaderText="Approval Type">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ApproveDate" HeaderText="Date Approved">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Remark" HeaderText="Remark">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrderNo" HeaderText="Order No">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="txtIsSelectd" runat="server" 
                                            Enabled='<%# Bind("IsEnabled") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                        </mcn:DataPagerGridView>
                    </div> 
                    <div class="row">
                        <div class="col-md-4">
                            <asp:DataPager ID="DataPager3" runat="server" PagedControlID="grdAppr" PageSize="10">
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
                            <div class="btn-group pull-right">
                                <%--<asp:Button ID="lnkAddAppr" runat="server" CausesValidation="false" UseSubmitBehavior="false" 
                                    cssClass="btn btn-default" OnClick="lnkAddAppr_Click"  Text="Add">
                                </asp:Button>
                                <asp:Button ID="lnkDeleteAppr" runat="server" CausesValidation="false"  UseSubmitBehavior="false" 
                                    cssClass="btn btn-default"  OnClick="lnkDeleteAppr_Click" Text="Delete">
                                </asp:Button>--%>
                            </div>
                            <%--<uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkDeleteAppr" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />--%>
                        </div>
                    </div>       
                </div>
            </div>
       </div>
 </div>
   

</asp:Content>