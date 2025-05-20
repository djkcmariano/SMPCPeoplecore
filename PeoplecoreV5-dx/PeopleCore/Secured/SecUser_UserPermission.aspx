<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecUser_UserPermission.aspx.vb" Inherits="Secured_SecUser_UserPermission" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">

    function GetSelected(lnk, id) {

        var ishide;
        var oItem = lnk.children;
        var theBox = (lnk.type == "checkbox") ? lnk : lnk.children.item[0];
        var columnIndex = theBox.id;
        var grid = '';
        var gridName = '';
        if (id == 1) {
            grid = document.getElementById("<%= grdMain1.ClientID %>");
            gridName = 'grdMain1';
        }
        else if (id == 2) {
            grid = document.getElementById("<%= grdMain2.ClientID %>");
            gridName = 'grdMain2';
        }
        else if (id == 3) {
            grid = document.getElementById("<%= grdMain3.ClientID %>");
            gridName = 'grdMain3';
        }
        else if (id == 4) {
            grid = document.getElementById("<%= grdMain4.ClientID %>");
            gridName = 'grdMain4';
        }
        else if (id == 5) {
            grid = document.getElementById("<%= grdMain5.ClientID %>");
            gridName = 'grdMain5';
        }
        else if (id == 6) {
            grid = document.getElementById("<%= grdMain6.ClientID %>");
            gridName = 'grdMain6';
        }
        var count = grid.rows.length;

        for (i = 1; i <= 2; i++) {
            if (document.getElementById('ctl00_cphBody_' + gridName + '_ctl01_txtSelectAll' + i) == ishide) {
            }
            else {
                if (('ctl00_cphBody_' + gridName + '_ctl01_txtSelectAll' + i) == columnIndex) {
                    if (document.getElementById('ctl00_cphBody_' + gridName + '_ctl01_txtSelectAll' + i).checked == true) {
                        for (j = 2; j <= 9; j++) {
                            document.getElementById('ctl00_cphBody_' + gridName  + '_ctl0' + j + '_txtIsSelect' + i).checked = 1;
                        }

                        for (j = 10; j <= count; j++) {
                            document.getElementById('ctl00_cphBody_' + gridName + '_ctl' + j + '_txtIsSelect' + i).checked = 1;
                        }
                    }
                    else {
                        for (j = 2; j <= 9; j++) {
                            document.getElementById('ctl00_cphBody_' + gridName + '_ctl0' + j + '_txtIsSelect' + i).checked = 0;
                        }

                        for (j = 10; j <= count; j++) {
                            document.getElementById('ctl00_cphBody_' + gridName + '_ctl' + j + '_txtIsSelect' + i).checked = 0;
                        }
                    }
                }
            }
        }

        return false;

    }

</script>

<div class="page-content-wrap">
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6 panel-title">
                        <asp:Label ID="lblDetl" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
    </div>         
    <div class="row">
        <div class="col-md-6">
            <!-- START PAYROLL GROUP PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">Payroll Group Permission</h4>

                    <uc:Filter runat="server" ID="Filter1" EnableContent="False">
                        <Content>
                        </Content>
                    </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain1" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="PayClassNo,UserGrantedClassNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedClassNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("PayClassNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="PayClassDesc" HeaderStyle-Width="40%" HeaderText="Payroll Group" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="PayClassDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll1" runat="server" Text="Allow Edit"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll1" OnClick ="GetSelected(this,1);" VerticalAlign="Top" runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect1" runat="server" Checked='<%# Bind("IsAllowEdit") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,1);" VerticalAlign="Top" runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect2" runat="server" Checked='<%# Bind("IsAllowView") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="14%" />
                                        </asp:TemplateField>
                                    </Columns>
                            </mcn:DataPagerGridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                    <div class="col-md-6">
                        <!-- Paging here -->
                        <asp:DataPager ID="dpMain1" runat="server" PagedControlID="grdMain1" PageSize="10">
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
                    <div class="col-md-6">
                        <!-- Button here -->
                        <div class="btn-group pull-right">
                            <asp:Button ID="btnSave1" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave1_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                        </div>
                        <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave1" />
                    </div>
                </div> 
                      
                </div>
            </div> 
            
            <!-- START HRAN TYPE PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">HRAN Type Permission</h4>

                        <uc:Filter runat="server" ID="Filter3" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain3" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="HRANTypeNo,UserGrantedHRANTypeNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedHRANTypeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("HRANTypeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="HRANTypeDesc" HeaderStyle-Width="40%" HeaderText="HRAN Type" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="HRANTypeDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll1" runat="server" Text="View Salary"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll1" OnClick ="GetSelected(this,3);" VerticalAlign="Top" runat="server" Enabled="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect1" runat="server" Checked='<%# Bind("IsAllowEdit") %>' Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,3);" VerticalAlign="Top" runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect2" runat="server" Checked='<%# Bind("IsAllowView") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="14%" />
                                        </asp:TemplateField>
                                    </Columns>
                            </mcn:DataPagerGridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <!-- Paging here -->
                            <asp:DataPager ID="dpMain3" runat="server" PagedControlID="grdMain3" PageSize="10">
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
                        <div class="col-md-6">
                            <!-- Button here -->
                            <div class="btn-group pull-right">
                                <asp:Button ID="btnSave3" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave3_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox3" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave3" />
                        </div>
                    </div> 
                      
                </div>
            </div>   

            <!-- START INCOME TYPE PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Income Type Permission</h4>

                        <uc:Filter runat="server" ID="Filter5" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain5" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="PayIncomeTypeNo,UserGrantedIncomeTypeNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedIncomeTypeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("PayIncomeTypeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="PayIncomeTypeDesc" HeaderStyle-Width="40%" HeaderText="Income Type" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="PayIncomeTypeDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,5);" VerticalAlign="Top" runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect2" runat="server" Checked='<%# Bind("IsAllowView") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                        </asp:TemplateField>
                                    </Columns>
                            </mcn:DataPagerGridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <!-- Paging here -->
                            <asp:DataPager ID="dpMain5" runat="server" PagedControlID="grdMain5" PageSize="10">
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
                        <div class="col-md-6">
                            <!-- Button here -->
                            <div class="btn-group pull-right">
                                <asp:Button ID="btnSave5" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave5_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox5" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave5" />
                        </div>
                    </div> 
                      
                </div>
            </div>

        </div>

        <div class="col-md-6">
            <!-- START RATE PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Rate Permission</h4>

                        <uc:Filter runat="server" ID="Filter2" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain2" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="EmployeeClassNo,UserGrantedRateNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedRateNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("EmployeeClassNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="EmployeeClassDesc" HeaderStyle-Width="40%" HeaderText="Employee Class" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="EmployeeClassDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll1" runat="server" Text="Allow Edit"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll1" OnClick ="GetSelected(this,2);" VerticalAlign="Top" runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect1" runat="server" Checked='<%# Bind("IsAllowEdit") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,2);" VerticalAlign="Top" runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect2" runat="server" Checked='<%# Bind("IsAllowView") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="14%" />
                                        </asp:TemplateField>
                                    </Columns>
                            </mcn:DataPagerGridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <!-- Paging here -->
                            <asp:DataPager ID="dpMain2" runat="server" PagedControlID="grdMain2" PageSize="10">
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
                        <div class="col-md-6">
                            <!-- Button here -->
                            <div class="btn-group pull-right">
                                <asp:Button ID="btnSave2" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave2_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox2" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave2" />
                        </div>
                    </div> 
                      
                </div>
            </div>    

           <!-- START REQUEST TYPE PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Request Type Permission</h4>

                        <uc:Filter runat="server" ID="Filter4" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain4" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="RequestTypeNo,UserGrantedRequestTypeNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedRequestTypeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("RequestTypeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="RequestTypeDesc" HeaderStyle-Width="40%" HeaderText="Request Type" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="RequestTypeDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Recipient?"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,4);" VerticalAlign="Top" runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect2" runat="server" Checked='<%# Bind("IsAllowView") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                        </asp:TemplateField>
                                    </Columns>
                            </mcn:DataPagerGridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <!-- Paging here -->
                            <asp:DataPager ID="dpMain4" runat="server" PagedControlID="grdMain4" PageSize="10">
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
                        <div class="col-md-6">
                            <!-- Button here -->
                            <div class="btn-group pull-right">
                                <asp:Button ID="btnSave4" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave4_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox4" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave4" />
                        </div>
                    </div> 
                      
                </div>
            </div>  

            <!-- START DEDUCT TYPE PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Deduction Type Permission</h4>

                        <uc:Filter runat="server" ID="Filter6" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain6" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="PayDeductTypeNo,UserGrantedDeductTypeNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedDeductTypeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("PayDeductTypeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="PayDeductTypeDesc" HeaderStyle-Width="40%" HeaderText="Deduction Type" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="PayDeductTypeDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,6);" VerticalAlign="Top" runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect2" runat="server" Checked='<%# Bind("IsAllowView") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                        </asp:TemplateField>
                                    </Columns>
                            </mcn:DataPagerGridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <!-- Paging here -->
                            <asp:DataPager ID="dpMain6" runat="server" PagedControlID="grdMain6" PageSize="10">
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
                        <div class="col-md-6">
                            <!-- Button here -->
                            <div class="btn-group pull-right">
                                <asp:Button ID="btnSave6" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave6_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox6" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave6" />
                        </div>
                    </div> 
                      
                </div>
            </div>

        </div>
     </div>
 </div>
                



</asp:content>
