<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpEI_Clearance.aspx.vb" Inherits="Secured_EmpEI_Clearance" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<br />
    <div class="page-content-wrap">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" Visible="true">
                           <ContentTemplate>
                                <ul class="panel-controls">
                                    
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" />
                                        <asp:LinkButton runat="server" ID="lnkCleared" OnClick="lnkCleared_Click" Text="Cleared" CssClass="control-primary" />
                                        <li><asp:LinkButton runat="server" ID="lnkAddMain" OnClick="lnkAddMain_Click" Text="Add" CssClass="control-primary" Visible="False"/></li>                            
                                        <li><asp:LinkButton runat="server" ID="lnkDeleteMain" OnClick="lnkDeleteMain_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                        <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteMain" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                        <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?"  MessageType="Post" />
                                        <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkCleared" ConfirmMessage="Cleared record cannot be modified, Proceed?"  MessageType="Post" />
                                    </li>
                                </ul>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExport" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                    </div>
                </div>
                <div class="panel-body">
                    <%--<div class="row">
                        <asp:GridView runat="server" ID="grdM" DataSourceID="SqlDataSource1" AutoGenerateColumns="true" EmptyDataText="no record found" DataKeyNames="EmployeeNo" />
                    </div>--%>
                    <div class="row">
                        <div class="table-responsive">
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeEIClearanceMainNo" OnCustomButtonCallback="lnkEditMain_Click">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditMain" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeEIClearanceMainNo") %>' OnClick="lnkEditMain_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="CodeMain" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                    <dx:GridViewDataTextColumn FieldName="Effectivity" Caption="Resignation Date" />
                                    <dx:GridViewDataTextColumn FieldName="PostedDate" Caption="Posted Date" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Form" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPrint" CssClass="fa fa-print" OnClick="lnkPrint_Click" Font-Size="Medium" OnPreRender="lnkPrint_PreRender"/>
                                    </DataItemTemplate>
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                </Columns>
                                <SettingsSearchPanel Visible="false" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                                        
                            <asp:SqlDataSource runat="server" ID="SqlDataSource1">
                                <%--<SelectParameters>
                                    <asp:Parameter Name="OnlineUserNo" Type="Int32" DefaultValue="-99" />
                                    <asp:Parameter Name="PayLocNo" Type="Int32" DefaultValue="0" />
                                    <asp:Parameter Name="TabIndex" Type="Int32" DefaultValue="1" />
                                </SelectParameters>--%>
                            </asp:SqlDataSource>                                                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <%--<div class="col-md-7">
                            <h4 class="panel-title">Reference No.: <asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
                        </div>--%>
                        <div class="col-md-6 panel-title">
                            <asp:Label runat="server" ID="lblDetl" />
                        </div>
                        <div>                                                
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkGenerate_Click" Text="Generate Checklist" CssClass="control-primary" /></li>                                    
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="EmployeeEIClearanceNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeEIClearanceNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="IfullName" Caption="Department Head" />
                                        <dx:GridViewDataTextColumn FieldName="DateReturned" Caption="Date Cleared" /> 
                                        <dx:GridViewDataTextColumn FieldName="InChargeName" Caption="Focal Person" />
                                        <dx:GridViewDataTextColumn FieldName="FcDateReturned" Caption="Date Cleared" /> 
                                        <dx:GridViewDataTextColumn FieldName="EmployeeClearanceTypeDesc" Caption="Item Accountability" />
                                        <dx:GridViewDataTextColumn FieldName="Amount" Caption="Item Amount" />
                                        <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" /> 
                                        <dx:GridViewDataTextColumn FieldName="UpdateByName" Caption="Processed By" />                                                                                 
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>                                
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>                                       
      
 
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">    
             
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtEmployeeEIClearanceNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Department Head :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtIfullName" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifImmediateSuperiorNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                        TargetControlID="txtIfullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="1" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                            <script type="text/javascript">
                                function getRecord1(source, eventArgs) {
                                    document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = eventArgs.get_value();
                                }
                            </script>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Focal Person :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtInChargeName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifInChargeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"  
                        TargetControlID="txtInChargeName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="1" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord2" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                            <script type="text/javascript">
                                function getRecord2(source, eventArgs) {
                                    document.getElementById('<%= hifInChargeNo.ClientID %>').value = eventArgs.get_value();
                               }
                            </script>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Item Accountability :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboEmployeeEIClearanceTypeNo" CssClass="form-control required" DataMember="EEmployeeEIClearanceType" runat="server"></asp:DropdownList>
                    </div>
                </div>

                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Amount :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control number" ></asp:TextBox>   
                       <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtAmount" FilterType="Numbers, Custom" ValidChars=".-" />--%> 
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Remarks :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </div>
                </div>  

                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-2">
                        <asp:CheckBox ID="txtIsReturned" runat="server" Text="&nbsp;Focal Person" ></asp:CheckBox>
                    </div>
                        <asp:TextBox ID="txtFcDateReturned" SkinID="txtdate" runat="server" CssClass="form-control" style="display:inline-block;" placeholder="Date Cleared"></asp:TextBox> 
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFcDateReturned"  Format="MM/dd/yyyy" />         
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                            TargetControlID="txtFcDateReturned"
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
                            ControlToValidate="txtFcDateReturned"
                            ErrorMessage="<b>Please enter valid entry</b>"
                            MinimumValue="1900-01-01"
                            MaximumValue="3000-12-31"
                            Type="Date" Display="None"  />
                                    
                            <ajaxToolkit:ValidatorCalloutExtender 
                            runat="Server" 
                            ID="ValidatorCalloutExtender1"
                            TargetControlID="RangeValidator3" /> 
                </div> 

                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-2">
                        <asp:CheckBox ID="txtIsCleared" runat="server" Text="&nbsp;Dept. Head" ></asp:CheckBox>
                    </div>
                        <asp:TextBox ID="txtDateReturned" SkinID="txtdate" runat="server" CssClass="form-control" style="display:inline-block;" placeholder="Date Cleared"></asp:TextBox> 
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateReturned"  Format="MM/dd/yyyy" />         
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                            TargetControlID="txtDateReturned"
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
                            ControlToValidate="txtDateReturned"
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
        </fieldset>
    </asp:Panel>

    <asp:Button ID="Button2" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="lnkCloseMain" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="Fieldset1">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkCloseMain" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveMain" OnClick="lnkSaveMain_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">    
             
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtEmployeeEIClearanceMainNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCodeMain" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Employee Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="1" 
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

            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>