<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="TrnPositionList.aspx.vb" Inherits="Secured_TrnPositionList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTab_Click" ></asp:Dropdownlist>
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                    
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="btnAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="btnDelete_Click" Text="Delete" CssClass="control-primary" /></li>
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="TrnPositionNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click"  CommandArgument='<%# Bind("TrnPositionNo") %>'/>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>                            
                                    <dx:GridViewDataTextColumn FieldName="TrnPositionCode" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />                                                                           
                                    <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" /> 
                                    <dx:GridViewDataComboBoxColumn FieldName="SeriesYear" Caption="Series Year" /> 
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Copy" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkCopy" CssClass="fa fa-copy" Font-Size="Medium" OnClick="lnkCopy_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>  
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
 </div>
 


<div class="page-content-wrap" > 
    <div class="col-md-12 bhoechie-tab-container" > 
        <div class="panel panel-default" style="margin-bottom:0px;">
             <div class="panel-heading">
                <h4 class="panel-title">Transaction No.: <asp:Label ID="lblMain" runat="server"></asp:Label></h4>

                <div id="divTrnBtn" runat="server">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                        <ContentTemplate>                    
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAddR" OnClick="lnkAddR_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDeleteR" OnClick="lnkDeleteR_Click" Text="Delete" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkExportTrn" OnClick="lnkExportTrn_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul> 
                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteR" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExportTrn" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </div> 


                <div id="divEmpBtn" runat="server" visible="false">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                        <ContentTemplate>                    
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAddE" OnClick="lnkAddE_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDeleteE" OnClick="lnkDeleteE_Click" Text="Delete" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkExportE" OnClick="lnkExportE_Click" Text="Export" CssClass="control-primary" /></li>
                            </ul> 
                            <uc:ConfirmBox runat="server" ID="ConfirmBox3" TargetControlID="lnkDeleteE" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                   
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExportE" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </div>  

             </div>  

             <div class="col-md-2 bhoechie-tab-menu">
                <div class="list-group">
                    <%--Select Category--%>
                    <asp:LinkButton ID="lnkTrn" OnClick="lnkTrn_Click" runat="server" CssClass="list-group-item active text-left" Text="Required Training(s)"></asp:LinkButton>
                    <%--Select Fix Rating--%>
                    <asp:LinkButton ID="lnkEmp" OnClick="lnkEmp_Click" runat="server" CssClass="list-group-item text-left" Text="Excluded Employee(s)"></asp:LinkButton>
                </div>
            </div>

             <div class="col-md-10 bhoechie-tab" style=" border-left:1px solid #e5e5e5;">                  

             <div class="panel-body">
             
              <div class="page-content-wrap" id="divTrn" runat="server">   
                    <%--Required Training--%>      
                    <div class="row">
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdTrn" ClientInstanceName="grdTrn" runat="server" Width="100%" KeyFieldName="TrnPositionDetlNo">                                                                                   
                                        <Columns>                         
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                            <dx:GridViewDataTextColumn FieldName="TrnTitleDesc" Caption="Training Title" />                                                                           
                                            <dx:GridViewDataTextColumn FieldName="Hrs" Caption="No. of Hrs." Visible="false" />                                                                                      
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select"  Width="5%" />
                                        </Columns>                            
                                    </dx:ASPxGridView>


                                    <dx:ASPxGridViewExporter ID="grdExportTrn" runat="server" GridViewID="grdTrn" />                             
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>

              </div> 

              <div class="page-content-wrap" id="divEmp" runat="server" visible="false"> 
                   <%--Excluded Employee--%>  
                  <div class="row">
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdEmp" ClientInstanceName="grdEmp" runat="server" Width="100%" KeyFieldName="TrnPositionEmpNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditE_Click"  CommandArgument='<%# Bind("TrnPositionEmpNo") %>'/>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>                            
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                                                           
                                                                                                                             
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
                                        </Columns>                            
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExportE" runat="server" GridViewID="grdEmp" />
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
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
                    <asp:Textbox ID="txtTrnPositionCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Position :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPositionNo" DataMember="EPosition" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Department :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboDepartmentNo" DataMember="EDepartment" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Series Year :</label>
                <div class="col-md-3"> 
                    <asp:Textbox ID="txtSeriesYear" runat="server" CssClass="form-control required" ></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" TargetControlID="txtSeriesYear" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                </div>
            </div>
       
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

<asp:Button ID="btntrn" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdtrn" runat="server" TargetControlID="btntrn" PopupControlID="pnltrn" 
CancelControlID="lnkclosetrn" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnltrn" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="Fieldset2">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkclosetrn" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveTrn" OnClick="btnSaveTrn_Click" CssClass="fa fa-floppy-o submit fsMain lnkSaveTrn" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTrnPositionDetlNo" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Training Title :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboTrnTitleNo" DataMember="ETrnTitle" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">No. of Hours :</label>
                <div class="col-md-3">                        
                    <asp:TextBox ID="txtHrs" runat="server" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtHrs" />
                </div>
            </div>
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

<asp:Button ID="btnEmp" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdEmp" runat="server" TargetControlID="btnEmp" PopupControlID="pnlemp" 
CancelControlID="lnkclosee" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlemp" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="Fieldset3">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkclosee" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveEmp" OnClick="btnSaveEmp_Click" CssClass="fa fa-floppy-o submit fsMain lnkSaveTrn" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTrnPositionEmpNo" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" CompletionSetCount="1" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                         }
                     </script>
                    
                </div>
            </div>
            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

<asp:Button ID="btnShowCopy" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlCopy" runat="server" TargetControlID="btnShowCopy" PopupControlID="pnlPopupCopy" CancelControlID="lnkCloseCopy" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupCopy" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="Fieldset1">
         <div class="cf popupheader">
              <h4>Copy Training(s)</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseCopy" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveCopy" OnClick="lnkSaveCopy_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">
            <%--<div class="form-group">
                <label class="col-md-4 control-label has-space">To Transaction No. :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboTrnPositionToNo" runat="server" CssClass="form-control" Enabled="false"></asp:DropdownList>
                </div>
            </div>--%>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">From Transaction No. :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboTrnPositionFromNo" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>
       
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>
