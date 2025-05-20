<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="../Masterpage/Masterpage.master" CodeFile="frmFileUploadList.aspx.vb" Inherits="Secured_frmFileUploadList" %>



<asp:content id="Content2" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <div class="col-md-2">
                            
                        </div>
                        <div >
                            <uc:Filter runat="server" ID="Filter1" EnableContent="false">
                                
                            </uc:Filter>
                        </div>
                        
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <!-- Gridview here -->
                            <mcn:DataPagerGridView  ID="grdMain" runat="server" DataKeyNames="FileDocAppNo">
                            <Columns>
                                <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lbl" runat="server" Text='<%# Bind("FileDocAppNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction No." HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                    <ItemTemplate >
                                        <asp:LinkButton ID="lnk" runat="server" Text='<%# Bind("Code") %>' OnClick="lnkEdit_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>                    
                                <asp:BoundField DataField="FileDocAppDesc" HeaderText="Description" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  />                    
                                <asp:BoundField DataField="FileDocAppActualPath" HeaderText="Actual File" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  />
                                <asp:BoundField DataField="UploadedDate" HeaderText="Date Uploaded" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left"  />
                                <asp:TemplateField HeaderText="Download File" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                    <ItemTemplate >
                                        <asp:LinkButton ID="lnkDownload"  OnPreRender="addTrigger_PreRender" runat="server" ToolTip='<%# Bind("FileDocAppPath") %>' Text="Click here..." OnClick="lnkDownload_Click">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                    <ItemTemplate >
                                        <asp:CheckBox runat="server" ID="chk" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>                    
                            </Columns>
                            <PagerSettings Mode="NextPreviousFirstLast" />
                        </mcn:DataPagerGridView >
                    </div> 
                    <div class="row">
                        <div class="col-md-4">
                            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="grdMain" PageSize="10">
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

                              <asp:button runat="server" ID="lnkAdd" cssClass="btn btn-default" UseSubmitBehavior="false" CausesValidation="false" OnClick="lnkAdd_Click" Text="Add" ></asp:button>
                         
                              <asp:button runat="server" ID="lnkDelete" cssClass="btn btn-default" UseSubmitBehavior="false" CausesValidation="false" OnClick="lnkDelete_Click" Text="Delete" ></asp:button>                          
                              </div>
                            
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </div>
                    </div>       
                </div>
            </div>
       </div>
 </div>

 <asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <asp:updatepanel runat="server" ID="upd">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                <div class="form-group">
                    <label class="col-md-4 control-label">Transaction no. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtfileNo" runat="server" ReadOnly="true" CssClass="form-control"
                            ></asp:Textbox>
                    </div>
                </div>
             
                 <div class="form-group">
                    <label class="col-md-4 control-label has-required">Details description :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtFileDesc" Rows="5" TextMode="MultiLine" CssClass="required form-control" runat="server" 
                            ></asp:TextBox>
                    </div>
                </div>
            
                 <div class="form-group">
                    <label class="col-md-4 control-label">Document :</label>
                    <div class="col-md-7">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="350" />
                        <asp:TextBox ID="txtActualFilename" Visible="false"  runat="server" Width="200px"></asp:TextBox>
                        <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="<b>Only doc, pdf, jpg or gif files are allowed.</b>" 
                        ValidationExpression="[a-zA-Z0_9].*\b(.doc|.DOC|.pdf|.PDF|.jpg|.JPG|.gif|.GIF|.txt|.DOCX|.docx)\b" ControlToValidate="FileUpload1" Display="None"></asp:RegularExpressionValidator>            
                        <ajaxToolkit:ValidatorCalloutExtender runat="server" TargetControlID="RegularExpressionValidator1" ID="ValidatorCalloutExtender1" />  
                    </div>
                </div>
                <br />
                </div>
              <!-- Footer here -->
              </ContentTemplate>
            </asp:updatepanel>
         
         </fieldset>
</asp:Panel>

</asp:content>