<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PEStandardMainDetiList.aspx.vb" Inherits="Secured_PENormsList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">


<div class="page-content-wrap">     
<uc:FormTab runat="server" ID="FormTab" />    
     <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">                    
                    
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXTotal" KeyFieldName="PEStandardCateNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="PECateTypeDesc" Caption="Performance Type" />
                            <dx:GridViewDataTextColumn FieldName="PECateDesc" Caption="Category" />
                            <dx:GridViewDataTextColumn FieldName="PEStandardCateCode" Caption="Code" />
                            <dx:GridViewDataTextColumn FieldName="PEStandardCateDesc" Caption="Description" />
                            <dx:GridViewDataTextColumn FieldName="RemarksCate" Caption="Remarks" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="Weighted" Caption="Weighted" CellStyle-HorizontalAlign="Left" FooterCellStyle-HorizontalAlign="Left" />
                            <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order No." CellStyle-HorizontalAlign="Left" />
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                        </Columns>  
                        <Settings ShowGroupFooter="VisibleIfExpanded" ShowFooter="true" /> 
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="Weighted" SummaryType="Sum" />
                        </TotalSummary>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>

 </div>
 


<div class="page-content-wrap" id="divCore" runat="server" visible="false">     
       <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-7">
                    <h4 class="panel-title"><asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                            <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" Width="100%" KeyFieldName="PEStandardDimNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="PEDimensionTypeDesc" Caption="Dimension Type" />
                            <dx:GridViewDataTextColumn FieldName="PEStandardDimCode" Caption="Code" />
                            <dx:GridViewDataTextColumn FieldName="PEStandardDimDesc" Caption="Description" />
                            <dx:GridViewDataTextColumn FieldName="Weighted" Caption="Weighted" CellStyle-HorizontalAlign="Left" FooterCellStyle-HorizontalAlign="Left" />
                            <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order No." CellStyle-HorizontalAlign="Left" />
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                        </Columns>              
                        <SettingsPager Mode="ShowAllRecords" />    
                        <Settings ShowGroupFooter="VisibleIfExpanded" ShowFooter="true" /> 
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="Weighted" SummaryType="Sum" />
                        </TotalSummary>            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />                                        
                </div>                            
            </div>
        </div>
    </div>

</div>


<div class="page-content-wrap" id="divObj" runat="server" visible="false">         
       <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-7">
                    <h4 class="panel-title"><asp:Label ID="lblObj" runat="server"></asp:Label></h4>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAddObj" OnClick="lnkAddObj_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                            <li><asp:LinkButton runat="server" ID="lnkDeleteObj" OnClick="lnkDeleteObj_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExportObj" OnClick="lnkExportObj_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteObj" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportObj" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdObj" ClientInstanceName="grdObj" runat="server" Width="100%" KeyFieldName="PEStandardObjNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEditObj" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditObj_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order No." CellStyle-HorizontalAlign="Left" />
                            <dx:GridViewDataTextColumn FieldName="Description" Caption="Column Name" />
                            <dx:GridViewDataTextColumn FieldName="ObjectiveTypeLDesc" Caption="Data Type" />
                            <dx:GridViewDataTextColumn FieldName="PEEvaluatorDesc" Caption="Responsible" />
                            <dx:GridViewDataTextColumn FieldName="PECycleDesc" Caption="Performance Cycle" />
                            <dx:GridViewDataTextColumn FieldName="ResponseTypeDesc" Caption="Response Type" />
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetailObj" CssClass='<%# Bind("Icon") %>' Enabled='<%# Bind("IsChoices") %>' Font-Size="Medium" OnClick="lnkDetailObj_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                        </Columns>        
                        <SettingsPager Mode="ShowAllRecords" />                     
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExportObj" runat="server" GridViewID="grdObj" />                                        
                </div>                            
            </div>
        </div>
    </div>

 </div>   




<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardCateNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeCate" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

           

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Performance Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPECateTypeNo" runat="server" DataMember="EPECateType" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboPECateTypeNo_SelectedIndexChanged" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required" runat="server" id="lblCate">Category :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPECateNo" runat="server" DataMember="EPECate" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboPECateNo_SelectedIndexChanged" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardCateCode" runat="server" CssClass="form-control" Placeholder="e.g. 1,2,3... or A,B,C..." ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardCateDesc" runat="server" CssClass="form-control" TextMode="Multiline" Rows="3" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Maximum number of item(s) :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtNoOfItems" runat="server" CssClass="form-control" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtNoOfItems" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required" runat="server" id="lblWeighted">Weighted :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtWeightedCate" runat="server" CssClass="form-control required" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtWeightedCate" />
                </div>
            </div>

             <div class="form-group">
                <label class="col-md-4 control-label has-required">Order No. :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOrderLevelCate" runat="server" CssClass="form-control required" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtOrderLevelCate" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks / Instructions :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemarksCate" runat="server" CssClass="form-control" TextMode="Multiline" Rows="3" ></asp:Textbox>
                </div>
            </div>
                                                   
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsDetl">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveDetl" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardDimNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDim" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Dimension Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPEDimensionTypeNo" DataMember="EPEDimensionType" runat="server" CssClass="form-control required"  AutoPostBack="true" OnSelectedIndexChanged="cboPEDimensionTypeNo_SelectedIndexChanged"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardDimCode" runat="server" CssClass="form-control" Placeholder="e.g. 1,2,3... or A,B,C..."></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardDimDesc" runat="server" CssClass="form-control" TextMode="Multiline" Rows="3"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks / Instructions :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemarksDim" runat="server" CssClass="form-control" TextMode="Multiline" Rows="3"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Weighted :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtWeightedDim" runat="server" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtWeightedDim" />
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Order No. :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOrderLevelDim"  runat="server" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="txtOrderLevelDim" />
                 </div>
            </div>

        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>




<asp:Button ID="btnShowObj" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlObj" runat="server" TargetControlID="btnShowObj" PopupControlID="pnlPopupObj" CancelControlID="lnkCloseObj" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupObj" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsObj">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseObj" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveObj" OnClick="lnkSaveObj_Click" CssClass="fa fa-floppy-o submit fsObj lnkSaveObj" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEStandardObjNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeObj" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Order No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOrderLevelObj"  runat="server" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtOrderLevelObj" />
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Column Name :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDescription" runat="server" CssClass="form-control required" ></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Data Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboObjectiveTypeNo" DataMember="EObjectiveTypeL" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Responsible :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPEEvaluatorNo" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Performance Cycle :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPECycleNo" DataMember="EPECycle" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Response Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboResponseTypeNo" DataMember="EResponseType" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

             <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsRequired" Text="&nbsp;Please check here to make item require" />
                </div>
            </div>  
  
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowObjDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlObjDetl" runat="server" TargetControlID="btnShowObjDetl" PopupControlID="pnlPopupObjDetl" CancelControlID="lnkCloseObjDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupObjDetl" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsObjDetl">
        <div class="cf popupheader">
            <h4><asp:Label runat="server" ID="lblChoices" /></h4>
            <asp:Linkbutton runat="server" ID="lnkCloseObjDetl" CssClass="fa fa-times" ToolTip="Close" />
        </div>
        <div  class="entryPopupDetl2 form-horizontal">
                <div class="panel panel-default">                
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <mcn:DataPagerGridView ID="grdObjDetl" runat="server" OnPageIndexChanging="grdObjDetl_PageIndexChanging" DataKeyNames="PEStandardObjNo,PEStandardObjDetiNo" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblObjNo" runat="server"   Text='<%# Bind("PEStandardObjNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDetlNo" runat="server"   Text='<%# Bind("PEStandardObjDetiNo") %>'></asp:Label>
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
                                                    <asp:Label ID="Label6" runat="server" Text="Select" /><br />
                                                </center>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelectObjDetl" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Top" />
                                        </asp:TemplateField>                                        
                                    </Columns>
                                </mcn:DataPagerGridView>
                            </div>
                        </div>
                    
                        <div class="row">
                            <div class="col-md-6">
                                <!-- Paging here -->
                                <asp:DataPager ID="dpObjDetl" runat="server" PagedControlID="grdObjDetl" PageSize="5">
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
                                    <asp:Button ID="btnSaveObjDetl" Text="Submit" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSaveObjDetl_Click" ToolTip="Click here to add" ></asp:Button>
                                    <asp:Button ID="btnDeleteObjDetl" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDeleteObjDetl_Click" ToolTip="Click here to delete" ></asp:Button>                       
                                </div>
                                <uc:ConfirmBox ID="ConfirmBox6" runat="server" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDeleteObjDetl" />
                            </div>
                        </div> 
                      
                    </div>
                                                 
                </div>
        </div>
        <div class="cf popupfooter">
        </div> 
    </fieldset>
</asp:Panel>



</asp:content>
