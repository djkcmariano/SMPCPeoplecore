<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="TrnCostList.aspx.vb" Inherits="Secured_TrnCostList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TrnCostNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="TrnCostCode" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="SeriesYear" Caption="Series Year"  CellStyle-HorizontalAlign="Left" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" /> 
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                                                                                     
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
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
                <div class="col-md-6">
                    <h4 class="panel-title">Transaction No.: <asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
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
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" Width="100%" KeyFieldName="TrnCostDetlNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="TrnCostDetlCode" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />  
                                <dx:GridViewDataTextColumn FieldName="InternalCost" Caption="Internal Cost" PropertiesTextEdit-DisplayFormatString="{0:N2}" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="ExternalCost" Caption="External Cost" PropertiesTextEdit-DisplayFormatString="{0:N2}" /> 
                                <dx:GridViewDataTextColumn FieldName="TotalCost" Caption="Total Cost" PropertiesTextEdit-DisplayFormatString="{0:N2}" />                                                                                                      
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>   
                            <SettingsBehavior AllowSort="true" />    
                            <Settings ShowFooter="true" />  
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="DepartmentDesc" SummaryType="Count" /> 
                                <dx:ASPxSummaryItem FieldName="InternalCost" SummaryType="Sum" />   
                                <dx:ASPxSummaryItem FieldName="ExternalCost" SummaryType="Sum" />   
                                <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" /> 
                            </TotalSummary>                           
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

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTrnCostCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Series Year :</label>
                <div class="col-md-3"> 
                    <asp:Textbox ID="txtSeriesYear" runat="server" CssClass="form-control required" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtSeriesYear" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtDescription" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="3" ></asp:Textbox>
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

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTrnCostDetlCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Department :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboDepartmentNo" DataMember="EDepartment" runat="server"  CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
               <label class="col-md-4 control-label has-space">Internal Cost :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtInternalCost" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtInternalCost" />
                </div>
            </div>

            <div class="form-group">
               <label class="col-md-4 control-label has-space">External Cost :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtExternalCost" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtExternalCost" />
                </div>
            </div>

            <div class="form-group">
               <label class="col-md-4 control-label has-space">Total Cost :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtTotalCost" CssClass="form-control" Enabled="false" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtTotalCost" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:Textbox>
                </div>
            </div>
        
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>
