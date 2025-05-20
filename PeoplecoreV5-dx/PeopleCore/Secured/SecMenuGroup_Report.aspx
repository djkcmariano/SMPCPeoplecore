<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecMenuGroup_Report.aspx.vb" Inherits="Secured_SecMenuGroup_Report" EnableEventValidation="false" %>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">
    function SelectAllCheckboxes(spanChk) {

        // Added as ASPX uses SPAN for checkbox
        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
    spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" && elm[i].name.indexOf("chkIsEnabled") > 0 &&
        elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
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
                           <mcn:DataPagerGridView ID="grdMain" SkinID="AllowPaging-No" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="MenuReportNo,ReportNo,MenuGroupNo" >                                    
                                   <Columns>
                                        <asp:TemplateField HeaderText="Id"  Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("MenuReportNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                                                            
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >                                      
                                            <ItemTemplate>
                                                <asp:Label ID="lblReportNo" runat="server" Text='<%# Bind("ReportNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                                                            
                                        <%--<asp:BoundField DataField="menumasscode" HeaderText="Code" HeaderStyle-Width="10%"  HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left" />
                                         --%>                                       
                                        <asp:BoundField DataField="ReportTitle" HeaderText="Report" HeaderStyle-Width="90%"  HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left" />
                                                                            
                                        <asp:TemplateField HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="Center"  ItemStyle-HorizontalAlign="Center"  >
                                            <HeaderTemplate  >
                                                    <asp:Label ID="Label2" Text="Allow To Print" runat="server" ></asp:Label><br />
                                                    <asp:CheckBox ID="txtIsSelectAll" onclick ="SelectAllCheckboxes(this);"  runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate >
                                                <asp:Checkbox ID="chkIsEnabled" runat="server"  Enabled="true"  Checked='<%# Bind("IsEnabled") %>'></asp:Checkbox>
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
