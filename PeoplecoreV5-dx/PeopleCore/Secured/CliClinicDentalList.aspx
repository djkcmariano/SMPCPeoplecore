<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CliClinicDentalList.aspx.vb" Inherits="Secured_CliClinicDentalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
         <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />            
            </center>            
            <asp:Label runat="server" ID="lbl" />        
        </Header>
        <Content>
            <br />
            <asp:Panel id="Panel1" runat="server">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <%--<div class="panel-heading">    
                                <h4 class="panel-title">Dental Profile</h4>                   
                            </div>--%>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                    <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="ClinicLocToothNo">
                                        <Columns>
                                        
                                        <asp:TemplateField HeaderText="ID" Visible="False">                                                                        
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("ClinicLocToothNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px"  />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="ID" Visible="False">                                                                        
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# Bind("ClinicLocToothTypeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px"  />
                                        </asp:TemplateField>                              
   
                                        <asp:BoundField DataField="ClinicLocToothTypeDesc" HeaderText="Location of Tooth">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Tooth Condition" Visible="true">                                                                        
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtToothCondition" Text='<%# Bind("ToothCondition") %>' runat="server" 
                                                Width="100%"  TextMode="MultiLine" Rows="2" ></asp:TextBox>                       
                                            </ItemTemplate>
                                            <HeaderStyle Height="30%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                                                    
                                        <asp:TemplateField HeaderText="Operation Performed" Visible="true">                                                                        
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOperationPerformed" Text='<%# Bind("OperationPerformed") %>' runat="server" 
                                                Width="100%"  TextMode="MultiLine" Rows="2" ></asp:TextBox>
                                                                                
                                            </ItemTemplate>
                                            <HeaderStyle Height="30%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Details" Visible="true">                                                                        
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDetails" Text='<%# Bind("Details") %>' runat="server" 
                                                Width="100%"  TextMode="MultiLine" Rows="2" ></asp:TextBox>
                                                                                
                                            </ItemTemplate>
                                            <HeaderStyle Height="30%" HorizontalAlign="Center"  />
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
                                        <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click"></asp:Button>          
                                        <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>                      
                        </div>                   
                    </div>                    
                    </div>
                </div>
            </asp:Panel>
            
        </Content>
    </uc:Tab>
</asp:Content>

