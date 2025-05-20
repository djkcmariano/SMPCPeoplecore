<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" EnableEventValidation="false"   MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpFBList.aspx.vb" Inherits="Secured_EmpFBList" %>



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
                        <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="FBNo" >
                        <Columns>
                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
               
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server"   Text='<%# Bind("FBNo") %>'></asp:Label>
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

                            <asp:BoundField DataField="Code" HeaderText="Trans. No."  
                                HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left" SortExpression="Code" />
                            <asp:BoundField DataField="FBCode" HeaderText="Code"  >
                                <HeaderStyle Width="10%"  HorizontalAlign  ="Left" />
                                 <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        
                            <asp:BoundField DataField="FBDesc" HeaderText="Description"  >
                                <HeaderStyle Width="20%"  HorizontalAlign  ="Left" />
                                 <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FBDate" HeaderText="Uploaded Date" SortExpression="FBDate" ItemStyle-HorizontalAlign="Left" >                            
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>                      
                        
                            <asp:BoundField DataField="FullName" HeaderText="Uploaded By" SortExpression="FullName" ItemStyle-HorizontalAlign="Left" >
                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>
                        
                                <asp:BoundField DataField="FBTypeDesc" HeaderText="Type" SortExpression="FBTypeDesc" ItemStyle-HorizontalAlign="Left" >                            
                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                        
                                <asp:BoundField DataField="FBCategoryDesc" HeaderText="Category" SortExpression="FBCategoryDesc" ItemStyle-HorizontalAlign="Left" >                            
                                <HeaderStyle HorizontalAlign="Left" Width="12%" />
                            </asp:BoundField>
                        
                                <asp:BoundField DataField="FBClassificationDesc" HeaderText="Classification" SortExpression="FBClassificationDesc" ItemStyle-HorizontalAlign="Left" Visible="false">                            
                                <HeaderStyle HorizontalAlign="Left" Width="12%" />
                            </asp:BoundField>
                                                
                            <asp:TemplateField HeaderText="Document" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate >
                                      <asp:Linkbutton ID="lnkDocView" runat="server" OnPreRender="addTrigger_PreRender" CssClass="nktextsmall-Color" CommandName='<%# Bind("DocNo") %>' CommandArgument='<%# Bind("DocExt") %>' 
                                     OnClick="lnkDocView_Click" CausesValidation="false" Text="Preview" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="6%" />
                            </asp:TemplateField>
                        
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                <ItemTemplate >
                                    <asp:Label runat="server" ID="lblExt" Text='<%# Bind("DocExt") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                <ItemTemplate >
                                    <asp:Label runat="server" ID="lblDesc" Text='<%# Bind("DocDesc") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="Select" SortExpression="SelectIdT" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="txtIsSelect" runat="server" />
                                </ItemTemplate>                            
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                       
                        
                        </Columns>
                        <PagerSettings Mode="NextPreviousFirstLast" />
                        </mcn:DataPagerGridView>  
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
                                <asp:Button ID="lnkAdd" runat="server" CausesValidation="false" 
                                    cssClass="btn btn-default" OnClick="lnkAdd_Click"  UseSubmitBehavior="false"  Text="Add" > 
                                </asp:Button>
                                <asp:Button ID="lnkDelete" runat="server" CausesValidation="false"  UseSubmitBehavior="false" 
                                    cssClass="btn btn-default" OnClick="lnkDelete_Click" Text="Delete">
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
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction no. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFBNo" runat="server" ReadOnly="true" CssClass="form-control"
                        ></asp:Textbox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label has-required">Short description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtFBCode" CssClass="required form-control" runat="server"
                        ></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label has-required">Details description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtFBDesc" Rows="5" TextMode="MultiLine" CssClass="required form-control" runat="server" 
                        ></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboFBTypeNo" DataMember="EFBType" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Document :</label>
                <div class="col-md-7">
                    <asp:FileUpload ID="txtFile" runat="server"  Width="404px" />
                    <asp:LinkButton  ID="lnkDocView"  runat="server" OnClick="lnkDocView_Click" ></asp:LinkButton>  
                </div>
            </div>
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>

</asp:content> 