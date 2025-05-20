<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="DTROTMassList.aspx.vb" Inherits="Secured_DTROTMassList" EnableEventValidation="false" %>



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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTROTMassNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="DTROTMassTransNo" Caption="Trans. No." />
                                <dx:GridViewDataTextColumn FieldName="Description" Caption="Reason" />                                                              
                                <dx:GridViewDataTextColumn FieldName="xDTRDate" Caption="Date" />
                                <dx:GridViewDataTextColumn FieldName="OvtIn1" Caption="In" />
                                <dx:GridViewDataTextColumn FieldName="OvtOut1" Caption="Out" />
                                <dx:GridViewDataTextColumn FieldName="OvtIn2" Caption="In 2" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="OvtOut2" Caption="Out 2" Visible="false" />
                                <%--<dx:GridViewDataTextColumn FieldName="ChargedTo" Caption="Charged to" Visible="false" />--%>
                                <dx:GridViewDataCheckColumn FieldName="IsForCompensatory" Caption="CTO" Visible="false" />
                                <dx:GridViewDataCheckColumn FieldName="IsOnCall" Caption="On Call" Visible="false" />
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
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" SkinID="grdDX" runat="server" KeyFieldName="DTROTMassDetiNo" Width="100%"
                            OnCommandButtonInitialize="grdDetl_CommandButtonInitialize" OnCustomCallback="gridDetl_CustomCallback" >
                            <Columns>                           
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />                                                              
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="FilteredBy" Caption="Filter By" /> 
                                <dx:GridViewDataTextColumn FieldName="FilteredValue" Caption="Filter Value" /> 
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Visible="true" /> 
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
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTROTMassNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTROTMassTransNo" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Reason :</label>
                <div class="col-md-6"> 
                    <asp:Textbox ID="txtDescription" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="3" ></asp:Textbox>
                </div>
            </div>
            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label">Filter By :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control"  AutoPostBack="true"  OnSelectedIndexChanged="cbofilterby_SelectedIndexChanged">
                    </asp:DropDownList>
                 </div>
             </div>

		     <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label">Filter Value :</label> 
                <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control">
                        </asp:DropDownList>
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
                <label class="col-md-4 control-label has-required">Time :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOvtIn1" runat="server" CssClass="form-control required" Placeholder="OT In" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                        TargetControlID="txtOvtIn1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                        ControlExtender="MaskedEditExtender4x"
                        ControlToValidate="txtOvtIn1"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
                </div>
                <%--<label class="col-md-1 control-label"></label>--%>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOvtOut1" runat="server" CssClass="form-control required" Placeholder="OT Out" ></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtOvtOut1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                        ControlExtender="MaskedEditExtender4"
                        ControlToValidate="txtOvtOut1"
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
                    <asp:TextBox ID="txtOvtIn2" runat="server"  CssClass="form-control" Placeholder="OT In 2"></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtOvtIn2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                        ControlExtender="MaskedEditExtender5"
                        ControlToValidate="txtOvtIn2"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="Input time" />
                </div>
            
                <%--<label class="col-md-1 control-label"></label>--%>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOvtOut2" runat="server" cssclass="form-control" Placeholder="OT Out 2"></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                        TargetControlID="txtOvtOut2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                        ControlExtender="MaskedEditExtender6"
                        ControlToValidate="txtOvtOut2"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">OT Break (hrs) :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOTBreak" runat="server" CssClass="number form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group" >
                <label class="col-md-4 control-label"></label>
                <div class="col-md-6">
                    <asp:Checkbox ID="txtIsForcompensatory" Text="&nbsp; For Compensatory Time Off" runat="server"></asp:Checkbox>
                 </div>
            </div>

             <div class="form-group" style="visibility:hidden; position:absolute;" >
                <label class="col-md-4 control-label"></label>
                <div class="col-md-6">
                    <asp:Checkbox ID="txtIsOncall" Text="&nbsp; For On-Call" runat="server"></asp:Checkbox>
                </div>
            </div>

            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label has-space">Charged to :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboCostCenterNo" runat="server" DataMember="ECostCenter" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
       
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
