<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CarJDEditKnownledge.aspx.vb" Inherits="Secured_CarJDEditKnownledge" Theme="PCoreStyle" %>

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
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="grdEditbtn" OnClick="btnEdit_Click" CausesValidation="false" CommandArgument='<%# Eval("JDKAreaNo") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="JDKAreaNo">                            
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                            </asp:BoundField>                                                                                                                                                        
                                            <asp:BoundField DataField="CompKAreaDesc" HeaderText="Knowledge Area" SortExpression="CompKAreaDesc">                            
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="40%" />
                                            </asp:BoundField>                                                         
                                            <asp:BoundField DataField="JDKAreaDesc" HeaderText="Description" SortExpression="JDKAreaDesc">                            
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                            </asp:BoundField>                                                                                                                                                                                                  
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
                                    <div class="btn-group pull-right">
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
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Eligibility Type :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboCompKAreaNo" DataMember="ECompKArea" runat="server" CssClass="form-control required" />                        
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Average :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtJDKAreaDesc" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                </div><br /><br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

