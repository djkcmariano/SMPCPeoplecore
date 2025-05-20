<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SelfDTRLogMassListAppr.aspx.vb" Inherits="SecuredManager_SelfDTRLogMassListAppr" EnableEventValidation="false" %>


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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRLogMassNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="DTRLogMassTransNo" Caption="Trans. No." />
                                <dx:GridViewDataTextColumn FieldName="Description" Caption="Reason" />
                                <dx:GridViewDataTextColumn FieldName="xDTRDate" Caption="Date" />
                                <dx:GridViewDataTextColumn FieldName="In1" Caption="In" />
                                <dx:GridViewDataTextColumn FieldName="Out1" Caption="Out" />
                                <dx:GridViewDataTextColumn FieldName="In2" Caption="In 2" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="Out2" Caption="Out 2" Visible="false" />
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
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" SkinID="grdDX" runat="server" KeyFieldName="DTRLogMassDetiNo" 
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
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRLogMassNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRLogMassTransNo" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
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
                        <asp:TextBox ID="txtDTRDate" runat="server" CssClass="required form-control" ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                            Format="MM/dd/yyyy" TargetControlID="txtDTRDate" />
                        <asp:RangeValidator ID="RangeValidator3" runat="server" 
                            ControlToValidate="txtDTRDate" Display="None" 
                            ErrorMessage="&lt;b&gt;Please enter valid entry&lt;/b&gt;" 
                            MaximumValue="3000-12-31" MinimumValue="1900-01-01" Type="Date" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                            AcceptNegative="Left" ClearTextOnInvalid="true" DisplayMoney="Left" 
                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" 
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtDTRDate" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" 
                            runat="Server" TargetControlID="RangeValidator3" />
                   </div>
            </div>
         
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Time :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIn1" runat="server" CssClass="form-control" Placeholder="In"></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                        TargetControlID="txtIn1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                        ControlExtender="MaskedEditExtender4x"
                        ControlToValidate="txtIn1"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOut1" runat="server" CssClass="form-control" Placeholder="Out"></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtOut1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                        ControlExtender="MaskedEditExtender4"
                        ControlToValidate="txtOut1"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
                
            </div>
       
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Time 2 :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIn2" runat="server"  CssClass="form-control" Placeholder="In 2"></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtIn2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                        ControlExtender="MaskedEditExtender5"
                        ControlToValidate="txtIn2"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="Input time" />
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOut2" runat="server" cssclass="form-control" Placeholder="Out 2"></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                        TargetControlID="txtOut2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                        ControlExtender="MaskedEditExtender6"
                        ControlToValidate="txtOut2"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
                
                
            </div>
       
        </div>
        <br />    
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

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Shift :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboShiftNo" DataMember="EShiftL" runat="server"  CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div>

                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>
