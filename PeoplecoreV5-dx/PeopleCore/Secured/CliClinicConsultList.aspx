<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CliClinicConsultList.aspx.vb" Inherits="Secured_CliClinicConsultList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />            
            </center>            
            <asp:Label runat="server" ID="lbl" />        
        </Header>
        <Content>
        <br />

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
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="ClinicConsultNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>                            
                                            <dx:GridViewDataTextColumn FieldName="ClinicConsultCode" Caption="Trans. No." />                                                                       
                                            <dx:GridViewDataComboBoxColumn FieldName="ClinicConsultTypeDesc" Caption="Type of Consultation" /> 
                                            <dx:GridViewDataTextColumn FieldName="DateReg" Caption="Date of Consult" />                    
                                            <dx:GridViewDataTextColumn FieldName="TimeReg" Caption="Time of Consult" />   
                                            <dx:GridViewBandColumn Caption="Next Schedule" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="NextScheduleDate" Caption="Date" />    
                                                    <dx:GridViewDataTextColumn FieldName="NextSchedTime" Caption="Time" />  
                                                </Columns>
                                            </dx:GridViewBandColumn>       
                                            <dx:GridViewDataTextColumn FieldName="ClinicStatDesc" Caption="Status" />                                                                                                                                           
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

            <asp:Button ID="Button1" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none" >
                <fieldset class="form" id="fsMain">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">                        
                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtClinicConsultCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-5 control-label has-space">HMS No. :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtPatientNo" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Weight :</label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtWeight" CssClass="form-control" />
                            </div>
                            <div class="col-md-2">
                                <span>lbs.</span>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Height :</label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtHeight" CssClass="form-control" />
                            </div>
                            <div class="col-md-2">
                                <span>cm.</span>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-required">Type of Consultation :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboClinicConsultTypeNo" DataMember="EClinicConsultType" runat="server" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-required">Date of Consultation :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtDateReg" runat="server" CssClass="form-control required"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                    TargetControlID="txtDateReg"
                                    Format="MM/dd/yyyy" />  
                                      
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                    TargetControlID="txtDateReg"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Date"
                                    DisplayMoney="Left"
                                    AcceptNegative="Left" />
                                    
                                    <asp:RangeValidator
                                    ID="RangeValidator1"
                                    runat="server"
                                    ControlToValidate="txtDateReg"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                                    
                                    <ajaxToolkit:ValidatorCalloutExtender 
                                    runat="Server" 
                                    ID="ValidatorCalloutExtender1"
                                    TargetControlID="RangeValidator1" />                                                                           
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Time of Consultation :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtTimeReg" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                                    TargetControlID="txtTimeReg" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                            
                                    CultureName="en-US" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                    ControlExtender="MaskedEditExtender4x"
                                    ControlToValidate="txtTimeReg"
                                    IsValidEmpty="true"
                                    EmptyValueMessage=""
                                    InvalidValueMessage=""
                                    ValidationGroup="Demo1"
                                    Display="Dynamic"
                                    TooltipMessage="" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-5 control-label has-required">Medical Conditions :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtMedicalConditions" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-required">Chief Complaint/Brief Clinical History :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtComplaints" TextMode="MultiLine" Rows="3" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Clinical Impression/Findings/Diagnosis :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtDiagnosis" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Procedure Done/Treatment/Referrals :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtTreatment" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Prescription :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtPrescription" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-5 control-label has-required">Place Occured :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtPlaceOccured" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-5 control-label has-required">Assessment :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtAssessment" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-5 control-label has-space">Recommendation :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRecommendation" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-5 control-label has-space">Recommendation :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboRecommendationNo" DataMember="ERecommendation" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Remarks :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-5 control-label has-space">Start of SL :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                    TargetControlID="txtStartDate"
                                    Format="MM/dd/yyyy" />  
                                      
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                    TargetControlID="txtStartDate"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Date"
                                    DisplayMoney="Left"
                                    AcceptNegative="Left" />
                                    
                                    <asp:RangeValidator
                                    ID="RangeValidator2"
                                    runat="server"
                                    ControlToValidate="txtStartDate"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                                    
                                    <ajaxToolkit:ValidatorCalloutExtender 
                                    runat="Server" 
                                    ID="ValidatorCalloutExtender2"
                                    TargetControlID="RangeValidator2" />                                                                           
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-5 control-label has-space">End of SL :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                    TargetControlID="txtEndDate"
                                    Format="MM/dd/yyyy" />  
                                      
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                    TargetControlID="txtEndDate"
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
                                    ControlToValidate="txtEndDate"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                                    
                                    <ajaxToolkit:ValidatorCalloutExtender 
                                    runat="Server" 
                                    ID="ValidatorCalloutExtender3"
                                    TargetControlID="RangeValidator3" />                                                                           
                            </div>
                        </div>

                        <div class="form-group" style="visibility:hidden;position:absolute;">
                            <label class="col-md-5 control-label"></label>
                            <div class="col-md-6">
                                <asp:CheckBox runat="server" ID="chkIsFitToWork" Text="&nbsp;Please tick here if fit to work after SL" />
                            </div>
                        </div>

                        <div class="form-group" style="visibility:hidden;position:absolute;">
                            <label class="col-md-5 control-label"></label>
                            <div class="col-md-6">
                                <asp:CheckBox runat="server" ID="chkIsFollowup" Text="&nbsp;Please tick here if need a follow-up check-up" />
                            </div>
                        </div>

                        <div class="form-group" style="display:none;">
                            <label class="col-md-5 control-label has-space">Name of Doctor :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboDoctorNo" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Name of Doctor :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtDoctorName" TextMode="MultiLine" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Status :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboClinicStatNo" DataMember="EClinicStat" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Date for Next Schedule :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtNextSchedDate" runat="server" CssClass="form-control"></asp:TextBox> 
                                                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                    TargetControlID="txtNextSchedDate"
                                    Format="MM/dd/yyyy" />  
                                      
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                    TargetControlID="txtNextSchedDate"
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
                                    ControlToValidate="txtNextSchedDate"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                                    
                                    <ajaxToolkit:ValidatorCalloutExtender 
                                    runat="Server" 
                                    ID="ValidatorCalloutExtender4"
                                    TargetControlID="RangeValidator4" />                                                                           
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-5 control-label has-space">Next Schedule Time :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtNextSchedTime" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                    TargetControlID="txtNextSchedTime" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                            
                                    CultureName="en-US" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                    ControlExtender="MaskedEditExtender4x"
                                    ControlToValidate="txtNextSchedTime"
                                    IsValidEmpty="true"
                                    EmptyValueMessage=""
                                    InvalidValueMessage=""
                                    ValidationGroup="Demo1"
                                    Display="Dynamic"
                                    TooltipMessage="" />
                            </div>
                        </div>

                        <br />
                    </div>                    
                </fieldset>
            </asp:Panel>

        </Content>
    </uc:Tab>
</asp:Content>

