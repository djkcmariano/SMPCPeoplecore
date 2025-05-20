<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpCompList.aspx.vb" Inherits="Secured_EmpCompList" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
   


    $(window).resize(function () {
        adjustPanelEntry();

    });

    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (args, sender) {
            adjustPanelEntry();
        });

    });   
</script>
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
    
                        <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="EmployeeCompNo"  >
                            <Columns>
                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server"   Text='<%# Bind("EmployeeCompNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField  ShowHeader="false" >
                                <ItemTemplate >
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="grdEditbtn" OnClick="lnkEdit_Click" CausesValidation="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="3%" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Code" HeaderText="Record No."  
                            HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left" SortExpression="Code" />

                            <asp:BoundField DataField="CompDesc" HeaderText="Competency"  >
                            <HeaderStyle Width="30%"  HorizontalAlign  ="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField Visible="false"  DataField="Comptypedesc" HeaderText="Type"  >
                                <HeaderStyle Width="20%"  HorizontalAlign  ="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Anchor" HeaderText="Anchor"  >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="20%"  HorizontalAlign  ="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CompScaleDesc" HeaderText="Rating"  >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="Remark"  >
                                <HeaderStyle Width="20%"  HorizontalAlign  ="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                                
                            <asp:TemplateField HeaderText="Select"  >
                                <ItemTemplate>
                                    <asp:CheckBox ID="txtIsSelect" runat="server" />
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
                                <asp:button runat="server" cssClass="btn btn-default"  ID="lnkAdd" OnClick="lnkAdd_Click"  UseSubmitBehavior="false" CausesValidation="false" Text="Add">
                                </asp:button>
                                <asp:Button ID="lnkDelete" runat="server" CausesValidation="false"  UseSubmitBehavior="false" 
                                        cssClass="btn btn-default"  OnClick="lnkDelete_Click" Text="Delete">
                                </asp:Button>
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
                <asp:LinkButton runat="server" ID="LinkButton1" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction no. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtemployeeCompNo" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label">Transaction no. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtemployeeCompCode" runat="server" ReadOnly="true" CssClass="form-control"
                        ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Competency scale :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboCompScaleNo" DataMember="ECompScale" CssClass="required form-control" runat="server" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Competency :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboCompNo" DataMember="EComp" runat="server" CssClass="required form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Anchor :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtAnchor" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control"  
                        ></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Remark :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control" 
                        ></asp:TextBox>
               </div>
            </div>
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
    </asp:Panel>
    </Content>
</uc:Tab>
</asp:content> 