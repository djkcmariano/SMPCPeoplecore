<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterApp.master" AutoEventWireup="false" CodeFile="frmFileUpload.aspx.vb" Inherits="SecuredApp_frmFileUpload" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">
                    <h4 class="panel-title"><asp:Label runat="server" ID="lblTrans" /></h4>                    
                </div>
                <div>
                    <uc:Filter runat="server" ID="Filter1" />                                                    
                </div>                           
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging">
                        <Columns>                                                                                                
                        <asp:BoundField DataField="Code" HeaderText="Detail No." SortExpression="DocNo">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DocDesc" HeaderText="Description" SortExpression="DocDesc">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DocFile" HeaderText="Filename" SortExpression="DocFile">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="30%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkDownload" OnClick="lnkDownload_Click" CssClass="fa fa-download" Font-Size="Large" CommandArgument='<%# Bind("DocNo") %>' />
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
                            <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDelete_Click" />
                        </div>
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="btnDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" />
                    </div>
                </div>                       
            </div>                   
        </div>
    </div>
</div>
<asp:UpdatePanel runat="server" ID="upUpload">
<ContentTemplate>
<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDocDesc" runat="server" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Filename :</label>
                <div class="col-md-7">
                    <asp:FileUpload runat="server" ID="fuDoc" Width="100%" CssClass="required" />
                </div>
            </div>                        
        </div>
        <br /><br />
    </fieldset>
</asp:Panel>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="lnkSave" />
</Triggers>
</asp:UpdatePanel> 
</asp:Content>

