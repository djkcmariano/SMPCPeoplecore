<%@ Page Language="VB" AutoEventWireup="false"  Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRJackUpLog.aspx.vb" Inherits="Secured_DTRJackUpLog" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }

</script>

<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" Visible="false"/>
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                                                
                    <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" /></li>                                                        
                        <li><asp:LinkButton runat="server" ID="lnkAddMass" OnClick="lnkAddMass_Click" Text="Mass Application" CssClass="control-primary" Visible="false"/></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false"/></li>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmpCode"  >                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="EmpCode" Caption = "Employee Code"/> 
                                <dx:GridViewDataTextColumn FieldName="EmpName" Caption="Full Name" />                                            
                                <dx:GridViewDataTextColumn FieldName="Company" Caption="Company" />
                                <dx:GridViewDataTextColumn FieldName="PayGroup" Caption="Payroll Group" />
                                <dx:GridViewDataTextColumn FieldName="Section" Caption="Section" />
                                <dx:GridViewDataTextColumn FieldName="Position" Caption="Position" />
                                <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Pay Date" />
                                <dx:GridViewDataTextColumn FieldName="RegHrs" Caption="Reg Hrs" />
                                <dx:GridViewDataTextColumn FieldName="NdHrs" Caption="Nd Hrs" />
                                <dx:GridViewDataTextColumn FieldName="OtHrs" Caption="Ot Hrs" />                                                                
                                <dx:GridViewDataTextColumn FieldName="NdoHrs" Caption="Ndo Hrs" />
                                <dx:GridViewDataTextColumn FieldName="CreatedBy" Caption="Created By" />                                                               
                                <dx:GridViewDataTextColumn FieldName="UploadedBy" Caption="Uploaded By" />
                                <dx:GridViewDataTextColumn FieldName="DateUploaded" Caption="Date Uploaded" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="2%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                                                                 
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                               
            </div>   
  
  <div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">                
                <div class="col-md-6 panel-title">
                    <asp:Label ID="lblDetl" runat="server" />
                </div>
                <div> 
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                    <ContentTemplate>                    
                        <ul class="panel-controls">                                    
                            <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                        </ul>
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
                        <dx:ASPxGridView ID="grdDetl" runat="server" ClientInstanceName="grdDetl" KeyFieldName="RecNo" Width="100%" >
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>  
                                <dx:GridViewDataTextColumn FieldName="RecNo" Caption = "Record No."/> 
                                <dx:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel" />                                            
                                <dx:GridViewDataTextColumn FieldName="Position" Caption="Position" />
                                <dx:GridViewDataTextColumn FieldName="Date" Caption="Date" />
                                <dx:GridViewDataTextColumn FieldName="RegHrs" Caption="Reg Hrs" Width="4%" />
                                <dx:GridViewDataTextColumn FieldName="NdHrs" Caption="Nd Hrs" Width="4%" />
                                <dx:GridViewDataTextColumn FieldName="OtHrs" Caption="Ot Hrs" Width="4%" />
                                <dx:GridViewDataTextColumn FieldName="NdoHrs" Caption="Ndo Hrs" Width="4%" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
				                </dx:GridViewCommandColumn>                                                                                                                  
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="GrdExportDetl" runat="server" GridViewID="grdDetl" />
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
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-6">
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
  
<%--              <div class="form-group">
                <label class="col-md-4 control-label has-space"> Employee No. :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtEmployeeCode" ReadOnly="true" runat="server" cssclass="form-control" />
                </div>
            </div> --%>
                  
            <div class="form-group">
                <label class="col-md-4 control-label has-required">From :</label>
                <div class="col-md-2">
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
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">To :</label>
                <div class="col-md-2">
                        <asp:TextBox ID="txtDTRDateTo" runat="server" CssClass="required form-control" 
                            ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                            Format="MM/dd/yyyy" TargetControlID="txtDTRDateTo" />
                        <asp:RangeValidator ID="RangeValidator1" runat="server" 
                            ControlToValidate="txtDTRDateTo" Display="None" 
                            ErrorMessage="&lt;b&gt;Please enter valid entry&lt;/b&gt;" 
                            MaximumValue="3000-12-31" MinimumValue="1900-01-01" Type="Date" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                            AcceptNegative="Left" ClearTextOnInvalid="true" DisplayMoney="Left" 
                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" 
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtDTRDate" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" 
                            runat="Server" TargetControlID="RangeValidator3" />
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Jackup Vessel :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtVessel" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifVesselNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                    TargetControlID="txtVessel" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateReferential" CompletionSetCount="0" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecordVessel" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecordVessel(source, eventArgs) {
                             document.getElementById('<%= hifVesselNo.ClientID %>').value = eventArgs.get_value();
                         }
                     </script>     
                </div>
            </div>
<%--            <div class="form-group">
                <label class="col-md-4 control-label has-required">Position :</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPosition" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifPositionNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                    TargetControlID="txtPosition" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateReferential" CompletionSetCount="1~" + "hifVesselNo" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecordPosition" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecordPosition(source, eventArgs) {
                             document.getElementById('<%= hifPositionNo.ClientID %>').value = eventArgs.get_value();
                         }
                     </script>     
                </div>
            </div> --%>
                     
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Reg Hrs :</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtRegHrs" CssClass="form-control required" Placeholder="0.00" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRegHrs" FilterType="Numbers, Custom" ValidChars="." /> 
                </div>
                <label class="col-sm-2 control-label">ND Hrs :</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtNdHrs" cssclass="form-control" Placeholder="0.00"/>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNdHrs" FilterType="Numbers, Custom" ValidChars="." /> 
                </div>
            </div>

              <div class="form-group">
                <label class="col-md-4 control-label">OT Hrs :</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtOTHrs" CssClass="form-control" Placeholder="0.00" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtOTHrs" FilterType="Numbers, Custom" ValidChars="." /> 
                </div>
                <label class="col-sm-2 control-label">NDO Hrs :</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtNDOHrs" cssclass="form-control" Placeholder="0.00"/>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtNDOHrs" FilterType="Numbers, Custom" ValidChars="." /> 
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtRecNo" cssclass="form-control" Placeholder="0" Visible ="false"/>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtRecNo" FilterType="Numbers, Custom" /> 
                </div>
            </div>
         </div>
          <!-- Footer here -->
         <br />
    </fieldset>
</asp:Panel>

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose2" PopupControlID="Panel3" TargetControlID="Button1" />
<asp:Panel id="Panel3" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsUpload">      
         <div class="cf popupheader">
            <h4>Upload Jack-up Manhours</h4>
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

</asp:Content>
