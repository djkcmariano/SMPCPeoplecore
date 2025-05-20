<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRShiftRefList.aspx.vb" Inherits="Secured_DTRShiftRefList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function disableenable(chk) {

        if (chk.checked) {
            document.getElementById("ctl00_cphBody_txtAddLate").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_txtAddLate").disabled = true;
            document.getElementById("ctl00_cphBody_txtAddLate").value = "";
        }
            
    };
    function disableenable_behind(fval) {
        if (fval=='True') {
            document.getElementById("ctl00_cphBody_txtAddLate").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_txtAddLate").disabled = false;
            document.getElementById("ctl00_cphBody_txtAddLate").value = "";
        }
    };

    function disableenableOT(chk) {

        if (chk.checked) {
            document.getElementById("ctl00_cphBody_txtOTEnd").disabled = false;
        } else {
            document.getElementById("ctl00_cphBody_txtOTEnd").disabled = true;
            document.getElementById("ctl00_cphBody_txtOTEnd").value = "";
        }

    };

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
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                                                
                    <ul class="panel-controls">                                                                                
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>                            
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible ="false"/></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                                                                                
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                          
                        <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Selected items will be archived. Proceed?"  />                    </ul>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ShiftNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />                                              
                                <dx:GridViewDataTextColumn FieldName="ShiftCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="ShiftDesc" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Required<br/>Hours" Width="3%" CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataTextColumn FieldName="NoofSwipe" Caption="No. of<br/>Swipes" Width="3%" CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataTextColumn FieldName="WFH" Caption="WFH" />
                                <dx:GridViewDataTextColumn FieldName="In1" Caption="In1" />
                                <dx:GridViewDataTextColumn FieldName="Out1" Caption="Out1" />
                                <dx:GridViewDataTextColumn FieldName="In2" Caption="In2" Visible="false"/>
                                <dx:GridViewDataTextColumn FieldName="Out2" Caption="Out2" Visible="false"/>
                                <dx:GridViewDataTextColumn FieldName="OTStart" Caption="OT Start" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="BreakHrs1" Caption="Break Hours" Visible="false" CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataTextColumn FieldName="BreakIn" Caption="Break Start" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="BreakOut" Caption="Break End" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="AdjustedHrs" Caption="Adjusted Hours" ReadOnly="true" Visible="false" CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataCheckColumn FieldName="IsNonPunching" Caption="Non Punching" ReadOnly="true" Visible="false" />
                                <dx:GridViewDataCheckColumn FieldName="IsCompress" Caption="Compress" ReadOnly="true" Visible="false" />
                                <dx:GridViewDataCheckColumn FieldName="IsGraveYard" Caption="Graveyard" ReadOnly="true" Visible="false" />
                                <dx:GridViewDataCheckColumn FieldName="IsOTApply" Caption="OT Auto Apply" ReadOnly="true" Visible="false" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />                 
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

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="imgClose" BackgroundCssClass="modalBackground" />

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtShiftNo" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Shift Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtShiftCode" runat="server" CssClass="required form-control" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Shift Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtShiftDesc" runat="server" CssClass="required form-control" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Required Hours :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtHrs" runat="server" CssClass="required form-control" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtHrs" />
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">No. of Swipes :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboNoOfSwipe" CssClass="required form-control" AutoPostBack="true" OnSelectedIndexChanged="cboNoOfSwipe_OnSelectedIndexChanged" >
                        <asp:ListItem Text="-- Select --" Value="" />
                        <asp:ListItem Text="2" Value="2" />
                        <asp:ListItem Text="4" Value="4" />
                    </asp:DropDownList>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-required">In1 :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtIn1" runat="server"  SkinID="txtdate" CssClass="required form-control"></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtIn1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                        ControlExtender="MaskedEditExtender1"
                        ControlToValidate="txtIn1"
                        IsValidEmpty="true"
                        EmptyValueMessage="Time is required"
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
               </div>
               <label class="col-md-2 control-label has-required">Out1 :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOut1" runat="server"  SkinID="txtdate" CssClass="required form-control"></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtOut1" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                        ControlExtender="MaskedEditExtender2"
                        ControlToValidate="txtOut1"
                        IsValidEmpty="true"
                        EmptyValueMessage="Time is required"
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">In2 :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtIn2" runat="server"  SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtIn2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                        ControlExtender="MaskedEditExtender3"
                        ControlToValidate="txtIn2"
                        IsValidEmpty="true"
                        EmptyValueMessage="Time is required"
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
               </div>

               <label class="col-md-2 control-label has-space">Out2 :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOut2" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtOut2" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                        ControlExtender="MaskedEditExtender4"
                        ControlToValidate="txtOut2"
                        IsValidEmpty="true"
                        EmptyValueMessage="Time is required"
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
               </div>

            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Break Hours :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBreakHrs1" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtBreakHrs1" />
               </div>

               
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Break Start :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBreakIn" runat="server"  SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                        TargetControlID="txtBreakIn" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server"
                        ControlExtender="MaskedEditExtender3"
                        ControlToValidate="txtBreakIn"
                        IsValidEmpty="true"
                        EmptyValueMessage="Time is required"
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
               </div>

               <label class="col-md-2 control-label has-space">Break End :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBreakOut" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server"
                        TargetControlID="txtBreakOut" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server"
                        ControlExtender="MaskedEditExtender4"
                        ControlToValidate="txtBreakOut"
                        IsValidEmpty="true"
                        EmptyValueMessage="Time is required"
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
               </div>
               

            </div>


            

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-2" style="padding-top:7px;">            
                    <asp:CheckBox runat="server" ID="txtIsAdjustedFlex" AutoPostBack="true" OnCheckedChanged="txtIsAdjustedFlex_OnSelectedIndexChanged" />
                    <span>Adjusted flex</span>
               </div>
               <label class="col-md-2 control-label has-space">Adjusted Hrs :</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtAdjustedHrs"  CssClass="form-control" SkinID="txtdate" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtAdjustedHrs" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsFlexiBreak" />
                    <span>Flexible break (4 swipes only).</span>
                </div>
            </div>
            <div class="form-group" style="visibility:hidden;position:absolute;">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsFlex" />
                    <span>Full flexible works in a cut-off</span>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsDailyFlex" />
                    <span>Daily flexible works</span>
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsNonPunching" />
                    <span>Non punching employees</span>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsCompress" />
                    <span>Compress employee</span>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsGraveyard" />
                    <span>Graveyard shift</span>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="txtIsOTApply" onclick="disableenableOT(this);" />
                    <span>Overtime auto apply</span>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Overtime Start :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOTStart" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtOTStart" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                        ControlExtender="MaskedEditExtender4"
                        ControlToValidate="txtOTStart"
                        IsValidEmpty="true"
                        EmptyValueMessage="Time is required"
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
               </div>

               <label class="col-md-2 control-label has-space">Overtime End :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOTEnd" runat="server" SkinID="txtdate" CssClass="form-control"  ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server"
                        TargetControlID="txtOTEnd" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator8" runat="server"
                        ControlExtender="MaskedEditExtender4"
                        ControlToValidate="txtOTEnd"
                        IsValidEmpty="true"
                        EmptyValueMessage="Time is required"
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
               </div>
            </div>

            <div class="form-group">
                        <label class="col-md-3 control-label has-space">Mandatory OT Hr/s :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="TxtOTAdj" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="TxtOTAdj" />
                        </div>
                    </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">            
                    <asp:CheckBox runat="server" ID="txtIsAddLate" onclick="disableenable(this);" Text="&nbsp; Enable paid shorten hours" />
               </div>
               
            </div>


            <div class="form-group">
                <label class="col-md-4 control-label has-space">No. of paid shorten hours :</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtAddLate"  CssClass="form-control" SkinID="txtdate" Enabled="false" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtAddLate" />
                </div>
            </div>

            <div class="form-group" style=display:none">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
             </div> 
            <br />
        </div>        
         </fieldset>
</asp:Panel>
</asp:Content>
