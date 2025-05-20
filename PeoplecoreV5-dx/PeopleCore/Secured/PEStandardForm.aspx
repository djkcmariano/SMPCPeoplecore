<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PEStandardForm.aspx.vb" Inherits="Secured_PEReviewForm" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="row">
    <div class="col-md-11">
        <uc:FormTab runat="server" ID="FormTab" />
    </div>
    <div class="col-md-1">
        <div class="btn-group pull-right">
          <button type="button" class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown">&nbsp;<span class="fa fa-cog" style="padding-top:5px;"></span></button>
          <ul class="dropdown-menu">
            <li><asp:LinkButton runat="server" ID="lnkCycle" OnClick="lnkCycle_Click" >Settings</asp:LinkButton></li>
            <li><asp:LinkButton runat="server" ID="lnkEval" OnClick="lnkCycle_Click" Visible="false">Evaluator</asp:LinkButton></li>
          </ul>
        </div>

    </div>
</div>


<div class="panel panel-default">
<asp:Panel runat="server" ID="pHeader" />


<uc:PEStandardTab runat="server" ID="PEStandardTab">

<Content>
            <br />
            <uc:ChatBox runat="server" ID="ChatBox1">
            </uc:ChatBox>

            <div class="row">
                <asp:Panel runat="server" ID="pCate" />
            </div>

            <!-- KRA here -->
            <div class="row">
                <asp:Panel runat="server" ID="pForm" />
                <div class="row">
                    <div class="panel-body">
                        <div class="panel-body">
                            <div class="pull-right">  
                                <asp:Button runat="server"  ID="lnkAdd" CssClass="btn btn-success" Text="Add New Item" OnClick="lnkAdd_Click" />
                                <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary" ValidationGroup="EvalValidationGroup" Text="Save" OnClick="lnkSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            

    </Content>
</uc:PEStandardTab>

</div>


<asp:Panel ID="pConfirmBox" runat="server" Style="display: none">
<div class="message-box animated fadeIn open">
    <div class="mb-container">
        <div class="mb-middle">
            <div class="mb-title">
                <span class="fa fa-question"></span>
                <div class="pull-left"> 
                    Confirm
                </div>
            </div>
            <div class="mb-content">    
                <div class="pull-left"> 
                    <asp:Label runat="server" ID="lblMessage" Text="Do you want to proceed?" />
                </div>
            </div>
            <div class="mb-footer">
                <div class="pull-right">                                                
                    <asp:Button runat="server" CssClass="btn btn-success btn-lg" ID="btnYes" Text="Yes" />
                    <asp:Button runat="server" CssClass="btn btn-default btn-lg" ID="btnNo" Text="Cancel" />                    
                </div>
            </div>                    
        </div>
    </div>
</div>
</asp:Panel>

<asp:Button ID="btnShowItem" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlItem" runat="server" TargetControlID="btnShowItem" PopupControlID="pnlPopupItem" CancelControlID="lnkCloseItem" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupItem" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsItem">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseItem" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveItem" OnClick="lnkSaveItem_Click"  CssClass="fa fa-floppy-o submit fsItem lnkSaveItem" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeItem" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Dimension :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPEStandardDimNo" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Order No. :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOrderLevelItem"  runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtOrderLevelItem" />
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardCode" runat="server" CssClass="form-control" Placeholder="e.g. 1,2,3... or a,b,c..."></asp:Textbox>
                 </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardDesc" runat="server" CssClass="form-control" TextMode="Multiline" Rows="2"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Standard Question :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtStandard" runat="server" CssClass="form-control required" TextMode="Multiline" Rows="2"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsRequired" Text="&nbsp; Please check here to make item require" />
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkHasComment" Text="&nbsp; Please check here to add comment box" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Response Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboResponseTypeNo" DataMember="EResponseType" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>





<asp:Button ID="btnShowChoices" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlChoices" runat="server" TargetControlID="btnShowChoices" PopupControlID="pnlPopupChoices" CancelControlID="lnkCloseChoices" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupChoices" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsChoices">
        <div class="cf popupheader">
            <h4><asp:Label runat="server" ID="lblChoices" /></h4>
            <asp:Linkbutton runat="server" ID="lnkCloseChoices" CssClass="fa fa-times" ToolTip="Close"/>
            &nbsp;<asp:Linkbutton runat="server" ID="lnkRefreshChoices" OnClick="lnkRefreshChoices_Click"  CssClass="fa fa-floppy-o" ToolTip="Save" />
        </div>
        <div  class="entryPopupDetl2 form-horizontal">
                <div class="panel panel-default">                   
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <mcn:DataPagerGridView ID="grdChoices" runat="server" OnPageIndexChanging="grdChoices_PageIndexChanging" DatakeyNames="PEStandardNo,PEStandardDetiNo,PEStandardObjTempNo">
                                    <Columns>
                                                        
                                        <asp:TemplateField ShowHeader="false" Visible="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditChoices" runat="server" CausesValidation="false" CssClass="cancel" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("PEStandardDetiNo") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDetlNo" runat="server"   Text='<%# Bind("PEStandardDetiNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>     
                                                        
                                        <asp:TemplateField HeaderText="Id"  Visible="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrder" runat="server"   Text='<%# Bind("OrderLevel") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CodeDeti" HeaderText="Order No." >
                                            <HeaderStyle Width="10%"  HorizontalAlign  ="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>  
                                                                  
                                        <asp:TemplateField HeaderText="Rating" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" Visible="false" >
                                            <ItemTemplate >                       
                                                <asp:DropDownList CssClass="form-control" ID="cboPERatingNo"   Text='<%# Bind("PERatingNo") %>' AppendDataBoundItems="True"  runat="server" DataSourceID="ObjectDataSource2" DataTextField="tDesc" DataValueField="tNo">
                                                </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Lookup_PERating" TypeName="clsLookup"></asp:ObjectDataSource>                            
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="30%"/>
                                        </asp:TemplateField> 
                                        
                                        <asp:TemplateField HeaderText="Proficiency" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                            <ItemTemplate >                       
                                                <asp:Textbox CssClass="form-control" ID="txtProfeciency" Text='<%# Bind("Profeciency") %>' runat="server"></asp:Textbox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" TargetControlID="txtProfeciency" ValidChars="-." />                                   
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%"/>
                                        </asp:TemplateField>   

                                        <asp:TemplateField HeaderText="Indicator" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign ="LEFT"  ItemStyle-HorizontalAlign="left" >
                                            <ItemTemplate >                  
                                                <asp:Textbox CssClass="form-control" ID="txtAnchor" Text='<%# Bind("Anchor") %>' runat="server"></asp:Textbox>                                 
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="30%"/>
                                        </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Select"  >
                                            <HeaderTemplate>
                                                <center>
                                                    <asp:Label ID="Label6" runat="server" Text="Select"/>
                                                    &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtIsSelectd" onclick ="chkselectdeti(this);"  runat="server" Visible="false" />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="txtdIsSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                                                                                                                                               
                                                                                        
                                    </Columns>
                                </mcn:DataPagerGridView>
                            </div>
                        </div>
                    
                        <div class="row">
                            <div class="col-md-6" style="padding-top:5px;">
                                <!-- Paging here -->
                                <asp:DataPager ID="dpChoices" runat="server" PagedControlID="grdChoices" PageSize="10">
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
                            <div class="col-md-6">
                                <!-- Button here btn-group -->
                                <div class="pull-right">
                                    <asp:Button ID="lnkSaveChoices" Text="Submit" runat="server" CausesValidation="false" CssClass="btn btn-success" OnClick="lnkSaveChoices_Click" ToolTip="Click here to add" ></asp:Button>
                                    <asp:Button ID="lnkDeleteChoices" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="lnkDeleteChoices_Click" ToolTip="Click here to delete" ></asp:Button>                       
                                </div>
                                <%--<uc:ConfirmBox ID="ConfirmBox4" runat="server" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDeleteChoices" />--%>
                            </div>
                        </div> 
                      
                    </div>
                                                 
                </div>
        </div>
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowSetting" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlSetting" runat="server" TargetControlID="btnShowSetting" PopupControlID="pnlPopupSetting" CancelControlID="lnkCloseSetting" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupSetting" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsSetting">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseSetting" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveSetting" OnClick="lnkSaveSetting_Click"  CssClass="fa fa-floppy-o submit fsSetting lnkSaveSetting" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Evaluator :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPEEvaluatorNo" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Performance Cycle :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPECycleNo" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

            
            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowCate" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlCate" runat="server" TargetControlID="btnShowCate" PopupControlID="pnlPopupCate" CancelControlID="lnkCloseCate" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupCate" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsCate">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseCate" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveCate" OnClick="lnkSaveCate_Click"  CssClass="fa fa-floppy-o submit fsCate lnkSaveCate" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">
            
            <div class="form-group" style="visibility:hidden;position:absolute;">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsDistributeWeight" Text="&nbsp; Tick here to distribute the weight percentage" runat="server" /><br />
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Maximum number of item(s) :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtNoOfItems" runat="server" CssClass="form-control" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtNoOfItems" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Item :</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsAddCate" Text="&nbsp; Allow Add " runat="server" /><br />
                    <asp:CheckBox ID="txtIsDeleteCate" Text="&nbsp; Allow Delete " runat="server" /><br />
                    <asp:CheckBox ID="txtIsEditCate" Text="&nbsp; Allow Edit " runat="server" /><br />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Indicator :</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsAddCateDeti" Text="&nbsp; Allow Add " runat="server" /><br />
                    <asp:CheckBox ID="txtIsDeleteCateDeti" Text="&nbsp; Allow Delete " runat="server" /><br />
                    <asp:CheckBox ID="txtIsEditCateDeti" Text="&nbsp; Allow Edit " runat="server" /><br />
                </div>
            </div>

            

        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowDim" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDim" runat="server" TargetControlID="btnShowDim" PopupControlID="pnlPopupDim" CancelControlID="lnkCloseDim" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupDim" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsDim">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseDim" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDim" OnClick="lnkSaveDim_Click" CssClass="fa fa-floppy-o submit fsDim lnkSaveDim" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Item</label>
                <div class="col-md-3">
                    <asp:CheckBox ID="txtIsAddDim" Text="&nbsp; Allow Add " runat="server" /><br />
                    <asp:CheckBox ID="txtIsDeleteDim" Text="&nbsp; Allow Delete " runat="server" /><br />
                    <asp:CheckBox ID="txtIsEditDim" Text="&nbsp; Allow Edit " runat="server" /><br />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Indicator</label>
                <div class="col-md-3">
                    <asp:CheckBox ID="txtIsAddDimDeti" Text="&nbsp; Allow Add " runat="server" /><br />
                    <asp:CheckBox ID="txtIsDeleteDimDeti" Text="&nbsp; Allow Delete " runat="server" /><br />
                    <asp:CheckBox ID="txtIsEditDimDeti" Text="&nbsp; Allow Edit " runat="server" /><br />
                </div>
            </div>

        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

        
</asp:Content>

