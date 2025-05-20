<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpCheckListEdit.aspx.vb" Inherits="Secured_EmpCheckListEdit" %>


<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">


<uc:Tab runat="server" ID="Tab">
    <Header >
        <div style="text-align:center;">
            <asp:Image Width="80" Height="80" runat="server" ID="img" ImageAlign="Middle" style="text-align:center;" />
            <br />
            <asp:LinkButton runat="server" ID="lnkUploadPhoto" CausesValidation="false" Text="Upload Photo" />
        </div>
    
    <asp:Button ID="btnUpload" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlUpload" runat="server" TargetControlID="btnUpload" PopupControlID="pnlUpload"
    CancelControlID="btnCancelUpload" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>

    <asp:Panel id="pnlUpload" runat="server" CssClass="entryPopup2">
        <fieldset class="form" id="Fieldset1">
         <div class="cf popupheader">
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="btnCancelUpload" CssClass="fa fa-times cancel" ToolTip="Close" />&nbsp;
               
         </div>
        <div  class="entryPopupDetl2 form-horizontal">
            <asp:updatepanel runat="server" ID="upd">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUploadx" />
                </Triggers>
                <ContentTemplate>

                 <div class="form-group">
                    <label class="col-md-4 control-label">Filename :</label>
                    <div class="col-md-7">
                        <asp:FileUpload ID="fuPhoto" runat="server" Width="350" />
                    </div>
                </div>
                 <div class="form-group">
                    <label class="col-md-4 control-label">Transaction No. :</label>
                    <div class="col-md-7">
                            <asp:Button runat="server" ID="btnUploadx" CssClass="btn btn-default" Text="Upload" OnClick="lnkUploadSave_Click" />
                     </div>
                </div>
                </ContentTemplate>
            </asp:updatepanel>
            <br />
        </div>
       </fieldset>
    </asp:Panel>
    </Header>
    <Content> 

    <div class="page-content-wrap">         
        <div class="row">
            <div class="panel panel-default">
                 <div class="panel-heading">
                       
                </div>
                <div class="panel-body">
                    <div class="table-responsive">

                        <mcn:DataPagerGridView ID="grdMain" runat="server"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdd" runat="server"   Text='<%# Bind("EmployeeCheckListNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Code" HeaderText="Check List"  ItemStyle-HorizontalAlign="left" SortExpression="Code"  >
                                <HeaderStyle Width="10%"  HorizontalAlign  ="LEFT" />
                            </asp:BoundField>
                                                                            
                                <asp:BoundField DataField="ApplicantStandardChecklistDesc" HeaderText="Check List"  ItemStyle-HorizontalAlign="left" SortExpression="ApplicantStandardChecklistDesc"  >
                                <HeaderStyle Width="50%"  HorizontalAlign  ="LEFT" />
                            </asp:BoundField>
                                                                                                                                                        
                            <asp:TemplateField HeaderText="Submitted" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                <ItemTemplate >
                                    <asp:CheckBox ID="txtIsSubmitted" Checked='<%# Bind("IsSubmitted") %>'  runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                            </asp:TemplateField>
                                                                            
                                                                                                                                                
                        </Columns>
                        <PagerSettings Mode="NextPreviousFirstLast" />
                        </mcn:DataPagerGridView>
                    </div> 
                    <div class="row">
                        <div class="col-md-4">
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
                        <div class="col-md-6 col-md-offset-2">
                            <div class="btn-group pull-right">

                                <asp:Button runat="server"  ID="lnkSubmit2" OnClick="lnkSubmit_Click" cssClass="btn btn-default" UseSubmitBehavior="false" CausesValidation="false" Text="Submit"></asp:Button>
                            </div>
                            
                        </div>
                    </div>       
                </div>
            </div>
       </div>
 </div>
 </Content>
</uc:Tab>
</asp:content>