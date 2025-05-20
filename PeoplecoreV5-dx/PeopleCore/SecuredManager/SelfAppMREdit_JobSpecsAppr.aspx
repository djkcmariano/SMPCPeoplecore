<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfAppMREdit_JobSpecsAppr.aspx.vb" Inherits="SecuredManager_SelfAppMREdit_JobSpecsAppr" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <uc:Tab runat="server" ID="Tab">
        <Header>                   
            <asp:Label runat="server" ID="lbl" />                                    
        </Header>
        <Content>
            <div class="page-content-wrap" >         
                <div class="panel-body">
                    <div class="panel-body">
                        <!-- Education -->
                        <div class="panel panel-default ">
                            <div class="panel-heading">
                                 <h4 class="panel-title">Educational Requirements</h4>                   
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <mcn:DataPagerGridView ID="grdEduc" SkinID="AllowPaging-No" runat="server" DataKeyNames="MRNo,MREducno">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdeduc" runat="server" Text='<%# Bind("MREducNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkEditEduc" runat="server" CausesValidation="false" OnClick="lnkEditEduc_Click" SkinID="grdEditbtn" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Code" Visible="false" HeaderText="Trans. No." ItemStyle-HorizontalAlign="left" SortExpression="Code">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="10%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EducTypeDesc" HeaderText="Educational Type" ItemStyle-HorizontalAlign="left" SortExpression="EducTypeDesc">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="65%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="QS?" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsQSED" runat="server" Enabled="false" Checked='<%# Bind("IsQS") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsSelected" runat="server"  Enabled='<%# Bind("IsServed") %>'/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NextPreviousFirstLast" />
                                        </mcn:DataPagerGridView>                           
                                    </div>
                                </div>                    
                                <div class="row">
                                    <div class="col-md-6">
                                        <!-- Paging here -->
                                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="grdEduc" PageSize="10">
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
                                    <div class="col-md-4 col-md-offset-2">
                                        <!-- Button here btn-group -->
                                        <div class="pull-right">                                
                                            <asp:Button ID="lnkAddEduc" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Add"></asp:Button>
                                            <asp:Button ID="lnkDeleteEduc" runat="server" CausesValidation="false" cssClass="btn btn-default" OnClick="lnkDeleteEduc_Click" Text="Delete" UseSubmitBehavior="false" />
                                        </div>
                                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDeleteEduc" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </div>                        
                                </div>                       
                            </div>            
                        </div>

                        <!-- Experience -->
                        <div class="panel panel-default ">
                            <div class="panel-heading">
                                 <h4 class="panel-title">Experience Requirements</h4>                     
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <mcn:DataPagerGridView ID="grdExpe" SkinID="AllowPaging-No" runat="server" DataKeyNames="MRNo,MRExpeNo">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" Visible="False">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdExpe" runat="server" Text='<%# Bind("MRExpeNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkEditExpe" runat="server" CausesValidation="false" OnClick="lnkEditExpe_Click" SkinID="grdEditbtn" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Code" Visible="false" HeaderText="Trans. No." ItemStyle-HorizontalAlign="left" SortExpression="Code">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="10%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ExpeTypeDesc" HeaderText="Experience Type" ItemStyle-HorizontalAlign="left" SortExpression="ExpeTypeDesc">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="65%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="QS?" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsQSEX" runat="server" Enabled="false" Checked='<%# Bind("IsQS") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="LEFT" HeaderStyle-Width="3%" 
                                                    HeaderText="Select" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsSelectEx" runat="server" Enabled='<%# Bind("IsServed") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NextPreviousFirstLast" />
                                        </mcn:DataPagerGridView>                          
                                    </div>
                                </div>                    
                                <div class="row">
                                    <div class="col-md-6">
                                        <!-- Paging here -->
                                        <asp:DataPager ID="DataPager2" runat="server" PagedControlID="grdExpe" PageSize="10">
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
                                    <div class="col-md-4 col-md-offset-2">
                                        <!-- Button here btn-group -->
                                        <div class="pull-right">                                
                                                <asp:Button ID="lnkAddExpe" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Add" OnClick="lnkAddExpe_Click"></asp:Button>
                                                <asp:Button ID="lnkDeleteExpe" runat="server" CausesValidation="false" cssClass="btn btn-default" OnClick="lnkDeleteExpe_Click" Text="Delete" UseSubmitBehavior="false" />
                                        </div>
                                        <uc:ConfirmBox runat="server" ID="cfbDeletex" TargetControlID="lnkDeleteExpe" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </div>                        
                                </div>                       
                            </div>             
                        </div>
      
                        <!-- Competency -->
                        <div class="panel panel-default ">
                            <div class="panel-heading">                       
                                 <h4 class="panel-title">Skills and Competency Requirements</h4> 
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <mcn:DataPagerGridView ID="grdComp" SkinID="AllowPaging-No" runat="server" DataKeyNames="MRNo,MRCompNo">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" Visible="False">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdComp" runat="server" Text='<%# Bind("MRCompNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkEditComp" runat="server" CausesValidation="false" OnClick="lnkEditComp_Click" SkinID="grdEditbtn" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Code" Visible="false" HeaderText="Trans. No." ItemStyle-HorizontalAlign="left" SortExpression="Code">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="10%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CompDesc" HeaderText="Competency" ItemStyle-HorizontalAlign="left" SortExpression="CompDesc">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="65%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="QS?" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsQSCO" runat="server" Enabled="false" Checked='<%# Bind("IsQS") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="1%" HeaderText="Select" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsSelectComp" runat="server"  Enabled='<%# Bind("IsServed") %>'/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NextPreviousFirstLast" />
                                        </mcn:DataPagerGridView>                           
                                    </div>
                                </div>                    
                                <div class="row">
                                    <div class="col-md-6">
                                        <!-- Paging here -->
                                        <asp:DataPager ID="DataPager3" runat="server" PagedControlID="grdComp" PageSize="10">
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
                                    <div class="col-md-4 col-md-offset-2">
                                        <!-- Button here btn-group -->
                                        <div class="pull-right">                                
                                            <asp:Button ID="lnkAddComp" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Add" OnClick="lnkAddComp_Click"></asp:Button>
                                            <asp:Button ID="lnkDeleteComp" runat="server" CausesValidation="false" cssClass="btn btn-default" OnClick="lnkDeleteComp_Click" Text="Delete" UseSubmitBehavior="false" />
                                        </div>
                                        <uc:ConfirmBox runat="server" ID="cfbDeletexx" TargetControlID="lnkDeleteComp" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </div>                        
                                </div>                       
                            </div>                   
                        </div>

                        <!-- Eligibility -->
                        <div class="panel panel-default ">
                            <div class="panel-heading">
                                 <h4 class="panel-title">Eligibility Requirements</h4> 
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <mcn:DataPagerGridView ID="grdelig" SkinID="AllowPaging-No" runat="server" DataKeyNames="MRNo,MREligNo">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" Visible="False">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdElig" runat="server" Text='<%# Bind("MREligNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkEditELig" runat="server" CausesValidation="false" OnClick="lnkEditELig_Click" SkinID="grdEditbtn" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Code" Visible="false" HeaderText="Trans. No." ItemStyle-HorizontalAlign="left" SortExpression="Code">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="10%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ExamTypeDesc" HeaderText="Examination Type" ItemStyle-HorizontalAlign="left" SortExpression="ExamTypeDesc">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="65%" />
                                                </asp:BoundField>
 
                                                 <asp:TemplateField HeaderText="QS?" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsQSEL" runat="server" Enabled="false" Checked='<%# Bind("IsQS") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsSelectElig" runat="server"  Enabled='<%# Bind("IsServed") %>'/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NextPreviousFirstLast" />
                                        </mcn:DataPagerGridView>                         
                                    </div>
                                </div>                    
                                <div class="row">
                                    <div class="col-md-6">
                                        <!-- Paging here -->
                                        <asp:DataPager ID="DataPager4" runat="server" PagedControlID="grdelig" PageSize="10">
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
                                    <div class="col-md-4 col-md-offset-2">
                                        <!-- Button here btn-group -->
                                        <div class="pull-right">                                
                                                <asp:Button ID="lnkAddElig" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Add" OnClick="lnkAddElig_Click"></asp:Button>
                                                <asp:Button ID="lnkDeleteElig" runat="server" CausesValidation="false" cssClass="btn btn-default" OnClick="lnkDeleteElig_Click" Text="Delete" UseSubmitBehavior="false" />
                                        </div>
                                        <uc:ConfirmBox runat="server" ID="cfbDeletexxxx" TargetControlID="lnkDeleteElig" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </div>                        
                                </div>                       
                            </div>                   
                        </div>
       
                        <!-- Training -->
                        <div class="panel panel-default ">
                            <div class="panel-heading">
                                 <h4 class="panel-title">Training Requirements</h4>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <mcn:DataPagerGridView ID="grdTrn" SkinID="AllowPaging-No" runat="server" DataKeyNames="MRNo,MRTrnno">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" Visible="False">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdTrn" runat="server" Text='<%# Bind("MRTrnNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkEditTrn" runat="server" CausesValidation="false" OnClick="lnkEditTrn_Click" SkinID="grdEditbtn" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Code" Visible="false" HeaderText="Trans. No." ItemStyle-HorizontalAlign="left" SortExpression="Code">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="10%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CompTrnDesc" HeaderText="Training Title" ItemStyle-HorizontalAlign="left" SortExpression="CompTrnDesc">
                                                    <HeaderStyle HorizontalAlign="LEFT" Width="65%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="QS?" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsQSTraining" runat="server"  Enabled="false" Checked='<%# Bind("IsQS") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="LEFT" HeaderStyle-Width="1%" HeaderText="Select" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="txtIsSelectTrn" runat="server" Enabled='<%# Bind("IsServed") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NextPreviousFirstLast" />
                                        </mcn:DataPagerGridView>                           
                                    </div>
                                </div>                    
                                <div class="row">
                                    <div class="col-md-6">
                                        <!-- Paging here -->
                                        <asp:DataPager ID="DataPager5" runat="server" PagedControlID="grdTrn" PageSize="10">
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
                                    <div class="col-md-4 col-md-offset-2">
                                        <!-- Button here btn-group -->
                                        <div class="pull-right">                                
                                            <asp:Button ID="lnkAddTrn" runat="server" CausesValidation="false" cssClass="btn btn-primary" Text="Add" OnClick="lnkAddTrn_Click"></asp:Button>
                                            <asp:Button ID="lnkDeleteTrn" runat="server" CausesValidation="false" cssClass="btn btn-default" OnClick="lnkDeleteTrn_Click" Text="Delete" UseSubmitBehavior="false" />
                                        </div>
                                        <uc:ConfirmBox runat="server" ID="cfbDeletexxxxx" TargetControlID="lnkDeleteTrn" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                    </div>                        
                                </div>                       
                            </div>                   
                        </div>
                    </div>   
                 </div> 
             </div>

            <asp:Button ID="btnShowEduc" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlEduc" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlpopupEduc" TargetControlID="btnShowEduc"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupEduc" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsMain">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveEduc" CssClass="fa fa-floppy-o submit fsMain btnSaveEduc" OnClick="btnSaveEduc_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMREducNo" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMREducCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Educational Type :</label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="cboEducTypeNo" runat="server" DataMember="EEducType" CssClass="required form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSEduc" runat="server" Text="&nbsp; For Qualification Standards (QS)."></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                     </fieldset>
            </asp:Panel>

            <asp:Button ID="btnShowExpe" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlExpe" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClosed" PopupControlID="pnlpopupExpe" TargetControlID="btnShowExpe"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupExpe" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsExpe">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveExpe" CssClass="fa fa-floppy-o submit fsExpe btnSaveExpe" OnClick="btnSaveExpe_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRExpeNo" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRExpeCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Experience Type :</label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="cboExpeTypeno" runat="server" DataMember="EExpetype" CssClass="required form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSExpe" runat="server" Text="&nbsp; For Qualification Standards (QS)."></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                     </fieldset>
            </asp:Panel>

            <asp:Button ID="btnShowComp" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlComp" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClosedComp" PopupControlID="pnlpopupComp" TargetControlID="btnShowComp"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupComp" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsComp">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClosedComp" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveComp" CssClass="fa fa-floppy-o submit fsComp btnSaveComp" OnClick="btnSaveComp_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRCompNo" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRCompCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Competency :</label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="cboCompNo" runat="server" DataMember="EComp" CssClass="required form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSComp" runat="server" Text="&nbsp; For Qualification Standards (QS)."></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                     </fieldset>
            </asp:Panel>

            <asp:Button ID="btnShowElig" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlElig" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClosedElig" PopupControlID="pnlpopupElig" TargetControlID="btnShowElig"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupElig" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsElig">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClosedElig" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveElig" CssClass="fa fa-floppy-o submit fsElig btnSaveElig" OnClick="btnSaveElig_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMREligNo" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMREligCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Examination Type :</label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="cboExamtypeNo" runat="server" DataMember="EExamType" CssClass="required form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSElig" runat="server" Text="&nbsp; For Qualification Standards (QS)." ></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                     </fieldset>
            </asp:Panel>

            <asp:Button ID="btnShowTrn" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mdlTrn" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClosedd" PopupControlID="pnlpopupTrn" TargetControlID="btnShowTrn"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopupTrn" runat="server" CssClass="entryPopup">
                   <fieldset class="form" id="fsTrn">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClosedd" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSaveTrn" CssClass="fa fa-floppy-o submit fsTrn btnSaveTrn" OnClick="btnSaveTrn_Click"  />      
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRTrnNo" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtMRTrnCode" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Training Title :</label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="cboCompTrnNo" runat="server" DataMember="ECompTrn" CssClass="required form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-7">
                                <asp:CheckBox ID="chkIsQSTrn" runat="server" Text="&nbsp; For Qualification Standards (QS)." ></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <br />
                     </fieldset>
            </asp:Panel>
                
        </Content>
    </uc:Tab>    
</asp:Content>

