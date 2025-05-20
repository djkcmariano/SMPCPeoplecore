<%@ Page Language="VB" AutoEventWireup="false"  Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpProject.aspx.vb" Inherits="Secured_EmpProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" runat="server">
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">                    
                        
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeProjectNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataTextColumn FieldName="Effectivity" Caption="Effectivity Date" />
                            <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                            <dx:GridViewDataTextColumn FieldName="ProjectDesc" Caption="Project" />
                            <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                            <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />
                            <dx:GridViewBandColumn Caption="Employee Rate" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="EmployeeRate" Caption="Monthly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="EmployeeRateD" Caption="Daily" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="EmployeeRateH" Caption="Hourly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </dx:GridViewBandColumn> 
                            <dx:GridViewBandColumn Caption="Billing Rate" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="BillingRate" Caption="Monthly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="BillingRateD" Caption="Daily" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="BillingRateH" Caption="Hourly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </dx:GridViewBandColumn>  
                            <dx:GridViewBandColumn Caption="OT Rate" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="OTRate" Caption="Monthly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="OTRateD" Caption="Daily" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    <dx:GridViewDataTextColumn FieldName="OTRateH" Caption="Hourly" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </dx:GridViewBandColumn>                     
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>


<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeProjectNo" runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber"
                        ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label  has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" EnableCaching="false"
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

                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                         }
                            </script>
                    
                </div>
            </div> 
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Payroll Group :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayclassNo" runat="server" DataMember = "EPayClass" CssClass="required form-control" />
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Project :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboProjectNo" runat="server" DataMember = "EProject" CssClass="required form-control" />
                </div>
            </div> 
 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Position :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPositionNo" runat="server" DataMember="EPosition" CssClass="required form-control" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Department :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboDepartmentNo" runat="server" DataMember="EDepartment" CssClass="form-control" />
                </div>
            </div>                                     
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Effectivity Date :</label>
                <div class="col-md-3">
                      <asp:TextBox ID="txtEffectivity" runat="server" CssClass="required form-control" style="display:inline-block;" placeholder="Effectivity"></asp:TextBox> 
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEffectivity"  Format="MM/dd/yyyy" />  
                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtEffectivity"
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
                        ControlToValidate="txtEffectivity"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                    
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender4"
                        TargetControlID="RangeValidator3" />                                                                           
               </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">EMPLOYEE RATE :</label>
                <div class="col-md-7">
                    
                </div>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Monthly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeRate" runat="server" CssClass="form-control number" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Daily :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeRateD" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Hourly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeRateH" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">BILLING RATE :</label>
                <div class="col-md-7">
                    
                </div>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Monthly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBillingRate" runat="server" CssClass="form-control number" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Daily :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBillingRateD" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Hourly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtBillingRateH" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">OT RATE :</label>
                <div class="col-md-7">
                    
                </div>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Monthly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOTRate" runat="server" CssClass="form-control number" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Daily :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOTRateD" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Hourly :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtOTRateH" runat="server" CssClass="form-control number" />
                </div>
            </div>  
            
        </div>
        <!-- Footer here -->
         
   </fieldset>
</asp:Panel>


</asp:Content>

