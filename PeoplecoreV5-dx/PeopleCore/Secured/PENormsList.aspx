<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PENormsList.aspx.vb" Inherits="Secured_PENormsList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }

    </script>
<%--<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <div>
                        <uc:Filter runat="server" ID="Filter1" EnableContent="false">
                            <Content>
                               
                            </Content>
                        </uc:Filter>
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="PENormsNo, Code" >
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEdit_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("PENormsNo") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="code" HeaderText="Reference No."  SortExpression="code"   >
                                            <HeaderStyle Width="10%"  HorizontalAlign  ="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField> 
                        
                                        <asp:BoundField DataField="PENormsCode" HeaderText="Norms Code" SortExpression="PENormsCode" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                        
                                         <asp:BoundField DataField="PENormsDesc" HeaderText="Norms Description" SortExpression="PENormsDesc" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:BoundField>                        
                                        
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="xStartDate" HeaderText="Start Date" SortExpression="StartDate" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                        
                                         <asp:BoundField DataField="xEndDate" HeaderText="End Date" SortExpression="EndDate" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                        
                                        <asp:BoundField DataField="InActive" HeaderText="Activated" SortExpression="InActive" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                        
                                        
                          
                                        <asp:TemplateField HeaderText="Details" >
                                            <ItemTemplate >
                                                <asp:ImageButton ID="btnPreview" runat="server" SkinID="grdDetail"  OnClick="lnkView_Click" CausesValidation="false" />                                   
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </mcn:DataPagerGridView>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-4">
                            <!-- Paging here -->
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
                            <!-- Button here btn-group -->
                            <div class="pull-right">
                                <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAdd_Click" ToolTip="Click here to add" ></asp:Button>
                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDelete_Click" ToolTip="Click here to delete" ></asp:Button>                       
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDelete" />
                        </div>
                    </div> 
                      
                </div>
                   
            </div>
       </div>
 </div>
 
 
 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title"><asp:Label ID="lblDetl" CssClass="lbltextsmall11-color"  runat="server" /></h4>
                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <mcn:DataPagerGridView ID="grdDetl" runat="server" AllowSorting="true" OnSorting="grdDetl_Sorting" OnPageIndexChanging="grdDetl_PageIndexChanging" DataKeyNames="PENormsNo, PENormsDetiNo" >
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditDetl" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEditDetl_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("PENormsDetiNo") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="code"  SortExpression="code" HeaderText="Detail No."    >
                                            <HeaderStyle Width="10%"  HorizontalAlign  ="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="PERatingDesc" SortExpression="PERatingDesc" HeaderText="Classification / Rating" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:BoundField>  
                                                                                                      
                                        <asp:BoundField DataField="PENormsDetiCode" SortExpression="PENormsDetiCode" HeaderText="Code" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                        
                                        <asp:BoundField DataField="PENormsDetiDesc" SortExpression="PENormsDetiDesc" HeaderText="Description" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="40%" />
                                        </asp:BoundField>
                        
                                         <asp:BoundField DataField="FromRate" SortExpression="FromRate" HeaderText="From" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>               
                        
                                        <asp:BoundField DataField="ToRate" SortExpression="ToRate" HeaderText="To" ItemStyle-HorizontalAlign="Left" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>  
                        
                                        <asp:BoundField DataField="DivisibleBy" HeaderText="Divisible By" ItemStyle-HorizontalAlign="Left" visible="false" >                            
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>  

                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" Width="4%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </mcn:DataPagerGridView>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-4">
                            <!-- Paging here -->
                            <asp:DataPager ID="dpDetl" runat="server" PagedControlID="grdDetl" PageSize="10">
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
                            <!-- Button here btn-group -->
                            <div class="pull-right">
                                <asp:Button ID="btnAddDetl" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAddDetl_Click" ToolTip="Click here to add" ></asp:Button>
                                <asp:Button ID="btnDeleteDetl" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDeleteDetl_Click" ToolTip="Click here to delete" ></asp:Button>                       
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox2" runat="server" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDeleteDetl" />
                        </div>
                    </div> 
                      
                </div>
                   
            </div>
       </div>
 </div>               
--%>
<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul> 
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                    </asp:UpdatePanel> 
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PeNormsNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="PENormsCode" Caption="Norms Code" />    
                                <dx:GridViewDataTextColumn FieldName="PENormsDesc" Caption="Norms Description" />                                                                         
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />                                                   
                                <dx:GridViewDataTextColumn FieldName="xStartDate" Caption="Start Date" />       
                                <dx:GridViewDataTextColumn FieldName="xEndDate" Caption="End Date" /> 
                                <dx:GridViewDataCheckColumn FieldName="InActive" Caption="Activated" />                                                                                                                                  
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                                                                                     
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder"/>                       
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                    <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                            </dx:ASPxCheckBox>
                                        </HeaderTemplate>
				                 </dx:GridViewCommandColumn>
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>


 <div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Label ID="lblDetl" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul> 
                        <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                    </Triggers>
                    </asp:UpdatePanel> 
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="PENormsDetiNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="PERatingDesc" Caption="Classification / Rating" />  
                                <dx:GridViewDataTextColumn FieldName="PENormsDetiCode" Caption="Code" />                                                                                                                                                                                 
                                <dx:GridViewDataTextColumn FieldName="PENormsDetiDesc" Caption="Description" />   
                                <dx:GridViewDataTextColumn FieldName="FromRate" Caption="From" /> 
                                <dx:GridViewDataTextColumn FieldName="ToRate" Caption="From" /> 
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPENormsNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Norms Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPENormsCode" runat="server" CssClass="form-control required"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Norms Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPENormsDesc" runat="server" CssClass="form-control required"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="Multiline" Rows="3" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Start Date :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtStartDate" SkinID="txtdate" runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                      
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="RangeValidator2" /> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">End Date :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtEndDate" SkinID="txtdate" runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedBirthDate" runat="server" TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtEndDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5" TargetControlID="RangeValidator1" /> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-7">
                    <label class="check">
                        <asp:Checkbox ID="txtIsActive" Visible="True"  runat="server" CssClass="icheckbox"></asp:Checkbox>
                        Tick if to be activated.
                    </label>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
             </div> 

                                                        
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>




<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="fsDetl">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveDetl" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPENormsDetiNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDetl" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Classification / Rating :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPERatingNo" DataMember="EPERating" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
           </div>

           <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPENormsdetiCode" runat="server" CssClass="form-control required"></asp:Textbox>
                </div>
            </div>

           <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPENormsDetiDesc" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="3"></asp:Textbox>
                </div>
            </div>



            <div class="form-group">
                <label class="col-md-4 control-label has-required">From :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtFromRate" runat="server" CssClass="form-control required" SkinID="txtdate"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtFromRate" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">To :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtToRate" runat="server" CssClass="form-control required" SkinID="txtdate"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtToRate" />
                </div>
            </div>  
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Divisible By :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtDivisibleBy" runat="server" CssClass="form-control" SkinID="txtdate"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtDivisibleBy" />
                </div>
            </div>          

                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>
