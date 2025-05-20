<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecUser_EncoderPermission.aspx.vb" Inherits="Secured_SecUser_EncoderPermission" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


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
        else if (id == 7) {
            grid = document.getElementById("<%= grdMain7.ClientID %>");
            gridName = 'grdMain7';
        }
        else if (id == 8) {
            grid = document.getElementById("<%= grdMain8.ClientID %>");
            gridName = 'grdMain8';
        }
        else if (id == 9) {
            grid = document.getElementById("<%= grdMain9.ClientID %>");
            gridName = 'grdMain9';
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
        <div class="col-md-6">
            <!-- START DIVISION PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">Division Permission</h4>

                    <uc:Filter runat="server" ID="Filter1" EnableContent="False">
                        <Content>
                        </Content>
                    </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain1" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="DivisionNo,UserGrantedDivisionNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedDivisionNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("DivisionNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="DivisionDesc" HeaderStyle-Width="40%" HeaderText="Division" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="DivisionDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,1);" VerticalAlign="Top" runat="server"  />
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
            
            <!-- START SECTION PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Section Permission</h4>

                        <uc:Filter runat="server" ID="Filter3" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain3" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="SectionNo,UserGrantedSectionNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedSectionNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("SectionNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="SectionDesc" HeaderStyle-Width="40%" HeaderText="Section" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="SectionDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,3);" VerticalAlign="Top" runat="server"  />
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

            <!-- START POSITION PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Position Permission</h4>

                        <uc:Filter runat="server" ID="Filter5" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain5" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="PositionNo,UserGrantedPositionNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedPositionNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("PositionNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="PositionDesc" HeaderStyle-Width="40%" HeaderText="Position" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="PositionDesc" />

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

             <!-- START Group PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Group Permission</h4>
                        <uc:Filter runat="server" ID="Filter8" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain8" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="GroupNo, UserGrantedGroupNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedGroupNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("GroupNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="GroupDesc" HeaderStyle-Width="40%" HeaderText="Sector" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="GroupDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,8);" VerticalAlign="Top" runat="server"  />
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
                            <asp:DataPager ID="DataPager3" runat="server" PagedControlID="grdMain8" PageSize="10">
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
                                <asp:Button ID="btnSave8" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave8_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox8" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave8" />
                        </div>
                    </div> 
                      
                </div>
            </div>                          
            <!-- END Group PERMISSION -->


        </div>

        <div class="col-md-6">
            <!-- START DEPARTMENT PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Department Permission</h4>

                        <uc:Filter runat="server" ID="Filter2" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain2" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="DepartmentNo,UserGrantedDepartmentNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedDepartmentNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("DepartmentNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="DepartmentDesc" HeaderStyle-Width="40%" HeaderText="Department" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="DepartmentDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,2);" VerticalAlign="Top" runat="server"  />
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

           <!-- START UNIT PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Unit Permission</h4>

                        <uc:Filter runat="server" ID="Filter4" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain4" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="UnitNo,UserGrantedUnitNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedUnitNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("UnitNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="UnitDesc" HeaderStyle-Width="40%" HeaderText="Unit" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="UnitDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
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
                       
           <!-- START HRAN TYPE PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">HRAN Type Permission</h4>
                        <uc:Filter runat="server" ID="Filter6" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain6" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="HRANTypeNo,UserGrantedHRANTypeNo" >
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
                                                <asp:CheckBox ID="txtSelectAll1" OnClick ="GetSelected(this,6);" VerticalAlign="Top" runat="server" Enabled="false" />
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
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,6);" VerticalAlign="Top" runat="server"  />
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
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="grdMain6" PageSize="10">
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
            
            
            <!-- START Facility PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Sector Permission</h4>
                        <uc:Filter runat="server" ID="Filter7" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain7" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="FacilityNo, UserGrantedFacilityNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedFacilityNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("FacilityNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="FacilityDesc" HeaderStyle-Width="40%" HeaderText="Sector" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="DepartmentDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,7);" VerticalAlign="Top" runat="server"  />
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
                            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="grdMain7" PageSize="10">
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
                                <asp:Button ID="btnSave7" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave7_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox7" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave7" />
                        </div>
                    </div> 
                      
                </div>
            </div>                          
            <!-- END Facility PERMISSION -->
             <!-- START location PERMISSION -->
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title">Location Permission</h4>
                        <uc:Filter runat="server" ID="Filter9" EnableContent="False">
                            <Content>
                            </Content>
                        </uc:Filter>                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive-vertical">
                            <div class="table-responsive" style="margin-bottom: 0px;">
                                <mcn:DataPagerGridView ID="grdMain9" runat="server" SkinID="AllowPaging-No" AllowPaging="false" DataKeyNames="LocationNo, UserGrantedLocationNo" >
                                <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                   
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransNo" runat="server"   
                                                    Text='<%# Bind("UserGrantedLocationNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"   
                                                    Text='<%# Bind("LocationNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="LocationDesc" HeaderStyle-Width="40%" HeaderText="Sector" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" SortExpression="DepartmentDesc" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSelectAll2" runat="server" Text="Allow Access"></asp:Label><br/>
                                                <asp:CheckBox ID="txtSelectAll2" OnClick ="GetSelected(this,9);" VerticalAlign="Top" runat="server"  />
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
                            <asp:DataPager ID="DataPager4" runat="server" PagedControlID="grdMain9" PageSize="10">
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
                                <asp:Button ID="btnSave9" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave9_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox9" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave9" />
                        </div>
                    </div> 
                      
                </div>
            </div>                          
            <!-- END Facility PERMISSION -->
        </div>
     </div>
 </div>
                



</asp:content>
