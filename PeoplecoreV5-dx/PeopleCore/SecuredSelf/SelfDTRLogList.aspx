<%@ Page Language="VB" AutoEventWireup="false"  Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfDTRLogList.aspx.vb" Inherits="SecuredSelf_SelfDTRLogList" %>



<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                                                                                
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRLogNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." /> 
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />                                            
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="xDTRDate" Caption="Date" />
                                <dx:GridViewDataTextColumn FieldName="In1" Caption="In" />
                                <dx:GridViewDataTextColumn FieldName="Out1" Caption="Out" />
                                <dx:GridViewDataTextColumn FieldName="In2" Caption="In 2" Visible="false"/>
                                <dx:GridViewDataTextColumn FieldName="Out2" Caption="Out 2" Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Disapprover<br />Remarks" Width="12%" Visible="false" />                                                               
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatDesc" Caption="Approval Status"  Width="12%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveBy" Caption="Approved /<br />Disapproved<br />By" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveDate" Caption="Approved /<br />Disapproved<br />Date"  Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Date <br /> Applied &nbsp;&nbsp;" Width="5%" HeaderStyle-VerticalAlign="Top" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Applied By" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Facility" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" Visible="false" />      
                                <dx:GridViewDataComboBoxColumn FieldName="LocationDesc" Caption="Location" Visible="false" />                                                                                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="ProjectDesc" Caption="Project" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />                                        
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
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    Transaction No. : &nbsp;<asp:Label runat="server" ID="lbl" />
                </div>
                <div>                
                    <ul class="panel-controls"> 
                        &nbsp;                                                       
                        <li><asp:LinkButton runat="server" ID="lnkAddD" OnClick="lnkAddD_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDeleteD" OnClick="lnkDeleteD_Click" Text="Delete" CssClass="control-primary" /></li>      
                                     
                    </ul>  
                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteD" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                                                                                                                                        
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="DTRLogDetlNo;Code" Width="100%">
                            <Columns>    
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditD" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditD_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="ProjectDesc" Caption="Project" />
                                <dx:GridViewDataTextColumn FieldName="In1" Caption="In" />
                                <dx:GridViewDataTextColumn FieldName="Out1" Caption="Out" />
                                <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Hrs" />
                                <dx:GridViewDataTextColumn FieldName="Task" Caption="Task"  PropertiesTextEdit-EncodeHtml="false" HeaderStyle-Wrap="true" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Comments" HeaderStyle-Wrap="true"  PropertiesTextEdit-EncodeHtml="false"/>    
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
                                    
                                </dx:GridViewCommandColumn>                   
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style=" display:none;">
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
                <label class="col-md-4 control-label">DTR Correction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRLogNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtDTRLogTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" ></asp:TextBox>
                </div>
            </div> 
        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">
                        <asp:TextBox ID="txtDTRDate" runat="server" CssClass="required form-control" 
                            ></asp:TextBox>
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
                <div class="col-md-3">
                    <asp:LinkButton runat="server" ID="lnkViewLogs" OnClick="lnkViewLogs_Click" CausesValidation="false" Text="Click here to view raw logs." />
                </div>
            </div>
         
            <div class="form-group">
                <label class="col-md-4 control-label has-space">In 1 :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIn1" runat="server" CssClass="form-control" Placeholder="In" ></asp:TextBox>
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
                <label class="col-md-1 control-label">Out 1 :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOut1" runat="server" CssClass="form-control" Placeholder="Out" ></asp:TextBox>   
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
       
            <div class="form-group" style="Display: none">
                <label class="col-md-4 control-label has-space">In 2 :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIn2" runat="server" CssClass="form-control" Placeholder="In 2"></asp:TextBox>   
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
                         />
                </div>
            
                <label class="col-md-1 control-label">Out 2 :</label>
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

            <div class="form-group">
                    <label class="col-md-4 control-label  has-required">DTR Correction Reason :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboDTRLogReasonNo" runat="server" CssClass="form-control required" AutoPostBack="True" OnSelectedIndexChanged="cboDTRLogReasonNo_OnSelectedIndexChanged">
                        </asp:DropdownList>
                    </div>
            </div>

            <div class="form-group" >
                <label class="col-md-4 control-label has-required">Reason :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtReason" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control required"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                    <label class="col-md-4 control-label  has-space">Biometric Location :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboDTRBioLogNo" runat="server" CssClass="form-control" >
                        </asp:DropdownList>
                    </div>
            </div>

             <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Approval Status :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboApprovalStatNo" runat="server" DataMember="EApprovalStat" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group" >
                <div class="col-md-6" Style="Text-align: center; left: 160px;">
                    <label id="Label1"  style="Color: Red; Text-align: center; font-weight: Bold;" runat="server" CssClass="form-control">Note: Time in Military Format</label>
                </div>
            </div>
         </div>
          <!-- Footer here -->
         <br />
            
        
    </fieldset>
</asp:Panel>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="Fieldset1">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4></h4>
                <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDTRLogDetlNo" CssClass="form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"   Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Project :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboProjectNo" DataMember="EProject" CssClass="form-control" runat="server" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Hours Work :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtHrs" SkinID="txtdate" runat="server" CssClass="form-control"
                        ></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">In :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIn11" runat="server" CssClass="form-control" Placeholder="In" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1x" runat="server"
                        TargetControlID="txtIn11" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                            
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                        ControlExtender="MaskedEditExtender1x"
                        ControlToValidate="txtIn11"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage=""
                            
                            />
                </div>
                <label class="col-md-1 control-label">Out :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOut11" runat="server" CssClass="form-control" Placeholder="Out" ></asp:TextBox>   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2x" runat="server"
                        TargetControlID="txtOut11" 
                        Mask="99:99"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Time"
                        AcceptAMPM="false" 
                        CultureName="en-US" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server"
                        ControlExtender="MaskedEditExtender2x"
                        ControlToValidate="txtOut11"
                        IsValidEmpty="true"
                        EmptyValueMessage=""
                        InvalidValueMessage=""
                        ValidationGroup="Demo1"
                        Display="Dynamic"
                        TooltipMessage="" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Task :</label>
                <div class="col-md-6">
                    <%--<asp:Textbox ID="txtRemarks" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" 
                        ></asp:Textbox>--%>
                    <dx:ASPxHtmlEditor ID="txtTask" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Comments :</label>
                <div class="col-md-6">
                    <%--<asp:Textbox ID="txtRemarks" TextMode="MultiLine" Rows="3"  CssClass="form-control required" runat="server" 
                        ></asp:Textbox>--%>
                    <dx:ASPxHtmlEditor ID="txtRemarks" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic" />
                </div>
            </div>
            <div class="form-group">
                <br />
            </div>

        </div>
        
         </fieldset>
</asp:Panel>
</asp:Content>
