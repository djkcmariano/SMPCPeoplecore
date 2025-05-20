<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRRawLogs.aspx.vb" Inherits="Secured_DTROTList" EnableEventValidation="false" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-4">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                                                
                            <ul class="panel-controls pull-left">   
                                <li><asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload Logs" CssClass="control-primary" /></li> 
                                <%--<li><asp:LinkButton runat="server" ID="lnkDownload" OnClick="lnkDownload_Click" Text="Download Logs" CssClass="control-primary" /></li>--%>                                                     
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                                                                                  
                            </ul>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div>
                    <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                        <Content>
                                <asp:Panel ID="Panel1" runat="server">

                                    <div class="form-group">
                                        <label class="col-md-4 control-label">Filter By :</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control" />
                                            </div>
                                            <ajaxToolkit:CascadingDropDown ID="cdlfilterby" TargetControlID="cbofilterby" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterBy" runat="server" Category="tNo" LoadingText="Loading..." />
                                    </div>
		                            <div class="form-group">
                                        <label class="col-md-4 control-label">Filter Value :</label> 
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control" />
                                        </div>
                                        <ajaxToolkit:CascadingDropDown ID="cdlfiltervalue" TargetControlID="cbofiltervalue" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterValue" runat="server" Category="tNo" ParentControlID="cbofilterby" LoadingText="Loading..." PromptText="-- Select --" />
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label">Date From :</label>
                                        <div class="col-md-8">
                                            <asp:Textbox runat="server" ID="fltxtStartDate" SkinID="txtdate" CssClass="form-control" ></asp:Textbox>
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="fltxtStartDate" Format="MM/dd/yyyy"/>  
                                        <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="fltxtStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator2" /> 
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="fltxtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />

                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-4 control-label">Date To :</label>
                                        <div class="col-md-8">
                                            <asp:Textbox runat="server" ID="fltxtEndDate" SkinID="txtdate" CssClass="form-control" ></asp:Textbox>
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="fltxtEndDate" Format="MM/dd/yyyy"/>  
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="fltxtEndDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5" TargetControlID="RangeValidator2" /> 
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="fltxtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />

                                    </div>
                                </asp:Panel>
                          </Content>
                    </uc:Filter>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="FPDTRId">                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="FPId" Caption="FPID" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataDateColumn FieldName="DTRDate" Caption="DTR Date" />
                                <dx:GridViewDataTextColumn FieldName="LogType" Caption="Log Type" />
                                <dx:GridViewDataTextColumn FieldName="DTRTime" Caption="DTR Time" />
                                <dx:GridViewBandColumn Caption="Converted" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <Columns>
                                        <dx:GridViewDataDateColumn FieldName="CDTRDate" Caption="DTR Date" />
                                        <dx:GridViewDataTextColumn FieldName="CDTRTime" Caption="DTR Time" />
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataTextColumn FieldName="FPMachineDescription" Caption="Machine Name"/>
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="EncodedDate" Caption="Date Uploaded" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Visible="false" />
                               
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
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose2" PopupControlID="Panel3" TargetControlID="Button1" />
<asp:Panel id="Panel3" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsUpload">      
         <div class="cf popupheader">
            <h4>Upload Logs</h4>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    <asp:LinkButton runat="server" ID="lnkSave2" CssClass="fa fa-floppy-o submit fsUpload lnkSave2" OnClick="lnkSave2_Click"/>   
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkSave2" />
                </Triggers>
            </asp:UpdatePanel>            
         </div>         
         <div  class="entryPopupDetl form-horizontal">
                    
           <div class="form-group">
                <label class="col-md-4 control-label has-required">DTR Source :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboSourceNo" DataMember="EDTRSource" runat="server" CssClass="required form-control" AutoPostBack="true" OnSelectedIndexChanged="cboSourceNo_SelectedIndexChanged" />                    
                </div>
            </div> 
            <div class="form-group">
                <p class="col-md-4 control-label  has-space"></p>                        
                <div class="col-md-7">                                                                       
                    <code><i class="fa fa-info-circle fa-lg">&nbsp;</i><asp:Label ID="lblFormat" runat="server"></asp:Label></code>
                </div>
            </div>    
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Filename :</label>
                <div class="col-md-7">
                    <asp:FileUpload runat="server" ID="fuFilename" Width="100%" CssClass="required" />                   
                </div>
            </div>

            <div class="form-group" style="Display:none">
                <label class="col-md-4 control-label has-space">Machine Name :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboFPMachineNo" DataMember="EFPMachineB" runat="server" CssClass="form-control" />                    
                </div>
            </div> 

           <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDescription" runat="server" Rows="4" textmode="MultiLine" CssClass="form-control" />
                </div>
            </div>            
            <br /> 
        </div>         
        <div class="cf popupfooter">
        </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup" CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style=" display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Download Logs</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;    
                <asp:LinkButton runat="server" ID="lnkProcess" CssClass="fa fa-floppy-o submit fsMain lnkProcess" OnClick="lnkProcess_Click"/>  
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">
                      <asp:TextBox ID="txtDateFrom" runat="server" CssClass="required form-control" style="display:inline-block;" placeholder="From"></asp:TextBox> 
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateFrom"  Format="MM/dd/yyyy" />  
                                      
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
<%--            </div>
            <div class="form-group">
                <label class="col-md-5 control-label has-required">Date To :</label>--%>
               <div class="col-md-3">
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="required form-control" style="display:inline-block;" placeholder="To"></asp:TextBox> 
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
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
        
        </fieldset>
</asp:Panel>  

</asp:Content>
