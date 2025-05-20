<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpExitmainList.aspx.vb" Inherits="Secured_EmpExitmainList" %>



<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
    <script type="text/javascript">
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].name.indexOf("txtIsSelect") > 0 &&
            elm[i].id != theBox.id) {
                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
                }
            }

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
                    <div>
                        <uc:Filter runat="server" ID="Filter1" EnableContent="false"> 
                        </uc:Filter>
                        
                    </div>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <!-- Gridview here -->
                        <mcn:DataPagerGridView ID="grdMain" runat="server"  DataKeyNames="Applicantstandardmainno" >
                        <Columns>
                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server"   Text='<%# Bind("Applicantstandardmainno") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false"   >
                            <ItemTemplate > 
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="grdEditbtn" OnClick="lnkEdit_Click"  CausesValidation="false" />
                    
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                        </asp:TemplateField>
        
                        <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Transaction No."     >
                            <HeaderStyle Width="8%" HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                                                                      
                        <asp:BoundField DataField="applicableyear" SortExpression="applicableyear" HeaderText="Series Year"     >
                            <HeaderStyle Width="15%" HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                                                                            
                        <asp:BoundField DataField="tdesc" SortExpression="tdesc" HeaderText="Description" >
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="25%" />
                        </asp:BoundField>
                                                                            
                            <asp:BoundField  Visible="true" DataField="PositionDesc" HeaderText="Position" >
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="30%" />
                        </asp:BoundField>
                                                                            
                                                                           
                            <asp:TemplateField HeaderText="Apply to All"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="txtisapplytoall" Enabled="false" checked='<%# Bind("IsApplytoall") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                        </asp:TemplateField>
                                                       
                            <asp:TemplateField HeaderText="Details" >
                            <ItemTemplate >

                                <asp:ImageButton ID="btnPreview" runat="server" SkinID="grdPreviewbtn"  OnClick="lnkDetails_Click" CausesValidation="false" />                                   
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                        </asp:TemplateField>
                                                                            
                        <asp:TemplateField HeaderText="Select"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="txtIsSelect" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
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


    
     
      
 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading hover-link" data-toggle="collapse" data-parent="false" data-target="#collapseTable">
                    <h4 class="panel-title"><asp:Label ID="lblDetl"  runat="server"></asp:Label></h4>
                </div>
                <div id="collapseTable" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdDetl" runat="server" DataKeyNames="Applicantstandardmainno,Applicantstandardno"  >
                            <Columns>
                                <asp:TemplateField HeaderText="Id"  Visible="False"  >
         
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server"   Text='<%# Bind("Applicantstandardmainno") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id"  Visible="False"  >
                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdd" runat="server"   Text='<%# Bind("Applicantstandardno") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField  ShowHeader="false"  >
                                    <ItemTemplate >
                                         <asp:ImageButton ID="btnEditD" runat="server" SkinID="grdEditbtn" OnClick="lnkEditDetl_Click"  CausesValidation="false" />

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="code" HeaderText="Code" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Applicantdimensiontypedesc" HeaderText="Dimension Type" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                                                            
                                    <asp:BoundField DataField="Applicantstandardcode" HeaderText="Code" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                </asp:BoundField>
                                                                            
                                <asp:BoundField DataField="Applicantstandarddesc" HeaderText="Description" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                                                            
                                                                                                                                                       
                                    <asp:BoundField DataField="Standard" HeaderText="Standard Question" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                </asp:BoundField>
                                    <asp:BoundField DataField="responsetypedesc" HeaderText="Response Type" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:BoundField>
                                                                            
                                    <asp:BoundField DataField="orderlevel" HeaderText="Order Level" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                </asp:BoundField>
                                             
                                <asp:TemplateField HeaderText="Details" ><ItemTemplate >
                       
                                        <asp:ImageButton ID="lnkCaptionS" runat="server" SkinID="grdPreviewbtn"  OnClick="lnkView_Click" CausesValidation="false" /> 

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="4%" />
                                </asp:TemplateField>
                                                                            
                                    <asp:TemplateField HeaderText="Select"  >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="txtIsSelectd" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" Width="4%" />
                                </asp:TemplateField>

                                                                          
                            </Columns>
                        <PagerSettings Mode="NextPreviousFirstLast" />
                        </mcn:DataPagerGridView> 
                    </div> 
                    <div class="row">
                        <div class="col-md-4">
                            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="grdDetl" PageSize="10">
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
                                <asp:button runat="server" cssClass="btn btn-default"  ID="lnkAddD" OnClick="lnkAddD_Click"  UseSubmitBehavior="false" CausesValidation="false" Text="Add">
                                </asp:button>
                                <asp:Button ID="lnkDeleteD" runat="server" CausesValidation="false"  UseSubmitBehavior="false" 
                                        cssClass="btn btn-default"  OnClick="lnkDeleteD_Click" Text="Delete">
                                </asp:Button>
                            </div>
                            <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </div>
                    </div>       
                </div>
            </div>
       </div>
 </div>

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlpopupMain"
        CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                        <asp:TextBox ID="txtApplicantStandardmainNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txttcode" runat="server" CssClass="required form-control"></asp:TextBox>
                    </div>
            </div>
             
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txttdesc" TextMode="MultiLine" Rows="3" runat="server" CssClass="required form-control"></asp:TextBox>
                    </div>
            </div>
             
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Series year :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txtApplicableyear" runat="server" CssClass="required form-control"></asp:TextBox>
                        
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Position :</label>
                <div class="col-md-6">
                        <asp:DropdownList ID="cboPositionNo"  runat="server" DataMember="EPosition" CssClass="form-control">
                        </asp:DropdownList>
                 </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Screening type :</label>
                <div class="col-md-6">
                        <asp:DropdownList ID="cboApplicantScreenTypeNo"  runat="server" DataMember="EApplicantScreenType" CssClass="form-control">
                        </asp:DropdownList>
                    </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Order level :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txtOrderLevel" SkinID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-6">
                        <asp:CheckBox ID="txtIsApplytoall" runat="server" />&nbsp; <span >if applicable to all. </span>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Instruction :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txtInstruction" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control" ></asp:TextBox>
                 </div>
            </div>
            <br />
        </div>
        
         </fieldset>
</asp:Panel>

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlpopupDetl"
        CancelControlID="imgClosed" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsDetl">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="imgCloseD" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsDetl btnSaveDetl" OnClick="btnSaveDetl_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
              <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txtApplicantStandardNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                  </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label">Code :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txtApplicantStandardcode" runat="server" CssClass="form-control"></asp:TextBox>
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txtApplicantStandarddesc" TextMode="MultiLine" Rows="3" runat="server" CssClass="required form-control"></asp:TextBox>
                  </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Dimension Type :</label>
                <div class="col-md-6">
                        <asp:DropdownList ID="cboApplicantdimensiontypeno"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Response Type :</label>
                <div class="col-md-6">
                        <asp:DropdownList ID="cboresponsetypeno" DataMember="EResponseType"  runat="server" CssClass="form-control">
                        </asp:DropdownList>
                  </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Standard question :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txtStandard" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Order level :</label>
                <div class="col-md-6">
                        <asp:TextBox ID="txtOrderLevelD" runat="server" CssClass="form-control"></asp:TextBox>
                  </div>
            </div>
            <br />
        </div>
        
         </fieldset>
</asp:Panel>  
  
  
</asp:Content>