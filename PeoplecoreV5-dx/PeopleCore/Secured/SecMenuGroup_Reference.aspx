<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecMenuGroup_Reference.aspx.vb" Inherits="Secured_SecMenuGroup_Reference" EnableEventValidation="false" %>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function GetSelectedAll(lnk) {
        var oItem = lnk.children;
        var theBox = (lnk.type == "checkbox") ? lnk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (x = 0; x < elm.length; x++)
            if (elm[x].type == "checkbox" && elm[x].name.indexOf("chkIsSelect") > 0 &&
                elm[x].id != theBox.id) {
                //elm[i].click();
                if (elm[x].checked != xState)
                    elm[x].click();
                //elm[i].checked=xState;
            }
    }

    function GetSelectedRow(lnk) {
        var ishide;
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex + 1;
        var grid = document.getElementById("<%= grdMain.ClientID %>");
        var count = grid.rows.length

        for (i = 2; i <= 9; i++) {
            if (i == rowIndex) {
                if (document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl0' + i + '_chkIsSelect') == ishide) {
                }
                else {
                    if (document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl0' + i + '_chkIsSelect').checked == true) {
                        for (j = 1; j <= 6; j++) {
                            document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl0' + i + '_txtIsSelect' + j).checked = 1;
                        }
                    }
                    else {
                        for (j = 1; j <= 6; j++) {
                            document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl0' + i + '_txtIsSelect' + j).checked = 0;
                        }
                    }
                }
            }
        }

        for (i = 10; i <= count; i++) {
            if (i == rowIndex) {
                if (document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl' + i + '_chkIsSelect') == ishide) {
                }
                else {
                    if (document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl' + i + '_chkIsSelect').checked == true) {
                        for (j = 1; j <= 6; j++) {
                            document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl' + i + '_txtIsSelect' + j).checked = 1;
                        }
                    }
                    else {
                        for (j = 1; j <= 6; j++) {
                            document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl' + i + '_txtIsSelect' + j).checked = 0;
                        }
                    }
                }
            }
        }
        return false;

    }


    function GetSelected(lnk) {
        var ishide;
        var oItem = lnk.children;
        var theBox = (lnk.type == "checkbox") ? lnk : lnk.children.item[0];
        var columnIndex = theBox.id
        var grid = document.getElementById("<%= grdMain.ClientID %>");
        var count = grid.rows.length

        for (i = 1; i <= 6; i++) {
            if (document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl01_txtSelectAll' + i) == ishide) {
            }
            else {
                if (('ctl00_cphBody_SecMenu_grdMain_ctl01_txtSelectAll' + i) == columnIndex) {
                    if (document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl01_txtSelectAll' + i).checked == true) {
                        for (j = 2; j <= 9; j++) {
                            document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl0' + j + '_txtIsSelect' + i).checked = 1;
                        }

                        for (j = 10; j <= count; j++) {
                            document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl' + j + '_txtIsSelect' + i).checked = 1;
                        }
                    }
                    else {
                        for (j = 2; j <= 9; j++) {
                            document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl0' + j + '_txtIsSelect' + i).checked = 0;
                        }

                        for (j = 10; j <= count; j++) {
                            document.getElementById('ctl00_cphBody_SecMenu_grdMain_ctl' + j + '_txtIsSelect' + i).checked = 0;
                        }
                    }
                }
            }
        }
        return false;
    }
</script> 


<uc:SecTab runat="server" ID="SecTab" />

    <uc:SecMenu runat="server" ID="SecMenu">
        <Header>         
            <asp:Label runat="server" ID="lbl" />     
        </Header>
        <Content>

<br />
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">

                    <uc:Filter runat="server" ID="Filter1" EnableContent="false">
                        <Content>
                        </Content>
                    </uc:Filter>
                                               
                </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="table-responsive">
                           <mcn:DataPagerGridView ID="grdMain" SkinID="AllowPaging-No" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="MenuGroupDetiNo,MenuMassNo,MenuGroupNo" >
                                    <Columns>
                                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server"   Text='<%# Bind("MenuGroupDetiNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMenuNo" runat="server"   Text='<%# Bind("MenuNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                                            
                                            <asp:BoundField DataField="Menutitle" HeaderText="Reference" >
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="32%" VerticalAlign="Top" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Select All"  HeaderStyle-Width="8%" 
                                                HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  >
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblchkSelectAll" runat="server"   Text="Select All" ></asp:Label><br/>
                                                    <asp:CheckBox ID="chkIsSelectAll" onclick ="GetSelectedAll(this);" VerticalAlign="Top" runat="server"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsSelect" onclick ="GetSelectedRow(this);"  runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="LEFT" />
                                                <HeaderStyle HorizontalAlign="LEFT" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Allow View"  HeaderStyle-Width="8%" 
                                                HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  >
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSelectAll1" runat="server"   Text="Allow View"></asp:Label><br/>
                                                    <asp:CheckBox ID="txtSelectAll1" onclick ="GetSelected(this);" VerticalAlign="Top" runat="server"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="txtIsSelect1" Checked='<%# bind("Viewed") %>'  runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Edit"  HeaderStyle-Width="8%" 
                                                HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  >
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSelectAll2" runat="server"   Text="Allow Edit"></asp:Label><br/>
                                                    <asp:CheckBox ID="txtSelectAll2" onclick ="GetSelected(this);" VerticalAlign="Top" runat="server"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="txtIsSelect2" Checked='<%# bind("Edited") %>'  runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Add"  HeaderStyle-Width="8%" 
                                                HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  >
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSelectAll3" runat="server"   Text="Allow Add"></asp:Label><br/>
                                                    <asp:CheckBox ID="txtSelectAll3" onclick ="GetSelected(this);" VerticalAlign="Top" runat="server"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="txtIsSelect3" Checked='<%# bind("Added") %>'  runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Delete"  HeaderStyle-Width="9%" 
                                                HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  >
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSelectAll4" runat="server"   Text="Allow Delete"></asp:Label><br/>
                                                    <asp:CheckBox ID="txtSelectAll4" onclick ="GetSelected(this);" VerticalAlign="Top" runat="server"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="txtIsSelect4" Checked='<%# bind("Deleted") %>'  runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Post"  HeaderStyle-Width="8%" 
                                                HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  >
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSelectAll5" runat="server"   Text="Allow Post"></asp:Label><br/>
                                                    <asp:CheckBox ID="txtSelectAll5" onclick ="GetSelected(this);" VerticalAlign="Top" runat="server"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="txtIsSelect5" Checked='<%# bind("Posted") %>'  runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Process"  HeaderStyle-Width="10%" 
                                                HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  >
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSelectAll6" runat="server"   Text="Allow Process"></asp:Label><br/>
                                                    <asp:CheckBox ID="txtSelectAll6" onclick ="GetSelected(this);" VerticalAlign="Top" runat="server"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="txtIsSelect6" Checked='<%# bind("Processed") %>'  runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                                    
                                        </Columns>
                                </mcn:DataPagerGridView>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-4">
                            <!-- Paging here -->
                            <asp:DataPager ID="dpMain" runat="server" PagedControlID="grdMain" PageSize="10">
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
                            <div class="btn-group pull-right">
                                <asp:Button ID="btnSave" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="Click here to sumbit" ></asp:Button>                      
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Are you sure you want to save changes?" TargetControlID="btnSave" />
                        </div>
                    </div> 
                      
                </div>
                   
            </div>
       </div>
 </div>
                
</Content>
</uc:SecMenu>

</asp:content>
