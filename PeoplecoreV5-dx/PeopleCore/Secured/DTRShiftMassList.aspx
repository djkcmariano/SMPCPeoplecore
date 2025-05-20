<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="DTRShiftMassList.aspx.vb" Inherits="Secured_DTRShiftMassList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

     <script type="text/javascript">
         function cbCheckAll_CheckedChanged(s, e) {
             grdDetl.PerformCallback(s.GetChecked().toString());
         }

    </script>

<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>                                                
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Forward" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                              
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be recovered, Proceed?"  />          
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRShiftMassNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="DTRShiftMassTransNo" Caption="Trans. No." />
                                <dx:GridViewDataTextColumn FieldName="Description" Caption="Reason" />                                                              
                                <dx:GridViewDataTextColumn FieldName="xDateFrom" Caption="From" />
                                <dx:GridViewDataTextColumn FieldName="xDateTo" Caption="To" />
                                <dx:GridViewDataTextColumn FieldName="ShiftDesc" Caption="Shift" />
                                <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" />
                                <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Forwarded By" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PostedDate" Caption="Date Forwarded" Visible="false" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                        
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
                            </Columns>                            
                        </dx:ASPxGridView>    
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                               
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    Transaction No. :&nbsp;<asp:Label runat="server" ID="lbl" />
                </div>
                <div>                                                
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAppend" OnClick="lnkAppend_Click" Text="Add" CssClass="control-primary" /></li>                                                   
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
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" SkinID="grdDX" runat="server" KeyFieldName="DTRShiftMassDetiNo"
                        OnCommandButtonInitialize="grdDetl_CommandButtonInitialize" OnCustomCallback="gridDetl_CustomCallback">
                            <Columns>                               
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />                                                              
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="FilteredBy" Caption="Filter By" /> 
                                <dx:GridViewDataTextColumn FieldName="FilteredValue" Caption="Filter Value" /> 
                                <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" Visible="false" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodedBy" Caption="Encoded By" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date Encoded" Visible="false" />                                                               
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
				                </dx:GridViewCommandColumn>  
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
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDTRShiftMassNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRShiftMassTransNo" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Reason :</label>
                <div class="col-md-6"> 
                    <asp:Textbox ID="txtDescription" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="3" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="required form-control" Placeholder="From"></asp:TextBox> 
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtDateFrom"
                        Format="MM/dd/yyyy" />  
                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtDateFrom"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                                    
                        <asp:RangeValidator
                        ID="RangeValidator3"
                        runat="server"
                        ControlToValidate="txtDateFrom"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                    
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender4"
                        TargetControlID="RangeValidator3" />                                                                           
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="required form-control"  Placeholder="To"></asp:TextBox> 
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtDateTo"
                        Format="MM/dd/yyyy" />  
                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtDateTo"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                                    
                        <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtDateTo"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                    
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender2"
                        TargetControlID="RangeValidator3" />                                                                           
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-4 control-label has-required">Shift :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNo" DataMember="EShiftL" runat="server" CssClass="required form-control" OnSelectedIndexChanged="cboShiftNo_SelectedIndexChanged" AutoPostBack="true" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Shift Mon :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNoMon" DataMember="EShiftL" runat="server"  CssClass="form-control"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Shift Tue :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNoTue" DataMember="EShiftL" runat="server"  CssClass="form-control"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Shift Wed :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNoWed" DataMember="EShiftL" runat="server"  CssClass="form-control"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Shift Thu :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNoThu" DataMember="EShiftL" runat="server"  CssClass="form-control"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Shift Fri :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNoFri" DataMember="EShiftL" runat="server"  CssClass="form-control"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Shift Sat :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNoSat" DataMember="EShiftL" runat="server"  CssClass="form-control"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Shift Sun :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNoSun" DataMember="EShiftL" runat="server"  CssClass="form-control"></asp:Dropdownlist>
                </div>
            </div>
            <br />
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShowAppend" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlAppend" runat="server" TargetControlID="btnShowAppend" PopupControlID="pnlPopupAppend" CancelControlID="lnkCloseAppend" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupAppend" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="Fieldset1">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseAppend" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveAppend" OnClick="lnkSaveAppend_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveAppend" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Filter By :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboFilteredbyNo" DataMember="EFilteredByAll" AutoPostBack="true"  runat="server" CssClass="form-control required" 
                        OnSelectedIndexChanged="cboFilteredbyNo_SelectedIndexChanged" ></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-4 control-label has-required">Filter Value :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hiffilterbyid"/>
                    <ajaxToolkit:AutoCompleteExtender ID="drpAC" runat="server"  
                    TargetControlID="txtName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="populateDataDropdown" CompletionSetCount="0"
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" />
                     <script type="text/javascript">
                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hiffilterbyid.ClientID %>').value = eventArgs.get_value();
                         }
                     </script>
                    
                </div>
            </div>
 
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>
