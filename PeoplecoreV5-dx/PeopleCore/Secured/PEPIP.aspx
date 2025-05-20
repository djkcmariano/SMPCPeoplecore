<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" Theme="PCoreStyle" CodeFile="PEPIP.aspx.vb" Inherits="Secured_PEPIP" %>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }

    </script>

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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEPIPNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="empname" Caption="Name of Employee" />    
                                <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />                                                                         
                                <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />                                                   
                                <dx:GridViewDataTextColumn FieldName="immediatename" Caption="Immediate Superior" />                                                                                                                    
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
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="PEPIPDetiNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="Goals" Caption="Goals" />  
                                <dx:GridViewDataTextColumn FieldName="Target" Caption="Target" />                                                                                                                                                                                 
                                <dx:GridViewDataTextColumn FieldName="Result" Caption="Result" />   
                                <dx:GridViewDataTextColumn FieldName="TargetDate" Caption="Target Date" /> 
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
                    <asp:Textbox ID="txtPEPIPNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" onblur="ResetEmployee()" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="1" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function Split(obj, index) {
                             var items = obj.split("|");
                             for (i = 0; i < items.length; i++) {
                                 if (i == index) {
                                     return items[i];
                                 }
                             }
                         }
                         function ResetEmployee() {
                             if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                 document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                                 document.getElementById('<%= cboPositionNo.ClientID %>').value = "";
                                 document.getElementById('<%= cboDepartmentNo.ClientID %>').value = "";
                                 document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = "";
                                 document.getElementById('<%= txtimmediatename.ClientID %>').value = "";
                                 
                             }
                         }
                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                             document.getElementById('<%= cboDepartmentNo.ClientID %>').value = Split(eventArgs.get_value(), 1);
                             document.getElementById('<%= cboPositionNo.ClientID %>').value = Split(eventArgs.get_value(), 6);
                             document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = Split(eventArgs.get_value(), 13);
                             document.getElementById('<%= txtimmediatename.ClientID %>').value = Split(eventArgs.get_value(), 14);

                         }
                     </script>
                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Position :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPositionNo" DataMember="EPosition" runat="server" CssClass=" form-control" >
                        </asp:Dropdownlist>
                    </div>
             </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Department :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboDepartmentNo" DataMember="EDepartment" runat="server" CssClass=" form-control" >
                        </asp:Dropdownlist>
                    </div>
             </div> 

            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Immediate Head :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtimmediatename" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifImmediateSuperiorNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                        TargetControlID="txtimmediatename" MinimumPrefixLength="2" 
                        CompletionInterval="500" ServiceMethod="PopulateManager" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecordI" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                         <script type="text/javascript">
                             function getRecordI(source, eventArgs) {
                                 document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = eventArgs.get_value();
                             }
                        </script>                                                                    
                    </div>
                </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date From :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtDateFrom" SkinID="txtdate" runat="server" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDateFrom" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                      
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="RangeValidator2" /> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date To :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtDateTo" SkinID="txtdate" runat="server" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedBirthDate" runat="server" TargetControlID="txtDateTo" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDateTo" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5" TargetControlID="RangeValidator1" /> 
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
                    <asp:Textbox ID="txtPEPIPDetiNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDetl" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Goals :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtGoals" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="3"></asp:Textbox>
                </div>
            </div>

           <div class="form-group">
                <label class="col-md-4 control-label has-required">Target :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtTarget" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="3"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Result :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtResult" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="3"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Target Date :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtTargetDate" SkinID="txtdate" runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTargetDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtTargetDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtTargetDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator3" /> 
                </div>
            </div>
   
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>
