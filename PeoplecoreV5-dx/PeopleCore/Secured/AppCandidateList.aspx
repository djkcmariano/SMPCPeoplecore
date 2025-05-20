<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppCandidateList.aspx.vb" Inherits="Secured_AppCandidateList" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="page-content-wrap" >         
    <div class="row">                       
        <div class="col-md-3">                        
            <div class="block">
                <h4>Applicant Type</h4>
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:RadioButtonList runat="server" ID="rbl">
                                <asp:ListItem Text="&nbsp;&nbsp;External Applicant" Value="1" Selected="True" />
                                <asp:ListItem Text="&nbsp;&nbsp;Internal Applicant" Value="2" />
                            </asp:RadioButtonList>                                 
                        </div>                                                       
                    </div>
                </div>
            </div>       
        </div>
        <div class="col-md-9">
            <div class="block">
                <h4>Filter</h4>
                <div class="panel panel-default">
                    <div class="panel-body">
                        <dx:ASPxFilterControl ID="ASPxFilterControl1" runat="server" Styles-GroupType-CssClass="ontop" />
                        <br /><br />           
                        <asp:Button runat="server" ID="btnFilter" CssClass="btn btn-primary" Text="Apply" OnClick="btnFilter_Click" />
                    </div>             
                </div>            
            </div>
        </div>                             
    </div>
    <div class="row">
        <div class="panel panel-default">   
            <div class="panel-heading">
                <div class="col-md-2">
                    <h4>&nbsp;</h4>                                
                </div>
                <div>                                
                </div>                           
            </div>             
            <div class="panel-body">
                <div class="row">                        
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging">
                            <Columns>                                                                                                           
                                <asp:TemplateField HeaderText="Name" SortExpression="Fullname">
                                    <ItemTemplate >
                                        <asp:LinkButton runat="server" ID="lnk" Text='<%# Bind("Fullname") %>' CommandArgument='<%# Eval("ID") & "|" & Eval("IsApplicant") & "|" & Eval("Fullname") %>' OnClick="lnk_Click"  />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="85%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BirthAge" HeaderText="Age" SortExpression="BirthAge">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="10%"  HorizontalAlign  ="Left" />
                                </asp:BoundField>                                                
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
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
                    <div class="col-md-5 col-md-offset-3">
                        <!-- Button here -->
                        <div class="btn-group pull-right">                                                                                                
                            <div class="input-group">                                    
                                <asp:DropDownList runat="server" ID="cboMRNo" CssClass="form-control" />
                                <div class="input-group-btn">    
                                    <asp:Button runat="server" ID="btnPost" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnPost_Click" Text="Post" />
                                </div>
                            </div>
                        </div>
                        <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Are you sure you want to forward to MR?" TargetControlID="btnPost" />
                    </div>                        
                </div>                       
            </div>                   
        </div>
    </div>
 </div>

<uc:Info runat="server" ID="Info1" />

</asp:Content>
