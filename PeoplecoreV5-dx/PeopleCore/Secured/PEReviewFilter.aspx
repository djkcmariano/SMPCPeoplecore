<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PEReviewFilter.aspx.vb" Inherits="Secured_PEReviewFilter" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Label runat="server" ID="lbl" /> 
        </Header>        
        <Content>
        <br />
        <div class="page-content-wrap">         
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
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEdit_Click" CommandArgument='<%# Bind("PEReviewFilterNo") %>' />                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="Code">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="PositionDesc" HeaderText="Position" SortExpression="PositionDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:BoundField>
                                                                  
                                    <asp:TemplateField HeaderText="Select">
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
                                <div class="pull-right">
                                    <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false"  CssClass="btn btn-default" OnClick="btnDelete_Click" />                                    
                                </div>
                                <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="btnDelete" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" />
                            </div>
                        </div>                       
                    </div>                   
                </div>
            </div>
        </div>
        
        <br />
        <div class="page-content-wrap">         
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
                                <mcn:DataPagerGridView ID="grdLevel" runat="server" AllowSorting="true" OnPageIndexChanging="grdLevel_PageIndexChanging">
                                    <Columns>                                                                        
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEditLevel_Click" CommandArgument='<%# Bind("PEReviewFilterNo") %>' />                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="Code">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="JobGradeDesc" HeaderText="Job Grade" SortExpression="JobGradeDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:BoundField>
                                                                  
                                    <asp:TemplateField HeaderText="Select">
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
                                <asp:DataPager ID="dpLevel" runat="server" PagedControlID="grdLevel" PageSize="10">
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
                                <div class="pull-right">
                                    <asp:Button ID="btnAddLevel" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAddLevel_Click" />
                                    <asp:Button ID="btnDeleteLevel" Text="Delete" runat="server" CausesValidation="false"  CssClass="btn btn-default" OnClick="btnDeleteLevel_Click" />                                    
                                </div>
                                <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="btnDeleteLevel" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" />
                            </div>
                        </div>                       
                    </div>                   
                </div>
            </div>
        </div> 
        
        <br />
        <div class="page-content-wrap">         
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
                                <mcn:DataPagerGridView ID="grdStatus" runat="server" AllowSorting="true" OnPageIndexChanging="grdStatus_PageIndexChanging">
                                    <Columns>                                                                        
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEditStatus_Click" CommandArgument='<%# Bind("PEReviewFilterNo") %>' />                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="Code">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="EmployeeStatDesc" HeaderText="Employee Status" SortExpression="EmployeeStatDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:BoundField>
                                                                  
                                    <asp:TemplateField HeaderText="Select">
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
                                <asp:DataPager ID="dpStatus" runat="server" PagedControlID="grdStatus" PageSize="10">
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
                                <div class="pull-right">
                                    <asp:Button ID="btnAddStatus" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAddStatus_Click" />
                                    <asp:Button ID="btnDeleteStatus" Text="Delete" runat="server" CausesValidation="false"  CssClass="btn btn-default" OnClick="btnDeleteStatus_Click" />                                    
                                </div>
                                <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="btnDelete" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" />
                            </div>
                        </div>                       
                    </div>                   
                </div>
            </div>
        </div> 
                                       
    </Content>        
    </uc:Tab>

    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">   
                <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPEReviewFilterNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                       
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Position :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboPositionNo" DataMember="EPosition" runat="server" CssClass="form-control required"></asp:DropdownList>
                    </div>
                </div>                
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>


    <asp:Button ID="btnShowLevel" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlLevel" runat="server" TargetControlID="btnShowLevel" PopupControlID="pnlPopupLevel" CancelControlID="lnkCloseLevel" BackgroundCssClass="modalBackground" />
    <asp:Panel id="pnlPopupLevel" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsLevel">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkCloseLevel" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveLevel" OnClick="lnkSaveLevel_Click" CssClass="fa fa-floppy-o submit lnkSaveLevel" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">   
                <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPEReviewFilterLevelNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                       
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCodeLevel" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Job Grade :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboJobGradeNo" DataMember="EJobGrade" runat="server" CssClass="form-control required"></asp:DropdownList>
                    </div>
                </div>                
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>


    <asp:Button ID="btnShowStatus" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlStatus" runat="server" TargetControlID="btnShowStatus" PopupControlID="pnlPopupStatus" CancelControlID="lnkCloseStatus" BackgroundCssClass="modalBackground" />
    <asp:Panel id="pnlPopupStatus" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsStatus">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkCloseStatus" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveStatus" OnClick="lnkSaveStatus_Click" CssClass="fa fa-floppy-o submit lnkSaveStatus" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">   
                <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPEReviewFilterStatusNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                       
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCodeStatus" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Employee Status :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control required"></asp:DropdownList>
                    </div>
                </div>                 
                <br />
            </div>                    
        </fieldset>
    </asp:Panel>

</asp:Content>

