<%@ Page Title="" Theme="PCoreStyle" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="TrnTakenDetlList.aspx.vb" Inherits="Secured_TrnTakenDetlList" %>
<%@ Register Src="~/Include/wucTrnHeader.ascx" TagName="wucTrnHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc:wucTrnHeader runat="server" ID="wucTrnHeader1" />

   <script type="text/javascript">

       function cbCheckAll_CheckedChanged(s, e) {
           grdMain.PerformCallback(s.GetChecked().toString());
       }

       function grid_ContextMenu(s, e) {
           if (e.objectType == "row")
               hiddenfield.Set('VisibleIndex', parseInt(e.index));
           pmRowMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
       }

       function OnContextMenuItemClick(sender, args) {
           if (args.item.name == "Refresh") {
               args.processOnServer = true;
               args.usePostBack = true;
           }
       }

    </script>

<div class="page-content-wrap">        
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            
                            <li><asp:LinkButton runat="server" ID="lnkPreStatus" OnClick="lnkPreStatus_Click" Text="Enrollment Status" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkPostStatus" OnClick="lnkPostStatus_Click" Text="Post Status" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkAppend" OnClick="lnkAppend_Click" Text="Append" CssClass="control-primary" /></li>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXTotal" KeyFieldName="TrnTakenDetlNo;EmployeeNo;TrnTakenNo;TrnTitleNo;IsAllowEdit"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback"
                        OnFillContextMenuItems="MyGridView_FillContextMenuItems" OnContextMenuItemVisibility="Grid_ContextMenuItemVisibility"
                        OnContextMenuItemClick="Grid_ContextMenuItemClick">                                                                                   
                            <Columns> 
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("TrnTakenDetlNo") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                           
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />  
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" Visible="false" /> 
                                <dx:GridViewDataTextColumn FieldName="CostCenterDesc" Caption="Cost Center"  /> 
                                <dx:GridViewDataTextColumn FieldName="ActualHrs" Caption="Hour(s)" />   
                                <dx:GridViewDataTextColumn FieldName="TrnPreStatusDesc" Caption="Enrollment Status" FooterCellStyle-HorizontalAlign="Right" />
                                <dx:GridViewDataTextColumn FieldName="TrnPostStatusDesc" Caption="Post Status" FooterCellStyle-HorizontalAlign="Right" />                                                     
                                <dx:GridViewBandColumn Caption="Certificate Date" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <dx:GridViewDataDateColumn FieldName="CertStartDate" Caption="From" /> 
                                            <dx:GridViewDataDateColumn FieldName="CertEndDate" Caption="To" />
                                        </Columns>
                                </dx:GridViewBandColumn>   
                                <dx:GridViewBandColumn Caption="Service Contract Date" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <Columns>
                                        <dx:GridViewDataDateColumn FieldName="ServStartDate" Caption="From" /> 
                                        <dx:GridViewDataDateColumn FieldName="ServEndDate" Caption="To" />
                                    </Columns>
                                </dx:GridViewBandColumn>  
                                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" Visible="false" />   
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Visible="false" /> 
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveBy" Caption="Approved /<br />Disapproved<br />By" Width="5%" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ApproveDisApproveDate" Caption="Approved /<br />Disapproved<br />Date" Width="5%" Visible="false" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
				                </dx:GridViewCommandColumn>

                            </Columns>   
                            <Settings ShowGroupFooter="VisibleIfExpanded" ShowFooter="true" />  
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="TrnPreStatusDesc" SummaryType="Custom" />
                                <dx:ASPxSummaryItem FieldName="TrnPostStatusDesc" SummaryType="Custom" />
                            </TotalSummary>
                            <ClientSideEvents ContextMenuItemClick="function(s,e) { OnContextMenuItemClick(s, e); }" />
                            <ClientSideEvents ContextMenu="grid_ContextMenu" />                        
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                        <dx:ASPxPopupMenu ID="pmRowMenu" runat="server" ClientInstanceName="pmRowMenu">
                                <Items>
                                    <dx:MenuItem Text="Report" Name="Name">
                                        <Template>
                                        </Template>
                                    </dx:MenuItem>
                                </Items>
                                <ItemStyle Width="270px"></ItemStyle>
                        </dx:ASPxPopupMenu>
                        <dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hiddenfield" />
                    </div>

                    
                </div>  
                
                
                                                                             
            </div>
                   
            </div>
       </div>
 </div>

            <asp:Button ID="Button1" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
                <fieldset class="form" id="fsMain">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">    

                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:Textbox ID="txtTrnTakenDetlNo" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>
                                            
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-7">
                                <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Employee Name :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                                <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                                <ajaxToolkit:AutoCompleteExtender ID="aceEmployee" runat="server"
                                TargetControlID="txtFullName" MinimumPrefixLength="2" EnableCaching="true"                    
                                CompletionSetCount="1" CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="GetRecord" FirstRowSelected="true" UseContextKey="true" />
                                    <script type="text/javascript">
                                        function SplitH(obj, index) {
                                            var items = obj.split("|");
                                            for (i = 0; i < items.length; i++) {
                                                if (i == index) {
                                                    return items[i];
                                                }
                                            }
                                        }
                                        function GetRecord(source, eventArgs) {
                                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                        }
                            </script>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Time In :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtActualIn" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                                    TargetControlID="txtActualIn" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                            
                                    CultureName="en-US" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                    ControlExtender="MaskedEditExtender4x"
                                    ControlToValidate="txtActualIn"
                                    IsValidEmpty="true"
                                    EmptyValueMessage=""
                                    InvalidValueMessage=""
                                    ValidationGroup="Demo1"
                                    Display="Dynamic"
                                    TooltipMessage="" />
                            </div>

                            <label class="col-md-2 control-label">Time Out :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtActualOut" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                    TargetControlID="txtActualOut" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                            
                                    CultureName="en-US" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                    ControlExtender="MaskedEditExtender4x"
                                    ControlToValidate="txtActualOut"
                                    IsValidEmpty="true"
                                    EmptyValueMessage=""
                                    InvalidValueMessage=""
                                    ValidationGroup="Demo1"
                                    Display="Dynamic"
                                    TooltipMessage="" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Certificate Date From :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtCertStartDate" runat="server" CssClass="form-control"></asp:TextBox> 
                                                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                    TargetControlID="txtCertStartDate"
                                    Format="MM/dd/yyyy" />  
                                      
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                    TargetControlID="txtCertStartDate"
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
                                    ControlToValidate="txtCertStartDate"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                                    
                                    <ajaxToolkit:ValidatorCalloutExtender 
                                    runat="Server" 
                                    ID="ValidatorCalloutExtender2"
                                    TargetControlID="RangeValidator2" />                                                                           
                            </div>

                            <label class="col-md-2 control-label has-space">Date To :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtCertEndDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox> 
                                                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                    TargetControlID="txtCertEndDate"
                                    Format="MM/dd/yyyy" />  
                                      
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                    TargetControlID="txtCertEndDate"
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
                                    ControlToValidate="txtCertEndDate"
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

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Service Contract Date From :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtServStartDate" runat="server" CssClass="form-control"></asp:TextBox> 
                                                                    
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                    TargetControlID="txtServStartDate"
                                    Format="MM/dd/yyyy" />  
                                      
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                    TargetControlID="txtServStartDate"
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
                                    ControlToValidate="txtServStartDate"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                                    
                                    <ajaxToolkit:ValidatorCalloutExtender 
                                    runat="Server" 
                                    ID="ValidatorCalloutExtender1"
                                    TargetControlID="RangeValidator1" />                                                                           
                            </div>

                            <label class="col-md-2 control-label has-space">Date To :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtServEndDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox> 
                                                                    
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                    TargetControlID="txtServEndDate"
                                    Format="MM/dd/yyyy" />  
                                      
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                    TargetControlID="txtServEndDate"
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
                                    ControlToValidate="txtServEndDate"
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
                            <label class="col-md-4 control-label has-space">Remarks :</label>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Enrollment Status :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboTrnPreStatusNo" runat="server" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Post Status :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboTrnPostStatusNo" DataMember="ETrnPostStatus" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                    </div>                    
                </fieldset>
            </asp:Panel>

            <asp:Button ID="Button2" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlAppend" runat="server" TargetControlID="Button2" PopupControlID="Panel3" CancelControlID="lnkCloseApp" BackgroundCssClass="modalBackground" />
            <asp:Panel id="Panel3" runat="server" CssClass="entryPopup">
                <fieldset class="form" id="fsMainApp">                    
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="lnkCloseApp" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveApp" OnClick="lnkSaveApp_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
                    </div>                                            
                    <div class="entryPopupDetl form-horizontal">
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label">Position :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboPositionNo" DataMember="EPosition" runat="server" CssClass="form-control" />
                            </div>
                        </div>                      

                        <div class="form-group">
                            <label class="col-md-4 control-label">Division :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboDivisionNo" DataMember="EDivision" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Department :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboDepartmentNo" DataMember="EDepartment" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Section :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboSectionNo" DataMember="ESection" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Unit :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboUnitNo" DataMember="EUnit" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Location :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboLocationNo" DataMember="ELocation" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Employee Class :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label">Gender :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboGenderNo" DataMember="EGender" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                    </div>                    
                </fieldset>
            </asp:Panel>




            <asp:Button ID="btnShowPre" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlShowPre" runat="server" TargetControlID="btnShowPre" PopupControlID="pnlPopupPre" CancelControlID="ImgClosePre" BackgroundCssClass="modalBackground" >
            </ajaxtoolkit:ModalPopupExtender>
            <asp:Panel id="pnlPopupPre" runat="server" CssClass="entryPopup2" style=" display:none;">
                <fieldset class="form" id="Fieldset1">
                    <!-- Header here -->
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="ImgClosePre" CssClass="cancel fa fa-times" ToolTip="Close" />
                        &nbsp;
                        <asp:LinkButton runat="server" ID="lnkSavePre" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSavePre_Click"  />
                    </div>
                    <!-- Body here -->
                    <div  class="entryPopupDetl2 form-horizontal">
                
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Enrollment Status :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboPreStatusNo" runat="server" CssClass="form-control required" />
                            </div>
                        </div>

                    </div>
                    <br />
                </fieldset>
            </asp:Panel>



            <asp:Button ID="btnShowPost" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlShowPost" runat="server" TargetControlID="btnShowPost" PopupControlID="pnlPopupPost" CancelControlID="ImgClosePost" BackgroundCssClass="modalBackground" >
            </ajaxtoolkit:ModalPopupExtender>
            <asp:Panel id="pnlPopupPost" runat="server" CssClass="entryPopup2" style=" display:none;">
                <fieldset class="form" id="Fieldset2">
                    <!-- Header here -->
                    <div class="cf popupheader">
                        <h4>&nbsp;</h4>
                        <asp:Linkbutton runat="server" ID="ImgClosePost" CssClass="cancel fa fa-times" ToolTip="Close" />
                        &nbsp;
                        <asp:LinkButton runat="server" ID="lnkSavePost" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSavePost_Click"  />
                    </div>
                    <!-- Body here -->
                    <div  class="entryPopupDetl2 form-horizontal">
                
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Post Status :</label>
                            <div class="col-md-7">
                                <asp:Dropdownlist ID="cboPostStatusNo" DataMember="ETrnPostStatus" runat="server" CssClass="form-control required" />
                            </div>
                        </div>

                    </div>
                    <br />
                </fieldset>
            </asp:Panel>


</asp:Content>

